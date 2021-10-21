using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Models;

namespace TMAS.DB.Context
{
    public class FilesConfig
    {
        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<File>()
                .HasOne(p => p.Card)
                .WithMany(b => b.Files)
                .HasForeignKey(b => b.CardId);

            modelBuilder.Entity<File>()
               .Property(u => u.FileType)
               .HasColumnType("varchar(30)")
               .IsRequired();

            modelBuilder.Entity<File>()
               .Property(u => u.Path)
               .HasColumnType("varchar(100)")
               .IsRequired();

            modelBuilder.Entity<File>()
               .Property(u => u.Name)
               .HasColumnType("varchar(100)")
               .IsRequired();
        }
    }
}
