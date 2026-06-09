using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.Memorandum;
using ic.backend.precotex.web.Entity.Entities.Tejeduria;
using ic.backend.precotex.web.Service.Services.Implementacion.Tejeduria;
using ic.backend.precotex.web.Service.Services.Tejeduria;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.Tejeduria
{
    [Route("api/[controller]")]
    [ApiController]
    public class TjSeguimientoSaldoHiloController: ControllerBase
    {
        private readonly ITjSeguimientoSaldoHiloService _tjSeguimientoSaldoHiloService;

        public TjSeguimientoSaldoHiloController(ITjSeguimientoSaldoHiloService tjSeguimientoSaldoHiloService)
        {
            _tjSeguimientoSaldoHiloService = tjSeguimientoSaldoHiloService;
        }

        [HttpGet]
        [Route("getListaOT_Programada")]
        public async Task<IActionResult> getListaOT_Programada(string? Cod_OrdProv, string? Cod_HilTel)
        {
            if (Cod_HilTel == null)
            {
                Cod_HilTel = "";
            }
            var result = await _tjSeguimientoSaldoHiloService.ListaOT_Programada(Cod_OrdProv!, Cod_HilTel!);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaOT_Terminada")]
        public async Task<IActionResult> getListaOT_Terminada(DateTime Fecha, string Flg_Pendiente)
        {
            var result = await _tjSeguimientoSaldoHiloService.ListaOT_Terminada(Fecha, Flg_Pendiente);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postProceso")]
        public async Task<IActionResult> postProceso([FromBody] SeguimientoSaldoHiloTelaParameter parameters)
        {
            tj_seguimiento_saldo_hilo_tela _tj_seguimiento_saldo_hilo_tela = new tj_seguimiento_saldo_hilo_tela
            {
                Num_Traslado = parameters.Num_Traslado,
                Cod_OrdProv = parameters.Cod_OrdProv,
                Cod_Ordtra_Ori = parameters.Cod_Ordtra_Ori,
                Cod_Maquina_Ori = parameters.Cod_Maquina_Ori,
                Cod_HilTel = parameters.Cod_HilTel,
                Cod_Color = parameters.Cod_Color,
                Kg_Programado = parameters.Kg_Programado,
                Kg_Salida = parameters.Kg_Salida,
                Kg_Consumo = parameters.Kg_Consumo,
                Kg_Devolver = parameters.Kg_Devolver,
                Estado = parameters.Estado,
                Cod_Ordtra_Des = parameters.Cod_Ordtra_Des,
                Cod_Maquina_Des = parameters.Cod_Maquina_Des,
                Cod_Usuario = parameters.Cod_Usuario
            };

            var result = await _tjSeguimientoSaldoHiloService.Proceso(_tj_seguimiento_saldo_hilo_tela, parameters.Accion!);
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
