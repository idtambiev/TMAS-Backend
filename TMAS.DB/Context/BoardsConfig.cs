using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Models;

namespace TMAS.DB.Context
{
    public class BoardsConfig
    {
        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Board>()
                .Property(b => b.Title)
                .HasColumnType("varchar(100)")
                .IsRequired();

            modelBuilder.Entity<Board>()
               .HasOne(p => p.User)
               .WithMany(b => b.Boards)
               .HasForeignKey(b => b.BoardUserId)
               ;

            modelBuilder.Entity<Board>()
                .Property(b => b.CreatedDate)
                .IsRequired();

            modelBuilder.Entity<Board>()
                .Property(b => b.UpdatedDate)
                .IsRequired();

            modelBuilder.Entity<Board>()
                .Property(b => b.IsActive)
                .IsRequired();
        }
    }  
}
