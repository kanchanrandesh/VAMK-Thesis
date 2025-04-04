namespace VAMK.FWMS.DataObjects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SequenceNumbers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(maxLength: 50),
                        Prefix = c.String(maxLength: 50),
                        LastNumber = c.Int(nullable: false),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(fixedLength: true, timestamp: true, storeType: "timestamp"),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.Employees", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Title", c => c.Int());
            DropTable("dbo.SequenceNumbers");
        }
    }
}
