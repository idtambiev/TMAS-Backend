using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Models;

namespace TMAS.DB.Context
{
    public class UsersConfig
    {
        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .Property(u => u.Name)
               .HasColumnType("varchar(30)")
               .IsRequired();

            modelBuilder.Entity<User>()
               .Property(u => u.Lastname)
               .HasColumnType("varchar(30)")
               .IsRequired();

            modelBuilder.Entity<User>()
               .Property(u => u.Photo)
               .HasColumnType("varchar(100)")
               .IsRequired(false);
        }
     }
}
