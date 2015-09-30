using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using Fruitcorp.Models.Blocks;

namespace Fruitcorp.Controllers
{
    public class ProductBlockController : BlockController<ProductBlock>
    {
        public override ActionResult Index(ProductBlock currentBlock)
        {
            return PartialView(currentBlock);
        }
    }
}
