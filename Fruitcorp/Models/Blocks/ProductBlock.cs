using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace Fruitcorp.Models.Blocks
{
    [ContentType(DisplayName = "ProductBlock", GUID = "c7cd1de1-1cb1-44b3-9eaa-92c7a8ead326", Description = "")]
    public class ProductBlock : BlockData
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