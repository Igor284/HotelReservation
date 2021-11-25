namespace Hotel_Reservation_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changehours : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stays", "Hours", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stays", "Hours", c => c.Double(nullable: false));
        }
    }
}
