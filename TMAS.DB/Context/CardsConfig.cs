using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Models;

namespace TMAS.DB.Context
{
    public class CardsConfig
    {
        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        }
    }
}
