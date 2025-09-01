using ic.backend.precotex.web.Entity.Entities;

namespace ic.backend.precotex.web.Api.Parameters
{
    public class PrintContentParameter
    {
        public string? version { get; set; } = null!;
        public string? content { get; set; } = null!;
        public string? PrintName { get; set; } = null!;
        public int? CountPrint { get; set; } = null!;
        public Tx_TelaEstructuraColgador tx_TelaEstructuraColgador { get; set; } = null!;
    }
}
