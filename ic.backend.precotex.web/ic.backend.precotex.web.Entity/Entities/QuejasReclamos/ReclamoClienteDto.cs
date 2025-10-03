using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ic.backend.precotex.web.Entity.Entities.QuejasReclamos
{
    public class ReclamoClienteDto
    {
        public string? Id { get; set; }
        public string NroCaso { get; set; }
        public string Cliente { get; set; }

        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public string TipoRegistro { get; set; }

        public string UnidadNegocio { get; set; }

        public string UsuarioRegistro { get; set; }

        public string Responsable { get; set; }

        public string MotivoRegistro { get; set; }

        public string EstadoSolicitud { get; set; }

        public string Observacion { get; set; }

        public string NombreArchivo { get; set; } 

        //public IFormFile? archivoAdjunto { get; set; }

        public string archivoAdjunto { get; set; }

        //NUEVOS CAMPOS
        public string? Cod_Cliente_Tex { get; set; }
        public string? Cod_Ordtra { get; set; }
        public string? Cod_Tela { get; set; }
        public string? Cod_Color { get; set; }
        public int? Id_Unidad_NegocioKey { get; set; }
        public string? Cod_Motivo { get; set; }

        public string? Des_Tela { get; set; }
        public string? Des_Color { get; set; }
        public string? Des_Unidad_Negocio { get; set; }

        //Nuevos Campos
        public int? IdArea { get; set; }
        public int? IdResponsable { get; set; }
    }
}
