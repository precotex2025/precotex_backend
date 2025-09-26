using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Memorandum
{
    public class Tx_Memorandum_Detalle_Exportacion
    {
        public string? Num_Memo { get; set; }
        public DateTime? Fecha_Emisor { get; set; }
        public string? Emisor { get; set; }
        public string? Planta_Origen { get; set; }
        public string? Receptor { get; set; }
        public string? Planta_Destino { get; set; }
        public string? Descripcion_Tipo_Memorandum { get; set; }
        public string? Descripcion_Motivo_Memorandum { get; set; }
        public DateTime? Fch_Recepcion { get; set; }
        public string? Descripcion_Estado_Memo { get; set; }
        public string? Glosa { get; set; }
        public int? Cantidad { get; set; }
        public DateTime? Fch_Listo { get; set; }
        public DateTime? Fch_Confirma_Garita1 { get; set; }
        public DateTime? Fch_Confirma_Garita2 { get; set; }
        public string? Revertir { get; set; }
    }
}
