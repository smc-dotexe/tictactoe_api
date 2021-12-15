using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.App.Exceptions
{
    public class GameWinException : Exception
    {
        public GameWinException() { }
        public GameWinException(string message) : base(message) { }
    }
}
