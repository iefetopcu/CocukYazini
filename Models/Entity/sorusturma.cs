//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CocukYazini.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class sorusturma
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sorusturma()
        {
            this.posttables = new HashSet<posttable>();
        }
    
        public long id { get; set; }
        public string sorusturmaname { get; set; }
        public Nullable<System.DateTime> datetime { get; set; }
        public string sorusturmaphotourl { get; set; }
        public Nullable<long> isaktif { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<posttable> posttables { get; set; }
    }
}