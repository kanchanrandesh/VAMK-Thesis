namespace VAMK.FWMS.DataObjects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _123 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ContactPersons", name: "RecepientID", newName: "RecipientID");
            RenameIndex(table: "dbo.ContactPersons", name: "IX_RecepientID", newName: "IX_RecipientID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ContactPersons", name: "IX_RecipientID", newName: "IX_RecepientID");
            RenameColumn(table: "dbo.ContactPersons", name: "RecipientID", newName: "RecepientID");
        }
    }
}
