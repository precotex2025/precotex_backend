namespace ic.backend.precotex.web.Api.Security
{
    public interface IJwtTokenService
    {
        string GenerateToken(string codUsuario, string? codRol);
    }
}
