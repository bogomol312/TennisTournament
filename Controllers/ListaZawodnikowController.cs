using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TurniejTenisowy.Models;

namespace TurniejTenisowy.Controllers
{
    public class ListaZawodnikowController : Controller
    {
        //connection String
        DataBase db = new DataBase();

        public IActionResult Index()//list
        {
            List<Zawodnik> data = db.getZawodnikList();

            return View(data);
        }

        [HttpGet]                                   // !:::::! !:::::! !:::::! updated 11.12 DODANE : redirect na komunikat, sprawdzenie na exist,szczegoly inne !:::::! !:::::! !:::::! !:::::!
        public IActionResult AddZawodnik()
        {
          
            List<Zawodnik> treners = db.getZawodnikList(); //lista trenerow czyli zawodnikow

            treners.Add(new Zawodnik{ IdGosc=0,Nazwisko = "null" });

            ViewBag.Treners = treners;

            return View();
        } 

        [HttpPost]
        public IActionResult AddZawodnik(Zawodnik zawodnik)//add 
        {
            if (zawodnik.Trener == zawodnik.IdGosc) //jezeli wybral sebe samego jako trenera
                zawodnik.Trener = 0;

            ViewBag.Treners = db.getZawodnikList(); // pobieramy liste zawodnikow

            if (ModelState.IsValid) //walidacja po stronie serwera
            {
                foreach (Zawodnik c in ViewBag.Treners)  
                    if (c.Imie == zawodnik.Imie && c.Nazwisko == zawodnik.Nazwisko)//sprawdzamy na najawnosc
                    {
                        ViewBag.Error = "Podany zawodnik juz istnieje";          
                        return View(zawodnik);
                    }

                if (!db.insertZawodnik(zawodnik)){ // insert do bazy
                    Console.WriteLine("SQL Exception");
                    return View(zawodnik);
                }
            }else{
                Console.WriteLine("Model State Exception");
                return View(zawodnik);
            }

            return RedirectToAction("ZawodnikKomunikat", new { msg = "add" });
        }

        [HttpGet]
        public IActionResult EditZawodnik(int? id)
        {

            if (id == null) // nie poprawny id
                return NotFound();

            var zawodnik = db.getZawodnikById(id); // pobieramy zawodnika z bazy

            if (zawodnik == null)   //nie istnieje
                return NotFound();

            List<Zawodnik> treners = db.getZawodnikList();

            treners.Add(new Zawodnik { IdGosc = 0, Nazwisko = "null" });

            ViewBag.Treners = treners;

            return View(zawodnik);
        }

        [HttpPost]
        public IActionResult EditZawodnik(Zawodnik newzawodnik) //edit
        {
            if (newzawodnik.Trener == newzawodnik.IdGosc)//jezeli wybral sebe samego jako trenera
                newzawodnik.Trener = 0;

            if (ModelState.IsValid) 
            {
                if(!db.updateZawodnik(newzawodnik)) //update do bazy
                {
                    Console.WriteLine("SQL Exception"); 
                    return View(newzawodnik);
                }
            }

            return RedirectToAction("ZawodnikKomunikat", new { msg = "edit" });
        }

        [HttpGet]
        public IActionResult DeleteZawodnik(int? id)  
        {
            if (id == null)
                return NotFound();

            var zawodnik = db.getZawodnikById(id); //pobieramy

            if (zawodnik == null)
                return NotFound();


            return View(zawodnik); //wyswietlamy
        }

        [HttpPost]
        public IActionResult DeleteZawodnik(int id, bool pp)//delete 
        {
            if (pp)//pp odpowiada czy chcemy kasowac wszystkie mecze zawodnika
            {
                if (!db.deleteZawodnikWithMecz(id)) //kasujemy z bazy
                {
                    Console.WriteLine("SQL Exception");
                    return View();
                }
            }
            else
            {
                if(!db.deleteZawodnik(id))  //kasujemy z bazy
                {
                    Console.WriteLine("SQL Exception"); 
                    return View();
                }
            }

            return RedirectToAction("ZawodnikKomunikat", new { msg = "delete" });
        }

        public IActionResult ZawodnikKomunikat(string msg) //komunikat
        {
            if (msg == "delete")
                ViewBag.Msg = "usunięty";
            else if (msg == "edit")
                ViewBag.Msg = "modyfikowany";
            else if (msg == "add")
                ViewBag.Msg = "dodany";

            return View();
        }

        public IActionResult ZawodnikDetails(int? id) //details
        {
            if (id == null)
                return NotFound();

            Zawodnik z= db.getZawodnikById(id); // pobieramy zawodnika

            if (z.Trener == 0)          //jego trenera jezeli jest
                ViewBag.Trener = "null";
            else ViewBag.Trener = db.getZawodnikById(z.Trener).Imie + " " +db.getZawodnikById(z.Trener).Nazwisko;

            ViewBag.Zawodnik = z;

            List<ZawodnikDetails> data = db.getDetailsList(id); //jego mecze jezeli sa

            if (data.Count==0)          
                ViewBag.Msg = "podany zawodnik jeszcze nie zagral meczow";

                return View(data);
        }
    }
}