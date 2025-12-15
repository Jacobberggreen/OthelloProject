using Microsoft.AspNetCore.Mvc;
using OthelloProject.Models;
using OthelloProject.Models.Methods;
using System.Diagnostics;
using Microsoft.IdentityModel.Tokens;

namespace OthelloProject
{
	public class BoardController : Controller
	{
		public BoardController(){}

		public IActionResult makeMove(int [,] cell)
		{
			GameMethods gm = new GameMethods();
			GameDetails gd = new GameDetails();
			OthelloLogic gameLogic = new OthelloLogic();

			string currentGame = HttpContext.Session.GetString("GameName") ?? "";
			gd = gm.GetGameByName(currentGame, out string message1);

			string currentBoard = gm.GetBoard(gd, out string message2);

			Console.WriteLine(cell);
			Console.WriteLine("hej");
			return RedirectToAction("OthelloBoard");
		}
	}
}