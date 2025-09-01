using ic.backend.precotex.web.Data.Repositories.Implementation.Tejeduria;
using ic.backend.precotex.web.Data.Repositories.Implementation.Tintoreria;
using ic.backend.precotex.web.Data.Repositories.Tintoreria;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.Tejeduria;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ic.backend.precotex.web.Service.Services.Tejeduria
{
    public class TxTelaEstructuraTejidoItemsService: ITxTelaEstructuraTejidoItemsService
    {
        private readonly ITxTelaEstructuraTejidoItemsRepository _txTelaEstructuraTejidoItemsRepository;
        public TxTelaEstructuraTejidoItemsService(ITxTelaEstructuraTejidoItemsRepository txTelaEstructuraTejidoItemsRepository)
        {
            _txTelaEstructuraTejidoItemsRepository = txTelaEstructuraTejidoItemsRepository;
        }

        public async Task<ServiceResponseList<Tx_Ots_Hojas_Arranque_Versiones>?> GeneraVersionHojasArranque(string? Cod_Ordtra, string? Num_Secuencia, string? Cod_Talla)
        {
            var result = new ServiceResponseList<Tx_Ots_Hojas_Arranque_Versiones>();
            try
            {
                var resultData = await _txTelaEstructuraTejidoItemsRepository.GeneraVersionHojasArranque(Cod_Ordtra, Num_Secuencia, Cod_Talla);
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

        public async Task<ServiceResponse<int>> InsertarCargaEstructuraTejido(string NombreVersion, string Cod_Tela, string Servicio, string Observacion, string Elaborado, string Revisado, string Cod_Usuario, string XMLData)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txTelaEstructuraTejidoItemsRepository.InsertarCargaEstructuraTejido(NombreVersion, Cod_Tela, Servicio, Observacion, Elaborado, Revisado, Cod_Usuario, XMLData);
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

        public async Task<ServiceResponse<int>> InsertarEstructuraTejidoItem(string Cod_Ordtra, int Num_Secuencia, string Cod_Comb, string Cod_Talla, string Cod_Usuario, string XMLData)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txTelaEstructuraTejidoItemsRepository.InsertarEstructuraTejidoItem(Cod_Ordtra, Num_Secuencia, Cod_Comb, Cod_Talla, Cod_Usuario, XMLData);
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

        public async Task<ServiceResponse<int>> InsertarTelaMedida(string Cod_Ordtra, int Num_Secuencia, string Cod_Tela, string Cod_Comb, string Cod_Talla, string Cod_Usuario, string XMLData)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txTelaEstructuraTejidoItemsRepository.InsertarTelaMedida(Cod_Ordtra, Num_Secuencia, Cod_Tela, Cod_Comb, Cod_Talla, Cod_Usuario, XMLData);
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

        public async Task<ServiceResponseList<Tx_Ots_Hojas_Arranque_Versiones>?> ObtenerVersionHojasArranque(string? Cod_Ordtra, string? Num_Secuencia)
        {
            var result = new ServiceResponseList<Tx_Ots_Hojas_Arranque_Versiones>();
            try
            {
                var resultData = await _txTelaEstructuraTejidoItemsRepository.ObtenerVersionHojasArranque(Cod_Ordtra, Num_Secuencia);
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

        //public async Task<ServiceResponse<int>> ModificarMedida(string Cod_Ordtra, int Num_Secuencia)
        //{
        //    var result = new ServiceResponse<int>();
        //    try
        //    {
        //        var resultData = await _txTelaEstructuraTejidoItemsRepository.ModificarMedida(Cod_Ordtra, Num_Secuencia);
        //        if (resultData.Codigo > 0)
        //        {
        //            result.Message = resultData.Mensaje;
        //            result.Success = true;
        //            result.CodeTransacc = resultData.Codigo;

        //            return result;
        //        }

        //        result.Message = resultData.Mensaje;
        //        result.Success = false;
        //        return result;

        //    }
        //    catch (SqlException sql)
        //    {
        //        result.Message = "Error en Servidor: " + sql.Message;
        //        result.Success = false;
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Message = "Ocurrio una excepción" + ex.Message;
        //        result.Success = false;
        //        return result;
        //    }
        //}

        public async Task<ServiceResponseList<Tx_TelaEstructuraTejidoItems>?> ObtieneEstructuraTejidoItem(string? codTela, string? Cod_Ordtra, string? Num_Secuencia)
        {
            var result = new ServiceResponseList<Tx_TelaEstructuraTejidoItems>();
            try
            {
                var resultData = await _txTelaEstructuraTejidoItemsRepository.ObtieneEstructuraTejidoItem(codTela, Cod_Ordtra, Num_Secuencia);
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

        public async Task<ServiceResponseList<Tx_TelaMed>?> ObtieneTelaMedida(string codTela, string Cod_Talla)
        {
            var result = new ServiceResponseList<Tx_TelaMed>();
            try
            {
                var resultData = await _txTelaEstructuraTejidoItemsRepository.ObtieneTelaMedida(codTela, Cod_Talla);
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

        public async Task<ServiceResponseList<Tx_TelaMed>?> ObtieneTelaMedidaHist(string codTela, string Cod_Ordtra, string Num_Secuencia, string Cod_Comb, string Cod_Talla)
        {
            var result = new ServiceResponseList<Tx_TelaMed>();
            try
            {
                var resultData = await _txTelaEstructuraTejidoItemsRepository.ObtieneTelaMedidaHist(codTela, Cod_Ordtra, Num_Secuencia, Cod_Comb, Cod_Talla);
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

        public async Task<ServiceResponse<int>> ValidaVersionHojasArranque(string Cod_Ordtra, int Num_Secuencia, int Version, string Flg_Rectilineo)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txTelaEstructuraTejidoItemsRepository.ValidaVersionHojasArranque(Cod_Ordtra, Num_Secuencia, Version, Flg_Rectilineo);
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

        public async Task<ServiceResponseList<Tx_Ots_Hojas_Arranque_Versiones_Listado>?> ListadoVersionHojasArranqueHist(DateTime FecIni, DateTime FecFin, string Cod_Ordtra)
        {
            var result = new ServiceResponseList<Tx_Ots_Hojas_Arranque_Versiones_Listado>();
            try
            {
                var resultData = await _txTelaEstructuraTejidoItemsRepository.ListadoVersionHojasArranqueHist(FecIni, FecFin, Cod_Ordtra);
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

        public async Task<ServiceResponseList<Tx_Maquinas_Revisadoras>?> ListaMaquinaRevisadora()
        {
            var result = new ServiceResponseList<Tx_Maquinas_Revisadoras>();
            try
            {
                var resultData = await _txTelaEstructuraTejidoItemsRepository.ListaMaquinaRevisadora();
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

        public async Task<ServiceResponse<int>> CrudArranqueCtrol(Tx_Ots_Hojas_Arranque_Ctrol tx_Ots_Hojas_Arranque_Ctrol, string sTipoTransac)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txTelaEstructuraTejidoItemsRepository.CrudArranqueCtrol(tx_Ots_Hojas_Arranque_Ctrol, sTipoTransac);
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

        public async Task<ServiceResponseList<Tx_Ots_Hojas_Arranque_Ctrol>?> ObtenerArranqueCtrol(string Cod_OrdTra, int Num_Secuencia, int Version)
        {
            var result = new ServiceResponseList<Tx_Ots_Hojas_Arranque_Ctrol>();
            try
            {
                var resultData = await _txTelaEstructuraTejidoItemsRepository.ObtenerArranqueCtrol(Cod_OrdTra, Num_Secuencia, Version);
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

        public async Task<ServiceResponseList<Tx_Ots_Hojas_Arranque_Ctrol>?> ObtenerArranqueCtrolSinVersion(string Cod_OrdTra, int Num_Secuencia)
        {
            var result = new ServiceResponseList<Tx_Ots_Hojas_Arranque_Ctrol>();
            try
            {
                var resultData = await _txTelaEstructuraTejidoItemsRepository.ObtenerArranqueCtrolSinVersion(Cod_OrdTra, Num_Secuencia);
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

        public async Task<ServiceResponseList<Tx_Ots_Hojas_Arranque_Versiones_Listado>?> ListadoVersionHojasArranqueHistDetail(DateTime FecIni, DateTime FecFin, string Cod_Ordtra)
        {
            var result = new ServiceResponseList<Tx_Ots_Hojas_Arranque_Versiones_Listado>();
            try
            {
                var resultData = await _txTelaEstructuraTejidoItemsRepository.ListadoVersionHojasArranqueHistDetail(FecIni, FecFin, Cod_Ordtra);
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
