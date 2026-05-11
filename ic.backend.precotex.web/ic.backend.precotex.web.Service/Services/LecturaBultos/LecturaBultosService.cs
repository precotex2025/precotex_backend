using ic.backend.precotex.web.Data;
using ic.backend.precotex.web.Entity;
using ic.backend.precotex.web.Service.common;

namespace ic.backend.precotex.web.Service;

public class LecturaBultosService: ILecturaBultosService
{
    private readonly ILecturaBultosRepository _repository;

    public LecturaBultosService(ILecturaBultosRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponseList<Lg_LecturaBultos_Almacenes>?> ListarAlmacenesDisponibles()
    {
        var result = new ServiceResponseList<Lg_LecturaBultos_Almacenes>();
        try
        {
            var resultData = await _repository.ListarAlmacenesDisponibles();
            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
            }
            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = resultData.ToList();
            result.TotalElements = resultData.ToList().Count();
            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponseList<Lg_LecturaBultos>?> ListarMovimientos(string? Cod_Almacen, string? Num_MovStk, string? Fec_MovStk, string? Flg_Pendiente)
    {
        var result = new ServiceResponseList<Lg_LecturaBultos>();
        try
        {
            var resultData = await _repository.ListarMovimientos(Cod_Almacen, Num_MovStk, Fec_MovStk, Flg_Pendiente);
            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
            }
            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = resultData.ToList();
            result.TotalElements = resultData.ToList().Count();
            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponseList<Lg_Bultos>?> ListarBultos(string? Num_MovStk, string? Cod_Almacen)
    {
        var result = new ServiceResponseList<Lg_Bultos>();
        try
        {
            var resultData = await _repository.ListarBultos(Num_MovStk, Cod_Almacen);
            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
            }
            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = resultData.ToList();
            result.TotalElements = resultData.ToList().Count();
            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> LecturarBulto(Lg_Bultos valores)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _repository.LecturarBulto(valores);
                if (resultData.Codigo > 0)
                {
                    result.Success = true;
                    result.Message = resultData.Mensaje;
                    result.CodeTransacc = resultData.Codigo;
                    return result;
                }
                result.Success = false;
                result.CodeTransacc = resultData.Codigo;
                result.Message = resultData.Mensaje;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error inesperado " + ex.Message;
                return result;
            }
        }


}
