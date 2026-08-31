using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Tintoreria
{
    public class Ubicaciones
    {
        public class ListaBultoUbicaciones
        {

            public int Num_Corre { get; set; }                // Identificador único
            public string? Cod_Almacen { get; set; }           // Código del almacén
            public string? Cod_Item { get; set; }              // Código interno del ítem
            public string? Producto { get; set; }              // Nombre del producto
            public string? Proveedor { get; set; }             // Nombre del proveedor
            public string? Lote { get; set; }                  // Número de lote
            public decimal Kilos { get; set; }                // Cantidad en kilogramos
            public decimal Peso_Bruto { get; set; }           // Peso con envase
            public decimal Peso_Neto { get; set; }            // Peso sin envase
            public DateTime Fec_Ingreso_Almacen { get; set; } // Fecha de ingreso
            public DateTime Fec_Vencimiento { get; set; }     // Fecha de vencimiento
            public string? Ccodigo_Grupo { get; set; }         // Código de grupo
            public string? Ubicacion { get; set; }             // Ubicación física
        }

        public class InsertarBultoGrupo
        {
            public string? Accion { get; set; }
            public int Id_Bulto_Hilado_Grupo { get; set; }
            public string? Num_Corre { get; set; }
            public string? Cod_Usuario { get; set; }
        }

        public class GrupoCreadoResponseDto
        {
            public int IdAgrupamiento { get; set; }
            public string? CodigoBarraGrupo { get; set; }
        }

        public class ListaAgrupamientosDelDia
        {
            public int Id_Agrupamiento { get; set; }
            public string? Codigo_Barra_Grupo { get; set; }
            public int Cantidad_Bultos { get; set; }
            public int Capacidad_Maxima { get; set; }
            public string? Flg_Status { get; set; }
            public string? Estado_Descripcion { get; set; }
            public string? Cod_Usuario { get; set; }
            public DateTime? Fec_Creacion { get; set; }
        }

        public class ListaDetalleBultosAgrupados
        {
            public int Id_Bulto { get; set; }                  // Identificador del bulto
            public int Num_Corre { get; set; }                 // Correlativo del bulto
            public string? Cod_Item { get; set; }              // Código interno del ítem
            public string? Producto { get; set; }              // Nombre del producto
            public string? Lote { get; set; }                  // Número de lote
            public decimal? Peso_Neto { get; set; }            // Peso sin envase
            public string? Flg_Status { get; set; }            // Estado del bulto
            public string? Codigo_Barra_Grupo { get; set; }    // Código de barra del agrupamiento
            public string? Codigo_Ubicacion { get; set; }      // Ubicación física
            public DateTime? Fecha_Vinculacion { get; set; }   // Fecha de vinculación al grupo
        }

    }
}
