using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Models
{
    [Table("Tickets")]
    public class Ticket
    {
        // many users can have one ticket
        [Key]
        [Column("Id")]
        public int ticketId { get; set; }
        
        [Column("Number")]
        public int ticketNumber { get; set; } // this shall be generated in a later method

        [Column("Description")]
        public string? description { get; set; }

        [Column("Related_Tickets")]
        public List<int>? relatedTickets { get; set; } // really this should be stringified 

        [ForeignKey("Users")]
        [Column("Issuer_Id")]
        public int issuerId { get; set; }

        public User? issuerDetails { get; set; }

        [Column("Assignee_Id")]
        public int assigneeId { get; set; }
        public User? assigneeDetails { get; set; }

        [Column("Status")]
        public string? ticketStatus { get; set; }

        [Column("Priority")]
        public string? ticketPriority { get; set; }

        [Column("Blocked")]
        public bool isBlocked { get ; set; }

        [Column("Issue_Date")]
        public DateTime? ticketIssueDate { get; set; }

        [Column("Update_Date")]
        public DateTime? ticketUpdateDate { get; set; }
    }
}
