using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Sgml;

namespace Fruitcorp.Models.Pages
{
    [Access(Roles = "CmsAdmins")]
    [ContentType(GUID = "ad6c947e-c2f1-43a6-934d-fa268f48ad3e",
        GroupName = "Specialized")]
    [ImageUrl("~/Content/Icons/Home.png")]
    [AvailableContentTypes(Include = new[] { typeof(StandardPage)})]
    public class StartPage : BasePage
    {
        
    }
}