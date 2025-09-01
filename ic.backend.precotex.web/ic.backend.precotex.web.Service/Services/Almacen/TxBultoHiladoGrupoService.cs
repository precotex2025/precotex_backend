using ic.backend.precotex.web.Data.Repositories.Almacen;
using ic.backend.precotex.web.Data.Repositories.Implementation.Almacen;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.Almacen;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Almacen
{
    public class TxBultoHiladoGrupoService : ITxBultoHiladoGrupoService
    {
        private readonly ITxBultoHiladoGrupoRepository _txBultoHiladoGrupoRepository;

        public TxBultoHiladoGrupoService(ITxBultoHiladoGrupoRepository txBultoHiladoGrupoRepository)
        {
            _txBultoHiladoGrupoRepository = txBultoHiladoGrupoRepository;
        }

        public async Task<ServiceResponseList<Tx_Bulto_Hilado_Grupo>?> Lista(DateTime? FecCrea, string? Grupo)
        {
            var result = new ServiceResponseList<Tx_Bulto_Hilado_Grupo>();
            try
            {
                var resultData = await _txBultoHiladoGrupoRepository.Lista(FecCrea, Grupo);
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

        public async Task<ServiceResponse<int>> Insertar(Tx_Bulto_Hilado_Grupo tx_Bulto_Hilado_Grupo)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txBultoHiladoGrupoRepository.Insertar(tx_Bulto_Hilado_Grupo);
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

        public async Task<ServiceResponseList<Tx_Bulto_Hilado_Grupo>?> ListaDet(string? Grupo)
        {
            var result = new ServiceResponseList<Tx_Bulto_Hilado_Grupo>();
            try
            {
                var resultData = await _txBultoHiladoGrupoRepository.ListaDet(Grupo);
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

        public async Task<ServiceResponseList<Tx_Bulto_Hilado_Grupo>?> Obtener(int? IdBultoHiladoGrupo)
        {
            var result = new ServiceResponseList<Tx_Bulto_Hilado_Grupo>();
            try
            {
                var resultData = await _txBultoHiladoGrupoRepository.Obtener(IdBultoHiladoGrupo);
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

        public async Task<ServiceResponse<int>> Validar(string? Grupo)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txBultoHiladoGrupoRepository.Validar(Grupo);
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

        public async Task<ServiceResponseList<Tx_Bulto_Hilado_Grupo>?> ObtenerByCode(string? Grupo)
        {
            var result = new ServiceResponseList<Tx_Bulto_Hilado_Grupo>();
            try
            {
                var resultData = await _txBultoHiladoGrupoRepository.ObtenerByCode(Grupo);
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

        public async Task<ServiceResponseList<Tx_Bulto_Hilado_Grupo>?> ListaDetById(int? IdBultoHiladoGrupo)
        {
            var result = new ServiceResponseList<Tx_Bulto_Hilado_Grupo>();
            try
            {
                var resultData = await _txBultoHiladoGrupoRepository.ListaDetById(IdBultoHiladoGrupo);
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

        public async Task<ServiceResponseList<Tx_Bulto_Hilado_Grupo>?> ListaByIdUbicacion(string? CodUbicacion)
        {
            var result = new ServiceResponseList<Tx_Bulto_Hilado_Grupo>();
            try
            {
                var resultData = await _txBultoHiladoGrupoRepository.ListaByIdUbicacion(CodUbicacion);
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

        public async Task<ServiceResponse<int>> UbicarReubicar(Tx_Bulto_Hilado_Grupo tx_Bulto_Hilado_Grupo)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txBultoHiladoGrupoRepository.UbicarReubicar(tx_Bulto_Hilado_Grupo);
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
