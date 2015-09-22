using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPiServer.Web.Mvc;
using Fruitcorp.Models.Pages;

namespace Fruitcorp.Controllers
{
    public class StartController : PageController<StartPage>
    {
        //
        // GET: /Start/

        public ActionResult Index(StartPage currentPage)
        {
            return View(currentPage);
        }

    }
}
