using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.App.Exceptions
{
    public class SpotTakenException : Exception
    {
        public SpotTakenException() { }
        public SpotTakenException(string message) : base(message) { }
    }
}
