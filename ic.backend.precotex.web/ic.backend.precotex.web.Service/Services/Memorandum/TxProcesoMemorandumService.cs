using ic.backend.precotex.web.Data.Repositories.Implementation.DDT;
using ic.backend.precotex.web.Data.Repositories.Implementation.Memorandum;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.Memorandum;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.Memorandum;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Memorandum
{
    public class TxProcesoMemorandumService : ITxProcesoMemorandumService
    {
        private readonly ITxProcesoMemorandumRepository _txProcesoMemorandumRepository;

        public TxProcesoMemorandumService(ITxProcesoMemorandumRepository txProcesoMemorandumRepository)
        {
            _txProcesoMemorandumRepository = txProcesoMemorandumRepository;
        }

        public async Task<ServiceResponse<int>> AvanzaEstadoMemorandum(string sCodUsuario, string sNumMemo, string sObservaciones)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txProcesoMemorandumRepository.AvanzaEstadoMemorandum(sCodUsuario, sNumMemo, sObservaciones);
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

        public async Task<ServiceResponse<int>> DevolverMemorandum(Tx_Memorandum tx_Memorandum)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txProcesoMemorandumRepository.DevolverMemorandum(tx_Memorandum);
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

        public async Task<ServiceResponseList<Tx_Movimiento_Memorandum>?> HistorialMovimientoMemorandum(string sNumMemo)
        {
            var result = new ServiceResponseList<Tx_Movimiento_Memorandum>();
            try
            {
                var resultData = await _txProcesoMemorandumRepository.HistorialMovimientoMemorandum(sNumMemo);
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

        public async Task<ServiceResponseList<Tx_Material_Memorandum>?> Materiales()
        {
            var result = new ServiceResponseList<Tx_Material_Memorandum>();
            try
            {
                var resultData = await _txProcesoMemorandumRepository.Materiales();
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

        public async Task<ServiceResponseList<Tx_Motivo_Memorandum>?> Motivos()
        {
            var result = new ServiceResponseList<Tx_Motivo_Memorandum>();
            try
            {
                var resultData = await _txProcesoMemorandumRepository.Motivos();
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

        public async Task<ServiceResponseList<Tx_Roles>?> ObtenerInfoUsuarioMemorandum(string sCodUsuario)
        {
            var result = new ServiceResponseList<Tx_Roles>();
            try
            {
                var resultData = await _txProcesoMemorandumRepository.ObtenerInfoUsuarioMemorandum(sCodUsuario);
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

        public async Task<ServiceResponseList<Tx_Transicion_Memorandum>?> ObtenerPermisosMemorandum(string sCodUsuario, string sNumMemo)
        {
            var result = new ServiceResponseList<Tx_Transicion_Memorandum>();
            try
            {
                var resultData = await _txProcesoMemorandumRepository.ObtenerPermisosMemorandum(sCodUsuario, sNumMemo);
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

        public async Task<ServiceResponseList<Tx_Roles>?> ObtenerRolUsuarioMemorandum(string sCodUsuario, string sNumMemo)
        {
            var result = new ServiceResponseList<Tx_Roles>();
            try
            {
                var resultData = await _txProcesoMemorandumRepository.ObtenerRolUsuarioMemorandum(sCodUsuario, sNumMemo);
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

        public async Task<ServiceResponseList<Tx_Memorandum_Detalle>?> ObtieneDetalleMemorandumByNumMemo(string NumMemo)
        {
            var result = new ServiceResponseList<Tx_Memorandum_Detalle>();
            try
            {
                var resultData = await _txProcesoMemorandumRepository.ObtieneDetalleMemorandumByNumMemo(NumMemo);
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

        public async Task<ServiceResponseList<Tx_Memorandum>?> ObtieneInformacionMemorandum(DateTime FecIni, DateTime FecFin, string NumMemo, string codUsuario, string CodPlantaGarita)
        {
            var result = new ServiceResponseList<Tx_Memorandum>();
            try
            {
                var resultData = await _txProcesoMemorandumRepository.ObtieneInformacionMemorandum(FecIni, FecFin, NumMemo, codUsuario, CodPlantaGarita);
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

        public async Task<ServiceResponseList<Sg_Planta>?> Plantas()
        {
            var result = new ServiceResponseList<Sg_Planta>();
            try
            {
                var resultData = await _txProcesoMemorandumRepository.Plantas();
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

        public async Task<ServiceResponse<int>> ProcesoMntoMemorandum(Tx_Memorandum tx_Memorandum, List<Tx_Memorandum_Detalle> detalle, string sTipoTransac)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txProcesoMemorandumRepository.ProcesoMntoMemorandum(tx_Memorandum, detalle, sTipoTransac);
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

        public async Task<ServiceResponse<int>> RevertirEstadoMemorandum(string sCodUsuario, string sNumMemo, string sObservaciones)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txProcesoMemorandumRepository.RevertirEstadoMemorandum(sCodUsuario, sNumMemo, sObservaciones);
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

        public async Task<ServiceResponseList<Tx_Tipo_Memorandum>?> TipoMemorandum()
        {
            var result = new ServiceResponseList<Tx_Tipo_Memorandum>();
            try
            {
                var resultData = await _txProcesoMemorandumRepository.TipoMemorandum();
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

        public async Task<ServiceResponseList<SEG_Usuarios>?> Usuario(string Cod_Trabajador, string Tip_Trabajador)
        {
            var result = new ServiceResponseList<SEG_Usuarios>();
            try
            {
                var resultData = await _txProcesoMemorandumRepository.Usuario(Cod_Trabajador, Tip_Trabajador);
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
