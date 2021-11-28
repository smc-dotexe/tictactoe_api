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



    }
}
