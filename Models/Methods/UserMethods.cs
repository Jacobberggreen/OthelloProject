using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace OthelloProject.Models
{
	public class UserMethods
	{

		public UserMethods() { }

		public IConfigurationRoot GetConnection() // Metod för att hämta koppling till databasen
		{
			var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
			return builder;
		}

		public SqlConnection Connect()
		{
			SqlConnection dbConnection = new SqlConnection(GetConnection().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
			return dbConnection;
		}
	}

}