using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Models;

namespace TMAS.DB.Context
{
    public class ColumnsConfig
    {
        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        }
    }
}
