using OthelloProject.Models.Methods;

namespace OthelloProject.Models
{
	public class OthelloLogic
	{
		public OthelloLogic() { }

		public bool isInsideBoard(int row, int col)
		{
			return row <= 7 && col <= 7 && row >= 0 && col >= 0;
		}

		public int flipIfValid(int[,] board, int row, int col, int dirRow, int dirCol, int player, GameDetails gd)
		{
			int nextRowInDir = row + dirRow;
			int nextColInDir = col + dirCol;

			if (!isInsideBoard(nextRowInDir, nextColInDir) || board[nextRowInDir, nextColInDir] == player)
			{
				return 0;
			}

			while (isInsideBoard(nextRowInDir, nextColInDir))
			{
				if (board[nextRowInDir, nextColInDir] == 0)
				{
					return 0;
				}

				if (board[nextRowInDir, nextColInDir] == player)
				{
					int flipRow = row + dirRow;
					int flipCol = col + dirCol;

					while (board[flipRow, flipCol] != player)
					{
						board[flipRow, flipCol] = player;
						flipRow += dirRow;
						flipCol += dirCol;
						Console.WriteLine("WALLAH");
					}

					string newBoard = new ConverterMethods().ConvertBoardArrayToString(board);
					gd.Board = newBoard;
					int success = new GameMethods().UpdateBoard(gd, out string message);

					if (success != 1)
					{
						return -1;

					}
					Console.WriteLine("VI KOM HIT");
					return 1;
				}

				nextRowInDir += dirRow;
				nextColInDir += dirCol;
			}

			return 0;
		}

		public bool BoardState(int row, int col, int player, int[,] board, GameDetails gd)
		{
			if (!isInsideBoard(row, col) || board[row, col] != 0)
			{
				return false;
			}

			int flipped = 0;
			for (int dirRow = -1; dirRow <= 1; dirRow++)
			{
				for (int dirCol = -1; dirCol <= 1; dirCol++)
				{
					if (dirRow == 0 && dirCol == 0) // detta är ingen riktning
					{
						continue;
					}

					flipped += flipIfValid(board, row, col, dirRow, dirCol, player, gd);

					if (flipped == -1)
					{
						return false;
					}

					if (dirRow == 1 && dirCol == 1 && flipped != 0)
					{
						string currentBoard = new GameMethods().GetBoard(gd, out string message);
						int[,] newBoard = new ConverterMethods().ConvertBoardStringToArray(currentBoard);
						newBoard[row, col] = player;
						string updatedBoard = new ConverterMethods().ConvertBoardArrayToString(newBoard);
						gd.Board = updatedBoard;
						int success = new GameMethods().UpdateBoard(gd, out string message2);
						
						return true;
					}

				}
			}

			return false;

		}
	}
}