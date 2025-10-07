namespace ic.backend.precotex.web.Api.Parameters
{
    public class Tx_ReporteNCParameter
    {
        public int? Rep_Id { get; set; }
        public string? Rep_FecObs { get; set; }
        public string? Rep_HorObs { get; set; }
        public string? Cod_Planta_Tg { get; set; }
        public int? Are_Id { get; set; }
        public string? Rep_Esp { get; set; }
        public string? Rep_Clas { get; set; }
        public string? Rep_DesNC { get; set; }
        public int? Rep_NivRgo { get; set; }
        public string? Rep_AccCor { get; set; }
        public int? Resp_Id { get; set; }
        public string? Rep_RepPor { get; set; }
        public string? Rep_Est { get; set; }
        public string? Rep_FecSub { get; set; }
        public string? Rep_Aceptado { get; set; }
        public string? Rep_HorSub { get; set; }
        public string? Rep_AccCor_Tom { get; set; }
        public string? Rep_DetObs { get; set; }
        public string? Rep_Resp_Levantamiento { get; set; }

        //VARIABLES PARA MOSTRAR DESCRIPCION EN LOS SELECTS
        public string? Est_Des { get; set; }
        public string? Niv_Rgo_Des { get; set; }
        public string? Responsable { get; set; }
        public string? Are_Des { get; set; }
    }
}
