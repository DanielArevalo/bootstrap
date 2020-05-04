using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Services;

public partial class DataCredito : GlobalWeb
{
    DataCreditoServices servicesDataCredito = new DataCreditoServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(servicesDataCredito.CodigoPrograma, "L");
            Site toolBar = (Site)this.Master;
       
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicesDataCredito.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {       
            if (!IsPostBack)
            {
                fechaCierre.Inicializar("R", "D");
                cargaDropDown();
                PanelDatos.Visible = true;
                PanelFinal.Visible = false;
                fechaCierre.Visible = true;                       
                btnGenerar.Visible = true;
                rblEntidadReporta_SelectedIndexChanged(rblEntidadReporta, null);         
             }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicesDataCredito.GetType().Name + "D", "Page_Load", ex);
        }
    }


    protected void cargaDropDown()
    {
        ddlCodPaquete.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlCodPaquete.Items.Insert(1, new ListItem("CARTERA TOTAL", "6"));
        ddlCodPaquete.Items.Insert(2, new ListItem("CARTERA TOTAL - OPERACIONES LEASING", "13"));
        ddlCodPaquete.Items.Insert(3, new ListItem("CARTERA TOTAL - SECTOR FIDUCIARIO", "14"));
        ddlCodPaquete.Items.Insert(4, new ListItem("CUENTA CORRIENTE", "1"));
        ddlCodPaquete.Items.Insert(5, new ListItem("CUENTAS DE AHORRO", "23"));
        ddlCodPaquete.Items.Insert(6, new ListItem("ENDEUDAMIENTO GLOBAL", "5"));
        ddlCodPaquete.Items.Insert(7, new ListItem("SECTOR ASEGURADOR CARTERA", "19"));
        ddlCodPaquete.Items.Insert(8, new ListItem("SECTOR ASEGURADOR INTERMEDIARIOS", "18"));
        ddlCodPaquete.Items.Insert(9, new ListItem("SECTOR ASEGURADOR SINIESTROS", "20"));
        ddlCodPaquete.Items.Insert(10, new ListItem("SECTOR REAL", "7"));
        ddlCodPaquete.Items.Insert(11, new ListItem("SECTOR REAL - CARTERA PRESTAMOS", "12"));
        ddlCodPaquete.Items.Insert(12, new ListItem("SECTOR REAL - CARTERA SERVICIOS", "11"));
        ddlCodPaquete.Items.Insert(13, new ListItem("SECTOR SOLIDARIO - COOPERATIVO", "21"));
        ddlCodPaquete.Items.Insert(14, new ListItem("TARJETA DE CREDITO", "2"));
        ddlCodPaquete.SelectedIndex = 0;
        ddlCodPaquete.DataBind();

        ddlTipoEntidad.Items.Insert(0, new ListItem("Seleccione un item", "-1"));
        ddlTipoEntidad.Items.Insert(1, new ListItem("ACTIVIDADES AUXILIARES Y  DE INTERMEDIACION FINANCIERAS", "113"));
        ddlTipoEntidad.Items.Insert(2, new ListItem("ACTIVIDADES DE ALQUILER, EMPRESARIALES  Y INMOBILIARIAS", "109"));
        ddlTipoEntidad.Items.Insert(3, new ListItem("ACTIVIDADES DE ESPARCIMIENTO, CULTURALES Y DEPORTIVAS", "122"));
        ddlTipoEntidad.Items.Insert(4, new ListItem("ACTIVIDADES DE  EDICION E IMPRESION  Y DE REPRODUCCION DE GR", "124"));
        ddlTipoEntidad.Items.Insert(5, new ListItem("ACTIVIDADES  DE  ASOCIACIONES  NCP", "132"));
        ddlTipoEntidad.Items.Insert(6, new ListItem("ACTIVIDADES  DE  SERVICIOS  COMUNITARIOS  SOCIALES  Y  PERSO", "128"));
        ddlTipoEntidad.Items.Insert(7, new ListItem("ADMINISTRACION .PUBLICA Y DEFENSA ,PLANES DE SEGURIDAD", "110"));
        ddlTipoEntidad.Items.Insert(8, new ListItem("AGENCIAS DE SEGUROS", "16"));
        ddlTipoEntidad.Items.Insert(9, new ListItem("AGRICULTURA,GANADERIA,CAZA,SILVICULTURA Y PESCA", "101"));
        ddlTipoEntidad.Items.Insert(10, new ListItem("AGUA, GAS Y  ELECTRICIDAD", "104"));
        ddlTipoEntidad.Items.Insert(11, new ListItem("ALM.  GENERAL  DE   DEPOSITO", "6"));
        ddlTipoEntidad.Items.Insert(12, new ListItem("ALQUILER DE MAQUINARIA Y EQUIPO SIN OPERARIOS Y DE EFECTOS P", "139"));
        ddlTipoEntidad.Items.Insert(13, new ListItem("ASESORES DE SEGUROS", "120"));
        ddlTipoEntidad.Items.Insert(14, new ListItem("ASOCIACIONES  MUTUALISTAS", "209"));
        ddlTipoEntidad.Items.Insert(15, new ListItem("BANCO", "1"));
        ddlTipoEntidad.Items.Insert(16, new ListItem("BANCO", "30"));
        ddlTipoEntidad.Items.Insert(17, new ListItem("BANCO CENTRAL HIPOTECARIO", "21"));
        ddlTipoEntidad.Items.Insert(18, new ListItem("BANCO DE LA REPUBLICA", "0"));
        ddlTipoEntidad.Items.Insert(19, new ListItem("CAJA DE COMPENSACION", "52"));
        ddlTipoEntidad.Items.Insert(20, new ListItem("CAMARAS  DE  COMERCIO", "51"));
        ddlTipoEntidad.Items.Insert(21, new ListItem("CASAS   DE   CAMBIO", "29"));
        ddlTipoEntidad.Items.Insert(22, new ListItem("CIA. DE FINAN.COMERCIAL", "4"));
        ddlTipoEntidad.Items.Insert(23, new ListItem("CIA. DE SEGUROS DE VIDA", "14"));
        ddlTipoEntidad.Items.Insert(24, new ListItem("CIA. DE SEGUROS GENERALES", "13"));
        ddlTipoEntidad.Items.Insert(25, new ListItem("COMERCIO  AL POR MAYOR  Y  AL MENOR", "106"));
        ddlTipoEntidad.Items.Insert(26, new ListItem("COMPANIA REASEGURADORA", "9"));
        ddlTipoEntidad.Items.Insert(27, new ListItem("COMPANIAS  DE  LEASING", "18"));
        ddlTipoEntidad.Items.Insert(28, new ListItem("COMPAÑIAS  DE  TELEFONIA   CELULAR", "108"));
        ddlTipoEntidad.Items.Insert(29, new ListItem("COMUNICACIONES", "119"));
        ddlTipoEntidad.Items.Insert(30, new ListItem("CONSTRUCCION", "105"));
        ddlTipoEntidad.Items.Insert(31, new ListItem("CONVENIOS", "996"));
        ddlTipoEntidad.Items.Insert(32, new ListItem("COOPERATIVA FINANCIERA", "32"));
        ddlTipoEntidad.Items.Insert(33, new ListItem("COOPERATIVAS", "50"));
        ddlTipoEntidad.Items.Insert(34, new ListItem("COOPERATIVAS DE APORTES  Y CREDITOS", "210"));
        ddlTipoEntidad.Items.Insert(35, new ListItem("COOPERATIVAS ESPECIALIZADAS  DIFERENTES DE AHORRO Y CREDITO", "203"));
        ddlTipoEntidad.Items.Insert(36, new ListItem("COOPERATIVAS FINANCIERAS", "31"));
        ddlTipoEntidad.Items.Insert(37, new ListItem("COOPERATIVAS  DE TRABAJO  ASOCIADO", "211"));
        ddlTipoEntidad.Items.Insert(38, new ListItem("COOPERATIVAS  ESPECIALIZADAS   DE AHORRO  Y CREDITO", "201"));
        ddlTipoEntidad.Items.Insert(39, new ListItem("COOPERATIVAS  MULTIACTIVAS O INTEGRALES SIN SECCION DE AHORR", "204"));
        ddlTipoEntidad.Items.Insert(40, new ListItem("COOPERATIVAS  MULTIACTIVAS  O INTEGRALES CON SECCION DE AHOR", "202"));
        ddlTipoEntidad.Items.Insert(41, new ListItem("CORP. DE AHORRO Y VIVIENDA", "3"));
        ddlTipoEntidad.Items.Insert(42, new ListItem("CORPORACION FINANCIERA", "2"));
        ddlTipoEntidad.Items.Insert(43, new ListItem("CORREDOR DE SEGUROS", "11"));
        ddlTipoEntidad.Items.Insert(44, new ListItem("ENSEÑANZA", "111"));
        ddlTipoEntidad.Items.Insert(45, new ListItem("ENTID ADM. REGIMEN SOL PRIMA MEDIA", "25"));
        ddlTipoEntidad.Items.Insert(46, new ListItem("ENTIDAD ESTATAL", "12"));
        ddlTipoEntidad.Items.Insert(47, new ListItem("EXPLOTACION DE MINAS Y CANTERAS", "102"));
        ddlTipoEntidad.Items.Insert(48, new ListItem("FABRICACION DE OTROS TIPOS DE EQUIPOS DE TRANSPORTE", "138"));
        ddlTipoEntidad.Items.Insert(49, new ListItem("FABRICACION DE PRENDAS DE VESTIR, PREPARADO Y TEÑIDO DE PIEL", "137"));
        ddlTipoEntidad.Items.Insert(50, new ListItem("FABRICACION DE PRODUCTOS ELABORADOS DE METAL EXCEPTO MAQUINA", "140"));
        ddlTipoEntidad.Items.Insert(51, new ListItem("FABRICACION DE VEHICULOS AUTOMOTORES, REMOLQUES Y SEMIRREMOL", "141"));
        ddlTipoEntidad.Items.Insert(52, new ListItem("FABRICACION   DE SUSTANCIAS Y  PRODUSTOS QUIMICOS", "125"));
        ddlTipoEntidad.Items.Insert(53, new ListItem("FABRICACION  DE ARTICULOS DE CUERO, DE TALABARTERIA Y GUARNE", "136"));
        ddlTipoEntidad.Items.Insert(54, new ListItem("FABRICACION  DE PAPEL, CARTON Y PRODUCTOS DERIVADOS", "135"));
        ddlTipoEntidad.Items.Insert(55, new ListItem("FABRICACION  DE PRODUCTOS  TEXTILES", "131"));
        ddlTipoEntidad.Items.Insert(56, new ListItem("FABRICACION  DE  MAQUINARIA  Y  EQUIPO", "129"));
        ddlTipoEntidad.Items.Insert(57, new ListItem("FABRICACION  DE  PRODUCTOS  DE CAUCHO  Y  DE  PLASTICO", "127"));
        ddlTipoEntidad.Items.Insert(58, new ListItem("FABRICACION   DE OTROS  PRODUCTOS  MINERALES  NO METALICOS", "126"));
        ddlTipoEntidad.Items.Insert(59, new ListItem("FABRICACION   DE PRODUCTOS   METALURGICOS  BASICOS", "133"));
        ddlTipoEntidad.Items.Insert(60, new ListItem("FIDUCIARIAS", "5"));
        ddlTipoEntidad.Items.Insert(61, new ListItem("FONDO DE PENSIONES  Y CESANTIAS", "23"));
        ddlTipoEntidad.Items.Insert(62, new ListItem("FONDOS DE EMPLEADOS", "118"));
        ddlTipoEntidad.Items.Insert(63, new ListItem("FONDOS DE EMPLEADOS", "208"));
        ddlTipoEntidad.Items.Insert(64, new ListItem("GOBERNACIONES", "142"));
        ddlTipoEntidad.Items.Insert(65, new ListItem("GREMIOS", "115"));
        ddlTipoEntidad.Items.Insert(66, new ListItem("HOTELES Y RESTAURANTES", "107"));
        ddlTipoEntidad.Items.Insert(67, new ListItem("INDUSTRIA   MANUFACTURERA", "103"));
        ddlTipoEntidad.Items.Insert(68, new ListItem("INFORMATICA  Y  ACTIVIDADES  CONEXAS", "121"));
        ddlTipoEntidad.Items.Insert(69, new ListItem("INSTITUCION OFICIAL ESPECI", "22"));
        ddlTipoEntidad.Items.Insert(70, new ListItem("INSTITUCIONES  AUXILIARES  DEL  COOPERATIVISMO", "205"));
        ddlTipoEntidad.Items.Insert(71, new ListItem("MUNICIPIOS", "134"));
        ddlTipoEntidad.Items.Insert(72, new ListItem("ORG. FINANCIERO  DEL  EXTERIOR", "20"));
        ddlTipoEntidad.Items.Insert(73, new ListItem("ORG.COOP.DE GRADO SUPERIOR", "8"));
        ddlTipoEntidad.Items.Insert(74, new ListItem("ORGANISMO  DE  SEGUNDO  GRADO", "206"));
        ddlTipoEntidad.Items.Insert(75, new ListItem("ORGANISMO   DE  TERCER   GRADO", "207"));
        ddlTipoEntidad.Items.Insert(76, new ListItem("OTRAS  ACTIVIDADES DE  SERVICIO", "130"));
        ddlTipoEntidad.Items.Insert(77, new ListItem("OTRAS  ACTIVIDADES  EMPRESARIALES", "123"));
        ddlTipoEntidad.Items.Insert(78, new ListItem("OTROS", "997"));
        ddlTipoEntidad.Items.Insert(79, new ListItem("SERVICIOS DE CONSULTORIA", "114"));
        ddlTipoEntidad.Items.Insert(80, new ListItem("SERVICIOS PUBLCOS", "117"));
        ddlTipoEntidad.Items.Insert(81, new ListItem("SERVICIOS SOCIALES  Y  DE SALUD", "112"));
        ddlTipoEntidad.Items.Insert(82, new ListItem("SOCIEDADES COOPERATIVAS", "15"));
        ddlTipoEntidad.Items.Insert(83, new ListItem("SOCIEDADES  DE CAPITALIZACION", "10"));
        ddlTipoEntidad.Items.Insert(84, new ListItem("TRANSPORTE Y ALMACENAMIENTO", "116"));
        ddlTipoEntidad.SelectedIndex = 0;
        ddlTipoEntidad.DataBind();
    }

    protected Boolean ValidarDatos()
    {
        if (fechaCierre.fecha_cierre == null)
        {
            VerError("Seleccione la fecha de corte");
            fechaCierre.Focus();
            return false;
        }
        if (rblEntidadReporta.SelectedIndex == 0) //DATACREDITO
        {

        }
        else //CIFIN
        {
            if (ddlCodPaquete.SelectedIndex == 0)
            {
                VerError("Seleccione el código del paquete.");
                ddlCodPaquete.Focus();
                return false;
            }
            if (ddlTipoEntidad.SelectedIndex == 0)
            {
                VerError("Seleccione el tipo de entidad.");
                ddlTipoEntidad.Focus();
                return false;
            }
        }
        if (txtArchivo.Text == "")
        {
            VerError("Ingrese el nombre del archivo");
            txtArchivo.Focus();
            return false;
        }
        if (rbTipoArchivo.SelectedItem == null)
        {
            VerError("Seleccione el tipo de Archivo para realizar el reporte.");
            rbTipoArchivo.Focus();
            return false;
        }
        return true;
    }

    /// <summary>
    /// Método para cuando se da click en generar el archivo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGenerar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (!ValidarDatos())
            return;
        // Determinando el tipo de archivo
        int tipo = 0;
        if (rbTipoArchivo.SelectedIndex==0)
             tipo = 1;
        else
             tipo = 2;

        // Determinando el tipo de entrega
        string tipoEntrega = "T";
        if (rbTipoEntrega.SelectedIndex == 0)
            tipoEntrega = "T";
        else
            tipoEntrega = "F";
        
        // Determinando el nombre del archivo
        string fic = txtArchivo.Text;
        if (txtArchivo.Text != "")
        {
            if (rbTipoArchivo.SelectedIndex == 0)
            {
                fic = txtArchivo.Text.ToLower().Trim().Contains(".txt") ? txtArchivo.Text : txtArchivo.Text + ".txt";
            }
            else if (rbTipoArchivo.SelectedIndex == 1)
            {
                fic = txtArchivo.Text.Trim().Contains(".csv") ? txtArchivo.Text : txtArchivo.Text + ".csv";
            }
        }
        else
        {
            VerError("Ingrese el Nombre del archivo a Generar");
            return;
        }
        
        string texto = "";

        // Validar si ya se ejecuto el proceso
        DateTime lfecha_corte = Convert.ToDateTime(fechaCierre.fecha_cierre);
        Boolean IsAhorro = ddlCodPaquete.SelectedValue == "23" ? true : false;
        string pFiltro = IsAhorro == true && rblEntidadReporta.SelectedIndex != 0 ? " AND A.TIPO_PRODUCTO = 3" : " AND A.TIPO_PRODUCTO != 3";
        if (servicesDataCredito.ValidarInformacionCentrales(lfecha_corte, pFiltro, (Usuario)Session["Usuario"]) == 0)
        {
            int pNuevo = 1;
            int pCodeudores = 1;
            string sError = "";
            try
            {
                pNuevo = Convert.ToInt32(ConfigurationManager.ConnectionStrings["InfCenRieNuevo"].ConnectionString);
            }
            catch
            {
                pNuevo = 1;
            }
            try
            {
                pCodeudores = Convert.ToInt32(ConfigurationManager.ConnectionStrings["InfCenRieCodeudores"].ConnectionString);
            }
            catch
            {
                pCodeudores = 1;
            }
            //asignando tipo de producto
            int pTipo_prod = 1;
            if (rblEntidadReporta.SelectedIndex == 0)
                pTipo_prod = 1;
            else
            {
                pTipo_prod = 1;
                if (ddlCodPaquete.SelectedValue == "23")
                    pTipo_prod = 3;
            }
            servicesDataCredito.InformacionCentralesRiesgo(lfecha_corte, pNuevo, pCodeudores, pTipo_prod,(Usuario)Session["Usuario"], ref sError);
            if (sError.Trim() != "")
            {
                VerError(sError);
                return;
            }
        }

        // Generando los datos
        List<Xpinn.Cartera.Entities.DataCredito> listaArchivo = new List<Xpinn.Cartera.Entities.DataCredito>();
        if (rblEntidadReporta.SelectedIndex == 0) // DATACREDITO
            servicesDataCredito.ArchivoPlano(Convert.ToDateTime(fechaCierre.fecha_cierre), txttipocuenta.Text, txtoficina.Text, txtCiudad.Text, txtsuscriptor.Text, tipoEntrega, tipo, Convert.ToInt32(cbEmpleados.Checked), (Usuario)Session["Usuario"]);
        else  //CIFIN
        {
            servicesDataCredito.ArchivoPlanoCIFIN(Convert.ToDateTime(fechaCierre.fecha_cierre), Convert.ToInt32(ddlCodPaquete.SelectedValue), Convert.ToInt32(ddlTipoEntidad.SelectedValue), txtCodEntidad.Text, txtProbabilidad.Text, tipoEntrega, tipo, Convert.ToInt32(cbEmpleados.Checked), IsAhorro, (Usuario)Session["Usuario"]);
        }
        listaArchivo = servicesDataCredito.listarArchivoPlano((Usuario)Session["Usuario"]);

        // Copiar información en el archivo
        try
        {
            File.Delete(Server.MapPath("Archivo\\"));
        }
        catch { }
        foreach (Xpinn.Cartera.Entities.DataCredito item in listaArchivo)
        {
            texto = item.descripcion.Replace("Ñ","N").Replace("ñ", "n");
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("Archivo\\") + fic, true, Encoding.UTF8);
            sw.WriteLine(texto);
            sw.Close();
        }
        
        // Copiar el archivo al cliente        
        if (File.Exists(Server.MapPath("Archivo\\") + fic))
        {            
            System.IO.StreamReader sr;
            sr = File.OpenText(Server.MapPath("Archivo\\") + fic);
            texto = sr.ReadToEnd();
            sr.Close();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/octet-stream"; // "text /plain";
            HttpContext.Current.Response.Write(texto);
            HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + fic);
            HttpContext.Current.Response.Flush();
            File.Delete(Server.MapPath("Archivo\\") + fic);
            HttpContext.Current.Response.End();
            PanelFinal.Visible = true;
            PanelDatos.Visible = false;
        }
        else
        {
            VerError("No se genero el archivo para la fecha solicitado no se encontraron datos");
        }
    }
    

    protected void rblEntidadReporta_SelectedIndexChanged(object sender, EventArgs e)
    {
        panelDataCred.Visible = false;
        panelCifin.Visible = false;
        if (rblEntidadReporta.SelectedItem != null)
        {
            if (rblEntidadReporta.SelectedValue == "1")
                panelDataCred.Visible = true;
            else if(rblEntidadReporta.SelectedValue == "2")
                panelCifin.Visible = true;
        }
    }
}
