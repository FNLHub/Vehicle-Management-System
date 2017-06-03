//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Transportation
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.SignOuts = new HashSet<SignOut>();
            this.TransporationRequests = new HashSet<TransporationRequest>();
        }
    
        public int UserId { get; set; }
        public string BannerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DepartmentId { get; set; }
        public int StatusId { get; set; }
        public string OfficeAreaCode { get; set; }
        public string OfficePhoneNumber { get; set; }
        public string CellAreaCode { get; set; }
        public string CellPhoneNumber { get; set; }
    
        public virtual Department Department { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SignOut> SignOuts { get; set; }
        public virtual UserStatus UserStatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransporationRequest> TransporationRequests { get; set; }
    }
}
