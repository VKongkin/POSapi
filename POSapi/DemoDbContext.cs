using Microsoft.EntityFrameworkCore;
using POSapi.Model.Data;

namespace POSapi
{
    public class DemoDbContext : DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder opt)
        //{
        //    opt.UseSqlServer(@"Server=KONGKIN\SQLEXPRESS;Database=POS;Trusted_Connection=True;TrustServerCertificate=True");
        //    //opt.UseOracle("User Id=BBU207; Password=mypassword; Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1524))(CONNECT_DATA=(SERVER=dedicated)(SID=xe)))");
        //}
        public DemoDbContext(DbContextOptions<DemoDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Staff> Staffs { get; set;}
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<UserDesc> UserDesc { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<SKU> Skus { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<ProImage> ProImages { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PODetail> PODetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PODetail>()
            .HasOne(_ => _.PurchaseOrder)
            .WithMany(a => a.Details)
            .HasForeignKey(p => p.PoId);

            modelBuilder.Entity<InvoiceItem>()
                .HasOne(_ => _.Invoice)
                .WithMany(a => a.InvoiceItems)
                .HasForeignKey(i => i.InvoiceId);
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoicesItem { get; set; }

    }
}
