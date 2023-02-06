using Microsoft.EntityFrameworkCore;
using TemplateRoleAccess.API.Models.Entities;

namespace TemplateRoleAccess.API.Models.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee>? Employees { get; set; }
        public DbSet<Departement>? Departements { get; set; }
        public DbSet<Account>? Accounts { get; set; }
        public DbSet<Role>? Roles { get; set; }
        public DbSet<AccountRole>? AccountRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountRole>().HasKey(ar => new { ar.AccountNIK, ar.RoleId } );

            modelBuilder.Entity<AccountRole>()
                .HasOne(ar => ar.Accounts)
                .WithMany(e => e.AccountRoles)
                .HasForeignKey(ar => ar.AccountNIK);

            modelBuilder.Entity<AccountRole>()
                .HasOne(ar => ar.Roles)
                .WithMany(r => r.AccountRoles)
                .HasForeignKey(ar => ar.RoleId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }

}
