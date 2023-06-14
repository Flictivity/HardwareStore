using FluentMigrator;

namespace HardwareStore.Data.Migrations;

[Migration(2023_06_14_01)]
public class InitialMigration : Migration
{
    public override void Up()
    {
        Create.Table("main_category")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("name").AsString(100).NotNullable();
        
        Create.Table("category")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("name").AsString(100).NotNullable()
            .WithColumn("main_category_id").AsInt64().ForeignKey("main_category", "id");

        Create.Table("user")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("email").AsString(50).NotNullable()
            .WithColumn("password").AsString(50).NotNullable()
            .WithColumn("last_name").AsString(150).NotNullable()
            .WithColumn("first_name").AsString(150).NotNullable();

        Create.Table("product")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("name").AsString(150)
            .WithColumn("cost").AsInt64()
            .WithColumn("count").AsInt32()
            .WithColumn("code").AsInt64()
            .WithColumn("description").AsString().Nullable();

        Create.Table("product_image")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("product_id").AsInt64().ForeignKey("product", "id")
            .WithColumn("image_source").AsString().NotNullable();

        Create.Table("category_title")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("name").AsString(100).NotNullable()
            .WithColumn("category_id").AsInt64().ForeignKey("category","id");

        Create.Table("category_title_value")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("product_id").AsInt64().ForeignKey()
            .WithColumn("category_title_id").AsInt64().ForeignKey("category_title","id")
            .WithColumn("value").AsString().NotNullable();

        Create.Table("user_cart")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("product_id").AsInt64().ForeignKey("product","id")
            .WithColumn("user_id").AsInt64().ForeignKey("user","id")
            .WithColumn("product_count").AsInt64();

        Create.Table("user_favorite")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("product_id").AsInt64().ForeignKey("product", "id")
            .WithColumn("user_id").AsInt64().ForeignKey("user", "id");

        Create.Table("order")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("user_id").AsInt64().ForeignKey("user", "id")
            .WithColumn("order_date").AsDateTime()
            .WithColumn("order_sum").AsInt64();

        Create.Table("order_product")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("order_id").AsInt64().ForeignKey("order", "id")
            .WithColumn("product_id").AsInt64().ForeignKey("product", "id")
            .WithColumn("count").AsInt64();

        Create.UniqueConstraint()
            .OnTable("user")
            .Column("email");
    }

    public override void Down()
    {
        
    }
}