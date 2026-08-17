namespace ic.backend.precotex.web.Entity.Entities.Laboratorio
{
    public class PartidasVinculadasEntity
    {
        public string Cod_OrdTra { get; set; } = string.Empty;
        public int Num_Secuencia { get; set; }
        public string Tela { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Comb { get; set; } = string.Empty;
        public string Cod_Talla { get; set; } = string.Empty;
        public decimal Kgs_Tenido { get; set; }
        public string Status_Partida { get; set; } = string.Empty;
        public string Cod_GrupoTex { get; set; } = string.Empty;
        public string Des_CompEst { get; set; } = string.Empty;
        public string Nom_Cliente { get; set; } = string.Empty;
        public string Nom_TemCli { get; set; } = string.Empty;
        public string ListaEstCli { get; set; } = string.Empty;
        public string Lista_Cod_OrdPro { get; set; } = string.Empty;
        public string Lista_Primer_ModoProceso { get; set; } = string.Empty;
        public string Obs_Calidad { get; set; } = string.Empty;
        public string Icono1 { get; set; } = string.Empty;
        public string? Des_Motivo_Levantamiento { get; set; }

    }
}
