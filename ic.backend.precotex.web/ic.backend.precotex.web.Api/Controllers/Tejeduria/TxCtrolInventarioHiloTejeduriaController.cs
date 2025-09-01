using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.Services.Implementacion.Tejeduria;
using ic.backend.precotex.web.Service.Services.Tejeduria;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace ic.backend.precotex.web.Api.Controllers.Tejeduria
{
    [Route("api/[controller]")]
    [ApiController]

    public class TxCtrolInventarioHiloTejeduriaController : ControllerBase
    {
        private readonly ITxCtrolInventarioHiloTejeduriaService _txCtrolInventarioHiloTejeduriaService;

        public TxCtrolInventarioHiloTejeduriaController(ITxCtrolInventarioHiloTejeduriaService txCtrolInventarioHiloTejeduriaService)
        {
            _txCtrolInventarioHiloTejeduriaService = txCtrolInventarioHiloTejeduriaService;
        }

        [HttpGet]
        [Route("getObtenerCtrolInventarioHiloTejeduriaByLote")]
        public async Task<IActionResult> getObtenerCtrolInventarioHiloTejeduriaByLote(string? Lote)
       {
            var result = await _txCtrolInventarioHiloTejeduriaService.ObtenerCtrolInventarioHiloTejeduriaByLote(Lote);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postCrudCtrolInventarioHiloTejeduria")]
        public async Task<IActionResult> postCrudCtrolInventarioHiloTejeduria([FromBody] txCtrolInventarioHiloTejeduriaParameter parameters)
        {
            var obj = setDataTx_Ctrol_Inventario_Hilo_Tejeduria(parameters);
            var result = await _txCtrolInventarioHiloTejeduriaService.CrudCtrolInventarioHiloTejeduria(obj, 
                parameters.Tipo);

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        #region SET VALORES
        private Tx_Ctrol_Inventario_Hilo_Tejeduria setDataTx_Ctrol_Inventario_Hilo_Tejeduria(txCtrolInventarioHiloTejeduriaParameter txCtrolInventarioHiloTejeduriaParameter)
        {
            return new Tx_Ctrol_Inventario_Hilo_Tejeduria
            {
                Tipo = txCtrolInventarioHiloTejeduriaParameter.Tipo,
                Lote = txCtrolInventarioHiloTejeduriaParameter.Lote,
                Num_Semana = txCtrolInventarioHiloTejeduriaParameter.Num_Semana,
                Titulo = txCtrolInventarioHiloTejeduriaParameter.Titulo,
                Ser_OrdComp = txCtrolInventarioHiloTejeduriaParameter.Ser_OrdComp,
                Cod_OrdComp = txCtrolInventarioHiloTejeduriaParameter.Cod_OrdComp,
                Color = txCtrolInventarioHiloTejeduriaParameter.Color,
                Hilo_Tipo = txCtrolInventarioHiloTejeduriaParameter.Hilo_Tipo,
                Hilo_Codigo = txCtrolInventarioHiloTejeduriaParameter.Hilo_Codigo,
                Ubicacion = txCtrolInventarioHiloTejeduriaParameter.Ubicacion,

                Cantidad_Cono = txCtrolInventarioHiloTejeduriaParameter.Cantidad_Cono,
                Peso_Tara = txCtrolInventarioHiloTejeduriaParameter.Peso_Tara,
                Peso_Bruto = txCtrolInventarioHiloTejeduriaParameter.Peso_Bruto,
                Peso_Neto = txCtrolInventarioHiloTejeduriaParameter.Peso_Neto,
                Pallet = txCtrolInventarioHiloTejeduriaParameter.Pallet,
                Diferencia = txCtrolInventarioHiloTejeduriaParameter.Diferencia,

                Observacion = txCtrolInventarioHiloTejeduriaParameter.Observacion,
                Proveedor = txCtrolInventarioHiloTejeduriaParameter.Proveedor,
                Usu_Registro = txCtrolInventarioHiloTejeduriaParameter.Usu_Registro,

            };
        }
        #endregion
    }
}
