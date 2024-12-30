using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace All_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordGeneratorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GeneratePassword()
        {
            string password = GenerateRandomPassword();
            return Ok(new { Password = password });
        }

        private string GenerateRandomPassword()
        {
            // Define the character set for the password
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
            var random = new Random();

            // Generate an 11-character password
            var stringBuilder = new StringBuilder();

            for (int i = 0; i <8; i++)
            {
                stringBuilder.Append(chars[random.Next(chars.Length)]);
            }

            return stringBuilder.ToString();
        }
    }
}
