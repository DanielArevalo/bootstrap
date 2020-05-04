using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace  Xpinn.Seguridad.Entities
{
      public enum TipoProveedorBaseDatos
    {
        Oracle = 0,
        SqlServer = 1
    }

    public class BaseDatosEntidad
    {

        String owner = "";
        String tablaseleccionada = "";

        public String Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        public String Tablaseleccionada
        {
            get { return tablaseleccionada; }
            set { tablaseleccionada = value; }
        }

        String nombre = "";

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        String cadenaConexion = "";

        public String CadenaConexion
        {
            get { return cadenaConexion; }
            set { cadenaConexion = value; }
        }
        TipoProveedorBaseDatos tipoproveedor;

        public TipoProveedorBaseDatos TipoProveedor
        {
            get { return tipoproveedor; }
            set { tipoproveedor = value; }
        }
        List<TablaEntidad> tablas = new List<TablaEntidad>();

        public List<TablaEntidad> Tablas
        {
            get { return tablas; }
            set { tablas = value; }
        }
        RegistrosEntidad registros = new RegistrosEntidad();

        public RegistrosEntidad Registros
        {
            get { return registros; }
            set { registros = value; }
        }
    }
}
