using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.Memorandum;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.HelpCommon
{
    public interface IHelpCommonService
    {
        Task<ServiceResponse<int>> PrintTicket(string content, string PrintName);
        Task<ServiceResponse<int>> PrintQRCode(string content, string PrintName);
        Task<ServiceResponse<int>> PrintQRCode_v1(string content, string PrintName, Tx_TelaEstructuraColgador tx_TelaEstructuraColgador, int? iCantidadPrint);
        Task<ServiceResponse<int>> PrintQRCode_v2(string content, string PrintName, Tx_TelaEstructuraColgador tx_TelaEstructuraColgador);
        //Task<ServiceResponse<int>> PrintA4(List<Tx_Memorandum?> memo, string PrintName, int iCantidad);
        Task<ServiceResponse<string>> PrintA4ToPdf(List<Tx_Memorandum?> memo, int iCantidad);
    }
}
