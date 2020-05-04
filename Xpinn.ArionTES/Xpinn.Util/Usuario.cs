using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Util
{
    /// <summary>
    /// Entidad Usuario
    /// </summary>
    [DataContract]
    [Serializable]
    public class Usuario
    {
        [DataMember]
        public Int64 codusuario { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string login { get; set; }
        [DataMember]
        public string clave { get; set; }
        [DataMember]
        public DateTime fechacreacion { get; set; }
        [DataMember]
        public Int64 estado { get; set; }
        [DataMember]
        public Int64 codperfil { get; set; }
        [DataMember]
        public string nombreperfil { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public string nombre_oficina { get; set; }
        [DataMember]
        public string IP { get; set; }
        [DataMember]
        public string navegador { get; set; }
        [DataMember]
        public Boolean programaGeneraLog { get; set; }
        [DataMember]
        public Int64 codOpcionActual { get; set; }
        [DataMember]
        public Int64 idEmpresa { get; set; }
        [DataMember]
        public string clave_sinencriptar { get; set; }
        [DataMember]
        public List<string> LstIP { get; set; }
        [DataMember]
        public List<string> LstMac { get; set; }
        [DataMember]
        public List<int> LstAtribuciones { get; set; }
        [DataMember]
        public string empresa { get; set; }
        [DataMember]
        public string nitempresa { get; set; }
        [DataMember]
        public string representante_legal { get; set; }
        [DataMember]
        public string contador { get; set; }
        [DataMember]
        public string tarjeta_contador { get; set; }

        //AGREGADO
        [DataMember]
        public string sigla_empresa { get; set; }
        [DataMember]
        public Int64 tipo { get; set; }
        [DataMember]
        public string reporte_egreso { get; set; }
        [DataMember]
        public string reporte_ingreso { get; set; }
        [DataMember]
        public string revisor_Fiscal { get; set; }
        [DataMember]
        public string revisor_Fisacal { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public string cod_uiaf { get; set; }
        [DataMember]
        public long codperfilhelpdesk { get; set; }
        [DataMember]
        public TipoUsuarioHelpDesk tipo_usuario_read
        {
            get
            {
                return codperfilhelpdesk.ToEnum<TipoUsuarioHelpDesk>(TipoUsuarioHelpDesk.SinTipoUsuario);
            }
            set
            {
                codperfilhelpdesk = (int)value;
            }
        }
        [DataMember]
        public long? cod_encargado { get; set; }
        [DataMember]
        public string conexionBD { get; set; }
        [DataMember]
        public long? hd_cod_cliente { get; set; }
        [DataMember]
        public string hd_desc_cliente { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public TipoUsuarioHelpDesk tipo_usuario { get; set; }
        [DataMember]
        public bool recuerdame { get; set; }
        [DataMember]
        public string direccion_empresa { get; set; }
        [DataMember]
        public string version_pl { get; set; }
        [DataMember]
        public string idioma { get; set; }
        [DataMember]
        public string administrador { get; set; }

        //AGREGADO
        [DataMember]
        public string documento { get; set; }

        [DataMember]
        public string cod_cuenta { get; set; }
    }
}