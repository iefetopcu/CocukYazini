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
    
    public partial class usertable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usertable()
        {
            this.comenttables = new HashSet<comenttable>();
            this.contacttables = new HashSet<contacttable>();
            this.posttables = new HashSet<posttable>();
        }
    
        public long id { get; set; }
        public string username { get; set; }
        public string usersurname { get; set; }
        public string usermail { get; set; }
        public string password { get; set; }
        public string userphoto { get; set; }
        public Nullable<int> authority { get; set; }
        public Nullable<int> isaktif { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<comenttable> comenttables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<contacttable> contacttables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<posttable> posttables { get; set; }
    }
}
