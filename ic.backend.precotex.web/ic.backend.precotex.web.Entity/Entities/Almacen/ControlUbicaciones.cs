using System;
using System.Collections.Generic;

namespace ic.backend.precotex.web.Entity.Entities.Almacen
{
    /// <summary>
    /// Resumen de la ubicación ('R'): código, peso total y conteo de bultos
    /// </summary>
    public class ControlUbicacionResumen
    {
        public string? Des_Ubicacion { get; set; }
        public decimal Tot_Peso_Neto { get; set; }
        public int Tot_Bulto { get; set; }
    }

    /// <summary>
    /// Detalle de cada rollo ('D'): rollo, partida, color y peso
    /// </summary>
    public class ControlUbicacionDetalle
    {
        public string? ID_Rollo_X_Anio { get; set; }
        public string? Cod_Ordtra { get; set; }
        public string? Des_Color { get; set; }
        public decimal Peso_Neto { get; set; }
    }

    /// <summary>
    /// Contenedor completo para enviar en una sola llamada al Frontend
    /// </summary>
    public class ControlUbicacionResponse
    {
        public ControlUbicacionResumen? Resumen { get; set; }
        public List<ControlUbicacionDetalle> Detalle { get; set; } = new List<ControlUbicacionDetalle>();
    }

    /// <summary>
    /// Bulto registrado en la sesión temporal para la guía (SP Tmp_Tx_Muestra_Ubicacion_ID_Rollo_X_Anio_Guia 'V')
    /// </summary>
    public class ControlUbicacionGuiaBulto
    {
        public string? ID_Rollo_X_Anio { get; set; }
        public string? Cod_Ordtra { get; set; }
        public decimal Peso_Neto { get; set; }
    }

    /// <summary>
    /// Totales de la guía (SP Tx_Muestra_Detalle_Ubicacion_Guia) y de la lectura temporal (SP Tmp_... 'T')
    /// </summary>
    public class ControlUbicacionGuiaTotales
    {
        public int Tot_Enviados { get; set; }
        public int Tot_Ubicados { get; set; }
        public int Tot_Pendientes { get; set; }
        public int Tot_Lecturados { get; set; }
        public decimal Tot_Peso_Neto { get; set; }
    }

    /// <summary>
    /// Contenedor completo devuelto al Frontend para la pantalla de Ubicar Bultos
    /// </summary>
    public class ControlUbicacionGuiaResponse
    {
        public int Cod { get; set; }
        public string? Msg { get; set; }
        public ControlUbicacionGuiaTotales Totales { get; set; } = new ControlUbicacionGuiaTotales();
        public List<ControlUbicacionGuiaBulto> Bultos { get; set; } = new List<ControlUbicacionGuiaBulto>();
    }

    /// <summary>
    /// Modelo de petición para lecturar bulto en la guía
    /// </summary>
    public class ControlUbicacionLecturaRequest
    {
        public string NumGuia { get; set; } = string.Empty;
        public string IdRollo { get; set; } = string.Empty;
        public string? CodUsuario { get; set; }
    }

    /// <summary>
    /// Modelo de petición para guardar la ubicación definitiva de los bultos de la guía
    /// </summary>
    public class ControlUbicacionGuardarRequest
    {
        public string NumGuia { get; set; } = string.Empty;
        public string DesUbicacion { get; set; } = string.Empty;
        public string? CodUsuario { get; set; }
    }

    // ==========================================
    // MODELOS PARA LA PANTALLA DE REUBICACIÓN
    // ==========================================

    /// <summary>
    /// Bulto registrado en la sesión temporal para Reubicación (SP Tmp_Tx_Muestra_Reubicacion_ID_Rollo_X_Anio)
    /// </summary>
    public class ControlUbicacionReubicacionBulto
    {
        public string? ID_Rollo_X_Anio { get; set; }
        public string? Des_Ubicacion { get; set; }
        public string? Cod_Ordtra { get; set; }
        public decimal Peso_Neto { get; set; }
    }

    /// <summary>
    /// Totales de Reubicación (SP Tmp_Tx_Muestra_Reubicacion_ID_Rollo_X_Anio 'T')
    /// </summary>
    public class ControlUbicacionReubicacionTotales
    {
        public int Tot_Bulto { get; set; }
        public decimal Tot_Peso_Neto { get; set; }
    }

    /// <summary>
    /// Respuesta completa para Reubicación con bultos, totales y último bulto lecturado
    /// </summary>
    public class ControlUbicacionReubicacionResponse
    {
        public int Cod { get; set; }
        public string? Msg { get; set; }
        public ControlUbicacionReubicacionBulto? UltimoBulto { get; set; }
        public ControlUbicacionReubicacionTotales Totales { get; set; } = new ControlUbicacionReubicacionTotales();
        public List<ControlUbicacionReubicacionBulto> Bultos { get; set; } = new List<ControlUbicacionReubicacionBulto>();
    }

    /// <summary>
    /// Petición para lecturar bulto en Reubicación
    /// </summary>
    public class ControlUbicacionReubicacionLecturaRequest
    {
        public string IdRollo { get; set; } = string.Empty;
        public string? CodUsuario { get; set; }
    }

    /// <summary>
    /// Petición para confirmar y guardar la Reubicación (Tx_Man_Reubicacion_ID_Rollo_X_Anio 'I')
    /// </summary>
    public class ControlUbicacionGuardarReubicacionRequest
    {
        public string DesUbicacion { get; set; } = string.Empty;
        public string? CodUsuario { get; set; }
    }

    /// <summary>
    /// Respuesta del guardado de Reubicación
    /// </summary>
    public class ControlUbicacionGuardarReubicacionResponse
    {
        public int Cod { get; set; }
        public string Msg { get; set; } = string.Empty;
    }
}
