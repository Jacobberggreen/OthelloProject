using System;
using System.Text;

namespace OthelloProject.Models
{
	public class ConverterMethods
	{

		public ConverterMethods (){}
		public int[,] ConvertBoardStringToArray(string board)
		{
			// Expecting 64 chars: E (empty), B (black), W (white)
			var normalized = (board ?? string.Empty).Trim();
			if (normalized.Length != 64)
			{
				normalized = "EEEEEEEEEEEEEEEEEEEEEEEEEEEBWEEEEEEWBEEEEEEEEEEEEEEEEEEEEEEEEEEE";
			}

			int[,] result = new int[8, 8];
			for (int i = 0; i < 64; i++)
			{
				int r = i / 8;
				int c = i % 8;
				result[r, c] = normalized[i] switch
				{
					'B' or 'b' => 1,
					'W' or 'w' => 2,
					_ => 0
				};
			}

			return result;
		}

		public string ConvertBoardArrayToString(int[,] board)
		{
			var newBoard = new StringBuilder(64);
			for (int r = 0; r < 8; r++)
			{
				for (int c = 0; c < 8; c++)
				{
					char cell = board[r, c] switch
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