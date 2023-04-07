using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Models
{
    public static class Status
    {      
        public const string ISSUED = "ISSUED";
        public const string IN_PROGRESS = "IN PROGRESS";
        public const string IN_REVIEW = "IN REVIEW";
        public const string RESOLVED = "RESOLVED";
        public const string ARCHIVED = "ARCHIVED";
    }
}
