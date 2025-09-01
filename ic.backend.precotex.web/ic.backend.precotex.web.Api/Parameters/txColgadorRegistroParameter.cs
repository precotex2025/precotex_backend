using ic.backend.precotex.web.Entity.Entities;

namespace ic.backend.precotex.web.Api.Parameters
{
    public class txColgadorRegistroParameter
    {
        public int?  Id_Tx_Colgador_Registro_Cab { get; set; }
        public string? Cod_Tela { get; set; }
        public string? Cod_OrdTra { get; set; }
        public string? Cod_Ruta { get; set; }
        public string? Cod_Cliente_Tex { get; set; }
        public string? Fabric { get; set; }
        public string? Yarn { get; set; }
        public string? Composicion { get; set; }
        public string? Flg_Estatus { get; set; }
        public string? Usu_Registro { get; set; }
        public string? Accion { get; set; }
        public List<Tx_Colgador_Registro_Det>? Detalle { get; set; }


    }
}
