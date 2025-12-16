using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Data.Repositories.Implementation.CalificacionRollosFinal;
using ic.backend.precotex.web.Entity.Entities.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Entity.Entities.ReporteNC;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.CalificacionRollosFinal;
using Microsoft.Graph.Models;
using Org.BouncyCastle.Ocsp;

namespace ic.backend.precotex.web.Service.Services.CalificacionrollosFinal
{
    public class SCalificacionRolloFinal: ICalificacionRollosFinalService
    {
        private readonly ICalificacionRolloFinal _txtCalificacion;

        public SCalificacionRolloFinal(ICalificacionRolloFinal txtCalificacion)
        {
            _txtCalificacion = txtCalificacion;

        }

        public async Task<ServiceResponseList<EDefectos>?> ObtenerDefecto(EDefectos filtro)
        {
            var result = new ServiceResponseList<EDefectos>();
            try
            {
                var resultData = await _txtCalificacion.ObtenerDefecto(filtro);
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

        public async Task<ServiceResponseList<EMaquina>?> ObtenerMaquina()
        {
            var result = new ServiceResponseList<EMaquina>();
            try
            {
                var resultData = await _txtCalificacion.ObtenerMaquina();
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

        public async Task<ServiceResponseList<EAuditor>?> ObtenerSupervisor()
        {
            var result = new ServiceResponseList<EAuditor>();
            try
            {
                var resultData = await _txtCalificacion.ObtenerSupervisor();
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

        public async Task<ServiceResponseList<EAuditor>?> ObtenerAuditor()
        {
            var result = new ServiceResponseList<EAuditor>();
            try
            {
                var resultData = await _txtCalificacion.ObtenerAuditor();
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

        public async Task<ServiceResponseList<ETurno>?> ObtenerTurno()
        {
            var result = new ServiceResponseList<ETurno>();
            try
            {
                var resultData = await _txtCalificacion.ObtenerTurno();
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

        public async Task<ServiceResponseList<EUnidadNegocio>?> ObtenerUnidadNegocio()
        {
            var result = new ServiceResponseList<EUnidadNegocio>();
            try
            {
                var resultData = await _txtCalificacion.ObtenerUnidadNegocio();
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

        public async Task<ServiceResponseList<EEstadoPartida>?> ObtenerEstadoPartida()
        {
            var result = new ServiceResponseList<EEstadoPartida>();
            try
            {
                var resultData = await _txtCalificacion.ObtenerEstadoPartida();
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

        public async Task<ServiceResponseList<EEstadoPartida>?> ObtenerProcesoAuditado()
        {
            var result = new ServiceResponseList<EEstadoPartida>();
            try
            {
                var resultData = await _txtCalificacion.ObtenerProcesoAuditado();
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

        public async Task<ServiceResponseList<ECalificacion>?> ObtenerCalificacion()
        {
            var result = new ServiceResponseList<ECalificacion>();
            try
            {
                var resultData = await _txtCalificacion.ObtenerCalificacion();
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

        public async Task<ServiceResponseList<ECalificacion>?> ObtenerEstadoProceso()
        {
            var result = new ServiceResponseList<ECalificacion>();
            try
            {
                var resultData = await _txtCalificacion.ObtenerEstadoProceso();
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

        public async Task<ServiceResponseList<ERrollosPorPartida>?> BuscarPorPartida(string partida)
        {
            var result = new ServiceResponseList<ERrollosPorPartida>();
            try
            {
                var resultData = await _txtCalificacion.BuscarPorPartida(partida);
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

        public async Task<ServiceResponseList<ERrollosPorPartida>?> BuscarRolloPorPartidaDetalle(string partida, string articulo, string sObs, string sCodUsu, string sReco, string sIns, string sResDig, string sObsRec, string sCodCal, string sCodTel, int Reproceso, string Maquina)
        {
            var result = new ServiceResponseList<ERrollosPorPartida>();
            try
            {
                var resultData = await _txtCalificacion.BuscarRolloPorPartidaDetalle(partida, articulo,sObs,sCodUsu,sReco,sIns,sResDig,sObsRec,sCodCal, sCodTel, Reproceso, Maquina);
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

        public async Task<ServiceResponseList<EPartidaCab>?> GuardarPartida(EPartidaCab partida)
        {
            var result = new ServiceResponseList<EPartidaCab>();
            try
            {
                var resultData = await _txtCalificacion.GuardarPartida(partida);
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

        public async Task<ServiceResponseList<EPartidaPorRollo>?> BuscarPartidaPorRollo(string partida, string usuario)
        {
            var result = new ServiceResponseList<EPartidaPorRollo>();
            try
            {
                var resultData = await _txtCalificacion.BuscarPartidaPorRollo(partida, usuario);
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

        public async Task<ServiceResponseList<EPartidaPorRollo>?> updatePartidaPorRollo(string partida, int id)
        {
            var result = new ServiceResponseList<EPartidaPorRollo>();
            try
            {
                var resultData = await _txtCalificacion.updatePartidaPorRollo(partida, id);
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

        public async Task<ServiceResponseList<EUnionRollos>?> ObtenerDatosUnionRollos(EUnionRollos filtro)
        {
            var result = new ServiceResponseList<EUnionRollos>();
            try
            {
                var resultData = await _txtCalificacion.ObtenerDatosUnionRollos(filtro);
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

        public async Task<ServiceResponseList<EGuardarUnioRollo>?> guardarDatosUnionRollos(EGuardarUnioRollo unionRollos)
        {
            var result = new ServiceResponseList<EGuardarUnioRollo>();
            try
            {
                var resultData = await _txtCalificacion.guardarDatosUnionRollos(unionRollos);
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

        public async Task<ServiceResponseList<EDefectosRegistrados>?> ObtenerDefectosRegistradosPorRollo(string Cod_OrdTra, string Cod_Tela, string PrefijoMaquina, string CodigoRollo)
        {
            var result = new ServiceResponseList<EDefectosRegistrados>();
            try
            {
                var resultData = await _txtCalificacion.ObtenerDefectosRegistradosPorRollo(Cod_OrdTra, Cod_Tela, PrefijoMaquina, CodigoRollo);
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

        public async Task<ServiceResponseList<EPartidaCab>?> GuardarDefectosPartida(EPartidaCab partida)
        {
            var result = new ServiceResponseList<EPartidaCab>();
            try
            {
                var resultData = await _txtCalificacion.GuardarDefectosPartida(partida);
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

        public async Task<ServiceResponse<int>> EliminarDefectoRollo(string CodOrdTra, string CodigoRollo, string CodMotivo)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txtCalificacion.EliminarDefectoRollo(CodOrdTra, CodigoRollo, CodMotivo);
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

        public async Task<ServiceResponseList<EReproceso>?> ObtenerReproceso()
        {
            var result = new ServiceResponseList<EReproceso>();
            try
            {
                var resultData = await _txtCalificacion.ObtenerReproceso();
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

        public async Task<ServiceResponse<int>> RegistrarImagenPorRollo(string Img_Cod_OrdTra, string Img_Cod_Rollo, string Img_Des)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txtCalificacion.RegistrarImagenPorRollo(Img_Cod_OrdTra, Img_Cod_Rollo, Img_Des);
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


        public async Task<ServiceResponseList<EImagenes>?> ObtenerImagenes(string Img_Cod_OrdTra, string Img_Cod_Rollo)
        {
            var result = new ServiceResponseList<EImagenes>();
            try
            {
                var resultData = await _txtCalificacion.ObtenerImagenes(Img_Cod_OrdTra, Img_Cod_Rollo);
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



    }

}
