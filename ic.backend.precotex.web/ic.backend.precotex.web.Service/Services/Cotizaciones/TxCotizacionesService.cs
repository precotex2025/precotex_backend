using ic.backend.precotex.web.Data.Repositories.Cotizaciones;
using ic.backend.precotex.web.Data.Repositories.Implementation.Cotizaciones;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.Cotizaciones;
using ic.backend.precotex.web.Entity.Entities.Laboratorio;
using ic.backend.precotex.web.Entity.Entities.Memorandum;
using ic.backend.precotex.web.Entity.Entities.QuejasReclamos;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.Cotizaciones;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public async Task<ServiceResponseList<Tx_Cotizaciones>?> ListarProcesosExportacion(int Pro_Cen_Cos, string Tipo, string Cod_Cliente_Tex, string Cod_Tela, string Cod_Ruta, string? Cod_Color)
        {
            var result = new ServiceResponseList<Tx_Cotizaciones>();
            try
            {
                var resultData = await _txCotizacionesRepository.ListarProcesosExportacion(Pro_Cen_Cos, Tipo, Cod_Cliente_Tex, Cod_Tela, Cod_Ruta, Cod_Color);
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

        public async Task<ServiceResponseList<Tx_Cotizaciones>?> ListarProcesosExportacionFooter(int Pro_Cen_Cos)
        {
            var result = new ServiceResponseList<Tx_Cotizaciones>();
            try
            {
                var resultData = await _txCotizacionesRepository.ListarProcesosExportacionFooter(Pro_Cen_Cos);
                if(result == null || !resultData.Any())
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
                result.Message = "Excepcion nocontrolada" + ex.Message;
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

        public async Task<ServiceResponseList<Tx_Cotizaciones_Centro_Costo>?> ListaCentroCosto()
        {
            var result = new ServiceResponseList<Tx_Cotizaciones_Centro_Costo>();
            try
            {
                var resultData = await _txCotizacionesRepository.ListaCentroCosto();
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

        public async Task<ServiceResponse<int>> ProcesoCotizacion(Tx_Cotizaciones_Cab tx_Cotizaciones_Cab, List<Tx_Cotizaciones_Det> detalle, string sTipoTransac)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txCotizacionesRepository.ProcesoCotizacion(tx_Cotizaciones_Cab, detalle, sTipoTransac);
                if (resultData.Codigo > 0)
                {
                    result.Message = resultData.Mensaje;
                    result.Success = true;
                    result.CodeTransacc = resultData.Codigo;

                    return result;
                }

                result.Message = resultData.Mensaje;
                result.Success = false;
                return result;

            }
            catch (SqlException sql)
            {
                result.Message = "Error en Servidor: " + sql.Message;
                result.Success = false;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Ocurrio una excepción" + ex.Message;
                result.Success = false;
                return result;
            }
        }

        public async Task<ServiceResponseList<ComboGral>?> ValidaColorExiste(string Cod_Color)
        {
            var result = new ServiceResponseList<ComboGral>();
            try
            {
                var resultData = await _txCotizacionesRepository.ValidaColorExiste(Cod_Color);
                if (result == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información, para el Codigo de color {" + Cod_Color + "}, en consulta";
                    return result;
                }
                result.Success = true;
                result.Message = "Completado con éxito";
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Excepcion nocontrolada" + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponseList<ComboGral>?> ListaUnidadNegocio()
        {

            var result = new ServiceResponseList<ComboGral>();
            try
            {
                var resultData = await _txCotizacionesRepository.ListaUnidadNegocio();
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

        public async Task<ServiceResponseList<ComboGral>?> ListaIntensidad(int Id_Unidad_NegocioKey)
        {
            var result = new ServiceResponseList<ComboGral>();
            try
            {
                var resultData = await _txCotizacionesRepository.ListaIntensidad(Id_Unidad_NegocioKey);
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

        public async Task<ServiceResponseList<Tx_HilosTel>?> ListaHiladoxTela(string Cod_Tela)
        {
            var result = new ServiceResponseList<Tx_HilosTel>();
            try
            {
                var resultData = await _txCotizacionesRepository.ListaHiladoxTela(Cod_Tela);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                    result.Elements = null;
                    return result;
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

        public async Task<ServiceResponseList<ComboGral>?> ListaUnidadNegocioTipo(int Id_Unidad_NegocioKey)
        {
            var result = new ServiceResponseList<ComboGral>();
            try
            {
                var resultData = await _txCotizacionesRepository.ListaUnidadNegocioTipo(Id_Unidad_NegocioKey);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                    result.Elements = null;
                    return result;
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

        public async Task<ServiceResponseList<ComboGral>?> ListaColoresXCliente(string Cod_Cliente)
        {
            var result = new ServiceResponseList<ComboGral>();
            try
            {
                var resultData = await _txCotizacionesRepository.ListaColoresXCliente(Cod_Cliente);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                    result.Elements = null;
                    return result;
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

        public async Task<ServiceResponseList<Tx_PreciosColor>?> ListaPrecioXColor(string Cod_Color)
        {
            var result = new ServiceResponseList<Tx_PreciosColor>();
            try
            {
                var resultData = await _txCotizacionesRepository.ListaPrecioXColor(Cod_Color);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                    result.Elements = null;
                    return result;
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

        public async Task<ServiceResponseList<ComboGral>?> ListaRecetasAntipilling()
        {
            var result = new ServiceResponseList<ComboGral>();
            try
            {
                var resultData = await _txCotizacionesRepository.ListaRecetasAntipilling();
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                    result.Elements = null;
                    return result;
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
