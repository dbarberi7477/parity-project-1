//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeatherAggregator.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class location
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public location()
        {
            this.raw = new HashSet<raw>();
        }
    
        public int location_id { get; set; }
        public Nullable<int> region_id { get; set; }
        public string description { get; set; }
        public Nullable<decimal> lat { get; set; }
        public Nullable<decimal> lng { get; set; }
    
        public virtual region region { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<raw> raw { get; set; }
    }
}