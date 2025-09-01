namespace ic.backend.precotex.web.Api.Parameters
{
    public class TxUbicacionColgadorItemsParameter
    {
        public int? Id_Tx_Ubicacion_Colgador_Items { get; set; }
        public int? Id_Tx_Ubicacion_Colgador { get; set; }
        public int? Id_Tx_Colgador_Registro_Cab { get; set; }
        public string? Accion { get; set; } = null!;
        public string? CodigoBarra { get; set; } = null!;
        public string? Flg_Estatus { get; set; }
        public string? Cod_Usuario { get; set; }
        public int? Id_Tx_Ubicacion_Fisica { get; set; }
    }
}
