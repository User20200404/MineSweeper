using MineSweeper;
using System.Runtime.InteropServices;
using System.Text;

char unitChar = '?';

GlobalResources.BoardColumnCount = 15;
GlobalResources.BoardRowCount = 15;
GlobalResources.MineCount = 20;
GlobalResources.Initialize();
char[][] unitChars = new char[15][];

GlobalResources.UnitCollection.UnitRevealed += UnitCollection_UnitRevealed;

for (int i = 0; i < unitChars.Length; i++)
{
    unitChars[i] = Enumerable.Repeat(unitChar, 15).ToArray();
}

while (true)
{
    int row = int.Parse(Console.ReadLine());
    int column = int.Parse(Console.ReadLine());
    GlobalResources.UnitCollection.RevealUnitAt(row, column);
    PrintBoard();
}




void UnitCollection_UnitRevealed(UnitCollection sender, MineSweeper.Delegates.UnitRevealedEventArgs args)
{
    ref Unit unit = ref args.RefUnit;
    unitChars[unit.RowIndex][unit.ColumnIndex] = unit.SurroundingMineCount.ToString()[0];
}

void PrintBoard()
{
    StringBuilder builder = new StringBuilder();
    for (int i = 0;i<15;i++)
    {
        for(int j = 0;j<15;j++)
        {
            builder.Append(unitChars[i][j]);    
            builder.Append(' ');
        }
        builder.Append('\n');
    }
    Console.WriteLine(builder.ToString());
}