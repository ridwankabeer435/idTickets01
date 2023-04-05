using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Models
{
  
    public class Ticket
    {
        // many users can have one ticket
       
        [Column("id")]
        public int ticketId { get; set; }


        [Column("title")]
        public string? title { get; set; }


        [Column("description")]
        public string? details{ get; set; }


        [Column("requestor_id")]
        public long requestorId { get; set; }


        [Column("assignee_id")]
        public long assigneeId { get; set; }


        [Column("status")]
        public Status ticketStatus { get; set; }

        [Column("priority")]
        public Priority ticketPriority { get; set; }


        [Column("created_at")]
        public DateTime? ticketIssueDate { get; set; }


        [Column("updated_at")]
        public DateTime? ticketUpdateDate { get; set; }
    }
}
