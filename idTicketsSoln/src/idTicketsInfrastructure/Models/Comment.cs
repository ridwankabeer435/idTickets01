using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Models
{

    public class Comment
    {

        [Column("id")]
        public int commentId { get; set; }

      
        // comment description (in text)
        // commment may have file attachments
        [Column("text_content")]
        public string? details { get; set; }

     
        [Column("ticket_Id")]
        public int ticketId { get; set; }

   
        [Column("user_Id")]
        public int userId { get; set; }

        [Column("created_at")]
        public DateTime? postingDate { get; set; }


    }
}
