using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models.Entities;

namespace TicTacToe.App.Repositories.Interfaces
{
    public interface IPlayerRepository : IBaseRepository<Player, Guid>
    {
    }
}
