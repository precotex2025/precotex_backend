using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Data.Repositories.Implementation.RegistroPartidaParihuela;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.ReporteNC;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.RegistroPartidaParihuela;

namespace ic.backend.precotex.web.Service.Services.RegistroPartidaParihuela
{
    public class SRegistroPartidaParihuela: IRegistroPartidaParihuelaService
    {
        private readonly IRegistroPartidaParihuela _txIRegistroPartidaParihuela;
        public SRegistroPartidaParihuela(IRegistroPartidaParihuela txIRegistroPartidaParihuela)
        {
            _txIRegistroPartidaParihuela = txIRegistroPartidaParihuela;
        }

        public async Task<ServiceResponseList<E_RegistroPartidaParihuela>?> ObtenerDetPartida(string pCod_Partida, string pOpcion)
        {
            var result = new ServiceResponseList<E_RegistroPartidaParihuela>();
            try
            {
                var resultData = await _txIRegistroPartidaParihuela.ObtenerDetPartida(pCod_Partida, pOpcion);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                    return result;
                }

                result.Success = true;
                result.Message = "Encontrado";
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (SqlException sql)
            {
                result.Message = "BD SQL: " + sql.Message;
                return result;
            }
        }

        public async Task<ServiceResponseList<E_RegistroPartidaParihuela>?> UpdateDetPartida(List<E_RegistroPartidaParihuela> pData, string pCod_Usuario, string pEstadoParihuela, string pReposicion)
        {
            var result = new ServiceResponseList<E_RegistroPartidaParihuela>();
            try
            {
                var resultData = await _txIRegistroPartidaParihuela.UpdateDetPartida(pData, pCod_Usuario, pEstadoParihuela, pReposicion);
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
                result.Message = "BD SQL: " + sql.Message;
                return result;
            }
        }

        public async Task<ServiceResponseList<E_Complemento>?> ObtenerCategoriasPorId(string pIdPartida)
        {
            var result = new ServiceResponseList<E_Complemento>();
            try
            {
                var resultData = await _txIRegistroPartidaParihuela.ObtenerCategoriasPorId(pIdPartida);
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
                result.Message = "BD SQL: " + sql.Message;
                return result;
            }
        }

        public async Task<ServiceResponseList<string>?> ValidarMerma(string pCod_Partida)
        {
            var result = new ServiceResponseList<string>();
            try
            {
                var resultData = await _txIRegistroPartidaParihuela.validarMerma(pCod_Partida);
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
                result.Message = "BD SQL: " + sql.Message;
                return result;
            }
        }

        public async Task<ServiceResponseList<string>?> UpdateEstadoMermaAsync(string pCod_Partida)
        {
            var result = new ServiceResponseList<string>();
            try 
            { 
            
                var resultData = await _txIRegistroPartidaParihuela.UpdateEstadoMermaAsync(pCod_Partida);
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
                result.Message = "BD SQL: " + sql.Message;
                return result;
            }
        }

        public async Task<ServiceResponseList<string>?> EnviarDespacho(string pCod_Partida)
        {
            var result = new ServiceResponseList<string>();
            try
            {
                var resultData = await _txIRegistroPartidaParihuela.EnviarDespacho(pCod_Partida);
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
                result.Message = "BD SQL: " + sql.Message;
                return result;
            }

        }

        public async Task<ServiceResponse<int>> EnviarCabecera(string pCod_Partida)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txIRegistroPartidaParihuela.EnviarCabecera(pCod_Partida);
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
