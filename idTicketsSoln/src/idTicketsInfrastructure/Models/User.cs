using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Models
{
    [Table("users")]

    public class User
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Column("firstName")]
        public string? firstName { get; set; }


        [Column("lastName")]
        public string? lastName { get; set; }

        [Column("email")]
        public string? email { get; set; } // will need to add email pattern to format email


        [Column("creationDate")]
        public DateTime creationDate { get; set; }


        [Column("updateDate")]
        public DateTime updateDate { get; set; }
        
        [Column("isITStaff")]
        public bool isITStaff { get; set; }

        [Column("isSupervisor")]
        public bool isSupervisor { get; set; }

        [Column("departmentId")]
        public long? departmentId { get; set; }



    }
}
