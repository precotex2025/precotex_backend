namespace ic.backend.precotex.web.Api.Parameters.Laboratorio
{
    public class ValidarCorridaDuplicadaRequest
    {
        public string? CorrCarta { get; set; }
        public int Sec { get; set; }
        public int Correlativo { get; set; }
        public string? Tip_Receta { get; set; }
        public string? Usr_Cod { get; set; }
    }
}
