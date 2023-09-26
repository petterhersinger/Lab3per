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

            return View("InsertPerson2");
        }

        [HttpGet]
        public IActionResult Delete(int person_id)
        {
            PersonMetoder pm = new PersonMetoder();
            string error = "";
            var person = pm.GetPerson(person_id,out error);
            ViewBag.error = error;
            if (person != null)
            {
                return View(person);
            }
            return RedirectToAction("SelectWithDataSet");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete2(int person_id)
        {
            PersonMetoder pm = new PersonMetoder();
            string error = "";
            int i = pm.DeletePerson(person_id, out error);
            HttpContext.Session.SetString("antal", i.ToString());
            return RedirectToAction("SelectWithDataSet");
        }

        [HttpGet]
        public IActionResult Edit(int person_id)
        {
            PersonMetoder pm = new PersonMetoder();
            string error = "";
            var person = pm.GetPerson(person_id, out error);

            if (person != null)
            {
                return View(person);
            }

            ViewBag.error = error; // You can choose to handle the error message as needed
            return RedirectToAction("SelectWithDataSet");
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int person_id, PersonDetalj updatedPerson)
        {
            if (ModelState.IsValid)
            {
                PersonMetoder pm = new PersonMetoder();
                string error = "";

                // Call the UpdatePerson method to update the person's details
                int result = pm.UpdatePerson(person_id, updatedPerson, out error);

                if (result > 0)
                {
                    return RedirectToAction("Details", new { person_id = person_id });
                }
                else
                {
                    ViewBag.error = error; // Handle the error message as needed
                }
            }

            // If ModelState is not valid or an error occurred, redisplay the Edit view
            return View(updatedPerson);
        }


        public IActionResult SelectWithDataSet()
        {
            List<PersonDetalj> PersonList = new List<PersonDetalj>();
            PersonMetoder pm = new PersonMetoder();
            string error = "";
            PersonList = pm.GetPersonWithDataSet(out error);
            ViewBag.antal = HttpContext.Session.GetString("antal");
            ViewBag.error = error;
            return View(PersonList);
        }

        public IActionResult Details(int person_id)
        {
            PersonDetalj Person = new PersonDetalj();
            PersonMetoder pm = new PersonMetoder();
            Person = pm.GetPerson(person_id, out string error);
            ViewBag.error = error;
            return View(Person);
        }

        public IActionResult SelectWithDataReader()
        {
            List<PersonDetalj> PersonList = new List<PersonDetalj>();
            PersonMetoder pm = new PersonMetoder();
            string error = "";
            PersonList = pm.GetPersonWithReader(out error);
            ViewBag.antal = HttpContext.Session.GetString("antal");
            ViewBag.error = error;
            return View(PersonList);
        }
        public IActionResult SelectAktivitet()
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
        public IActionResult Filtrering()
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
        public IActionResult Filtrering2()
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
        public IActionResult Filtrering2(string Aktivitet)
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
        [HttpGet]
        public IActionResult Filtrering3()
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
        public IActionResult Filtrering3(string Aktivitet)
        {
            int i = Convert.ToInt32(Aktivitet);
            PersonAktivitetMetoder pm = new PersonAktivitetMetoder();
            AktivitetMetoder am = new AktivitetMetoder();

            ViewModelPA myModel = new ViewModelPA
            {
                PersonAktivitetModelLista = pm.GetPersonAktivitetModel(out string errormsg,i),
                AktivitetModelLista = am.GetAktivitetLista(out string errormsg2)
            };

            List<AktivitetModel> AktivitetLista = new List<AktivitetModel>();
            AktivitetLista = am.GetAktivitetLista(out string errormsg3);

            ViewBag.error = "1: " + errormsg + "2: " + errormsg2 + "3: " + errormsg3;
            ViewData["aktivitetslista"] = AktivitetLista;

            ViewBag.aktivitetslista = AktivitetLista;
            ViewBag.message = Aktivitet;
            ViewData["Aktivitet"] = i;

            return View(myModel);
        }
        [HttpGet]
        public IActionResult Sortering(string sortering)
        {
            PersonAktivitetMetoder pm = new PersonAktivitetMetoder();
            AktivitetMetoder am = new AktivitetMetoder();

            List<PersonAktivitetModel> PersonAktivitetModelLista = pm.GetPersonAktivitetModel(out string errormsg);

            string aktuellRiktning = HttpContext.Session.GetString("Riktning");

            bool stigande = true;

            if (aktuellRiktning != null)
            {
                stigande = aktuellRiktning == "asc";
            }

            ViewBag.Riktning = stigande ? "asc" : "desc";

            if (sortering == "fornamn")
            {
                if (stigande)
                {
                    PersonAktivitetModelLista = PersonAktivitetModelLista.OrderBy(s => s.Fornamn).ToList();
                    HttpContext.Session.SetString("Riktning", "desc");
                }
                else
                {
                    PersonAktivitetModelLista = PersonAktivitetModelLista.OrderByDescending(s => s.Fornamn).ToList();
                    HttpContext.Session.SetString("Riktning", "asc");
                }
            }
            else
            {
            }

            ViewModelPA myModel = new ViewModelPA
            {
                PersonAktivitetModelLista = PersonAktivitetModelLista,
                AktivitetModelLista = am.GetAktivitetLista(out string errormsg2)
            };

            ViewBag.sortera = sortering;

            return View(myModel);
        }
        [HttpGet]
        public IActionResult Search()
        {
            List<PersonDetalj> personLista = new List<PersonDetalj>();
            PersonMetoder pm = new PersonMetoder();
            string error = "";
            personLista = pm.GetPersonWithReader(out error);
            ViewBag.error = error;
            return View(personLista);
        }


        [HttpPost]
        public IActionResult Search(string input)
        {
            PersonMetoder pm = new PersonMetoder();
            string error = "";

            List<PersonDetalj> person = pm.SearchPerson(input, out string errormsg);

            ViewBag.error = errormsg;

            if (person != null)
            {
                return View(person);
            }

            return RedirectToAction("SelectWithDataSet");
        }

    }
}
