using System.Data.Entity.Migrations;

namespace VAMK.FWMS.DataObjects.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<Context.FWMSDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        protected override void Seed(Context.FWMSDbContext context)
        {
            //TODO:Uncomment to insert the init data set for the first time and comment during Migrations
            //new DataSet.InitialDataSet().InsertDataSet(context);
        }
    }
}
