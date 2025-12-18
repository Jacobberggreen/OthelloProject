using System;
using System.Text;

namespace OthelloProject.Models
{
	public class ConverterMethods
	{

		public ConverterMethods() { } // Tom konstruktor

		/*	Namn: ConvertBoardStringToArray
			Tar in det nuvarande brädet i form av en sträng och
			gör sedan om den till en 2D array som returneras.
		*/
		public int[,] ConvertBoardStringToArray(string board)
		{
			// Vi förväntar oss 64 karaktärer: E (tom), B (svar), W (vit)
			var normalized = (board ?? string.Empty).Trim(); // Trim tar bort onödiga mellanslag
			if (normalized.Length != 64) 
			{
				normalized = "EEEEEEEEEEEEEEEEEEEEEEEEEEEBWEEEEEEWBEEEEEEEEEEEEEEEEEEEEEEEEEEE";
			}

			int[,] result = new int[8, 8];
			for (int i = 0; i < 64; i++)
			{
				int r = i / 8;
				int c = i % 8;
				result[r, c] = normalized[i] switch // Byter ut alla B till 1, alla W till 2 och resten till 0
				{
					'B' or 'b' => 1,
					'W' or 'w' => 2,
					_ => 0
				};
			}

			return result;
		}

		/*	Namn: ConvertBoardArrayToString
			Tar in ett bräde i formatet 2D array och gör
			sedan om den till en sträng som returneras. 
		*/
		public string ConvertBoardArrayToString(int[,] board)
		{
			var newBoard = new StringBuilder(64); // Måste använda stringbuilder för att bygga en sträng
			for (int r = 0; r < 8; r++)
			{
				for (int c = 0; c < 8; c++)
				{
					char cell = board[r, c] switch // Byter ut alla 1 till B, 2 till W och 0 till E
					{
						1 => 'B',
						2 => 'W',
						0 => 'E'
					};

					newBoard.Append(cell);
				}
			}

			return newBoard.ToString();
		}
	}
}