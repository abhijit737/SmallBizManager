using Microsoft.EntityFrameworkCore;
using SmallBizManager.Models;

namespace SmallBizManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Tables
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        //    protected override void OnModelCreating(ModelBuilder modelBuilder)
        //    {
        //        base.OnModelCreating(modelBuilder);

        //        // Set relationships
        //        modelBuilder.Entity<OrderItem>()
        //            .HasOne(o => o.Order)
        //            .WithMany(o => o.Items)
        //            .HasForeignKey(o => o.OrderId);

        //        modelBuilder.Entity<OrderItem>()
        //            .HasOne(o => o.Product)
        //            .WithMany()
        //            .HasForeignKey(o => o.ProductId);

        //        // Specify precision and scale for decimal properties
        //        modelBuilder.Entity<Employee>()
        //            .Property(e => e.Salary)
        //            .HasColumnType("decimal(18,2)");

        //        modelBuilder.Entity<Order>()
        //            .Property(o => o.TotalAmount)
        //            .HasColumnType("decimal(18,2)");

        //        modelBuilder.Entity<Order>()
        //.HasOne(o => o.Employee)
        //.WithMany(e => e.Orders) // Add a navigation property in Employee if not already present
        //.HasForeignKey(o => o.EmployeeId)
        //.OnDelete(DeleteBehavior.SetNull); // Optional: Set null if the employee is deleted


        //        modelBuilder.Entity<Product>()
        //.HasMany(p => p.OrderItems)
        //.WithOne(oi => oi.Product)
        //.HasForeignKey(oi => oi.ProductId);

        //        modelBuilder.Entity<OrderItem>()
        //.HasOne(oi => oi.Order)
        //.WithMany(o => o.Items)
        //.HasForeignKey(oi => oi.OrderId)
        //.OnDelete(DeleteBehavior.Cascade);



        //        modelBuilder.Entity<Product>()
        //            .Property(p => p.Price)
        //            .HasColumnType("decimal(18,2)");

        //        modelBuilder.Entity<Product>()
        //            .Property(p => p.Stock)
        //            .HasColumnType("decimal(18,2)");

        //        modelBuilder.Entity<OrderItem>()
        //        .Property(o => o.UnitPrice)
        //        .HasColumnType("decimal(18,2)");
        //    }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade); 
           
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems) 
                .HasForeignKey(oi => oi.ProductId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Employee)
                .WithMany(e => e.Orders) 
                .HasForeignKey(o => o.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull); 

            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.Stock)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasColumnType("decimal(18,2)");



            modelBuilder
    .Entity<Order>()
    .Property(o => o.Status)
    .HasConversion<string>()
    .HasMaxLength(20);


            modelBuilder.Entity<User>()
    .Property(u => u.Role)
    .HasConversion<string>()           
    .HasMaxLength(20);

            modelBuilder.Entity<User>()
    .Property(u => u.Role)
    .HasConversion<string>()
    .HasMaxLength(20);


        }


    }
}
