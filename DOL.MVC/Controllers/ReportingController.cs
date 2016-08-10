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
    public class ReportingController : Controller
    {

       private readonly IRepositoryAsync<users> _usersRepositoryAsync;
       private readonly IRepositoryAsync<user_profile> _userProfileRepositoryAsync;
       private readonly IRepositoryAsync<rpt_spendReport> _Report_Spend_RepositoryAsync;
       private readonly IRepositoryAsync<client_organization> _ClientOrganizationRepositoryAsync;


        private readonly IUnitOfWorkAsync _unitOfWorkAsync;


        public ReportingController(IUnitOfWorkAsync unitOfWorkAsync,
              IRepositoryAsync<users> accountRepositoryAsync, 
              IRepositoryAsync<user_profile> userProfileRepositoryAsync,
              IRepositoryAsync<client_organization> ClientOrganizationRepositoryAsync,
              IRepositoryAsync<rpt_spendReport> Report_Spend_RepositoryAsync
            )
            {
           
            _usersRepositoryAsync = accountRepositoryAsync;
            _userProfileRepositoryAsync = userProfileRepositoryAsync;
            _Report_Spend_RepositoryAsync = Report_Spend_RepositoryAsync;
            _ClientOrganizationRepositoryAsync = ClientOrganizationRepositoryAsync;

            _unitOfWorkAsync = unitOfWorkAsync;
            }
        // GET: Reporting
        [HttpPost]
        public ActionResult Index()
        {
         
         


            Session["ReportPath"] = string.Empty;
            Session["RDLC_Dataset_Name"] = string.Empty;
            
            return View();
        }


        public ActionResult GenerateReport()
        {
            // Obviously you apply the parameters as predicates and hit the real database


            var userList = _userProfileRepositoryAsync
                .GetRepository<users>()
                .Query(a => a.user_role == 200 && a.status == "A")
                .Select()
                .ToList();


            Session["ReportPath"] = "Reports\\rptUsers.rdlc";
            Session["RDLC_Dataset_Name"] = "UsersDataset";
            Session["ReportData"] = userList;
            ViewBag.ShowIFrame = true;
            return View();
        }

        public ActionResult Display_Report()
        {

            //ViewBag.SelectedClient = "208660";

             string strDateSelected = Request.Form["InvoiceStartDate"];

            ViewBag.SelectedClient = Request.Form["ClientSelected"];
            ViewBag.SortCenter = ViewBag.SelectedClient;
            ViewBag.DateFrom = strDateSelected;


            
            //string strSPtobeExecuted = "rpt_spendReport '" + ViewBag.SelectedClient + "','04/01/2016'";

            string strSPtobeExecuted = "rpt_spendReport '" + ViewBag.SelectedClient + "','" + strDateSelected + "'";


            var lstReportSP = Get_SP_Result(strSPtobeExecuted);



            var listReportSpend = (from p in lstReportSP
                                   select new rpt_SpendReportModels
                                   {
                                       Client_Name = p.Field<String>("Client_Name"),
                                       Dept_Name = p.Field<String>("Dept_Name"),
                                       client_id = p.Field<Int32>("client_id"),
                                       invoice_number = p.Field<String>("invoice_number"),
                                       invoice_date = p.Field<DateTime>("invoice_date"),
                                       order_number = p.Field<Int64>("order_number"),
                                       timesheet_number = p.Field<String>("timesheet_number"),
                                       line_description = p.Field<String>("line_description"),
                                       hours_worked = p.Field<Double>("hours_worked"),
                                       hourly_bill_rate = p.Field<decimal>("hourly_bill_rate"),
                                       billing_amt = p.Field<decimal>("billing_amt")

                                   }).ToList();


            Session["ReportPath"] = "Reports\\rptSpend.rdlc";
            Session["RDLC_Dataset_Name"] = "SpendReport";
            Session["ReportData"] = listReportSpend;
            ViewBag.ShowIFrame = true;


            ViewBag.ReportSource = "..//Reports//ReportViewer.aspx";

            ViewData["tempClientList"] = Bind_Client_Org();

            return View("Spend_Report");


        }

       
        public ActionResult Spend_Report()
        {


            if (Request.HttpMethod != "POST")
            {
                ViewBag.SelectedClient = 0;
                ViewData["tempClientList"] = Bind_Client_Org();
            }

          

            return View();
        }
        







        private List<DataRow> Get_SP_Result(string _StoredProc_Name)
        {


            string strQry = string.Empty;


            List<DataRow> list = null;

            strQry = "EXEC  " + _StoredProc_Name ;
              
            

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





        //** LOOKUP CLIENT FILTER  **\\
        public List<ClientOrganizationModels> Bind_Client_Org()
        {
            var lstDefaultValues = new ClientOrganizationModels();


            var lstClients = _ClientOrganizationRepositoryAsync
            .GetRepository<client_organization>()
            .Query(a => a.modified_on.Value.Year >= ( DateTime.Today.Year - 2 )  && a.parent_group_id == -1)
            .Select(d => new ClientOrganizationModels()
            {
                group_id = d.group_id,
                client_org_name = d.client_org_name,
            })
            .ToList();

            lstClients = lstClients.OrderBy(x => x.client_org_name).ToList();

            if (lstClients.Count == 0)
            {
                lstDefaultValues.group_id = 0;
                lstDefaultValues.client_id = 0;
                lstDefaultValues.parent_group_id = 0;
                lstDefaultValues.client_org_name = "";

                lstClients.Add(lstDefaultValues);
            }

            return lstClients;

        }


    }
}