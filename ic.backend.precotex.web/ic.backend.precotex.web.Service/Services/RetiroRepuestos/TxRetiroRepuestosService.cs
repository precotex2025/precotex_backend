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

        public async Task<ServiceResponseList<Lg_Item>?> ListaItems()
        {
            var result = new ServiceResponseList<Lg_Item>();
            try
            {
                var resultData = await _txRetiroRepuestosRepository.ListaItems();
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

        public async Task<ServiceResponse<int>> RegistrarRequerimientoDetalle(Tx_Retiro_Repuestos_Detalle tx_Retiro_Repuestos_Detalle)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txRetiroRepuestosRepository.RegistrarRequerimientoDetalle(tx_Retiro_Repuestos_Detalle);
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
                result.Message = "Error inesperado" + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponse<int>> ActualizarRequerimientoDetalle(Tx_Retiro_Repuestos_Detalle tx_Retiro_Repuestos_Detalle)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txRetiroRepuestosRepository.ActualizarRequerimientoDetalle(tx_Retiro_Repuestos_Detalle);
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


    }
}
