using Microsoft.Extensions.Configuration.UserSecrets;

namespace OthelloProject.Models.Methods
{
	public class UserDetails
	{
		public UserDetails() { }
		int UserID { get; set; }
		string Username { get; set; }
		string Email { get; set; }
		string Password { get; set; }
	}
}