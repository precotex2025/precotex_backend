using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Data.Repositories.Implementation.QuejasReclamos;
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

        public async Task<ServiceResponseList<UnidadNegocioDto>?> ObtenerMotivo()
        {
            var result = new ServiceResponseList<UnidadNegocioDto>();
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
    }
}
