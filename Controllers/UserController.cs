using Microsoft.AspNetCore.Mvc;
using OthelloProject.Models.Methods;
using OthelloProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;


namespace OthelloProject.Controllers
{
	public class UserController : Controller
	{
		[AllowAnonymous]
		[HttpGet]
		public IActionResult RegisterUser()
		{
			return View("Register");
		}

		[AllowAnonymous]
		[HttpPost]
		public IActionResult RegisterUser(UserDetails userDetail)
		{

			UserMethods userMethods = new UserMethods();

			int i = userMethods.InsertUser(userDetail, out string message);

			switch (i)
			{
				case -2:
					ViewBag.Message = "Email is already in use!";
					break;
				case -1:
					ViewBag.Message = "User already exists.";
					break;
				case 1:
					ViewBag.Message = "Registrition succesful .";
					break;
				default:
					ViewBag.Message = "Unexpected error.";
					break;
			}

			return View("Register");
		}

		[AllowAnonymous]
		[HttpGet]
		public IActionResult Login()
		{
			return View("LoginPage");
		}

		[AllowAnonymous]
		[HttpPost]
		public IActionResult Login(UserDetails ud)
		{
			var uMethod = new UserMethods();
			UserDetails retrievedUser = uMethod.VerifyLogin(ud.Username!, out string errormsg);

			if (retrievedUser != null)
			{
				var passwordHasher = new PasswordHasher<UserDetails>();
				var verificationResult = passwordHasher.VerifyHashedPassword(retrievedUser, retrievedUser.Password, ud.Password);

				if (verificationResult == PasswordVerificationResult.Success)
				{
					// Inloggning lyckades
					HttpContext.Session.SetInt32("UserID", retrievedUser.UserID);
					return RedirectToAction("Games", "Games");
				}
				else
				{
					// Fel lösenord
					ViewBag.ErrorMessage = "Felaktigt användarnamn eller lösenord.";
					return View("LoginPage");
				}
			}
			else
			{
				// Användaren hittades inte
				ViewBag.ErrorMessage = "Användaren hittades inte.";
				return View("LoginPage");
			}
		}

		[HttpGet]
		public IActionResult UserPage()
		{
			int? userId = HttpContext.Session.GetInt32("UserID");
			if (userId == null)
			{
				return RedirectToAction("Login");
			}

			var um = new UserMethods();
			var gm = new GameMethods();
			var user = um.GetUserInfoByID(userId, out string userMsg);
			if (user == null)
			{
				ViewBag.Error = userMsg;
				return View("UserPage");
			}

			int totalGames, gamesWon;
				int result = um.GetWinningStats(userId.Value, out totalGames, out gamesWon, out string statsMsg);
			int gamesLost = totalGames - gamesWon;

			if (result != 1)
			{
				ViewBag.Error = statsMsg;
			}
 
			ViewBag.Username = user.Username;
			ViewBag.TotalGames = totalGames;
			ViewBag.GamesWon = gamesWon;
			ViewBag.GamesLost = gamesLost;
			ViewBag.RecentGames = gm.GetRecentGamesForUser(userId.Value, 5, out string recentMsg);
			if (!string.IsNullOrEmpty(recentMsg))
			{
				ViewBag.Error = recentMsg;
			}

			return View("UserPage");
		}
	}
}
