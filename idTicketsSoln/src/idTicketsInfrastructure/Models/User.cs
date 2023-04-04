using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Models
{

    public class User
    {

        [Column("id")]
        public int userId { get; set; }

        [Column("first_name")]
        public string? firstName { get; set; }


        [Column("last_name")]
        public string? lastName { get; set; }

        [Column("email")]
        public string? email { get; set; } // will need to add email pattern to format email


        [Column("created_at")]
        public DateTime dateCreated { get; set; }


        [Column("updated_at")]
        public DateTime dateUpdated { get; set; }
        
        [Column("is_it")]
        public bool isITPersonnel { get; set; }

        [Column("is_supervisor")]
        public bool isSupervisor { get; set; }

        [Column("department_id")]
        public long departmentId { get; set; }


    }
}
