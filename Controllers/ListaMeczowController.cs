using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TurniejTenisowy.Models;

namespace TurniejTenisowy.Controllers
{
    public class ListaMeczowController : Controller
    {
        
        DataBase db = new DataBase();

        public IActionResult Index() //list()
        {
            List<MeczListOpak> data = db.getMeczList();

            return View(data);
        }

        
        [HttpGet]
        public IActionResult AddMecz()
        {
            ViewBag.Zawodniki = db.getZawodnikList();
            ViewBag.Sedzia = db.getSedziaList();

            return View();
        }

        [HttpPost]
        public IActionResult AddMecz(Mecz m, int sedzia) //add()
        {

            if (ModelState.IsValid)
            {
                if (!db.insertMecz(m, sedzia))
                {
                    Console.WriteLine("Sql Exception");
                    return View();
                }
            }

            return RedirectToAction("Komunikat", new { msg = "add" });
        }

        [HttpGet]
        public IActionResult EditMecz(int? id)
        {
            Mecz m = db.getMeczById(id);
            Sedzia sd = db.getSedziaByMecz(m.IdMecz);

            if (m == null)
                ViewBag.Error = "SQL Exception";

            ViewBag.Zawodniki = db.getZawodnikList();
            List<Sedzia> lst = db.getSedziaList();

            if (sd.IdSedzia == 0)
            {
                m.Sedzia = 0;
                lst.Add(new Sedzia { IdSedzia = 0, Nazwisko = "null" });
            }
            else
                m.Sedzia = sd.IdSedzia;

            ViewBag.Sedzia = lst; 

            return View(m);
        }

        [HttpPost]
        public IActionResult EditMecz(Mecz m,int sedzia) //edit()
        {
            if (ModelState.IsValid)
            {
                if(!db.updateMecz(m,sedzia))
                {
                    Console.WriteLine("SQL EXCEPTION");
                    return View(m);
                }
            }

            return RedirectToAction("Komunikat", new { msg = "edit" });
        }

        [HttpGet]
        public IActionResult DeleteMecz(int? id)
        {
            List<MeczListOpak> data = db.getMeczList();
            MeczListOpak retmecz = null;

            foreach(MeczListOpak m in data){
                if (m.IdMecz == id)
                    retmecz = m;
            }

            return View(retmecz);
        }

        [HttpPost]
        public IActionResult DeleteMecz(MeczListOpak retmecz) //delete()
        {

            if (!db.deleteMecz(retmecz.IdMecz))
            {
                ViewBag.Error = "SQL Exception";
                return View(retmecz);
            }

            return RedirectToAction("Komunikat", new { msg = "delete" });
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            List<MeczListOpak> lis = db.getMeczList();

            MeczListOpak mecz = new MeczListOpak();

            foreach (MeczListOpak m in lis)
                if (m.IdMecz == id)
                    mecz = m;

            MeczDetails details = new MeczDetails
            {
                Kibice = db.getKibicList(id),
                Sedzia = db.getSedziaByMecz(id)
            };


            if (details.Kibice.Count == 0)
                details.Kibice.Add(new Zawodnik{ Imie = "null", Nazwisko = "null" });

            if (details.Sedzia.Imie == null)
                details.Sedzia.Imie = "null";


            ViewBag.Gosci = details;

            return View(mecz);
        } //details list()

        public IActionResult Komunikat(string msg) //komunikat
        {
            if (msg == "delete")
                ViewBag.Msg = "usunięty";
            else if (msg == "edit")
                ViewBag.Msg = "modyfikowany";
            else if (msg == "add")
                ViewBag.Msg = "dodany";

            return View();
        }
    }
}
