using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Models
{

    [Table("tickets")]
    public class Ticket
    {
        // many users can have one ticket

        [Key]
        [Column("id")]
        public int id { get; set; }


        [Column("title")]
        public string? title { get; set; }


        [Column("details")]
        public string? details{ get; set; }


        [Column("requestorId")]
        public long requestorId { get; set; }


        [Column("assigneeId")]
        public long assigneeId { get; set; }


        [Column("status")]
        public string? status { get; set; }

        [Column("priority")]
        public string? priority { get; set; }


        [Column("creationDate")]
        public DateTime? creationDate { get; set; }


        [Column("updateDate")]
        public DateTime? updateDate { get; set; }
    }
}
