using FluentMigrator;

namespace HardwareStore.Data.Migrations;

[Migration(2023_06_20_01)]
public class Migration2023062001 : Migration
{
    public override void Up()
    {
        Alter.Column("order_date")
            .OnTable("order")
            .AsCustom("timestamp with time zone");
    }

    public override void Down()
    {
    }
}