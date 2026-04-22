namespace CrudApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddUniqueEmailConstraint : DbMigration
    {
        public override void Up()
        {
            // First alter the column from nvarchar(MAX) to nvarchar(256)
            // This is required before we can create an index on it
            AlterColumn("dbo.Students", "Email", c => c.String(maxLength: 256));

            // Now create the unique index on the Email column
            CreateIndex("dbo.Students", "Email", unique: true, name: "IX_Student_Email");
        }

        public override void Down()
        {
            DropIndex("dbo.Students", "IX_Student_Email");
            AlterColumn("dbo.Students", "Email", c => c.String());
        }
    }
}