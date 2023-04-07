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

        [Key]
        [Column("id")]
        public int id { get; set; }

        // comment description (in text)
        // commment may have file attachments
        [Column("textContent")]
        public string? textContent { get; set; }

     
        [Column("ticketId")]
        public int ticketId { get; set; }

   
        [Column("userId")]
        public int userId { get; set; }

        [Column("creationDate")]
        public DateTime? creationDate { get; set; }


    }
}
