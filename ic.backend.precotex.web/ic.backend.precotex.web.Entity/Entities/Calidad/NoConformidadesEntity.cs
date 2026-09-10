using System.Collections.Generic;

namespace ic.backend.precotex.web.Entity.Entities.Calidad
{
    public class NoConformidades
    {
        public string? Numero { get; set; }
        public string? NumeroNC { get; set; }
        public string? Partida { get; set; }
        public string? Fecha { get; set; }
        public string? CodCliente { get; set; }
        public string? Cliente { get; set; }
        public string? CodColor { get; set; }
        public string? Color { get; set; }
        public string? Area { get; set; }
        public string? Usuario { get; set; }
        public string? Responsable { get; set; }
    }

    public class InformeGuardarRequest
    {
        public string Accion { get; set; } = "I";
        public string Num_Informe { get; set; } = "";
        public string Cod_OrdPro { get; set; } = ""; // Partida
        public string Cod_Cli { get; set; } = "";
        public string Nom_Cli { get; set; } = "";
        public string Cod_Color { get; set; } = "";
        public string Color { get; set; } = "";
        public decimal? Kg_Total { get; set; }
        public string Observacion { get; set; } = "";
        public string Cod_Usuario { get; set; } = "";
        public List<ArticuloGuardarDto> Articulos { get; set; } = new();
    }

    public class ArticuloGuardarDto
    {
        public string Accion { get; set; } = "U";
        public string Item { get; set; } = "1";
        public string Cod_Tela { get; set; } = "";
        public string Comb { get; set; } = "";
        public string Cod_Color { get; set; } = "";
        public string Talla { get; set; } = "";
        public decimal Kgs { get; set; }
        public int Rollos { get; set; }
        public int Cant_Rollos_Rech { get; set; }
        public List<DefectoGuardarDto> Defectos { get; set; } = new();
    }

    public class DefectoGuardarDto
    {
        public string Accion { get; set; } = "I";
        public string Item { get; set; } = "1";
        public string Cod_Area { get; set; } = "";
        public string Cod_Motivo { get; set; } = "";
        public string Observacion { get; set; } = "";
    }

    public class ResponseResultado
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public string Num_Informe { get; set; } = "";
    }
}
