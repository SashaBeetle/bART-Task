using bART_Task.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace bART_Task.EF
{
    public class bARTTaskContext : DbContext
    {
        public bARTTaskContext(DbContextOptions options) : base(options){}
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Incident>()
                .HasKey(i => i.Name);

            modelBuilder.Entity<Account>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Account>()
                .HasIndex(a => a.Name)
                .IsUnique();

            modelBuilder.Entity<Contact>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Contact>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Incident>()
                .HasOne(i => i.Account)
                .WithMany(a => a.Incidents)
                .HasForeignKey(i => i.AccountId);

            modelBuilder.Entity<Contact>()
                .HasOne(c => c.Account)
                .WithMany(a => a.Contacts)
                .HasForeignKey(c => c.AccountId);
        }
    }
}
