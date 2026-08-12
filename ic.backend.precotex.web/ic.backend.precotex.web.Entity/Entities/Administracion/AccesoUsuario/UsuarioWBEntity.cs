namespace ic.backend.precotex.web.Entity.Entities.Administracion.AccesoUsuario
{
    public class UsuarioWBEntity
    {
        public string CodUsuario { get; set; } = string.Empty;
        public string NomUsuario { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int? FlgActivo { get; set; }
        public string TipTrabajador { get; set; } = string.Empty;
        public string CodTrabajador { get; set; } = string.Empty;
        public string AccCod { get; set; } = string.Empty;
    }
}
