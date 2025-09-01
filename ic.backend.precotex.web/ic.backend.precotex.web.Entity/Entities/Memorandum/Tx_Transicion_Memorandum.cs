using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ic.backend.precotex.web.Entity.Entities.Memorandum
{
    public class Tx_Transicion_Memorandum
    {

        public string? Cod_Estado_Memo_Actual { get; set; }
        public string? Cod_Estado_Memo_Siguiente { get; set; }
        public string? Descripcion { get; set; }
        public int? bEditar { get; set; }
        public int? bAvanzar { get; set; }
        
        
      
    }
}
