using Microsoft.Owin;
using Owin;
using System.Web.Mvc;


[assembly: OwinStartupAttribute(typeof(DOL.MVC.Startup))]
namespace DOL.MVC
{
    public partial class Startup
    {
       
        

        public void Configuration(IAppBuilder app)
        {

            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false; 

            ConfigureAuth(app);

            
        }
    }
}
