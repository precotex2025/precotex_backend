using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ic.backend.precotex.web.Entity.Entities.Calidad
{
    // ============================================================================
    // ENTIDADES PARA LECTURA (Responses de SPs)
    // ============================================================================

    /// <summary>
    /// Catálogos de Motivos y Áreas (UP_CC_Listar_Datos_Informe_Calidad)
    /// </summary>
    public class DatoInformeCalidadEntity
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
    }

    /// <summary>
    /// Cabecera de bandeja principal (UP_CC_Muestra_Informe_Calidad_Cabecera)
    /// </summary>
    public class InformeCalidadCabeceraEntity
    {
        public string Num_Informe { get; set; }
        public string Fecha { get; set; }
        public string Partida { get; set; }
        public string Nom_Cli { get; set; }
        public string Color { get; set; }
        public string Flg_Status { get; set; }
        public string Des_Estado { get; set; }
        public string Cod_Usuario { get; set; }
        public string Observacion { get; set; }
        public decimal? Kg_Total { get; set; }
    }

    /// <summary>
    /// Datos generales de Partida (UP_CC_Mostrar_Partida)
    /// </summary>
    public class PartidaCalidadEntity
    {
        public string Partida { get; set; }
        public string Cod_Cli { get; set; }
        public string Nom_Cli { get; set; }
        public string Cod_Color { get; set; }
        public string Color { get; set; }
        public decimal? Kg_Total { get; set; }
        public string Fecha { get; set; }
    }

    /// <summary>
    /// Artículos por Partida / Informe (UP_CC_Muestra_Informe_Calidad_Detalle)
    /// </summary>
    public class InformeCalidadDetalleEntity
    {
        public string Num_Informe { get; set; }
        public int Item { get; set; }
        public string Cod_Tela { get; set; }
        public string Nom_Tela { get; set; }
        public string Talla { get; set; }
        public int Cant_Rollos_Asig { get; set; }
        public int Cant_Rollos_Rech { get; set; }
        public decimal Kg_Afectados { get; set; }
        public decimal? Kg_Crudo { get; set; }
    }

    /// <summary>
    /// Defectos y Áreas por Artículo (UP_CC_Muestra_Informe_Calidad_Detalle_Motivo)
    /// </summary>
    public class InformeCalidadMotivoEntity
    {
        public string Num_Informe { get; set; }
        public int Item { get; set; }
        public int Item_Motivo { get; set; }
        public string Cod_Motivo { get; set; }
        public string Nom_Motivo { get; set; }
        public string Cod_Area { get; set; }
        public string Nom_Area { get; set; }
        public string Observacion { get; set; }
        public string Foto_Url { get; set; }
    }

    // ============================================================================
    // MODELOS PARA ESCRITURA (Request para Guardar/Editar/Anular)
    // ============================================================================

    public class InformeGuardarRequest
    {
        [Required(ErrorMessage = "La acción es requerida (I, U, D)")]
        [StringLength(1)]
        public string Accion { get; set; } // 'I' = Insertar, 'U' = Modificar, 'D' = Anular

        public string Num_Informe { get; set; }

        [Required(ErrorMessage = "La partida es obligatoria")]
        public string Cod_OrdPro { get; set; }

        public string Cod_Cli { get; set; }
        public string Nom_Cli { get; set; }
        public string Cod_Color { get; set; }
        public string Color { get; set; }
        public decimal? Kg_Total { get; set; }
        public string Observacion { get; set; }
        public string Motivo_Anulacion { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string Cod_Usuario { get; set; }

        public List<ArticuloGuardarRequest> Articulos { get; set; } = new List<ArticuloGuardarRequest>();
    }

    public class ArticuloGuardarRequest
    {
        public string Accion { get; set; } = "I";
        public int Item { get; set; }
        public string Cod_Tela { get; set; }
        public string Nom_Tela { get; set; }
        public string Talla { get; set; }
        public int Cant_Rollos_Asig { get; set; }
        public int Cant_Rollos_Rech { get; set; }
        public decimal Kg_Afectados { get; set; }

        public List<MotivoGuardarRequest> Defectos { get; set; } = new List<MotivoGuardarRequest>();
    }

    public class MotivoGuardarRequest
    {
        public string Accion { get; set; } = "I";
        public int Item_Motivo { get; set; }
        public string Cod_Motivo { get; set; }
        public string Nom_Motivo { get; set; }
        public string Cod_Area { get; set; }
        public string Nom_Area { get; set; }
        public string Observacion_Defecto { get; set; }
        public string Foto_Base64 { get; set; }
    }

    // ============================================================================
    // RESPUESTAS GENERALES
    // ============================================================================

    public class ResponseResultado
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Num_Informe { get; set; }
        public object Data { get; set; }
    }
}
