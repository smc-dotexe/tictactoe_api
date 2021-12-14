using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.App.Repositories.Interfaces;
using TicTacToe.Models.Entities;

namespace TicTacToe.App.Repositories
{
    public class PlayerRepository : BaseRepository<Player, Guid, ApplicationDbContext>, IPlayerRepository
    {
        public PlayerRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
