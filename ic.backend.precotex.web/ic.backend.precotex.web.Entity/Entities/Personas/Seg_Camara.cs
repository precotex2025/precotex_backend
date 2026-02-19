using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Personas
{
    public class Seg_Camara
    {
        public int? Cam_Mar_Id { get; set; }
        public int? Cam_Id { get; set; }
        public int? Cam_Cod_Cam { get; set; }
        public string? Cam_Mar_Cod_Usr { get; set; }
        public string? Cam_Mar_Fec { get; set; }
        public string? TipoMarcacion { get; set; }
        public string? Flg_Cargado { get; set; }
        public string? Foto_Ruta { get; set; }
        public string? FotoBase64 { get; set; }
        public string? Nombre { get; set; }
    }
}
