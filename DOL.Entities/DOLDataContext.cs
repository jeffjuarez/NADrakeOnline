using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using DOL.Entities.Models;
using Repository.Pattern.Ef6;


namespace DOL.Entities
{
    public class DOLDataContext : DataContext
    {
        static DOLDataContext()
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<EResultDataContext, Migrations.Configuration>("EResultDataContext"));
        }

        public DOLDataContext()
            : base("Name=DOLDataContext")
        {
        }

    
        public DbSet<Document> Documents { get; set; }
        public DbSet<users> Users { get; set; }
        public DbSet<Bankholidays> BankHolidays { get; set; }
        public DbSet<TimeSheetView> TimeSheetView { get; set; }
        public DbSet<Timesheet_orders> Timesheet_orders { get; set; }
        public DbSet<vw_client_timesheets> TimesheetDetails { get; set; }
        public DbSet<Lookup_Master> Lookup_Master { get; set; }
        public DbSet<Timesheet_Details> Timesheet_Details { get; set; }
        public DbSet<client_organization> Client_Organization { get; set; }

        public DbSet<client_users> Client_Users { get; set; }
        public DbSet<user_profile> User_Profile { get; set; }
        public DbSet<timeclock_transform> timeclock_transform { get; set; }

        public DbSet<rpt_spendReport> Report_Spend { get; set; }


        /*

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // account
            modelBuilder.Entity<Account>().HasRequired(r => r.Role).WithMany().HasForeignKey(r => r.RoleId);
            modelBuilder.Entity<Account>().HasRequired(e => e.Employee).WithMany().HasForeignKey(e => e.EmployeeId);
            modelBuilder.Entity<Account>().Property(t => t.Username).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("IX_Username", 1) { IsUnique = true }));

            // document
            modelBuilder.Entity<Document>().HasRequired(e => e.Employee).WithMany().HasForeignKey(e => e.EmployeeId);

            // employee
            modelBuilder.Entity<Employee>().HasRequired(c => c.Company).WithMany().HasForeignKey(c => c.CompanyId);
            

            base.OnModelCreating(modelBuilder);
        }*/
    }
}
