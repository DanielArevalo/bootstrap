using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Text;
using System.Web;
using System.Linq;

/// <summary>
/// Funciones para uso transversal en capa de presentacion
/// </summary>
public class GlobalWeb : Page
{
    public ExcepcionBusiness BOexcepcion;
    private xpinnWSLogin.Acceso opcionActual;
    protected enum Pagina { Nuevo = 1, Lista = 2, Detalle = 3, Editar = 4, Modificar = 5 };
    protected Dictionary<ParametroCorreo, string> parametrosFormatoCorreo;
    public static string MOV_GRAL_CRED_PRODUC = "idMovGralCredito";
    protected ConnectionDataBase dbConnectionFactory;
    Configuracion global = new Configuracion();
    public static string gFormatoFecha = "dd/MM/yyyy";
    public static string gFormatoTime = "hh:mm:ss";
    public static string gSeparadorDecimal = ",";
    public static string gSeparadorMiles = ".";
    public static Boolean bMostrarPDF = false;
    public static string gMarcarRecogerDesembolso = "";
    public static string gValidarTasaReestructuracion = "";

    xpinnWSLogin.Persona1 _persona;
    public xpinnWSLogin.Persona1 PersonaLogin
    {
        get
        {
            if (_persona == null)
            {
                _persona = Session["persona"] as xpinnWSLogin.Persona1;
            }

            return _persona;
        }
        private set
        {
            _persona = value;
        }
    }

    public GlobalWeb()
    {
        try
        {
            BOexcepcion = new Xpinn.Util.ExcepcionBusiness();
            bMostrarPDF = Convert.ToBoolean(global.ObtenerValorConfig("MostrarPDF"));
            gFormatoFecha = global.ObtenerValorConfig("FormatoFechaBase");
            gSeparadorDecimal = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            if (gSeparadorDecimal == ".")
            {
                gSeparadorMiles = ",";
            }
            else
            {
                gSeparadorMiles = ".";
            }
            gMarcarRecogerDesembolso = global.ObtenerValorConfig("MarcarRecogerDesembolso");
            gValidarTasaReestructuracion = global.ObtenerValorConfig("ValidarTasaReestructuracion");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "GlobalWeb", ex);
        }
    }


    protected void RegistrarPostBack()
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:__doPostBack('', '');", true);
    }

    protected string ReemplazarParametrosEnElMensajeCorreo(string mensaje)
    {
        StringBuilder MyStringBuilder = new StringBuilder(mensaje);

        foreach (var parametro in parametrosFormatoCorreo.Keys)
        {
            MyStringBuilder.Replace(parametro.ToString(), parametrosFormatoCorreo[parametro]);
        }

        MyStringBuilder.Replace("&nbsp;", " ");
        return MyStringBuilder.ToString();
    }

    public bool ValidarProcesoContable(DateTime pFecha, Int64 pTipoOpe)
    {
        xpinnWSLogin.Persona1 pPersona = (xpinnWSLogin.Persona1)Session["persona"];
        // Validar que exista la parametrización contable por procesos
        xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient BOValidacion = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
        List<xpinnWSEstadoCuenta.ProcesoContable> lstProceso = new List<xpinnWSEstadoCuenta.ProcesoContable>();
        lstProceso = BOValidacion.ConsultaProceso(0, pTipoOpe, pFecha, pPersona.identificacion, pPersona.clavesinecriptar);
        if (lstProceso == null)
        {
            return false;
        }
        if (lstProceso.Count <= 0)
        {
            return false;
        }
        return true;
    }

    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);
        // Seguimiento a variables de session
        for (int i = 0; i < Session.Count; i++)
        {
            var crntSession = Session.Keys[i];
        }
    }

    public void ValidarSession()
    {
        if (Session["persona"] == null)
            Response.Redirect("~/Pages/Account/FinSesion.htm");
    }

    public void AdicionarTitulo(string pTitulo, string pTipoPagina)
    {
        try
        {
            if (Session["persona"] != null)
            {
                Panel panelOpcion = (Panel)Master.FindControl("panelOpcion");
                panelOpcion.Visible = string.IsNullOrEmpty(pTitulo) ? false : true;
                switch (pTipoPagina)
                {
                    case "A": // Agregar
                        ((Label)Master.FindControl("lblOpcion")).Text = pTitulo + " - Nuevo";
                        break;
                    case "D": // Detalle
                        ((Label)Master.FindControl("lblOpcion")).Text = pTitulo + " - Detalle";
                        break;
                    case "E": // Editar
                        ((Label)Master.FindControl("lblOpcion")).Text = pTitulo + " - Edicion";
                        break;
                    case "L": // Lista
                        ((Label)Master.FindControl("lblOpcion")).Text = pTitulo + " - Consulta";
                        break;
                    case "G":
                        ((ImageButton)Master.FindControl("btnGuardar")).Attributes.Add("onClick", "LoadingList()");
                        break;
                    default:
                        break;
                }
            }
            else
                Response.Redirect("~/Pages/Account/FinSesion.htm", false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "VisualizarOpciones", ex);
        }
    }

    public void VisualizarTitulo(string nombre)
    {
        ((Label)Master.FindControl("lblOpcion")).Text = nombre;
    }

    public void VisualizarTitulo(string pPrograma, string pTipoPagina)
    {
        try
        {
            if (Session["persona"] != null)
            {
                opcionActual = ObtenerOpcionActual(pPrograma.Trim());
                if (!string.IsNullOrEmpty(opcionActual.nombreopcion))
                {
                    Label lblOpcion = (Label)Master.FindControl("lblOpcion");
                    string NomOpcion = !string.IsNullOrEmpty(opcionActual.nombreopcion) ? (opcionActual.nombreopcion.Contains("|") ? opcionActual.nombreopcion.Split('|')[0] : opcionActual.nombreopcion) : string.Empty; 
                    lblOpcion.Text = NomOpcion;
                    switch (pTipoPagina)
                    {
                        case "A": // Agregar
                            ((Label)Master.FindControl("lblOpcion")).Text = NomOpcion;
                            //((ImageButton)Master.FindControl("btnGuardar")).Attributes.Add("onClick", "LoadingList()");
                            break;
                        case "D": // Detalle
                            ((Label)Master.FindControl("lblOpcion")).Text = NomOpcion;
                            break;
                        case "E": // Editar
                            ((Label)Master.FindControl("lblOpcion")).Text = NomOpcion;
                            break;
                        case "L": // Lista
                            ((Label)Master.FindControl("lblOpcion")).Text = NomOpcion;
                            //((ImageButton)Master.FindControl("btnConsultar")).Attributes.Add("onClick", "LoadingList()");
                            break;
                        case "G":
                            ((ImageButton)Master.FindControl("btnGuardar")).Attributes.Add("onClick", "LoadingList()");
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Response.Redirect("~/General/Global/noAcceso.htm", false);
                }
            }
            else
                Response.Redirect("~/Pages/Account/FinSesion.htm", false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "VisualizarOpciones", ex);
        }
    }

    public void ValidarPermisosGrilla(GridView pGrilla)
    {        
        if (opcionActual.modificar != 1)    // Editar
        {
            if (pGrilla.Columns[2].GetType().ToString() == "System.Web.UI.WebControls.TemplateField")
                pGrilla.Columns[2].Visible = false; 
        }
        if (opcionActual.borrar != 1)       // Borrar   
        {
            if (pGrilla.Columns[3].GetType().ToString() == "System.Web.UI.WebControls.TemplateField")
                pGrilla.Columns[3].Visible = false;      
        }
        if (opcionActual.consultar != 1)    // Detalle
        {
            if (pGrilla.Columns[1].GetType().ToString() == "System.Web.UI.WebControls.TemplateField")
                pGrilla.Columns[1].Visible = false; 
        }
    }

    public string ObtenerCadenaConsulta(ArrayList pCriterios)
    {
        String cadenaConsulta = "";
        int contar = 0;
        try
        {
            for (int j = 0; j < pCriterios.Count; j++)
            {
                String[] criterio;
                criterio = pCriterios[j].ToString().Split('|');

                if (criterio[1].ToString().Trim() != "" && criterio[1].ToString().Trim() != "01/01/0001")
                    contar++;
            }

            if (contar != 0)
                cadenaConsulta += " WHERE ";

            for (int c = 0; c < pCriterios.Count; c++)
            {
                String[] criterio;
                criterio = pCriterios[c].ToString().Split('|');

                if (criterio[1].ToString().Trim() != "" && criterio[1].ToString().Trim() != "01/01/0001")
                {
                    String[] valoresCriterio = criterio[1].ToString().Split(' ');
                    for (int j = 0; j < valoresCriterio.Length; j++)
                    {
                        if (EsFecha(valoresCriterio[j].ToString()))
                        {
                            cadenaConsulta += " YEAR( " + criterio[0].ToString() + ") = '" + valoresCriterio[j].ToString().Trim().Substring(6, 4).ToString() + "'" +
                                              " AND MONTH (" + criterio[0].ToString() + ") = '" + valoresCriterio[j].ToString().Trim().Substring(3, 2).ToString() + "'" +
                                              " AND DAY (" + criterio[0].ToString() + ") = '" + valoresCriterio[j].ToString().Trim().Substring(0, 2).ToString() + "'";
                        }
                        else
                            cadenaConsulta += "UPPER(" + criterio[0].ToString() + ") LIKE " + "UPPER('%" + valoresCriterio[j].ToString() + "%')";

                        if (j != valoresCriterio.Length - 1)
                            cadenaConsulta += " AND ";
                    }

                    if (contar - 1 != 0)
                    {
                        cadenaConsulta += " AND ";
                        contar--;
                    }
                }
            }
            return cadenaConsulta;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "ObtenerCadenaConsulta", ex);
            return null;
        }
    }

    private Boolean EsFecha(String pCadena)
    {
        try
        {
            DateTime.Parse(pCadena);
        }
        catch
        {
            return false;
        }
        return true;
    }

    protected void Navegar(Pagina page)
    {
        try
        {
            switch (page.ToString())
            {
                case "Nuevo":
                    Response.Redirect("Nuevo.aspx", false);
                    break;
                case "Detalle":
                    Response.Redirect("Detalle.aspx", false);
                    break;
                case "Editar":
                    Response.Redirect("Nuevo.aspx?o=E", false);
                    break;
                case "Modificar":
                    Response.Redirect("Lista.aspx?modificar=1", false);
                    break;
                default:
                    Response.Redirect("Lista.aspx", false);
                    break;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "Navegar", ex);
        }
    }

    protected void Navegar(String pPath)
    {
        Response.Redirect(pPath, false);
    }


    public void DeshabilitarObjetosPantalla(Panel plControles)
    {
        try
        {
            foreach (Control control in plControles.Controls)
            {
                if (control is TextBox)
                    ((TextBox)(control)).Enabled = false;
                else if (control is DropDownList)
                    ((DropDownList)(control)).Enabled = false;
                else if (control is RadioButton)
                    ((RadioButton)(control)).Enabled = false;
                else if (control is CheckBox)
                    ((CheckBox)(control)).Enabled = false;
                else if (control is Panel)
                    DeshabilitarObjetosPantalla((Panel)control);                
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "LimpiarPanel", ex);
        }
    }
    

    public void VerError(String pError)
    {
        try
        {
            if (pError.Trim() == "")
            {
                ((Label)Master.FindControl("lblError")).Visible = false;
                ((Label)Master.FindControl("lblError")).Text = pError;
                ((Panel)Master.FindControl("plError")).Visible = false;
            }
            else
            {
                ((Label)Master.FindControl("lblError")).Visible = true;
                ((Label)Master.FindControl("lblError")).Text = pError;
                ((Panel)Master.FindControl("plError")).Visible = true;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "VerError", ex);
        }
    }

    public void OcultarError()
    {
        try
        {
            ((Panel)Master.FindControl("pError")).Visible = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "OcultarError", ex);
        }
    }

    public void VerErrorControl(String pError)
    {
        try
        {
            ClientScript.RegisterStartupScript(this.GetType(), "jsError", "alert('" + pError + "')", true);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "VerError", ex);
        }
    }

    public void CerrarNuevo(string pPrograma)
    {
        try
        {
            ClientScriptManager close = Page.ClientScript;
            close.RegisterClientScriptBlock(this.GetType(), "ClientScript", "javascript:cambiarDisplayLista('divL" + pPrograma + "', 'divN" + pPrograma + "')", true);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "CerrarNuevo", ex);
        }
    }


    public void ConfirmarEliminarFila(GridViewRowEventArgs e, String pBoton)
    {
        if (e.Row == null)
            return;
        try
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    ImageButton btnGrilla = (ImageButton)e.Row.FindControl(pBoton);
                    if (btnGrilla != null)
                        btnGrilla.Attributes.Add("onClick", "return confirm('Esta seguro que desea eliminar este registro?')");
                    break;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "ConfirmarEliminarFila", ex);
        }
    }

    public void GuardarValoresConsulta(Panel plControles, string pPrograma)
    {
        try
        {
            foreach (Control control in plControles.Controls)
            {
                if (control is TextBox)
                {
                    if (((TextBox)(control)).Text.Trim() != "")
                    {
                        string txtTexto = ((TextBox)(control)).Text.Trim();
                        Session.Add(pPrograma + "." + control.ID, txtTexto);
                    }
                }
                else if (control is DropDownList)
                {
                    if (((DropDownList)(control)).SelectedIndex != 0)
                    {
                        string ddlSeleccion = ((DropDownList)(control)).SelectedValue;
                        Session.Add(pPrograma + "." + control.ID, ddlSeleccion);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "GuardarValoresConsulta", ex);
        }
    }

    public void CargarValoresConsulta(Panel plControles, string pPrograma)
    {
        try
        {
            foreach (Control control in plControles.Controls)
            {
                if (Session[pPrograma + "." + control.ID] != null)
                {
                    if (control is TextBox)
                        ((TextBox)(plControles.FindControl(control.ID))).Text = Session[pPrograma + "." + control.ID].ToString();
                    else if (control is DropDownList)
                        ((DropDownList)(plControles.FindControl(control.ID))).SelectedValue = Session[pPrograma + "." + control.ID].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "CargarValoresConsulta", ex);
        }
    }

    public void LimpiarValoresConsulta(Panel plControles, string pPrograma)
    {
        try
        {
            foreach (Control control in plControles.Controls)
            {

                if (control is TextBox)
                {
                    ((TextBox)(control)).Text = "";

                    if (Session[pPrograma + "." + control.ID] != null)
                        Session.Remove(pPrograma + "." + control.ID);
                }
                else if (control.TemplateControl.ToString().Contains("ASP.general_controles_fecha_ascx"))
                {
                    //((UserControl)(control)).Text = "";

                    if (Session[pPrograma + "." + control.ID] != null)
                        Session.Remove(pPrograma + "." + control.ID);
                }
                else if (control is DropDownList)
                {
                    ((DropDownList)(control)).SelectedIndex = 0;

                    if (Session[pPrograma + "." + control.ID] != null)
                        Session.Remove(pPrograma + "." + control.ID);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "LimpiarValoresConsulta", ex);
        }
    }

    public string idObjeto
    {
        get { return ((HiddenField)Master.FindControl("hfObj")).Value.Trim(); }

        set { ((HiddenField)Master.FindControl("hfObj")).Value = value; }
    }

    public int pageSize
    {
        get { return Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]); }
    }

    public String emptyQuery
    {
        get { return ConfigurationManager.AppSettings["EmptyQuery"]; }
    }

    private xpinnWSLogin.Acceso ObtenerOpcionActual(string pIdOpcion)
    {
        try
        {
            xpinnWSLogin.Acceso accesos = new xpinnWSLogin.Acceso();

            if (Session["Procesos"] != null)
            {
                List<xpinnWSLogin.Acceso> lstAccesos = new List<xpinnWSLogin.Acceso>();
                lstAccesos = (List<xpinnWSLogin.Acceso>)Session["Procesos"];

                foreach (xpinnWSLogin.Acceso ent in lstAccesos)
                    if (ent.cod_opcion == Convert.ToInt64(pIdOpcion))
                    {
                        accesos = ent;
                        break;
                    }
            }

            return accesos;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "ObtenerOpcionActual", ex);
            return null;
        }
    }

    public String FormatoDecimal(string str)
    {
        string formateado = "";

        string s = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        if (s == ".")
            str = str.Replace(",", "");
        else
        {
            str = str.Replace(".", "");
            str = str.Replace(",", ".");
        }

        try
        {
            if (str != "" && str.ToLower() != "null" && Convert.ToInt64(str) > 0)
            {

                var strI = Convert.ToInt64(str);  //Convierte a entero y luego a string para quitar ceros a la izquierda
                str = strI.ToString();

                if (str.Length > 9)
                { str = str.Substring(0, 9); }

                int longi = str.Length;
                string mill = "";
                string mil = "";
                string cen = "";


                if (longi > 0 && longi <= 3)
                {
                    cen = str.Substring(0, longi);
                    formateado = Convert.ToInt64(cen).ToString();
                }
                else if (longi > 3 && longi <= 6)
                {
                    mil = str.Substring(0, longi - 3);
                    cen = str.Substring(longi - 3, 3);
                    formateado = Convert.ToInt64(mil) + "." + cen;
                }
                else if (longi > 6 && longi <= 9)
                {
                    mill = str.Substring(0, longi - 6);
                    mil = str.Substring(longi - 6, 3);
                    cen = str.Substring(longi - 3, 3);
                    formateado = Convert.ToInt64(mill) + "." + mil + "." + cen;
                }
                else
                { formateado = "0"; }
            }
            else { if (str.ToLower() != "null") formateado = "0"; else formateado = ""; }

            return formateado.ToString();
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "";
        }
    }

    public string ImagenReporte()
    {
        string cRutaDeImagen;
        cRutaDeImagen = Server.MapPath("~/Imagenes\\") + "LogoCootregua.jpg";
        return cRutaDeImagen;
    }
   
    public string CalcularDigitoVerificacion(string Nit)
    {
        string Temp;
        int Contador;
        int Residuo;
        int Acumulador;
        int[] Vector = new int[15];

        Vector[0] = 3;
        Vector[1] = 7;
        Vector[2] = 13;
        Vector[3] = 17;
        Vector[4] = 19;
        Vector[5] = 23;
        Vector[6] = 29;
        Vector[7] = 37;
        Vector[8] = 41;
        Vector[9] = 43;
        Vector[10] = 47;
        Vector[11] = 53;
        Vector[12] = 59;
        Vector[13] = 67;
        Vector[14] = 71;

        Acumulador = 0;

        Residuo = 0;

        for (Contador = 0; Contador < Nit.Length; Contador++)
        {
            Temp = Nit.Substring((Nit.Length - 1) - Contador, 1);
            Acumulador = Acumulador + (Convert.ToInt32(Temp) * Vector[Contador]);
        }

        Residuo = Acumulador % 11;

        return Residuo > 1 ? Convert.ToString(11 - Residuo) : Residuo.ToString();
    }

    public DateTime ConvertirStringToDate(String pCadena)
    {
        try
        {
            return DateTime.ParseExact(pCadena, gFormatoFecha, null);
        }
        catch
        {
            return DateTime.MinValue;
        }
    }

    public DateTime? ConvertirStringToDateN(String pCadena)
    {
        try
        {
            return DateTime.ParseExact(pCadena, gFormatoFecha, null);
        }
        catch
        {
            return null;
        }
    }

    public decimal ConvertirStringToDecimal(String pCadena)
    {
        if (pCadena == "")
            return 0;
        try
        {
            return Convert.ToDecimal(pCadena);
        }
        catch
        {
            return 0;
        }
    }

    public void Deshabilitar(ContentPlaceHolder pContenedor)
    {
        if (pContenedor == null)
            return;
        try
        {
            foreach (Control control in pContenedor.Controls)
            {
                DeshabilitarControl(control);
            }
        }
        catch 
        {
            return;
        }
    }

    public void DeshabilitarControl(Control pControl)
    {
        if (pControl == null)
            return;
        try
        {
            if (pControl is TextBox)
                ((TextBox)pControl).Enabled = false;
            else if (pControl is DropDownList)
                ((DropDownList)(pControl)).Enabled = false;
            else if (pControl is Image)
                ((Image)(pControl)).Enabled = false;
            else if (pControl is Button)
                ((Button)(pControl)).Enabled = false;
            else if (pControl is GridView)
                ((GridView)(pControl)).Enabled = false;
            else if (pControl is GridView)
                ((GridView)(pControl)).Enabled = false;
            else if (pControl is RadioButton)
                ((RadioButton)(pControl)).Enabled = false;
            else if (pControl is RadioButtonList)
                ((RadioButtonList)(pControl)).Enabled = false;
            else if (pControl is CheckBox)
                ((CheckBox)(pControl)).Enabled = false;
            // Deshabilitar los hijos
            if (pControl.Controls != null)
            {
                foreach (Control pHijo in pControl.Controls)
                {
                    DeshabilitarControl(pHijo);
                }
            }
        }
        catch 
        {
            return;
        }
    }

    public static string GetUserIP()
    {
        var ip = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null
              && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "")
             ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]
             : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        if (ip.Contains(","))
            ip.Split(',').First().Trim();
        return ip;
    }


}