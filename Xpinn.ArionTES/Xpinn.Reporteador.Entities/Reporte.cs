using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace Xpinn.Reporteador.Entities
{
    /// <summary>
    /// Entidad Reporte
    /// </summary>
    [DataContract]
    [Serializable]
    public class Reporte
    {
        #region Encabezado
        [DataMember]
        public Int64 idreporte { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Int64 tipo_reporte { get; set; }
        [DataMember]
        public DateTime fecha_creacion { get; set; }
        [DataMember]
        public Int64 cod_elabora { get; set; }
        [DataMember]
        public string sentencia_sql { get; set; }
        [DataMember]
        public string url_crystal { get; set; }
        [DataMember]
        public string encabezado { get; set; }
        [DataMember]
        public string piepagina { get; set; }
        [DataMember]
        public int numerar { get; set; }
        [DataMember]
        public List<Tabla> lstTablas { get; set; }
        [DataMember]
        public List<Encadenamiento> lstEncadenamiento { get; set; }
        [DataMember]
        public List<Condicion> lstCondicion { get; set; }
        [DataMember]
        public List<ColumnaReporte> lstColumnaReporte { get; set; }
        [DataMember]
        public List<Orden> lstOrden { get; set; }
        [DataMember]
        public List<Grupo> lstGrupo { get; set; }
        [DataMember]
        public List<UsuariosReporte> lstUsuarios { get; set; }
        [DataMember]
        public List<PerfilReporte> lstPerfil { get; set; }
        [DataMember]
        public List<Parametros> lstParametros { get; set; }
        [DataMember]
        public List<Plantilla> lstPlantilla { get; set; }
        #endregion
    }

    /// <summary>
    /// Entidad Tabla
    /// </summary>
    [DataContract]
    [Serializable]
    public class Tabla
    {
        [DataMember]
        public Int64 idreporte { get; set; }
        [DataMember]
        public Int64 idtabla { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
    }


    /// <summary>
    /// Entidad Encadenamiento
    /// </summary>
    [DataContract]
    [Serializable]
    public class Encadenamiento
    {
        [DataMember]
        public Int64 idreporte { get; set; }
        [DataMember]
        public Int64 idencadenamiento { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string tabla1 { get; set; }
        [DataMember]
        public string columna1 { get; set; }
        [DataMember]
        public string tabla2 { get; set; }
        [DataMember]
        public string columna2 { get; set; }
    }

    /// <summary>
    /// Entidad Columna
    /// </summary>
    [DataContract]
    [Serializable]
    public class Columna
    {
        [DataMember]
        public Int64 idreporte { get; set; }
        [DataMember]
        public Int64 idcolumna { get; set; }
        [DataMember]
        public string tabla { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string tipo_dato { get; set; }
        [DataMember]
        public int longitud { get; set; }
        [DataMember]
        public int precision { get; set; }
        [DataMember]
        public int escala { get; set; }
    }

    /// <summary>
    /// Entidad Condicion
    /// </summary>
    [DataContract]
    [Serializable]
    public class Condicion
    {
        [DataMember]
        public Int64 idreporte { get; set; }
        [DataMember]
        public Int64 idcondicion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string andor { get; set; }
        [DataMember]
        public string parentesisizq { get; set; }
        [DataMember]
        public int? tipo1 { get; set; }
        [DataMember]
        public string tabla1 { get; set; }
        [DataMember]
        public string columna1 { get; set; }
        [DataMember]
        public string valor1 { get; set; }
        [DataMember]
        public string operador { get; set; }
        [DataMember]
        public int? tipo2 { get; set; }
        [DataMember]
        public string tabla2 { get; set; }
        [DataMember]
        public string columna2 { get; set; }
        [DataMember]
        public string valor2 { get; set; }
        [DataMember]
        public string parentesisder { get; set; }
        [DataMember]
        public int? idlista { get; set; }
        [DataMember]
        public string nomlista { get; set; }
    }

    /// <summary>
    /// Entidad Columna
    /// </summary>
    [DataContract]
    [Serializable]
    public class ColumnaReporte
    {
        [DataMember]
        public Int64 idreporte { get; set; }
        [DataMember]
        public Int64 idcolumna { get; set; }
        [DataMember]
        public Int64 orden { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public string tabla { get; set; }
        [DataMember]
        public string columna { get; set; }
        [DataMember]
        public string titulo { get; set; }
        [DataMember]
        public string formato { get; set; }
        [DataMember]
        public string tipodato { get; set; }
        [DataMember]
        public string alineacion { get; set; }
        [DataMember]
        public int? ancho { get; set; }
        [DataMember]
        public Boolean total { get; set; }
        [DataMember]
        public string formula { get; set; }
    }

    /// <summary>
    /// Entidad Orden
    /// </summary>
    [DataContract]
    [Serializable]
    public class Orden
    {
        [DataMember]
        public Int64 idreporte { get; set; }
        [DataMember]
        public Int64 idorden { get; set; }
        [DataMember]
        public string tabla { get; set; }
        [DataMember]
        public string columna { get; set; }
        [DataMember]
        public string orden { get; set; }
    }

    /// <summary>
    /// Entidad Grupo
    /// </summary>
    [DataContract]
    [Serializable]
    public class Grupo
    {
        [DataMember]
        public Int64 idreporte { get; set; }
        [DataMember]
        public Int64 idgrupo { get; set; }
        [DataMember]
        public string tabla { get; set; }
        [DataMember]
        public string columna { get; set; }        
    }

    /// <summary>
    /// Entidad Plantilla
    /// </summary>
    [DataContract]
    [Serializable]
    public class Plantilla
    {
        [DataMember]
        public Int64 idreporte { get; set; }
        [DataMember]
        public Int64 idplantilla { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string archivo { get; set; }
    }
}
