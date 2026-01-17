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
    public class LbColaTrabajoService : ILbColaTrabajoService
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

        public async Task<ServiceResponse<int>> ActualizarEstadoDeColorTricomia(Lb_AgrOpc_Colorantes _lbAgrOpcColorante)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ActualizarEstadoDeColorTricomia(_lbAgrOpcColorante);
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

        public async Task<ServiceResponse<int>> ActualizarEstadoDeColorTricomiaAutolab(Lb_AgrOpc_Colorantes _lbAgrOpcColorante)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ActualizarEstadoDeColorTricomiaAutolab(_lbAgrOpcColorante);
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

        //COPIAR OPCION AGREGADA
        public async Task<ServiceResponse<int>> CopiarOpcionColorante(Lb_AgrOpc_Colorantes lb_AgrOpc_Colorantes)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.CopiarOpcionColorante(lb_AgrOpc_Colorantes);
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

        //ELIMINAR OPCION AGREGADA
        public async Task<ServiceResponse<int>> EliminarOpcionColorante(int Corr_Carta, int Sec, int Correlativo)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.EliminarOpcionColorante(Corr_Carta, Sec, Correlativo);
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

        public async Task<ServiceResponseList<Lb_Colorantes>?> ListarColorantesAgregarOpcion()
        {
            var result = new ServiceResponseList<Lb_Colorantes>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ListarColorantesAgregarOpcion();
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

        public async Task<ServiceResponseList<Lb_Jabonados>?> ListarJabonados()
        {
            var result = new ServiceResponseList<Lb_Jabonados>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ListarJabonados();
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

        public async Task<ServiceResponseList<Lb_Jabonados>?> ListarJabonadosCalculado(decimal Colorante_Total, string Familia)
        {
            var result = new ServiceResponseList<Lb_Jabonados>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ListarJabonadosCalculado(Colorante_Total, Familia);
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

        public async Task<ServiceResponseList<Lb_Fijados>?> ListarFijados()
        {
            var result = new ServiceResponseList<Lb_Fijados>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ListarFijados();
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

        public async Task<ServiceResponseList<Lb_Fijados>?> ListarFijadosCalculado(decimal Colorante_Total, string Familia)
        {
            var result = new ServiceResponseList<Lb_Fijados>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ListarFijadosCalculado(Colorante_Total, Familia);
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

        public async Task<ServiceResponseList<Lb_Colorantes_Componentes_Extra>?> ListarCarbonatoSodaCalculado(decimal Colorante_Total, string Familia, int Com_Cod_Con)
        {
            var result = new ServiceResponseList<Lb_Colorantes_Componentes_Extra>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ListarCarbonatoSodaCalculado(Colorante_Total, Familia, Com_Cod_Con);
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

        //LISTAR COLA AUTOLAB
        public async Task<ServiceResponseList<Lb_ColTra_Det>?> ListarColaAutolab()
        {
            var result = new ServiceResponseList<Lb_ColTra_Det>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ListarColaAutolab();
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

        //ENVIAR A DISPENSAR
        public async Task<ServiceResponse<int>> EnviarADispensado(Lb_AgrOpc_Colorantes lb_AgrOpc_Colorantes)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.EnviarADispensado(lb_AgrOpc_Colorantes);
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

        //LISTAR DISPENSADO
        public async Task<ServiceResponseList<Lb_ColTra_Det>?> ListarDispensado()
        {
            var result = new ServiceResponseList<Lb_ColTra_Det>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ListarDispensado();
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

        //LISTAR AHIBAS
        public async Task<ServiceResponseList<Lb_Ahibas>?> ListaAhibas()
        {
            var result = new ServiceResponseList<Lb_Ahibas>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ListaAhibas();
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

        public async Task<ServiceResponse<int>> CargarAahiba(Lb_AgrOpc_Colorantes _lbAgrOpcColorante)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.CargarAahiba(_lbAgrOpcColorante);
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

        //LISTAR ITEMS EN AHIBA
        public async Task<ServiceResponseList<Lb_ColTra_Det>?> ListarItemsEnAhiba(int Ahi_Id)
        {
            var result = new ServiceResponseList<Lb_ColTra_Det>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ListarItemsEnAhiba(Ahi_Id);
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

        //ACTUALIZAR PH - INI - FIN - JAB
        public async Task<ServiceResponse<int>> ActualizarPH(Lb_ColTra_Det lb_ColTra_Det)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ActualizarPH(lb_ColTra_Det);
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

        public async Task<ServiceResponse<int>> EnviarAutolab(Lb_AgrOpc_Colorantes _lbAgrOpcColorante)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.EnviarAutolab(_lbAgrOpcColorante);
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

        public async Task<ServiceResponse<int>> AgregarAuxiliaresHojaFormulacion(Lb_AgrOpc_Colorantes _lbAgrOpcColorante)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.AgregarAuxiliaresHojaFormulacion(_lbAgrOpcColorante);
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

        public async Task<ServiceResponse<int>> LlenarTextoFinal(Lb_AgrOpc_Colorantes _lbAgrOpcColorante)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.LlenarTextoFinal(_lbAgrOpcColorante);
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

        //LISTAR JABONADO
        public async Task<ServiceResponseList<Lb_ColTra_Det>?> ListarJabonado()
        {
            var result = new ServiceResponseList<Lb_ColTra_Det>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ListarJabonado();
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

        public async Task<ServiceResponseList<Lb_Colorantes_Componentes_Extra>?> ListarFamiliasProceso()
        {
            var result = new ServiceResponseList<Lb_Colorantes_Componentes_Extra>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ListarFamiliasProceso();
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

        public async Task<ServiceResponseList<Lb_AgrOpc_Colorantes>?> CargarColoranteParaCopiar(int Corr_Carta, int Sec, int Correlativo)
        {
            var result = new ServiceResponseList<Lb_AgrOpc_Colorantes>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.CargarColoranteParaCopiar(Corr_Carta, Sec, Correlativo);
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

        public async Task<ServiceResponseList<Lb_AgrOpc_Colorantes>?> CargarColoranteParaDetalle(int Corr_Carta, int Sec, int Correlativo)
        {
            var result = new ServiceResponseList<Lb_AgrOpc_Colorantes>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.CargarColoranteParaDetalle(Corr_Carta, Sec, Correlativo);
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

        public async Task<ServiceResponseList<Lb_Usuarios>?> GetUsuarioWeb(string Cod_Usuario)
        {
            var result = new ServiceResponseList<Lb_Usuarios>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.GetUsuarioWeb(Cod_Usuario);
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
        public async Task<ServiceResponseList<Lb_AgrOpc_Colorantes>?> ListarIngresoManual(int Corr_Carta, int Sec, int Correlativo)
        {
            var result = new ServiceResponseList<Lb_AgrOpc_Colorantes>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ListarIngresoManual(Corr_Carta, Sec, Correlativo);
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

        public async Task<ServiceResponseList<Lb_Reporte>?> CargarDatosReporte(int Corr_Carta, int Sec, int Correlativo)
        {
            var result = new ServiceResponseList<Lb_Reporte>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.CargarDatosReporte(Corr_Carta, Sec, Correlativo);
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

        /***********************************************MANTENIMIENTOS**************************************************/
        public async Task<ServiceResponseList<Lb_Jabonados>?> ListarJabonadoMantenimiento()
        {
            var result = new ServiceResponseList<Lb_Jabonados>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ListarJabonadoMantenimiento();
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


        public async Task<ServiceResponse<int>> RegistrarJabonado(Lb_Jabonados lb_Jabonados)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.RegistrarJabonado(lb_Jabonados);
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

        public async Task<ServiceResponse<int>> ModificarJabonado(Lb_Jabonados lb_Jabonados)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ModificarJabonado(lb_Jabonados);
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
        public async Task<ServiceResponse<int>> DeshabilitarJabonado(Lb_Jabonados lb_Jabonados)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.DeshabilitarJabonado(lb_Jabonados);
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

        public async Task<ServiceResponseList<Lb_Jabonados_Detalle>?> ListarJabonadosDetalleMantenimiento(int Jab_Id)
        {
            var result = new ServiceResponseList<Lb_Jabonados_Detalle>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ListarJabonadosDetalleMantenimiento(Jab_Id);
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

        public async Task<ServiceResponse<int>> RegistrarJabonadoDetalle(Lb_Jabonados_Detalle lb_Jabonados_Detalle)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.RegistrarJabonadoDetalle(lb_Jabonados_Detalle);
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

        public async Task<ServiceResponse<int>> ModificarJabonadoDetalle(Lb_Jabonados_Detalle lb_Jabonados_Detalle)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ModificarJabonadoDetalle(lb_Jabonados_Detalle);
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
        public async Task<ServiceResponse<int>> DeshabilitarJabonadoDetalle(Lb_Jabonados_Detalle lb_Jabonados_Detalle)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.DeshabilitarJabonadoDetalle(lb_Jabonados_Detalle);
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

        public async Task<ServiceResponseList<Lb_Fijados>?> ListarFijadosMantenimiento()
        {
            var result = new ServiceResponseList<Lb_Fijados>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ListarFijadosMantenimiento();
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

        public async Task<ServiceResponse<int>> RegistrarFijado(Lb_Fijados lb_Fijados)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.RegistrarFijado(lb_Fijados);
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

        public async Task<ServiceResponse<int>> ModificarFijado(Lb_Fijados lb_Fijados)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ModificarFijado(lb_Fijados);
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
        public async Task<ServiceResponse<int>> DeshabilitarFijado(Lb_Fijados lb_Fijados)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.DeshabilitarFijado(lb_Fijados);
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

        public async Task<ServiceResponseList<Lb_Fijados_Detalle>?> ListarFijadosDetalleMantenimiento(int Fij_Id)
        {
            var result = new ServiceResponseList<Lb_Fijados_Detalle>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ListarFijadosDetalleMantenimiento(Fij_Id);
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

        public async Task<ServiceResponse<int>> RegistrarFijadoDetalle(Lb_Fijados_Detalle lb_Fijados_Detalle)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.RegistrarFijadoDetalle(lb_Fijados_Detalle);
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

        public async Task<ServiceResponse<int>> ModificarFijadoDetalle(Lb_Fijados_Detalle lb_Fijados_Detalle)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ModificarFijadoDetalle(lb_Fijados_Detalle);
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
        public async Task<ServiceResponse<int>> DeshabilitarFijadoDetalle(Lb_Fijados_Detalle lb_Fijados_Detalle)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.DeshabilitarFijadoDetalle(lb_Fijados_Detalle);
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

        public async Task<ServiceResponse<int>> RegistrarProceso(ComponentesExtra _ComponentesExtra)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.RegistrarProceso(_ComponentesExtra);
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

        public async Task<ServiceResponse<int>> ModificarProceso(ComponentesExtra _ComponentesExtra)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ModificarProceso(_ComponentesExtra);
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
        public async Task<ServiceResponse<int>> DeshabilitarProceso(ComponentesExtra _ComponentesExtra)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.DeshabilitarProceso(_ComponentesExtra);
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

        public async Task<ServiceResponseList<ComponentesExtraValores>?> ListarProcesoValor(string Pro_Cod)
        {
            var result = new ServiceResponseList<ComponentesExtraValores>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ListarProcesoValor(Pro_Cod);
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

        public async Task<ServiceResponse<int>> RegistrarProcesoValor(ComponentesExtraValores _ComponentesExtraValores)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.RegistrarProcesoValor(_ComponentesExtraValores);
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

        public async Task<ServiceResponse<int>> ModificarProcesoValor(ComponentesExtraValores _ComponentesExtraValores)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.ModificarProcesoValor(_ComponentesExtraValores);
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
        public async Task<ServiceResponse<int>> DeshabilitarProcesoValor(ComponentesExtraValores _ComponentesExtraValores)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _lbColaTrabajoRepository.DeshabilitarProcesoValor(_ComponentesExtraValores);
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
