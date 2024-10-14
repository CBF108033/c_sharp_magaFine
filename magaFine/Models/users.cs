using System;

namespace magaFine.Models
{
    public partial class users
    {
        public int Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; } = "custom";
        public Nullable<System.DateTime> createdAt { get; set; }
        public Nullable<System.DateTime> updatedAt { get; set; }
    }
}
