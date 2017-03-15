namespace CarSharing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CS0002 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.T_Log",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdUser = c.String(),
                        DatOra = c.DateTime(nullable: false),
                        Azione = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.T_Log");
        }
    }
}
