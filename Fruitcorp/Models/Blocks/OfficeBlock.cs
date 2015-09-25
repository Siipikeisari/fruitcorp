using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.BaseLibrary.Search;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace Fruitcorp.Models.Blocks
{
    [ContentType(DisplayName = "OfficeBlock", GUID = "5eac42c8-6ce7-4b1b-84e7-e23d0ede84b2", Description = "")]
    public class OfficeBlock : BlockData
    {
        [Display(Order = 20)]
        public virtual string Header { get; set; }
        [Display(Order = 30)]
        [UIHint("UIHint.longstring")]
        public virtual string Text { get; set; }
        [Display(Order = 40)]
        [UIHint("Image")]
        public virtual ContentReference Image { get; set; }
        [Display(Order = 50)]
        public virtual PageReference Link { get; set; }
    }
}