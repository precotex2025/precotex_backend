using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Cotizaciones
{
    public class Tx_Cotizaciones_Rutas_Detalle
    {
        public string? Cod_Ruta_Tela { get; set; }
        public string? Cod_Ruta    { get; set; }
        public int? Secuencia { get; set; }
        public int? Cod_Proceso_Tex { get; set; }
        public string? Descripcion { get; set; }
        public string? Orden   { get; set; }
        public string? Receta { get; set; }
        public string? Cod_RecetaAcabado   { get; set; }
        public string? Des_Condicion { get; set; }
        public string? Cod_Maquina_Tinto   { get; set; }
        public string? Flg_Subproceso { get; set; }
        public string? Flg_Activo{ get; set; }
    }
}
