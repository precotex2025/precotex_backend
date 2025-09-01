using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class PrintTicketContent
    {
        public string Version { get; set; }
        public string Content { get; set; }
        public string PrintName { get; set; }
        public int? CountPrint { get; set; }
        public Tx_TelaEstructuraColgador tx_TelaEstructuraColgador { get; set; }

    }
}
