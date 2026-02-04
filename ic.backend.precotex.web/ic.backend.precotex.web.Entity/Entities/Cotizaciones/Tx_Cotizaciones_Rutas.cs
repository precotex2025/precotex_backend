using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Cotizaciones
{
    public class Tx_Cotizaciones_Rutas
    {
        public string? Cod_Ruta { get; set; }
        public string? Descripcion  { get; set; }
        public string? Fec_Creacion  { get; set; }
        public int? IdUnico  { get; set; }
        public string? Status  { get; set; }
        public int? id_plantilla_procesos    { get; set; }
        public string? Cod_Usuario  { get; set; }
        public string? Tip_Merma_Tela_Acabada   { get; set; }
        public string? Descripcion_Tipo_Merma  { get; set; }
        public string? COD_GAMA_COTIZACION  { get; set; }
        public string? ObsRiesgo  { get; set; }
        public string? flg_status_ligamentos    { get; set; }
        public string? Cod_Ruta_Tela  { get; set; }
        public string? Des_Ruta     { get; set; }
        public string? TipoAncho { get; set; }
    }
}
