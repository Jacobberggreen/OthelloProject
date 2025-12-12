using Microsoft.Data.SqlClient;
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

		public void flipIfValid(int[,] board, int row, int col, int dirRow, int dirCol, int player)
		{
			int nextRowInDir = row + dirRow;
			int nextColInDir = col + dirCol;

			if (!isInsideBoard(nextRowInDir, nextColInDir) || board[nextRowInDir, nextColInDir] == player)
			{
				return;
			}

			while (isInsideBoard(nextRowInDir, nextColInDir))
			{
				if(board[nextRowInDir,nextColInDir] == 0)
				{
					return;
				}

				if(board[nextRowInDir, nextColInDir] == player)
				{
					int flipRow = row + dirRow;
					int flipCol = col + dirCol;

					while (board[flipRow, flipCol] != player)
					{
						board[flipRow, flipCol] = player;
						flipRow += dirRow;
						flipCol += dirCol;
					}

					
					return;
				}

				nextRowInDir += dirRow;
				nextColInDir += dirCol;
			}

			return;
		}

		public bool BoardState(int row, int col, int player, int[,] board)
		{
			if(!isInsideBoard(row, col) || board[row, col] != 0)
			{
				return false;
			}

			for(int dirRow = -1; dirRow <= 1; dirRow++)
			{
				for(int dirCol = -1; dirCol <= 1; dirCol++)
				{
					if(dirRow == 0 && dirCol == 0) // detta är ingen riktning
					{
						continue;
					}

					flipIfValid(board, row, col, dirRow, dirCol, player);

					if (dirRow == 1 && dirCol == 1)
					{
						return true;
					}
				}
			}

			return false;
		}

	}
}