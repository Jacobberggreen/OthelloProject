using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using OthelloProject.Models.Methods;

namespace OthelloProject.Models
{
	public class GameMethods
	{
		public GameMethods() { }

		public IConfigurationRoot GetConnection() // Metod för att hämta koppling till databasen
		{
			var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
			return builder;
		}

		public SqlConnection Connect() // Hjälpfuntion
		{
			SqlConnection dbConnection = new SqlConnection(GetConnection().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
			return dbConnection;
		}

		public int InsertGame(GameDetails gd, out string message)
		{
			message = "";

			SqlConnection conn = Connect();

			string sqlQuery = "INSERT INTO [Game] (User1ID, GameStatus, Board) VALUES (@User1ID, @GameStatus, @Board)";
			SqlCommand cmd = new SqlCommand(sqlQuery, conn);

			cmd.Parameters.AddWithValue("@User1ID", gd.User1ID);
			cmd.Parameters.AddWithValue("@GameStatus",gd.GameStatus.ToString());
			cmd.Parameters.AddWithValue("@Board", gd.Board.ToString());

			try
			{
				conn.Open();
				int rowsAffected = cmd.ExecuteNonQuery();
				message = "Successfully created a new game";
				if(rowsAffected != 1)
				{
					message = "An error occurred when creating a game";
				}
				return rowsAffected;
			}
			catch(SqlException ex)
			{
				message = ex.Message;
				return 0;
			}
			finally
			{
				conn.Close();
			}
		}

		public GameDetails GetGameInfoByID(int selectedGameID, out string message)
		{
			message = "";

			SqlConnection conn = Connect();

			string sqlQuery = "SELECT * FROM [Game] WHERE [GameID] = @GameID";
			SqlCommand cmd = new SqlCommand(sqlQuery, conn);

			cmd.Parameters.AddWithValue("@GameID", selectedGameID);
			GameDetails gd = new GameDetails();

			try
			{
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					gd.GameID = (int)reader["GameID"];
					gd.User1ID = (int)reader["User1ID"];
					gd.User2ID = (int)reader["User2ID"];
					gd.GameStatus = reader["GameStatus"].ToString();
					gd.Board = reader["Board"].ToString();
					gd.WinnerID = (int)reader["WinnderID"];
				}
				return gd;
			}
			catch(SqlException ex)
			{
				message = ex.Message;
				return null;
			}
			finally
			{
				conn.Close();
			}
		}

	}
}