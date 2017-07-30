using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleManagementSystem.Models
{
    public class Drivers
    {
        public int TransReqId{ get; set; }
        public int DriverUserId { get; set; }
        public int VehicleTypeId { get; set; }
        public int VehicleAddonId { get; set; }
        public bool NeedGasCard { get; set; }
        public string PickupLocation { get; set; }
    }
}