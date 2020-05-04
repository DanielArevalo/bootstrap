<%@ WebHandler Language="C#" Class="PrintHandler" %>

using System;
using System.Web;
using System.Linq;
using Neodynamic.SDK.Web;
using Xpinn.Util;
using System.Configuration;
using System.Collections.Generic;

public class PrintHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {

        if (WebClientPrint.ProcessPrintJob(context.Request.Url.Query))
        {
            string param1 = context.Server.UrlDecode(context.Request["sid"]);
            string param2 = context.Server.UrlDecode(context.Request["ope"]);
            Int64 cod_ope;
            if (param2 == null || param2.Trim() == "" || param2.Trim() == "undefined")
                return;
            cod_ope = Convert.ToInt64(param2);
            decimal efectivo = 0, cheques = 0, otros = 0;
            // Determinar el usuario
            CifradoBusiness cifrar = new CifradoBusiness();
            Usuario pUsuario = new Usuario();
            try
            {
                pUsuario.identificacion = Convert.ToString(context.Request.QueryString["Us"]);
                pUsuario.clave_sinencriptar = Convert.ToString(context.Request.QueryString["Pw"]);
                pUsuario.identificacion = "EXPINNADM";
                pUsuario.clave_sinencriptar = "Expadm2019*";
                pUsuario.clave = cifrar.Encriptar(pUsuario.clave_sinencriptar);
            }
            catch
            {
                pUsuario = null;
            }
            try
            {
                pUsuario.clave_sinencriptar = cifrar.Desencriptar(pUsuario.clave);
            }
            catch
            {
                pUsuario = DeterminarUsuario("DataBase");
            }

            // Determinar los datos de la empresa
            Xpinn.Caja.Services.PersonaService peopleServicio = new Xpinn.Caja.Services.PersonaService();
            Xpinn.Caja.Entities.Persona empresa = new Xpinn.Caja.Entities.Persona();
            empresa = peopleServicio.ConsultarEmpresa(empresa, pUsuario);

            // Determinar los datos del recibo
            Xpinn.Caja.Entities.Persona people = new Xpinn.Caja.Entities.Persona();
            Xpinn.Caja.Entities.TipoOperacion tipOpe = new Xpinn.Caja.Entities.TipoOperacion();
            Xpinn.Caja.Services.TipoOperacionService tipOpeService = new Xpinn.Caja.Services.TipoOperacionService();
            List<Xpinn.Caja.Entities.TipoOperacion> lstConsulta = new List<Xpinn.Caja.Entities.TipoOperacion>();
            tipOpe.cod_operacion = Convert.ToString(cod_ope);
            try { lstConsulta = tipOpeService.ConsultarTranCred(tipOpe, pUsuario); } catch { }
            Xpinn.Caja.Entities.TipoOperacion operacion = lstConsulta.First();
            people.cod_persona = long.Parse(operacion.cod_persona);
            people = peopleServicio.ConsultarPersonaXCodigo(people, pUsuario);

            // Determinar el número de factura        
            tipOpe.num_factura = tipOpeService.ConsultarFactura(Convert.ToInt64(cod_ope), true, pUsuario);

            // Consultar el valor en efectivo
            Xpinn.Caja.Entities.Persona totales = new Xpinn.Caja.Entities.Persona();
            totales.cod_ope = cod_ope;
            totales = peopleServicio.ConsultarValorEfectivo(totales, pUsuario);
            efectivo = totales.valor_total_efectivo;

            // Consultar el valor en cheque
            totales.cod_ope = cod_ope;
            totales = peopleServicio.ConsultarValorCheque(totales, pUsuario);
            cheques = totales.valor_total_cheques;

            // Consultar el valor por otras formas de pago
            totales.cod_ope = cod_ope;
            totales = peopleServicio.ConsultarValorOtros(totales, pUsuario);
            otros = totales.valor_total_otros;


            Xpinn.Comun.Entities.General pData = new Xpinn.Comun.Entities.General();
            Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
            pData = ConsultaData.ConsultarGeneral(19, pUsuario);
            Int64 parametro = Convert.ToInt64(pData.valor);

            // Creando los comandos ESC/POS para la impresión del recibo
            string ESC = "0x1B";                //ESC byte en notación hex
            string NewLine = "0x0A";            //LF byte en notación hex 
            string cmds = ESC + "@";            //Inicializando la impresora(ESC @)
            string boldDoble = "!" + "0x19";    //Emphasized + Double-height + Double-width mode selected (ESC ! (8 + 16 + 32)) 56 dec => 38 hex
            string bold = "!" + "0x09";         //letra en negrilla
            string letranormal = "!" + "0x01";  //Character font A selected (ESC ! 1)
            string letragrande = ESC + "g";       //Determinar el tipo de letra(ESC g)

            // Generando el recibo
            cmds += ESC + boldDoble;
            int longitudCampo = 23;
            string empresa1 = empresa.nom_empresa.Length <= longitudCampo ? empresa.nom_empresa : empresa.nom_empresa.Substring(0, longitudCampo);
            string empresa2 = empresa.nom_empresa.Length <= longitudCampo ? "" : empresa.nom_empresa.Substring(longitudCampo, empresa.nom_empresa.Length - longitudCampo);
            cmds += AjustarCampo(empresa1, longitudCampo, "I") + " " + "Recibo No.";
            cmds += NewLine;
            cmds += AjustarCampo(empresa2.Trim(), longitudCampo, "I") + " " + tipOpe.num_factura;
            cmds += NewLine;
            cmds += "NIT " + AjustarCampo(empresa.nit, longitudCampo, "I") + " ";
            cmds += NewLine;
            cmds += ESC + letranormal + letragrande;

            longitudCampo = 10;
            cmds += "                            ";
            cmds += NewLine;
            cmds += AjustarCampo("Fecha: ", longitudCampo, "I") + DateTime.Now.ToString();
            cmds += NewLine;
            cmds += AjustarCampo("Tel:", longitudCampo, "I") + empresa.telefono;
            cmds += NewLine;
            cmds += AjustarCampo("Operac.:", longitudCampo, "I") + cod_ope.ToString();
            cmds += NewLine;
            cmds += AjustarCampo("Oficina:", longitudCampo, "I") + operacion.nombre_oficina;
            cmds += NewLine;
            cmds += AjustarCampo("Caja:", longitudCampo, "I") + operacion.nombre_caja;
            cmds += NewLine;
            cmds += AjustarCampo("Cajero:", longitudCampo, "I") + operacion.nombre_cajero;
            cmds += NewLine;
            cmds += AjustarCampo("Ciudad:", longitudCampo, "I") + people.ciudad;
            cmds += NewLine;
            cmds += AjustarCampo("Fecha:", longitudCampo, "I") + Convert.ToString(operacion.fecha_operacion.HasValue ? operacion.fecha_operacion.Value.ToShortDateString() : " ");
            cmds += NewLine;
            people.nom_persona = QuitarCaracteresEspeciales(people.nom_persona);
            if (people.nom_persona.Length <= 30)
            {
                cmds += AjustarCampo("Cliente:", longitudCampo, "I") + AjustarCampo(people.nom_persona, 30, "I");
                cmds += NewLine;
            }
            else
            {
                cmds += AjustarCampo("Cliente:", longitudCampo, "I") + AjustarCampo(people.nom_persona.Substring(0, 30), 30, "I");
                cmds += NewLine;
                cmds += AjustarCampo("        ", longitudCampo, "I") + AjustarCampo(people.nom_persona.Substring(30, people.nom_persona.Length - 30), 30, "I");
                cmds += NewLine;
            }
            cmds += AjustarCampo("Identif.:", longitudCampo, "I") + people.identificacion;
            cmds += NewLine + NewLine;
            cmds += ESC + bold + AjustarCampo("Concepto", 15, "I") + " " + AjustarCampo("Nro Ref", 12, "I") + " " + AjustarCampo("Valor", 11, "D") + ESC + letranormal;
            cmds += NewLine;
            decimal acum = 0;
            foreach (Xpinn.Caja.Entities.TipoOperacion item in lstConsulta)
            {

                cmds += AjustarCampo(EsNulo(item.concepto, "").Trim(), 15, "I") + " " + AjustarCampo(EsNulo(item.nro_producto, "").Trim(), 12, "I") + " " + AjustarCampo(item.valor.ToString("N0"), 11, "D");
                cmds += NewLine;
                acum += item.valor;
            }
            longitudCampo = 28;
            cmds += AjustarCampo("SUBTOTAL ", longitudCampo, "D") + AjustarCampo(acum.ToString("N0"), 12, "D");
            cmds += NewLine;
            cmds += AjustarCampo("BASE IVA ", longitudCampo, "D") + AjustarCampo(tipOpe.valor_base.ToString("N0"), 12, "D");
            cmds += NewLine;
            cmds += AjustarCampo("IVA      ", longitudCampo, "D") + AjustarCampo(tipOpe.valor_iva.ToString("N0"), 12, "D");
            cmds += NewLine;
            cmds += AjustarCampo("TOTAL    ", longitudCampo, "D") + AjustarCampo((acum + tipOpe.valor_iva).ToString("N0"), 12, "D");
            cmds += NewLine;
            cmds += "EFECTIVO: " + AjustarCampo(efectivo.ToString("N0"), 12, "D");
            cmds += NewLine;
            cmds += "CHEQUE:   " + AjustarCampo(cheques.ToString("N0"), 12, "D");
            cmds += NewLine;
            cmds += "OTROS:    " + AjustarCampo(otros.ToString("N0"), 12, "D");
            cmds += NewLine;
            cmds += "          ";
            cmds += NewLine;
          
            cmds += NewLine + NewLine;


            if (parametro == 1)
            {
                List<Xpinn.Caja.Entities.TipoOperacion> lstProductos = new List<Xpinn.Caja.Entities.TipoOperacion>();
                lstProductos = tipOpeService.ConsultarSaldoProductos(Convert.ToInt64(tipOpe.cod_operacion), pUsuario);

                if (lstProductos.Count > 0)
                {
                    cmds += NewLine;
                    cmds += AjustarCampo("Saldos Productos", 25, "I");
                    cmds += NewLine;

                    cmds += AjustarCampo("Producto", 12, "I")  + AjustarCampo("Nro. Ref", 8, "I")   + AjustarCampo("Valor", 11, "D") + " ";

                    foreach (Xpinn.Caja.Entities.TipoOperacion item in lstProductos)
                    {

                        cmds += NewLine;
                        cmds += AjustarCampo(EsNulo(item.nom_tipo_producto, "").Trim(), 12, "I") + " " + AjustarCampo(EsNulo(item.nro_producto, "").Trim(), 8, "I") + " " + AjustarCampo(item.saldo.ToString("N0"), 11, "D");
                        cmds += NewLine;

                    }
                }

            }

            cmds += ESC + bold + "OBSERVACIONES:" + ESC + letranormal;
            cmds += NewLine;
            if (operacion.observaciones != null)
            {
                int lonIni = 0;
                int lonFin = 40;
                while (operacion.observaciones.Length >= lonIni)
                {
                    if (operacion.observaciones.Length > lonFin)
                        cmds += AjustarCampo(operacion.observaciones.Substring(lonIni, lonFin - lonIni), 40, "I");
                    else
                        cmds += AjustarCampo(operacion.observaciones.Substring(lonIni, operacion.observaciones.Length - lonIni), 40, "I");
                    cmds += NewLine;
                    lonIni = lonFin;
                    lonFin += 40;
                }
            }



            // Enviar los datos a la impresora
            ClientPrintJob cpj1 = new ClientPrintJob();
            cpj1.PrinterCommands = cmds;
            cpj1.FormatHexValues = true;
            cpj1.ClientPrinter = new DefaultPrinter();
            ClientPrintJobGroup cpjg = new ClientPrintJobGroup();
            cpjg.Add(cpj1);

            // Enviando
            context.Response.ContentType = "application/octet-stream";
            context.Response.BinaryWrite(cpjg.GetContent());
            context.Response.End();

        }

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    public Xpinn.Util.Usuario DeterminarUsuario(string pName = "DataBase")
    {
        try
        {
            // Determinar parámetros de conexión del webconfig
            string connectionString = ConfigurationManager.ConnectionStrings[pName].ConnectionString;
            string[] sParametros = new string[3] { "", "", "" };
            sParametros = connectionString.Split(';');
            string[] sTexto = new string[3] { "", "", "" };
            sTexto = sParametros[1].Split('=');
            string sUsuario = sTexto[1];
            sTexto = sParametros[2].Split('=');
            string sClave = sTexto[1];
            // Definición de entidades y servicios
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            Xpinn.Seguridad.Services.UsuarioService usuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
            // Validar usuario y obtener accesos
            usuario.identificacion = sUsuario;
            usuario.clave_sinencriptar = sClave;
            usuario = usuarioServicio.ValidarUsuario(usuario.identificacion, sClave, "", "", usuario);

            usuario.conexionBD = pName;

            return usuario;
        }
        catch
        {
            return null;
        }
    }

    public string AjustarCampo(string pCampo, int pAncho, string pAlineacion)
    {
        if (pCampo == null)
            pCampo = "";
        string nuevoCampo = "";
        if (pCampo.Length > pAncho)
        {
            nuevoCampo = pCampo.Substring(0, pAncho);
        }
        else
        {
            nuevoCampo = pCampo;
            for (int i = pCampo.Length; i < pAncho; i++)
            {
                if (pAlineacion == "I")
                    nuevoCampo += " ";
                if (pAlineacion == "D")
                    nuevoCampo = " " + nuevoCampo;
            }
        }
        return nuevoCampo;
    }

    public string QuitarCaracteresEspeciales(string pCampo)
    {
        if (pCampo == null)
            return pCampo;
        return pCampo.Replace("Ñ", "N");
    }

    public string EsNulo(string pDato, string pDefault)
    {
        if (pDato == null)
            return pDefault;
        return pDato;
    }

    //public string SepararCampo(string pCampo, int pAncho, int pParte)
    //{
    //    if (pCampo == null)
    //        pCampo = "";

    //    string[] campos = new string[] {"", "", ""};
    //    int posEspacio = -1;
    //    for (int i = pCampo.Length; i < pAncho; i++)
    //    {

    //    }
    //    return nuevoCampo;
    //}

}