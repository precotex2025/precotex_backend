namespace ic.backend.precotex.web.Api.Parameters
{
    public class TmSolicitudMantenimientoParameter
    {
        public string Accion { get; set; } = null!;
        public string? Cod_Solicitud { get; set; }
        public string? Cod_Estado_Mant { get; set; }
        public string? Cod_Area { get; set; }
        public string? Cod_Maquina { get; set; }
        public string? Observacion { get; set; }
        public string? Prioridad { get; set; }
        public string? Paro_Maquina { get; set; }
        public string? Ruta_Fotografia { get; set; }
        public string? Hora_Inicio { get; set; }
        public string? Hora_Fin { get; set; }
        public string? Usu_Registro { get; set; }

    }
}
