using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Tx_Muestra_Control_Proceso
    {

		public DateTime? FECHA { get; set; }
        public string? NRO_REFERENCIA { get; set; }
        public string? HORA_CARGA { get; set; }
        public string? OPERARIO { get; set; }
        public string? MAQUINA { get; set; }
        public string? PARTIDA { get; set; }
        public string? COLOR { get; set; }
        public string? ARTICULO { get; set; }
        public int? PESO { get; set; }
        public string? CUERDAS { get; set; }
        public string? CLIENTE { get; set; }
        public string? RELBANO { get; set; }
        public int? VOLRECETA { get; set; }				
						/*PARAMETROS CRUDO*/
		public decimal CR_ANCHO { get; set; }
        public int? CR_DENSIDAD { get; set; }
            /*PARAMETROS PREVIO*/
        public decimal PR_BAR { get; set; }
        public int?		PR_TOBERA { get; set; }
        public int?		PR_ACUMULADOR { get; set; }
        public int?		PR_BOMBA { get; set; }
        public int?		PR_VELOCIDAD { get; set; }
        public decimal? PR_TIEMPO_CICLO_1 { get; set; }
        public int?PR_NIV_BANO_MAQ { get; set; }
        public decimal? PR_PH_PILLING { get; set; }
        public decimal? PR_PH_PILLING_2 { get; set; }
						/*PARAMETROS TEÑIDO REACTIVO*/
		public decimal TR_BAR { get; set; }
        public int?		TR_TOBERA { get; set; }
        public int?		TR_ACUMULADOR { get; set; }
        public int?		TR_BOMBA { get; set; }
        public int?		TR_VELOCIDAD { get; set; }
        public decimal? TR_TIEMPO_CICLO_1 { get; set; }
        public int? TR_VOLUMEN { get; set; }
        public int? TR_NIV_BANO_MAQ_1 { get; set; }
			public decimal? TR_PH_INICIO1_CSAL	{ get; set; }	
			public decimal? TR_PH_INICIO2_CSAL	{ get; set; }	
			public decimal? TR_PH_INICIO1_SSAL { get; set; }
        public decimal? TR_PH_INICIO2_SSAL { get; set; }
            public int?TR_DENSIDAD_SAL_1	{ get; set; }	
			public int?TR_DENSIDAD_SAL_2	{ get; set; }	
			public int?TR_TEMPERATURA_1		{ get; set; }
			public int?TR_TEMPERATURA_2		{ get; set; }
			public int?TR_CANT_DOSIF		{ get; set; }	
			public int?TR_GL_DENSIDAD		{ get; set; }	
			public int?TR_GL_DENSIDAD2		{ get; set; }
			public int? TR_LT_DENSIDAD { get; set; }
			public int? TR_LT_DENSIDAD2 { get; set; }
			public decimal?TR_CORR_TEORICA { get; set; }
        public decimal?TR_CORR_TEORICA2 { get; set; }
        public decimal?TR_CORR_REAL { get; set; }
        public decimal?TR_CORR_REAL2 { get; set; }
        public decimal?TR_LT_DOSIF_COLOR { get; set; }
        public decimal?TR_LT_DOSIF_SAL { get; set; }
        public decimal?TR_LT_DOSIF1_ALCA { get; set; }
        public decimal?TR_PH_1_ALCALI_1 { get; set; }
        public decimal?TR_PH_1_ALCALI_2 { get; set; }
        public decimal?TR_LT_DOSIF2_ALCA { get; set; }
        public decimal?TR_PH_2_ALCALI_1 { get; set; }
        public decimal?TR_PH_2_ALCALI_2 { get; set; }
        public decimal? TR_LT_DOSIF3_ALCA { get; set; }
        public int? TR_NIV_BANO_MAQ_2 { get; set; }
        public decimal? TR_AGOTAMIENTO_1 { get; set; }
        public decimal? TR_AGOTAMIENTO_2 { get; set; }
        public int? TR_TIEMPO_AGOTA { get; set; }
        /*PARAMETROS JABONADO*/
        public decimal? NE_PH_1 { get; set; }
        public decimal? NE_PH_2 { get; set; }
        public decimal?JA_PH_1{ get; set; }		
			public decimal?JA_PH_2{ get; set; }		
			public decimal?FI_PH_1{ get; set; }		
			public decimal?FI_PH_2{ get; set; }		
			public decimal?AC_PH_1 { get; set; }
        public decimal? AC_PH_2 { get; set; }
						/*PARAMETROS TEÑIDO DISPERSO*/
		public decimal TD_BAR { get; set; }
        public int?TD_TOBERA { get; set; }
        public int?TD_ACUMULADOR { get; set; }
        public int?TD_BOMBA { get; set; }
        public int? TD_VELOCIDAD { get; set; }	
			public decimal?TD_TIEMPO_CICLO_1		{ get; set; }	
			public decimal?TD_PH_TENIDO_1			{ get; set; }	
			public decimal?TD_PH_TENIDO_2			{ get; set; }	
			public decimal? TD_PH_DESCARGA_DISP_1 { get; set; }
        public decimal? TD_PH_DESCARGA_DISP_2 { get; set; }
        public string? CAMBIO_TURNO { get; set; }
        public string? 	OBSERVACIONES { get; set; }
        public decimal? PESO_POR_CUERDA { get; set; }
    }
}
