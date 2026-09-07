using ic.backend.precotex.web.Data.Repositories.Implementation.Tintoreria;
using ic.backend.precotex.web.Entity.common;
using ic.backend.precotex.web.Entity.Entities.Tintoreria;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.Tintoreria;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static ic.backend.precotex.web.Entity.Entities.Tintoreria.Ubicaciones;

namespace ic.backend.precotex.web.Service.Services.Tintoreria
{
    public class UbicacionesService : IUbicacionesService
    {
        private readonly IUbicacionesRepository _ubicacionesRepository;
        public UbicacionesService(IUbicacionesRepository ubicacionesRepository)
        {
            _ubicacionesRepository = ubicacionesRepository;
        }

        public async Task<ServiceResponseList<Ubicaciones.ListaBultoUbicaciones>?> ListaBultoUbicaciones(string? Cod_Almacen, string? Codigo_Barra_Grupo)
        {
            var result = new ServiceResponseList<Ubicaciones.ListaBultoUbicaciones>();
            try
            {
                var resultData = await _ubicacionesRepository.ListaBultoUbicaciones(Cod_Almacen, Codigo_Barra_Grupo);
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

        public async Task<ServiceResponse<Ubicaciones.GrupoCreadoResponseDto>> InsertarBultoGrupo(Ubicaciones.InsertarBultoGrupo ubicaciones)
        {
            var result = new ServiceResponse<Ubicaciones.GrupoCreadoResponseDto>();
            try
            {
                // 1. Desempaquetamos la tupla de 3 valores que viene del repositorio dapper
                var (codigo, mensaje, codigoBarraGrupo) = await _ubicacionesRepository.InsertarBultoGrupo(ubicaciones);

                if (codigo > 0)
                {
                    result.Message = mensaje;
                    result.Success = true;
                    result.CodeTransacc = codigo; // Conservamos el ID numérico si tu servicio lo mapea en cabecera

                    // 2. Encapsulamos los datos en el DTO de respuesta dentro de la propiedad 'Data' de tu ServiceResponse
                    result.Element = new GrupoCreadoResponseDto
                    {
                        IdAgrupamiento = codigo,
                        CodigoBarraGrupo = codigoBarraGrupo // Aquí viaja el "G00000000142" o null si fue vinculación
                    };

                    return result;
                }

                result.Message = mensaje;
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
                result.Message = "Ocurrio una excepción: " + ex.Message;
                result.Success = false;
                return result;
            }
        }

        public async Task<ServiceResponse<ServiceResponseTransacSQL>> UbicarGrupoOBulto(Ubicaciones.UbicarGrupoOBulto ubicaciones)
        {
            var result = new ServiceResponse<ServiceResponseTransacSQL>();
            try
            {
                var (codigo, mensaje) = await _ubicacionesRepository.UbicarGrupoOBulto(ubicaciones);

                if (codigo > 0)
                {
                    result.Success = true;
                    result.Message = mensaje;
                    result.CodeTransacc = codigo;
                    result.Element = new ServiceResponseTransacSQL
                    {
                        nCod = codigo,
                        sMsj = mensaje
                    };
                    return result;
                }

                result.Message = mensaje;
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
                result.Message = "Ocurrio una excepción: " + ex.Message;
                result.Success = false;
                return result;
            }
        }

        public async Task<ServiceResponseList<Ubicaciones.ListaAgrupamientosDelDia>?> ListaAgrupamientosDelDia(DateTime? Fec_Creacion, string? Codigo_Barra_Grupo)
        {
            var result = new ServiceResponseList<Ubicaciones.ListaAgrupamientosDelDia>();
            try
            {
                var resultData = await _ubicacionesRepository.ListaAgrupamientosDelDia(Fec_Creacion, Codigo_Barra_Grupo);
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

        public async Task<ServiceResponseList<Ubicaciones.ListaDetalleBultosAgrupados>?> ListaDetalleBultosAgrupados(string? Cod_Almacen, int? Id_Agrupamiento, string? Codigo_Barra_Grupo)
        {
            var result = new ServiceResponseList<Ubicaciones.ListaDetalleBultosAgrupados>();
            try
            {
                var resultData = await _ubicacionesRepository.ListaDetalleBultosAgrupados(Cod_Almacen, Id_Agrupamiento, Codigo_Barra_Grupo);
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

        public async Task<ServiceResponse<Ubicaciones.ConsultaKardexPda>> ConsultaKardexPda(string? Cod_Almacen, string? Codigo_Escaneado)
        {
            var result = new ServiceResponse<Ubicaciones.ConsultaKardexPda>();
            try
            {
                var resultData = await _ubicacionesRepository.ConsultaKardexPda(Cod_Almacen, Codigo_Escaneado);
                if (resultData?.Cabecera == null)
                {
                    result.Success = true;
                    result.Message = "No existe información";
                    return result;
                }

                result.Success = true;
                result.Element = resultData;
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
                result.Message = "Ocurrio una excepción: " + ex.Message;
                result.Success = false;
                return result;
            }
        }
    }
}
