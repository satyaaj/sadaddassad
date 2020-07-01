namespace AirportManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PilotSchedules", name: "AdminDetails_AdminID", newName: "AdminID");
            RenameIndex(table: "dbo.PilotSchedules", name: "IX_AdminDetails_AdminID", newName: "IX_AdminID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PilotSchedules", name: "IX_AdminID", newName: "IX_AdminDetails_AdminID");
            RenameColumn(table: "dbo.PilotSchedules", name: "AdminID", newName: "AdminDetails_AdminID");
        }
    }
}
