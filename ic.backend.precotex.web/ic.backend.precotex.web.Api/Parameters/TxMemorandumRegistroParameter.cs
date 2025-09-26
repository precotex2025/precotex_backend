using ic.backend.precotex.web.Entity.Entities.Memorandum;

namespace ic.backend.precotex.web.Api.Parameters
{
    public class TxMemorandumRegistroParameter
    {
        public string? Num_Memo { get; set; }
        public string? Cod_Usuario_Emisor { get; set; }
        public string? Cod_Usuario_Receptor { get; set; }
        public string? Num_Planta_Origen { get; set; }
        public string? Num_Planta_Destino { get; set; }
        public string? Cod_Usuario_Seguridad_Emisor { get; set; }
        public string? Cod_Usuario_Seguridad_Receptor { get; set; }
        public string? Cod_Tipo_Memo { get; set; }
        public string? Cod_Motivo_Memo { get; set; }
        public string? Accion { get; set; }
        public List<Tx_Memorandum_Detalle>? Detalle { get; set; }

        //NUEVOS CAMPOS
        public string? Cod_Tipo_Movimiento { get; set; }
        public string? Datos_Externo { get; set; }
        public string? Direccion_Externo { get; set; }
    }

    public class Tx_Memorandum_Det
    {
        public int Id_Memorandum_Detalle { get; set; }
        public string? Num_Memo { get; set; }
        public string? Glosa { get; set; }
        public int? Cantidad { get; set; }
        public string? Flg_Estatus { get; set; }
        public string? Usu_Registro { get; set; }
    }
}
