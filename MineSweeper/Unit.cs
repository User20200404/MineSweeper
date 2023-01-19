using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    public struct Unit
    {
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public bool IsRevealed { get; set; }
        public bool HasMine { get; set; }
        public bool IsProbed { get; set; }

        /// <summary>
        /// 单元格周围的至多8个单元格的信息，该属性在单元格被揭露前应为null。
        /// </summary>
        public SurroundingInfo SurroundingInfo { get; set; }

        /// <summary>
        /// 单元格周围的至多8个单元格的地雷数，该值在单元格被揭露前为null。
        /// </summary>
        public byte? SurroundingMineCount => SurroundingInfo.SurroundingMineCount;
        public void Reveal() => IsRevealed = true;
        public Unit(int rowIndex, int columnIndex, bool hasMine = false, bool isProbed = false, bool isRevealed = false)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            IsRevealed = isRevealed;
            HasMine = hasMine;
            IsProbed = isProbed;
            SurroundingInfo = null;
        }
    }
}
