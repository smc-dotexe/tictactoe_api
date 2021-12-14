using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.Models.Entities;

namespace TicTacToe.App
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<GamePlayer> GamePlayer { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<GamePlayer>()
                .HasKey(gp => new { gp.GameId, gp.PlayerId });
            builder.Entity<GamePlayer>()
                .HasOne(gp => gp.Game)
                .WithMany(g => g.GamePlayer)
                .HasForeignKey(gp => gp.GameId);
            builder.Entity<GamePlayer>()
                .HasOne(gp => gp.Player)
                .WithMany(p => p.GamePlayer)
                .HasForeignKey(gp => gp.PlayerId);
        }


    }
}
