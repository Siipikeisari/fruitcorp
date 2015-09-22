using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPiServer.Web.Mvc;
using Fruitcorp.Models.Pages;

namespace Fruitcorp.Controllers
{
    public class StandardPageController : PageController<StandardPage>
    {
        //
        // GET: /StandardPage/

        public ActionResult Index(StandardPage currentPage)
        {
            return View(currentPage);
        }

    }
}
