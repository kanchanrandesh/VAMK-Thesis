namespace JitSys.DataObjects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1809201658AS : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssociatedCompanies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrganizationID = c.Int(),
                        CompanyID = c.Int(),
                        PrimaryRepresentativeID = c.Int(),
                        SecondaryRepresentativeID = c.Int(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.CompanyID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.PrimaryRepresentativeID)
                .ForeignKey("dbo.Employees", t => t.SecondaryRepresentativeID)
                .Index(t => t.OrganizationID)
                .Index(t => t.CompanyID)
                .Index(t => t.PrimaryRepresentativeID)
                .Index(t => t.SecondaryRepresentativeID);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 20),
                        PrefixCode = c.String(maxLength: 2),
                        Name = c.String(maxLength: 200),
                        AddressLine1 = c.String(maxLength: 200),
                        AddressLine2 = c.String(maxLength: 200),
                        AddressLine3 = c.String(maxLength: 200),
                        Phone1 = c.String(maxLength: 20),
                        Phone2 = c.String(maxLength: 20),
                        Email = c.String(maxLength: 200),
                        PettyCashFloat = c.Decimal(precision: 12, scale: 2),
                        PettyCashBalance = c.Decimal(precision: 12, scale: 2),
                        PettyCashOfficerID = c.Int(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.PettyCashOfficerID)
                .Index(t => t.Code, unique: true, name: "IX_Companies_Code")
                .Index(t => t.PettyCashOfficerID);
            
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
                        Extension = c.String(maxLength: 20),
                        Mobile = c.String(maxLength: 20),
                        Email = c.String(maxLength: 200),
                        UserName = c.String(maxLength: 200),
                        Password = c.String(maxLength: 200),
                        TemporaryPassword = c.String(maxLength: 200),
                        TemporaryPasswordEnabled = c.Boolean(nullable: false),
                        CompanyID = c.Int(),
                        Designation = c.String(maxLength: 200),
                        DateOfBirth = c.DateTime(),
                        EligibleForMedicalClaims = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        JobCategoryID = c.Int(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.CompanyID)
                .ForeignKey("dbo.JobCategories", t => t.JobCategoryID)
                .Index(t => t.Code, unique: true, name: "IX_Employees_Code")
                .Index(t => t.CompanyID)
                .Index(t => t.JobCategoryID);
            
            CreateTable(
                "dbo.EmployeeFamilyMembers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(),
                        Name = c.String(maxLength: 200),
                        DateOfBirth = c.DateTime(),
                        Relationship = c.Int(),
                        EligibleForMedicalClaims = c.Boolean(nullable: false),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        EmployeeFamilyMember_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EmployeeFamilyMembers", t => t.EmployeeFamilyMember_ID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.EmployeeFamilyMember_ID);
            
            CreateTable(
                "dbo.JobCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MCAnnualAmount = c.Decimal(precision: 12, scale: 2),
                        Name = c.String(maxLength: 200),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EmployeeProjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(),
                        ProjectID = c.Int(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Projects", t => t.ProjectID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.ProjectID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 20),
                        Name = c.String(maxLength: 200),
                        AuthorizedOfficerID = c.Int(),
                        DepartmentID = c.Int(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.AuthorizedOfficerID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID)
                .Index(t => t.AuthorizedOfficerID)
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 20),
                        Name = c.String(maxLength: 200),
                        AuthorizedOfficerID = c.Int(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.AuthorizedOfficerID)
                .Index(t => t.AuthorizedOfficerID);
            
            CreateTable(
                "dbo.ProjectExpenseAllocations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProjectID = c.Int(),
                        ExpenseCategoryID = c.Int(),
                        Allocated = c.Decimal(precision: 12, scale: 2),
                        Utilized = c.Decimal(precision: 12, scale: 2),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ExpenseCategories", t => t.ExpenseCategoryID)
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
                .Index(t => t.ProjectID)
                .Index(t => t.ExpenseCategoryID);
            
            CreateTable(
                "dbo.ExpenseCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 20),
                        Description = c.String(maxLength: 200),
                        IsTimeRequired = c.Boolean(nullable: false),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EmployeeUnits",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(),
                        UnitID = c.Int(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Units", t => t.UnitID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.UnitID);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 20),
                        Name = c.String(maxLength: 200),
                        AuthorizedOfficerID = c.Int(),
                        DepartmentID = c.Int(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.AuthorizedOfficerID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID)
                .Index(t => t.AuthorizedOfficerID)
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.Int(),
                        Code = c.String(maxLength: 20),
                        Name = c.String(maxLength: 200),
                        AddressLine1 = c.String(maxLength: 200),
                        AddressLine2 = c.String(maxLength: 200),
                        AddressLine3 = c.String(maxLength: 200),
                        CountryID = c.Int(),
                        Phone = c.String(maxLength: 20),
                        Fax = c.String(maxLength: 20),
                        Email = c.String(maxLength: 200),
                        LoginURL = c.String(maxLength: 500),
                        IsActive = c.Boolean(nullable: false),
                        Notes = c.String(maxLength: 500),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Countries", t => t.CountryID)
                .Index(t => t.CountryID);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Name, unique: true, name: "IX_Countries_Name");
            
            CreateTable(
                "dbo.OrganizationIndustries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrganizationID = c.Int(),
                        IndustryID = c.Int(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Industries", t => t.IndustryID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID, cascadeDelete: true)
                .Index(t => t.OrganizationID)
                .Index(t => t.IndustryID);
            
            CreateTable(
                "dbo.Industries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Name, unique: true, name: "IX_Industries_Name");
            
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
                "dbo.Contacts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.Int(),
                        FirstName = c.String(maxLength: 200),
                        LastName = c.String(maxLength: 200),
                        Phone = c.String(maxLength: 20),
                        Extension = c.String(maxLength: 20),
                        Mobile = c.String(maxLength: 20),
                        Email = c.String(maxLength: 200),
                        Secretary = c.String(maxLength: 200),
                        AddressLine1 = c.String(maxLength: 200),
                        AddressLine2 = c.String(maxLength: 200),
                        AddressLine3 = c.String(maxLength: 200),
                        CountryID = c.Int(),
                        OrganizationID = c.Int(),
                        JobRole = c.String(maxLength: 200),
                        Department = c.String(maxLength: 200),
                        DesignationCategoryID = c.Int(),
                        AccountManagerID = c.Int(),
                        BirthYear = c.Int(),
                        BirthMonth = c.Int(),
                        BirthDate = c.Int(),
                        Notes = c.String(maxLength: 500),
                        PhotoURL = c.String(maxLength: 500),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.AccountManagerID)
                .ForeignKey("dbo.Countries", t => t.CountryID)
                .ForeignKey("dbo.DesignationCategories", t => t.DesignationCategoryID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .Index(t => t.CountryID)
                .Index(t => t.OrganizationID)
                .Index(t => t.DesignationCategoryID)
                .Index(t => t.AccountManagerID);
            
            CreateTable(
                "dbo.DesignationCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 20),
                        Description = c.String(maxLength: 200),
                        Priority = c.Int(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Code, unique: true, name: "IX_DesignationCategories_Code");
            
            CreateTable(
                "dbo.EmailOutboxes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreatedByID = c.Int(),
                        Sender = c.Int(nullable: false),
                        SenderDisplayName = c.String(maxLength: 200),
                        Recipient = c.String(maxLength: 200),
                        Bc = c.String(maxLength: 200),
                        Cc = c.String(maxLength: 200),
                        IsBodyHtml = c.Boolean(nullable: false),
                        MailContent = c.String(maxLength: 100),
                        Subject = c.String(maxLength: 200),
                        EmailCreatedDate = c.DateTime(),
                        EmailStatus = c.Int(nullable: false),
                        Note = c.String(maxLength: 500),
                        EmailSendAttempt = c.Int(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.CreatedByID)
                .Index(t => t.CreatedByID);
            
            CreateTable(
                "dbo.EventCheckLists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EventID = c.Int(),
                        Task = c.String(maxLength: 200),
                        DueDate = c.DateTime(),
                        AssigneeID = c.Int(),
                        Notes = c.String(maxLength: 500),
                        HasCompleted = c.Boolean(nullable: false),
                        DateCompleted = c.DateTime(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.AssigneeID)
                .ForeignKey("dbo.Events", t => t.EventID, cascadeDelete: true)
                .Index(t => t.EventID)
                .Index(t => t.AssigneeID);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        Date = c.DateTime(),
                        TimeFrom = c.DateTime(),
                        TimeTo = c.DateTime(),
                        VenueName = c.String(maxLength: 200),
                        VenueAddressLine1 = c.String(maxLength: 200),
                        VenueAddressLine2 = c.String(maxLength: 200),
                        VenueAddressLine3 = c.String(maxLength: 200),
                        Notes = c.String(maxLength: 200),
                        EnableOnlineRegistration = c.Boolean(nullable: false),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EventFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EventID = c.Int(),
                        FileName = c.String(maxLength: 200),
                        DisplayName = c.String(maxLength: 500),
                        UploadedDate = c.DateTime(nullable: false),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Events", t => t.EventID, cascadeDelete: true)
                .Index(t => t.EventID);
            
            CreateTable(
                "dbo.EventHostCompanies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EventID = c.Int(),
                        CompanyID = c.Int(),
                        IsPrimary = c.Boolean(nullable: false),
                        Coordinator1ID = c.Int(),
                        Coordinator2ID = c.Int(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.CompanyID)
                .ForeignKey("dbo.Employees", t => t.Coordinator1ID)
                .ForeignKey("dbo.Employees", t => t.Coordinator2ID)
                .ForeignKey("dbo.Events", t => t.EventID, cascadeDelete: true)
                .Index(t => t.EventID)
                .Index(t => t.CompanyID)
                .Index(t => t.Coordinator1ID)
                .Index(t => t.Coordinator2ID);
            
            CreateTable(
                "dbo.EventSessions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EventID = c.Int(),
                        TimeFrom = c.DateTime(),
                        TimeTo = c.DateTime(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Events", t => t.EventID, cascadeDelete: true)
                .Index(t => t.EventID);
            
            CreateTable(
                "dbo.EventTrackSessions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SessionID = c.Int(),
                        TrackID = c.Int(),
                        Title = c.String(maxLength: 200),
                        Description = c.String(maxLength: 500),
                        SpeakerName = c.String(maxLength: 200),
                        SpeakerDescription = c.String(maxLength: 500),
                        Coordinator1ID = c.Int(),
                        Coordinator2ID = c.Int(),
                        RoomName = c.String(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.Coordinator1ID)
                .ForeignKey("dbo.Employees", t => t.Coordinator2ID)
                .ForeignKey("dbo.EventTracks", t => t.TrackID)
                .ForeignKey("dbo.EventSessions", t => t.SessionID, cascadeDelete: true)
                .Index(t => t.SessionID)
                .Index(t => t.TrackID)
                .Index(t => t.Coordinator1ID)
                .Index(t => t.Coordinator2ID);
            
            CreateTable(
                "dbo.EventTracks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EventID = c.Int(),
                        Name = c.String(maxLength: 200),
                        Description = c.String(maxLength: 500),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Events", t => t.EventID, cascadeDelete: true)
                .Index(t => t.EventID);
            
            CreateTable(
                "dbo.EventSponsors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EventID = c.Int(),
                        Name = c.String(maxLength: 200),
                        Category = c.String(maxLength: 200),
                        LogoURL = c.String(maxLength: 500),
                        ContactPerson = c.String(maxLength: 200),
                        Phone = c.String(maxLength: 20),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Events", t => t.EventID, cascadeDelete: true)
                .Index(t => t.EventID);
            
            CreateTable(
                "dbo.EventVendorInformations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EventID = c.Int(),
                        Name = c.String(maxLength: 200),
                        Description = c.String(maxLength: 500),
                        LogoURL = c.String(maxLength: 500),
                        Website = c.String(maxLength: 200),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Events", t => t.EventID, cascadeDelete: true)
                .Index(t => t.EventID);
            
            CreateTable(
                "dbo.EventParticipants",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EventID = c.Int(),
                        ReferenceNumber = c.String(maxLength: 20),
                        ParticipantType = c.Int(),
                        Title = c.Int(),
                        FirstName = c.String(maxLength: 200),
                        LastName = c.String(maxLength: 200),
                        Phone = c.String(maxLength: 20),
                        Extension = c.String(maxLength: 20),
                        Mobile = c.String(maxLength: 20),
                        Email = c.String(maxLength: 200),
                        SecretaryName = c.String(maxLength: 200),
                        OrganizationID = c.Int(),
                        OrganizationName = c.String(maxLength: 200),
                        IndustryID = c.Int(),
                        JobRole = c.String(maxLength: 200),
                        Department = c.String(maxLength: 200),
                        DesignationCategoryID = c.Int(),
                        AddressLine1 = c.String(maxLength: 200),
                        AddressLine2 = c.String(maxLength: 200),
                        AddressLine3 = c.String(maxLength: 200),
                        CountryID = c.Int(),
                        AccountManagerID = c.Int(),
                        HasResponded = c.Boolean(nullable: false),
                        IsParticipating = c.Boolean(nullable: false),
                        IsParticipatingKeyNote = c.Boolean(nullable: false),
                        IsParticipatingTrack = c.Boolean(nullable: false),
                        IsParticipatingSOC = c.Boolean(nullable: false),
                        HasIDPrinted = c.Boolean(nullable: false),
                        HasParticipated = c.Boolean(nullable: false),
                        ParticipatedTime = c.DateTime(),
                        ContactID = c.Int(),
                        ReferenceKey = c.String(maxLength: 200),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.AccountManagerID)
                .ForeignKey("dbo.Countries", t => t.CountryID)
                .ForeignKey("dbo.DesignationCategories", t => t.DesignationCategoryID)
                .ForeignKey("dbo.Events", t => t.EventID)
                .ForeignKey("dbo.Industries", t => t.IndustryID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .Index(t => t.EventID)
                .Index(t => t.OrganizationID)
                .Index(t => t.IndustryID)
                .Index(t => t.DesignationCategoryID)
                .Index(t => t.CountryID)
                .Index(t => t.AccountManagerID);
            
            CreateTable(
                "dbo.EventParticipantTracks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EventParticipantID = c.Int(),
                        TrackID = c.Int(),
                        HasParticipated = c.Boolean(nullable: false),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EventTrackSessions", t => t.TrackID)
                .ForeignKey("dbo.EventParticipants", t => t.EventParticipantID, cascadeDelete: true)
                .Index(t => t.EventParticipantID)
                .Index(t => t.TrackID);
            
            CreateTable(
                "dbo.EventParticipantVendors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EventParticipantID = c.Int(),
                        VendorID = c.Int(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EventVendorInformations", t => t.VendorID)
                .ForeignKey("dbo.EventParticipants", t => t.EventParticipantID, cascadeDelete: true)
                .Index(t => t.EventParticipantID)
                .Index(t => t.VendorID);
            
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
                "dbo.IOUItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IOUID = c.Int(),
                        Description = c.String(maxLength: 200),
                        CategoryID = c.Int(),
                        AccountID = c.Int(),
                        Amount = c.Decimal(precision: 12, scale: 2),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LedgerAccounts", t => t.AccountID)
                .ForeignKey("dbo.ExpenseCategories", t => t.CategoryID)
                .ForeignKey("dbo.IOUs", t => t.IOUID, cascadeDelete: true)
                .Index(t => t.IOUID)
                .Index(t => t.CategoryID)
                .Index(t => t.AccountID);
            
            CreateTable(
                "dbo.LedgerAccounts",
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
                "dbo.IOUs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Number = c.String(maxLength: 20),
                        CompanyID = c.Int(),
                        ProjectID = c.Int(),
                        UnitID = c.Int(),
                        PayeeID = c.Int(),
                        RequestedByID = c.Int(),
                        RequestedDate = c.DateTime(),
                        LeadApprovedByID = c.Int(),
                        LeadApprovedDate = c.DateTime(),
                        HODApprovedByID = c.Int(),
                        HODApprovedDate = c.DateTime(),
                        FinanceApprovedByID = c.Int(),
                        FinanceApprovedDate = c.DateTime(),
                        PaidByID = c.Int(),
                        PaidDate = c.DateTime(),
                        RejectedByID = c.Int(),
                        RejectedDate = c.DateTime(),
                        SettledByID = c.Int(),
                        SettledDate = c.DateTime(),
                        Amount = c.Decimal(precision: 12, scale: 2),
                        ReturnedAmount = c.Decimal(precision: 12, scale: 2),
                        SettledAmount = c.Decimal(precision: 12, scale: 2),
                        Status = c.Int(),
                        Notes = c.String(maxLength: 500),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.CompanyID)
                .ForeignKey("dbo.Employees", t => t.FinanceApprovedByID)
                .ForeignKey("dbo.Employees", t => t.HODApprovedByID)
                .ForeignKey("dbo.Employees", t => t.LeadApprovedByID)
                .ForeignKey("dbo.Employees", t => t.PaidByID)
                .ForeignKey("dbo.Employees", t => t.PayeeID)
                .ForeignKey("dbo.Projects", t => t.ProjectID)
                .ForeignKey("dbo.Employees", t => t.RejectedByID)
                .ForeignKey("dbo.Employees", t => t.RequestedByID)
                .ForeignKey("dbo.Employees", t => t.SettledByID)
                .ForeignKey("dbo.Units", t => t.UnitID)
                .Index(t => t.CompanyID)
                .Index(t => t.ProjectID)
                .Index(t => t.UnitID)
                .Index(t => t.PayeeID)
                .Index(t => t.RequestedByID)
                .Index(t => t.LeadApprovedByID)
                .Index(t => t.HODApprovedByID)
                .Index(t => t.FinanceApprovedByID)
                .Index(t => t.PaidByID)
                .Index(t => t.RejectedByID)
                .Index(t => t.SettledByID);
            
            CreateTable(
                "dbo.IOUTxHistory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IOUID = c.Int(),
                        Notes = c.String(maxLength: 500),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.IOUs", t => t.IOUID, cascadeDelete: true)
                .Index(t => t.IOUID);
            
            CreateTable(
                "dbo.IOUSettlements",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IOUID = c.Int(),
                        PettyCashVoucherID = c.Int(),
                        AmountSettled = c.Decimal(precision: 12, scale: 2),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.IOUs", t => t.IOUID)
                .ForeignKey("dbo.PettyCashVouchers", t => t.PettyCashVoucherID)
                .Index(t => t.IOUID)
                .Index(t => t.PettyCashVoucherID);
            
            CreateTable(
                "dbo.PettyCashVouchers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Number = c.String(maxLength: 20),
                        CompanyID = c.Int(),
                        ProjectID = c.Int(),
                        UnitID = c.Int(),
                        PayeeID = c.Int(),
                        RequestedByID = c.Int(),
                        RequestedDate = c.DateTime(),
                        LeadApprovedByID = c.Int(),
                        LeadApprovedDate = c.DateTime(),
                        HODApprovedByID = c.Int(),
                        HODApprovedDate = c.DateTime(),
                        FinanceApprovedByID = c.Int(),
                        FinanceApprovedDate = c.DateTime(),
                        DisbursedByID = c.Int(),
                        DisbursedDate = c.DateTime(),
                        RejectedByID = c.Int(),
                        RejectedDate = c.DateTime(),
                        CancelledByID = c.Int(),
                        CancelledDate = c.DateTime(),
                        Amount = c.Decimal(precision: 12, scale: 2),
                        ReimbursementID = c.Int(),
                        ReimbursementItemID = c.Int(),
                        Status = c.Int(),
                        Notes = c.String(maxLength: 500),
                        SettledAmount = c.Decimal(precision: 12, scale: 2),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.CancelledByID)
                .ForeignKey("dbo.Companies", t => t.CompanyID)
                .ForeignKey("dbo.Employees", t => t.DisbursedByID)
                .ForeignKey("dbo.Employees", t => t.FinanceApprovedByID)
                .ForeignKey("dbo.Employees", t => t.HODApprovedByID)
                .ForeignKey("dbo.Employees", t => t.LeadApprovedByID)
                .ForeignKey("dbo.Employees", t => t.PayeeID)
                .ForeignKey("dbo.Projects", t => t.ProjectID)
                .ForeignKey("dbo.Employees", t => t.RejectedByID)
                .ForeignKey("dbo.Employees", t => t.RequestedByID)
                .ForeignKey("dbo.Units", t => t.UnitID)
                .Index(t => t.CompanyID)
                .Index(t => t.ProjectID)
                .Index(t => t.UnitID)
                .Index(t => t.PayeeID)
                .Index(t => t.RequestedByID)
                .Index(t => t.LeadApprovedByID)
                .Index(t => t.HODApprovedByID)
                .Index(t => t.FinanceApprovedByID)
                .Index(t => t.DisbursedByID)
                .Index(t => t.RejectedByID)
                .Index(t => t.CancelledByID);
            
            CreateTable(
                "dbo.PettyCashVoucherAttachments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PettyCashVoucherID = c.Int(),
                        FileName = c.String(maxLength: 500),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PettyCashVouchers", t => t.PettyCashVoucherID, cascadeDelete: true)
                .Index(t => t.PettyCashVoucherID);
            
            CreateTable(
                "dbo.PettyCashVoucherItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PettyCashVoucherID = c.Int(),
                        Description = c.String(maxLength: 200),
                        CategoryID = c.Int(),
                        AccountID = c.Int(),
                        Amount = c.Decimal(precision: 12, scale: 2),
                        Time = c.String(maxLength: 20),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LedgerAccounts", t => t.AccountID)
                .ForeignKey("dbo.ExpenseCategories", t => t.CategoryID)
                .ForeignKey("dbo.PettyCashVouchers", t => t.PettyCashVoucherID, cascadeDelete: true)
                .Index(t => t.PettyCashVoucherID)
                .Index(t => t.CategoryID)
                .Index(t => t.AccountID);
            
            CreateTable(
                "dbo.PettyCashVoucherTxHistory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PettyCashVoucherID = c.Int(),
                        Notes = c.String(maxLength: 500),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PettyCashVouchers", t => t.PettyCashVoucherID, cascadeDelete: true)
                .Index(t => t.PettyCashVoucherID);
            
            CreateTable(
                "dbo.MedicalClaimAttachments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MedicalClaimID = c.Int(),
                        FileName = c.String(maxLength: 500),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MedicalClaims", t => t.MedicalClaimID, cascadeDelete: true)
                .Index(t => t.MedicalClaimID);
            
            CreateTable(
                "dbo.MedicalClaims",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Number = c.String(maxLength: 20),
                        PayeeID = c.Int(),
                        RequestedDate = c.DateTime(),
                        HRCheckedByID = c.Int(),
                        HRCheckedDate = c.DateTime(),
                        HRApprovedByID = c.Int(),
                        HRApprovedDate = c.DateTime(),
                        FinanceApprovedByID = c.Int(),
                        FinanceApprovedDate = c.DateTime(),
                        PaidByID = c.Int(),
                        PaidDate = c.DateTime(),
                        RejectedByID = c.Int(),
                        RejectedDate = c.DateTime(),
                        CancelledByID = c.Int(),
                        CancelledDate = c.DateTime(),
                        Amount = c.Decimal(precision: 12, scale: 2),
                        PaidAmount = c.Decimal(precision: 12, scale: 2),
                        BillAmount = c.Decimal(precision: 12, scale: 2),
                        Status = c.Int(),
                        Notes = c.String(maxLength: 500),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.CancelledByID)
                .ForeignKey("dbo.Employees", t => t.FinanceApprovedByID)
                .ForeignKey("dbo.Employees", t => t.HRApprovedByID)
                .ForeignKey("dbo.Employees", t => t.HRCheckedByID)
                .ForeignKey("dbo.Employees", t => t.PaidByID)
                .ForeignKey("dbo.Employees", t => t.PayeeID)
                .ForeignKey("dbo.Employees", t => t.RejectedByID)
                .Index(t => t.PayeeID)
                .Index(t => t.HRCheckedByID)
                .Index(t => t.HRApprovedByID)
                .Index(t => t.FinanceApprovedByID)
                .Index(t => t.PaidByID)
                .Index(t => t.RejectedByID)
                .Index(t => t.CancelledByID);
            
            CreateTable(
                "dbo.MedicalClaimItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MedicalClaimID = c.Int(),
                        Patient = c.String(maxLength: 200),
                        Relationship = c.String(maxLength: 50),
                        NameOfSpecialist = c.String(maxLength: 200),
                        Illness = c.String(maxLength: 500),
                        DateOfExpenditure = c.DateTime(),
                        ReceiptNumber = c.String(maxLength: 50),
                        Amount = c.Decimal(precision: 12, scale: 2),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MedicalClaims", t => t.MedicalClaimID, cascadeDelete: true)
                .Index(t => t.MedicalClaimID);
            
            CreateTable(
                "dbo.MedicalClaimTxHistory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MedicalClaimID = c.Int(),
                        Notes = c.String(maxLength: 500),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MedicalClaims", t => t.MedicalClaimID, cascadeDelete: true)
                .Index(t => t.MedicalClaimID);
            
            CreateTable(
                "dbo.PettyCashReimbursementItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PettyCashReimbursementID = c.Int(),
                        Number = c.String(maxLength: 20),
                        PayeeID = c.Int(),
                        Amount = c.Decimal(precision: 12, scale: 2),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.PayeeID)
                .ForeignKey("dbo.PettyCashReimbursements", t => t.PettyCashReimbursementID, cascadeDelete: true)
                .Index(t => t.PettyCashReimbursementID)
                .Index(t => t.PayeeID);
            
            CreateTable(
                "dbo.PettyCashReimbursements",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Number = c.String(maxLength: 20),
                        CompanyID = c.Int(),
                        PreparedByID = c.Int(),
                        PreparedDate = c.DateTime(),
                        Amount = c.Decimal(precision: 12, scale: 2),
                        ReimbursedByID = c.Int(),
                        ReimbursedDate = c.DateTime(),
                        ReimbursedAmount = c.Decimal(precision: 12, scale: 2),
                        HasReimbursed = c.Boolean(nullable: false),
                        Notes = c.String(maxLength: 500),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.CompanyID)
                .ForeignKey("dbo.Employees", t => t.PreparedByID)
                .ForeignKey("dbo.Employees", t => t.ReimbursedByID)
                .Index(t => t.CompanyID)
                .Index(t => t.PreparedByID)
                .Index(t => t.ReimbursedByID);
            
            CreateTable(
                "dbo.PettyCashReimbursementTxHistory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PettyCashReimbursementID = c.Int(),
                        Notes = c.String(maxLength: 500),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PettyCashReimbursements", t => t.PettyCashReimbursementID, cascadeDelete: true)
                .Index(t => t.PettyCashReimbursementID);
            
            CreateTable(
                "dbo.SentMails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreatedByID = c.Int(),
                        Sender = c.Int(nullable: false),
                        Recipient = c.String(maxLength: 255),
                        MailContent = c.String(maxLength: 500),
                        Subject = c.String(maxLength: 255),
                        EmailCreatedDate = c.DateTime(),
                        EmailStatus = c.Int(nullable: false),
                        Note = c.String(maxLength: 500),
                        EmailSendAttempt = c.Int(),
                        SendDate = c.DateTime(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.CreatedByID)
                .Index(t => t.CreatedByID);
            
            CreateTable(
                "dbo.SystemConfigurations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RegistrationName = c.String(maxLength: 200),
                        TimeZoneID = c.Int(),
                        MasterEventCheckList = c.String(maxLength: 4000),
                        MasterSessionCheckList = c.String(maxLength: 4000),
                        AllowMultipleIOU = c.Boolean(nullable: false),
                        MaximumIOUAmount = c.Decimal(precision: 12, scale: 2),
                        MaximumPettyCashAmount = c.Decimal(precision: 12, scale: 2),
                        EmailServer = c.String(maxLength: 200),
                        EmailServerPort = c.Int(),
                        EmailSenderGeneral = c.String(maxLength: 200),
                        EmailSenderGeneralPassword = c.String(maxLength: 200),
                        EmailSenderGeneralDisplayName = c.String(maxLength: 200),
                        MCStartMonth = c.Int(),
                        MCAnnualAmount = c.Decimal(precision: 12, scale: 2),
                        MCHRCheckedByID = c.Int(),
                        MCHRApprovedByID = c.Int(),
                        MCFinanceApprovedByID = c.Int(),
                        MCPaidByID = c.Int(),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.MCFinanceApprovedByID)
                .ForeignKey("dbo.Employees", t => t.MCHRApprovedByID)
                .ForeignKey("dbo.Employees", t => t.MCHRCheckedByID)
                .ForeignKey("dbo.Employees", t => t.MCPaidByID)
                .ForeignKey("dbo.TimeZones", t => t.TimeZoneID)
                .Index(t => t.TimeZoneID)
                .Index(t => t.MCHRCheckedByID)
                .Index(t => t.MCHRApprovedByID)
                .Index(t => t.MCFinanceApprovedByID)
                .Index(t => t.MCPaidByID);
            
            CreateTable(
                "dbo.TimeZones",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Key = c.String(maxLength: 200),
                        DisplayName = c.String(maxLength: 200),
                        HasDayLightSavingTime = c.Boolean(nullable: false),
                        UserCreated = c.String(maxLength: 50),
                        TimeStamp = c.Binary(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Key, unique: true, name: "IX_SampleDropDowns_Code");
            
            CreateTable(
                "dbo.TempContacts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 200),
                        FirstName = c.String(maxLength: 200),
                        LastName = c.String(maxLength: 200),
                        Phone = c.String(maxLength: 20),
                        Extension = c.String(maxLength: 20),
                        Mobile = c.String(maxLength: 20),
                        Email = c.String(maxLength: 200),
                        Organization = c.String(maxLength: 200),
                        JobRole = c.String(maxLength: 200),
                        DesignationCategory = c.String(maxLength: 200),
                        AccountManager = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SystemConfigurations", "TimeZoneID", "dbo.TimeZones");
            DropForeignKey("dbo.SystemConfigurations", "MCPaidByID", "dbo.Employees");
            DropForeignKey("dbo.SystemConfigurations", "MCHRCheckedByID", "dbo.Employees");
            DropForeignKey("dbo.SystemConfigurations", "MCHRApprovedByID", "dbo.Employees");
            DropForeignKey("dbo.SystemConfigurations", "MCFinanceApprovedByID", "dbo.Employees");
            DropForeignKey("dbo.SentMails", "CreatedByID", "dbo.Employees");
            DropForeignKey("dbo.PettyCashReimbursementTxHistory", "PettyCashReimbursementID", "dbo.PettyCashReimbursements");
            DropForeignKey("dbo.PettyCashReimbursements", "ReimbursedByID", "dbo.Employees");
            DropForeignKey("dbo.PettyCashReimbursements", "PreparedByID", "dbo.Employees");
            DropForeignKey("dbo.PettyCashReimbursementItems", "PettyCashReimbursementID", "dbo.PettyCashReimbursements");
            DropForeignKey("dbo.PettyCashReimbursements", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.PettyCashReimbursementItems", "PayeeID", "dbo.Employees");
            DropForeignKey("dbo.MedicalClaimTxHistory", "MedicalClaimID", "dbo.MedicalClaims");
            DropForeignKey("dbo.MedicalClaims", "RejectedByID", "dbo.Employees");
            DropForeignKey("dbo.MedicalClaims", "PayeeID", "dbo.Employees");
            DropForeignKey("dbo.MedicalClaims", "PaidByID", "dbo.Employees");
            DropForeignKey("dbo.MedicalClaimItems", "MedicalClaimID", "dbo.MedicalClaims");
            DropForeignKey("dbo.MedicalClaims", "HRCheckedByID", "dbo.Employees");
            DropForeignKey("dbo.MedicalClaims", "HRApprovedByID", "dbo.Employees");
            DropForeignKey("dbo.MedicalClaims", "FinanceApprovedByID", "dbo.Employees");
            DropForeignKey("dbo.MedicalClaims", "CancelledByID", "dbo.Employees");
            DropForeignKey("dbo.MedicalClaimAttachments", "MedicalClaimID", "dbo.MedicalClaims");
            DropForeignKey("dbo.IOUSettlements", "PettyCashVoucherID", "dbo.PettyCashVouchers");
            DropForeignKey("dbo.PettyCashVouchers", "UnitID", "dbo.Units");
            DropForeignKey("dbo.PettyCashVoucherTxHistory", "PettyCashVoucherID", "dbo.PettyCashVouchers");
            DropForeignKey("dbo.PettyCashVouchers", "RequestedByID", "dbo.Employees");
            DropForeignKey("dbo.PettyCashVouchers", "RejectedByID", "dbo.Employees");
            DropForeignKey("dbo.PettyCashVouchers", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.PettyCashVouchers", "PayeeID", "dbo.Employees");
            DropForeignKey("dbo.PettyCashVouchers", "LeadApprovedByID", "dbo.Employees");
            DropForeignKey("dbo.PettyCashVoucherItems", "PettyCashVoucherID", "dbo.PettyCashVouchers");
            DropForeignKey("dbo.PettyCashVoucherItems", "CategoryID", "dbo.ExpenseCategories");
            DropForeignKey("dbo.PettyCashVoucherItems", "AccountID", "dbo.LedgerAccounts");
            DropForeignKey("dbo.PettyCashVouchers", "HODApprovedByID", "dbo.Employees");
            DropForeignKey("dbo.PettyCashVouchers", "FinanceApprovedByID", "dbo.Employees");
            DropForeignKey("dbo.PettyCashVouchers", "DisbursedByID", "dbo.Employees");
            DropForeignKey("dbo.PettyCashVouchers", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.PettyCashVouchers", "CancelledByID", "dbo.Employees");
            DropForeignKey("dbo.PettyCashVoucherAttachments", "PettyCashVoucherID", "dbo.PettyCashVouchers");
            DropForeignKey("dbo.IOUSettlements", "IOUID", "dbo.IOUs");
            DropForeignKey("dbo.IOUs", "UnitID", "dbo.Units");
            DropForeignKey("dbo.IOUTxHistory", "IOUID", "dbo.IOUs");
            DropForeignKey("dbo.IOUs", "SettledByID", "dbo.Employees");
            DropForeignKey("dbo.IOUs", "RequestedByID", "dbo.Employees");
            DropForeignKey("dbo.IOUs", "RejectedByID", "dbo.Employees");
            DropForeignKey("dbo.IOUs", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.IOUs", "PayeeID", "dbo.Employees");
            DropForeignKey("dbo.IOUs", "PaidByID", "dbo.Employees");
            DropForeignKey("dbo.IOUs", "LeadApprovedByID", "dbo.Employees");
            DropForeignKey("dbo.IOUItems", "IOUID", "dbo.IOUs");
            DropForeignKey("dbo.IOUs", "HODApprovedByID", "dbo.Employees");
            DropForeignKey("dbo.IOUs", "FinanceApprovedByID", "dbo.Employees");
            DropForeignKey("dbo.IOUs", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.IOUItems", "CategoryID", "dbo.ExpenseCategories");
            DropForeignKey("dbo.IOUItems", "AccountID", "dbo.LedgerAccounts");
            DropForeignKey("dbo.GroupRules", "RuleID", "dbo.Rules");
            DropForeignKey("dbo.GroupRules", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.GroupEmployees", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.GroupEmployees", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.EventParticipantVendors", "EventParticipantID", "dbo.EventParticipants");
            DropForeignKey("dbo.EventParticipantVendors", "VendorID", "dbo.EventVendorInformations");
            DropForeignKey("dbo.EventParticipantTracks", "EventParticipantID", "dbo.EventParticipants");
            DropForeignKey("dbo.EventParticipantTracks", "TrackID", "dbo.EventTrackSessions");
            DropForeignKey("dbo.EventParticipants", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.EventParticipants", "IndustryID", "dbo.Industries");
            DropForeignKey("dbo.EventParticipants", "EventID", "dbo.Events");
            DropForeignKey("dbo.EventParticipants", "DesignationCategoryID", "dbo.DesignationCategories");
            DropForeignKey("dbo.EventParticipants", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.EventParticipants", "AccountManagerID", "dbo.Employees");
            DropForeignKey("dbo.EventVendorInformations", "EventID", "dbo.Events");
            DropForeignKey("dbo.EventTracks", "EventID", "dbo.Events");
            DropForeignKey("dbo.EventSponsors", "EventID", "dbo.Events");
            DropForeignKey("dbo.EventSessions", "EventID", "dbo.Events");
            DropForeignKey("dbo.EventTrackSessions", "SessionID", "dbo.EventSessions");
            DropForeignKey("dbo.EventTrackSessions", "TrackID", "dbo.EventTracks");
            DropForeignKey("dbo.EventTrackSessions", "Coordinator2ID", "dbo.Employees");
            DropForeignKey("dbo.EventTrackSessions", "Coordinator1ID", "dbo.Employees");
            DropForeignKey("dbo.EventHostCompanies", "EventID", "dbo.Events");
            DropForeignKey("dbo.EventHostCompanies", "Coordinator2ID", "dbo.Employees");
            DropForeignKey("dbo.EventHostCompanies", "Coordinator1ID", "dbo.Employees");
            DropForeignKey("dbo.EventHostCompanies", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.EventFiles", "EventID", "dbo.Events");
            DropForeignKey("dbo.EventCheckLists", "EventID", "dbo.Events");
            DropForeignKey("dbo.EventCheckLists", "AssigneeID", "dbo.Employees");
            DropForeignKey("dbo.EmailOutboxes", "CreatedByID", "dbo.Employees");
            DropForeignKey("dbo.Contacts", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.Contacts", "DesignationCategoryID", "dbo.DesignationCategories");
            DropForeignKey("dbo.Contacts", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.Contacts", "AccountManagerID", "dbo.Employees");
            DropForeignKey("dbo.AuditTrailDetails", "AuditTrailID", "dbo.AuditTrails");
            DropForeignKey("dbo.AssociatedCompanies", "SecondaryRepresentativeID", "dbo.Employees");
            DropForeignKey("dbo.AssociatedCompanies", "PrimaryRepresentativeID", "dbo.Employees");
            DropForeignKey("dbo.OrganizationIndustries", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.OrganizationIndustries", "IndustryID", "dbo.Industries");
            DropForeignKey("dbo.Organizations", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.AssociatedCompanies", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.AssociatedCompanies", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.Companies", "PettyCashOfficerID", "dbo.Employees");
            DropForeignKey("dbo.EmployeeUnits", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.EmployeeUnits", "UnitID", "dbo.Units");
            DropForeignKey("dbo.Units", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Units", "AuthorizedOfficerID", "dbo.Employees");
            DropForeignKey("dbo.EmployeeProjects", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.EmployeeProjects", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.ProjectExpenseAllocations", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.ProjectExpenseAllocations", "ExpenseCategoryID", "dbo.ExpenseCategories");
            DropForeignKey("dbo.Projects", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Departments", "AuthorizedOfficerID", "dbo.Employees");
            DropForeignKey("dbo.Projects", "AuthorizedOfficerID", "dbo.Employees");
            DropForeignKey("dbo.Employees", "JobCategoryID", "dbo.JobCategories");
            DropForeignKey("dbo.EmployeeFamilyMembers", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.EmployeeFamilyMembers", "EmployeeFamilyMember_ID", "dbo.EmployeeFamilyMembers");
            DropForeignKey("dbo.Employees", "CompanyID", "dbo.Companies");
            DropIndex("dbo.TimeZones", "IX_SampleDropDowns_Code");
            DropIndex("dbo.SystemConfigurations", new[] { "MCPaidByID" });
            DropIndex("dbo.SystemConfigurations", new[] { "MCFinanceApprovedByID" });
            DropIndex("dbo.SystemConfigurations", new[] { "MCHRApprovedByID" });
            DropIndex("dbo.SystemConfigurations", new[] { "MCHRCheckedByID" });
            DropIndex("dbo.SystemConfigurations", new[] { "TimeZoneID" });
            DropIndex("dbo.SentMails", new[] { "CreatedByID" });
            DropIndex("dbo.PettyCashReimbursementTxHistory", new[] { "PettyCashReimbursementID" });
            DropIndex("dbo.PettyCashReimbursements", new[] { "ReimbursedByID" });
            DropIndex("dbo.PettyCashReimbursements", new[] { "PreparedByID" });
            DropIndex("dbo.PettyCashReimbursements", new[] { "CompanyID" });
            DropIndex("dbo.PettyCashReimbursementItems", new[] { "PayeeID" });
            DropIndex("dbo.PettyCashReimbursementItems", new[] { "PettyCashReimbursementID" });
            DropIndex("dbo.MedicalClaimTxHistory", new[] { "MedicalClaimID" });
            DropIndex("dbo.MedicalClaimItems", new[] { "MedicalClaimID" });
            DropIndex("dbo.MedicalClaims", new[] { "CancelledByID" });
            DropIndex("dbo.MedicalClaims", new[] { "RejectedByID" });
            DropIndex("dbo.MedicalClaims", new[] { "PaidByID" });
            DropIndex("dbo.MedicalClaims", new[] { "FinanceApprovedByID" });
            DropIndex("dbo.MedicalClaims", new[] { "HRApprovedByID" });
            DropIndex("dbo.MedicalClaims", new[] { "HRCheckedByID" });
            DropIndex("dbo.MedicalClaims", new[] { "PayeeID" });
            DropIndex("dbo.MedicalClaimAttachments", new[] { "MedicalClaimID" });
            DropIndex("dbo.PettyCashVoucherTxHistory", new[] { "PettyCashVoucherID" });
            DropIndex("dbo.PettyCashVoucherItems", new[] { "AccountID" });
            DropIndex("dbo.PettyCashVoucherItems", new[] { "CategoryID" });
            DropIndex("dbo.PettyCashVoucherItems", new[] { "PettyCashVoucherID" });
            DropIndex("dbo.PettyCashVoucherAttachments", new[] { "PettyCashVoucherID" });
            DropIndex("dbo.PettyCashVouchers", new[] { "CancelledByID" });
            DropIndex("dbo.PettyCashVouchers", new[] { "RejectedByID" });
            DropIndex("dbo.PettyCashVouchers", new[] { "DisbursedByID" });
            DropIndex("dbo.PettyCashVouchers", new[] { "FinanceApprovedByID" });
            DropIndex("dbo.PettyCashVouchers", new[] { "HODApprovedByID" });
            DropIndex("dbo.PettyCashVouchers", new[] { "LeadApprovedByID" });
            DropIndex("dbo.PettyCashVouchers", new[] { "RequestedByID" });
            DropIndex("dbo.PettyCashVouchers", new[] { "PayeeID" });
            DropIndex("dbo.PettyCashVouchers", new[] { "UnitID" });
            DropIndex("dbo.PettyCashVouchers", new[] { "ProjectID" });
            DropIndex("dbo.PettyCashVouchers", new[] { "CompanyID" });
            DropIndex("dbo.IOUSettlements", new[] { "PettyCashVoucherID" });
            DropIndex("dbo.IOUSettlements", new[] { "IOUID" });
            DropIndex("dbo.IOUTxHistory", new[] { "IOUID" });
            DropIndex("dbo.IOUs", new[] { "SettledByID" });
            DropIndex("dbo.IOUs", new[] { "RejectedByID" });
            DropIndex("dbo.IOUs", new[] { "PaidByID" });
            DropIndex("dbo.IOUs", new[] { "FinanceApprovedByID" });
            DropIndex("dbo.IOUs", new[] { "HODApprovedByID" });
            DropIndex("dbo.IOUs", new[] { "LeadApprovedByID" });
            DropIndex("dbo.IOUs", new[] { "RequestedByID" });
            DropIndex("dbo.IOUs", new[] { "PayeeID" });
            DropIndex("dbo.IOUs", new[] { "UnitID" });
            DropIndex("dbo.IOUs", new[] { "ProjectID" });
            DropIndex("dbo.IOUs", new[] { "CompanyID" });
            DropIndex("dbo.IOUItems", new[] { "AccountID" });
            DropIndex("dbo.IOUItems", new[] { "CategoryID" });
            DropIndex("dbo.IOUItems", new[] { "IOUID" });
            DropIndex("dbo.GroupRules", new[] { "RuleID" });
            DropIndex("dbo.GroupRules", new[] { "GroupID" });
            DropIndex("dbo.Groups", "IX_Groups_Description");
            DropIndex("dbo.GroupEmployees", new[] { "EmployeeID" });
            DropIndex("dbo.GroupEmployees", new[] { "GroupID" });
            DropIndex("dbo.EventParticipantVendors", new[] { "VendorID" });
            DropIndex("dbo.EventParticipantVendors", new[] { "EventParticipantID" });
            DropIndex("dbo.EventParticipantTracks", new[] { "TrackID" });
            DropIndex("dbo.EventParticipantTracks", new[] { "EventParticipantID" });
            DropIndex("dbo.EventParticipants", new[] { "AccountManagerID" });
            DropIndex("dbo.EventParticipants", new[] { "CountryID" });
            DropIndex("dbo.EventParticipants", new[] { "DesignationCategoryID" });
            DropIndex("dbo.EventParticipants", new[] { "IndustryID" });
            DropIndex("dbo.EventParticipants", new[] { "OrganizationID" });
            DropIndex("dbo.EventParticipants", new[] { "EventID" });
            DropIndex("dbo.EventVendorInformations", new[] { "EventID" });
            DropIndex("dbo.EventSponsors", new[] { "EventID" });
            DropIndex("dbo.EventTracks", new[] { "EventID" });
            DropIndex("dbo.EventTrackSessions", new[] { "Coordinator2ID" });
            DropIndex("dbo.EventTrackSessions", new[] { "Coordinator1ID" });
            DropIndex("dbo.EventTrackSessions", new[] { "TrackID" });
            DropIndex("dbo.EventTrackSessions", new[] { "SessionID" });
            DropIndex("dbo.EventSessions", new[] { "EventID" });
            DropIndex("dbo.EventHostCompanies", new[] { "Coordinator2ID" });
            DropIndex("dbo.EventHostCompanies", new[] { "Coordinator1ID" });
            DropIndex("dbo.EventHostCompanies", new[] { "CompanyID" });
            DropIndex("dbo.EventHostCompanies", new[] { "EventID" });
            DropIndex("dbo.EventFiles", new[] { "EventID" });
            DropIndex("dbo.EventCheckLists", new[] { "AssigneeID" });
            DropIndex("dbo.EventCheckLists", new[] { "EventID" });
            DropIndex("dbo.EmailOutboxes", new[] { "CreatedByID" });
            DropIndex("dbo.DesignationCategories", "IX_DesignationCategories_Code");
            DropIndex("dbo.Contacts", new[] { "AccountManagerID" });
            DropIndex("dbo.Contacts", new[] { "DesignationCategoryID" });
            DropIndex("dbo.Contacts", new[] { "OrganizationID" });
            DropIndex("dbo.Contacts", new[] { "CountryID" });
            DropIndex("dbo.AuditTrailDetails", new[] { "AuditTrailID" });
            DropIndex("dbo.Industries", "IX_Industries_Name");
            DropIndex("dbo.OrganizationIndustries", new[] { "IndustryID" });
            DropIndex("dbo.OrganizationIndustries", new[] { "OrganizationID" });
            DropIndex("dbo.Countries", "IX_Countries_Name");
            DropIndex("dbo.Organizations", new[] { "CountryID" });
            DropIndex("dbo.Units", new[] { "DepartmentID" });
            DropIndex("dbo.Units", new[] { "AuthorizedOfficerID" });
            DropIndex("dbo.EmployeeUnits", new[] { "UnitID" });
            DropIndex("dbo.EmployeeUnits", new[] { "EmployeeID" });
            DropIndex("dbo.ProjectExpenseAllocations", new[] { "ExpenseCategoryID" });
            DropIndex("dbo.ProjectExpenseAllocations", new[] { "ProjectID" });
            DropIndex("dbo.Departments", new[] { "AuthorizedOfficerID" });
            DropIndex("dbo.Projects", new[] { "DepartmentID" });
            DropIndex("dbo.Projects", new[] { "AuthorizedOfficerID" });
            DropIndex("dbo.EmployeeProjects", new[] { "ProjectID" });
            DropIndex("dbo.EmployeeProjects", new[] { "EmployeeID" });
            DropIndex("dbo.EmployeeFamilyMembers", new[] { "EmployeeFamilyMember_ID" });
            DropIndex("dbo.EmployeeFamilyMembers", new[] { "EmployeeID" });
            DropIndex("dbo.Employees", new[] { "JobCategoryID" });
            DropIndex("dbo.Employees", new[] { "CompanyID" });
            DropIndex("dbo.Employees", "IX_Employees_Code");
            DropIndex("dbo.Companies", new[] { "PettyCashOfficerID" });
            DropIndex("dbo.Companies", "IX_Companies_Code");
            DropIndex("dbo.AssociatedCompanies", new[] { "SecondaryRepresentativeID" });
            DropIndex("dbo.AssociatedCompanies", new[] { "PrimaryRepresentativeID" });
            DropIndex("dbo.AssociatedCompanies", new[] { "CompanyID" });
            DropIndex("dbo.AssociatedCompanies", new[] { "OrganizationID" });
            DropTable("dbo.TempContacts");
            DropTable("dbo.TimeZones");
            DropTable("dbo.SystemConfigurations");
            DropTable("dbo.SentMails");
            DropTable("dbo.PettyCashReimbursementTxHistory");
            DropTable("dbo.PettyCashReimbursements");
            DropTable("dbo.PettyCashReimbursementItems");
            DropTable("dbo.MedicalClaimTxHistory");
            DropTable("dbo.MedicalClaimItems");
            DropTable("dbo.MedicalClaims");
            DropTable("dbo.MedicalClaimAttachments");
            DropTable("dbo.PettyCashVoucherTxHistory");
            DropTable("dbo.PettyCashVoucherItems");
            DropTable("dbo.PettyCashVoucherAttachments");
            DropTable("dbo.PettyCashVouchers");
            DropTable("dbo.IOUSettlements");
            DropTable("dbo.IOUTxHistory");
            DropTable("dbo.IOUs");
            DropTable("dbo.LedgerAccounts");
            DropTable("dbo.IOUItems");
            DropTable("dbo.Rules");
            DropTable("dbo.GroupRules");
            DropTable("dbo.Groups");
            DropTable("dbo.GroupEmployees");
            DropTable("dbo.EventParticipantVendors");
            DropTable("dbo.EventParticipantTracks");
            DropTable("dbo.EventParticipants");
            DropTable("dbo.EventVendorInformations");
            DropTable("dbo.EventSponsors");
            DropTable("dbo.EventTracks");
            DropTable("dbo.EventTrackSessions");
            DropTable("dbo.EventSessions");
            DropTable("dbo.EventHostCompanies");
            DropTable("dbo.EventFiles");
            DropTable("dbo.Events");
            DropTable("dbo.EventCheckLists");
            DropTable("dbo.EmailOutboxes");
            DropTable("dbo.DesignationCategories");
            DropTable("dbo.Contacts");
            DropTable("dbo.AuditTrails");
            DropTable("dbo.AuditTrailDetails");
            DropTable("dbo.Industries");
            DropTable("dbo.OrganizationIndustries");
            DropTable("dbo.Countries");
            DropTable("dbo.Organizations");
            DropTable("dbo.Units");
            DropTable("dbo.EmployeeUnits");
            DropTable("dbo.ExpenseCategories");
            DropTable("dbo.ProjectExpenseAllocations");
            DropTable("dbo.Departments");
            DropTable("dbo.Projects");
            DropTable("dbo.EmployeeProjects");
            DropTable("dbo.JobCategories");
            DropTable("dbo.EmployeeFamilyMembers");
            DropTable("dbo.Employees");
            DropTable("dbo.Companies");
            DropTable("dbo.AssociatedCompanies");
        }
    }
}
