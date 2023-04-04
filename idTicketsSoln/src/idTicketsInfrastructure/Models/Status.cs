using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Models
{
    public enum Status
    {
        [Description("ISSUED")]
        Issued,

        [Description("IN PROGRESS")]
        InProgress,
        
        [Description("IN REVIEW")]
        InReview,

        [Description("RESOLVED")]
        Resolved,

        [Description("ARCHIVED")]
        Archived
    }
}
