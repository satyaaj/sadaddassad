namespace AirportManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newChanges4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PilotSchedules", "PilotAvailabilityFrom", c => c.DateTime());
            AlterColumn("dbo.PilotSchedules", "PilotAvailabilityTo", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PilotSchedules", "PilotAvailabilityTo", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PilotSchedules", "PilotAvailabilityFrom", c => c.DateTime(nullable: false));
        }
    }
}
