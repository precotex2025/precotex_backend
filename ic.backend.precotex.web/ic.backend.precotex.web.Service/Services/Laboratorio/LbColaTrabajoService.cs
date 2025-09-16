using ic.backend.precotex.web.Data.Repositories.Implementation.Laboratorio;
using ic.backend.precotex.web.Entity.Entities.Laboratorio;
using ic.backend.precotex.web.Entity.Entities.RetiroRepuestos;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.Laboratorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Laboratorio
{
    public class LbColaTrabajoService: ILbColaTrabajoService
    {
        private readonly ILbColaTrabajoRepository _lbColaTrabajoRepository;
        
        public LbColaTrabajoService(ILbColaTrabajoRepository lbColaTrabajoRepository)
        {
            _lbColaTrabajoRepository = lbColaTrabajoRepository;
        }

        /*
            CABECERA 
        */
        public async Task<ServiceResponseList<Lb_ColTra_Cab>?> ListaSDCPorEstado()
        {
            var result = new ServiceResponseList<Lb_ColTra_Cab>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ListaSDCPorEstado();
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



        /*
            DETALLE 
        */
        public async Task<ServiceResponseList<Lb_ColTra_Det>?> ListaColoresSDC(int Corr_Carta)
        {
            var result = new ServiceResponseList<Lb_ColTra_Det>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ListaColoresSDC(Corr_Carta);
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
}
