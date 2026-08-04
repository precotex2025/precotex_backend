using System;

namespace ic.backend.precotex.web.Api.Parameters
{
    public class SNObjetivoMedicionParameter
    {
        public string? Accion { get; set; }
        public int? Id_Obj_Medicion { get; set; }
        public int? Id_Objetivo { get; set; }
        public string? Codigo_Objetivo { get; set; }
        public string? Periodo { get; set; }
        public decimal? Valor { get; set; }
        public string? Usuario_Registro { get; set; }
    }
}
