namespace ic.backend.precotex.web.Api.Parameters.Administracion.AccesoUsuario
{
    public class RegistrarUsuarioLabRequest
    {
        public string? Accion {  get; set; }
        public string? Cod_Usuario { get; set; }
        public string? Nom_Usuario { get; set; }
        public string? Password { get; set; }
        public string? Tip_Trabajador { get; set; }
        public string? Cod_Trabajador { get; set; }
        public string? Acc_Cod { get; set; }
    }
}
