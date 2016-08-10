using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DOL.Entities;
using DOL.Entities.Models;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using DOL.MVC.Controllers;

namespace DOL.MVC
{
    public class ApplicationCastleInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Register working dependencies
            container.Register(Component.For<IDataContextAsync>().ImplementedBy<DOLDataContext>().LifestylePerWebRequest());
            container.Register(Component.For<IUnitOfWorkAsync>().ImplementedBy<UnitOfWork>().LifestylePerWebRequest());
            // container.Register(Component.For<IRepositoryAsync<Account>>().ImplementedBy<Repository<Account>>().LifestylePerWebRequest());

            container.Register(Component.For<IRepositoryAsync<users>>().ImplementedBy<Repository<users>>().LifestylePerWebRequest());
            container.Register(Component.For<IRepositoryAsync<Bankholidays>>().ImplementedBy<Repository<Bankholidays>>().LifestylePerWebRequest());
            container.Register(Component.For<IRepositoryAsync<Document>>().ImplementedBy<Repository<Document>>().LifestylePerWebRequest());
            container.Register(Component.For<IRepositoryAsync<TimeSheetView>>().ImplementedBy<Repository<TimeSheetView>>().LifestylePerWebRequest());
            container.Register(Component.For<IRepositoryAsync<Timesheet_orders>>().ImplementedBy<Repository<Timesheet_orders>>().LifestylePerWebRequest());
            container.Register(Component.For<IRepositoryAsync<vw_client_timesheets>>().ImplementedBy<Repository<vw_client_timesheets>>().LifestylePerWebRequest());
            container.Register(Component.For<IRepositoryAsync<Lookup_Master>>().ImplementedBy<Repository<Lookup_Master>>().LifestylePerWebRequest());
            container.Register(Component.For<IRepositoryAsync<Timesheet_Details>>().ImplementedBy<Repository<Timesheet_Details>>().LifestylePerWebRequest());
            container.Register(Component.For<IRepositoryAsync<client_organization>>().ImplementedBy<Repository<client_organization>>().LifestylePerWebRequest());
            container.Register(Component.For<IRepositoryAsync<client_users>>().ImplementedBy<Repository<client_users>>().LifestylePerWebRequest());
            container.Register(Component.For<IRepositoryAsync<user_profile>>().ImplementedBy<Repository<user_profile>>().LifestylePerWebRequest());
            container.Register(Component.For<IRepositoryAsync<timeclock_transform>>().ImplementedBy<Repository<timeclock_transform>>().LifestylePerWebRequest());

            container.Register(Component.For<IRepositoryAsync<rpt_spendReport>>().ImplementedBy<Repository<rpt_spendReport>>().LifestylePerWebRequest());

            var contollers = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.BaseType == typeof(Controller)).ToList();
            foreach (var controller in contollers)
            {
                container.Register(Component.For(controller).LifestylePerWebRequest());
            }
           
            container.Register(Types.FromThisAssembly().BasedOn(typeof(BaseController)).LifestyleTransient());
        }
    }
}