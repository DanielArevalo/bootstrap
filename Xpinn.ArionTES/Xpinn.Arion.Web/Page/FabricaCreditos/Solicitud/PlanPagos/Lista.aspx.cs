using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Web;

public partial class Lista : GlobalWeb
{
    CreditoPlanService creditoPlanServicio = new CreditoPlanService();
    private Xpinn.FabricaCreditos.Services.ControlCreditosService ControlCreditosServicio = new Xpinn.FabricaCreditos.Services.ControlCreditosService();
    List<Xpinn.FabricaCreditos.Entities.ControlTiempos> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.ControlTiempos>();  //Lista de los menus desplegables
    private Xpinn.FabricaCreditos.Services.ControlTiemposService ControlProcesosServicio = new Xpinn.FabricaCreditos.Services.ControlTiemposService();
   
    private Xpinn.FabricaCreditos.Entities.ViabilidadFinanciera viabilidad = new Xpinn.FabricaCreditos.Entities.ViabilidadFinanciera();
    private Xpinn.FabricaCreditos.Services.ViabilidadFinancieraService ViabilidadFinancieraServicio = new Xpinn.FabricaCreditos.Services.ViabilidadFinancieraService();

    
    String observaciones;
    String ListaSolicitada = null;
 
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        ViewState["CreditosCliente"] = "0";
        try
        {
            VisualizarOpciones(creditoPlanServicio.CodigoPrograma, "L");
            Site1 toolBar = (Site1)this.Master;
            //toolBar.eventoConsultar += btnConsultar_Click; //   Explota
            toolBar.eventoAdelante += btnAdelante_Click;
            //toolBar.eventoAtras += btnAtras_Click;

            if(Session["NumeroSolicitud"]!=null)
            txtNumeroSolicitud.Text = Session["NumeroSolicitud"].ToString();

            if (Session["Nombres"] != null && Session["Identificacion"] != null)
            {
                ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
                ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();
            }
            ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;

            btnAdelante.ValidationGroup = "";
            btnAdelante.ImageUrl = "~/Images/btnFin.jpg";

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarListas();
                CargarValoresConsulta(pConsulta, creditoPlanServicio.GetType().Name);

                if (txtNumeroSolicitud.Text != "")
                    Liquidar();
                mvLista.ActiveViewIndex = 0;

                Consultar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    /// <summary>
    ///  Se consulta los datos de los créditos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {

        ViewState["CreditosCliente"] = "1";
        Actualizar();
    }

    /// <summary>
    ///  Realizar la liquidación de una solicitud de crédito
    /// </summary>
    private void Liquidar()
    {
        try
        {
            CreditoPlan credito = new CreditoPlan();
            CreditoService creditoServicio = new CreditoService();
            Credito pCredito = new Credito();
            CreditoSolicitado proceso = new CreditoSolicitado();
            CreditoSolicitadoService servicio = new CreditoSolicitadoService();
            credito.NumeroSolicitud = Convert.ToInt64(txtNumeroSolicitud.Text.Trim());
            credito = creditoPlanServicio.Liquidar(credito, (Usuario)Session["usuario"]);
            txtCredito.Text = credito.Numero_Radicacion.ToString();
            //Agregado para consultar el estado del credito
            pCredito = creditoServicio.ConsultarCredito(Convert.ToInt64(txtCredito.Text), (Usuario)Session["usuario"]);
            proceso.estado = pCredito.nomestado;
            proceso = servicio.ConsultarCodigodelProceso(proceso, (Usuario)Session["usuario"]);
            ddlProceso.SelectedValue = Convert.ToString(proceso.Codigoproceso); 
            Session["Numero_Radicacion"] = credito.Numero_Radicacion.ToString();
            ConsultarObserControl(Convert.ToString(txtNumeroSolicitud.Text));
            GuardarControl();
           
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }
    private void ConsultarObserControl(String IdObjeto)
    {
        try
        {
            viabilidad=ViabilidadFinancieraServicio.ConsultarViabilidadFin_Control(Convert.ToInt64(txtNumeroSolicitud.Text), (Usuario)Session["usuario"]);
           // txtObservaciones.Text = "";
            if (!string.IsNullOrEmpty(viabilidad.observaciones))       
                txtObservaciones.Text = HttpUtility.HtmlDecode(viabilidad.observaciones.ToString().Trim());
           
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "ConsultarObserControl", ex);
        }       
    }
    private void GuardarControl()
    {
        Usuario pUsuario = (Usuario)Session["usuario"];
        
        if (Session["Numero_Radicacion"] != null)
        {
            Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditos = new Xpinn.FabricaCreditos.Entities.ControlCreditos();
            vControlCreditos.numero_radicacion = Convert.ToInt64(Session["Numero_Radicacion"].ToString());
            vControlCreditos.codtipoproceso = ddlProceso.SelectedItem != null ? ddlProceso.SelectedValue : null;
            vControlCreditos.fechaproceso = Convert.ToDateTime(DateTime.Now);
            vControlCreditos.cod_persona = pUsuario.codusuario;
            vControlCreditos.cod_motivo = 0;
            vControlCreditos.anexos = null;
            vControlCreditos.nivel = 0;
            if (Session["TipoCredito"] == "C")
            {
                vControlCreditos.observaciones = "CREDITO SOLICITADO CONSUMO";

                //else
                //{
                //    vControlCreditos.observaciones = this.txtObservaciones.Text.Length >= 250 ? txtObservaciones.Text.Substring(0, 249) : txtObservaciones.Text.Substring(0, txtObservaciones.Text.Length);

            }
            if (Session["TipoCredito"] == "M")
            {
                vControlCreditos.observaciones = Convert.ToString(Session["Observaciones"]);

                //else
                //{
                //    vControlCreditos.observaciones = this.txtObservaciones.Text.Length >= 250 ? txtObservaciones.Text.Substring(0, 249) : txtObservaciones.Text.Substring(0, txtObservaciones.Text.Length);

            }

            vControlCreditos = ControlCreditosServicio.CrearControlCreditos(vControlCreditos, (Usuario)Session["usuario"]);
        }            
     }
    
    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = ControlProcesosServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["usuario"]);
    }
    private void CargarListas()
    {
        try
        {

            ListaSolicitada = "EstadoProceso";
            TraerResultadosLista();
            ddlProceso.DataSource = lstDatosSolicitud;
            ddlProceso.DataTextField = "ListaDescripcion";
            ddlProceso.DataValueField = "ListaId";
            ddlProceso.DataBind();            
            ddlProceso.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }
    /// <summary>
    /// Consultar los datos de una solicitud
    /// </summary>
    private void Consultar()
    {

        Page.Validate();

        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, creditoPlanServicio.CodigoPrograma);
            Actualizar();
        }
    }

    /// <summary>
    /// Limpiar la pantala
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, creditoPlanServicio.CodigoPrograma);
    }

    /// <summary>
    /// Evento para cargar valores a la grilla.
    /// </summary>
    private void Actualizar()
    {
        try
        {
            List<CreditoPlan> lstConsulta = new List<CreditoPlan>();
            CreditoPlan credito = new CreditoPlan();
            String filtro = obtFiltro(ObtenerValores());
            lstConsulta = creditoPlanServicio.ListarCredito(credito, (Usuario)Session["usuario"], filtro);

            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
            }

            Session.Add(creditoPlanServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "Actualizar", ex);
        }
    }


    /// <summary>
    /// Esta función permite mostrar el plan de pagos del crédito seleccionado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.SelectedRow.Cells[1].Text;
        Session[creditoPlanServicio.CodigoPrograma + ".id"] = id;
        Session[creditoPlanServicio.CodigoPrograma + ".from"] = "l";
        Session[creditoPlanServicio.CodigoPrograma + ".origen"] = "~/Page/FabricaCreditos/Solicitud/PlanPagos/Lista.aspx";
        //Navegar(Pagina.Detalle);
        Response.Redirect("~/Page/FabricaCreditos/PlanPagos/Detalle.aspx");
    }


    /// <summary>
    /// Esta función actualiza la grilla de créditos al ir a la siguiente página de datos de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Evento para obtener los filtros ingresados por el usuario para realizar la consulta
    /// </summary>
    /// <param name="credito">Clase que tiene los datos del filtro</param>
    /// <returns>Retorna los filtros a aplicar</returns>
    private string obtFiltro(CreditoPlan credito)
    {

        String filtro = String.Empty;
        if (txtCredito.Text.Trim() != "")
        {
            //Consulta todos los creditos del cliente
            string CreditosCliente = ViewState["CreditosCliente"].ToString();
            if (CreditosCliente == "1")
            {
                filtro += " and Identificacion=" + Session["Identificacion"].ToString();
                ViewState["CreditosCliente"] = "0";
            }
            else
            {
                filtro += " and numero_radicacion=" + credito.Numero_radicacion;
            }
        }
        return filtro;
    }

    /// <summary>
    /// Determinar el número de radicación del crédito a mostrar los datos
    /// </summary>
    /// <returns></returns>
    private CreditoPlan ObtenerValores()
    {
        CreditoPlan credito = new CreditoPlan();
        if (txtCredito.Text.Trim() != "")
            credito.Numero_radicacion = Convert.ToInt64(txtCredito.Text.Trim());
        return credito;
    }



    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["TipoCredito"] != null)
        {
            if (Session["TipoCredito"].ToString() == "C")
                Response.Redirect("~/Page/FabricaCreditos/Solicitud/CaturaDocumentos/DocumentosAnexos/Lista.aspx");
            else
                Response.Redirect("~/Page/FabricaCreditos/Solicitud/ViabilidadFinanciera/Nuevo.aspx");
        }
    }

    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/General/Global/inicio.aspx"); //Finaliza la solicitud de credito
    }



    protected void btnInforme0_Click(object sender, EventArgs e)
    {
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();

        Xpinn.FabricaCreditos.Entities.Persona1 vPersona2 = new Xpinn.FabricaCreditos.Entities.Persona1();
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona3 = new Xpinn.FabricaCreditos.Entities.Persona1();
        Xpinn.FabricaCreditos.Entities.Referncias vPersona4 = new Xpinn.FabricaCreditos.Entities.Referncias();
        Xpinn.FabricaCreditos.Entities.Referencia refe = new Xpinn.FabricaCreditos.Entities.Referencia();
        Xpinn.FabricaCreditos.Entities.CreditoPlan creditos = new Xpinn.FabricaCreditos.Entities.CreditoPlan();
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Xpinn.FabricaCreditos.Entities.InformacionNegocio negocio = new Xpinn.FabricaCreditos.Entities.InformacionNegocio();
        if (txtCredito.Text.Trim() != "")
            negocio = DatosClienteServicio.Consultardatosnegocio(Convert.ToInt64(txtCredito.Text), Session["Identificacion"].ToString(), (Usuario)Session["usuario"]);
        else
        {
            VerError("Falta codigo de credito"); return;
        }

        if (txtCredito.Text.Trim() != "")
        creditos = DatosClienteServicio.ConsultarPersona1Paramcred(Convert.ToInt64(txtCredito.Text), Session["Identificacion"].ToString(), (Usuario)Session["usuario"]);

        mvLista.ActiveViewIndex = 1;

        List<Persona1> resultado = new List<Persona1>();
        List<Referencia> referencias = new List<Referencia>();

        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        resultado = DatosClienteServicio.ListadoPersonas1Reporte(vPersona1, (Usuario)Session["usuario"]);
        referencias = DatosClienteServicio.referencias(vPersona1, Convert.ToInt64(txtCredito.Text), (Usuario)Session["usuario"]);
 
        System.Data.DataTable table2 = new System.Data.DataTable();
        table2.Columns.Add("OBSERVACIONES");
        table2.Columns.Add("NOMBRE");
        table2.Columns.Add("TIEMPO");
        table2.Columns.Add("PROPIETARIO");
        table2.Columns.Add("CONCEPTO");
   
        DataRow datarw2;
        for (int i = 0; i < referencias.Count; i++)
        {
            datarw2 = table2.NewRow();
            refe = referencias[i];
        
                datarw2[0] = " "+ refe.observaciones;
                datarw2[1] = " " + refe.nombrereferencia;
                datarw2[2] = " " + refe.tiempo;
                datarw2[3] = " " + refe.propietario;
                datarw2[4] = " " + refe.concepto;
                table2.Rows.Add(datarw2);            
        }

        ReportParameter[] parame = new ReportParameter[29];

        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("nombre");
        table.Columns.Add("identificacion");
        table.Columns.Add("telefono");
        table.Columns.Add("direccion");
        table.Columns.Add("nombrer");
        table.Columns.Add("tipor");
        table.Columns.Add("parentesco");
        table.Columns.Add("direccionr");
        table.Columns.Add("telefonor");

        DataRow datarw;

        for (int i = 0; i < resultado.Count; i++)
        {
            datarw = table.NewRow();
            if (i == 0)
                vPersona1 = resultado[i];
            if (i == 1)
                vPersona2 = resultado[i];
            if (i >= 2)
            {
                vPersona3 = resultado[i];

                datarw[0] = vPersona3.primer_nombre + " " + vPersona3.segundo_nombre + " " + vPersona3.primer_apellido + " " + vPersona3.segundo_apellido;
                datarw[1] = vPersona3.tipo_identificacion + " " + vPersona3.identificacion;
                datarw[2] = vPersona3.direccion;
                datarw[3] = vPersona3.telefono;
                datarw[4] = "";
                datarw[5] = "";
                datarw[6] = "";
                datarw[7] = "";
                datarw[8] = "";
                table.Rows.Add(datarw);
            }
            if (txtCredito.Text == "" || Convert.ToString(resultado[i].identificacion) == "")
            { }
            else
            {
                vPersona4 = DatosClienteServicio.ListadoPersonas1ReporteReferencias(Convert.ToInt64(txtCredito.Text), resultado[i].identificacion, (Usuario)Session["usuario"]);
                datarw[0] = "";
                datarw[1] = "";
                datarw[2] = "";
                datarw[3] = "";
                datarw[4] = vPersona4.nombres;
                datarw[5] = vPersona4.descripcion;
                datarw[6] = vPersona4.ListaDescripcion;
                datarw[7] = vPersona4.direccion;
                datarw[8] = vPersona4.telefono;
                table.Rows.Add(datarw);
            }
        }
        if (resultado.Count < 3)
        {

            parame[26] = new ReportParameter("datosvacios", "NO SE ENCONTRARON DATOS PARA ESTE ITEM");

            datarw = table.NewRow();
            datarw[0] = "";
            datarw[1] = "";
            datarw[2] = "";
            datarw[3] = "";
            datarw[4] = "";
            datarw[5] = "";
            datarw[6] = "";
            datarw[7] = "";
            datarw[8] = "";
            table.Rows.Add(datarw);
        }
        else
            parame[26] = new ReportParameter("datosvacios", " ");

        parame[0] = new ReportParameter("Nombres", " " + vPersona1.primer_nombre + " " + vPersona1.segundo_nombre);
        parame[1] = new ReportParameter("Identificación", " " + vPersona1.tipo_identificacion + " " + vPersona1.identificacion);
        parame[2] = new ReportParameter("Fecha_nacimiento", " " + vPersona1.fechanacimiento.ToString());
        parame[3] = new ReportParameter("Nivel_Estudio", " ");
        parame[4] = new ReportParameter("Telefono", " " + vPersona1.telefono);
        parame[5] = new ReportParameter("Apellidos", " " + vPersona1.primer_apellido + vPersona1.segundo_apellido);
        parame[6] = new ReportParameter("Lugar_Expedicion", " ");
        parame[7] = new ReportParameter("Sexo", " " + vPersona1.sexo);
        parame[8] = new ReportParameter("mail", " " + vPersona1.email);
        parame[9] = new ReportParameter("direccion", " " + vPersona1.direccion);
        parame[10] = new ReportParameter("IdentificaciónConyuge", " " + vPersona2.tipo_identificacion + " " + vPersona2.identificacion);
        parame[11] = new ReportParameter("TelefonoConyuge", " " + vPersona2.telefono);
        parame[12] = new ReportParameter("ApellidosConyuge", " " + vPersona2.primer_apellido + vPersona2.segundo_apellido);
        parame[13] = new ReportParameter("SexoConyuge", " " + vPersona2.sexo);
        parame[14] = new ReportParameter("mailConyuge", " " + vPersona2.email);
        parame[15] = new ReportParameter("NombresConyuge", " " + vPersona2.primer_nombre + " " + vPersona2.segundo_nombre);
        parame[16] = new ReportParameter("MontoSolicitado", " " + creditos.Monto);
        parame[17] = new ReportParameter("NumeroSolicitud", " " + creditos.Numero_Obligacion);
        parame[18] = new ReportParameter("NumerodeCredito", " " + creditos.Numero_radicacion);
        parame[19] = new ReportParameter("LineadeCredito", " " + creditos.LineaCredito);
        parame[20] = new ReportParameter("ValorCuota", " " + creditos.Cuota);
        parame[21] = new ReportParameter("FechaSolicitud", " " + creditos.FechaSolicitud);
        parame[22] = new ReportParameter("nombrenegocio", " " + negocio.nombrenegocio);
        parame[23] = new ReportParameter("descripcionnegocio", " " + negocio.descripcion);
        parame[24] = new ReportParameter("direccionnegocio", " " + negocio.direccion);
        parame[25] = new ReportParameter("telefononegocio", " " + negocio.telefono );
        string cRutaDeImagen;
        cRutaDeImagen = Server.MapPath("~/Images\\") + "LogoEmpresa.jpg";
        Usuario pUsu = (Usuario)Session["usuario"];
        parame[27] = new ReportParameter("ImagenReport", cRutaDeImagen);
        parame[28] = new ReportParameter("entidad", pUsu.empresa);

        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.EnableExternalImages = true;
        ReportDataSource rds1 = new ReportDataSource("DataSet1", table);
        ReportViewer1.LocalReport.DataSources.Add(rds1);

        ReportDataSource rds2 = new ReportDataSource("DataSet2", table2);
        ReportViewer1.LocalReport.DataSources.Add(rds2);

        ReportViewer1.LocalReport.SetParameters(parame);
        ReportViewer1.LocalReport.Refresh();
        mvLista.ActiveViewIndex = 1;

    }
}