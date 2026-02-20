using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.Laboratorio;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.Laboratorio;


namespace ic.backend.precotex.web.Api.Controllers.Laboratorio
{
    [Route("api/[controller]")]
    [ApiController]
    public class LbColaTrabajoController : ControllerBase
    {
        public readonly ILbColaTrabajoService _LbColaTrabajoService;

        public LbColaTrabajoController(ILbColaTrabajoService LbColaTrabajoService)
        {
            _LbColaTrabajoService = LbColaTrabajoService;
        }

        /*
            CABECERA
        */

        //OBTIENE LISTA DE COLA DE TRABAJO
        [HttpGet]
        [Route("getListaSDCPorEstado")]
        public async Task<IActionResult> getListaSDCPorEstado(string Flg_Est_Lab, DateTime Fec_Ini, DateTime Fec_Fin, string Usr_Cod)
        {
            var result = await _LbColaTrabajoService.ListaSDCPorEstado(Flg_Est_Lab, Fec_Ini, Fec_Fin, Usr_Cod);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        /*
            DETALLE 
        */
        //OBTIENE COLORES DE UNA SDC 
        [HttpGet]
        [Route("getListaSDCDetalle")]
        public async Task<IActionResult> getListaSDCDetalle(int Corr_Carta)
        {
            var result = await _LbColaTrabajoService.ListaColoresSDC(Corr_Carta);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarDetalleColorSDC")]
        public async Task<IActionResult> RegistrarDetalleColorSDC([FromBody] LbColaTrabajoParameter_Detalle parametros)
        {
            Lb_ColTra_Det _lbColaTrabajoDet = new Lb_ColTra_Det
            {
                Corr_Carta = parametros.Corr_Carta ?? 0,
                Sec = parametros.Sec ?? 0,
                Cur_Ten = parametros.Cur_Ten ?? 0,
                Usr_Cod = parametros.Usr_Cod ?? "",
            };

            var result = await _LbColaTrabajoService.RegistrarDetalleColorSDC(_lbColaTrabajoDet);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getLlenarDesplegable")]
        public async Task<IActionResult> getLlenarDesplegable()
        {
            var result = await _LbColaTrabajoService.LlenarDesplegable();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getLlenarGrillaDesplegable")]
        public async Task<IActionResult> getLlenarGrillaDesplegable(int Corr_Carta, int Sec)
        {
            var result = await _LbColaTrabajoService.LlenarGrillaDesplegable(Corr_Carta, Sec);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarEstadoDeColor")]
        public async Task<IActionResult> patchActualizarEstadoDeColor([FromBody] Lb_ColTra_Det parametros)
        {
            Lb_ColTra_Det _lbColaTraDet = new Lb_ColTra_Det
            {
                Corr_Carta = parametros.Corr_Carta,
                Sec = parametros.Sec,
                Flg_Est_Lab = parametros.Flg_Est_Lab
            };

            var result = await _LbColaTrabajoService.ActualizarEstadoDeColor(_lbColaTraDet);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarEstadoDeColorTricomia")]
        public async Task<IActionResult> patchActualizarEstadoDeColorTricomia([FromBody] Lb_AgrOpc_Colorantes parametros)
        {
            Lb_AgrOpc_Colorantes _lbAgrOpcColorante = new Lb_AgrOpc_Colorantes
            {
                Corr_Carta = parametros.Corr_Carta,
                Sec = parametros.Sec,
                Correlativo = parametros.Correlativo,
                Flg_Est_Lab = parametros.Flg_Est_Lab
            };

            var result = await _LbColaTrabajoService.ActualizarEstadoDeColorTricomia(_lbAgrOpcColorante);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarEstadoDeColorTricomiaAutolab")]
        public async Task<IActionResult> patchActualizarEstadoDeColorTricomiaAutolab([FromBody] Lb_AgrOpc_Colorantes parametros)
        {
            Lb_AgrOpc_Colorantes _lbAgrOpcColorante = new Lb_AgrOpc_Colorantes
            {
                Corr_Carta = parametros.Corr_Carta,
                Sec = parametros.Sec,
                Correlativo = parametros.Correlativo,
                Flg_Est_Autolab = parametros.Flg_Est_Autolab
            };

            var result = await _LbColaTrabajoService.ActualizarEstadoDeColorTricomiaAutolab(_lbAgrOpcColorante);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postAgregarOpcionColorante")]
        public async Task<IActionResult> AgregarOpcionColorante([FromBody] Lb_AgrOpc_Colorantes parametros)
        {
            Lb_AgrOpc_Colorantes _lb_AgrOpc_Colorantes = new Lb_AgrOpc_Colorantes
            {
                Corr_Carta = parametros.Corr_Carta,
                Sec = parametros.Sec,
                Procedencia = parametros.Procedencia,
                Correlativo = parametros.Correlativo,
                Col_Cod = parametros.Col_Cod,
                Por_Ini = parametros.Por_Ini,
                Por_Aju = parametros.Por_Aju,
                Por_Fin = parametros.Por_Fin,
                Can_Jabo = parametros.Can_Jabo,
                Cur_Jabo = parametros.Cur_Jabo,
                Fijado = parametros.Fijado,
                Acidulado = parametros.Acidulado,
                Rel_Ban = parametros.Rel_Ban,
                Pes_Mue = parametros.Pes_Mue,
                Volumen = parametros.Volumen,
                Car_Gr = parametros.Car_Gr,
                Car_Por = parametros.Car_Por,
                Sod_Gr = parametros.Sod_Gr,
                Sod_Por = parametros.Sod_Por,
                Familia = parametros.Familia,
                Cambio = parametros.Cambio
            };

            var result = await _LbColaTrabajoService.AgregarOpcionColorante(parametros);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getCargarComboBoxItem")]
        public async Task<IActionResult> getCargarComboBoxItem()
        {
            var result = await _LbColaTrabajoService.CargarComboBoxItem();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getCargarInformeSDC")]
        public async Task<IActionResult> getCargarInformeSDC(int Corr_Carta, int Sec)
        {
            var result = await _LbColaTrabajoService.CargarInformeSDC(Corr_Carta, Sec);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getCargarGridHojaFormulacion")]
        public async Task<IActionResult> getCargarGridHojaFormulacion(int Corr_Carta, int Sec)
        {
            var result = await _LbColaTrabajoService.CargarGridHojaFormulacion(Corr_Carta, Sec);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postCopiarOpcionColorante")]
        public async Task<IActionResult> postCopiarOpcionColorante([FromBody] Lb_AgrOpc_Colorantes parametros)
        {
            Lb_AgrOpc_Colorantes _lb_AgrOpc_Colorantes = new Lb_AgrOpc_Colorantes
            {
                Corr_Carta = parametros.Corr_Carta,
                Sec = parametros.Sec,
                Correlativo = parametros.Correlativo,
            };

            var result = await _LbColaTrabajoService.CopiarOpcionColorante(parametros);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpDelete]
        [Route("deleteEliminarOpcionColorante")]
        public async Task<IActionResult> deleteEliminarOpcionColorante(int Corr_Carta, int Sec, int Correlativo)
        {
            var result = await _LbColaTrabajoService.EliminarOpcionColorante(Corr_Carta, Sec, Correlativo);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarColorantesAgregarOpcion")]
        public async Task<IActionResult> getListarColorantesAgregarOpcion()
        {
            var result = await _LbColaTrabajoService.ListarColorantesAgregarOpcion();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarJabonados")]
        public async Task<IActionResult> getListarJabonados()
        {
            var result = await _LbColaTrabajoService.ListarJabonados();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarJabonadosCalculado")]
        public async Task<IActionResult> getListarJabonadosCalculado(decimal Colorante_Total, string Familia)
        {
            var result = await _LbColaTrabajoService.ListarJabonadosCalculado(Colorante_Total, Familia);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarFijados")]
        public async Task<IActionResult> getListarFijados()
        {
            var result = await _LbColaTrabajoService.ListarFijados();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarFijadosCalculado")]
        public async Task<IActionResult> getListarFijadosCalculado(decimal Colorante_Total, string Familia)
        {
            var result = await _LbColaTrabajoService.ListarFijadosCalculado(Colorante_Total, Familia);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarCarbonatoSodaCalculado")]
        public async Task<IActionResult> getListarCarbonatoSodaCalculado(decimal Colorante_Total, string Familia, int Com_Cod_Con)
        {
            var result = await _LbColaTrabajoService.ListarCarbonatoSodaCalculado(Colorante_Total, Familia, Com_Cod_Con);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarColaAutolab")]
        public async Task<IActionResult> getListarColaAutolab()
        {
            var result = await _LbColaTrabajoService.ListarColaAutolab();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchEnviarADispensado")]
        public async Task<IActionResult> patchEnviarADispensado([FromBody] Lb_AgrOpc_Colorantes parametros)
        {
            Lb_AgrOpc_Colorantes _lbAgrOpcColorante = new Lb_AgrOpc_Colorantes
            {
                Corr_Carta = parametros.Corr_Carta,
                Sec = parametros.Sec,
                Correlativo = parametros.Correlativo,
                Posicion = parametros.Posicion
            };

            var result = await _LbColaTrabajoService.EnviarADispensado(_lbAgrOpcColorante);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarDispensado")]
        public async Task<IActionResult> getListarDispensado()
        {
            var result = await _LbColaTrabajoService.ListarDispensado();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaAhibas")]
        public async Task<IActionResult> getListaAhibas()
        {
            var result = await _LbColaTrabajoService.ListaAhibas();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchCargarAahiba")]
        public async Task<IActionResult> patchCargarAahiba([FromBody] Lb_AgrOpc_Colorantes parametros)
        {
            Lb_AgrOpc_Colorantes _lbAgrOpcColorante = new Lb_AgrOpc_Colorantes
            {
                Corr_Carta = parametros.Corr_Carta,
                Sec = parametros.Sec,
                Correlativo = parametros.Correlativo,
                Ahi_Id = parametros.Ahi_Id,
                Nro_Tubo = parametros.Nro_Tubo
            };

            var result = await _LbColaTrabajoService.CargarAahiba(_lbAgrOpcColorante);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarItemsEnAhiba")]
        public async Task<IActionResult> getListarItemsEnAhiba(int Ahi_Id)
        {
            var result = await _LbColaTrabajoService.ListarItemsEnAhiba(Ahi_Id);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarPH")]
        public async Task<IActionResult> patchActualizarPH([FromBody] Lb_ColTra_Det parametros)
        {
            Lb_ColTra_Det _lbColTraDet = new Lb_ColTra_Det
            {
                Corr_Carta = parametros.Corr_Carta,
                Sec = parametros.Sec,
                Correlativo = parametros.Correlativo,
                Tip_Ph = parametros.Tip_Ph,
                JabonadoIndex = parametros.JabonadoIndex,
                Ph_Val = parametros.Ph_Val
            };

            var result = await _LbColaTrabajoService.ActualizarPH(_lbColTraDet);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchEnviarAutolab")]
        public async Task<IActionResult> patchEnviarAutolab([FromBody] Lb_AgrOpc_Colorantes parametros)
        {
            Lb_AgrOpc_Colorantes _lbAgrOpcColorante = new Lb_AgrOpc_Colorantes
            {
                Corr_Carta = parametros.Corr_Carta,
                Sec = parametros.Sec,
                Correlativo = parametros.Correlativo
            };

            var result = await _LbColaTrabajoService.EnviarAutolab(_lbAgrOpcColorante);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postAgregarAuxiliaresHojaFormulacion")]
        public async Task<IActionResult> postAgregarAuxiliaresHojaFormulacion([FromBody] Lb_AgrOpc_Colorantes parametros)
        {
            Lb_AgrOpc_Colorantes _lbAgrOpcColorante = new Lb_AgrOpc_Colorantes
            {
                Corr_Carta = parametros.Corr_Carta,
                Sec = parametros.Sec,
                Correlativo = parametros.Correlativo,
                Familia = parametros.Familia,
                Cambio = parametros.Cambio,
                ProcedenciaHardCodeada = parametros.ProcedenciaHardCodeada
            };

            var result = await _LbColaTrabajoService.AgregarAuxiliaresHojaFormulacion(_lbAgrOpcColorante);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postLlenarTextoFinal")]
        public async Task<IActionResult> postLlenarTextoFinal([FromBody] Lb_AgrOpc_Colorantes parametros)
        {
            Lb_AgrOpc_Colorantes _lbAgrOpcColorante = new Lb_AgrOpc_Colorantes
            {
                Corr_Carta = parametros.Corr_Carta,
                Sec = parametros.Sec,
                Correlativo = parametros.Correlativo
            };

            var result = await _LbColaTrabajoService.LlenarTextoFinal(_lbAgrOpcColorante);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarJabonado")]
        public async Task<IActionResult> getListarJabonado()
        {
            var result = await _LbColaTrabajoService.ListarJabonado();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarJabonadoExcluido")]
        public async Task<IActionResult> ListarJabonadoExcluido()
        {
            var result = await _LbColaTrabajoService.ListarJabonadoExcluido();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarFamiliasProceso")]
        public async Task<IActionResult> getListarFamiliasProceso()
        {
            var result = await _LbColaTrabajoService.ListarFamiliasProceso();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getCargarColoranteParaCopiar")]
        public async Task<IActionResult> getCargarColoranteParaCopiar(int Corr_Carta, int Sec, int Correlativo)
        {
            var result = await _LbColaTrabajoService.CargarColoranteParaCopiar(Corr_Carta, Sec, Correlativo);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getCargarColoranteParaDetalle")]
        public async Task<IActionResult> getCargarColoranteParaDetalle(int Corr_Carta, int Sec, int Correlativo)
        {
            var result = await _LbColaTrabajoService.CargarColoranteParaDetalle(Corr_Carta, Sec, Correlativo);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getGetUsuarioWeb")]
        public async Task<IActionResult> getGetUsuarioWeb(string Cod_Usuario)
        {
            var result = await _LbColaTrabajoService.GetUsuarioWeb(Cod_Usuario);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarIngresoManual")]
        public async Task<IActionResult> getListarIngresoManual(int Corr_Carta, int Sec, int Correlativo)
        {
            var result = await _LbColaTrabajoService.ListarIngresoManual(Corr_Carta, Sec, Correlativo);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getCargarDatosReporte")]
        public async Task<IActionResult> getCargarDatosReporte(int Corr_Carta, int Sec, int Correlativo)
        {
            var result = await _LbColaTrabajoService.CargarDatosReporte(Corr_Carta, Sec, Correlativo);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }


        /***********************************************MANTENIMIENTOS**************************************************/

        [HttpGet]
        [Route("getListarJabonadoMantenimiento")]
        public async Task<IActionResult> getListarJabonadoMantenimiento()
        {
            var result = await _LbColaTrabajoService.ListarJabonadoMantenimiento();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarJabonado")]
        public async Task<IActionResult> postRegistrarJabonado([FromBody] Lb_Jabonados parametros)
        {
            Lb_Jabonados _lbJabonados = new Lb_Jabonados
            {
                Jab_Des = parametros.Jab_Des,
                Usr_Reg = parametros.Usr_Reg
            };

            var result = await _LbColaTrabajoService.RegistrarJabonado(_lbJabonados);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchModificarJabonado")]
        public async Task<IActionResult> patchModificarJabonado([FromBody] Lb_Jabonados parametros)
        {
            Lb_Jabonados _lbJabonados = new Lb_Jabonados
            {
                Jab_Id = parametros.Jab_Id,
                Jab_Des = parametros.Jab_Des,
                Usr_Mod = parametros.Usr_Mod
            };

            var result = await _LbColaTrabajoService.ModificarJabonado(_lbJabonados);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchDeshabilitarJabonado")]
        public async Task<IActionResult> patchDeshabilitarJabonado([FromBody] Lb_Jabonados parametros)
        {
            Lb_Jabonados _lbJabonados = new Lb_Jabonados
            {
                Jab_Id = parametros.Jab_Id,
                Flg_Status = parametros.Flg_Status
            };

            var result = await _LbColaTrabajoService.DeshabilitarJabonado(_lbJabonados);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarJabonadosDetalleMantenimiento")]
        public async Task<IActionResult> getListarJabonadosDetalleMantenimiento(int Jab_Id)
        {
            var result = await _LbColaTrabajoService.ListarJabonadosDetalleMantenimiento(Jab_Id);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarJabonadoDetalle")]
        public async Task<IActionResult> postRegistrarJabonadoDetalle([FromBody] Lb_Jabonados_Detalle parametros)
        {
            Lb_Jabonados_Detalle _lbJabonados_Detalle = new Lb_Jabonados_Detalle
            {
                Jab_Id = parametros.Jab_Id,
                Jab_Ran_Ini = parametros.Jab_Ran_Ini,
                Jab_Ran_Fin = parametros.Jab_Ran_Fin,
                Jab_Can = parametros.Jab_Can,
                Familia = parametros.Familia,
                Jab_Ran_Ini_Org = parametros.Jab_Ran_Ini_Org,
                Familia_Org = parametros.Familia_Org,
                Usr_Mod = parametros.Usr_Mod
            };

            var result = await _LbColaTrabajoService.RegistrarJabonadoDetalle(_lbJabonados_Detalle);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchModificarJabonadoDetalle")]
        public async Task<IActionResult> patchModificarJabonadoDetalle([FromBody] Lb_Jabonados_Detalle parametros)
        {
            Lb_Jabonados_Detalle _lbJabonados_Detalle = new Lb_Jabonados_Detalle
            {
                Jab_Id = parametros.Jab_Id,
                Jab_Ran_Ini = parametros.Jab_Ran_Ini,
                Jab_Ran_Fin = parametros.Jab_Ran_Fin,
                Jab_Can = parametros.Jab_Can,
                Familia = parametros.Familia,
                Jab_Ran_Ini_Org = parametros.Jab_Ran_Ini_Org,
                Familia_Org = parametros.Familia_Org,
                Usr_Mod = parametros.Usr_Mod
            };

            var result = await _LbColaTrabajoService.ModificarJabonadoDetalle(_lbJabonados_Detalle);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchDeshabilitarJabonadoDetalle")]
        public async Task<IActionResult> patchDeshabilitarJabonadoDetalle([FromBody] Lb_Jabonados_Detalle parametros)
        {
            Lb_Jabonados_Detalle _lbJabonados_Detalle = new Lb_Jabonados_Detalle
            {
                Jab_Id = parametros.Jab_Id,
                Jab_Ran_Ini = parametros.Jab_Ran_Ini,
                Familia = parametros.Familia,
                Flg_Status = parametros.Flg_Status
            };

            var result = await _LbColaTrabajoService.DeshabilitarJabonadoDetalle(_lbJabonados_Detalle);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarFijadosMantenimiento")]
        public async Task<IActionResult> getListarFijadosMantenimiento()
        {
            var result = await _LbColaTrabajoService.ListarFijadosMantenimiento();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarFijado")]
        public async Task<IActionResult> postRegistrarFijado([FromBody] Lb_Fijados parametros)
        {
            Lb_Fijados _lbFijados = new Lb_Fijados
            {
                Fij_Des = parametros.Fij_Des,
                Usr_Reg = parametros.Usr_Reg
            };

            var result = await _LbColaTrabajoService.RegistrarFijado(_lbFijados);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchModificarFijado")]
        public async Task<IActionResult> patchModificarFijado([FromBody] Lb_Fijados parametros)
        {
            Lb_Fijados _lbFijados = new Lb_Fijados
            {
                Fij_Id = parametros.Fij_Id,
                Fij_Des = parametros.Fij_Des,
                Usr_Mod = parametros.Usr_Mod
            };

            var result = await _LbColaTrabajoService.ModificarFijado(_lbFijados);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchDeshabilitarFijado")]
        public async Task<IActionResult> patchDeshabilitarFijado([FromBody] Lb_Fijados parametros)
        {
            Lb_Fijados _lbFijados = new Lb_Fijados
            {
                Fij_Id = parametros.Fij_Id,
                Flg_Status = parametros.Flg_Status
            };

            var result = await _LbColaTrabajoService.DeshabilitarFijado(_lbFijados);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarFijadosDetalleMantenimiento")]
        public async Task<IActionResult> getListarFijadosDetalleMantenimiento(int Fij_Id)
        {
            var result = await _LbColaTrabajoService.ListarFijadosDetalleMantenimiento(Fij_Id);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarFijadoDetalle")]
        public async Task<IActionResult> postRegistrarFijadoDetalle([FromBody] Lb_Fijados_Detalle parametros)
        {
            Lb_Fijados_Detalle _lbFijados_Detalle = new Lb_Fijados_Detalle
            {
                Fij_Id = parametros.Fij_Id,
                Fij_Ran_Ini = parametros.Fij_Ran_Ini,
                Fij_Ran_Fin = parametros.Fij_Ran_Fin,
                Familia = parametros.Familia,
                Fij_Ran_Ini_Org = parametros.Fij_Ran_Ini_Org,
                Familia_Org = parametros.Familia_Org,
                Usr_Mod = parametros.Usr_Mod
            };

            var result = await _LbColaTrabajoService.RegistrarFijadoDetalle(_lbFijados_Detalle);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchModificarFijadoDetalle")]
        public async Task<IActionResult> patchModificarFijadoDetalle([FromBody] Lb_Fijados_Detalle parametros)
        {
            Lb_Fijados_Detalle _lbFijados_Detalle = new Lb_Fijados_Detalle
            {
                Fij_Id = parametros.Fij_Id,
                Fij_Ran_Ini = parametros.Fij_Ran_Ini,
                Fij_Ran_Fin = parametros.Fij_Ran_Fin,
                Familia = parametros.Familia,
                Fij_Ran_Ini_Org = parametros.Fij_Ran_Ini_Org,
                Familia_Org = parametros.Familia_Org,
                Usr_Mod = parametros.Usr_Mod
            };

            var result = await _LbColaTrabajoService.ModificarFijadoDetalle(_lbFijados_Detalle);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchDeshabilitarFijadoDetalle")]
        public async Task<IActionResult> patchDeshabilitarFijadoDetalle([FromBody] Lb_Fijados_Detalle parametros)
        {
            Lb_Fijados_Detalle _lbFijados_Detalle = new Lb_Fijados_Detalle
            {
                Fij_Id = parametros.Fij_Id,
                Fij_Ran_Ini = parametros.Fij_Ran_Ini,
                Familia = parametros.Familia,
                Flg_Status = parametros.Flg_Status
            };

            var result = await _LbColaTrabajoService.DeshabilitarFijadoDetalle(_lbFijados_Detalle);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarProceso")]
        public async Task<IActionResult> postRegistrarProceso([FromBody] ComponentesExtra parametros)
        {
            ComponentesExtra _lbComponentesExtra = new ComponentesExtra
            {
                Pro_Cod = parametros.Pro_Cod,
                Pro_Des = parametros.Pro_Des,
                Usr_Reg = parametros.Usr_Reg
            };

            var result = await _LbColaTrabajoService.RegistrarProceso(_lbComponentesExtra);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchModificarProceso")]
        public async Task<IActionResult> patchModificarProceso([FromBody] ComponentesExtra parametros)
        {
            ComponentesExtra _lbComponentesExtra = new ComponentesExtra
            {
                Pro_Cod = parametros.Pro_Cod,
                Pro_Cod_Org = parametros.Pro_Cod_Org,
                Pro_Des = parametros.Pro_Des,
                Usr_Mod = parametros.Usr_Mod
            };

            var result = await _LbColaTrabajoService.ModificarProceso(_lbComponentesExtra);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchDeshabilitarProceso")]
        public async Task<IActionResult> patchDeshabilitarProceso([FromBody] ComponentesExtra parametros)
        {
            ComponentesExtra _lbComponentesExtra = new ComponentesExtra
            {
                Pro_Cod = parametros.Pro_Cod,
            };

            var result = await _LbColaTrabajoService.DeshabilitarProceso(_lbComponentesExtra);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarProcesoValor")]
        public async Task<IActionResult> getListarProcesoValor(string Pro_Cod)
        {
            var result = await _LbColaTrabajoService.ListarProcesoValor(Pro_Cod);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarProcesoValor")]
        public async Task<IActionResult> postRegistrarProcesoValor([FromBody] ComponentesExtraValores parametros)
        {
            ComponentesExtraValores value = new ComponentesExtraValores
            {
                Pro_Cod = parametros.Pro_Cod,
                Com_Cod_Con = parametros.Com_Cod_Con,
                Com_Ran_Ini = parametros.Com_Ran_Ini,
                Com_Ran_Fin = parametros.Com_Ran_Fin,
                Com_Cod_Extra1 = parametros.Com_Cod_Extra1,
                Com_Can_Extra1 = parametros.Com_Can_Extra1,
                Com_Cod_Extra2 = parametros.Com_Cod_Extra2,
                Com_Can_Extra2 = parametros.Com_Can_Extra2,
                Com_Cod_Extra3 = parametros.Com_Cod_Extra3,
                Com_Can_Extra3 = parametros.Com_Can_Extra3,
                Com_Cod_Extra4 = parametros.Com_Cod_Extra4,
                Com_Can_Extra4 = parametros.Com_Can_Extra4,
                Com_Cod_Extra5 = parametros.Com_Cod_Extra5,
                Com_Can_Extra5 = parametros.Com_Can_Extra5,
                Com_Cod_Extra6 = parametros.Com_Cod_Extra6,
                Com_Can_Extra6 = parametros.Com_Can_Extra6,
                Com_Cod_Extra7 = parametros.Com_Cod_Extra7,
                Com_Can_Extra7 = parametros.Com_Can_Extra7,
                Com_Cod_Extra8 = parametros.Com_Cod_Extra8,
                Com_Can_Extra8 = parametros.Com_Can_Extra8,
                Com_Cod_Extra9 = parametros.Com_Cod_Extra9,
                Com_Can_Extra9 = parametros.Com_Can_Extra9,
                Com_Cod_Extra10 = parametros.Com_Cod_Extra10,
                Com_Can_Extra10 = parametros.Com_Can_Extra10,
                Com_Cod_Extra11 = parametros.Com_Cod_Extra11,
                Com_Can_Extra11 = parametros.Com_Can_Extra11,
                Com_Cod_Extra12 = parametros.Com_Cod_Extra12,
                Com_Can_Extra12 = parametros.Com_Can_Extra12,
                Com_Cod_Extra13 = parametros.Com_Cod_Extra13,
                Com_Can_Extra13 = parametros.Com_Can_Extra13,
                Com_Cod_Extra14 = parametros.Com_Cod_Extra14,
                Com_Can_Extra14 = parametros.Com_Can_Extra14,
                Com_Cod_Extra15 = parametros.Com_Cod_Extra15,
                Com_Can_Extra15 = parametros.Com_Can_Extra15,
                Com_Cod_Extra16 = parametros.Com_Cod_Extra16,
                Com_Can_Extra16 = parametros.Com_Can_Extra16,
                Usr_Reg = parametros.Usr_Reg
            };

            var result = await _LbColaTrabajoService.RegistrarProcesoValor(value);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchModificarProcesoValor")]
        public async Task<IActionResult> patchModificarProcesoValor([FromBody] ComponentesExtraValores parametros)
        {
            ComponentesExtraValores value = new ComponentesExtraValores
            {
                Pro_Cod = parametros.Pro_Cod,
                Pro_Cod_Org = parametros.Pro_Cod_Org,
                Com_Cod_Con = parametros.Com_Cod_Con,
                Com_Cod_Con_Org = parametros.Com_Cod_Con_Org,
                Com_Ran_Ini = parametros.Com_Ran_Ini,
                Com_Ran_Ini_Org = parametros.Com_Ran_Ini_Org,
                Com_Ran_Fin = parametros.Com_Ran_Fin,
                Com_Cod_Extra1 = parametros.Com_Cod_Extra1,
                Com_Can_Extra1 = parametros.Com_Can_Extra1,
                Com_Cod_Extra2 = parametros.Com_Cod_Extra2,
                Com_Can_Extra2 = parametros.Com_Can_Extra2,
                Com_Cod_Extra3 = parametros.Com_Cod_Extra3,
                Com_Can_Extra3 = parametros.Com_Can_Extra3,
                Com_Cod_Extra4 = parametros.Com_Cod_Extra4,
                Com_Can_Extra4 = parametros.Com_Can_Extra4,
                Com_Cod_Extra5 = parametros.Com_Cod_Extra5,
                Com_Can_Extra5 = parametros.Com_Can_Extra5,
                Com_Cod_Extra6 = parametros.Com_Cod_Extra6,
                Com_Can_Extra6 = parametros.Com_Can_Extra6,
                Com_Cod_Extra7 = parametros.Com_Cod_Extra7,
                Com_Can_Extra7 = parametros.Com_Can_Extra7,
                Com_Cod_Extra8 = parametros.Com_Cod_Extra8,
                Com_Can_Extra8 = parametros.Com_Can_Extra8,
                Com_Cod_Extra9 = parametros.Com_Cod_Extra9,
                Com_Can_Extra9 = parametros.Com_Can_Extra9,
                Com_Cod_Extra10 = parametros.Com_Cod_Extra10,
                Com_Can_Extra10 = parametros.Com_Can_Extra10,
                Com_Cod_Extra11 = parametros.Com_Cod_Extra11,
                Com_Can_Extra11 = parametros.Com_Can_Extra11,
                Com_Cod_Extra12 = parametros.Com_Cod_Extra12,
                Com_Can_Extra12 = parametros.Com_Can_Extra12,
                Com_Cod_Extra13 = parametros.Com_Cod_Extra13,
                Com_Can_Extra13 = parametros.Com_Can_Extra13,
                Com_Cod_Extra14 = parametros.Com_Cod_Extra14,
                Com_Can_Extra14 = parametros.Com_Can_Extra14,
                Com_Cod_Extra15 = parametros.Com_Cod_Extra15,
                Com_Can_Extra15 = parametros.Com_Can_Extra15,
                Com_Cod_Extra16 = parametros.Com_Cod_Extra16,
                Com_Can_Extra16 = parametros.Com_Can_Extra16,
                Usr_Reg = parametros.Usr_Reg
            };

            var result = await _LbColaTrabajoService.ModificarProcesoValor(value);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchDeshabilitarProcesoValor")]
        public async Task<IActionResult> patchDeshabilitarProcesoValor([FromBody] ComponentesExtraValores parametros)
        {
            ComponentesExtraValores value = new ComponentesExtraValores
            {
                Pro_Cod = parametros.Pro_Cod,
                Com_Cod_Con = parametros.Com_Cod_Con,
                Com_Ran_Ini = parametros.Com_Ran_Ini,
                Flg_Status = parametros.Flg_Status
            };

            var result = await _LbColaTrabajoService.DeshabilitarProcesoValor(value);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarCurvas")]
        public async Task<IActionResult> getListarCurvas(string Pro_Cod)
        {
            var result = await _LbColaTrabajoService.ListarCurvas(Pro_Cod);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchProcesoAhiba")]
        public async Task<IActionResult> patchProcesoAhiba([FromBody] Lb_Ahibas parametros)
        {
            Lb_Ahibas value = new Lb_Ahibas
            {
                Ahi_Id = parametros.Ahi_Id,
                Ahi_Est_Pro = parametros.Ahi_Est_Pro,
            };

            var result = await _LbColaTrabajoService.ProcesoAhiba(value);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }


        //OBTENER RELACION BANO, VOLUMEN, PESO
        [HttpGet]
        [Route("getObtenerTrio")]
        public async Task<IActionResult> getObtenerTrio(int Corr_Carta, int Sec)
        {
            var result = await _LbColaTrabajoService.ObtenerTrio(Corr_Carta, Sec);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }



    }
}
