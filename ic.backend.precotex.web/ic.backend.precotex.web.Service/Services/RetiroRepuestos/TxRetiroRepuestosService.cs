using ic.backend.precotex.web.Data.Repositories.Implementation.RetiroRepuestos;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.RetiroRepuestos;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.RetiroRepuestos;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.RetiroRepuestos
{
    public class TxRetiroRepuestosService:ITxRetiroRepuestosService
    {
        private readonly ITxRetiroRepuestosRepository _txRetiroRepuestosRepository;

        public TxRetiroRepuestosService(ITxRetiroRepuestosRepository txRetiroRepuestosRepository)
        {
            _txRetiroRepuestosRepository = txRetiroRepuestosRepository;
        }

        public async Task<ServiceResponseList<Tx_Retiro_Repuestos>?> ListaRetiros(DateTime FecIni, DateTime FecFin)
        {
            var result = new ServiceResponseList<Tx_Retiro_Repuestos>();
            try
            {
                var resultData = await _txRetiroRepuestosRepository.ListaRetiros(FecIni, FecFin);
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
            catch(Exception ex)
            {
                result.Message = "Excepción no controlada " + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponseList<Tx_Retiro_Repuestos>?> ListaRetirosPorNumRequerimiento(int Num_Requerimiento)
        {
            var result = new ServiceResponseList<Tx_Retiro_Repuestos>();
            try
            {
                var resultData = await _txRetiroRepuestosRepository.ListaRetirosPorNumRequerimiento(Num_Requerimiento);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No Existe informacion";
                }
                result.Success = true;
                result.Message = "Completado con éxito";
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch(Exception ex)
            {
                result.Message = "Excepcion no controlada " + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponse<int>> RegistrarRequerimiento(Tx_Retiro_Repuestos tx_Retiro_Repuestos)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txRetiroRepuestosRepository.RegistrarRequerimiento(tx_Retiro_Repuestos);
                if(resultData.Codigo > 0)
                {
                    result.Success = true;
                    result.Message = resultData.Mensaje;
                    result.CodeTransacc = resultData.Codigo;
                    return result;
                }
                result.Success = false;
                result.Message = resultData.Mensaje;
                return result;
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = "Error inesperado " + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponse<int>> ActualizarRequerimiento(Tx_Retiro_Repuestos tx_Retiro_Repuestos)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txRetiroRepuestosRepository.ActualizarRequerimiento(tx_Retiro_Repuestos);
                if(resultData.Codigo > 0)
                {
                    result.Success = true;
                    result.Message = resultData.Mensaje;
                    result.CodeTransacc = resultData.Codigo;
                    return result;
                }
                result.Success = false;
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

        public async Task<ServiceResponse<int>> ActualizarRequerimientoPrecintoCierre(Tx_Retiro_Repuestos tx_Retiro_Repuestos)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txRetiroRepuestosRepository.ActualizarRequerimientoPrecintoCierre(tx_Retiro_Repuestos);
                if (resultData.Codigo > 0)
                {
                    result.Success = true;
                    result.Message = resultData.Mensaje;
                    result.CodeTransacc = resultData.Codigo;
                    return result;
                }
                result.Success = false;
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



        public async Task<ServiceResponseList<Tx_Retiro_Repuestos_Detalle>?> ListaRetiroDetallePorNumRequerimiento(int Num_Requerimiento)
        {
            var result = new ServiceResponseList<Tx_Retiro_Repuestos_Detalle>();
            try
            {
                var resultData = await _txRetiroRepuestosRepository.ListaRetiroDetallePorNumRequerimiento(Num_Requerimiento);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No Existe informacion";
                }
                result.Success = true;
                result.Message = "Completado con éxito";
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch(Exception ex)
            {
                result.Message = "Excepcion no controlada " + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponseList<Tx_Retiro_Repuestos_Detalle>?> ListaRetiroDetallePorNumReqySecuencia(int Num_Requerimiento, int Nro_Secuencia)
        {
            var result = new ServiceResponseList<Tx_Retiro_Repuestos_Detalle>();
            try
            {
                var resultData = await _txRetiroRepuestosRepository.ListaRetiroDetallePorNumReqySecuencia(Num_Requerimiento, Nro_Secuencia);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No Existe informacion";
                }
                result.Success = true;
                result.Message = "Completado con éxito";
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Excepcion no controlada " + ex.Message;
                return result;
            }
        }

        //public async Task<ServiceResponse<int>> RegistrarRequerimientoDetalle(string nNum_Requerimiento, string sCod_Item, string nCan_Requerida, string sRpt_Cambio, string nombreArchivo)
        //{
        //    var result = new ServiceResponse<int>();
        //    try
        //    {
        //        var resultData = await _txRetiroRepuestosRepository.RegistrarRequerimientoDetalle(nNum_Requerimiento, sCod_Item, nCan_Requerida, sRpt_Cambio, nombreArchivo);
        //        if (resultData.Codigo > 0)
        //        {
        //            result.Success = true;
        //            result.Message = resultData.Mensaje;
        //            result.CodeTransacc = resultData.Codigo;
        //            return result;
        //        }
        //        result.Success = false;
        //        result.Message = resultData.Mensaje;
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Success = false;
        //        result.Message = "Error inesperado" + ex.Message;
        //        return result;
        //    }
        //}

        public async Task<ServiceResponse<int>> ActualizarRequerimientoDetalle(string nNum_Requerimiento, string nNum_Secuencia, string nCan_Requerida, string sRpt_Cambio, string sNombreArchivo)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txRetiroRepuestosRepository.ActualizarRequerimientoDetalle(nNum_Requerimiento, nNum_Secuencia, nCan_Requerida, sRpt_Cambio, sNombreArchivo);
                if (resultData.Codigo > 0)
                {
                    result.Success = true;
                    result.Message = resultData.Mensaje;
                    result.CodeTransacc = resultData.Codigo;
                    return result;
                }
                result.Success = false;
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


        /*****************************************************COMPLEMENTARIOS**********************************************************/
        public async Task<ServiceResponseList<Lg_Item>?> ListaItems(string Cod_Item)
        {
            var result = new ServiceResponseList<Lg_Item>();
            try
            {
                var resultData = await _txRetiroRepuestosRepository.ListaItems(Cod_Item);
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
            catch (Exception ex)
            {
                result.Message = "Excepcion no controlada " + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponseList<Lg_Item>?> ListaItemsCompletos()
        {
            var result = new ServiceResponseList<Lg_Item>();
            try
            {
                var resultData = await _txRetiroRepuestosRepository.ListaItemsCompletos();
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
            catch (Exception ex)
            {
                result.Message = "Excepcion no controlada " + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponseList<Lg_Retiro_Repuesto_Usuario>?> ListaRetiroRepuestoUsuario(int Id_Usuario)
        {
            var result = new ServiceResponseList<Lg_Retiro_Repuesto_Usuario>();
            try
            {
                var resultData = await _txRetiroRepuestosRepository.ListaRetiroRepuestoUsuario(Id_Usuario);
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
            catch (Exception ex)
            {
                result.Message = "Excepcion no controlada " + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponseList<Lg_Retiro_Repuesto_Usuario>?> ListaRetiroRepuestoUsuarioPorTipo(int Tip_Usuario)
        {
            var result = new ServiceResponseList<Lg_Retiro_Repuesto_Usuario>();
            try
            {
                var resultData = await _txRetiroRepuestosRepository.ListaRetiroRepuestoUsuarioPorTipo(Tip_Usuario);
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
            catch (Exception ex)
            {
                result.Message = "Excepcion no controlada " + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponseList<Lg_Retiro_Repuesto_Usuario>?> ListaRetiroRepuestoUsuarioSeguridadNombres()
        {
            var result = new ServiceResponseList<Lg_Retiro_Repuesto_Usuario>();
            try
            {
                var resultData = await _txRetiroRepuestosRepository.ListaRetiroRepuestoUsuarioSeguridadNombres();
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
            catch (Exception ex)
            {
                result.Message = "Excepcion no controlada " + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponseList<Lg_Retiro_Repuesto_Usuario>?> ListaRetiroRepuestoUsuarioMantenimientoNombres()
        {
            var result = new ServiceResponseList<Lg_Retiro_Repuesto_Usuario>();
            try
            {
                var resultData = await _txRetiroRepuestosRepository.ListaRetiroRepuestoUsuarioMantenimientoNombres();
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
            catch (Exception ex)
            {
                result.Message = "Excepcion no controlada " + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponseList<Lg_Item>?> ListaDatosItemsPorNumReqySecuencia(int Num_Requerimiento, int Nro_Secuencia)
        {
            var result = new ServiceResponseList<Lg_Item>();
            try
            {
                var resultData = await _txRetiroRepuestosRepository.ListaDatosItemsPorNumReqySecuencia(Num_Requerimiento, Nro_Secuencia);
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
            catch (Exception ex)
            {
                result.Message = "Excepcion no controlada " + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponseList<Tx_Retiro_Repuestos_Reporte>?> ListaDatosReporte(DateTime FecIni, DateTime FecFin)
        {
            var result = new ServiceResponseList<Tx_Retiro_Repuestos_Reporte>();
            try
            {
                var resultData = await _txRetiroRepuestosRepository.ListaDatosReporte(FecIni, FecFin);
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
            catch (Exception ex)
            {
                result.Message = "Excepcion no controlada " + ex.Message;
                return result;
            }
        }
    }
}
