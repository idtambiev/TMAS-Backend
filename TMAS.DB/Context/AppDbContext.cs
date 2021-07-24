using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TMAS.DB.Models;

namespace TMAS.DB.Context
{
   public class AppDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<History> Histories { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
           //Database.EnsureDeleted();
           //Database.EnsureCreated();
           Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //boards
            modelBuilder.Entity<Board>()
                .Property(b => b.Title)
                .HasColumnType("varchar(100)")
                .IsRequired();

            modelBuilder.Entity<Board>()
               .HasOne(p => p.User)
               .WithMany(b => b.Boards)
               .HasForeignKey(b => b.BoardUserId);

            modelBuilder.Entity<Board>()
                .Property(b => b.CreatedDate)
                .IsRequired();

            modelBuilder.Entity<Board>()
                .Property(b => b.UpdatedDate)
                .IsRequired();

            modelBuilder.Entity<Board>()
                .Property(b => b.IsActive)
                .IsRequired();

            //columns
            modelBuilder.Entity<Column>()
               .Property(u => u.Title)
               .HasColumnType("varchar(100)")
               .IsRequired();

            modelBuilder.Entity<Column>()
                .HasOne(p => p.Board)
                .WithMany(c => c.Columns)
                .HasForeignKey(b => b.BoardId);

            modelBuilder.Entity<Column>()
                .Property(c => c.CreatedDate)
                .IsRequired();

            modelBuilder.Entity<Column>()
                .Property(c => c.UpdatedDate)
                .IsRequired();

            modelBuilder.Entity<Column>()
                .Property(c => c.IsActive)
                .IsRequired();

            modelBuilder.Entity<Column>()
                .Property(c => c.SortBy)
                .IsRequired();

            //cards
            modelBuilder.Entity<Card>()
                .Property(u => u.Title)
                .HasColumnType("varchar(100)")
                .IsRequired();

            modelBuilder.Entity<Card>()
                .Property(u => u.Text)
                .HasColumnType("varchar(5000)")
                .IsRequired(false);

            modelBuilder.Entity<Card>()
                .HasOne(p => p.Column)
                .WithMany(b => b.Cards);

            modelBuilder.Entity<Card>()
                .Property(c => c.CreatedDate)
                .IsRequired();

            modelBuilder.Entity<Card>()
                .Property(c => c.UpdatedDate)
                .IsRequired();

            modelBuilder.Entity<Card>()
                .Property(c => c.IsActive)
                .IsRequired();

            modelBuilder.Entity<Card>()
                .Property(c => c.SortBy)
                .IsRequired();

            modelBuilder.Entity<Card>()
                .Property(c => c.IsDone)
                .IsRequired();


            //history
            modelBuilder.Entity<History>()
                .HasOne(p => p.User)
                .WithMany(b => b.Histories)
                .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<History>()
                .Property(u => u.ActionObject)
                .HasColumnType("varchar(100)")
                .IsRequired();

            modelBuilder.Entity<History>()
                .Property(e => e.ActionType)
                .HasConversion<byte>()
                .IsRequired();

            modelBuilder.Entity<History>()
                .Property(e => e.CreatedDate)
                .IsRequired();

            modelBuilder.Entity<History>()
                .Property(e => e.UpdatedDate)
                .IsRequired();

            //user
            modelBuilder.Entity<User>()
               .Property(u => u.Name)
               .HasColumnType("varchar(30)")
               .IsRequired();


            modelBuilder.Entity<User>()
               .Property(u => u.Lastname)
               .HasColumnType("varchar(30)")
               .IsRequired();

        }
    }
}
