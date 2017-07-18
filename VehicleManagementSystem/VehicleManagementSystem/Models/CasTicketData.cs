using DotNetCasClient;

namespace VehicleManagementSystem.Models
{
    public class CasTicketData
    {
        public CasAuthenticationTicket CasTicket { get; set; }
        public string CasUserID { get; set; }
        public string DisplayName { get; set; }
        public string BannerID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }

    }
}