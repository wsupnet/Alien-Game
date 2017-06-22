using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameWeb.Models;


namespace GameWeb.Controllers
{
    public class HomeController : Controller
    {
        //Creating a new instance of the model (GLOBAL)
        CharactersViewModel model = new CharactersViewModel();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Characters(int enemy)
        {

            //Calls the method that populates our model
            ChangeEnemy(enemy);

            return View(model);
        }

        private CharactersViewModel ChangeEnemy(int enemy)
        {

            if (enemy <= 0)
                enemy = 1;

            model.alien = new AlienModel();
            model.weapon = new WeaponModel();

            switch (enemy)
            {
                case 1:
                    model.alien.alienName = "Patrick";
                    model.alien.strenght = new string[1] { "Freeze (make harder)" };
                    model.alien.weakness = new string[2] { "Torch (it)", "Candle (burn with prayers" };
                    model.alien.healthPoints = 1000;
                    model.alien.message = "You killed patrick";
                    model.weapon.weapons = new string[3, 2] { { "Torch (it)", "1000" }, { "Freeze (make harder)", "100" }, { "Candle (burn with prayers)", "500" } };
                    break;
                case 2:
                    model.alien.alienName = "Spongebob";
                    model.alien.strenght = new string[1] { "Splash It!" };
                    model.alien.weakness = new string[2] { "Torch (it)", "Candle (burn with prayers" };
                    model.alien.healthPoints = 1000;
                    model.alien.message = "You killed Spongebob";
                    model.weapon.weapons = new string[3, 2] { { "Torch (it)", "1000" }, { "Splash It!", "100" }, { "Candle (burn with prayers)", "500" } };
                    break;
                case 3:
                    model.alien.alienName = "Gary";
                    model.alien.strenght = new string[1] { "Feed It!" };
                    model.alien.weakness = new string[2] { "Torch (it)", "Candle (burn with prayers" };
                    model.alien.healthPoints = 1000;
                    model.alien.message = "You killed Gary";
                    model.weapon.weapons = new string[3, 2] { { "Torch (it)", "1000" }, { "Feed It!", "100" }, { "Candle (burn with prayers)", "500" } };
                    break;

                default:
                    break;
            }
            return model;

        }

        [HttpPost]
        public IActionResult FightEnemy(CharactersViewModel model)
        {

            /*
               Now I can capture the weapon value and the enemy.
               based on the name of the enemy, I can reduce it's health
               based on the points.
            */

            if (Convert.ToInt32(model.selectedWeapon) > 100)
            {
                int enemy = 1;
                model.gameStatus = "You are a hero!";
                switch (model.alien.alienName)
                {
                    case "Patrick":
                        enemy = 2;
                        break;
                    case "Spongebob":
                        enemy = 3;
                        break;
                    case "Gary":
                        enemy = 1;
                        break;
                    default:
                        break;
                }
                //You have to move on to the next enemy
                return RedirectToAction("Characters", new { enemy = enemy });
            }
            else
            {
                model.gameStatus = "You have died!";
            }

            return View(model);
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}
