using FluentMigrator;

namespace HardwareStore.Data.Migrations;

[Migration(2023_06_15_01)]
public class Migration20230615 : Migration
{
    public override void Up()
    {
        Alter.Table("user")
            .AddColumn("role").AsInt32();
    }

    public override void Down()
    {
    }
}