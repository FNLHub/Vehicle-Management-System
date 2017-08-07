using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace VehicleManagementSystem.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username or Password is not correct."), AllowHtml]
        [DisplayName("Student Email (or Faculty/Staff Username):")]
        public string UserName { get; set; }

        [Required(ErrorMessage = " "), AllowHtml]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Token { get; internal set; }
        public bool Success { get; internal set; }
    }
}