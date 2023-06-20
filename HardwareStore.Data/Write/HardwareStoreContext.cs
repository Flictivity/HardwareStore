using Microsoft.EntityFrameworkCore;
using HardwareStore.Data.Models;

namespace HardwareStore.Data.Write;

public partial class HardwareStoreContext : DbContext
{
    public HardwareStoreContext()
    {
    }

    public HardwareStoreContext(DbContextOptions<HardwareStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoryDb> Categories { get; set; } = null!;

    public virtual DbSet<CategoryTitleDb> CategoryTitles { get; set; } = null!;

    public virtual DbSet<CategoryTitleValueDb> CategoryTitleValues { get; set; } = null!;

    public virtual DbSet<MainCategoryDb> MainCategories { get; set; } = null!;

    public virtual DbSet<OrderDb> Orders { get; set; } = null!;

    public virtual DbSet<OrderProductDb> OrderProducts { get; set; } = null!;

    public virtual DbSet<ProductDb> Products { get; set; } = null!;

    public virtual DbSet<ProductImageDb> ProductImages { get; set; } = null!;

    public virtual DbSet<UserDb> Users { get; set; } = null!;

    public virtual DbSet<UserCartDb> UserCarts { get; set; } = null!;

    public virtual DbSet<UserFavoriteDb> UserFavorites { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryDb>(entity =>
        {
            entity.ToTable("category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MainCategoryId).HasColumnName("main_category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.MainCategoryDb).WithMany(p => p.Categories)
                .HasForeignKey(d => d.MainCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_category_main_category_id_main_category_id");
        });

        modelBuilder.Entity<CategoryTitleDb>(entity =>
        {
            entity.ToTable("category_title");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.CategoryDb).WithMany(p => p.CategoryTitles)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_category_title_category_id_category_id");
        });

        modelBuilder.Entity<CategoryTitleValueDb>(entity =>
        {
            entity.ToTable("category_title_value");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryTitleId).HasColumnName("category_title_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Value).HasColumnName("value");

            entity.HasOne(d => d.CategoryTitleDb).WithMany(p => p.CategoryTitleValues)
                .HasForeignKey(d => d.CategoryTitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_category_title_value_category_title_id_category_title_id");

            entity.HasOne(d => d.ProductDb).WithMany(p => p.CategoryTitleValues)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_category_title_value_product_id_product_id");
        });

        modelBuilder.Entity<MainCategoryDb>(entity =>
        {
            entity.ToTable("main_category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<OrderDb>(entity =>
        {
            entity.ToTable("order");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrderDate)
                .HasColumnType("timestamp wit time zone")
                .HasColumnName("order_date");
            entity.Property(e => e.OrderSum).HasColumnName("order_sum");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.UserDb).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_order_user_id_user_id");
        });

        modelBuilder.Entity<OrderProductDb>(entity =>
        {
            entity.ToTable("order_product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.OrderDb).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_order_product_order_id_order_id");

            entity.HasOne(d => d.ProductDb).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_order_product_product_id_product_id");
        });

        modelBuilder.Entity<ProductDb>(entity =>
        {
            entity.ToTable("product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ProductImageDb>(entity =>
        {
            entity.ToTable("product_image");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ImageSource).HasColumnName("image_source");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.ProductDb).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_product_image_product_id_product_id");
        });

        modelBuilder.Entity<UserDb>(entity =>
        {
            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "UC_user_email").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(150)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(150)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");
        });

        modelBuilder.Entity<UserCartDb>(entity =>
        {
            entity.ToTable("user_cart");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProductCount).HasColumnName("product_count");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.ProductDb).WithMany(p => p.UserCarts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_cart_product_id_product_id");

            entity.HasOne(d => d.UserDb).WithMany(p => p.UserCarts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_cart_user_id_user_id");
        });

        modelBuilder.Entity<UserFavoriteDb>(entity =>
        {
            entity.ToTable("user_favorite");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.ProductDb).WithMany(p => p.UserFavorites)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_favorite_product_id_product_id");

            entity.HasOne(d => d.UserDb).WithMany(p => p.UserFavorites)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_favorite_user_id_user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
