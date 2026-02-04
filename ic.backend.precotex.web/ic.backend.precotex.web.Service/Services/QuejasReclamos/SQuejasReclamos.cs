using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Data.Repositories.Implementation.QuejasReclamos;
using ic.backend.precotex.web.Entity.common;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Entity.Entities.QuejasReclamos;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.QuejasReclamos;
using static ic.backend.precotex.web.Entity.Entities.QuejasReclamos.Clientes;

namespace ic.backend.precotex.web.Service.Services.QuejasReclamos
{
    public class SQuejasReclamos: IQuejasReclamosService
    {
        private readonly IQuejasReclamos _txtIQuejasReclamos;

        public SQuejasReclamos(IQuejasReclamos txtIQuejasReclamos)
        {
            _txtIQuejasReclamos = txtIQuejasReclamos;
        }

        public async Task<ServiceResponseList<Cliente>?> ObtenerClintes()
        {
            var result = new ServiceResponseList<Cliente>();
            try
            {
                var resultData = await _txtIQuejasReclamos.ObtenerClintes();
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

        public async Task<ServiceResponseList<EstadoDto>?> ObtenerEstado()
        {
            var result = new ServiceResponseList<EstadoDto>();
            try
            {
                var resultData = await _txtIQuejasReclamos.ObtenerEstado();
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

        public async Task<ServiceResponseList<MotivoDto>?> ObtenerMotivo()
        {
            var result = new ServiceResponseList<MotivoDto>();
            try
            {
                var resultData = await _txtIQuejasReclamos.ObtenerMotivo();
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

        public async Task<ServiceResponseList<UnidadNegocioDto>?> ObtenerUnidadNegocio()
        {
            var result = new ServiceResponseList<UnidadNegocioDto>();
            try
            {
                var resultData = await _txtIQuejasReclamos.ObtenerUnidadNegocio();
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

        public async Task<ServiceResponseList<ResponsableDto>?> ObtenerResponsable()
        {
            var result = new ServiceResponseList<ResponsableDto>();
            try
            {
                var resultData = await _txtIQuejasReclamos.ObtenerResponsable();
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

        public async Task<ServiceResponseList<ReclamoClienteDto>?> GuardarReclamo(List<ReclamoClienteDto> reclamo, bool isNew)
        {
            var result = new ServiceResponseList<ReclamoClienteDto>();
            try
            {
                var resultData = await _txtIQuejasReclamos.GuardarReclamo(reclamo, isNew);
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

        public async Task<ServiceResponseList<FiltroReclamoDto>?> ObtenerReclamos(FiltroReclamoDto filtro)
        {
            var result = new ServiceResponseList<FiltroReclamoDto>();
            try
            {
                var resultData = await _txtIQuejasReclamos.ObtenerReclamos(filtro);
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

        public async Task<ServiceResponseList<ReclamoClienteDto>?> ObtenerDetReclamos(string nroCaso)
        {
            var result = new ServiceResponseList<ReclamoClienteDto>();
            try
            {
                var resultData = await _txtIQuejasReclamos.ObtenerDetReclamos(nroCaso);
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

        public async Task<ServiceResponseList<string>?> EliminarReclamoDetalle(string id)
        {
            var result = new ServiceResponseList<string>();
            try
            {
                var resultData = await _txtIQuejasReclamos.EliminarReclamoDetalle(id);
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

        public async Task<ServiceResponseList<bool>?> EliminarReclamos(string nroCaso)
        {
            var result = new ServiceResponseList<bool>();
            try
            {
                var resultData = await _txtIQuejasReclamos.EliminarReclamos(nroCaso);
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

        public async Task<ServiceResponseList<ArticuloDto>?> BuscarPorPartida(string partida)
        {
            var result = new ServiceResponseList<ArticuloDto>();
            try
            {
                var resultData = await _txtIQuejasReclamos.BuscarPorPartida(partida);
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

        public async Task<ServiceResponseList<UnidadNegocioDto>?> ListaUnidadNegocio()
        {
            var result = new ServiceResponseList<UnidadNegocioDto>();
            try
            {
                var resultData = await _txtIQuejasReclamos.ListaUnidadNegocio();
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

        public async Task<ServiceResponseList<AreasDto>?> ListaAreasCalidad()
        {
            var result = new ServiceResponseList<AreasDto>();
            try
            {
                var resultData = await _txtIQuejasReclamos.ListaAreasCalidad();
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

        public async Task<ServiceResponse<int>> AvanzaEstadoReclamo(int iId)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txtIQuejasReclamos.AvanzaEstadoReclamo(iId);
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

        public async Task<ServiceResponse<int>> ProcesoConfirmarReclamo(string sNroCaso, string sNombreArchivoCalidad, string sObservacionCalidad, string sCodAreaResponsableCalidad, string sCod_Usuario)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txtIQuejasReclamos.ProcesoConfirmarReclamo(sNroCaso, sNombreArchivoCalidad, sObservacionCalidad, sCodAreaResponsableCalidad, sCod_Usuario);
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

        public async Task<ServiceResponseList<ReclamoTipoConsecuenciaDto>?> ListaTipoConsecuencia()
        {
            var result = new ServiceResponseList<ReclamoTipoConsecuenciaDto>();
            try
            {
                var resultData = await _txtIQuejasReclamos.ListaTipoConsecuencia();
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

        public async Task<ServiceResponseList<ReclamoSubTipoDevolucion>?> ListaSubTipoDevolucion(string sCod_Tipo_Consecuencia)
        {
            var result = new ServiceResponseList<ReclamoSubTipoDevolucion>();
            try
            {
                var resultData = await _txtIQuejasReclamos.ListaSubTipoDevolucion(sCod_Tipo_Consecuencia);
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

        public async Task<ServiceResponse<int>> ProcesoCerrarReclamo(string sNroCaso, string sCod_Tipo_Consecuencia, string sCod_SubTipo_Devolucion, string sFlg_NotaCredito, string sFlg_FleteAereo, string sObservacion_Comercial_Cierre, string sCod_Usuario)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txtIQuejasReclamos.ProcesoCerrarReclamo(sNroCaso, sCod_Tipo_Consecuencia, sCod_SubTipo_Devolucion, sFlg_NotaCredito, sFlg_FleteAereo, sObservacion_Comercial_Cierre, sCod_Usuario);
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

        public async Task<ServiceResponseList<ReclamoUsuarioAreaDto>?> ObtieneUsuarioArea(string Cod_Trabajador)
        {
            var result = new ServiceResponseList<ReclamoUsuarioAreaDto>();
            try
            {
                var resultData = await _txtIQuejasReclamos.ObtieneUsuarioArea(Cod_Trabajador);
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

        public async Task<ServiceResponseList<InformeCalidadDto>?> ObtieneDetalleInformeCalidad(int Id)
        {
            var result = new ServiceResponseList<InformeCalidadDto>();
            try
            {
                var resultData = await _txtIQuejasReclamos.ObtieneDetalleInformeCalidad(Id);
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

        public async Task<ServiceResponseList<InformeComercialDto>?> ObtieneDetalleInformeComercial(int Id)
        {
            var result = new ServiceResponseList<InformeComercialDto>();
            try
            {
                var resultData = await _txtIQuejasReclamos.ObtieneDetalleInformeComercial(Id);
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

        public async Task<ServiceResponseList<ReclamoClienteEstadoDto>?> ListaEstados()
        {
            var result = new ServiceResponseList<ReclamoClienteEstadoDto>();
            try
            {
                var resultData = await _txtIQuejasReclamos.ListaEstados();
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

        public async Task<ServiceResponseList<ReclamoExportarDto>?> ExportarReclamo(FiltroReclamoDto filtro)
        {
            var result = new ServiceResponseList<ReclamoExportarDto>();
            try
            {
                var resultData = await _txtIQuejasReclamos.ExportarReclamo(filtro);
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

        public async Task<ServiceResponseList<dtoGeneral>?> ObtieneTemporada(string sCodCliente)
        {
            var result = new ServiceResponseList<dtoGeneral>();
            try
            {
                var resultData = await _txtIQuejasReclamos.ObtieneTemporada(sCodCliente);
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

        public async Task<ServiceResponseList<dtoGeneral>?> ObtieneEstilo(string sCodCliente, string sTemporada)
        {
            var result = new ServiceResponseList<dtoGeneral>();
            try
            {
                var resultData = await _txtIQuejasReclamos.ObtieneEstilo(sCodCliente, sTemporada);
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

        public async Task<ServiceResponse<int>> ProcesoReenviaReclamo(int iId)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txtIQuejasReclamos.ProcesoReenviaReclamo(iId);
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
