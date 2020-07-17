using CredoLoan.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CredoLoan.DAL {
    public class CredoLoanDbContext : DbContext {
        public CredoLoanDbContext(DbContextOptions<CredoLoanDbContext> options) : base(options) {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<LoanApplication> LoanApplications { get; set; }
        public DbSet<ClientRefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Client>(builder => {
                builder.HasKey(c => c.Id);

                builder.Property(c => c.FirstName).HasMaxLength(70).IsRequired();
                builder.Property(c => c.LastName).HasMaxLength(100).IsRequired();
                builder.Property(c => c.PersonalId).HasColumnType("char(11)").IsRequired();
                builder.Property(c => c.PasswordHash).HasColumnType("varchar(48)").IsRequired();
                builder.Property(c => c.DateOfBirth).HasColumnType("date");

                builder.HasMany(c => c.Applications).WithOne(a => a.Client).HasForeignKey(a => a.ClientId);
            });

            modelBuilder.Entity<ClientRefreshToken>(builder => {
                builder.HasKey(cr => cr.ClientId);
                builder.Property(cr => cr.RefreshToken).HasColumnType("varchar(50)").IsRequired();
            });

            modelBuilder.Entity<Client>().HasIndex(c => c.PersonalId).IsUnique();
        }
    }
}
