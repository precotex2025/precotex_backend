using ic.backend.precotex.web.Data.Repositories.DDT;
using ic.backend.precotex.web.Data.Repositories.Implementation.DDT;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.DDT;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.DDT
{
    public class TxProcesoColgadorRegistroService : ITxProcesoColgadorRegistroService
    {
        private readonly ITxProcesoColgadorRegistroRepository _txProcesoColgadorRegistroRepository;
        public TxProcesoColgadorRegistroService(ITxProcesoColgadorRegistroRepository txProcesoColgadorRegistroRepository)
        {
            _txProcesoColgadorRegistroRepository = txProcesoColgadorRegistroRepository;
        }

        public async Task<ServiceResponseList<Tx_Colgador_Registro_Cab>?> ListadoColgadoresBandeja(DateTime FecIni, DateTime FecFin, string Cod_Tela)
        {
            var result = new ServiceResponseList<Tx_Colgador_Registro_Cab>();
            try
            {
                var resultData = await _txProcesoColgadorRegistroRepository.ListadoColgadoresBandeja(FecIni, FecFin, Cod_Tela);
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

        public async Task<ServiceResponseList<Tx_Cliente>?> ObtieneInformacionClienteColgador()
        {
            var result = new ServiceResponseList<Tx_Cliente>();
            try
            {
                var resultData = await _txProcesoColgadorRegistroRepository.ObtieneInformacionClienteColgador();
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

        public async Task<ServiceResponseList<Tx_TelaEstructuraRuta>?> ObtieneInformacionRutaColgador(string Cod_Tela)
        {
            var result = new ServiceResponseList<Tx_TelaEstructuraRuta>();
            try
            {
                var resultData = await _txProcesoColgadorRegistroRepository.ObtieneInformacionRutaColgador(Cod_Tela);
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

        public async Task<ServiceResponseList<Tx_TelaEstructuraColgador>?> ObtieneInformacionTelaColgador(string Cod_Tela)
        {
            var result = new ServiceResponseList<Tx_TelaEstructuraColgador>();
            try
            {
                var resultData = await _txProcesoColgadorRegistroRepository.ObtieneInformacionTelaColgador(Cod_Tela);
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

        public async Task<ServiceResponseList<Tx_Colgador_Registro_Det>?> ObtieneInformacionTelaColgadorDet(int Id_Tx_Colgador_Registro_Cab)
        {
            var result = new ServiceResponseList<Tx_Colgador_Registro_Det>();
            try
            {
                var resultData = await _txProcesoColgadorRegistroRepository.ObtieneInformacionTelaColgadorDet(Id_Tx_Colgador_Registro_Cab);
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

        public async Task<ServiceResponse<int>> ProcesoEliminarColgador(int Id_Tx_Colgador_Registro_Cab, string Usu_Registro)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txProcesoColgadorRegistroRepository.ProcesoEliminarColgador(Id_Tx_Colgador_Registro_Cab, Usu_Registro);
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

        public async Task<ServiceResponse<int>> ProcesoMntoColgador(Tx_Colgador_Registro_Cab tx_Colgador_Registro_Cab, List<Tx_Colgador_Registro_Det> detalle, string sTipoTransac)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txProcesoColgadorRegistroRepository.ProcesoMntoColgador(tx_Colgador_Registro_Cab, detalle, sTipoTransac);
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
    }
}
