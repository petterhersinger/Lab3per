using Microsoft.AspNetCore.Mvc;
using Lab3.Models;

namespace Lab3.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult InsertPerson()
        {
            PersonDetalj pd = new PersonDetalj();
            PersonMetoder pm = new PersonMetoder();
            int i = 0;
            string error = "";

            pd.Fornamn = "Alice";
            pd.Efternamn = "Karlsson";
            pd.Epost = "Alice.karlsson@gmail.com";
            pd.Fodelsear = 1999;
            pd.Bor = 1;

            i = pm.InsertPerson(pd, out error);
            ViewBag.error = error;
            ViewBag.antal = i;

            return View();
        }

        [HttpGet]
        public IActionResult InsertPerson2() {
            return View();
        }
        [HttpPost]
        public IActionResult InsertPerson2(PersonDetalj pd)
        {
            PersonMetoder pm = new PersonMetoder();
            int i = 0;
            string error = "";
            i = pm.InsertPerson(pd, out error);
            ViewBag.error = error;
            ViewBag.antal = i;

            //if( i == 0) { return RedirectToAction("SelectWithDataSet"); }
            //else { return View("InsertPerson"); }

            return View("InsertPerson");
        }

        public IActionResult DeleteKarlsson()
        {
            PersonMetoder pm = new PersonMetoder();
            string error = "";
            int i = 0;
            i = pm.DeleteKarlsson(out error);
            HttpContext.Session.SetString("antal", i.ToString());
            return RedirectToAction("SelectWithDataSet");
        }

        public ActionResult SelectWithDataSet()
        {
            List<PersonDetalj> PersonList = new List<PersonDetalj>();
            PersonMetoder pm = new PersonMetoder();
            string error = "";
            PersonList = pm.GetPersonWithDataSet(out error);
            ViewBag.antal = HttpContext.Session.GetString("antal");
            ViewBag.error = error;
            return View(PersonList);
        }

        public ActionResult SelectWithDataReader()
        {
            List<PersonDetalj> PersonList = new List<PersonDetalj>();
            PersonMetoder pm = new PersonMetoder();
            string error = "";
            PersonList = pm.GetPersonWithReader(out error);
            ViewBag.antal = HttpContext.Session.GetString("antal");
            ViewBag.error = error;
            return View(PersonList);
        }
        public ActionResult SelectAktivitet()
        {
            List<PersonAktivitetModel> AktivitetLista = new List<PersonAktivitetModel>();
            PersonMetoder pm = new PersonMetoder();
            string error = "";
            AktivitetLista = pm.GetPersonAktivitet(out error);
            ViewBag.antal = HttpContext.Session.GetString("antal");
            ViewBag.error = error;
            return View(AktivitetLista);
        }
        [HttpGet]
        public ActionResult Filtrering()
        {
            PersonAktivitetMetoder pm = new PersonAktivitetMetoder();
            AktivitetMetoder am = new AktivitetMetoder();

            ViewModelPA myModel = new ViewModelPA
            {
                PersonAktivitetModelLista = pm.GetPersonAktivitetModel(out string errormsg),
                AktivitetModelLista = am.GetAktivitetLista(out string errormsg2)
            };
            ViewBag.error = "1: " + errormsg + "2: " + errormsg2;
            return View(myModel);
        }
        [HttpGet]
        public ActionResult Filtrering2()
        {
            PersonAktivitetMetoder pm = new PersonAktivitetMetoder();
            AktivitetMetoder am = new AktivitetMetoder();

            ViewModelPA myModel = new ViewModelPA
            {
                PersonAktivitetModelLista = pm.GetPersonAktivitetModel(out string errormsg),
                AktivitetModelLista = am.GetAktivitetLista(out string errormsg2)
            };

            List<AktivitetModel> AktivitetLista = new List<AktivitetModel>();
            AktivitetLista = am.GetAktivitetLista(out string errormsg3);
            ViewBag.error = "1: " + errormsg + "2: " + errormsg2 + "3: " + errormsg3;
            ViewData["aktivitetslista"] = AktivitetLista;

            ViewBag.aktivitetslista = AktivitetLista;

            return View(myModel);
        }
        [HttpPost]
        public ActionResult Filtrering2(string Aktivitet)
        {
            int i = Convert.ToInt32(Aktivitet);
            ViewData["Aktivitet"] = i;

            PersonAktivitetMetoder pm = new PersonAktivitetMetoder();
            AktivitetMetoder am = new AktivitetMetoder();

            ViewModelPA myModel = new ViewModelPA
            {
                PersonAktivitetModelLista = pm.GetPersonAktivitetModel(out string errormsg),
                AktivitetModelLista = am.GetAktivitetLista(out string errormsg2)
            };

            List<AktivitetModel> AktivitetLista = new List<AktivitetModel>();
            AktivitetLista = am.GetAktivitetLista(out string errormsg3);
            ViewBag.error = "1: " + errormsg + "2: " + errormsg2 + "3: " + errormsg3;
            ViewData["aktivitetslista"] = AktivitetLista;

            ViewBag.aktivitetslista = AktivitetLista;
            ViewBag.message = Aktivitet;

            return View(myModel);
        }
    }
}
