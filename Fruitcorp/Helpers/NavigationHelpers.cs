using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPiServer;
using EPiServer.Core;
using EPiServer.Filters;
using EPiServer.ServiceLocation;
using EPiServer.Shell.UI;
using EPiServer.Web.Mvc.Html;
using EPiServer.Web.Routing;
using Lucene.Net.Documents;

namespace Fruitcorp.Helpers
{
    public static class NavigationHelpers
    {
        public static void RenderMainNavigation(this HtmlHelper html, PageReference rootLink = null,
            ContentReference contentLink = null,
            bool includeRoot = true, IContentLoader contentLoader = null)
        {
            contentLink = contentLink ?? html.ViewContext.RequestContext.GetContentLink();
            rootLink = rootLink ?? ContentReference.StartPage;

            var writer = html.ViewContext.Writer;

            // top level 
            writer.WriteLine("<nav class=\"navbar navbar-inverse\">");
            writer.WriteLine("<ul class=\"nav navbar-nav\">");
            if (includeRoot)
            {
                if (rootLink.CompareToIgnoreWorkID(contentLink))
                {
                    writer.WriteLine("<li class=\"active\">");
                }
                else
                {
                    writer.WriteLine("<li>");
                }

                writer.WriteLine(html.PageLink(rootLink).ToHtmlString());
                writer.WriteLine("</li>");
            }

            // hämta ut alla barn från start

            contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();

            var topLevelPages = contentLoader.GetChildren<PageData>(rootLink);
            topLevelPages = FilterForVisitor.Filter(topLevelPages).OfType<PageData>().Where(x => x.VisibleInMenu);

            var currentBranch = contentLoader.GetAncestors(contentLink).Select(x => x.ContentLink).ToList();
            currentBranch.Add(contentLink);
            //skriv ut dom
            foreach (var topLevelPage in topLevelPages)
            {
                if (currentBranch.Any(x => x.CompareToIgnoreWorkID(topLevelPage.ContentLink)))
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

        public static void RenderSubNavigation(this HtmlHelper html, ContentReference contentLink = null,
            IContentLoader contentLoader = null)
        {
            contentLink = contentLink ?? html.ViewContext.RequestContext.GetContentLink();
            contentLoader = contentLoader ?? ServiceLocator.Current.GetInstance<IContentLoader>();

            //hitta alla sidor mellan nuvarande sida och start, uppifrån ner

            var path =
                NavigationPath(contentLink, contentLoader, ContentReference.StartPage).Select(x => x.PageLink).ToList();

            //

            var currentPage = contentLoader.Get<IContent>(contentLink) as PageData;

            if (currentPage != null)
            {
                path.Add(currentPage.PageLink);
            }

            var root = path.FirstOrDefault();
            if (root == null)
            {
                return;
            }

            RenderSubNavigationLevel(html, root, path, contentLoader);
        }

        private static void RenderSubNavigationLevel(HtmlHelper helper, ContentReference levelRootLink,
            IEnumerable<ContentReference> path, IContentLoader contentLoader)
        {
            var children = contentLoader.GetChildren<PageData>(levelRootLink);
            children = FilterForVisitor.Filter(children).OfType<PageData>().Where(x => x.VisibleInMenu);

            if (!children.Any())
            {
                return;
            }

            var writer = helper.ViewContext.Writer;

            writer.WriteLine("<ul class=\"nav\">");

            var indexedChildren = children.Select((page, index) => new
            {
                index,
                page

            }).ToList();

            foreach (var levelItem in indexedChildren)
            {
                var page = levelItem.page;
                var partOfCurrentBranch = path.Any(x => x.CompareToIgnoreWorkID(levelItem.page.ContentLink));

                if (partOfCurrentBranch)
                {
                    writer.WriteLine("<li class=\"active\">");
                }
                else
                {

                    writer.WriteLine("<li>");
                }

                writer.WriteLine(helper.PageLink(page).ToHtmlString());

                if (partOfCurrentBranch)
                {
                    RenderSubNavigationLevel(helper, page.ContentLink, path, contentLoader);
                }

                writer.WriteLine("</li>");
            }

            writer.WriteLine("</ul>");
        }

        private static IEnumerable<PageData> NavigationPath(ContentReference contentLink, IContentLoader contentLoader,
            ContentReference fromLink = null)
        {

            fromLink = fromLink ?? ContentReference.RootPage;
            var path =
                contentLoader.GetAncestors(contentLink)
                    .Reverse()
                    .SkipWhile(x => ContentReference.IsNullOrEmpty(x.ParentLink) ||
                                    !x.ParentLink.CompareToIgnoreWorkID(ContentReference.StartPage))
                    .OfType<PageData>()
                    .ToList();

            var currentPage = contentLoader.Get<IContent>(contentLink) as PageData;

            if (currentPage != null)
            {
                path.Add(currentPage);
            }
            return path;
        }

        public static void RenderBreadCrumb(this HtmlHelper html, ContentReference contentLink = null,
            IContentLoader contentLoader = null)
        {
            contentLink = contentLink ?? html.ViewContext.RequestContext.GetContentLink();

            contentLoader = contentLoader ?? ServiceLocator.Current.GetInstance<IContentLoader>();

            var pagePath = NavigationPath(contentLink, contentLoader);

            var path = FilterForVisitor.Filter(pagePath).OfType<PageData>().Select(x => x.PageLink);

            if (!path.Any())
            {
                
            }

            var writer = html.ViewContext.Writer;

            writer.WriteLine("<ol class=\"breadcrumb\">");

            foreach (var part in path)
            {
                if (part.CompareToIgnoreWorkID(contentLink))
                {
                    writer.WriteLine("<li class=\"active\">");

                    var currentPage = contentLoader.Get<PageData>(contentLink);

                    writer.WriteLine(html.Encode(currentPage.PageName));
                }
                else
                {
                    writer.WriteLine("<li>");
                    writer.WriteLine(html.PageLink(part));
                }

                writer.WriteLine("</li>");
            }

            writer.WriteLine("</ol>");
        }
    }
}