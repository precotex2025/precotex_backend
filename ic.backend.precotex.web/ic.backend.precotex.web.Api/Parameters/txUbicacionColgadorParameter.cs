namespace ic.backend.precotex.web.Api.Parameters
{
    public class txUbicacionColgadorParameter
    {
        public string Accion { get; set; } = null!;
        public int? Id_Tx_Ubicacion_Colgador { get; set; }
        public int? Id_Tipo_Ubicacion_Colgador { get; set; }
        public int? Id_Tipo_Ubicacion_Colgador_Padre { get; set; }
        public string? CodigoBarra { get; set; }
        public string? Cod_FamTela { get; set; }
        public int Correlativo { get; set; }
        public string? Flg_Estatus { get; set; }
        public string? Cod_Usuario { get; set; }

    }
}
