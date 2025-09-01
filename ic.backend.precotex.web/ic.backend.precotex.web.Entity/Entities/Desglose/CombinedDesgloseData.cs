using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Desglose
{
    public class CombinedDesgloseData
    {
        public IEnumerable<ListaDesgloseDetalle> LatestDesgloses { get; set; }
        public IEnumerable<E_DesgloseItem> DesgloseItems { get; set; }

        public CombinedDesgloseData()
        {
            LatestDesgloses = new List<ListaDesgloseDetalle>();
            DesgloseItems = new List<E_DesgloseItem>();
        }
    }
}
