//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserTable()
        {
            this.EmployeeTables = new HashSet<EmployeeTable>();
        }
    
        public int UserID { get; set; }
        public int UserType_ID { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ContactNo { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeTable> EmployeeTables { get; set; }
        public virtual UserTypeTable UserTypeTable { get; set; }
    }
}
