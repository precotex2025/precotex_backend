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
        public async Task<IActionResult> getListaSDCPorEstado(string Flg_Est_Lab, DateTime Fec_Ini, DateTime Fec_Fin)
        {
            var result = await _LbColaTrabajoService.ListaSDCPorEstado(Flg_Est_Lab, Fec_Ini, Fec_Fin);
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
                Sec = parametros.Sec ?? 0
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
                Ahi_Id = parametros.Ahi_Id
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
                Cambio = parametros.Cambio
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

    }
}
