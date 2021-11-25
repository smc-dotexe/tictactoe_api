using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.App
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        {
        }

        //public DbSet<> Games { get; set; }
        //public DbSet<> Players { get; set; }

    }
}
