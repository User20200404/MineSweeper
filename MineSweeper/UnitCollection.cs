using MineSweeper.Exceptions;
using MineSweeper.Delegates;
namespace MineSweeper
{
    public class UnitCollection
    {
        private Unit[][] units;
        public Unit[][] UnitArray => units;
        public int ColumnCount { get; set; } = 10;
        public int RowCount { get; set; } = 10;
        public int MineCount { get; set; } = 5;
        /// <summary>
        /// 指示了某个单元格周围不存在雷时，是否自动揭示。
        /// </summary>
        public bool AutoRevealEmpty { get; set; } = true;
        public event UnitRevealedEventHandler UnitRevealed;
            
        /// <summary>
        /// 获取在特定行列的单元格。
        /// </summary>
        /// <param name="rowIndex">行索引。</param>
        /// <param name="columnIndex">列索引。</param>
        /// <returns></returns>
        public ref Unit this[int rowIndex, int columnIndex]
        {
            get => ref units[rowIndex][columnIndex];
        }

        /// <summary>
        /// 初始化单元格并布置地雷，已有单元格和地雷数据会被覆盖。
        /// </summary>
        public void Initialize()
        {
            InitUnits();
            InitMines();
        }

        /// <summary>
        /// 揭示在指定位置的单元格，如果单元格存在雷，则游戏结束。
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void RevealUnitAt(int rowIndex, int columnIndex)
        {
            SurroundingInfo units = this.GetSurroundingUnits(rowIndex, columnIndex);
            if (units.SourceUnit.Span[0].HasMine)
                throw new MineRevealedException();

            units.SourceUnit.Span[0].IsRevealed = true;
            units.SourceUnit.Span[0].SurroundingInfo = units;
            UnitRevealed?.Invoke(this, new UnitRevealedEventArgs(units.SourceUnit));

            if (units.SurroundingMineCount == 0 && AutoRevealEmpty)
                foreach (var unit in units.Where(u => !u.IsEmpty && !u.Span[0].IsRevealed))
                {
                    RevealUnitAt(unit.Span[0].RowIndex, unit.Span[0].ColumnIndex);
                }
        }

        public void RevealUnit(ref Unit unit)
        {
            RevealUnitAt(unit.RowIndex, unit.ColumnIndex);
        }

        /// <summary>
        /// 初始化单元格，已有单元格会被清除，在<see cref="InitMines"/>之前调用。
        /// </summary>
        private void InitUnits()
        {
            units = new Unit[RowCount][];
            for (int i = 0; i < RowCount; i++)
            {
                units[i] = new Unit[ColumnCount]; //Every unit is intialized with HasMine=false, IsProbed=false.
                for (int j = 0; j < ColumnCount; j++)
                {
                    units[i][j].ColumnIndex = j;
                    units[i][j].RowIndex = i;
                }
            }
        }
        /// <summary>
        /// 初始化地雷，在<see cref="InitUnits"/>之后调用。
        /// </summary>
        private void InitMines()
        {

            for (int i = 0; i < MineCount; i++)
            {
                Random random = new Random();
                int row = random.Next(0, RowCount);
                int column = random.Next(0, ColumnCount);
                ref Unit unit = ref this[row, column];
                if (!unit.HasMine)
                    unit.HasMine = true;
                else i--;
            }
        }
    }
}
