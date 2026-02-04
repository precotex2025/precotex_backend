namespace ic.backend.precotex.web.Api.Parameters
{
    public class ProcesoCerrarReclamoParameter
    {
        public string? NroCaso { get; set; }
        public string? Cod_Tipo_Consecuencia { get; set; }
        public string? Cod_SubTipo_Devolucion { get; set; }
        public string? Flg_NotaCredito { get; set; }
        public string? Flg_FleteAereo { get; set; }
        public string? Observacion_Comercial_Cierre { get; set; }
        public string? Cod_Usuario { get; set; }


        /*
            @NroCaso						VARCHAR(100)	,
	        @Cod_Tipo_Consecuencia			CHAR(2)	,
	        @Cod_SubTipo_Devolucion			CHAR(2)	,
	        @Flg_NotaCredito				CHAR(1)	,
	        @Observacion_Comercial_Cierre	VARCHAR(MAX), 
         */

    }
}
