using ic.backend.precotex.web.Data.Repositories.Implementation.Laboratorio;
using ic.backend.precotex.web.Data.Repositories.Implementation.Personas;
using ic.backend.precotex.web.Data.Repositories.Laboratorio;
using ic.backend.precotex.web.Entity.Entities.Laboratorio;
using ic.backend.precotex.web.Entity.Entities.Personas;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.Personas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Personas
{
    public class TxPersonasService: ITxPersonasService
    {
        private readonly ITxPersonasRepository _ITxPersonasRepository;

        public TxPersonasService(ITxPersonasRepository ITxPersonasRepository)
        {
            _ITxPersonasRepository = ITxPersonasRepository;
        }

        public async Task<ServiceResponseList<Tx_Personas>?> ObtenerNombre(string Nro_Dni)
        {
            var result = new ServiceResponseList<Tx_Personas>();
            try
            {
                var resultData = await _ITxPersonasRepository.ObtenerNombre(Nro_Dni);
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

        public async Task<ServiceResponse<int>> RegistrarDniFoto(Tx_Personas valores)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _ITxPersonasRepository.RegistrarDniFoto(valores);
                if (resultData.Codigo > 0)
                {
                    result.Success = true;
                    result.CodeTransacc = resultData.Codigo;
                    result.Message = resultData.Mensaje;
                    return result;
                }
                result.Success = false;
                result.CodeTransacc = resultData.Codigo;
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

        public async Task<ServiceResponse<int>> ActualizarDniFoto(Tx_Personas valores)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _ITxPersonasRepository.ActualizarDniFoto(valores);
                if(resultData.Codigo > 0)
                {
                    result.Success = true;
                    result.CodeTransacc = resultData.Codigo;
                    result.Message = resultData.Mensaje;
                    return result;
                }
                result.Success = false;
                result.CodeTransacc = resultData.Codigo;
                result.Message = resultData.Mensaje;
                return result;
            }
            catch (Exception ex)
            {
                result.Success= false;
                result.Message = "Error inesperado " + ex.Message;
                return result;
            }   
        }


        public async Task<ServiceResponseList<Seg_Camara>?> ObtenerDatosRegistro(int Id_Marcacion, string Nro_Dni)
        {
            var result = new ServiceResponseList<Seg_Camara>();
            try
            {
                var resultData = await _ITxPersonasRepository.ObtenerDatosRegistro(Id_Marcacion, Nro_Dni);
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

        public async Task<ServiceResponseList<Seg_Camara>?> ObtenerMarcación1p1()
        {
            var result = new ServiceResponseList<Seg_Camara>();
            try
            {
                var resultData = await _ITxPersonasRepository.ObtenerMarcación1p1();
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
