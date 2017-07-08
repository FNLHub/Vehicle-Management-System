using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//Including Models
using Transportation.Models;

namespace Transportation.Helper
{
    public class DropDownListHelper
    {
        private List<DropDownListModel> _ItemDropdown;
        public IEnumerable<SelectListItem> DropdownItems
        {
            get
            {
                IEnumerable<SelectListItem> SelectItem = new List<SelectListItem>();
                SelectItem = _ItemDropdown.Select(x => new SelectListItem
                {
                    Value = x.ItemId.ToString(),
                    Text = x.ItemName
                });
                return DefaultItem.Concat(SelectItem);
            }
        }

        public IEnumerable<SelectListItem> DefaultItem
        {
            get
            {
                return Enumerable.Repeat(new SelectListItem
                {
                    Value = "",
                    Text = "- SELECT -"
                }, count: 1);
            }
        }
    }

    //private List<dropDownList> _content;
    //public IEnumerable<SelectListItem> vehicleItems
    //{
    //    get
    //    {
    //        IEnumerable<SelectListItem> selectVehicle = new List<SelectListItem>();
    //        selectVehicle = _vehicleDropdown.Select(x => new SelectListItem
    //        {
    //            Value = x.vehicleId.ToString(),
    //            Text = x.vehicleName
    //        });
    //        return DefaultItem.Concat(selectVehicle);
    //    }
    //}

    //public IEnumerable<SelectListItem> DefaultItem
    //{
    //    get
    //    {
    //        return Enumerable.Repeat(new SelectListItem
    //        {
    //            Value = "",
    //            Text = "- SELECT -"
    //        }, count: 1);
    //    }
    //}

    //public class GenericViewModel
    //{
    //    [Display(Name = "Vehicle")]
    //    public Guid SelectedCountryId { get; set; }
    //    public List<SelectListItem> vehicleItems { get; set; }

    //    public GenericViewModel()
    //    {
    //        var ddlHelper = new DropDownListHelper();
    //        this.vehicleItems = ddlHelper.CountryItems.ToList();
    //    }
    //}

}