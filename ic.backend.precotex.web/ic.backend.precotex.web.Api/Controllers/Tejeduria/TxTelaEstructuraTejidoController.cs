using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.Services.Implementacion.Tejeduria;
using ic.backend.precotex.web.Service.Services.Implementacion.Tintoreria;
using ic.backend.precotex.web.Service.Services.Tintoreria;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.Tejeduria
{
    [Route("api/[controller]")]
    [ApiController]
    public class TxTelaEstructuraTejidoController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ITxTelaEstructuraTejidoItemsService _txTelaEstructuraTejidoItemsService;
        public TxTelaEstructuraTejidoController(ITxTelaEstructuraTejidoItemsService txTelaEstructuraTejidoItemsService,
            HttpClient httpClient)
        {
            _txTelaEstructuraTejidoItemsService = txTelaEstructuraTejidoItemsService;
            _httpClient = httpClient;
        }

        [HttpGet]
        [Route("getObtieneEstructuraTejidoItem")]
        public async Task<IActionResult> getObtieneEstructuraTejidoItem(string? codTela, string? Cod_Ordtra, string? Num_Secuencia)
        {
            var result = await _txTelaEstructuraTejidoItemsService.ObtieneEstructuraTejidoItem(codTela, Cod_Ordtra, Num_Secuencia);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistraEstructuraTejidoItem")]
        public async Task<IActionResult> Insertar([FromBody] txOtsHojasArranqueDetParameter parameters)
        {
            var result = await _txTelaEstructuraTejidoItemsService.InsertarEstructuraTejidoItem(parameters.CodOrdtra, 
                parameters.NumSecuencia, parameters.CodComb, parameters.CodTalla, parameters.CodUsuario, parameters.XmlData);

            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc > 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        //[HttpPut]
        //[Route("putModificarMedida")]
        //public async Task<IActionResult> Modificar([FromBody] txOtsHojasArranqueDetParameter parameters)
        //{
        //    var result = await _txTelaEstructuraTejidoItemsService.ModificarMedida(parameters.CodOrdtra, parameters.NumSecuencia);

        //    if (result.Success)
        //    {
        //        result.CodeResult = result.CodeTransacc > 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
        //        return Ok(result);
        //    }

        //    result.CodeResult = StatusCodes.Status400BadRequest;
        //    return BadRequest(result);
        //}


        [HttpGet]
        [Route("getObtieneTelaMedida")]
        public async Task<IActionResult> getObtieneTelaMedida(string? codTela, string? Cod_Talla)
        {
            var result = await _txTelaEstructuraTejidoItemsService.ObtieneTelaMedida(codTela, Cod_Talla);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistraTelaMedida")]
        public async Task<IActionResult> postRegistraTelaMedida([FromBody] txTelaMedidaParameter parameters)
        {
            var result = await _txTelaEstructuraTejidoItemsService.InsertarTelaMedida(parameters.CodOrdtra,
                parameters.NumSecuencia, parameters.CodTela, parameters.CodComb, parameters.CodTalla, parameters.CodUsuario, parameters.XmlData);

            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc > 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtieneTelaMedidaHist")]
        public async Task<IActionResult> getObtieneTelaMedidaHist(string? codTela, string? Cod_Ordtra, string? Num_Secuencia, string? Cod_Comb, string? Cod_Talla)
        {
            var result = await _txTelaEstructuraTejidoItemsService.ObtieneTelaMedidaHist(codTela, Cod_Ordtra, Num_Secuencia, Cod_Comb, Cod_Talla);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistraEstructuraTejido")]
        public async Task<IActionResult> postRegistraEstructuraTejido([FromBody] txTelaEstructuraTejidoItemsParameter parameters)
        {
            var result = await _txTelaEstructuraTejidoItemsService.InsertarCargaEstructuraTejido(parameters.NombreVersion,
                parameters.Cod_Tela, parameters.Servicio, parameters.Observacion, parameters.Elaborado, parameters.Revisado, parameters.CodUsuario, parameters.XmlData);

            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc > 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getGeneraVersionHojasArranque")]
        public async Task<IActionResult> getGeneraVersionHojasArranque(string? Cod_Ordtra, string? Num_Secuencia, string? Cod_Talla)
        {
            var result = await _txTelaEstructuraTejidoItemsService.GeneraVersionHojasArranque(Cod_Ordtra, Num_Secuencia, Cod_Talla);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerVersionHojasArranque")]
        public async Task<IActionResult> getObtenerVersionHojasArranque(string? Cod_Ordtra, string? Num_Secuencia)
        {
            var result = await _txTelaEstructuraTejidoItemsService.ObtenerVersionHojasArranque(Cod_Ordtra, Num_Secuencia);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getValidaVersionHojasArranque")]
        public async Task<IActionResult> getValidaVersionHojasArranque(string Cod_Ordtra, int Num_Secuencia, int Version, string Flg_Rectilineo)
        {
            var result = await _txTelaEstructuraTejidoItemsService.ValidaVersionHojasArranque(Cod_Ordtra, Num_Secuencia, Version, Flg_Rectilineo);

            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc > 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoVersionHojasArranqueHist")]
        public async Task<IActionResult> getListadoVersionHojasArranqueHist(DateTime FecIni, DateTime FecFin, string Cod_Ordtra)
        {
            var result = await _txTelaEstructuraTejidoItemsService.ListadoVersionHojasArranqueHist(FecIni, FecFin, Cod_Ordtra);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaMaquinaRevisadora")]
        public async Task<IActionResult> getListaMaquinaRevisadora()
        {
            var result = await _txTelaEstructuraTejidoItemsService.ListaMaquinaRevisadora();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postCrudArranqueCtrol")]
        public async Task<IActionResult> postCrudArranqueCtrol([FromBody] txOtsHojasArranqueCtrolParameter parameters)
        {
            var obj = setDataTx_Ots_Hojas_Arranque_Ctrol(parameters);
            var result = await _txTelaEstructuraTejidoItemsService.CrudArranqueCtrol(obj, parameters.Accion);

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerArranqueCtrol")]
        public async Task<IActionResult> getObtenerArranqueCtrol(string Cod_OrdTra, int Num_Secuencia, int Version)
        {
            var result = await _txTelaEstructuraTejidoItemsService.ObtenerArranqueCtrol(Cod_OrdTra, Num_Secuencia, Version);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("ObtenerArranqueCtrolSinVersion")]
        public async Task<IActionResult> ObtenerArranqueCtrolSinVersion(string Cod_OrdTra, int Num_Secuencia)
        {
            var result = await _txTelaEstructuraTejidoItemsService.ObtenerArranqueCtrolSinVersion(Cod_OrdTra, Num_Secuencia);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoVersionHojasArranqueHistDetail")]
        public async Task<IActionResult> getListadoVersionHojasArranqueHistDetail(DateTime FecIni, DateTime FecFin, string Cod_Ordtra)
        {
            var result = await _txTelaEstructuraTejidoItemsService.ListadoVersionHojasArranqueHistDetail(FecIni, FecFin, Cod_Ordtra);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        private Tx_Ots_Hojas_Arranque_Ctrol setDataTx_Ots_Hojas_Arranque_Ctrol(txOtsHojasArranqueCtrolParameter obj)
        {
            return new Tx_Ots_Hojas_Arranque_Ctrol
            {
                Cod_OrdTra = obj.Cod_OrdTra,
                Version = obj.Version,
                Num_Secuencia = obj.Num_Secuencia,
                Usu_Registro = obj.Usu_Registro
            };
        }


    }
}
