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
    
    public partial class raw
    {
        public int raw_id { get; set; }
        public Nullable<int> location_id { get; set; }
        public Nullable<System.DateTime> collection_date { get; set; }
        public Nullable<decimal> temperature { get; set; }
        public Nullable<decimal> pressure { get; set; }
        public Nullable<decimal> humidity { get; set; }
        public Nullable<decimal> wind_speed { get; set; }
    
        public virtual location location { get; set; }
    }
}