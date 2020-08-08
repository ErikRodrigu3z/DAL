using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VPModel.Models;
using DAL.Services;

namespace VetPaq.Controllers  
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //referencia al proyecto de modelo
            List<Usuarios> listUsuario = new List<Usuarios>();
            DAL.Services.DAL repo = new DAL.Services.DAL();

            listUsuario = repo.GetReaderFromStringToList<Usuarios>("select * from Usuarios");
            return View(listUsuario);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}