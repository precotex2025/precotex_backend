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
    public class TxUbicacionColgadorService : ITxUbicacionColgadorService
    {

        private readonly ITxUbicacionColgadorRepository _txUbicacionColgadorRepository;
       
        public TxUbicacionColgadorService(ITxUbicacionColgadorRepository txUbicacionColgadorRepository)
        {
            _txUbicacionColgadorRepository = txUbicacionColgadorRepository;
        }

        public async Task<ServiceResponse<int>> CrudUbicacionColgador(Tx_Ubicacion_Colgador tx_Ubicacion_Colgador, string sTipoTransac)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txUbicacionColgadorRepository.CrudUbicacionColgador(tx_Ubicacion_Colgador, sTipoTransac);
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

        public async Task<ServiceResponseList<Tx_Ubicacion_Colgador>?> ListadoUbicacionColgador(DateTime FecIni, DateTime FecFin, int Id_Tipo_Ubicacion_Colgador)
        {
            var result = new ServiceResponseList<Tx_Ubicacion_Colgador>();
            try
            {
                var resultData = await _txUbicacionColgadorRepository.ListadoUbicacionColgador(FecIni, FecFin, Id_Tipo_Ubicacion_Colgador);
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

        public async Task<ServiceResponseList<Tx_FamTela>?> ListadoTipoFamTela()
        {
            var result = new ServiceResponseList<Tx_FamTela>();
            try
            {
                var resultData = await _txUbicacionColgadorRepository.ListadoTipoFamTela();
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

        public async Task<ServiceResponseList<Tx_Tipo_Ubicacion_Colgador>?> ListadoTipoUbicacionColgador()
        {
            var result = new ServiceResponseList<Tx_Tipo_Ubicacion_Colgador>();
            try
            {
                var resultData = await _txUbicacionColgadorRepository.ListadoTipoUbicacionColgador();
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

        public async Task<ServiceResponseList<Tx_TipoUbicacionControl>?> ObtenerCorrelativo(int Id_Tipo_Ubicacion_Colgador, string Cod_FamTela)
        {
            var result = new ServiceResponseList<Tx_TipoUbicacionControl>();
            try
            {
                var resultData = await _txUbicacionColgadorRepository.ObtenerCorrelativo(Id_Tipo_Ubicacion_Colgador, Cod_FamTela);
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

        public async Task<ServiceResponse<int>> CrudUbicacionColgadorItems(Tx_Ubicacion_Colgador_Items tx_Ubicacion_Colgador_Items, string CodigoBarra, string sTipoTransac)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txUbicacionColgadorRepository.CrudUbicacionColgadorItems(tx_Ubicacion_Colgador_Items, CodigoBarra, sTipoTransac);
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

        public async Task<ServiceResponseList<Tx_Ubicacion_Colgador>?> ObtenerUbicacionColgadorQR(string CodigoBarra)
        {
            var result = new ServiceResponseList<Tx_Ubicacion_Colgador>();
            try
            {
                var resultData = await _txUbicacionColgadorRepository.ObtenerUbicacionColgadorQR(CodigoBarra);
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

        public async Task<ServiceResponseList<Tx_Ubicacion_Colgador>?> ObtenerUbicacionColgadorById(int Id_Tx_Ubicacion_Colgador)
        {
            var result = new ServiceResponseList<Tx_Ubicacion_Colgador>();
            try
            {
                var resultData = await _txUbicacionColgadorRepository.ObtenerUbicacionColgadorById(Id_Tx_Ubicacion_Colgador);
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

        public async Task<ServiceResponseList<Tx_Ubicacion_Colgador>?> ListadoTotalColgadoresxTipoUbicaciones(DateTime? FecCrea)
        {
            var result = new ServiceResponseList<Tx_Ubicacion_Colgador>();
            try
            {
                var resultData = await _txUbicacionColgadorRepository.ListadoTotalColgadoresxTipoUbicaciones(FecCrea);
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

        public async Task<ServiceResponseList<Tx_Colgador_Registro_Cab>?> ListadoColgadoresxUbicacion(int Id_Tx_Ubicacion_Colgador)
        {
            var result = new ServiceResponseList<Tx_Colgador_Registro_Cab>();
            try
            {
                var resultData = await _txUbicacionColgadorRepository.ListadoColgadoresxUbicacion(Id_Tx_Ubicacion_Colgador);
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

        public async Task<ServiceResponseList<Tx_Ubicacion_Colgador>?> ListadoTotalColgadoresxCodigoBarra(string CodigoBarra)
        {
            var result = new ServiceResponseList<Tx_Ubicacion_Colgador>();
            try
            {
                var resultData = await _txUbicacionColgadorRepository.ListadoTotalColgadoresxCodigoBarra(CodigoBarra);
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

        public async Task<ServiceResponseList<Tx_Ubicacion_Fisica>?> ListadoUbicacioFisica()
        {
            var result = new ServiceResponseList<Tx_Ubicacion_Fisica>();
            try
            {
                var resultData = await _txUbicacionColgadorRepository.ListadoUbicacioFisica();
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

        public async Task<ServiceResponseList<Tx_Ubicacion_Colgador>?> ObtenerInformacionTotalCajasxUbicacion(int Id_Tx_Ubicacion_Fisica)
        {
            var result = new ServiceResponseList<Tx_Ubicacion_Colgador>();
            try
            {
                var resultData = await _txUbicacionColgadorRepository.ObtenerInformacionTotalCajasxUbicacion(Id_Tx_Ubicacion_Fisica);
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

        public async Task<ServiceResponseList<Tx_Ubicacion_Colgador>?> ObtenerInformacionCajasxUbicacion(int Id_Tx_Ubicacion_Fisica)
        {
            var result = new ServiceResponseList<Tx_Ubicacion_Colgador>();
            try
            {
                var resultData = await _txUbicacionColgadorRepository.ObtenerInformacionCajasxUbicacion(Id_Tx_Ubicacion_Fisica);
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

        public async Task<ServiceResponseList<Tx_Ubicacion_Impresora_Activa>?> ObtenerImpresoraPredeterminada()
        {
            var result = new ServiceResponseList<Tx_Ubicacion_Impresora_Activa>();
            try
            {
                var resultData = await _txUbicacionColgadorRepository.ObtenerImpresoraPredeterminada();
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

        public async Task<ServiceResponseList<Tx_Cliente>?> ObtieneAbreviaturaCliente(string Cod_Tela, string Cod_Ruta, string Cod_OrdTra)
        {
            var result = new ServiceResponseList<Tx_Cliente>();
            try
            {
                var resultData = await _txUbicacionColgadorRepository.ObtieneAbreviaturaCliente(Cod_Tela, Cod_Ruta, Cod_OrdTra);
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

        public async Task<ServiceResponseList<Tx_Colgador_Reporte_Gral>?> ReporteColgadoresGralDetallado()
        {
            var result = new ServiceResponseList<Tx_Colgador_Reporte_Gral>();
            try
            {
                var resultData = await _txUbicacionColgadorRepository.ReporteColgadoresGralDetallado();
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
