namespace Hotel_Reservation_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Isactive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Room_Usage", "Isactive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Room_Usage", "Isactive");
        }
    }
}
