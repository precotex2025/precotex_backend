using ic.backend.precotex.web.Data.Repositories.Implementation.Almacen;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.Almacen;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Almacen
{
    public class TxBultoHiladoService : ITxBultoHiladoService
    {

        private readonly ITxBultoHiladoRepository _txBultoHiladoRepository;

        public TxBultoHiladoService(ITxBultoHiladoRepository txBultoHiladoRepository)
        {
            _txBultoHiladoRepository = txBultoHiladoRepository;
        }

        public async Task<ServiceResponseList<Tx_Bulto_Hilado>?> ListaBultosUbicacion(string sCodProveedor, string sCodOrdProv, string? sNumSemana, string? sNomConera)
        {
            var result = new ServiceResponseList<Tx_Bulto_Hilado>();
            try
            {
                var resultData = await _txBultoHiladoRepository.ListaBultosUbicacion(sCodProveedor, sCodOrdProv, sNumSemana, sNomConera);
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

        public async Task<ServiceResponseList<Lg_Proveedor>?> ListaProveedores()
        {
            var result = new ServiceResponseList<Lg_Proveedor>();
            try
            {
                var resultData = await _txBultoHiladoRepository.ListaProveedores();
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
