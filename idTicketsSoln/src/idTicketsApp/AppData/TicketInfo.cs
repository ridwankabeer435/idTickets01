using System.Security.Policy;

namespace idTicketsApp.AppData
{
    public class TicketInfo
    {
        public int ticketId { get; set; }
        public string? ticketTitle { get; set; }
        public string? ticketDescription { get; set; }
        public DateTime ticketCreationDate { get; set; }
        public DateTime? ticketUpdateDate { get; set; }
        
        public long requestorId { get; set; }
        public string? requestorName { get; set; }

        public long? assigneeId { get; set; }
        public string? assigneeName { get; set; }

        // a ticket item may have no comments
        public List<TicketComment>? ticketComment { get; set; }

        
        
    }
}
