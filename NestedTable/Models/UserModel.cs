using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NestedTable.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public int UpdatedBy { get; set; }
        public string ContactNumber { get; set; }
        public string EmailID { get; set; }
    }
}