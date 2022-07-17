using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Models
{
    [Table("Users")]
    public class User
    {

        [Key]
        [Column("Id")]
        public int userId { get; set; }

        [Column("First_Name")]
        public string? firstName { get; set; }

        [Column("Middle_Name")]
        public string? MiddleName { get; set; }
        
        [Column("Last_Name")]
        public string? lastName { get; set; }

        [Column("Email")]
        public string? email { get; set; } // will need to add email pattern to format email


        [Column("Username")]
        public string? username { get; set; } // a custom username based on 'formal' names and random number

        [Column("Password")]
        public string? password { get; set; } // apply rules based on regex pattern

        [Column("Phone_Number")]
        public string? phoneNumber { get; set; }
        
        [Column("EmpNumber")]
        public string? userNumber { get; set; }

        [Column("CurrentEmployee")]
        public bool currentEmployee { get; set; }

        [Column("Role")]
        public string? userRole { get; set; }

        [Column("Last_updated")]
        public DateTime? lastUpdated { get; set; }
    }
}
