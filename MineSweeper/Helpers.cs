using System.Text;
using System.Collections;

namespace MineSweeper
{
    public class SurroundingInfo : IEnumerable<Memory<Unit>>
    {
        public Memory<Unit> SourceUnit { get; init; }
        public Memory<Unit> LeftUnit { get; init; }
        public Memory<Unit> TopUnit { get; init; }
        public Memory<Unit> RightUnit { get; init; }
        public Memory<Unit> BottomUnit { get; init; }
        public Memory<Unit> TopLeftUnit { get; init; }
        public Memory<Unit> TopRightUnit { get; init; }
        public Memory<Unit> BottomLeftUnit { get; init; }
        public Memory<Unit> BottomRightUnit { get; init; }

        public byte SurroundingMineCount => (byte)this.Where(u => !u.IsEmpty && u.Span[0].HasMine).Count();

        public IEnumerator<Memory<Unit>> GetEnumerator()
        {
            return new IEnumerator(this);
        }

        System.Collections.IEnumerator IEnumerable.GetEnumerator()
        {
            return new IEnumerator(this);
        }

        public class IEnumerator : IEnumerator<Memory<Unit>>
        {
            private bool disposedValue;
            private sbyte position = -1;
            private SurroundingInfo units;

            public Memory<Unit> Current => GetValue();

            object System.Collections.IEnumerator.Current => GetValue();

            public bool MoveNext()
            {
                position++;
                return position < 8;
            }

            public void Reset()
            {
                position = -1;
            }

            public IEnumerator(SurroundingInfo units)
            {
                this.units = units;
            }
            private Memory<Unit> GetValue()
            {
                return position switch
                {
                    0 => units.LeftUnit,
                    1 => units.TopLeftUnit,
                    2 => units.TopUnit,
                    3 => units.TopRightUnit,
                    4 => units.RightUnit,
                    5 => units.BottomRightUnit,
                    6 => units.BottomUnit,
                    7 => units.BottomLeftUnit,
                    _ => throw new NullReferenceException(),
                };
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: 释放托管状态(托管对象)
                    }

                    // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                    // TODO: 将大型字段设置为 null
                    disposedValue = true;
                }
            }

            // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
            // ~IEnumerator()
            // {
            //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            //     Dispose(disposing: false);
            // }

            public void Dispose()
            {
                // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }
    }
    public static class Helpers
    {
        private static void CheckValues(this UnitCollection collection, int rowIndex, int columnIndex)
        {
            bool rowIndexOutOfRange = false;
            bool columnIndexOutOfRange = false;
            if (rowIndex < 0 || rowIndex >= collection.RowCount)
                rowIndexOutOfRange = true;
            if (columnIndex < 0 || columnIndex >= collection.ColumnCount)
                columnIndexOutOfRange = true;

            StringBuilder msgBuilder = new StringBuilder();
            if (rowIndexOutOfRange)
                msgBuilder.AppendLine(string.Format("行索引超出界限：({0}/{1})", rowIndex, collection.RowCount - 1));
            if (columnIndexOutOfRange)
                msgBuilder.AppendLine(string.Format("列索引超出界限：({0}/{1})", columnIndex, collection.ColumnCount - 1));
            if (rowIndexOutOfRange || columnIndexOutOfRange)
                throw new IndexOutOfRangeException(msgBuilder.ToString());
        }

        public static Memory<Unit> GetTopLeftUnitOf(this UnitCollection collection, int rowIndex, int columnIndex)
        {
            CheckValues(collection, rowIndex, columnIndex);
            if (rowIndex == 0 || columnIndex == 0)
                return Memory<Unit>.Empty;
            return new Memory<Unit>(collection.UnitArray[rowIndex - 1], columnIndex - 1, 1);
        }
        public static Memory<Unit> GetTopRightUnitOf(this UnitCollection collection, int rowIndex, int columnIndex)
        {
            CheckValues(collection, rowIndex, columnIndex);
            if (rowIndex == 0 || columnIndex == collection.ColumnCount - 1)
                return Memory<Unit>.Empty;
            return new Memory<Unit>(collection.UnitArray[rowIndex - 1], columnIndex + 1, 1);
        }
        public static Memory<Unit> GetBottomLeftUnitOf(this UnitCollection collection, int rowIndex, int columnIndex)
        {
            CheckValues(collection, rowIndex, columnIndex);
            if (rowIndex == collection.RowCount - 1 || columnIndex == 0)
                return Memory<Unit>.Empty;
            return new Memory<Unit>(collection.UnitArray[rowIndex + 1], columnIndex - 1, 1);
        }
        public static Memory<Unit> GetBottomRightUnitOf(this UnitCollection collection, int rowIndex, int columnIndex)
        {
            CheckValues(collection, rowIndex, columnIndex);
            if (rowIndex == collection.RowCount - 1 || columnIndex == collection.ColumnCount - 1)
                return Memory<Unit>.Empty;
            return new Memory<Unit>(collection.UnitArray[rowIndex + 1], columnIndex + 1, 1);
        }

        public static Memory<Unit> GetTopUnitOf(this UnitCollection collection, int rowIndex, int columnIndex)
        {
            CheckValues(collection, rowIndex, columnIndex);
            if (rowIndex == 0)
                return Memory<Unit>.Empty;
            return new Memory<Unit>(collection.UnitArray[rowIndex - 1], columnIndex, 1);
        }
        public static Memory<Unit> GetLeftUnitOf(this UnitCollection collection, int rowIndex, int columnIndex)
        {
            CheckValues(collection, rowIndex, columnIndex);
            if (columnIndex == 0)
                return Memory<Unit>.Empty;
            return new Memory<Unit>(collection.UnitArray[rowIndex], columnIndex - 1, 1);
        }
        public static Memory<Unit> GetRightUnitOf(this UnitCollection collection, int rowIndex, int columnIndex)
        {
            CheckValues(collection, rowIndex, columnIndex);
            if (columnIndex == collection.ColumnCount - 1)
                return Memory<Unit>.Empty;
            return new Memory<Unit>(collection.UnitArray[rowIndex], columnIndex + 1, 1);
        }
        public static Memory<Unit> GetBottomUnitOf(this UnitCollection collection, int rowIndex, int columnIndex)
        {
            CheckValues(collection, rowIndex, columnIndex);
            if (rowIndex == collection.RowCount - 1)
                return Memory<Unit>.Empty;
            return new Memory<Unit>(collection.UnitArray[rowIndex + 1], columnIndex, 1);
        }

        public static SurroundingInfo GetSurroundingUnits(this UnitCollection collection, int rowIndex, int columnIndex)
        {
            SurroundingInfo units = new SurroundingInfo()
            {
                LeftUnit = GetLeftUnitOf(collection, rowIndex, columnIndex),
                RightUnit = GetRightUnitOf(collection, rowIndex, columnIndex),
                TopUnit = GetTopUnitOf(collection, rowIndex, columnIndex),
                BottomUnit = GetBottomUnitOf(collection, rowIndex, columnIndex),
                TopLeftUnit = GetTopLeftUnitOf(collection, rowIndex, columnIndex),
                TopRightUnit = GetTopRightUnitOf(collection, rowIndex, columnIndex),
                BottomLeftUnit = GetBottomLeftUnitOf(collection, rowIndex, columnIndex),
                BottomRightUnit = GetBottomRightUnitOf(collection, rowIndex, columnIndex),
                SourceUnit = new Memory<Unit>(collection.UnitArray[rowIndex], columnIndex, 1)
            };
            return units;
        }
    }
}
