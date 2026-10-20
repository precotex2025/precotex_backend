using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Usuario
    {
        public int Id_Usuario { get; set; }
        public string? Cod_Usuario { get; set; }
        public string? Password { get; set; }
        public string? Nom_Usuario { get; set; }
        public int? Cod_Rol { get; set; }
        public string? Des_Rol { get; set; }
        public string? Cod_Empresa { get; set; }
        public string? Empresa { get; set; }
        public string? Tip_Trabajador { get; set; }
        public string? Cod_Trabajador { get; set; }
        public string? Email { get; set; }
        public string? Denominacion { get; set; }
        public string? Codigo_Proceso { get; set; }
        public string? Codigo_Nivel { get; set; }
        public bool? Flg_Activo { get; set; }
        public string? Estado { get; set; }
        public bool? Primer_Ingreso { get; set; }
        public DateTime? Fec_Creacion { get; set; }
        public string? Cod_Usuario_Creacion { get; set; }
        public string? Accion { get; set; }

        // Propiedades de Puesto y Listado integrado
        public string? Id { get; set; }
        public string? Codigo_Puesto { get; set; }
        public string? Puesto { get; set; }
        public string? Proceso { get; set; }
        public string? Proceso_Nombre { get; set; }
        public string? Usuario { get; set; }
        public string? UserCode { get; set; }
        public string? Nivel { get; set; }
        public string? Nivel_Descripcion { get; set; }
        public string? Permisos { get; set; }
        public string? Fecha_Registro { get; set; }
        public DateTime? Fecha_Registro_Puesto { get; set; }
    }
}