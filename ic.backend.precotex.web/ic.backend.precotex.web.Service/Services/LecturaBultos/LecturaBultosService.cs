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
}
