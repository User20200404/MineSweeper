using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Exceptions
{
    public class MineSweeperException : Exception
    {
        public MineSweeperException() : base() { }
        public MineSweeperException(string message) : base(message)
        {

        }
        public MineSweeperException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
    public class GameOverException : MineSweeperException
    {
        public GameOverException(string message) : base(message)
        {

        }
        public GameOverException(string message, Exception innerException) : base(message, innerException)
        {

        }
        public GameOverException() : base("游戏结束！") { }
    }

    public class MineRevealedException : MineSweeperException
    {
        public MineRevealedException(string message) : base(message)
        {

        }
        public MineRevealedException(string message, Exception innerException) : base(message, innerException)
        {

        }
        public MineRevealedException() : base("踩到地雷了") { }
    }
}
