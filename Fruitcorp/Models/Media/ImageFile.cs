using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;

namespace Fruitcorp.Models.Media
{
    [ContentType(DisplayName = "ImageFile", GUID = "934f985f-ddc3-4c50-8c92-d86d8cc8a4ed", Description = "")]
    [MediaDescriptor(ExtensionString = "pdf,doc,docx,jpeg,jpg")]
    public class ImageFile : ImageData
    {
        public virtual string AltText { get; set; }
    }
}