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
    public class AccountController : BaseController
    {
        private readonly IRepositoryAsync<users> _usersRepositoryAsync;
        private readonly IRepositoryAsync<user_profile> _userProfileRepositoryAsync;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public AccountController(IUnitOfWorkAsync unitOfWorkAsync,
            IRepositoryAsync<users> accountRepositoryAsync, IRepositoryAsync<user_profile> userProfileRepositoryAsync)
        {
            _usersRepositoryAsync = accountRepositoryAsync;
            _userProfileRepositoryAsync = userProfileRepositoryAsync;
            _unitOfWorkAsync = unitOfWorkAsync;
        }


        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Loginv2(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        //
        // POST: /Account/Login
        //[HttpPost]
        //public ActionResult Login(LoginViewModel model, string returnUrl)
        //{
        //    if (!ModelState.IsValid) return View(model);
        //    var account = _usersRepositoryAsync
        //        .Query(x => x.login_name == model.UserName)
        //        //.Query(x => x.Username == model.UserName)
        //        //.Include(r => r.Role)
        //        .Select()
        //        .SingleOrDefault();

        //    if (account != null)
        //    {
        //      //  var role = account.Role.RoleType;
        //        if (AccountHelpers.ValidatePasswordDOL(model.UserName, model.Password, account.password))
        //        {
        //            FormsAuthentication.SetAuthCookie(account.login_name, false);

        //        //    System.Web.HttpContext.Current.Session["_EmployeeID"] = account.EmployeeId;
        //         //   return account.IsResetPassword ? RedirectToAction("Manage", "Account") : RedirectToAction("UploadForm", "Document");
        //        }
        //    }

        //   ModelState.AddModelError("", "Invalid username or password.");
        

        //    return View(model);
        //}


        // POST: /Account/Login
        [HttpPost]
        public ActionResult Loginv2(LoginViewModel model, string returnUrl)
        {

            if (!ModelState.IsValid) return View(model);
            var account = _usersRepositoryAsync
                .Query(x => x.login_name == model.UserName)
                .Select()
                .SingleOrDefault();


            if (account != null)
            {


                var accountProfile = _userProfileRepositoryAsync
                  .Query(x => x.user_id == account.user_id)
                  .Select()
                  .SingleOrDefault();


                //  var role = account.Role.RoleType;
                if (AccountHelpers.ValidatePasswordDOL(model.UserName, model.Password, account.password))
                {
                    FormsAuthentication.SetAuthCookie(account.user_id, false);

                    //  ViewData["CurrentUser"] = "User";
                    //  System.Web.HttpContext.Current.Session["_UserFullName"] = account.;
                    System.Web.HttpContext.Current.Session["_UserFullName"] = accountProfile.last_name + ", " + accountProfile.first_name;


                    return RedirectToAction("About", "Home");

                }

                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Username not Found!");
                return View(model);
            }


            // return View ();
        }

        //
        // GET: /Account/Manage
        [Authorize]
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.OldPasswordFailed ? "Old password did not match."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = true;
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(ManageUserViewModel model)
        {
            ViewBag.HasLocalPassword = true;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (!ModelState.IsValid) return View(model);
            var username = HttpContext.User.Identity.Name;
           // var account = _accountRepositoryAsync
            var account = _usersRepositoryAsync
                .Query(x => x.login_name == username)
              //  .Include(r => r.Role)
                .Select()
                .SingleOrDefault();

            if (account == null)
                return RedirectToAction("Manage", new {Message = ManageMessageId.Error});

            // validate first for old password
        //    if (!AccountHelpers.ValidatePassword(model.OldPassword, account.Salt, account.Password))
            if (!AccountHelpers.ValidatePasswordDOL(username, model.OldPassword,  account.password))
                return RedirectToAction("Manage", new { Message = ManageMessageId.OldPasswordFailed });

             
            var newPassword = AccountHelpers.HashPassword(model.NewPassword);
            //account.Salt = newPassword.Salt;
            //account.Password = newPassword.HashPassword;
            //account.IsResetPassword = false;
            account.ObjectState = ObjectState.Modified;

           // _accountRepositoryAsync.Update(account);
            _usersRepositoryAsync.Update(account);
            _unitOfWorkAsync.SaveChanges();

           // var role = account.Role.RoleType;
            return RedirectToAction("Index", IsAdminHr("Admin") ? "Employees" : "Document");
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Loginv2", "Account");
        }

        public enum ManageMessageId
        {
            OldPasswordFailed,
            Error
        }

        private bool IsAdminHr(string role)
        {
            return role == "Admin" || role == "HR";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
