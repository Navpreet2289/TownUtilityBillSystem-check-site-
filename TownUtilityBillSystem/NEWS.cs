//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TownUtilityBillSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class NEWS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NEWS()
        {
            this.NEWSCHAPTERs = new HashSet<NEWSCHAPTER>();
        }
    
        public int ID { get; set; }
        public string TITLE { get; set; }
        public System.DateTime DATE { get; set; }
        public Nullable<int> IMAGE_ID { get; set; }
    
        public virtual IMAGENEWS IMAGENEW { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NEWSCHAPTER> NEWSCHAPTERs { get; set; }
    }
}
