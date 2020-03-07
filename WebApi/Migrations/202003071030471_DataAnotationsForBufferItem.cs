namespace MultiBuffer.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataAnotationsForBufferItem : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BufferItems", "Name", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BufferItems", "Name", c => c.String());
        }
    }
}
