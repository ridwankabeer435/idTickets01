using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Models
{
    [Table("Comments")]
    public class Comment
    {
        // id
        [Key]
        [Column("Id")]
        public int commentId { get; set; }

        // ticket number -- this will be a foreign key

        // comment description
        [Column("Details")]
        public string? details { get; set; }

        [ForeignKey("Tickets")]
        [Column("Ticket_Id")]
        public int ticketId { get; set; }
        public Ticket? ticketInfo { get; set; }

        [ForeignKey("Users")]
        [Column("User_Id")]
        public int userId { get; set; }
        public User? userInfo { get; set; }


    }
}
