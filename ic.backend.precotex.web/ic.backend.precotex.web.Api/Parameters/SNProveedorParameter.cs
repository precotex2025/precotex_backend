namespace ic.backend.precotex.web.Api.Parameters
{
    public class SNProveedorParameter
    {
        public string? Accion { get; set; } // 'I', 'U', 'D', 'R' (Reactivar)
        public int Id_Proveedor { get; set; }
        public string? Ruc { get; set; }
        public string? Razon_Social { get; set; }
        public string? Bien_Servicio { get; set; }
        public string? Tipo_Proveedor { get; set; }
        public string? Proceso { get; set; }
        public string? Ids_Areas { get; set; } // Ej: '1,4,7' o '1|Carlos Chamaya,4|Edith Vásquez'
        public string? Contacto_Nombre { get; set; }
        public string? Contacto_Telefono { get; set; }
        public string? Contacto_Correo { get; set; }
        public bool Flg_Homologado { get; set; }
        public string? Cod_Usuario { get; set; }
    }
}
