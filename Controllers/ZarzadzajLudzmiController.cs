using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TurniejTenisowy.Models;

namespace TurniejTenisowy.Controllers
{
    public class ZarzadzajLudzmiController : Controller
    {
        //connection String
        DataBase db = new DataBase();

        public IActionResult Index() 
        {
            return View();
        }


        public IActionResult Sedzia() //list
        {
            return View(db.getSedziaList());
        }

        public IActionResult Kibice()//list
        {
            return View(db.getKibicList());
        }

        [HttpGet]
        public IActionResult EditSedzia(int? id)
        {
            List<Sedzia> list = db.getSedziaList();

            return View(list.Where(s => s.IdSedzia == id).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult EditSedzia(Sedzia edited) //edit 
        {
            List<Sedzia> list = db.getSedziaList();

            foreach (Sedzia s in list)
               if(s.IdSedzia != edited.IdSedzia) //id 
                if (s.Imie == edited.Imie && s.Nazwisko == edited.Nazwisko && s.DataUrodzenia==edited.DataUrodzenia )
                {
                    ViewBag.Error = "Podany Sedzia juz istnieje";
                    return View(edited);
                }
                else ViewBag.Error = "";

            if (!db.updateSedzia(edited))
                Console.WriteLine("SQL Exception");
            return RedirectToAction("Komunikat", new { msg = "edit"});
        }

        [HttpGet]
        public IActionResult DetailsSedzia(int? id)//details
        {

            List<MeczListOpak> list = db.getSedziaDetails(id);

            if (list.Count == 0)
                ViewBag.Error = "Sedzia jeszcze nie byl obecny na zadnych meczach";
            else ViewBag.Error = "";

            return View(list);                      
        }

        [HttpGet]
        public IActionResult DeleteSedzia(int? id)
        {
            List<Sedzia> list = db.getSedziaList();
            return View(list.Where(s => s.IdSedzia == id).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult DeleteSedzia(Sedzia s)//delete
        {
            if (!db.deleteSedzia(s.IdSedzia))
                Console.WriteLine("SQL Exception");
            return RedirectToAction("Komunikat", new { msg = "delete" });
        }

        [HttpGet]
        public IActionResult EditKibic(int? id)
        {
            List<Zawodnik> list = db.getGosciList();
            return View(list.Where(s => s.IdGosc == id).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult EditKibic(Zawodnik edited)//edit
        {
            List<Zawodnik> list = db.getGosciList();

            foreach (Zawodnik z in list)
                if (z.Imie == edited.Imie && z.Nazwisko == edited.Nazwisko && z.DataUrodzenia == edited.DataUrodzenia)
                {
                    ViewBag.Error = "podany gosc juz istnieje";
                    return View(edited);
                }
                else ViewBag.Error = "";

            if (!db.updateKibic(edited))
                Console.WriteLine("SQL Exception");

            return RedirectToAction("Komunikat", new { msg = "edit" });
        }

        [HttpGet]
        public IActionResult DetailsKibic(int? id)
        {
            List<KibicDetails> list = db.getDetailsKibic(id);

            if (list.Count == 0)
                ViewBag.Error = "Podany Kibic nie zarezerwowal zadnego miejsca";
            else ViewBag.Error = "";

            return View(list);
        }

        [HttpGet]
        public IActionResult DeleteKibic(int? id)
        {
            List<Zawodnik> list = db.getGosciList();
            return View(list.Where(s => s.IdGosc == id).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult DeleteKibic(Zawodnik s)//delete
        {
            if (!db.deleteKibic(s.IdGosc))
                Console.WriteLine("SQL Exception");
            return RedirectToAction("Komunikat", new { msg = "delete" });
        }


        public IActionResult Komunikat(string msg)//komunikat
        {
            if (msg == "delete")
                ViewBag.Msg = "usunieta";
            else if (msg == "edit")
                ViewBag.Msg = "modyfikowana";
            else if (msg == "add")
                ViewBag.Msg = "dodana";

            return View();
        }
    }
}