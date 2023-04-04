using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Models
{
    public enum Priority
    {
        [Description("VERY LOW")]
        Very_Low,

        [Description("LOW")]
        Low,

        [Description("MEDIUM")]
        Medium,

        [Description("HIGH")]
        High,

        [Description("VERY HIGH")]
        Very_High


    }
}
