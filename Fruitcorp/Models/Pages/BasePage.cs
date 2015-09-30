using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.XForms.WebControls;

namespace Fruitcorp.Models.Pages
{
    

    public abstract class BasePage : PageData
    {
    

        [Display(GroupName = "Meta Data", Order = 10)]
        public virtual string Title { 
            
            get { return this.GetPropertyValue(x => x.Title); } 
            set { this.SetPropertyValue(x => x.Title, value); } 

        }
         [Display(GroupName = "Meta Data", Order = 20)]
        [UIHint("textarea")]
        public virtual string MetaDescription
        {
            get { return this.GetPropertyValue(x => x.MetaDescription); } 
            set { this.SetPropertyValue(x => x.MetaDescription,value); }
        }

  
    }
}