namespace Hotel_Reservation_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dbtrue : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Room_Usage", newName: "Stays");
            AddColumn("dbo.Rooms", "Number", c => c.String(nullable: false));
            AddColumn("dbo.Rooms", "Type", c => c.String(nullable: false));
            AddColumn("dbo.Stays", "RoomId", c => c.Int(nullable: false));
            AddColumn("dbo.Stays", "GuestId", c => c.String(nullable: false));
            DropColumn("dbo.Rooms", "Name");
            DropColumn("dbo.Rooms", "RType");
            DropColumn("dbo.Stays", "Room_Id");
            DropColumn("dbo.Stays", "Guest_Id");
            DropColumn("dbo.Stays", "Isactive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stays", "Isactive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Stays", "Guest_Id", c => c.String(nullable: false));
            AddColumn("dbo.Stays", "Room_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Rooms", "RType", c => c.String(nullable: false));
            AddColumn("dbo.Rooms", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Stays", "GuestId");
            DropColumn("dbo.Stays", "RoomId");
            DropColumn("dbo.Rooms", "Type");
            DropColumn("dbo.Rooms", "Number");
            RenameTable(name: "dbo.Stays", newName: "Room_Usage");
        }
    }
}
