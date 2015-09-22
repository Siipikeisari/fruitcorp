using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace Fruitcorp.Models.Pages
{
    [ContentType]
    public class StandardPage : PageData
    {
        public virtual string MainIntro { get; set; }

        public virtual XhtmlString MainBody { get; set; }
    }
}