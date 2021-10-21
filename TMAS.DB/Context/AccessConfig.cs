using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Models;

namespace TMAS.DB.Context
{
    public class AccessConfig
    {
        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardsAccess>()
                .HasOne(p => p.User)
                .WithMany(b => b.BoardsAccesses)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BoardsAccess>()
                 .HasOne(p => p.Board)
                 .WithMany(b => b.BoardsAccesses)
                 .HasForeignKey(b => b.BoardId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
