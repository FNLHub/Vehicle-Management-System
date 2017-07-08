using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Transportation.Helper;

namespace Transportation.Models
{
        public class GenericViewModel
        {
            [Display(Name = "Generic")]
            public Guid SelectedItemId { get; set; }
            public List<SelectListItem> DropdownItems { get; set; }

            public GenericViewModel()
            {
                var ddlHelper = new DropDownListHelper();
                this.DropdownItems = ddlHelper.DropdownItems.ToList();
            }
        }
}