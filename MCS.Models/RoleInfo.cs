using Dapper;
using System;

namespace MCS.Models {

    [Table("tbl_Roles")]
    public class RoleInfo {
        public long Id { get; set; }
        public string Name { get; set; }        
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

}
