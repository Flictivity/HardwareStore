using FluentMigrator;

namespace HardwareStore.Data.Migrations;

[Migration(2023_06_17_01)]
public class Migration20230617 : Migration
{
    public override void Up()
    {
        Create.ForeignKey()
            .FromTable("category_title_value").ForeignColumn("product_id")
            .ToTable("product").PrimaryColumn("id");
    }

    public override void Down()
    {
    }
}