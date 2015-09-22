using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc.Html;
using EPiServer.Web.Routing;

namespace Fruitcorp.Helpers
{
    public static class NavigationHelpers
    {
        public static void RenderMainNavigation(this HtmlHelper html)
        {
            var contentLink = html.ViewContext.RequestContext.GetContentLink();

            var writer = html.ViewContext.Writer;

            // top level 
            writer.WriteLine("<nav class=\"navbar navbar-inverse\">");
            writer.WriteLine("<ul class=\"nav navbar-nav\">");
          
            //start page länk
            if (ContentReference.StartPage.CompareToIgnoreWorkID(contentLink))
            {
                writer.WriteLine("<li class=\"active\">");
            }
            else
            {
                writer.WriteLine("<li>");
            }
            
            writer.WriteLine(html.PageLink(ContentReference.StartPage).ToHtmlString());
            writer.WriteLine("</li>");

            // hämta ut alla barn från start

            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();

            var topLevelPages = contentLoader.GetChildren<PageData>(ContentReference.StartPage);
            //skriv ut dom
            foreach (var topLevelPage in topLevelPages)
            {
                if (topLevelPage.ContentLink.CompareToIgnoreWorkID(contentLink))
                {
                    writer.WriteLine("<li class=\"active\">");
                }
                else
                {
                    writer.WriteLine("<li>");
                }
                
                writer.WriteLine(html.PageLink(topLevelPage).ToHtmlString());
                writer.WriteLine("</li>");
            }
            //Close top level
            writer.WriteLine("</ul");
            writer.WriteLine("</nav>");


        }
    }
}