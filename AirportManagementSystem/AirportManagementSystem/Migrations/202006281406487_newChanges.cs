namespace AirportManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminDetails",
                c => new
                    {
                        AdminID = c.String(nullable: false, maxLength: 225),
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 225),
                        LastName = c.String(nullable: false, maxLength: 225),
                        Age = c.Short(nullable: false),
                        Gender = c.Int(nullable: false),
                        ContectNumber = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 225),
                        isApproved = c.Boolean(nullable: false),
                        RoleID = c.Int(),
                    })
                .PrimaryKey(t => t.AdminID)
                .ForeignKey("dbo.UserRoles", t => t.RoleID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.HangarDetails",
                c => new
                    {
                        HangerName = c.String(nullable: false, maxLength: 128),
                        ID = c.Int(nullable: false, identity: true),
                        AdminID = c.String(maxLength: 225),
                        Terminal = c.Int(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        isPlaneAllocated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.HangerName)
                .ForeignKey("dbo.AdminDetails", t => t.AdminID)
                .Index(t => t.AdminID);
            
            CreateTable(
                "dbo.PlanesAllotments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HangarName = c.String(nullable: false, maxLength: 128),
                        PlaneID = c.String(nullable: false, maxLength: 128),
                        ManagerID = c.String(maxLength: 128),
                        isPlaneAllocated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HangarDetails", t => t.HangarName, cascadeDelete: true)
                .ForeignKey("dbo.ManagerDetails", t => t.ManagerID)
                .ForeignKey("dbo.PlaneDetails", t => t.PlaneID, cascadeDelete: true)
                .Index(t => t.HangarName)
                .Index(t => t.PlaneID)
                .Index(t => t.ManagerID);
            
            CreateTable(
                "dbo.ManagerDetails",
                c => new
                    {
                        ManagerID = c.String(nullable: false, maxLength: 128),
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 225),
                        LastName = c.String(nullable: false, maxLength: 225),
                        Age = c.Short(nullable: false),
                        Gender = c.Int(nullable: false),
                        ContectNumber = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        isApproved = c.Boolean(nullable: false),
                        RoleID = c.Int(),
                    })
                .PrimaryKey(t => t.ManagerID)
                .ForeignKey("dbo.UserRoles", t => t.RoleID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RolesName = c.String(),
                        RoleID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PilotDetails",
                c => new
                    {
                        PilotID = c.String(nullable: false, maxLength: 128),
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 225),
                        LastName = c.String(nullable: false, maxLength: 225),
                        Age = c.Short(nullable: false),
                        Gender = c.Int(nullable: false),
                        ContactNumber = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 225),
                        isApproved = c.Boolean(nullable: false),
                        RoleID = c.Int(),
                    })
                .PrimaryKey(t => t.PilotID)
                .ForeignKey("dbo.UserRoles", t => t.RoleID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.SuperUsers",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false),
                        RoleID = c.Int(),
                    })
                .PrimaryKey(t => t.UserName)
                .ForeignKey("dbo.UserRoles", t => t.RoleID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.PlaneDetails",
                c => new
                    {
                        PlaneID = c.String(nullable: false, maxLength: 128),
                        OwnerID = c.String(nullable: false),
                        OwnerFirstName = c.String(nullable: false),
                        OwnerLastName = c.String(nullable: false),
                        OwnerEmail = c.String(nullable: false),
                        PlaneType = c.Int(nullable: false),
                        PlaneCapacity = c.Int(nullable: false),
                        isAllotted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PlaneID);
            
            CreateTable(
                "dbo.FlightPlanDetails",
                c => new
                    {
                        FlightPlanID = c.Int(nullable: false, identity: true),
                        PlaneID = c.String(nullable: false, maxLength: 128),
                        PilotID = c.String(),
                        DepartureLocation = c.Int(nullable: false),
                        ArrivalLocation = c.Int(nullable: false),
                        DepartureTime = c.DateTime(nullable: false),
                        ArrivalTime = c.DateTime(nullable: false),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FlightPlanID)
                .ForeignKey("dbo.PlaneDetails", t => t.PlaneID, cascadeDelete: true)
                .Index(t => t.PlaneID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdminDetails", "RoleID", "dbo.UserRoles");
            DropForeignKey("dbo.PlanesAllotments", "PlaneID", "dbo.PlaneDetails");
            DropForeignKey("dbo.FlightPlanDetails", "PlaneID", "dbo.PlaneDetails");
            DropForeignKey("dbo.PlanesAllotments", "ManagerID", "dbo.ManagerDetails");
            DropForeignKey("dbo.ManagerDetails", "RoleID", "dbo.UserRoles");
            DropForeignKey("dbo.SuperUsers", "RoleID", "dbo.UserRoles");
            DropForeignKey("dbo.PilotDetails", "RoleID", "dbo.UserRoles");
            DropForeignKey("dbo.PlanesAllotments", "HangarName", "dbo.HangarDetails");
            DropForeignKey("dbo.HangarDetails", "AdminID", "dbo.AdminDetails");
            DropIndex("dbo.FlightPlanDetails", new[] { "PlaneID" });
            DropIndex("dbo.SuperUsers", new[] { "RoleID" });
            DropIndex("dbo.PilotDetails", new[] { "RoleID" });
            DropIndex("dbo.ManagerDetails", new[] { "RoleID" });
            DropIndex("dbo.PlanesAllotments", new[] { "ManagerID" });
            DropIndex("dbo.PlanesAllotments", new[] { "PlaneID" });
            DropIndex("dbo.PlanesAllotments", new[] { "HangarName" });
            DropIndex("dbo.HangarDetails", new[] { "AdminID" });
            DropIndex("dbo.AdminDetails", new[] { "RoleID" });
            DropTable("dbo.FlightPlanDetails");
            DropTable("dbo.PlaneDetails");
            DropTable("dbo.SuperUsers");
            DropTable("dbo.PilotDetails");
            DropTable("dbo.UserRoles");
            DropTable("dbo.ManagerDetails");
            DropTable("dbo.PlanesAllotments");
            DropTable("dbo.HangarDetails");
            DropTable("dbo.AdminDetails");
        }
    }
}
