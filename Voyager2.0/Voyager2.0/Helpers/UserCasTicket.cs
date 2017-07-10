using Voyager2._0.Models;
using System.Web;

namespace Voyager2._0.Helpers
{
    public class UserCasTicket
    {
        /// <summary>
        /// Get the CAS ticket from HttpCookie
        /// </summary>
        /// <param name="ticketCookie"></param>
        /// <returns></returns>
        public CasTicketData GetTicket(HttpCookie ticketCookie)
        {
            CasTicketData casTicketClass = null;

            if (ticketCookie != null)
            {
                CASTicketClass casTicket = new CASTicketClass();
                casTicketClass = casTicket.GetTicketData(ticketCookie);
            }
            
            return casTicketClass;
        }
    }
}