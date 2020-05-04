using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Seguridad.Entities;
using Xpinn.Comun.Entities;
using System.Text;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Comun.Services;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;

/// <summary>
/// Funciones para uso transversal en capa de presentacion
/// </summary>
public class GlobalWeb : System.Web.UI.Page
{
    public ExcepcionBusiness BOexcepcion;
    private Acceso opcionActual;
    protected enum Pagina { Nuevo = 1, Lista = 2, Detalle = 3, Editar = 4, Modificar = 5 };

    public static string MOV_GRAL_CRED_PRODUC = "idMovGralCredito";
    protected ConnectionDataBase dbConnectionFactory;
    Configuracion global = new Configuracion();
    public static string gFormatoFecha = "dd/MM/yyyy";
    public static string gSeparadorDecimal = ",";
    public static string gSeparadorMiles = ".";
    public static Boolean bMostrarPDF = false;
    public static string gControlarCompCaja = "0";
    public static string gEdadMinima = "";
    public static string gEdadMaxima = "";
    public static string gMarcarRecogerDesembolso = "";
    public static string gValidarTasaReestructuracion = "";
    public static string guserID = "";
    public static string gclave = "";
    public static string gmenuRetractil = "";
    public static readonly string espacioBlancoHTML = "&nbsp;";
    protected Dictionary<ParametroCorreo, string> parametrosFormatoCorreo;
    protected List<ErroresCarga> _lstErroresCarga;
    public static Task Ejecutando;
    protected string TextoLaberError
    {
        get
        {
            return ((Label)Master.FindControl("lblError")).Text;
        }
    }

    Usuario _usuario;
    public Usuario Usuario
    {
        get
        {
            if (_usuario == null)
            {
                _usuario = Session["Usuario"] as Usuario;
            }

            return _usuario;
        }
        private set
        {
            _usuario = value;
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
            gControlarCompCaja = global.ObtenerValorConfig("ControlarCompCaja");
            gEdadMinima = global.ObtenerValorConfig("EdadMinima");
            gEdadMaxima = global.ObtenerValorConfig("EdadMaxima");
            gMarcarRecogerDesembolso = global.ObtenerValorConfig("MarcarRecogerDesembolso");
            gValidarTasaReestructuracion = global.ObtenerValorConfig("ValidarTasaReestructuracion");
            gmenuRetractil = global.ObtenerValorConfig("menuRetractil");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "GlobalWeb", ex);
        }
    }

    public bool LlenarListasDesplegables(string ListaSolicitada, params CheckBoxList[] checkBoxList)
    {
        Persona1Service persona1Servicio = new Persona1Service();

        try
        {
            List<Persona1> lstDatosSolicitud = persona1Servicio.ListasDesplegables(ListaSolicitada, Usuario);

            foreach (var dropDown in checkBoxList)
            {
                dropDown.DataSource = lstDatosSolicitud;
                dropDown.DataTextField = "ListaDescripcion";
                dropDown.DataValueField = "ListaIdStr";
                dropDown.DataBind();
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool LlenarListasDesplegables(string ListaSolicitada, params DropDownList[] dropdownList)
    {
        Persona1Service persona1Servicio = new Persona1Service();

        try
        {
            List<Persona1> lstDatosSolicitud = new List<Persona1>();

            if (ListaSolicitada == TipoLista.TipoPersona.ToString())
            {
                Persona1 entidad = new Persona1();
                entidad.ListaIdStr = "N";
                entidad.ListaDescripcion = "NATURAL";
                lstDatosSolicitud.Add(entidad);
                Persona1 entidad1 = new Persona1();
                entidad1.ListaIdStr = "J";
                entidad1.ListaDescripcion = "JURIDICA";
                lstDatosSolicitud.Add(entidad1);
            }
            else if (ListaSolicitada == TipoLista.TipoCliente.ToString())
            {
                Persona1 entidad = new Persona1();
                entidad.ListaIdStr = "A";
                entidad.ListaDescripcion = "Activos";
                lstDatosSolicitud.Add(entidad);
                Persona1 entidad1 = new Persona1();
                entidad1.ListaIdStr = "I";
                entidad1.ListaDescripcion = "Inactivos";
                lstDatosSolicitud.Add(entidad1);
            }
            else if (ListaSolicitada == TipoLista.Jurisdiccion.ToString())
            {
                Xpinn.Riesgo.Services.JurisdiccionDepaServices serviceRiesgo = new Xpinn.Riesgo.Services.JurisdiccionDepaServices();
                List<Xpinn.Riesgo.Entities.JurisdiccionDepa> lista = new List<Xpinn.Riesgo.Entities.JurisdiccionDepa>();
                lista = serviceRiesgo.ListasDesplegables(Usuario);
                lstDatosSolicitud.Clear();
                foreach (Xpinn.Riesgo.Entities.JurisdiccionDepa item in lista)
                {
                    Persona1 newItem = new Persona1();
                    newItem.ListaIdStr = item.ListaIdStr;
                    newItem.ListaDescripcion = item.ListaDescripcion;
                    lstDatosSolicitud.Add(newItem);
                }
            }
            else if (ListaSolicitada == TipoLista.ValoracionJurisdiccion.ToString())
            {
                Persona1 entidadv1 = new Persona1();
                entidadv1.ListaIdStr = "1";
                entidadv1.ListaDescripcion = "Bajo";
                lstDatosSolicitud.Add(entidadv1);
                Persona1 entidadv2 = new Persona1();
                entidadv2.ListaIdStr = "2";
                entidadv2.ListaDescripcion = "Moderado";
                lstDatosSolicitud.Add(entidadv2);
                Persona1 entidadv3 = new Persona1();
                entidadv3.ListaIdStr = "3";
                entidadv3.ListaDescripcion = "Alto";
                lstDatosSolicitud.Add(entidadv3);
                Persona1 entidadv4 = new Persona1();
                entidadv4.ListaIdStr = "4";
                entidadv4.ListaDescripcion = "Extremo";
                lstDatosSolicitud.Add(entidadv4);
            }
            else
            {
                lstDatosSolicitud = persona1Servicio.ListasDesplegables(ListaSolicitada, Usuario);
            }

            foreach (var dropDown in dropdownList)
            {
                dropDown.DataSource = lstDatosSolicitud;
                dropDown.DataTextField = "ListaDescripcion";
                dropDown.DataValueField = "ListaIdStr";
                dropDown.DataBind();
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool LlenarListasDesplegables(string ListaSolicitada, params ListControl[] listControl)
    {
        Persona1Service persona1Servicio = new Persona1Service();

        try
        {
            List<Persona1> lstDatosSolicitud = persona1Servicio.ListasDesplegables(ListaSolicitada, Usuario);

            foreach (var dropDown in listControl)
            {
                dropDown.DataSource = lstDatosSolicitud;
                dropDown.DataTextField = "ListaDescripcion";
                dropDown.DataValueField = "ListaIdStr";
                dropDown.DataBind();
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }


    public bool LlenarListasDesplegables(TipoLista ListaSolicitada, params CheckBoxList[] checkBoxList)
    {
        if (ListaSolicitada == TipoLista.SinTipoLista) throw new ArgumentException("Tipo de lista solicitada invalida!.");
        return LlenarListasDesplegables(ListaSolicitada.ToString(), checkBoxList);
    }

    public bool LlenarListasDesplegables(TipoLista ListaSolicitada, params DropDownList[] dropdownList)
    {
        if (ListaSolicitada == TipoLista.SinTipoLista) throw new ArgumentException("Tipo de lista solicitada invalida!.");
        return LlenarListasDesplegables(ListaSolicitada.ToString(), dropdownList);
    }

    public bool LlenarListasDesplegables(TipoLista ListaSolicitada, params ListControl[] listControl)
    {
        if (ListaSolicitada == TipoLista.SinTipoLista) throw new ArgumentException("Tipo de lista solicitada invalida!.");
        return LlenarListasDesplegables(ListaSolicitada.ToString(), listControl);
    }


    public bool LlenarListaDesplegablesConEnumSinDefault0(Type enumeration, params DropDownList[] dropDownList)
    {
        if (!enumeration.IsEnum) return false;

        var source = Enum.GetNames(enumeration)
                                    .Where(x => (int)(Enum.Parse(enumeration, x)) != 0)
                                    .Select(o => new { Text = o, Value = (int)(Enum.Parse(enumeration, o)) });

        foreach (var dropDown in dropDownList)
        {
            dropDown.DataTextField = "Text";
            dropDown.DataValueField = "Value";
            dropDown.DataSource = source;
            dropDown.DataBind();
        }

        return true;
    }


    protected void RegistrarPostBack()
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:__doPostBack('', '');", true);
    }


    protected void ImprimirReporteAsPDF(ReportViewer reportViewer, FormatoArchivo formato = FormatoArchivo.PDF)
    {
        //MOSTRAR REPORTE EN PANTALLA
        var bytes = reportViewer.LocalReport.Render(formato.ToString());
        Response.Buffer = true;
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "inline;attachment; filename=Reporte.pdf");
        Response.BinaryWrite(bytes);
        Response.Flush(); // send it to the client to download
        Response.Clear();
    }

    protected byte[] ObtenerBytesReporteAsPDF(ReportViewer reportViewer, FormatoArchivo formato = FormatoArchivo.PDF)
    {
        return reportViewer.LocalReport.Render(formato.ToString());
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

    protected General ConsultarParametroGeneral(long codigo, Usuario usuario = null)
    {
        if (usuario == null) usuario = this.Usuario;

        GeneralService generalService = new GeneralService();
        General general = generalService.ConsultarGeneral(codigo, usuario);
        return general;
    }

    // Por ahora da problemas si el control es un "CommandField"
    protected GridViewRow GetRowOfControlInsideGridViewOneLevel(Control controlWhoFireEvent)
    {
        Control controlOneLevel = (controlWhoFireEvent).NamingContainer;

        // No esta testeado, ignorad esto
        //// Si esta dentro de un Item Template falla, por eso se verifica si lo esta, y si lo esta sube un nivel mas para hallar el GridViewRow
        //if (controlOneLevel is ContentPlaceHolder)
        //{
        //    controlOneLevel = controlOneLevel.NamingContainer;
        //}

        return (GridViewRow)controlOneLevel;
    }

    protected int GetRowIndexOfControlInsideGridViewOneLevel(Control controlWhoFireEvent)
    {
        return GetRowOfControlInsideGridViewOneLevel(controlWhoFireEvent).RowIndex;
    }

    protected string obtFiltro(string pCod_Linea_Credito, string ExcepcionCredito, Int64 pCod_Persona)
    {
        string pFiltro = string.Empty;
        pFiltro += " WHERE COD_DEUDOR = " + pCod_Persona + " and COD_LINEA_CREDITO = '" + pCod_Linea_Credito + "' and ESTADO Not In ('T', 'N', 'B', 'P')";

        if (!string.IsNullOrEmpty(ExcepcionCredito))
        {
            if (ExcepcionCredito.Contains(","))
                ExcepcionCredito = ExcepcionCredito.Substring(0, ((ExcepcionCredito.Length) - 1));
            pFiltro += " AND NUMERO_RADICACION NOT IN (" + ExcepcionCredito + ")";
        }
        return pFiltro;
    }
    protected string obtFiltroAvance(string pCod_Linea_Credito, string ExcepcionCredito, Int64 pCod_Persona)
    {
        string pFiltro = string.Empty;
        pFiltro += " WHERE   SALDO_CAPITAL>0 AND COD_DEUDOR = " + pCod_Persona + " and COD_LINEA_CREDITO = '" + pCod_Linea_Credito + "' and ESTADO Not In ('T', 'N', 'B', 'P')";

        if (!string.IsNullOrEmpty(ExcepcionCredito))
        {
            if (ExcepcionCredito.Contains(","))
                ExcepcionCredito = ExcepcionCredito.Substring(0, ((ExcepcionCredito.Length) - 1));
            pFiltro += " AND NUMERO_RADICACION NOT IN (" + ExcepcionCredito + ")";
        }
        return pFiltro;
    }

    public Boolean ValidarNumCreditosPorLinea(string pCod_Linea_Credito, string ExcepcionCredito, Int64 pCod_Persona, Usuario usuario)
    {
        Xpinn.FabricaCreditos.Services.DatosSolicitudService DatosSolicitudServicio = new Xpinn.FabricaCreditos.Services.DatosSolicitudService();
        string pResult = string.Empty;

        int? maxCreditoXPersona = null;
        int numeroCreditoActivoPersona = 0;

        try
        {
            maxCreditoXPersona = DatosSolicitudServicio.ConsultarCreditosPermitidosXLinea(pCod_Linea_Credito, usuario);
            string pFiltro = obtFiltroAvance(pCod_Linea_Credito, ExcepcionCredito, pCod_Persona);
            numeroCreditoActivoPersona = DatosSolicitudServicio.ConsultarCreditosActivosXLinea(pFiltro, usuario);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
            return false;
        }

        if (maxCreditoXPersona != null)
        {
            if (maxCreditoXPersona == 0 || maxCreditoXPersona - numeroCreditoActivoPersona <= 0)
            {
                VerError("Este asociado ha llegado al número máximo de créditos permitidos para esta línea");
                return false;
            }
        }
        return true;
    }

    public void RegistrarErrorImportar(int pNumeroLinea, string pRegistro, string pError, string pDato)
    {
        if (_lstErroresCarga == null)
        {
            _lstErroresCarga = new List<ErroresCarga>();
        }

        ErroresCarga registro = new ErroresCarga();
        string placeholder = " Campo No.: ";

        registro.numero_registro = pNumeroLinea.ToString();
        registro.datos = pDato;

        if (string.IsNullOrWhiteSpace(pRegistro))
        {
            placeholder = string.Empty;
        }

        registro.error = placeholder + pRegistro + " Error:" + pError;

        _lstErroresCarga.Add(registro);
    }


    /// <summary>
    /// Debes tener una carpeta llamada "Archivos" en el mismo directorio para renderizar el pdf ahi y asi poder verlo en el literal
    /// </summary>
    protected void MostrarArchivoEnLiteral(byte[] bytes, Usuario pUsuario, Literal literal, string nombreArchivo = "documentoSalida")
    {
        string pNomUsuario = !string.IsNullOrWhiteSpace(pUsuario.codusuario.ToString()) ? nombreArchivo + " " + pUsuario.codusuario.ToString() : nombreArchivo;

        // ELIMINANDO ARCHIVOS GENERADOS SI LOS ENCUENTRA
        try
        {
            string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Archivos\\"));
            foreach (string ficheroActual in ficherosCarpeta)
                if (ficheroActual.Contains(pNomUsuario))
                    File.Delete(ficheroActual);
        }
        catch
        { }

        FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("Archivos/output" + pNomUsuario + ".pdf"),
        FileMode.Create);
        fs.Write(bytes, 0, bytes.Length);
        fs.Close();
        //MOSTRANDO REPORTE
        string adjuntar = "<object data=\"{0}\" type=\"application/pdf\" width=\"90%\" height=\"700px\">";
        adjuntar += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
        adjuntar += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
        adjuntar += "</object>";

        literal.Text = string.Format(adjuntar, ResolveUrl("Archivos/output" + pNomUsuario + ".pdf"));
    }


    Xpinn.Seguridad.Services.UsuarioService service = new Xpinn.Seguridad.Services.UsuarioService();
    override protected void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (Session["Usuario"] != null)
        {
            Usuario = (Usuario)Session["Usuario"];
        }
        else
        {
            Response.Redirect("~/General/Global/FinSesion.htm");
        }

        if (Session["user"] != null && Page.GetType().Name != "default_aspx")
        {
            Response.Redirect("~/General/Global/FinSesion.htm");

            if (Session["COD_INGRESO"] != null)
            {
                Ingresos pIngresos = new Ingresos();
                pIngresos.cod_ingreso = Convert.ToInt32(Session["COD_INGRESO"].ToString());
                pIngresos.fecha_horasalida = DateTime.Now;
                service.ModificarUsuarioIngreso(pIngresos, (Usuario)Session["usuario"]);
            }
        }
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

    public void VisualizarOpciones(string pPrograma, string pTipoPagina)
    {
        try
        {
            if (Session["usuario"] != null)
            {
                opcionActual = ObtenerOpcionActual(pPrograma.Trim());
                ((Usuario)Session["usuario"]).codOpcionActual = opcionActual.cod_opcion;

                if (opcionActual.generalog == 1)
                    ((Usuario)Session["usuario"]).programaGeneraLog = true;
                else
                    ((Usuario)Session["usuario"]).programaGeneraLog = false;

                if (opcionActual.nombreopcion != null)
                {
                    ((Label)Master.FindControl("lblOpcion")).Text = opcionActual.nombreopcion;

                    switch (pTipoPagina)
                    {
                        case "A": // Agregar
                            ((Label)Master.FindControl("lblOpcion")).Text = opcionActual.nombreopcion + " - Nuevo";
                            ((LinkButton)Master.FindControl("btnGuardar")).Attributes.Add("onClick", "LoadingList()");
                            break;
                        case "D": // Detalle
                            ((Label)Master.FindControl("lblOpcion")).Text = opcionActual.nombreopcion + " - Detalle";
                            break;
                        case "E": // Editar
                            ((Label)Master.FindControl("lblOpcion")).Text = opcionActual.nombreopcion + " - Edicion";
                            break;
                        case "L": // Lista
                            ((Label)Master.FindControl("lblOpcion")).Text = opcionActual.nombreopcion + " - Consulta";
                            ((LinkButton)Master.FindControl("btnConsultar")).Attributes.Add("onClick", "LoadingList()");
                            break;
                        case "G":
                            ((LinkButton)Master.FindControl("btnGuardar")).Attributes.Add("onClick", "LoadingList()");
                            break;
                        default:
                            break;
                    }

                    // PERMISO EN OPCION A BOTONES TRANSACCIONALES
                    if (opcionActual.insertar != 1) Master.FindControl("btnNuevo").Visible = false;
                    if (opcionActual.modificar != 1) Master.FindControl("btnEditar").Visible = false;
                    if (opcionActual.borrar != 1) Master.FindControl("btnEliminar").Visible = false;
                    if (opcionActual.consultar != 1) Master.FindControl("btnConsultar").Visible = false;
                }
                else
                {
                    Response.Redirect("~/General/Global/noAcceso.htm", false);
                }
            }
            else
            {
                if (Session["COD_INGRESO"] != null)
                {
                    Ingresos pIngresos = new Ingresos();
                    pIngresos.cod_ingreso = Convert.ToInt32(Session["COD_INGRESO"].ToString());
                    pIngresos.fecha_horasalida = DateTime.Now;
                    service.ModificarUsuarioIngreso(pIngresos, (Usuario)Session["usuario"]);
                }
                Response.Redirect("~/General/Global/FinSesion.htm", false);
            }
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

    public Boolean ValidarAccionesGrilla(String pAccion)
    {
        if (pAccion.ToUpper() == "UPDATE")
        {
            if (opcionActual.modificar != 1)    // Editar
            {
                VerError("Usted no tiene permiso para editar este registro, comuníquese con el Administrador del sistema si quiere acceder a esta opción.");
                return false;
            }
        }
        if (pAccion.ToUpper() == "DELETE")
        {
            if (opcionActual.borrar != 1)       // Borrar   
            {
                VerError("Usted no tiene permiso para eliminar este registro, comuníquese con el Administrador del sistema si quiere acceder a esta opción.");
                return false;
            }
        }
        if (pAccion.ToUpper() == "CONSULTAR")
        {
            if (opcionActual.consultar != 1)    // Detalle
            {
                VerError("Usted no tiene permiso para consultar este registro, comuníquese con el Administrador del sistema si quiere acceder a esta opción.");
                return false;
            }
        }
        return true;
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

    public void LimpiarFormulario()
    {
        try
        {
            ContentPlaceHolder contenedor = (ContentPlaceHolder)Master.FindControl("cphMain");
            foreach (Control control in contenedor.Controls)
            {
                if (control is TextBox)
                    ((TextBox)(control)).Text = "";
                else if (control is DropDownList)
                    ((DropDownList)(control)).SelectedIndex = 0;
                else if (control is RadioButton)
                    ((RadioButton)(control)).Checked = false;
                else if (control is CheckBox)
                    ((CheckBox)(control)).Checked = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("ACTRGenGeneral", "LimpiarFormulario", ex);
        }
    }

    public void LimpiarPanel(Panel plControles)
    {
        try
        {
            foreach (Control control in plControles.Controls)
            {
                if (control is TextBox)
                    ((TextBox)(control)).Text = "";
                else if (control is DropDownList)
                    ((DropDownList)(control)).SelectedIndex = 0;
                else if (control is RadioButton)
                    ((RadioButton)(control)).Checked = false;
                else if (control is CheckBox)
                    ((CheckBox)(control)).Checked = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "LimpiarPanel", ex);
        }
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

    public void ConfirmarEventoBoton(ImageButton pBoton, String pMensaje)
    {
        try
        {
            pBoton.Attributes.Add("onClick", "return confirm('" + pMensaje + "');");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "ConfirmarEventoBoton", ex);
        }
    }

    public void ConfirmarEventoBoton(LinkButton pBoton, String pMensaje)
    {
        try
        {
            pBoton.Attributes.Add("onClick", "return confirm('" + pMensaje + "');");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "ConfirmarEventoBoton", ex);
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
                if (pError.Contains("ORA-20101"))
                    if (pError.Length > 9)
                        pError = pError.Substring(9, pError.Length - 9);
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

    public void VerAuditoria(string pUsuarioCrea, DateTime pFechaCrea, string pUsuarioEdita, DateTime pFechaEdita)
    {
        try
        {
            string infoAuditoria = "";

            infoAuditoria += "Creado '" + pUsuarioCrea.Trim() + "' - '" + pFechaCrea + "'";

            if (!string.IsNullOrEmpty(pUsuarioEdita))
                infoAuditoria += ", modificado '" + pUsuarioEdita.Trim() + "' - '" + pFechaEdita + "'";

            ((Label)Master.FindControl("lblAuditoria")).Text = infoAuditoria;
            ((Label)Master.FindControl("lblAuditoria")).Visible = true;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "VerAuditoria", ex);
        }
    }

    public string idObjeto
    {
        get
        {
            if (Master != null) return ((HiddenField)Master.FindControl("hfObj")).Value.Trim();
            return "";
        }

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

    private Acceso ObtenerOpcionActual(string pIdOpcion)
    {
        try
        {
            Acceso accesos = new Acceso();

            if (Session["accesos"] != null)
            {
                List<Acceso> lstAccesos = new List<Acceso>();
                lstAccesos = (List<Acceso>)Session["accesos"];

                foreach (Acceso ent in lstAccesos)
                    if (ent.cod_opcion == Convert.ToInt64(pIdOpcion))
                        accesos = ent;
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

    public bool ValidarProcesoContable(DateTime pFecha, Int64 pTipoOpe)
    {
        // Validar que exista la parametrización contable por procesos
        Xpinn.Contabilidad.Services.ComprobanteService compServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
        List<Xpinn.Contabilidad.Entities.ProcesoContable> lstProceso = new List<Xpinn.Contabilidad.Entities.ProcesoContable>();
        lstProceso = compServicio.ConsultaProceso(0, pTipoOpe, pFecha, (Usuario)Session["Usuario"]);
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

    public Xpinn.Contabilidad.Entities.ProcesoContable ConsultarProcesoContable(int pTipoOpe, ref string pError, Usuario pUsuario)
    {
        pError = "";
        // Determinar el proceso del desembolso
        Xpinn.Contabilidad.Services.ProcesoContableService procesoContable = new Xpinn.Contabilidad.Services.ProcesoContableService();
        Xpinn.Contabilidad.Entities.ProcesoContable eproceso = new Xpinn.Contabilidad.Entities.ProcesoContable();
        eproceso = procesoContable.ConsultarProcesoContableOperacion(pTipoOpe, pUsuario);
        if (eproceso == null)
        {
            pError = "No hay ningún proceso contable parametrizado para el desembolso de créditos";
            return null;
        }
        if (eproceso.cod_proceso == null)
        {
            pError = "No hay ningún proceso contable parametrizado para el desembolso de créditos";
            return null;
        }
        return eproceso;
    }

    public string ImagenReporte()
    {
        string cRutaDeImagen;
        cRutaDeImagen = Server.MapPath("~/Images\\") + "LogoEmpresa.jpg";
        return cRutaDeImagen;
    }

    public List<ListasFijas> ListaEstadosGiro()
    {
        List<ListasFijas> lstEstados = new List<ListasFijas>();
        lstEstados.Add(new ListasFijas { codigo = "0", descripcion = "Pendiente" });
        lstEstados.Add(new ListasFijas { codigo = "1", descripcion = "Aprobado" });
        lstEstados.Add(new ListasFijas { codigo = "2", descripcion = "Desembolsado" });
        lstEstados.Add(new ListasFijas { codigo = "3", descripcion = "Anulado" });
        lstEstados.Add(new ListasFijas { codigo = "4", descripcion = "Fusionado" });
        lstEstados.Add(new ListasFijas { codigo = "5", descripcion = "Distribuido" });
        return lstEstados;
    }

    public string NomEstadosGiro(string pCodEstado)
    {
        string sNombre = "";
        List<ListasFijas> lstEstados = new List<ListasFijas>();
        lstEstados = ListaEstadosGiro();
        for (int i = 0; i < lstEstados.Count(); i++)
            if (lstEstados[i].codigo == pCodEstado)
                return lstEstados[i].descripcion;
        return sNombre;
    }



    public List<ListasFijas> ListaCreditoTipoDeDescuento()
    {
        List<ListasFijas> lstTipos = new List<ListasFijas>();
        lstTipos.Add(new ListasFijas { codigo = "0", descripcion = "" });
        lstTipos.Add(new ListasFijas { codigo = "1", descripcion = "Constante" });
        lstTipos.Add(new ListasFijas { codigo = "2", descripcion = "Factor" });
        lstTipos.Add(new ListasFijas { codigo = "3", descripcion = "Porcentaje" });
        lstTipos.Add(new ListasFijas { codigo = "4", descripcion = "Rango" });
        return lstTipos;
    }

    public List<ListasFijas> ListaCreditoTipoDeLiquidacion()
    {
        List<ListasFijas> lstTipos = new List<ListasFijas>();
        lstTipos.Add(new ListasFijas { codigo = "", descripcion = "" });
        lstTipos.Add(new ListasFijas { codigo = "0", descripcion = "" });
        lstTipos.Add(new ListasFijas { codigo = "1", descripcion = "Factor (Plazo+1) * Monto" });
        lstTipos.Add(new ListasFijas { codigo = "2", descripcion = "Factor * Monto" });
        lstTipos.Add(new ListasFijas { codigo = "3", descripcion = "Factor * Plazo * Monto" });
        lstTipos.Add(new ListasFijas { codigo = "4", descripcion = "Factor (Factor+1) ** Cuotas" });
        lstTipos.Add(new ListasFijas { codigo = "5", descripcion = "Factor * (Monto - Valor)" });
        lstTipos.Add(new ListasFijas { codigo = "6", descripcion = "Factor * Saldo" });
        lstTipos.Add(new ListasFijas { codigo = "7", descripcion = "Factor * (Saldo*Interes)" });
        lstTipos.Add(new ListasFijas { codigo = "8", descripcion = "Factor * Valor a Pagar" });
        lstTipos.Add(new ListasFijas { codigo = "9", descripcion = "FactorVeh * VrComercial" });
        lstTipos.Add(new ListasFijas { codigo = "10", descripcion = "Timbres -> Factor * Vlr_Leasing" });
        lstTipos.Add(new ListasFijas { codigo = "11", descripcion = "Factor * Vlr_Bien" });
        lstTipos.Add(new ListasFijas { codigo = "12", descripcion = "Factor * ((Vlr_Bien/Dias_Tot)*Dias_Mes)" });
        lstTipos.Add(new ListasFijas { codigo = "13", descripcion = "Factor * Plazo * Saldo" });
        lstTipos.Add(new ListasFijas { codigo = "14", descripcion = "Factor * Cuota" });
        lstTipos.Add(new ListasFijas { codigo = "15", descripcion = "(Canon-(Vlr_Bien*Dias_Mes/Factor))*IVA" });
        lstTipos.Add(new ListasFijas { codigo = "16", descripcion = "Tasa * Saldo Credito Diferido" });
        lstTipos.Add(new ListasFijas { codigo = "17", descripcion = "Factor * ( 1 + Codeudor ) * Saldo" });
        lstTipos.Add(new ListasFijas { codigo = "18", descripcion = "Factor * ( 1 + Codeudor ) * Monto" });
        lstTipos.Add(new ListasFijas { codigo = "19", descripcion = "Factor * Plazo" });
        lstTipos.Add(new ListasFijas { codigo = "20", descripcion = "Factor por Millón * Monto * Plazo" });
        lstTipos.Add(new ListasFijas { codigo = "21", descripcion = "Factor Anual * Monto * Plazo" });
        lstTipos.Add(new ListasFijas { codigo = "22", descripcion = "Factor/Plazo * Monto" });
        lstTipos.Add(new ListasFijas { codigo = "23", descripcion = "Comisión por Rango" });
        lstTipos.Add(new ListasFijas { codigo = "24", descripcion = "Factor * (Monto - Valores Recogidos)" });
        lstTipos.Add(new ListasFijas { codigo = "25", descripcion = "Factor * Cuota (Dias Mora)" });
        return lstTipos;
    }

    public List<ListasFijas> ListaCreditoFormadeDescuento()
    {
        List<ListasFijas> lstTipos = new List<ListasFijas>();
        lstTipos.Add(new ListasFijas { codigo = "", descripcion = "" });
        lstTipos.Add(new ListasFijas { codigo = "0", descripcion = "" });
        lstTipos.Add(new ListasFijas { codigo = "1", descripcion = "Descuento del Desembolso" });
        lstTipos.Add(new ListasFijas { codigo = "2", descripcion = "Sumado al monto" });
        lstTipos.Add(new ListasFijas { codigo = "3", descripcion = "Sumado a la Cuota" });
        lstTipos.Add(new ListasFijas { codigo = "4", descripcion = "Financiado Excluido" });
        lstTipos.Add(new ListasFijas { codigo = "5", descripcion = "Adicional a la cuota" });
        lstTipos.Add(new ListasFijas { codigo = "6", descripcion = "Pago en la Primera Cuota" });
        return lstTipos;
    }


    public void PoblarListaTipoCtaXPagar(DropDownList ddlControl)
    {
        ddlControl.Items.Clear();
        ddlControl.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlControl.Items.Insert(1, new ListItem("Facturas", "1"));
        ddlControl.Items.Insert(2, new ListItem("Orden de Pago", "2"));
        ddlControl.Items.Insert(3, new ListItem("Orden de Compra", "3"));
        ddlControl.Items.Insert(4, new ListItem("Orden de Servicio", "4"));
        ddlControl.Items.Insert(5, new ListItem("Contrato de Servicio", "5"));
        ddlControl.DataBind();
    }
    public void PoblarListaFromatosDIAN(DropDownList ddlControl)
    {
        ddlControl.Items.Clear();
        ddlControl.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlControl.Items.Insert(1, new ListItem("FORMATO 1019-Movimiento cuenta Ahorro", "1"));
        ddlControl.Items.Insert(2, new ListItem("FORMATO 1020-Inversiones en CDATS", "2"));
        ddlControl.Items.Insert(3, new ListItem("FORMATO 1010-Información socios accionistas, cooperados", "3"));
        ddlControl.Items.Insert(4, new ListItem("FORMATO 1008-Saldo de cuentas por Cobrar", "4"));
        ddlControl.Items.Insert(5, new ListItem("FORMATO 1001-Pago o Abonos en cuenta y Retenciones Practicadas", "5"));
        ddlControl.Items.Insert(6, new ListItem("FORMATO 1007-Ingresos Recibidos", "6"));
        ddlControl.Items.Insert(7, new ListItem("FORMATO 1009-Saldo de cuentas por pagar", "7"));
        ddlControl.Items.Insert(8, new ListItem("FORMATO 1026-Cartera neta colocada", "8"));
        ddlControl.DataBind();
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
            return Convert.ToDecimal(pCadena.Replace("$", "").Replace(gSeparadorMiles, ""));
        }
        catch
        {
            return 0;
        }
    }

    public decimal? ConvertirStringToDecimalN(String pCadena)
    {
        if (pCadena == "")
            return null;
        try
        {
            return Convert.ToDecimal(pCadena.Replace("$", "").Replace(gSeparadorMiles, ""));
        }
        catch
        {
            return 0;
        }
    }

    public Int64 ConvertirStringToInt(String pCadena)
    {
        if (pCadena == "")
            return 0;
        try
        {
            return Convert.ToInt64(pCadena.Replace("$", "").Replace(gSeparadorMiles, ""));
        }
        catch
        {
            return 0;
        }
    }

    public Int64? ConvertirStringToIntN(String pCadena)
    {
        if (pCadena == "")
            return null;
        try
        {
            return Convert.ToInt64(pCadena.Replace("$", "").Replace(gSeparadorMiles, ""));
        }
        catch
        {
            return 0;
        }
    }

    public Int32 ConvertirStringToInt32(String pCadena)
    {
        if (pCadena == "")
            return 0;
        try
        {
            return Convert.ToInt32(pCadena.Replace("$", "").Replace(gSeparadorMiles, ""));
        }
        catch
        {
            return 0;
        }
    }

    public Int32? ConvertirStringToInt32N(String pCadena)
    {
        if (pCadena == "")
            return null;
        try
        {
            return Convert.ToInt32(pCadena.Replace("$", "").Replace(gSeparadorMiles, ""));
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

    public GridView CopiarGridViewParaExportar(GridView gvLista, string pDatos)
    {
        GridView gvExportar = new GridView();
        gvExportar = gvLista;
        gvExportar.AllowPaging = false;
        foreach (DataControlField eColumna in gvExportar.Columns)
        {
            try
            {
                if (eColumna.GetType().ToString() == "System.Web.UI.WebControls.BoundField")
                {
                    BoundField eCol = (BoundField)eColumna;
                    if (eCol.DataFormatString != "{0:d}" && eCol.DataFormatString != "{0:c}")
                        eCol.DataFormatString = "";
                }
            }
            catch { }
        }

        gvExportar.DataSource = Session[pDatos];
        gvExportar.DataBind();
        gvExportar.EnableViewState = false;
        return gvExportar;
    }

    public List<ListasFijas> ListaEstadosLibreta()
    {
        List<ListasFijas> lstTipos = new List<ListasFijas>();
        lstTipos.Add(new ListasFijas { codigo = "", descripcion = "" });
        lstTipos.Add(new ListasFijas { codigo = "0", descripcion = "Activa" });
        lstTipos.Add(new ListasFijas { codigo = "1", descripcion = "Inactiva" });
        return lstTipos;
    }

    public List<ListasFijas> ListaTipoCuenta(int pTipo)
    {
        List<ListasFijas> lstTipos = new List<ListasFijas>();
        if (pTipo == 2)
        {
            lstTipos.Add(new ListasFijas { codigo = "", descripcion = "" });
            lstTipos.Add(new ListasFijas { codigo = "0", descripcion = "Disminución Anterior" });
            lstTipos.Add(new ListasFijas { codigo = "1", descripcion = "Disminución Actual" });
            lstTipos.Add(new ListasFijas { codigo = "2", descripcion = "Aumento Provisión" });
        }
        else
        {
            lstTipos.Add(new ListasFijas { codigo = "", descripcion = "" });
            lstTipos.Add(new ListasFijas { codigo = "1", descripcion = "Normal" });
            lstTipos.Add(new ListasFijas { codigo = "2", descripcion = "Causado" });
            lstTipos.Add(new ListasFijas { codigo = "3", descripcion = "Orden" });
        }
        return lstTipos;
    }


    public StringBuilder ExportarGridCSV(GridView gvDataSinPaginar, char pSeparadorCampo = ';')
    {
        StringBuilder sb = new StringBuilder();
        if (gvDataSinPaginar.Rows.Count > 0)
        {
            string pItem;
            for (int k = 0; k < gvDataSinPaginar.Columns.Count; k++)
            {
                string s = gvDataSinPaginar.Columns[k].HeaderStyle.CssClass;
                if (s != "gridIco")
                {
                    pItem = HttpUtility.HtmlDecode(gvDataSinPaginar.Columns[k].HeaderText);
                    sb.Append(pItem);
                    sb.Append(pSeparadorCampo);
                }
            }
            sb = sb.Remove(sb.Length - 1, 1);
            sb.Append("\r\n");

            for (int i = 0; i < gvDataSinPaginar.Rows.Count; i++)
            {
                for (int k = 0; k < gvDataSinPaginar.Columns.Count; k++)
                {
                    string s = gvDataSinPaginar.Columns[k].HeaderStyle.CssClass;
                    if (s != "gridIco")
                    {
                        if (gvDataSinPaginar.Rows[i].Cells[k].Text != "&nbsp;" || gvDataSinPaginar.Rows[i].Cells[k].Text != "")
                        {
                            pItem = HttpUtility.HtmlDecode(gvDataSinPaginar.Rows[i].Cells[k].Text);
                            sb.Append(pItem);
                        }
                        else
                            sb.Append(" ");
                        sb.Append(pSeparadorCampo);
                    }
                }
                sb = sb.Remove(sb.Length - 1, 1);
                sb.Append("\r\n");
            }
        }
        return sb;
    }

    public void ExportarGridCSVDirecto(GridView gvDataSinPaginar, string nombreArchivo = "Archivo", char pSeparadorCampo = ';')
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + nombreArchivo + ".csv");
        Response.Charset = "UTF-8";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.Default;

        StringBuilder sb = ExportarGridCSV(gvDataSinPaginar, pSeparadorCampo);
        Response.Output.Write(sb.ToString());
        Response.Flush();

        Response.End();
    }

    public void ExportarGridViewEnExcel(GridView gvDataSinPaginar, string nombreArchivo = "Archivo")
    {
        if (gvDataSinPaginar.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            using (Page pagina = new Page())
            using (HtmlForm form = new HtmlForm())
            {
                gvDataSinPaginar.EnableViewState = false;
                pagina.EnableEventValidation = false;

                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvDataSinPaginar);

                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + nombreArchivo + ".xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
            }
        }
    }

    public string ConvenioTarjeta()
    {
        return ConvenioTarjeta(0);
    }
        
    public string ConvenioTarjeta(int ptipo_convenio = 0)
    {
        string convenio = "";
        // Cargar el dato del convenio
        Xpinn.TarjetaDebito.Services.TarjetaConvenioService convenioServicio = new Xpinn.TarjetaDebito.Services.TarjetaConvenioService();
        Xpinn.TarjetaDebito.Entities.TarjetaConvenio tarjetaConvenio = new Xpinn.TarjetaDebito.Entities.TarjetaConvenio();
        tarjetaConvenio.tipo_convenio = ptipo_convenio;
        List<Xpinn.TarjetaDebito.Entities.TarjetaConvenio> lsttarjetaConvenio = new List<Xpinn.TarjetaDebito.Entities.TarjetaConvenio>();
        lsttarjetaConvenio = convenioServicio.ListarTarjetaConvenio(tarjetaConvenio, (Usuario)Session["Usuario"]);
        if (lsttarjetaConvenio != null)
            if (lsttarjetaConvenio.Count > 0)
                if (lsttarjetaConvenio[0] != null)
                    convenio = lsttarjetaConvenio[0].codigo_bin;
        return convenio;
    }

    public string IpApplianceConvenioTarjeta()
    {
        string ip_appliance = "";
        // Cargar el dato del convenio
        Xpinn.TarjetaDebito.Services.TarjetaConvenioService convenioServicio = new Xpinn.TarjetaDebito.Services.TarjetaConvenioService();
        Xpinn.TarjetaDebito.Entities.TarjetaConvenio tarjetaConvenio = new Xpinn.TarjetaDebito.Entities.TarjetaConvenio();
        List<Xpinn.TarjetaDebito.Entities.TarjetaConvenio> lsttarjetaConvenio = new List<Xpinn.TarjetaDebito.Entities.TarjetaConvenio>();
        lsttarjetaConvenio = convenioServicio.ListarTarjetaConvenio(tarjetaConvenio, (Usuario)Session["Usuario"]);
        if (lsttarjetaConvenio != null)
            if (lsttarjetaConvenio.Count > 0)
                if (lsttarjetaConvenio[0] != null)
                    ip_appliance = lsttarjetaConvenio[0].ip_appliance;
        return ip_appliance;
    }

    public string IpSwitchConvenioTarjeta()
    {
        string ip_appliance = "";
        // Cargar el dato del convenio
        Xpinn.TarjetaDebito.Services.TarjetaConvenioService convenioServicio = new Xpinn.TarjetaDebito.Services.TarjetaConvenioService();
        Xpinn.TarjetaDebito.Entities.TarjetaConvenio tarjetaConvenio = new Xpinn.TarjetaDebito.Entities.TarjetaConvenio();
        List<Xpinn.TarjetaDebito.Entities.TarjetaConvenio> lsttarjetaConvenio = new List<Xpinn.TarjetaDebito.Entities.TarjetaConvenio>();
        lsttarjetaConvenio = convenioServicio.ListarTarjetaConvenio(tarjetaConvenio, (Usuario)Session["Usuario"]);
        if (lsttarjetaConvenio != null)
            if (lsttarjetaConvenio.Count > 0)
                if (lsttarjetaConvenio[0] != null)
                    ip_appliance = lsttarjetaConvenio[0].ip_switch;
        return ip_appliance;
    }



    public bool SeguridadConvenioTarjeta(ref string pUsuario, ref string pClave)
    {
        // Cargar el dato del convenio
        Xpinn.TarjetaDebito.Services.TarjetaConvenioService convenioServicio = new Xpinn.TarjetaDebito.Services.TarjetaConvenioService();
        Xpinn.TarjetaDebito.Entities.TarjetaConvenio tarjetaConvenio = new Xpinn.TarjetaDebito.Entities.TarjetaConvenio();
        List<Xpinn.TarjetaDebito.Entities.TarjetaConvenio> lsttarjetaConvenio = new List<Xpinn.TarjetaDebito.Entities.TarjetaConvenio>();
        lsttarjetaConvenio = convenioServicio.ListarTarjetaConvenio(tarjetaConvenio, (Usuario)Session["Usuario"]);
        if (lsttarjetaConvenio != null)
            if (lsttarjetaConvenio.Count > 0)
                if (lsttarjetaConvenio[0] != null)
                {
                    pUsuario = lsttarjetaConvenio[0].usuario_appliance;
                    pClave = lsttarjetaConvenio[0].clave_appliance;
                    return true;
                }
        return false;
    }

    public readonly string _tipoOperacionRetiroEnpacto = "1";
    public readonly string _tipoOperacionDepositoEnpacto = "3";
    public bool AplicarTransaccionEnpacto(string pTipoIdentificacion, string pIdentificacion, string pNombreCliente, string pCodTipoProducto, string pNumeroProducto, string pTipoMov, decimal pValor, Int64 pCodOpe, string pObservaciones, ref string pError)
    {
        pError = "";
        string _convenio = "";
        try
        {
            // Determinar código del convenio
            _convenio = ConvenioTarjeta(0);

            // Busco si esta habilitado las operaciones con Enpacto
            General general = ConsultarParametroGeneral(36);

            if (general != null && general.valor == "1")
            {
                // Busco la homologacion de la cedula para los tipos de cedula de enpacto               
                HomologacionServices homologaService = new HomologacionServices();
                Homologacion homologacion = homologaService.ConsultarHomologacionTipoIdentificacion(pTipoIdentificacion, Usuario);

                // Si no tengo los datos para homologar la cedula no hago nada y me voy
                if (!(homologacion != null && !string.IsNullOrWhiteSpace(homologacion.tipo_identificacion_enpacto)))
                {
                    pError = "No existe homologación para el tipo de identificación " + pTipoIdentificacion;
                    return false;
                }

                // Realizar la instanciación de la interfaz y de los servicios
                InterfazENPACTO interfazEnpacto = new InterfazENPACTO("0123456789ABCDEFFEDCBA9876543210", "00000000000000000000000000000000");
                Xpinn.TarjetaDebito.Services.TarjetaService tarjetaService = new Xpinn.TarjetaDebito.Services.TarjetaService();
                Xpinn.Interfaces.Services.EnpactoServices enpactoService = new Xpinn.Interfaces.Services.EnpactoServices();

                // Reviso todas las transacciones para aplicar
                string codigoTipoProducto = pCodTipoProducto;
                TipoDeProducto tipoDeProducto = codigoTipoProducto.ToEnum<TipoDeProducto>();

                // Si no soy ahorro vista no hago nada, siguiente vuelta. Pendiente crédito rotativo.
                if (!(tipoDeProducto == TipoDeProducto.AhorrosVista || tipoDeProducto == TipoDeProducto.Credito))
                {
                    return true;
                }

                // Determinar si el producto tiene una tarjeta débito asignada
                string nroprod = Convert.ToString(pNumeroProducto);
                Xpinn.TarjetaDebito.Entities.Tarjeta tarjetaDeLaPersona = tarjetaService.ConsultarTarjetaDeUnaCuenta(nroprod, Usuario);

                // Si el producto no tiene tarjeta entonces no hacer nada
                if (tarjetaDeLaPersona == null || string.IsNullOrWhiteSpace(tarjetaDeLaPersona.numtarjeta))
                {
                    return true;
                }

                // Generar la transacción en ENPACTO
                Xpinn.TarjetaDebito.Entities.TransaccionEnpacto transaccionEnpacto = new Xpinn.TarjetaDebito.Entities.TransaccionEnpacto();
                long tipomov = long.Parse(pTipoMov);
                string nomtipomov = (tipomov == 2 ? "INGRESO" : "EGRESO");
                if (tipomov == 2) // Tipo movimiento = 2 (Deposito) - 1 = (Retiro)
                {
                    transaccionEnpacto.tipo = _tipoOperacionDepositoEnpacto;
                }
                else
                {
                    transaccionEnpacto.tipo = _tipoOperacionRetiroEnpacto;
                }
                transaccionEnpacto.fecha = DateTime.Now.ToString("yyMMdd");
                transaccionEnpacto.hora = DateTime.Now.Hour.ToString("D2") + DateTime.Now.Minute.ToString("D2") + "00";
                transaccionEnpacto.reverso = "false";
                transaccionEnpacto.secuencia = pCodOpe.ToString();
                transaccionEnpacto.nombre = pNombreCliente;
                transaccionEnpacto.identificacion = pIdentificacion;
                transaccionEnpacto.tipo_identificacion = homologacion.tipo_identificacion_enpacto;
                transaccionEnpacto.tarjeta = tarjetaDeLaPersona.numtarjeta;
                transaccionEnpacto.cuenta = _convenio + tarjetaDeLaPersona.numero_cuenta;
                transaccionEnpacto.tipo_cuenta = tarjetaDeLaPersona.tipo_cuenta;
                transaccionEnpacto.monto = (pValor * 100).ToString();  // Sin carácter decimal, los últimos 2 dígitos son los centavos

                Xpinn.TarjetaDebito.Entities.RespuestaEnpacto respuesta = new Xpinn.TarjetaDebito.Entities.RespuestaEnpacto();
                string error = string.Empty;

                try
                {
                    // Determinar parametros del APPLIANCE para ejecutar la transacción
                    string s_usuario_applicance = "webservice";
                    string s_clave_appliance = "WW.EE.99";
                    SeguridadConvenioTarjeta(ref s_usuario_applicance, ref s_clave_appliance);
                    interfazEnpacto.ConfiguracionAppliance(IpSwitchConvenioTarjeta(), s_usuario_applicance, s_clave_appliance);

                    // Consumir el WEBSERVICES para aplicar la transacción en ENPACTO
                    interfazEnpacto.GenerarTransaccionENPACTO(_convenio, transaccionEnpacto, false, ref respuesta, ref error);

                    // Si la transacción fue aplicada sin errores entonces grabar los datos en TRAN_TARJETA.
                    if (string.IsNullOrWhiteSpace(error) && respuesta != null && respuesta.tran != null)
                    {
                        string fechaTransaccionFormato = DateTime.Now.ToString("yyyy") + "-" + DateTime.Now.Month.ToString("D2") + "-" + DateTime.Now.Day.ToString("D2");

                        Xpinn.TarjetaDebito.Entities.Movimiento movimiento = new Xpinn.TarjetaDebito.Entities.Movimiento
                        {
                            fecha = fechaTransaccionFormato,
                            hora = transaccionEnpacto.hora,
                            documento = transaccionEnpacto.identificacion,
                            nrocuenta = transaccionEnpacto.cuenta,
                            tarjeta = transaccionEnpacto.tarjeta,
                            tipotransaccion = transaccionEnpacto.tipo,
                            descripcion = pObservaciones,
                            monto = Convert.ToDecimal(transaccionEnpacto.monto) / 100,
                            lugar = Usuario.direccion,
                            operacion = respuesta.tran.secuencia,
                            comision = 0,
                            red = "9",
                            cod_ope = pCodOpe,
                            saldo_total = !string.IsNullOrWhiteSpace(respuesta.tran.saldo_total) ? Convert.ToDecimal(respuesta.tran.saldo_total) / 100 : default(decimal?),
                            cod_cliente = tarjetaDeLaPersona.cod_persona
                        };

                        Xpinn.TarjetaDebito.Services.CuentaService cuentaService = new Xpinn.TarjetaDebito.Services.CuentaService();
                        cuentaService.CrearMovimiento(movimiento, pCodOpe, Usuario);
                    }

                    respuesta.Error = error;

                    // Dejar registro de auditoria del consumo del WEBSERVICES.
                    Xpinn.Interfaces.Entities.Enpacto_Aud enpactoEntity = new Xpinn.Interfaces.Entities.Enpacto_Aud
                    {
                        exitoso = string.IsNullOrWhiteSpace(error) ? 1 : 0,
                        jsonentidadpeticion = Newtonsoft.Json.JsonConvert.SerializeObject(transaccionEnpacto),
                        jsonentidadrespuesta = Newtonsoft.Json.JsonConvert.SerializeObject(respuesta),
                        tipooperacion = 1 // 1- WebServices Transacciones
                    };

                    // Creo la auditoria para enpacto
                    enpactoService.CrearEnpacto_Aud(enpactoEntity, Usuario);

                    return true;
                }
                catch (Exception ex)
                {
                    // Buildeo la entidad para la auditoria
                    Xpinn.Interfaces.Entities.Enpacto_Aud enpactoEntity = new Xpinn.Interfaces.Entities.Enpacto_Aud
                    {
                        exitoso = 0,
                        jsonentidadpeticion = Newtonsoft.Json.JsonConvert.SerializeObject(transaccionEnpacto),
                        jsonentidadrespuesta = Newtonsoft.Json.JsonConvert.SerializeObject(ex),
                        tipooperacion = 1 // 1- WebServices Transacciones
                    };

                    // Creo la auditoria para enpacto
                    enpactoService.CrearEnpacto_Aud(enpactoEntity, Usuario);

                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            pError = ex.Message;
            return false;
        }

        return false;
    }

    public int CalcularMesesDeDiferencia(DateTime fechaDesde, DateTime fechaHasta)
    {
        return Math.Abs((fechaHasta.Month - fechaDesde.Month) + 12 * (fechaHasta.Year - fechaDesde.Year));
    }

    #region OCULTAR FOOTER DE GRIDVIEW

    public void OcultarGridFooter(GridView pGridInfo, bool IsVisible)
    {
        if (pGridInfo == null)
            return;
        try
        {
            ControlCollection pControls = pGridInfo.FooterRow.Controls;
            if (pControls != null)
            {
                foreach (Control pControl in pControls)
                {
                    OcultarControl(pControl, IsVisible);
                }
            }
        }
        catch
        {
            return;
        }
    }

    public void OcultarControl(Control pControl, bool IsVisible)
    {
        if (pControl == null)
            return;
        try
        {
            if (pControl is Label)
                ((Label)pControl).Visible = IsVisible;
            if (pControl is TextBox)
                ((TextBox)pControl).Visible = IsVisible;
            else if (pControl is DropDownList)
                ((DropDownList)(pControl)).Visible = IsVisible;
            else if (pControl is Button)
                ((Button)(pControl)).Visible = IsVisible;
            else if (pControl is ImageButton)
                ((ImageButton)(pControl)).Visible = IsVisible;
            else if (pControl is DataControlFieldCell)
            {
                foreach (Control pHijo in pControl.Controls)
                {
                    OcultarControl(pHijo, IsVisible);
                }
            }
        }
        catch
        {
            return;
        }
    }

    #endregion


    public bool dejarCuotaPendiente()
    {
        bool bdejaCuotasPendientes = false;
        Xpinn.Comun.Services.GeneralService generalServicio = new Xpinn.Comun.Services.GeneralService();
        Xpinn.Comun.Entities.General _general = new Xpinn.Comun.Entities.General();
        _general = generalServicio.ConsultarGeneral(431, (Usuario)Session["Usuario"]);
        if (_general != null)
            if (_general.valor == "1")
                bdejaCuotasPendientes = true;
        return bdejaCuotasPendientes;
    }

}