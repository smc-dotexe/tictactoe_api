using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.App.Exceptions
{
    public class NotPlayersTurnException : Exception
    {
        public NotPlayersTurnException() { }
        public NotPlayersTurnException(string message) : base(message) { }
    }
}
