using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Models;

namespace TMAS.DB.Context
{
    public class HistoriesConfig
    {
        public DbSet<History> Histories { get; set; }
        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<History>()
               .HasOne(p => p.Board)
               .WithMany(b => b.Histories)
               .HasForeignKey(b => b.BoardId);
        }
    }
}
