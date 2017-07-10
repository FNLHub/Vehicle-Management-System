using DotNetCasClient;
using System.Web;
using System.Web.Security;

namespace Voyager2._0.Models
{
    public class CASTicketClass
    {
        public CasTicketData GetTicketData(HttpCookie ticketCookie)
        {
            var myTicketData = new CasTicketData();
            if (ticketCookie != null)
            {
                if (!string.IsNullOrEmpty(ticketCookie.Value))
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(ticketCookie.Value);
                    if (ticket != null)
                    {
                        if (CasAuthentication.ServiceTicketManager != null)
                        {
                            CasAuthenticationTicket casTicket = CasAuthentication.ServiceTicketManager.GetTicket(ticket.UserData);
                            if (casTicket != null)
                            {
                                myTicketData.CasTicket = casTicket;
                                myTicketData.CasUserID = casTicket.NetId;
                                GetAssertAttribute(myTicketData, casTicket);
                            }
                        }
                    }
                }
            }
            return myTicketData;
        }

        private static void GetAssertAttribute(CasTicketData myTicketData, CasAuthenticationTicket casTicket)
        {
            try
            {
                myTicketData.BannerID = casTicket.Assertion.Attributes["BannerID"][0];
            }
            catch
            {
                myTicketData.BannerID = "No BannerID";
            }
            try
            {
                myTicketData.FirstName = casTicket.Assertion.Attributes["FirstName"][0];
            }
            catch
            {
                myTicketData.FirstName = "Invalid FirstName";
            }

            try
            {
                myTicketData.LastName = casTicket.Assertion.Attributes["LastName"][0];
            }
            catch
            {
                myTicketData.LastName = "Invalid LastName";
            }

            try
            {
                myTicketData.Email = casTicket.Assertion.Attributes["emailAddress"][0];
            }
            catch
            {
                myTicketData.Email = "Invalid Email";
            }

            try
            {
                myTicketData.FullName = $"{myTicketData.FirstName} {myTicketData.LastName}";
            }
            catch
            {
                myTicketData.FullName = "Invalid FullName";
            }
        }        
    }
}