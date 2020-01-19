using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TurniejTenisowy.Models;

namespace TurniejTenisowy.Controllers
{
    public class ZarejestrujController : Controller
    {
        //connection String 
        DataBase db = new DataBase();

        [HttpGet]
        public IActionResult Index(){  //list
            ViewBag.Treners = db.getZawodnikList();

            return View();
        }

        [HttpPost]
        public IActionResult Index(Zawodnik z,string Rola) //dodane : sprawdzenie
        {
            if (!ModelState.IsValid)
                return NotFound();

            ViewBag.Treners = db.getZawodnikList();

            if (Rola == "Zawodnik")  //dodajemy zawodnika
            {
                List<Zawodnik> list = db.getZawodnikList();

                foreach (Zawodnik zz in list)
                    if (zz.Imie == z.Imie && zz.Nazwisko == z.Nazwisko)
                    {
                        ViewBag.Error = "Podany Zawodnik juz istnieje";
                        return View(z);
                    }
                        

                if (!db.insertZawodnik(z))
                    Console.WriteLine("Sql Exception");
            }
            else if(Rola == "Sadz") //dodajemy sedziego
            {
                List<Sedzia> sedzi = db.getSedziaList();

                foreach(Sedzia g in sedzi)
                    if(g.Imie == z.Imie && g.Nazwisko == z.Nazwisko)
                    {
                        ViewBag.Error = "Podany Sedzia juz istnieje";
                        return View(z);
                    }

                if (!db.insertSedzia(z))
                    Console.WriteLine("Sql Exception");
            }
            else                //dodajemy goscia
            {
                List<Zawodnik> gosci = db.getGosciList();

                foreach(Zawodnik g in gosci)
                    if(g.Imie == z.Imie && g.Nazwisko == z.Nazwisko)
                    {
                        ViewBag.Error = "Podany gosc juz istnieje";
                        return View(z);
                    }

                if (!db.insertGosc(z))
                    Console.WriteLine("Sql Exception");
            }

            return RedirectToAction("Komunikat");
        }

        public IActionResult Komunikat() //komunikat
        {
            return View();
        }
    }
}