using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using DOL.Entities.Models;
using DOL.MVC.Helpers;
using DOL.MVC.Models;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Data.Objects.SqlClient;
using System.Configuration;


namespace DOL.MVC.Controllers
{
    public class TimesheetController : BaseController
    {

        private readonly IRepositoryAsync<TimeSheetView> _TimesheetRepositoryAsync;
        private readonly IRepositoryAsync<Timesheet_orders> _TimesheetOrdersRepositoryAsync;
        private readonly IRepositoryAsync<vw_client_timesheets> _TimesheetDetailsRepositoryAsync;
        private readonly IRepositoryAsync<Lookup_Master> _LookupMasterRepositoryAsync;
        private readonly IRepositoryAsync<Timesheet_Details> _DetailsTimeSheetRepositoryAsync;
        private readonly IRepositoryAsync<client_organization> _ClientOrganizationRepositoryAsync;
        private readonly IRepositoryAsync<client_users> _ClientUsersRepositoryAsync;
        private readonly IRepositoryAsync<timeclock_transform> _TimeClockTransformRepositoryAsync;

        private readonly IRepositoryAsync<users> _usersRepositoryAsync;
        private readonly IRepositoryAsync<user_profile> _userProfileRepositoryAsync;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;


        public TimesheetController(IUnitOfWorkAsync unitOfWorkAsync,
            IRepositoryAsync<TimeSheetView> TimeSheetViewRepositoryAsync, 
            IRepositoryAsync<Timesheet_orders> TimeSheetOrdersRepositoryAsync,
            IRepositoryAsync<vw_client_timesheets> TimeSheetDetailsRepositoryAsync,
            IRepositoryAsync<Lookup_Master>  LookupMasterRepositoryAsync,
            IRepositoryAsync<Timesheet_Details>  DetailsTimeSheetRepositoryAsync,
            IRepositoryAsync<client_organization>  ClientOrganizationRepositoryAsync,
            IRepositoryAsync<client_users> ClientUsersRepositoryAsync,
             IRepositoryAsync<timeclock_transform> TimeClockTransformRepositoryAsync,
              IRepositoryAsync<users> accountRepositoryAsync, 
              IRepositoryAsync<user_profile> userProfileRepositoryAsync
            )
        {
            _TimesheetRepositoryAsync = TimeSheetViewRepositoryAsync;
            _TimesheetOrdersRepositoryAsync = TimeSheetOrdersRepositoryAsync;
            _TimesheetDetailsRepositoryAsync = TimeSheetDetailsRepositoryAsync;
            _LookupMasterRepositoryAsync = LookupMasterRepositoryAsync;
            _DetailsTimeSheetRepositoryAsync = DetailsTimeSheetRepositoryAsync;
            _ClientOrganizationRepositoryAsync = ClientOrganizationRepositoryAsync;
            _ClientUsersRepositoryAsync = ClientUsersRepositoryAsync;
            _TimeClockTransformRepositoryAsync = TimeClockTransformRepositoryAsync;

            _usersRepositoryAsync = accountRepositoryAsync;
            _userProfileRepositoryAsync = userProfileRepositoryAsync;

            _unitOfWorkAsync = unitOfWorkAsync;
        }








        [HttpPost]
        public ActionResult Search_TimeSheet(FormCollection form)
        {

            TimesheetFilterModel objFilterModel = new TimesheetFilterModel();

        

            var clients_id = String.IsNullOrWhiteSpace(form["ClientSelected"].ToString()) ? 0 : Convert.ToInt32(form["ClientSelected"].ToString().Replace(",", ""));

            var other_clients_id = String.IsNullOrWhiteSpace(form["OthersSelected"].ToString()) ? 0 : Convert.ToInt32(form["OthersSelected"].ToString().Replace(",", ""));
       
            
            // Binding Others

              objFilterModel.List_Others = Bind_Client_Org(clients_id); ;
              objFilterModel.ClientSelected = clients_id;
              objFilterModel.OtherSelected = other_clients_id;

              ViewData["tempOtherList"]= objFilterModel.List_Others;


              if (form["TimeSheetNo"].ToString() != string.Empty)
              {
                  objFilterModel.TimeSheetNo = form["TimeSheetNo"].ToString();
              }
            

            if (form["orderStartDate"].ToString() != string.Empty && form["orderFinishDate"].ToString() != string.Empty)
            {

                objFilterModel.OrderDateFrom = Convert.ToDateTime(DateTime.ParseExact(form["orderStartDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToShortDateString());
                objFilterModel.OrderDateTo = Convert.ToDateTime(DateTime.ParseExact(form["orderFinishDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToShortDateString());

            }

            TempData["TimeSheetFilterModel"] = objFilterModel;// Assign to Temp Data

            Session["FilterModel"] = objFilterModel;

          
        
            return RedirectToAction("TimesheetClientViewIndex", "Timesheet"); // Reload with Filter
        }


        //***  VIEW PER - TIMESHEET# DETAILS **\\
        public ActionResult ViewETdetails(Int64? id)
        {
        
            ViewBag.TimeSheetNumber = "ET" + id.ToString();
            System.Web.HttpContext.Current.Session["TimeSheetNo"] = id.ToString();


            var ETdetails = _TimesheetDetailsRepositoryAsync
                .GetRepository<vw_client_timesheets>()
                .Query(a => a.Timesheet_Number == id)
                .Select(d => new TimesheetDetailsModels()
                {


                    Timesheet_Number = d.Timesheet_Number,
                    Timesheet_Serial_Number = d.Timesheet_Serial_Number,
                    Shift_date = d.Shift_date,
                    TS_start_date = d.TS_start_date,
                    TS_end_date = d.TS_end_date,
                    contractor_id = d.contractor_id,
                    Actual_start_time = d.Actual_start_time,
                    Actual_end_time = d.Actual_end_time,
                    Actual_break_start = d.Actual_break_start,
                    Actual_break_end = d.Actual_break_end,
                    leave_reason = d.leave_reason,
                    Total_Hours = d.Total_Hours,
                    to_status = d.to_status,
                    comment_supervisor = d.comment_supervisor,
                })
                .ToList();


          
                var totalHours = ETdetails.Sum(t => t.Total_Hours);
                ViewBag.TotalHours = totalHours.ToString();
                ViewBag.CommentSupervisor = String.IsNullOrEmpty(ETdetails[0].comment_supervisor) ? string.Empty : ETdetails[0].comment_supervisor.ToString();
          
            //BIND LOOKUP MASTER  - LEAVE REASONS
             ViewData["tempEmpList"] = Bind_LookupMaster("LEAVE_REASON");

          
            return PartialView("ViewETdetails", ETdetails);
            //return View(ETdetails);

        
        }


        //***  VIEW PER - TIMESHEET# DETAILS FROM KRONOS ( TIMECLOCKTRIGGER )**\\
        public ActionResult ViewETdetailspartial(Int64? id)
        {

            ViewBag.TimeSheetNumber = "ET" + id.ToString();
            System.Web.HttpContext.Current.Session["TimeSheetNo"] = id.ToString();


            var ETdetails = _TimeClockTransformRepositoryAsync
                .GetRepository<timeclock_transform>()
                .Query(a => a.Timesheet_Number == id)
                .Select(d => new TimeclockTransformDetailsModels()
                {


                    Timesheet_Number = d.Timesheet_Number,
                 //   Timesheet_Serial_Number = d.Timesheet_Serial_Number,
                    Shift_date = d.Clock_Date,
                    TS_start_date = d.Time_In,
                    TS_end_date = d.Time_Out,
                    contractor_id = d.contractor_id,
                    Actual_start_time = d.Time_In,
                    Actual_end_time = d.Time_Out,
                    Actual_break_start = d.Break_In,
                    Actual_break_end = d.Break_Out,
                //    leave_reason = d.leave_reason,
                    Total_Hours = d.Daily_Hours,
                  //  to_status = d.to_status,
                  //  comment_supervisor = d.comment_supervisor,
                })
                .ToList();
            //


            var totalHours = ETdetails.Sum(t => t.Total_Hours);
            ViewBag.TotalHours = totalHours.ToString();
           // ViewBag.CommentSupervisor = String.IsNullOrEmpty(ETdetails[0].comment_supervisor) ? string.Empty : ETdetails[0].comment_supervisor.ToString();

            ViewBag.CommentSupervisor = string.Empty;
            //BIND LOOKUP MASTER  - LEAVE REASONS
            ViewData["tempEmpList"] = Bind_LookupMaster("LEAVE_REASON");


            return PartialView("ViewETdetailspartial", ETdetails);
            //return View(ETdetails);


        }



        //** LOOKUP CLIENT FILTER  **\\
        public List<ClientOrganizationModels> Bind_Client_Org(Int32 Group_Id)
        {
            var lstDefaultValues = new ClientOrganizationModels();


            var lstClients = _ClientOrganizationRepositoryAsync
            .GetRepository<client_organization>()
            .Query(a => a.parent_group_id == Group_Id)
            .Select(d => new ClientOrganizationModels()
            {
                group_id = d.group_id,
                client_org_name = d.client_org_name,
            })
            .ToList();

            lstClients = lstClients.OrderBy(x => x.client_org_name).ToList();

            if(lstClients.Count ==0 )
            {
                lstDefaultValues.group_id =0;
                lstDefaultValues.client_id =0;
                lstDefaultValues.parent_group_id =0;
                lstDefaultValues.client_org_name = "";

                lstClients.Add(lstDefaultValues);
            }

            return lstClients;

        }
        

        //** LOOKUP MASTER BINDING **\\
        public List<LookupMasterModels> Bind_LookupMaster(string LOOKUP_TYPE)
        {
            var lstLeaveReasons = _LookupMasterRepositoryAsync
            .GetRepository<Lookup_Master>()
            .Query(a => a.lookup_type == LOOKUP_TYPE)
            .Select(d => new LookupMasterModels()
            {
                lookup_id = d.lookup_id,
                lookup_value = d.lookup_value,
            })
            .ToList();



             return lstLeaveReasons;

        }

       
        public List<String> ConcatTime(string _shiftDates, string _timeChanges)
        {
            List<String> lstTimeResult = new List<String>();

            List<string> lstShiftDates = _shiftDates.Split(',').ToList<string>();
            List<string> lstTimeChanges = _timeChanges.Split(',').ToList<string>();

         //   string strMaxFormat = dateTime.ToString("HH:mm:ss tt");//24 hours format

            for (int i = 0; i <= 6; i++)
            {
                if (lstTimeChanges[i].ToString() == string.Empty)
                {
                    lstTimeResult.Add(string.Empty);
                }
                else
                { 
                lstTimeResult.Add(Convert.ToDateTime(Convert.ToDateTime(lstShiftDates[i]).ToShortDateString() + " " + lstTimeChanges[i].ToString()).ToString("MM/dd/yyyy HH:mm:ss tt"));
                }
            }





            return lstTimeResult;
        }

       [HttpPost]
        public ActionResult UpdateTimesheetClientDetails(FormCollection form)
        {

              var _Shift_Date = form["item.Shift_date"];
              DateTime? nullDateTime = null; 
           

              
                 var _Timesheet_Serial_Number = form["item.Timesheet_Serial_Number"];
                 List<string> lstSerialNo = _Timesheet_Serial_Number.Split(',').ToList<string>();

                var _Actual_Start_Time = form["item.Actual_start_time"];
                List<String> lstActualStart = ConcatTime(_Shift_Date, _Actual_Start_Time);
                var _Actual_End_Time = form["item.Actual_End_Time"];
                List<String> lstActualEnd = ConcatTime(_Shift_Date, _Actual_End_Time);

                var _Actual_break_start = form["item.Actual_break_start"];
                List<String> lstActualBreakStart = ConcatTime(_Shift_Date, _Actual_break_start);
                var _Actual_break_end = form["item.Actual_break_end"];
                List<String> lstActualBreakEnd = ConcatTime(_Shift_Date, _Actual_break_end);

                var _LeaveReason = form["item.SelectedReason"];
                List<string> _lstLeaveReason = _LeaveReason.Split(',').ToList<string>();  

                 var _HoursperDay= form["item.Total_Hours"];
                List<string> _lstHoursperDay = _HoursperDay.Split(',').ToList<string>();  


                var _SupComment = form["item.txtSupComment"];
                Double  _GrandTotalHours = 0;
           

                Int64 TimeSheetNo = Convert.ToInt64(System.Web.HttpContext.Current.Session["TimeSheetNo"]);


                for (int i = 0; i <= 6; i++)
                {
           
                    double  _TotalHours =0;
                    Int64 TimeSheetSerial =  Convert.ToInt64(lstSerialNo[i]);
                 
                    //var Timesheet_Details = _DetailsTimeSheetRepositoryAsync
                    //   .Query(vw => vw.Timesheet_Number == TimeSheetNo && vw.Timesheet_Serial_Number == TimeSheetSerial)
                    //   .Select()
                    //   .SingleOrDefault();

                    var Timesheet_Details = _DetailsTimeSheetRepositoryAsync.Find(TimeSheetSerial);
                    if (Timesheet_Details == null) return HttpNotFound();



                    //Timesheet_Details.Actual_start_time = Convert.ToDateTime(lstActualStart[i]);

                    Timesheet_Details.Actual_start_time = String.IsNullOrWhiteSpace(lstActualStart[i].ToString()) ? nullDateTime : Convert.ToDateTime(lstActualStart[i].ToString());
                    Timesheet_Details.Actual_end_time = String.IsNullOrWhiteSpace(lstActualEnd[i].ToString()) ? nullDateTime : Convert.ToDateTime(lstActualEnd[i].ToString());
                    Timesheet_Details.Actual_break_start = String.IsNullOrWhiteSpace(lstActualBreakStart[i].ToString()) ? nullDateTime : Convert.ToDateTime(lstActualBreakStart[i].ToString());
                    Timesheet_Details.Actual_break_end = String.IsNullOrWhiteSpace(lstActualBreakEnd[i].ToString()) ? nullDateTime : Convert.ToDateTime(lstActualBreakEnd[i].ToString()); 
                    Timesheet_Details.leave_reason = Convert.ToInt32(String.IsNullOrWhiteSpace(_lstLeaveReason[i].ToString()) ? "0" : _lstLeaveReason[i].ToString());
                
                    //   Timesheet_Details.to_status ='A'; //Approved'

                    _DetailsTimeSheetRepositoryAsync.Update(Timesheet_Details);
                    _unitOfWorkAsync.SaveChanges();


                    //Validate the Values if NULL 
                   if(Timesheet_Details.Actual_start_time != null || Timesheet_Details.Actual_end_time != null)
                   {
                        _TotalHours = (DateTime.Parse(Timesheet_Details.Actual_end_time.ToString()) - DateTime.Parse(Timesheet_Details.Actual_start_time.ToString())).TotalHours;
                   }
                    //** Deaduct the Breaks **
                   if (Timesheet_Details.Actual_break_end != null || Timesheet_Details.Actual_break_start != null)
                   {
                       _TotalHours = _TotalHours - (DateTime.Parse(Timesheet_Details.Actual_break_end.ToString()) - DateTime.Parse(Timesheet_Details.Actual_break_start.ToString())).TotalHours;
                   }

                   _GrandTotalHours = _GrandTotalHours  + _TotalHours; // Sum the Hours

                }

                //Update the Table - timesheet_orders
                 var Timesheet_Orders = _TimesheetOrdersRepositoryAsync.Find(TimeSheetNo);
                // Timesheet_Orders.Approved_by = HttpContext.User.Identity.Name;
                 Timesheet_Orders.No_of_Hours = Math.Round(_GrandTotalHours,2);
                 Timesheet_Orders.Comment_Supervisor = _SupComment.ToString().Trim();

                 _unitOfWorkAsync.SaveChanges();




                return RedirectToAction("TimesheetClientViewIndex", "TimeSheet");


        }

       public ActionResult Report_Timesheet()
       {

           var _TimeSheetVIewModel = new TimesheetViewModels();
           var _listTimeSheetView = Session["_listTimeSheetView"] as List<DataRow>;


           var newItems = (from p in _listTimeSheetView
                           select new TimesheetViewModels
                           {
                               Timesheet_Number = p.Field<Int64>("timesheet_number"),
                               Order_Start_Date = p.Field<DateTime>("o_start_date"),
                               Order_Finish_Date = p.Field<DateTime>("o_end_date"),
                               Contractor_FullName = p.Field<String>("last_name") + ", " + p.Field<String>("first_name"),
                               Organization_Level = p.Field<String>("dept"),
                               Contractor_Title = p.Field<String>("title_name"),
                               Approval_Status = p.Field<String>("to_status"),
                               Approval_Status_Description = GetStatusDescription(p.Field<String>("to_status")),
                               No_of_Hours = p.Field<double?>("no_of_hours")
                           }).ToList();

         //  return View(Session["IndexView"]);
           return PartialView("Report_Timesheet", newItems);
           

       }

        


        // GET: /Document/
        public ActionResult TimesheetClientViewIndex()
        {
            var objfilterModel = Session["FilterModel"] as TimesheetFilterModel; // Getting the TempData Filter Model


            if (objfilterModel == null)
            { 
            //BIND CLIENTS - FILTER
             var ClientUser = _ClientUsersRepositoryAsync
              .Query(x => x.user_id == HttpContext.User.Identity.Name)
              .Select()
              .SingleOrDefault();

                var currentClientUserID = ClientUser.group_id;

            ViewData["tempClientList"] = Bind_Client_Org(currentClientUserID);
            ViewData["tempOtherList"] = Bind_Client_Org(0);

            Session["tempClientList"] = ViewData["tempClientList"];


            }

            else
            {

               ViewData["tempClientList"] = Session["tempClientList"];
               ViewData["tempOtherList"] = objfilterModel.List_Others;

               ViewBag.SelectedClient = objfilterModel.ClientSelected;
               ViewBag.SelectedClientOther = objfilterModel.OtherSelected;
            }


            var _listTimeSheetView = GetTimeSheetView();
            
            Session["_listTimeSheetView"] = _listTimeSheetView;


          //  var items = GetCustomers();

            var newItems = (from p in _listTimeSheetView
                            select new TimesheetViewModels
                            {
                                Timesheet_Number = p.Field<Int64>("timesheet_number"),
                                Order_Start_Date = p.Field<DateTime>("o_start_date"),
                                Order_Finish_Date = p.Field<DateTime>("o_end_date"),
                                Contractor_FullName = p.Field<String>("last_name") + ", " + p.Field<String>("first_name"),
                                Organization_Level = p.Field<String>("dept"),
                                Contractor_Title = p.Field<String>("title_name"),
                                Approval_Status = p.Field<String>("to_status"),
                                Approval_Status_Description =GetStatusDescription(p.Field<String>("to_status")),
                                No_of_Hours = p.Field<double?>("no_of_hours")
                            }).ToList();


            Session["IndexView"] = newItems;

            return View(newItems);



         
        
        }



        private List<DataRow> GetTimeSheetView()
        {


            string strQry = string.Empty;

            var _Filtermodel = TempData["TimeSheetFilterModel"] as TimesheetFilterModel; // Getting the TempData ChartFilter Model

            string TimeSheetNumber = string.Empty;
            //string week_TS_DateFrom = DateTime.Today.AddDays(-7).ToShortDateString();
            //string week_TS_DateTo =DateTime.Today.ToShortDateString();

            string week_TS_DateFrom = DateTime.Today.AddDays(-7 - Convert.ToInt16(DateTime.Today.DayOfWeek) + 1).ToShortDateString();
            string week_TS_DateTo = DateTime.Today.AddDays(-Convert.ToInt16(DateTime.Today.DayOfWeek)).ToShortDateString();


         

            string _strTimeSheetNoFilter = string.Empty;
            string _strDateRangeFilter = string.Empty;
            Int32 _ClientGroupID = 0;



            List<DataRow> list = null;

           
         
          if (_Filtermodel != null) //Set the Filters
            {
                if (_Filtermodel.TimeSheetNo != null) { _strTimeSheetNoFilter = " AND timesheet_number ='" + _Filtermodel.TimeSheetNo + "' "; }
                if(_Filtermodel.OrderDateFrom != null && _Filtermodel.OrderDateTo !=null )
                {
                    week_TS_DateFrom = Convert.ToDateTime(_Filtermodel.OrderDateFrom).ToShortDateString();
                    week_TS_DateTo = Convert.ToDateTime(_Filtermodel.OrderDateTo).ToShortDateString();
                 
                }

              if(_Filtermodel.OtherSelected ==0)
              {
                  _ClientGroupID = _Filtermodel.ClientSelected;
              }
              else { _ClientGroupID = _Filtermodel.OtherSelected; }


            }
       
       
          //_strDateRangeFilter = " AND  ts_start_date>= CONVERT(CHAR(10), CONVERT(DATETIME, LEFT('" + week_TS_DateFrom + "',10),105), 101) AND ts_start_date<= CONVERT(CHAR(10),CONVERT(DATETIME,LEFT('" + week_TS_DateTo + "',10),105), 101) ";
       
        




            ViewBag.Periods = " From : " + week_TS_DateFrom + " To : " + week_TS_DateTo;
            var username = HttpContext.User.Identity.Name;

            string week_TS_DateFrom_Now = DateTime.Now.AddDays(-7 - Convert.ToInt16(DateTime.Now.DayOfWeek) + 1).ToShortDateString();
            string week_TS_DateTo_Now = DateTime.Now.AddDays(-Convert.ToInt16(DateTime.Now.DayOfWeek)).ToShortDateString();

          
                _strDateRangeFilter = " AND  ts_start_date>='" + week_TS_DateFrom + "' AND ts_start_date<='" + week_TS_DateTo + "' ";


                 strQry = "SELECT distinct timesheet_number,no_of_hours,invoice_charge,group_id,contractor_id,first_name,last_name, to_status,order_id,parent_order_id,order_type,o_start_date,o_end_date,dept,title_name,role_name,owner_Consultant,comment_supervisor,comment_payroll, completed ,grade_name   ";
                strQry += "FROM vw_client_timesheets  where 1=1 AND ((parent_order_id > 0 AND order_type = 102) or (parent_order_id >= -1 And order_type <> 102))  ";
                strQry += _strTimeSheetNoFilter;
                if (_strTimeSheetNoFilter == string.Empty) { strQry += _strDateRangeFilter; }
                strQry += " And group_id in (SELECT c.group_id  From client_users_permission c where c.user_id= '" + username + "' ";
                strQry += " AND  (((c.access_level & 8 )=8 ) OR ((c.traverse_permission & 8 )= 8 ))) ";


                if (_Filtermodel != null)
                {
                    if (_Filtermodel.ClientSelected > 0 || _Filtermodel.OtherSelected > 0)
                    {
                        strQry += " AND  group_id IN  (SELECT group_id FROM client_organization  WHERE ','+link_string+',' LIKE '%," + _ClientGroupID + ",%'   OR group_id = " + _ClientGroupID + ")";
                    }
                }

                strQry += " Order by timesheet_number,parent_order_id,o_start_date desc ";


      



         


         

//(SELECT group_id FROM client_organization   WHERE ','+link_string+',' LIKE '%,2010882,%' OR group_id = 2010882) 


            string connstr = ConfigurationManager.ConnectionStrings["DOLDataContext"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connstr))
            {


                using (SqlCommand objCommand = new SqlCommand(strQry, conn))
                {
                    objCommand.CommandType = CommandType.Text;
                    DataTable dt = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(objCommand);
                    conn.Open();
                    adp.Fill(dt);
                    if (dt != null)
                    {
                        list = dt.AsEnumerable().ToList();
                    }
                }
            }
            return list;
        }


      
        public ActionResult UpdateTimeSheetDetails(Int64? id, string status)
        {
            //if (!ModelState.IsValid) return View(model);
            var Timesheet_Orders = _TimesheetOrdersRepositoryAsync.Find(id);
            if (Timesheet_Orders == null) return HttpNotFound();

            Timesheet_Orders.Approved_by = HttpContext.User.Identity.Name;
            Timesheet_Orders.Approved_on = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            Timesheet_Orders.Status = status;


            _TimesheetOrdersRepositoryAsync.Update(Timesheet_Orders);
            _unitOfWorkAsync.SaveChanges();


            return RedirectToAction("TimesheetClientViewIndex", "TimeSheet");
        }





      
        public ActionResult UpdateTimeSheet(Int64? id , string status)
        {
            //if (!ModelState.IsValid) return View(model);
            var Timesheet_Orders = _TimesheetOrdersRepositoryAsync.Find(id);
            if (Timesheet_Orders == null) return HttpNotFound();

            Timesheet_Orders.Approved_by = HttpContext.User.Identity.Name;
            Timesheet_Orders.Approved_on = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            Timesheet_Orders.Status = status;


            _TimesheetOrdersRepositoryAsync.Update(Timesheet_Orders);
            _unitOfWorkAsync.SaveChanges();


            return RedirectToAction("TimesheetClientViewIndex", "TimeSheet");
        }


        private string GetStatusDescription(string _Aproval_Status)
        {
            string retStatus = string.Empty;
            switch (_Aproval_Status)
            {
                case "A": retStatus = "Approved";
                    break;
                case "N": retStatus ="Not Appproved";
                    break;
                case "Y": retStatus = "Approved";
                    break;
                case "P": retStatus = "Processed";
                    break;
                case "E": retStatus = "Sent to Payroll/Billing";
                    break;
                case "S": retStatus = "Selected for Payroll/Billing";
                    break;
           
                default:
                    retStatus = "No Status";
                    break;
            }

            return retStatus;
        }
     


        // GET: Timesheet
        public ActionResult Index()
        {
            return View();
        }
    }
}