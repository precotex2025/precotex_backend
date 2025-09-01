using ic.backend.precotex.web.Data.Repositories.Almacen;
using ic.backend.precotex.web.Data.Repositories.Implementation.Almacen;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.Almacen;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Almacen
{
    public class TmpVisorPermanenciaTelaCrudaService: ITmpVisorPermanenciaTelaCrudaService
    {
        private readonly ITmpVisorPermanenciaTelaCrudaRepository _tmpVisorPermanenciaTelaCrudaRepository;

        public TmpVisorPermanenciaTelaCrudaService(ITmpVisorPermanenciaTelaCrudaRepository tmpVisorPermanenciaTelaCrudaRepository)
        {
            _tmpVisorPermanenciaTelaCrudaRepository = tmpVisorPermanenciaTelaCrudaRepository;
        }

        public async Task<ServiceResponseList<Tx_Visor_Permanencia_Tela_Cruda>?> ObtieneListaPermanenciaTelaCruda(int? anio)
        {
            var result = new ServiceResponseList<Tx_Visor_Permanencia_Tela_Cruda>();
            try
            {
                var resultData = await _tmpVisorPermanenciaTelaCrudaRepository.ObtieneListaPermanenciaTelaCruda(anio);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                    return result;
                }

                result.Success = true;
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (SqlException sql)
            {
                result.Message = "Error en Servidor: " + sql.Message;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Ocurrio una excepción" + ex.Message;
                return result;
            }
        }
    }
}
