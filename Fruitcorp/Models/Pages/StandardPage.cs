using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace Fruitcorp.Models.Pages
{
    [ContentType(GUID = "fcb77134-1547-452b-ac4b-cf7a0a94fbc6",
        DisplayName = "Standard",
        Description = "Used for the standard editorial content",
        GroupName = "Editorial")]
    [ImageUrl("~/Content/Icons/Standard.png")]
    [AvailableContentTypes(Include = new[] { typeof(StandardPage) })]
    public class StandardPage : BasePage
    {
            public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
                MainIntro = "Default, Change me";
        }
        [Display(GroupName = "Meta Data", Order = 10)]
        [UIHint("textarea")]
        public override string Title
        {
            get
            {
                var title = base.Title;

                if (!string.IsNullOrEmpty(title))
                {
                    title = PageName;
                }
                return title;
            }
            set
            {
                base.Title = value;
            }
        }
        [Display(GroupName = "Meta Data", Order = 20)]
        [UIHint("textarea")]
        public override string MetaDescription
        {
            get
            {
                var metaDescription = base.MetaDescription;
                if (!string.IsNullOrEmpty(metaDescription))
                {
                    metaDescription = MainIntro;
                }
                return metaDescription;
            }
            set
            {
                base.MetaDescription = value;
            }
        }

        
        public virtual string MainIntro { get; set; }

        public virtual XhtmlString MainBody { get; set; }
    }
}