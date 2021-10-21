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
        public DbSet<File> Files { get; set; }
        public DbSet<BoardsAccess> BoardsAccesses{get;set;}

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
          // Database.EnsureDeleted();
           //Database.EnsureCreated();
           //Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AccessConfig.OnModelCreating(modelBuilder);
            BoardsConfig.OnModelCreating(modelBuilder);
            CardsConfig.OnModelCreating(modelBuilder);
            ColumnsConfig.OnModelCreating(modelBuilder);
            FilesConfig.OnModelCreating(modelBuilder);
            HistoriesConfig.OnModelCreating(modelBuilder);
            UsersConfig.OnModelCreating(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
