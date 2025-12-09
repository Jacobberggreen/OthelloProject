using Microsoft.AspNetCore.Mvc;
using OthelloProject.Models.Methods;
using OthelloProject.Models;


namespace OthelloProject.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View("Register");
        }

        [HttpPost]
        public IActionResult RegisterUser(UserDetails userDetail){
            
            UserMethods userMethods = new UserMethods();

            int i = userMethods.InsertUser(userDetail, out string message);

            switch(i)
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

        [HttpGet]
        public IActionResult Login(){

            return View("LoginPage");  
        }

        [HttpPost]
        public IActionResult Login(string username, string password){
            UserMethods userMethods = new UserMethods();
            var userList = userMethods.GetAllUsers();

            var user = new UserDetails()
            {
                Username = username,
                Password = password
            };
            
           var loginUser = userList.FirstOrDefault(u => u.Username.Equals(user.Username, StringComparison.OrdinalIgnoreCase)
           && u.Password == user.Password);

            if (loginUser != null)
            {

                HttpContext.Session.SetString("UserName", loginUser.Name);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Invalid email or password.";
                return View("LoginPage", user);
            }


        }
    }
}
