using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tombola.Models;
using System.Net;

namespace Tombola.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ResetGame()
        {
            string cookie_key = "numbers";

            CookieOptions option = new CookieOptions();

            option.Expires = DateTime.Now.AddMilliseconds(1000000);

            Response.Cookies.Append(cookie_key, "", option);

            return View("Index");

        }

        public string NextValues()
        {
            string[] smorfia = {"L'Italia", "'A piccerella", "'A jatta", "'O puorco", "'A mano", "Chella ca guarda 'nterra", "'O vasetto", "'A Maronna", "'A figliata", "'E fasule", "'E suricille", "'O surdato", "Sant'Antonio", "'O mbriaco", "'O guaglione", "'O culo", "'A disgrazia", "'O ssango", "'A resata", "'A festa", "'A femmena annura", "'O pazzo", "'O scemo", "'E Gguardie", "Natale", "Nanninella", "'O càntaro", "'E zzizze", "'O pate d' 'e ccriature", "'E ppalle d' 'o tenente", "'O patrone 'e casa", "'O capitone", "Ll'anne 'e Cristo", "'A capa", "Ll'aucielluzzo", "'E ccastagnelle", "'O monaco", "'E mmazzate", "'A fune 'n ganna", "'A noja", "'O curtiello", "'O ccafè", "Onna pereta fore ô barcone", "'E ccancelle", "'O vino bbuono", "'E denare", "'O muorto", "'O muorto che pparla", "'O piezz' 'e carne", "'O ppane", "'O ciardino", "'A mamma", "'O viecchio", "'O cappiello", "'A museca", "'A caruta", "'O scartellato", "'O paccotto", "'E pile", "'O lamiento", "'O cacciatore", "'O muorto acciso", "'A sposa", "'A sciammerìa", "'O chianto", "'E ddoje zetelle", "'O totaro dint' 'a chitarra", "'A zuppa cotta", "sott' e 'ncoppa", "'O palazzo", "L'omm 'e mmerda", "'A maraviglia", "'O spitale", "'A grotta", "Pulecenella", "'A funtana", "'E riavulille", "'A bbella figliola", "'O mariuolo", "'A vocca", "'E sciure", "'A tavula 'mbandita", "'O maletiempo", "'A chiesa", "Ll'aneme d' 'o priatorio", "'A puteca", "'E perucchie", "'E casecavalle", "'A vecchia", "'A paura" };
            string cookie_key = "numbers";
            string cookieValueFromReq = WebUtility.UrlDecode(Request.Cookies[cookie_key]);
            
            List<int> numbers = new List<int>();

            if (cookieValueFromReq != null && cookieValueFromReq != "")
            {
                string[] s_numbers = cookieValueFromReq.Split("-");

                for (int i = 0; i < s_numbers.Length; i++)
                    numbers.Add(Int32.Parse(s_numbers[i]));
            }
            else cookieValueFromReq = "";

            var rand = new Random();

            int number;

            if (numbers.Count == 90) return "0";

            while (numbers.Contains(number = rand.Next(1, 91))) ;

            numbers.Add(number);

            cookieValueFromReq = cookieValueFromReq != "" ? String.Format("{0}-{1}", cookieValueFromReq, number) : String.Format("{0}", number);
            CookieOptions option = new CookieOptions();

            option.Expires = DateTime.Now.AddMilliseconds(1000000);

            Response.Cookies.Append(cookie_key, cookieValueFromReq, option);

            return number + ";" + smorfia[number-1];
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
