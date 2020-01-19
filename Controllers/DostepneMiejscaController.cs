using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TurniejTenisowy.Models;

namespace TurniejTenisowy.Controllers
{
    public class DostepneMiejscaController : Controller
    {
        //connection String
        DataBase db = new DataBase();

        [HttpGet]
        public IActionResult Index() // list()
        {
            return View(db.getMeczList());
        }

        [HttpGet]
        public IActionResult Organise(int? id)
        {
          
            if (db.getMeczById(id) == null) // nie istnieje
                return NotFound();

            MeczDetails details = new MeczDetails
            {
                IdMecz = Convert.ToInt32(id),
                Kibice = db.getKibicList(id) //lista kibicow na tym meczu
            };


            if (details.Kibice.Count == 0)
                ViewBag.Error = "Nie ma jeszcze gosci na wybrany mecz";
            else ViewBag.Error = "";

            return View(details);
        }

        [HttpPost]
        public IActionResult Organise(int IdMecz,int IdGosc)//delete 
        {
            if (!db.deleteMiejsce(IdGosc, IdMecz)) //kasujemy
                Console.WriteLine("SQL Exception");

            return RedirectToAction("Organise", new { idMecz = IdMecz }); 
        }

        [HttpGet]
        public IActionResult Add(int id)
        {
            Mecz m = db.getMeczById(id);

            if (m == null)
                return NotFound();

            List<Miejsce> lista = db.getMiejscaList(m.IdMecz); //lista dostepnych miejsc na mecz z bazy

            if (lista.Count == 0)
                ViewBag.Error = "niestety, nie zostalo dostepnych miesc dla wybranego meczu";
            else ViewBag.Error = "";

            ViewBag.Miejsca = lista;
            ViewBag.Gosci = db.getKibicList(); 

            return View(m);
        }

        [HttpPost]
        public IActionResult Add(int IdMecz,int idgosc, int idMiejsce)//add
        {

            if (IdMecz == 0 || idgosc == 0 || idMiejsce == 0)
                throw new Exception("Nie poprawnie zostalo wybrane dane");

            db.insertMiejsce(idMiejsce,idgosc,IdMecz);
            Miejsce m = db.getMiesceById(idMiejsce); //pobieramy numer miejsca

           return RedirectToAction("Komunikat", new { numer = m.NumerMiejsca, idgosc = idgosc });
        }

        [HttpGet]
        public IActionResult Komunikat(int numer,int idgosc)
        {
            ViewBag.Numer = numer;
            return View(db.getGoscById(idgosc));
        }

    }
}