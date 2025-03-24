using VAMK.FWMS.DataObjects.Mappers;
using VAMK.FWMS.Models;
using MySql.Data.Entity;
using System.Data.Common;
using System.Data.Entity;

namespace VAMK.FWMS.DataObjects.Context
{
    /// <summary>
    /// This Context has reference to the underlining datasource.Any entity that need to be persist in the database
    /// should be include here as a property.mapping to the data column done trough the Mapper class.These will be 
    /// include in the EntityMapper folder.
    /// </summary>
    //[DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class FWMSDbContext : DbContext
    {
        public FWMSDbContext()
            : this("VAMK.FWMS.Data.ConnectionString")
        {
        }

        public FWMSDbContext(string connection)
            : base(connection)
        {
            this.Configuration.ProxyCreationEnabled = false;

        }

        public FWMSDbContext(DbConnection connection) : base(connection, false)
        {
        }

        /// <summary>
        /// Any entity that need to be persist in the database mappers
        /// should be include here as a property
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new TimeZoneMapper());
            //modelBuilder.Configurations.Add(new SystemConfigurationMapper());
            //modelBuilder.Configurations.Add(new CompanyMapper());
            modelBuilder.Configurations.Add(new EmployeeMapper());
            modelBuilder.Configurations.Add(new AuditTrailMapper());
            modelBuilder.Configurations.Add(new AuditTrailDetailMapper());
            //modelBuilder.Configurations.Add(new CountryMapper());            
            //modelBuilder.Configurations.Add(new EmailOutBoxMapper());
            //modelBuilder.Configurations.Add(new SentMailMapper());
            modelBuilder.Configurations.Add(new RuleMapper());
            modelBuilder.Configurations.Add(new GroupMapper());
            modelBuilder.Configurations.Add(new GroupRuleMapper());
            modelBuilder.Configurations.Add(new GroupEmployeeMapper());
            //modelBuilder.Configurations.Add(new DepartmentMapper());
            //modelBuilder.Configurations.Add(new LogMapper());

            //fwms
            modelBuilder.Configurations.Add(new RecipientMapper());            
            modelBuilder.Configurations.Add(new DonerMapper());
            modelBuilder.Configurations.Add(new ContactPersonMapper());
            modelBuilder.Configurations.Add(new DonationMapper());
            modelBuilder.Configurations.Add(new DonationItemMapper());
            modelBuilder.Configurations.Add(new ItemMapper());
            modelBuilder.Configurations.Add(new UnitOfMeasurementMapper());
            modelBuilder.Configurations.Add(new RequestMapper());
            modelBuilder.Configurations.Add(new RequestItemMapper());
            modelBuilder.Configurations.Add(new CoordinatorIntentoryItemMapper());
            modelBuilder.Configurations.Add(new SystemUserMapper());
            modelBuilder.Configurations.Add(new UserRightsMapper());
            modelBuilder.Configurations.Add(new SecureAccessFormMapper());
        }

        public IDbSet<TimeZone> TimeZones { get; set; }
        public IDbSet<SystemConfiguration> SystemConfiguration { get; set; }
        public IDbSet<Company> Companies { get; set; }
        public IDbSet<Employee> Employees { get; set; }
        public IDbSet<AuditTrail> AuditTrails { get; set; }
        public IDbSet<AuditTrailDetail> AuditTrailDetails { get; set; }
        public IDbSet<Country> Countries { get; set; }       
        public IDbSet<EmailOutBox> EmailOutBox { get; set; }
        public IDbSet<SentMail> SentMails { get; set; }
        public IDbSet<Rule> Rules { get; set; }
        public IDbSet<Group> Groups { get; set; }
        public IDbSet<GroupRule> GroupRules { get; set; }
        public IDbSet<GroupEmployee> GroupEmployees { get; set; }
        public IDbSet<Department> Departments { get; set; }        
        public IDbSet<Log> Logs { get; set; }
        //fwms
        public IDbSet<Doner> Doners { get; set; }
        public IDbSet<Recipient> Recipient { get; set; }
        public IDbSet<ContactPerson> ContactPerson { get; set; }
        public IDbSet<Donation> Donation { get; set; }
        public IDbSet<DonationItem> DonationItem { get; set; }
        public IDbSet<Item> Item { get; set; }
        public IDbSet<UnitOfMeasurement> UnitOfMeasurement { get; set; }
        public IDbSet<Request> Request { get; set; }
        public IDbSet<RequestItem> RequestItem { get; set; }
        public IDbSet<CoordinatorIntentoryItem> CoordinatorIntentoryItem { get; set; }
        public IDbSet<SystemUser> SystemUser { get; set; }
        public IDbSet<UserRights> UserRights { get; set; }
        public IDbSet<SecureAccessForm> SecureAccessForm { get; set; }

    }
}
