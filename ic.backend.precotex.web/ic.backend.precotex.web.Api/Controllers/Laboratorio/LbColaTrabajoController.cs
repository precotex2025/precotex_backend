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
                Sod_Por = parametros.Sod_Por
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
        [Route("deleteEliminarArea")]
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

    }
}
