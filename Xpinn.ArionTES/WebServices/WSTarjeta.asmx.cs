using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Text;
using System.IO;
using Xpinn.Util;
using Xpinn.TarjetaDebito;


namespace WebServices
{
    /// <summary>
    /// Descripción breve de WSTarjeta
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class WSTarjeta : System.Web.Services.WebService
    {
        public static string gSeparadorMiles = ".";
        public static string gSeparadorDecimal = ",";

        [WebMethod]
        public string testService(string sec)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString;
                // Definición de entidades y servicios            
                Conexion conexion = new Conexion();
                return conexion.TestDeterminarUsuarioOficina(sec);
            }
            catch (Exception ex)
            {
                return "Error en servidor: " + ex;
            }
        }

        [WebMethod]
        public string DatosClientes(int ptipo_convenio, string sec, string rutayarchivo)
        {
            string perror = "";
            System.IO.StreamReader file = ArchivoClientes(ptipo_convenio, sec, rutayarchivo, ref perror);
            if (file == null)
                return perror;
            string data = "";
            string line = "";
            int counter = 0;
            while ((line = file.ReadLine()) != null)
            {
                data += line + Environment.NewLine;
                counter++;
            }
            return data;
        }

        [WebMethod]
        public System.IO.StreamReader ArchivoClientes(int ptipo_convenio, string sec, string rutayarchivo, ref string perror)
        {
            perror = "";
            /// Validar acceso del usuario
            Xpinn.Seguridad.Services.AccesoService BOAcceso = new Xpinn.Seguridad.Services.AccesoService();
            Conexion conexion = new Conexion();
            Usuario _usuario = new Usuario();
            _usuario = conexion.DeterminarUsuarioOficina(sec, ref perror);
            if (_usuario == null)
            {
                perror = "No se pudo validar usuario y clave de acceso";
                return null;
            }
            // Listado de cuentas
            string separador = ",";
            List<Xpinn.TarjetaDebito.Entities.CuentaCoopcentral> lstConsulta = new List<Xpinn.TarjetaDebito.Entities.CuentaCoopcentral>();
            Xpinn.TarjetaDebito.Services.CuentaService cuentaservice = new Xpinn.TarjetaDebito.Services.CuentaService();
            Xpinn.TarjetaDebito.Entities.Cuenta ecuenta = new Xpinn.TarjetaDebito.Entities.Cuenta();
            try { lstConsulta = cuentaservice.ListarCuentaCoopcentral(ecuenta, _usuario); }
            catch (Exception ex) { perror = "Error:" + ex.Message; return null;  }
            if (lstConsulta == null)
            {
                perror = "No se pudo generar archivo de listado de cuentas";
                return null;
            }
            // Determinar el convenio
            string convenio = ConvenioTarjeta(ptipo_convenio, _usuario);
            // Generando el archivo
            System.IO.StreamWriter newfile = new System.IO.StreamWriter(rutayarchivo, true, Encoding.UTF8);
            newfile = cuentaservice.GenerarArchivoClientesCoopcentral(lstConsulta, separador, newfile, _usuario);
            if (newfile == null)
            {
                perror = "No se pudo generar archivo -->" + rutayarchivo + "<-- -->" + convenio + "<--";
                return null;
            }
            newfile.Close();
            // Verificar que el archivo se creeo correctamente
            System.IO.StreamReader file = new System.IO.StreamReader(rutayarchivo);
            if (file == null)
            {
                perror = "No se pudo leer archivo " + rutayarchivo;
                return null;
            }
            return file;
        }

        private string VerificarCampo(string pcampo)
        {
            Xpinn.TarjetaDebito.Services.CuentaService _cuen = new Xpinn.TarjetaDebito.Services.CuentaService();
            return _cuen.VerificarCampo(pcampo);
        }

        private string VerificarTexto(string pcampo, string pseparador)
        {
            Xpinn.TarjetaDebito.Services.CuentaService _cuen = new Xpinn.TarjetaDebito.Services.CuentaService();
            return _cuen.VerificarTexto(pcampo, pseparador);
        }

        public string ConvenioTarjeta(int ptipo_convenio, Usuario pusuario)
        {
            string convenio = "";
            // Cargar el dato del convenio
            Xpinn.TarjetaDebito.Services.TarjetaConvenioService convenioServicio = new Xpinn.TarjetaDebito.Services.TarjetaConvenioService();
            Xpinn.TarjetaDebito.Entities.TarjetaConvenio tarjetaConvenio = new Xpinn.TarjetaDebito.Entities.TarjetaConvenio();
            List<Xpinn.TarjetaDebito.Entities.TarjetaConvenio> lsttarjetaConvenio = new List<Xpinn.TarjetaDebito.Entities.TarjetaConvenio>();
            tarjetaConvenio.tipo_convenio = ptipo_convenio;
            lsttarjetaConvenio = convenioServicio.ListarTarjetaConvenio(tarjetaConvenio, pusuario);
            if (lsttarjetaConvenio != null)
                if (lsttarjetaConvenio.Count > 0)
                    if (lsttarjetaConvenio[0] != null)
                        convenio = lsttarjetaConvenio[0].codigo_bin;
            return convenio;
        }

        [WebMethod]
        public Int64 CargarMovimientos(int ptipo_convenio, string sec, string[] data, DateTime fecha, ref string perror)
        {
            Int64 pCodOpe = 0;
            /// Validar acceso del usuario
            Xpinn.Seguridad.Services.AccesoService BOAcceso = new Xpinn.Seguridad.Services.AccesoService();
            Conexion conexion = new Conexion();
            Usuario _usuario = new Usuario();
            _usuario = conexion.DeterminarUsuarioOficina(sec, ref perror);
            if (_usuario == null)
            {
                perror = "No se pudo validar usuario y clave de acceso";
                return 0;
            }
            // Determinar el convenio
            string convenio = ConvenioTarjeta(ptipo_convenio, _usuario);
            // Convertir el archivo en un list 6 aplicarlos
            List<Xpinn.TarjetaDebito.Entities.MovimientoCoopcentral> lstConsulta = new List<Xpinn.TarjetaDebito.Entities.MovimientoCoopcentral>();
            Xpinn.TarjetaDebito.Services.CuentaService _cuen = new Xpinn.TarjetaDebito.Services.CuentaService();
            lstConsulta = _cuen.CargarArchivoMovCoopcentral(data, gSeparadorMiles, gSeparadorDecimal);
            if (lstConsulta != null)
            {
                if (lstConsulta.Count > 0)
                {
                    // Validar el archivo para cargarlo
                    perror = _cuen.ValidarArchivoCargaCoopcentral(1, convenio, lstConsulta, true, _usuario);
                    if (perror.Trim() != "")
                    {
                        return 0;
                    }
                    // Aplicar los movimientos                    
                    if (!_cuen.AplicarMovimientosCoopcentral(convenio, fecha, lstConsulta, _usuario, ref perror, ref pCodOpe, 1))
                    {
                        return 0;
                    }
                }
            }
            else
            {
                perror = "No se pudo cargar el archivo o no se pudieron encontrar movimientos";
            }
            return pCodOpe;
        }



    }
}
