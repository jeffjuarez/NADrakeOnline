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


namespace DOL.MVC.Controllers
{
    public class MaintenanceController : BaseController
    {
      
         private readonly IRepositoryAsync<Bankholidays> _bankHolidayRepositoryAsync;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;


        public MaintenanceController(IUnitOfWorkAsync unitOfWorkAsync,
            IRepositoryAsync<Bankholidays> bankHolidayRepositoryAsync)
        {
            _bankHolidayRepositoryAsync = bankHolidayRepositoryAsync;
                        _unitOfWorkAsync = unitOfWorkAsync;
        }

        
        
        // GET: Maintenance
        public ActionResult Index()
        {
            return View();
        }


        // GET: /Document/
        public ActionResult BankholidayIndex()
        {
            
            var viewModels = _bankHolidayRepositoryAsync
                .Query(e => e.id > 0)
                .Select(d => new BankholidaysViewModels()
                {
                    Id = d.id,
                    description = d.description,
                    status = d.status,
                    holiday_date = d.holiday_date,
                    createdby = d.createdby,
                    date_created = d.date_created,
                    modified_on = d.modified_on,
                

                })
                .ToList();

            viewModels = viewModels.OrderBy(x => x.holiday_date).ToList();
//            viewModels = viewModels.OrderByDescending(x => x.holiday_date).ToList();

            return View(viewModels);

        }


        //DELETE DATA UPLOADED
        public ActionResult DeleteBank(int? id)
        {
            if (id == null) return HttpNotFound();
            var holiday = _bankHolidayRepositoryAsync.Find(id);
            if (holiday == null) return HttpNotFound();

            _bankHolidayRepositoryAsync.Delete(holiday);
            _unitOfWorkAsync.SaveChanges();
            return RedirectToAction("BankHolidayIndex", "Maintenance");
        }

        
        public ActionResult AddNewBankHoliday(BankholidaysViewModels model)
        {

        

            var NewHoliday = new Bankholidays()
            {
                description = model.description,
                status = model.status,
                holiday_date = Convert.ToDateTime(model.holiday_date),
                createdby = "admin",
                date_created = Convert.ToDateTime(DateTime.Now.ToString("M-d-yyyy")),
                modified_on = Convert.ToDateTime(DateTime.Now.ToString("M-d-yyyy")),

            };


            if (isAlreadyUploaded(Convert.ToDateTime(model.holiday_date)) == true)
            {
                //return null;

                //   ModelState.AddModelError("", "Sort Center with Received Date : " + strHEADER_REC_DATEFROM  + " Already Exist!.");

                Danger("<b>Oh Snap!</b>  This Holiday Date : " + Convert.ToDateTime(model.holiday_date).ToShortDateString() + "  Already Exist!", true);

                return RedirectToAction("BankHolidayIndex", "Maintenance");

            }


            _bankHolidayRepositoryAsync.GetRepository<Bankholidays>().Insert(NewHoliday);

            _unitOfWorkAsync.SaveChanges();

            return RedirectToAction("BankHolidayIndex", "Maintenance");

           
        }


        //Confirm Uploaded Data
        public Boolean isAlreadyUploaded(DateTime HolidayDate)
        {
            Boolean boolResult = false;

            if (HolidayDate == null) return false;
            //  var document = _documentRepositoryAsync.Find(id);
            var viewModels = _bankHolidayRepositoryAsync
               .Query(e => e.holiday_date == HolidayDate)
               .Select(d => new DocumentViewModel()
               {
                   Id = d.id
               
               })
               .ToList();

            if (viewModels.Count > 0)
                boolResult = true;

            return boolResult;



        }



        public ActionResult ContactIT()
        {
            FormsAuthentication.SignOut();
            return PartialView("Contact");

        }



        public ActionResult BankHolidayAdd()
        {
            //Account model = new Account();
            //Bankholidays model = new Bankholidays();
            //IEnumerable<SelectListItem> categories = _employeeRepositoryAsync.Query().Select().ToList()
            //    .Select(s => new SelectListItem
            //    {
            //        Value = s.Id.ToString(),
            //        Text = s.Fullname
            //    });
            //ViewBag.EmployeeId = new SelectList(categories, "Value", "Text");

            //IEnumerable<SelectListItem> roles = _roleRepositoryAsync.Query().Select().ToList()
            //    .Select(s => new SelectListItem
            //    {
            //        Value = s.Id.ToString(),
            //        Text = s.RoleType
            //    });
            //ViewBag.RoleId = new SelectList(roles, "Value", "Text");

            return View();
        }

    }
}