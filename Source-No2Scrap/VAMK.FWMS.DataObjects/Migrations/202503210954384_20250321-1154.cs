namespace VAMK.FWMS.DataObjects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _202503211154 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditTrailDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AuditTrailID = c.Int(),
                        EntityType = c.String(maxLength: 200),
                        EntityID = c.Int(),
                        Property = c.String(maxLength: 200),
                        PreviousValue = c.String(maxLength: 500),
                        NewValue = c.String(maxLength: 500),
                        Action = c.Int(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AuditTrails", t => t.AuditTrailID, cascadeDelete: true)
                .Index(t => t.AuditTrailID);
            
            CreateTable(
                "dbo.AuditTrails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EntityType = c.String(maxLength: 200),
                        EntityID = c.Int(),
                        EmployeeID = c.Int(),
                        Date = c.DateTime(),
                        Action = c.Int(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        PrefixCode = c.String(),
                        Name = c.String(),
                        AddressLine1 = c.String(),
                        AddressLine2 = c.String(),
                        AddressLine3 = c.String(),
                        Phone1 = c.String(),
                        Phone2 = c.String(),
                        Email = c.String(),
                        User = c.String(),
                        TimeStamp = c.Binary(),
                        State = c.Int(nullable: false),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        AuditReference = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ContactPersons",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 200),
                        Name = c.String(maxLength: 200),
                        Email = c.String(maxLength: 200),
                        PhoneNumber = c.String(maxLength: 20),
                        Mobile = c.String(maxLength: 20),
                        ContactPersonTypeID = c.Int(nullable: false),
                        RecepientID = c.Int(),
                        DonerID = c.Int(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Doners", t => t.DonerID, cascadeDelete: true)
                .ForeignKey("dbo.Recipients", t => t.RecepientID, cascadeDelete: true)
                .Index(t => t.RecepientID)
                .Index(t => t.DonerID);
            
            CreateTable(
                "dbo.Doners",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 20),
                        Name = c.String(maxLength: 200),
                        Address = c.String(maxLength: 500),
                        LocationCoordinates = c.String(maxLength: 200),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Recipients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 20),
                        Name = c.String(maxLength: 200),
                        Address = c.String(maxLength: 500),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CoordinatorIntentoryItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Number = c.DateTime(),
                        InventoryEffectedby = c.Int(),
                        EffectedTransacionID = c.Int(),
                        ItemID = c.Int(),
                        EffectedQty = c.Decimal(precision: 18, scale: 2),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Items", t => t.ItemID)
                .Index(t => t.ItemID);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 50),
                        Name = c.String(maxLength: 200),
                        ItemCategory = c.Int(),
                        UnitOfMeasurementID = c.Int(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UnitsOfMeasurement", t => t.UnitOfMeasurementID)
                .Index(t => t.UnitOfMeasurementID);
            
            CreateTable(
                "dbo.UnitsOfMeasurement",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 20),
                        UnitName = c.String(maxLength: 200),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        User = c.String(),
                        TimeStamp = c.Binary(),
                        State = c.Int(nullable: false),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        AuditReference = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        AuthorizedOfficerID = c.Int(),
                        User = c.String(),
                        TimeStamp = c.Binary(),
                        State = c.Int(nullable: false),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        AuditReference = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.AuthorizedOfficerID)
                .Index(t => t.AuthorizedOfficerID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.Int(),
                        Code = c.String(maxLength: 20),
                        FirstName = c.String(maxLength: 200),
                        LastName = c.String(maxLength: 200),
                        Phone = c.String(maxLength: 20),
                        Mobile = c.String(maxLength: 20),
                        Email = c.String(maxLength: 200),
                        UserName = c.String(maxLength: 200),
                        Password = c.String(maxLength: 200),
                        CompanyID = c.Int(),
                        Designation = c.String(maxLength: 200),
                        DateOfBirth = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsLocked = c.Boolean(nullable: false),
                        UnSuccessfulLoginAttempts = c.Int(),
                        PasswordResetDate = c.DateTime(),
                        IsRelationshipManager = c.Boolean(nullable: false),
                        IsSalesManager = c.Boolean(nullable: false),
                        IsSalesEngineer = c.Boolean(nullable: false),
                        IsPreSaleEngineer = c.Boolean(nullable: false),
                        IsProjectManager = c.Boolean(nullable: false),
                        IsBizDeveloper = c.Boolean(nullable: false),
                        IsTechnicalPerson = c.Boolean(nullable: false),
                        IsLeagalOfficer = c.Boolean(nullable: false),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.CompanyID)
                .Index(t => t.Code, unique: true, name: "IX_Employees_Code")
                .Index(t => t.UserName, unique: true, name: "IX_Employees_UserName")
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.Donations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DonerID = c.Int(),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(maxLength: 200),
                        DonationSatus = c.Int(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Doners", t => t.DonerID, cascadeDelete: true)
                .Index(t => t.DonerID);
            
            CreateTable(
                "dbo.DonationItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DonationID = c.Int(),
                        ItemID = c.Int(),
                        Qty = c.Decimal(nullable: false, precision: 12, scale: 2),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Items", t => t.ItemID, cascadeDelete: true)
                .ForeignKey("dbo.Donations", t => t.DonationID, cascadeDelete: true)
                .Index(t => t.DonationID)
                .Index(t => t.ItemID);
            
            CreateTable(
                "dbo.EmailOutBoxes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreatedByID = c.Int(),
                        Sender = c.Int(nullable: false),
                        SenderDisplayName = c.String(),
                        To = c.String(),
                        Bc = c.String(),
                        Cc = c.String(),
                        IsBodyHtml = c.Boolean(nullable: false),
                        MailContent = c.String(),
                        Subject = c.String(),
                        EmailCreatedDate = c.DateTime(),
                        EmailStatus = c.Int(nullable: false),
                        Note = c.String(),
                        EmailSendAttempt = c.Int(),
                        User = c.String(),
                        TimeStamp = c.Binary(),
                        State = c.Int(nullable: false),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        AuditReference = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.CreatedByID)
                .Index(t => t.CreatedByID);
            
            CreateTable(
                "dbo.GroupEmployees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GroupID = c.Int(),
                        EmployeeID = c.Int(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID)
                .ForeignKey("dbo.Groups", t => t.GroupID)
                .Index(t => t.GroupID)
                .Index(t => t.EmployeeID);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 200),
                        SalesFullControl = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Description, unique: true, name: "IX_Groups_Description");
            
            CreateTable(
                "dbo.GroupRules",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GroupID = c.Int(),
                        RuleID = c.Int(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Groups", t => t.GroupID)
                .ForeignKey("dbo.Rules", t => t.RuleID)
                .Index(t => t.GroupID)
                .Index(t => t.RuleID);
            
            CreateTable(
                "dbo.Rules",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 20),
                        Description = c.String(maxLength: 200),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Thread = c.String(),
                        Level = c.String(),
                        Logger = c.String(),
                        Message = c.String(),
                        Exception = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RecipientD = c.Int(),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(maxLength: 200),
                        RequestStatus = c.Int(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        Recipient_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Recipients", t => t.Recipient_ID)
                .Index(t => t.Recipient_ID);
            
            CreateTable(
                "dbo.RequestItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RequestID = c.Int(),
                        ItemID = c.Int(),
                        RequestQty = c.Decimal(nullable: false, precision: 12, scale: 2),
                        AllocatedQty = c.Decimal(nullable: false, precision: 12, scale: 2),
                        IsFullfilled = c.Boolean(nullable: false),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Items", t => t.ItemID, cascadeDelete: true)
                .ForeignKey("dbo.Requests", t => t.RequestID, cascadeDelete: true)
                .Index(t => t.RequestID)
                .Index(t => t.ItemID);
            
            CreateTable(
                "dbo.SecureAccessForms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FormCode = c.String(maxLength: 50),
                        FormName = c.String(maxLength: 200),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SentMails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreatedByID = c.Int(),
                        Sender = c.Int(nullable: false),
                        Recipient = c.String(),
                        MailContent = c.String(),
                        Subject = c.String(),
                        EmailCreatedDate = c.DateTime(),
                        EmailStatus = c.Int(nullable: false),
                        Note = c.String(),
                        EmailSendAttempt = c.Int(),
                        SendDate = c.DateTime(),
                        User = c.String(),
                        TimeStamp = c.Binary(),
                        State = c.Int(nullable: false),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        AuditReference = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.CreatedByID)
                .Index(t => t.CreatedByID);
            
            CreateTable(
                "dbo.SystemConfigurations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RegistrationName = c.String(),
                        TimeZoneID = c.Int(),
                        EmailServer = c.String(),
                        EmailServerPort = c.Int(),
                        EmailSenderGeneral = c.String(),
                        EmailSenderGeneralPassword = c.String(),
                        EmailSenderGeneralDisplayName = c.String(),
                        MCFinanceApprovedByID = c.Int(),
                        User = c.String(),
                        TimeStamp = c.Binary(),
                        State = c.Int(nullable: false),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        AuditReference = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TimeZones", t => t.TimeZoneID)
                .Index(t => t.TimeZoneID);
            
            CreateTable(
                "dbo.TimeZones",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        DisplayName = c.String(),
                        HasDayLightSavingTime = c.Boolean(nullable: false),
                        User = c.String(),
                        TimeStamp = c.Binary(),
                        State = c.Int(nullable: false),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        AuditReference = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SystemUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 20),
                        FirstName = c.String(maxLength: 200),
                        LastName = c.String(maxLength: 200),
                        Phone = c.String(maxLength: 20),
                        Email = c.String(maxLength: 200),
                        Password = c.String(maxLength: 200),
                        DonerID = c.Int(),
                        RecipientID = c.Int(),
                        Designation = c.String(maxLength: 200),
                        SystemUserTypeID = c.Int(nullable: false),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Doners", t => t.DonerID)
                .ForeignKey("dbo.Recipients", t => t.RecipientID)
                .Index(t => t.UserName, unique: true, name: "IX_SystemUsers_Code")
                .Index(t => t.DonerID)
                .Index(t => t.RecipientID);
            
            CreateTable(
                "dbo.UserRights",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemUserID = c.Int(),
                        SecureAccessFormID = c.Int(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SecureAccessForms", t => t.SecureAccessFormID)
                .ForeignKey("dbo.SystemUsers", t => t.SystemUserID)
                .Index(t => t.SystemUserID)
                .Index(t => t.SecureAccessFormID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRights", "SystemUserID", "dbo.SystemUsers");
            DropForeignKey("dbo.UserRights", "SecureAccessFormID", "dbo.SecureAccessForms");
            DropForeignKey("dbo.SystemUsers", "RecipientID", "dbo.Recipients");
            DropForeignKey("dbo.SystemUsers", "DonerID", "dbo.Doners");
            DropForeignKey("dbo.SystemConfigurations", "TimeZoneID", "dbo.TimeZones");
            DropForeignKey("dbo.SentMails", "CreatedByID", "dbo.Employees");
            DropForeignKey("dbo.RequestItems", "RequestID", "dbo.Requests");
            DropForeignKey("dbo.RequestItems", "ItemID", "dbo.Items");
            DropForeignKey("dbo.Requests", "Recipient_ID", "dbo.Recipients");
            DropForeignKey("dbo.GroupRules", "RuleID", "dbo.Rules");
            DropForeignKey("dbo.GroupRules", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.GroupEmployees", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.GroupEmployees", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.EmailOutBoxes", "CreatedByID", "dbo.Employees");
            DropForeignKey("dbo.Donations", "DonerID", "dbo.Doners");
            DropForeignKey("dbo.DonationItems", "DonationID", "dbo.Donations");
            DropForeignKey("dbo.DonationItems", "ItemID", "dbo.Items");
            DropForeignKey("dbo.Departments", "AuthorizedOfficerID", "dbo.Employees");
            DropForeignKey("dbo.Employees", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.CoordinatorIntentoryItems", "ItemID", "dbo.Items");
            DropForeignKey("dbo.Items", "UnitOfMeasurementID", "dbo.UnitsOfMeasurement");
            DropForeignKey("dbo.ContactPersons", "RecepientID", "dbo.Recipients");
            DropForeignKey("dbo.ContactPersons", "DonerID", "dbo.Doners");
            DropForeignKey("dbo.AuditTrailDetails", "AuditTrailID", "dbo.AuditTrails");
            DropIndex("dbo.UserRights", new[] { "SecureAccessFormID" });
            DropIndex("dbo.UserRights", new[] { "SystemUserID" });
            DropIndex("dbo.SystemUsers", new[] { "RecipientID" });
            DropIndex("dbo.SystemUsers", new[] { "DonerID" });
            DropIndex("dbo.SystemUsers", "IX_SystemUsers_Code");
            DropIndex("dbo.SystemConfigurations", new[] { "TimeZoneID" });
            DropIndex("dbo.SentMails", new[] { "CreatedByID" });
            DropIndex("dbo.RequestItems", new[] { "ItemID" });
            DropIndex("dbo.RequestItems", new[] { "RequestID" });
            DropIndex("dbo.Requests", new[] { "Recipient_ID" });
            DropIndex("dbo.GroupRules", new[] { "RuleID" });
            DropIndex("dbo.GroupRules", new[] { "GroupID" });
            DropIndex("dbo.Groups", "IX_Groups_Description");
            DropIndex("dbo.GroupEmployees", new[] { "EmployeeID" });
            DropIndex("dbo.GroupEmployees", new[] { "GroupID" });
            DropIndex("dbo.EmailOutBoxes", new[] { "CreatedByID" });
            DropIndex("dbo.DonationItems", new[] { "ItemID" });
            DropIndex("dbo.DonationItems", new[] { "DonationID" });
            DropIndex("dbo.Donations", new[] { "DonerID" });
            DropIndex("dbo.Employees", new[] { "CompanyID" });
            DropIndex("dbo.Employees", "IX_Employees_UserName");
            DropIndex("dbo.Employees", "IX_Employees_Code");
            DropIndex("dbo.Departments", new[] { "AuthorizedOfficerID" });
            DropIndex("dbo.Items", new[] { "UnitOfMeasurementID" });
            DropIndex("dbo.CoordinatorIntentoryItems", new[] { "ItemID" });
            DropIndex("dbo.ContactPersons", new[] { "DonerID" });
            DropIndex("dbo.ContactPersons", new[] { "RecepientID" });
            DropIndex("dbo.AuditTrailDetails", new[] { "AuditTrailID" });
            DropTable("dbo.UserRights");
            DropTable("dbo.SystemUsers");
            DropTable("dbo.TimeZones");
            DropTable("dbo.SystemConfigurations");
            DropTable("dbo.SentMails");
            DropTable("dbo.SecureAccessForms");
            DropTable("dbo.RequestItems");
            DropTable("dbo.Requests");
            DropTable("dbo.Logs");
            DropTable("dbo.Rules");
            DropTable("dbo.GroupRules");
            DropTable("dbo.Groups");
            DropTable("dbo.GroupEmployees");
            DropTable("dbo.EmailOutBoxes");
            DropTable("dbo.DonationItems");
            DropTable("dbo.Donations");
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
            DropTable("dbo.Countries");
            DropTable("dbo.UnitsOfMeasurement");
            DropTable("dbo.Items");
            DropTable("dbo.CoordinatorIntentoryItems");
            DropTable("dbo.Recipients");
            DropTable("dbo.Doners");
            DropTable("dbo.ContactPersons");
            DropTable("dbo.Companies");
            DropTable("dbo.AuditTrails");
            DropTable("dbo.AuditTrailDetails");
        }
    }
}
