using ic.backend.precotex.web.Data.Repositories.Implementation.Laboratorio;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.Laboratorio;
using ic.backend.precotex.web.Entity.Entities.RetiroRepuestos;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.Laboratorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace ic.backend.precotex.web.Service.Services.Laboratorio
{
    public class LbColaTrabajoService: ILbColaTrabajoService
    {
        private readonly ILbColaTrabajoRepository _lbColaTrabajoRepository;
        
        public LbColaTrabajoService(ILbColaTrabajoRepository lbColaTrabajoRepository)
        {
            _lbColaTrabajoRepository = lbColaTrabajoRepository;
        }

        /*
            CABECERA 
        */
        public async Task<ServiceResponseList<Lb_ColTra_Cab>?> ListaSDCPorEstado(string Flg_Est_Lab, DateTime Fec_Ini, DateTime Fec_Fin)
        {
            var result = new ServiceResponseList<Lb_ColTra_Cab>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ListaSDCPorEstado(Flg_Est_Lab, Fec_Ini, Fec_Fin);
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



        /*
            DETALLE 
        */
        public async Task<ServiceResponseList<Lb_ColTra_Det>?> ListaColoresSDC(int Corr_Carta)
        {
            var result = new ServiceResponseList<Lb_ColTra_Det>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ListaColoresSDC(Corr_Carta);
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

        public async Task<ServiceResponse<int>> RegistrarDetalleColorSDC(Lb_ColTra_Det lbColaTraDet)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.RegistrarDetalleColorSDC(lbColaTraDet);
                if(resultData.Codigo > 0)
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
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = "Error inesperado " + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponseList<Lb_ColTra_Det>?> LlenarDesplegable()
        {
            var result = new ServiceResponseList<Lb_ColTra_Det>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.LlenarDesplegable();
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
        
        public async Task<ServiceResponseList<Lb_ColTra_Cab_y_Det>?> LlenarGrillaDesplegable(int Corr_Carta, int Sec)
        {
            var result = new ServiceResponseList<Lb_ColTra_Cab_y_Det>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.LlenarGrillaDesplegable(Corr_Carta, Sec);
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
        public async Task<ServiceResponse<int>> ActualizarEstadoDeColor(Lb_ColTra_Det lb_ColTra_Det)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ActualizarEstadoDeColor(lb_ColTra_Det);
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

        public async Task<ServiceResponse<int>> AgregarOpcionColorante(Lb_AgrOpc_Colorantes lb_AgrOpc_Colorantes)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.AgregarOpcionColorante(lb_AgrOpc_Colorantes);
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
        
        public async Task<ServiceResponseList<Lg_Item>?> CargarComboBoxItem()
        {
            var result = new ServiceResponseList<Lg_Item>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.CargarComboBoxItem();
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



        /*
            INFORMACION SDC
        */
        public async Task<ServiceResponseList<Lb_Informe_SDC>?> CargarInformeSDC(int Corr_Carta, int Sec)
        {
            var result = new ServiceResponseList<Lb_Informe_SDC>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.CargarInformeSDC(Corr_Carta, Sec);
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

        /*
            HOJA DE FORMULACION
        */
        public async Task<ServiceResponseList<Lb_AgrOpc_Colorantes>?> CargarGridHojaFormulacion(int Corr_Carta, int Sec)
        {
            var result = new ServiceResponseList<Lb_AgrOpc_Colorantes>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.CargarGridHojaFormulacion(Corr_Carta, Sec);
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
