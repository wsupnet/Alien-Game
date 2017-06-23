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

            CharactersViewModel model = new CharactersViewModel();
            model.hero = new HeroModel();

            return View();
        }

        [HttpPost]
        public IActionResult BeginGame(CharactersViewModel cmodel)
        {

            return RedirectToAction("Characters", new { enemy = 1, heroName = cmodel.hero.heroName });
        }

        public IActionResult Characters(int enemy, string heroName)
        {

            //Calls the method that populates our model
            ChangeEnemy(enemy, heroName);

            return View(model);
        }


        private CharactersViewModel ChangeEnemy(int enemy, string heroName)
        {

            if (enemy <= 0)
                enemy = 1;

            model.alien = new AlienModel();
            model.weapon = new WeaponModel();
            model.hero = new HeroModel();

            model.hero.heroName = heroName;

            switch (enemy)
            {
                case 1:
                    model.alien.alienName = "Slimey";
                    model.alien.strenght = new string[1] { "Ice Wand" };
                    model.alien.weakness = new string[2] { "Torch", "Hammer" };
                    model.alien.healthPoints = 1000;
                    model.alien.message = "You killed Slimey";
                    model.alien.alienStory = "One day you are at your desk working on some code.  You decide to take a break to get a quick snack. As you walk into the kitchen the light go out! Then you hear a slimey sound coming from the refrigerator...";
                    model.weapon.weapons = new string[3, 2] { { "Torch", "1000" }, { "Ice Wand", "100" }, { "Hammer", "500" } };
                    model.drawAlien = "https://upload.wikimedia.org/wikipedia/en/1/13/Slime_%28Dragon_Quest%29.jpg";
                    break;
                case 2:
                    model.alien.alienName = "Anna";
                    model.alien.strenght = new string[1] { "Whip Cream (on it)" };
                    model.alien.weakness = new string[2] { "Spoon (her)", "Fork (it)" };
                    model.alien.healthPoints = 1000;
                    model.alien.message = "You killed Anna";
                    model.alien.alienStory = "After you took care of the slime, you need something sweet to bring your blood sugar up. You open the freezer and a mutated banana split flies out!";
                    model.weapon.weapons = new string[3, 2] { { "Spoon (her)", "1000" }, { "Whip Cream (on it)", "100" }, { "Fork (it)", "500" } };
                    model.drawAlien = "https://cdn.weasyl.com/static/media/31/35/08/313508507aaf7f5d571b3995a4e9ba55113ae214b9814a76ad91ce0b83739e68.png";
                    break;
                case 3:
                    model.alien.alienName = "Gaga";
                    model.alien.strenght = new string[1] { "Vodka (martini)" };
                    model.alien.weakness = new string[2] { "Toothpick (poker face)", "Mouth (smash)" };
                    model.alien.healthPoints = 1000;
                    model.alien.message = "You killed Gaga";
                    model.weapon.weapons = new string[3, 2] { { "Toothpick (poker face)", "1000" }, { "Vodka (martini)", "100" }, { "Mouth (smash)", "500" } };
                    model.drawAlien = "https://vignette4.wikia.nocookie.net/finalfantasy/images/2/2e/Ffcc-mlaad_monster_ahriman.png/revision/latest?cb=20091012033316";
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
                    case "Slimey":
                        enemy = 2;
                        break;
                    case "Anna":
                        enemy = 3;
                        break;
                    case "Gaga":
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
                model.gameStatus = "You have died! Game over!";
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
