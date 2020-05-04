using System;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class PolizasSeguros
    {
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public Int64 cod_poliza { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public Int64 cod_asegurado { get; set; }
        [DataMember]
        public Int64 identificacion { get; set; }
        [DataMember]
        public String tipo_iden { get; set; }
        [DataMember]
        public string nombre_deudor { get; set; }
        [DataMember]
        public string primer_nombre { get; set; }
        [DataMember]
        public string segundo_nombre { get; set; }
        [DataMember]
        public string primer_apellido { get; set; }
        [DataMember]
        public string segundo_apellido { get; set; }
        [DataMember]
        public Int64 monto_desembolsado { get; set; }
        [DataMember]
        public Int64 valor_prima_mensual { get; set; }
        [DataMember]
        public Int64 valor_prima_total { get; set; }
        [DataMember]        
        public String nom_tomador { get; set; }
        [DataMember]
        public String nit_tomador { get; set; }
        [DataMember]
        public Int64 tipo_plan_s { get; set; }//Campo viene de la tabla Poliza_Seguros
        [DataMember]
        public Int64 tipo_plan { get; set; }//campo  que viene de la tabla Polizas_Seguros_Vida
        [DataMember]
        public string tipo { get; set; }//vida grupo//accidentes 
        [DataMember]
        public Int64 valor_prima { get; set; }
        [DataMember]
        public Int64 individual { get; set; }
        [DataMember]
        public Int64 accidentes { get; set; }
        [DataMember]
        public Int64 num_poliza { get; set; }
        [DataMember]
        public Int64 icodigo { get; set; }
        [DataMember]
        public Int64 ident_asesor { get; set; }
        [DataMember]
        public String nombre_asesor { get; set; }    
        [DataMember]
        public DateTime fec_ini_vig { get; set; }
        [DataMember]
        public DateTime fec_fin_vig { get; set; }    
        [DataMember]
        public String oficina { get; set; }
        [DataMember]
        public String codoficina { get; set; }   
        [DataMember]
        public DateTime fechanacimiento { get; set; }    
        [DataMember]
        public String estado_civil { get; set; }    
        [DataMember]
        public String sexo { get; set; }
        [DataMember]
        public String actividad { get; set; }
        [DataMember]
        public String direccion { get; set; }
        [DataMember]
        public String telefono { get; set; }
        [DataMember]
        public String ciudad_residencia { get; set; }
        [DataMember]
        public String email { get; set; }
        [DataMember]
        public String celular { get; set; }
        [DataMember]
        public Int64 valor_maximo = 64;
        [DataMember]
        public Int64 edad_maxima { get; set; }
        [DataMember]
        public String Nombres { get; set; }
        [DataMember]
        public Int64 monto { get; set; }
        [DataMember]
        public Int64 plazo { get; set; }
        [DataMember]
        public Int64 cuota { get; set; }
       
    }
}
