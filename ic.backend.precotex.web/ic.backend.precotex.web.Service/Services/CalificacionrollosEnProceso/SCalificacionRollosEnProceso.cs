using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Data.Repositories.Implementation.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Entity.Entities.Desglose;
using ic.backend.precotex.web.Entity.Entities.QR;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.CalificacionRollosEnProceso;


namespace ic.backend.precotex.web.Service.Services.CalificacionrollosEnProceso
{
    
    public class SCalificacionRollosEnProceso: ICalificacionRollosEnProcesoService
    {
        private readonly ICalificacionRollosEnProceso _txtCalificacion;

        public SCalificacionRollosEnProceso(ICalificacionRollosEnProceso txtCalificacion)
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

        public async Task<ServiceResponseList<EMaquina>?> ObtenerProveedores()
        {
            var result = new ServiceResponseList<EMaquina>();
            try
            {
                var resultData = await _txtCalificacion.ObtenerProveedores();
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

        public async Task<ServiceResponseList<ERrollosPorPartida>?> BuscarArticuloPorPartida(string partida)
        {
            var result = new ServiceResponseList<ERrollosPorPartida>();
            try
            {
                var resultData = await _txtCalificacion.BuscarArticuloPorPartida(partida);
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

        public async Task<ServiceResponseList<ERrollosPorPartida>?> BuscarRolloPorPartidaDetalle(string partida, string articulo)
        {
            var result = new ServiceResponseList<ERrollosPorPartida>();
            try
            {
                var resultData = await _txtCalificacion.BuscarRolloPorPartidaDetalle(partida, articulo);
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


        public async Task<ServiceResponseList<EPartidaPorRollo>?> updatePartidaPorRollo(string partida,int id)
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

        //REGISTRO QR

        public async Task<ServiceResponseList<E_RegistroQR>?> GrabarQR(E_RegistroQR request)
        {
            var result = new ServiceResponseList<E_RegistroQR>();
            try
            {
                var resultData = await _txtCalificacion.GrabarQR(request);
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

        //REGISTRO DE SERVICIO DE DESGLOSE
        public async Task<ServiceResponseList<String>?> ObtenerDni(string usuario)
        {
            var result = new ServiceResponseList<String>();
            try
            {
                var resultData = await _txtCalificacion.ObtenerDni(usuario);
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

        public async Task<ServiceResponseList<ERrollosPorPartida>?> BuscarPartida(string partida)
        {
            var result = new ServiceResponseList<ERrollosPorPartida>();
            try
            {
                var resultData = await _txtCalificacion.BuscarPartida(partida);
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

        public async Task<ServiceResponseList<E_RegistroDesgloseRequest>?> RegistrarDesglose(E_RegistroDesgloseRequest model)
        {
            var result = new ServiceResponseList<E_RegistroDesgloseRequest>();
            try
            {
                var resultData = await _txtCalificacion.RegistrarDesglose(model);
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

        public async Task<ServiceResponseList<ListaDesgloseDetalle>?> ListarDesglose()
        {
            var result = new ServiceResponseList<ListaDesgloseDetalle>();
            try
            {
                var resultData = await _txtCalificacion.ListarDesglose();
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

        public async Task<ServiceResponseList<E_DesgloseItem>?> ListarDesgloseItem(string id_Desglose)
        {
            var result = new ServiceResponseList<E_DesgloseItem>();
            try
            {
                var resultData = await _txtCalificacion.ListarDesgloseItem(id_Desglose);
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

        public async Task<ServiceResponseList<E_UpdateDesglose>?> ActualizarDesgloseItem(E_UpdateDesglose model)
        {
            var result = new ServiceResponseList<E_UpdateDesglose>();
            try
            {
                var resultData = await _txtCalificacion.ActualizarDesgloseItem(model);
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

        public async Task<ServiceResponseList<E_RegistroDesgloseRequest>?> EliminarDesglose(int id)
        {
            var result = new ServiceResponseList<E_RegistroDesgloseRequest>();
            try
            {
                var resultData = await _txtCalificacion.EliminarDesglose(id);
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

        public async Task<ServiceResponseList<Tx_Maquinas_Gral_QR_P2>?> ObtenerMaquinaQRP2(string CodMaquina)
        {
            var result = new ServiceResponseList<Tx_Maquinas_Gral_QR_P2>();
            try
            {
                var resultData = await _txtCalificacion.ObtenerMaquinaQRP2(CodMaquina);
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
    }
}
