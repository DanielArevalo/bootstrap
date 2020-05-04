using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.CDATS.Entities
{
    [DataContract]
    [Serializable]
    public class AdministracionCDAT
    {
        [DataMember]
        public Int64 codigo_cdat { get; set; }
        [DataMember]
        public decimal codigo_gmf { get; set; }
        [DataMember]
        public string numero_cdat { get; set; }
        [DataMember]
        public string numero_fisico { get; set; }
        [DataMember]
        public int cod_oficina { get; set; }
        [DataMember]
        public string cod_lineacdat { get; set; }
        [DataMember]
        public Int64 cod_destinacion { get; set; }
        [DataMember]
        public DateTime fecha_apertura { get; set; }
        [DataMember]
        public string modalidad { get; set; }
        [DataMember]
        public int codforma_captacion { get; set; }
        [DataMember]
        public int plazo { get; set; }
        [DataMember]
        public int tipo_calendario { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public int cod_moneda { get; set; }
        [DataMember]
        public DateTime fecha_inicio { get; set; }
        [DataMember]
        public DateTime fecha_vencimiento { get; set; }
        [DataMember]
        public int cod_asesor_com { get; set; }
        [DataMember]
        public string tipo_interes { get; set; }
        [DataMember]
        public decimal tasa_interes { get; set; }
        [DataMember]
        public int cod_tipo_tasa { get; set; }
        [DataMember]
        public int tipo_historico { get; set; }
        [DataMember]
        public decimal desviacion { get; set; }
        [DataMember]
        public int cod_periodicidad_int { get; set; }
        [DataMember]
        public int modalidad_int { get; set; }
        [DataMember]
        public int capitalizar_int { get; set; }
        [DataMember]
        public int capitalizar_rete { get; set; }
        [DataMember]
        public int cobra_retencion { get; set; }
        [DataMember]
        public decimal tasa_nominal { get; set; }
        [DataMember]
        public decimal tasa_efectiva { get; set; }
        [DataMember]
        public decimal intereses_cap { get; set; }
        [DataMember]
        public decimal intereses { get; set; }
        [DataMember]
        public decimal retencion_cap { get; set; }
        [DataMember]
        public DateTime fecha_intereses { get; set; }
        [DataMember]
        public DateTime fecha_provision { get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public int dias { get; set; }
        [DataMember]
        public decimal retencion { get; set; }
        [DataMember]
        public int desmaterializado { get; set; }

        //AGREGADO
        [DataMember]
        public string nomcapta { get; set; }
        [DataMember]
        public string nommoneda { get; set; }
        [DataMember]
        public string nomtipocalendario { get; set; }
        [DataMember]
        public string nomdestinacion { get; set; }
        [DataMember]
        public string nomoficina { get; set; }
        [DataMember]
        public string nomtipointeres { get; set; }
        [DataMember]
        public string nomtipotasa { get; set; }
        [DataMember]
        public string nomtipohistorico { get; set; }
        [DataMember]
        public string nomperiodicidad { get; set; }
        [DataMember]
        public string nomcapitaliza { get; set; }
        [DataMember]
        public string nomretencion { get; set; }
        [DataMember]
        public string nomdesmate { get; set; }
        [DataMember]
        public string nomusuario { get; set; }
        [DataMember]
        public string nommodalidadint { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public String nom_linea_cdat { get; set; }
        [DataMember]
        public decimal saldo { get; set; }
        [DataMember]
        public String nom_periodicidad { get; set; }
        
        //AGREGADO PARA USO EN LIBRO OFICIAL
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string identificacion{ get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidos { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string nomestado { get; set; }
    }

    [DataContract]
    [Serializable]
    public class NovedadCDAT 
    {
        [DataMember]
        public Int64 IDNOVEDAD { get; set; }

        [DataMember]
        public int CODIGO_CDAT { get; set; }

        [DataMember]
        public Int64 TIPO_NOVEDAD { get; set; }

        [DataMember]
        public DateTime FECHA_NOVEDAD { get; set; }
        
        [DataMember]
        public String OBSERVACIONES { get; set; }
        
        [DataMember]
        public Int64 COD_USUARIO { get; set; }

        [DataMember]
        public DateTime FECHACREA { get; set; }
        [DataMember]
        public Int64 EstadoActual { get; set; }

        [DataMember]
        public Int64 EstadoSiguiente { get; set; }

    }
}
