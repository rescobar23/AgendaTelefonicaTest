using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgendaTelefonica.API.Controllers
{
    public class AgendaController : Controller
    {
        // GET: Agenda
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AgregarContacto()
        {
            return View();
        }

        public ActionResult EditarContacto(int id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}