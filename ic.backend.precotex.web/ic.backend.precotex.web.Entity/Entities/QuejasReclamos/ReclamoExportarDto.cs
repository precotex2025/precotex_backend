using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.QuejasReclamos
{
    public class ReclamoExportarDto
    {
        public int? Id { get; set; }
        public string? NroCaso { get; set; }
        public string? FechaRegistro { get; set; }
        public string? NombreUnidadNegocio { get; set; }
        public string? Cod_Ordtra { get; set; }
        public string? Cliente { get; set; }
        public string? Cod_Tela { get; set; }
        public string? Des_Tela { get; set; }
        public string? Cod_Color { get; set; }
        public string? Des_Color { get; set; }
        public decimal? Kgr_Asignados { get; set; }
        public decimal? Nro_Rollos_Despachados { get; set; }
        public string? Orden_Compra { get; set; }
        public string? Area_Responsable { get; set; }
        public string? Usuario_Responsable { get; set; }
        public string? MotivoReclamo { get; set; }
        public string? Observacion { get; set; }
        public string? Estado { get; set; }
        public string? Consecuencia_Principal { get; set; }
        public string? Tipo_Devolucion { get; set; }
        public string? Nota_Credito { get; set; }
    }
}
