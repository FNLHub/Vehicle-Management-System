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
    
    public partial class GasCard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GasCard()
        {
            this.SignOuts = new HashSet<SignOut>();
        }
    
        public int GasCardId { get; set; }
        public string GasCardName { get; set; }
        public string GasCardNum { get; set; }
        public string GasCardPin { get; set; }
        public int StatusId { get; set; }
    
        public virtual GasCardStatus GasCardStatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SignOut> SignOuts { get; set; }
    }
}
