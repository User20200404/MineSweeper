using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Delegates
{
    public class UnitRevealedEventArgs : EventArgs
    {
        private Memory<Unit> unit;
        public ref Unit RefUnit => ref unit.Span[0];
        public Memory<Unit> Unit => unit;
        public UnitRevealedEventArgs(Memory<Unit> unit)
        {
            this.unit = unit;
        }
    }
    public delegate void UnitRevealedEventHandler(UnitCollection sender, UnitRevealedEventArgs args);
}
