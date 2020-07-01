namespace AirportManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newChanges3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PilotSchedules",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    PilotID = c.String(maxLength: 128),
                    AdminID = c.String(maxLength: 225),
                    PilotAvailabilityFrom = c.DateTime(nullable: false),
                    PilotAvailabilityTo = c.DateTime(nullable: false),
                    IsActive = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AdminDetails", t => t.AdminID)
                .ForeignKey("dbo.PilotDetails", t => t.PilotID)
                .Index(t => t.PilotID)
                .Index(t => t.AdminID);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PilotSchedules", "PilotID", "dbo.PilotDetails");
            DropForeignKey("dbo.PilotSchedules", "AdminID", "dbo.AdminDetails");
            DropIndex("dbo.PilotSchedules", new[] { "AdminID" });
            DropIndex("dbo.PilotSchedules", new[] { "PilotID" });
            DropTable("dbo.PilotSchedules");
        }
    }
}
