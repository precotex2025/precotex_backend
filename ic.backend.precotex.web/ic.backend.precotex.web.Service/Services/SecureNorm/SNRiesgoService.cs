using ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Entity.Entities.SecureNorm.Parameters;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.SecureNorm
{
    public class SNRiesgoService : ISNRiesgoService
    {
        private readonly ISNRiesgoRepository _repository;

        public SNRiesgoService(ISNRiesgoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponseList<SN_Riesgo>?> GetListadoRiesgos(string sFiltro)
        {
            var response = new ServiceResponseList<SN_Riesgo>();
            try
            {
                var result = await _repository.Listado(sFiltro);
                if (result != null)
                {
                    response.Success = true;
                    response.CodeResult = 200;
                    response.Elements = result;
                    response.TotalElements = ((List<SN_Riesgo>)result).Count;
                }
                else
                {
                    response.Success = false;
                    response.CodeResult = 404;
                    response.Message = "No se encontraron riesgos declarados.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.CodeResult = 500;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<int>> PostProcesoMntoRiesgo(SNRiesgoParameter request)
        {
            var response = new ServiceResponse<int>();
            try
            {
                var riesgo = new SN_Riesgo
                {
                    Codigo = request.Codigo,
                    Tipo = request.Tipo,
                    Descripcion_Breve = request.Descripcion_Breve,
                    Proceso = request.Proceso,
                    Nivel = request.Nivel,
                    Estado = request.Estado,
                    Responsable = request.Responsable,
                    Fecha_Revision = request.Fecha_Revision,
                    Medida_Control = request.Medida_Control,
                    Usuario_Registro = request.Usuario_Registro ?? "SISTEMAS"
                };

                var (codigo, mensaje) = await _repository.Mnto(riesgo, request.Accion ?? "I");
                response.Success = codigo == 1;
                response.CodeResult = codigo == 1 ? 200 : 400;
                response.Message = mensaje;
                response.CodeTransacc = codigo;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.CodeResult = 500;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
