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
    
    public partial class METER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public METER()
        {
            this.METER_ITEM = new HashSet<METER_ITEM>();
        }
    
        public int ID { get; set; }
        public string SERIAL_NUMBER { get; set; }
        public int ADDRESS_ID { get; set; }
        public int METER_TYPE_ID { get; set; }
        public System.DateTime RELEASE_DATE { get; set; }
        public System.DateTime VARIFICATION_DATE { get; set; }
    
        public virtual ADDRESS ADDRESS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<METER_ITEM> METER_ITEM { get; set; }
        public virtual METER_TYPE METER_TYPE { get; set; }
    }
}
