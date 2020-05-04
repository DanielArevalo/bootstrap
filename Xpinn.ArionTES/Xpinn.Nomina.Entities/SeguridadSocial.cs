using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class SeguridadSocial
    {
        [DataMember]
        public Int64 IDSEGURIDAD { get; set; }
        [DataMember]
        public decimal PORCENTAJE_SALUD { get; set; }
        [DataMember]
        public decimal PORCENTAJE_PENSION { get; set; }
        [DataMember]
        public decimal PORC_EMPLEADOR_SALUD { get; set; }
        [DataMember]
        public decimal PORC_EMPLEADOR_PENSION { get; set; }
        [DataMember]
        public decimal PORCENTAJE_INCAPACIDAD { get; set; }
        [DataMember]
        public int? PERMITE_INCAPACIDAD_TOPE { get; set; }

        [DataMember]
        public decimal PORCENTAJE_SALARIO_INTEGRAL { get; set; }


        [DataMember]
        public int? DESCONTAR_APORTES { get; set; }
        [DataMember]
        public int? MARCAR_VST { get; set; }
        [DataMember]
        public int? DESCONTAR_APORTE_EMPL { get; set; }
        [DataMember]
        public int? INACTIVIDAD_DIAS_CAL { get; set; }
        [DataMember]
        public int? DESCUENTA_DIAS_CASTIGO { get; set; }
        [DataMember]
        public int? NIT_ARCHIVO { get; set; }
        [DataMember]
        public int? BASE_INACTIVIDAD_DIAS { get; set; }
        [DataMember]
        public int? PROCEDIMIENTO_CENTRO_ARP { get; set; }
        [DataMember]
        public int? IBC_INACTIVIDADES { get; set; }
        [DataMember]
        public int? CALCULO_PRIMDIAS { get; set; }
        [DataMember]
        public int? SALPEN_VACACIONES { get; set; }

        [DataMember]
        public decimal PORCENTAJE_SALUD_PENSIONADO { get; set; }

        [DataMember]
        public decimal PERIODOS_MAXIMOS_VACACIONES { get; set; }

        [DataMember]
        public int? MAXSALARIOSARL { get; set; }

        [DataMember]
        public int? MAXSALARIOSPARAFISCALES { get; set; }


        [DataMember]
        public decimal? MAXSALARIOSSALUDPENSION { get; set; }


        [DataMember]
        public decimal? CajaCompensacion { get; set; }


        [DataMember]
        public decimal? sena { get; set; }

        [DataMember]
        public decimal? icbf { get; set; }

        [DataMember]
        public decimal? prima { get; set; }

        [DataMember]
        public decimal? cesantias { get; set; }

        [DataMember]
        public decimal? interescesantias { get; set; }

        [DataMember]
        public decimal? vacaciones { get; set; }

        [DataMember]
        public Int16 diasvacaciones { get; set; }


        [DataMember]
        public Int16 diasminimoprima { get; set; }

        [DataMember]
        public String aprobador { get; set; }
        [DataMember]
        public String revisor { get; set; }

        [DataMember]
        public decimal? porcentaje_retencion { get; set; }

        [DataMember]
        public decimal? uvt { get; set; }

        [DataMember]
        public int? basemax { get; set; }

        [DataMember]
        public Int16 cantidadsalretencion { get; set; }
        [DataMember]
        public int? RegimenTEspecial { get; set; }

        [DataMember]
        public int? Tercero { get; set; }

        [DataMember]
        public int? Contribuyente { get; set; }


        [DataMember]
        public int? SaludContribuyente { get; set; }
        [DataMember]
        public int? SenaContribuyente { get; set; }
        [DataMember]
        public int? icbfContribuyente { get; set; }
        [DataMember]
        public int? ccfContribuyente { get; set; }



        [DataMember]
        public decimal? baseRTE { get; set; }

        [DataMember]
        public int? ManejaAproximacion { get; set; }
        [DataMember]
        public int? AproxCentesima { get; set; }
        [DataMember]
        public int? AproxMilesima { get; set; }
        [DataMember]
        public int? Aprox50mascercano { get; set; }


        [DataMember]
        public Int16 diasincapacidades { get; set; }

        [DataMember]
        public int? novvacaciones { get; set; }

        [DataMember]
        public int? vacacionesanticipadas { get; set; }
        [DataMember]
        public int? retroactivo { get; set; }
        [DataMember]
        public int? Cuentabancaria { get; set; }
        [DataMember]
        public int? codigobanco { get; set; }

        [DataMember]
        public int? incap_smlv { get; set; }

        [DataMember]
        public int? formato_desprendible { get; set; }
    }
}
