using ic.backend.precotex.web.Data.Repositories.Cotizaciones;
using ic.backend.precotex.web.Data.Repositories.Implementation.Cotizaciones;
using ic.backend.precotex.web.Entity.Entities.Cotizaciones;
using ic.backend.precotex.web.Entity.Entities.Laboratorio;
using ic.backend.precotex.web.Entity.Entities.QuejasReclamos;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.Cotizaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Cotizaciones
{
    public class TxCotizacionesService: ITxCotizacionesService
    {
        private readonly ITxCotizacionesRepository _txCotizacionesRepository;

        public TxCotizacionesService(ITxCotizacionesRepository txCotizacionesRepository)
        {
            _txCotizacionesRepository = txCotizacionesRepository;
        }

        public async Task<ServiceResponseList<Tx_Cotizaciones>?> ListarProcesosExportacion(string Pro_Cen_Cos)
        {
            var result = new ServiceResponseList<Tx_Cotizaciones>();
            try
            {
                var resultData = await _txCotizacionesRepository.ListarProcesosExportacion(Pro_Cen_Cos);
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

        //LISTAR RUTAS POR COD TELA
        public async Task<ServiceResponseList<Tx_Cotizaciones_Rutas>?> RutaXCodTela(string Cod_Tela) 
        {
            var result = new ServiceResponseList<Tx_Cotizaciones_Rutas>();
            try
            {
                var resultData = await _txCotizacionesRepository.RutaXCodTela(Cod_Tela);
                if(resultData == null || !resultData.Any())
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

        //LISTAR PROCESOS POR RUTA
        public async Task<ServiceResponseList<Tx_Cotizaciones_Rutas_Detalle>?> RutaXCodTelaDetalle(string Cod_Tela, string Cod_Ruta) 
        {
            var result = new ServiceResponseList<Tx_Cotizaciones_Rutas_Detalle>();
            try
            {
                var resultData = await _txCotizacionesRepository.RutaXCodTelaDetalle(Cod_Tela, Cod_Ruta);
                if(resultData == null || !resultData.Any())
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

        public async Task<ServiceResponseList<Tx_Cotizaciones_Telas>?> ListaTelas(string Cod_Tela)
        {
            var result = new ServiceResponseList<Tx_Cotizaciones_Telas>();
            try
            {
                var resultData = await _txCotizacionesRepository.ListaTelas(Cod_Tela);
                if(resultData == null || !resultData.Any())
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
