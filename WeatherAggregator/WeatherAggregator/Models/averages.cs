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
    
    public partial class averages
    {
        public int averages_id { get; set; }
        public Nullable<int> region_id { get; set; }
        public Nullable<decimal> temperature { get; set; }
        public Nullable<decimal> pressure { get; set; }
        public Nullable<decimal> humidity { get; set; }
        public Nullable<decimal> wind_speed { get; set; }
    
        public virtual region region { get; set; }
    }
}
