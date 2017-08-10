using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportationDB;

namespace VehicleManagementSystem.Models
{
    public class UniversalDropDownClass
    {
        private List<SelectListItem> _ListItem{ get; set; }
        private FacilitiesDBEntities _Transcontext { get; set; }
        private DropDownModel _Drop { get; set; }

        //Default Constructor
        public UniversalDropDownClass()
        {

            _ListItem = new List<SelectListItem>();
            _Transcontext = new FacilitiesDBEntities();
            _Drop = new DropDownModel();

        }

        //Reset Variables
        private void Reset()
        {
            _ListItem = new List<SelectListItem>();
            _Transcontext = new FacilitiesDBEntities();
            _Drop = new DropDownModel();
        }
        //Populate Users
        public List<SelectListItem> PopulateUsers()
        {

            //Reset Variables on call
            Reset();

            for (int i = 0; i <= _Transcontext.Users.Count(); i++)
            {

                _Drop.Id = _Transcontext.Users.Where(v => v.UserId == i).Select(v => v.UserId).FirstOrDefault();
                _Drop.Value = _Transcontext.Users.Where(v => v.UserId == i).Select(v => v.FirstName + " " + v.LastName).FirstOrDefault();
                
                _ListItem.Add(new SelectListItem() { Value = _Drop.Value, Text = _Drop.Id.ToString() });
            }

            return _ListItem;
        }
        //Populate Vehicles
        public List<SelectListItem> PopulateVehicles()
        {

            //Reset Variables on call
            Reset();

            for (int i = 1; i <= _Transcontext.Vehicles.Count(); i++)
            {

                _Drop.Id = _Transcontext.Vehicles.Where(v => v.VehicleId == i).Select(v => v.VehicleId).FirstOrDefault();
                _Drop.Value = _Transcontext.Vehicles.Where(v => v.VehicleId == i).Select(v => v.VehicleName).FirstOrDefault();

                _ListItem.Add(new SelectListItem() { Value = _Drop.Value, Text = _Drop.Id.ToString() });
            }

            return _ListItem;
        }

        //Populate Vehicles Addons
        public List<SelectListItem> PopulateVehiclesAddons()
        {
            //Reset Variables on call
            Reset();

            for (int i = 1; i <= _Transcontext.VehicleAddons.Count(); i++)
            {

                _Drop.Id = _Transcontext.VehicleAddons.Where(v => v.VehicleAddonId == i).Select(v => v.VehicleAddonId).FirstOrDefault();
                _Drop.Value = _Transcontext.VehicleAddons.Where(v => v.VehicleAddonId == i).Select(v => v.AddonName).FirstOrDefault();

                _ListItem.Add(new SelectListItem() { Value = _Drop.Value, Text = _Drop.Id.ToString() });
            }

            return _ListItem;
        }

        //Populate Vehicle Types
        public List<SelectListItem> PopulateVehicleTypes()
        {
            //Reset Variables on call
            Reset();

            for (int i = 1; i <= _Transcontext.VehicleTypes.Count(); i++)
            {

                _Drop.Id = _Transcontext.VehicleTypes.Where(v => v.VehicleTypeId == i).Select(v => v.VehicleTypeId).FirstOrDefault();
                _Drop.Value = _Transcontext.VehicleTypes.Where(v => v.VehicleTypeId == i).Select(v => v.TypeName).FirstOrDefault();

                _ListItem.Add(new SelectListItem() { Value = _Drop.Value, Text = _Drop.Id.ToString() });
            }

            return _ListItem;
        }

        //Populate Gas Cards
        public List<SelectListItem> PopulateGasCards()
        {
            //Reset Variables on call
            Reset();

            for (int i = 1; i <= _Transcontext.GasCards.Count(); i++)
            {

                _Drop.Id = _Transcontext.GasCards.Where(v => v.GasCardId == i).Select(v => v.GasCardId).FirstOrDefault();
                _Drop.Value = _Transcontext.GasCards.Where(v => v.GasCardId == i).Select(v => v.GasCardName).FirstOrDefault();

                _ListItem.Add(new SelectListItem() { Value = _Drop.Value, Text = _Drop.Id.ToString() });
            }

            return _ListItem;
        }

        //Populate Approved Drivers
        public List<SelectListItem> PopulateApprovedDrivers()
        {
            //Reset Variables on call
            Reset();

            for (int i = 1; i <= _Transcontext.Users.Count(); i++)
            {

                _Drop.Id = _Transcontext.Users.Where(v => v.UserId == i).Select(v => v.UserId).FirstOrDefault();
                _Drop.Value = _Transcontext.Users.Where(v => v.UserId == i).Select(v => v.FirstName + " " + v.LastName).FirstOrDefault();//v.BannerId + " " + 

                _ListItem.Add(new SelectListItem() { Value = _Drop.Value, Text = _Drop.Id.ToString() });
            }

            return _ListItem;
        }
    }
}