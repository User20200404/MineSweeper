using System.Text;

namespace MineSweeper
{
    public class GlobalResources
    {
        public static UnitCollection UnitCollection { get; private set; } = new UnitCollection();
        public static int BoardRowCount { get; set; } = 10000;
        public static int BoardColumnCount { get; set; } = 40;
        public static int MineCount { get; set; } = 5;
        public static bool AutoRevealEmpty { get; set; } = true;   
        public static bool IsInitialized { get; private set; } = false;

        public static void Initialize()
        {
            CheckValues();
            UnitCollection.ColumnCount = BoardColumnCount;
            UnitCollection.RowCount = BoardRowCount;
            UnitCollection.MineCount = MineCount;
            UnitCollection.AutoRevealEmpty = AutoRevealEmpty;
            UnitCollection.Initialize();
        }

        private static void CheckValues()
        {
            if (BoardRowCount <= 0 || BoardColumnCount <= 0 || MineCount <= 0)
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 获取以字符串形式表达的扫雷棋盘，此方法通常用于控制台输出棋盘形状。
        /// </summary>
        /// <param name="backgroundChar">没有雷的单元格字符。</param>
        /// <param name="mineChar">有雷的单元格字符。</param>
        /// <returns>已格式化的棋盘形状字符串，可直接用于控制台输出。</returns>
        public static string GetMineBoardShapeString(char backgroundChar = '□', char mineChar = '■')
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < BoardRowCount; i++)
            {
                for (int j = 0; j < BoardColumnCount; j++)
                {
                    if (UnitCollection[i, j].HasMine)
                        builder.Append(mineChar);
                    else builder.Append(backgroundChar);
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }
    }
}