using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class Lista : GlobalWeb
{
    Usuario usuario = new Usuario();
    GarantiasComunitarias entityGarantiasComunitarias = new GarantiasComunitarias();
    GarantiasComunitariasService servicegrantias = new GarantiasComunitariasService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(servicegrantias.CodigoProgramaAplicacionReclamaciones, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicegrantias.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvAplicar.ActiveViewIndex = 0;
                btnCargarMetas.Visible = true;
                btnGuardar.Visible = false;
                lblFechaAplica.Visible = false;
                lblNumeroReclamacion.Visible = false;
                txtNumeroReclamacion.Visible = false;
                ucFecha.Visible = false;
                ucFecha.ToDateTime = DateTime.Now;
                CargarValoresConsulta(pConsulta, servicegrantias.GetType().Name);
                if (Session[servicegrantias.GetType().Name + ".consulta"] != null)
                    Actualizar();
            }
            msg.Text = "";
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicegrantias.GetType().Name + "L", "Page_Load", ex);
        }

    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, servicegrantias.GetType().Name);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, servicegrantias.GetType().Name);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicegrantias.CodigoProgramaAplicacionReclamaciones + "L", "gvLista_RowDeleting", ex);
        }
    }

    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs evt)
    {
        if (evt.CommandName == "Editar")
        {
            String[] tmp = evt.CommandArgument.ToString().Split('|');
            GarantiasComunitarias ejeMeta = new GarantiasComunitarias();
        }
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicegrantias.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            Session.Add(servicegrantias.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicegrantias.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private GarantiasComunitarias ObtenerValores()
    {
        return null;
    }

    protected void btnCargarMetas_Click(object sender, EventArgs e)
    {
        string error = "";
        try
        {
            if (FileUploadMetas.HasFile)
            {
                entityGarantiasComunitarias = new GarantiasComunitarias();
                entityGarantiasComunitarias.stream = FileUploadMetas.FileContent;

                if (servicegrantias.CargarArchivo(entityGarantiasComunitarias, ref error, (Usuario)Session["usuario"]))
                {
                    if (error.Trim() != "")
                    {
                        VerError(error);
                        return;
                    }
                    Actualizar();
                    Label1.Visible = true;
                    Label1.Text = "Su Archivo " + FileUploadMetas.FileName + " Se ha Cargado";

                    List<GarantiasComunitarias> listgarantias = new List<GarantiasComunitarias>();
                    Int64 reclamacion = 0;
                    reclamacion = entityGarantiasComunitarias.numero_reclamacion;

                    listgarantias = servicegrantias.ConsultarGarantiasComunitariasReclamacionesDetalle_Pago(reclamacion, (Usuario)Session["Usuario"]);
                    gvMovGeneral.DataSource = listgarantias;
                    gvMovGeneral.DataBind();
                    GarantiasComunitarias lista1 = new GarantiasComunitarias();
                    int i = 0;
                    foreach (GridViewRow row in gvMovGeneral.Rows)
                    {
                        lista1 = listgarantias[i];
                        Control ctrl = row.Cells[6].FindControl("ddlreclamacion");
                        if (ctrl != null)
                        {
                            DropDownList ddl_Resultado = (DropDownList)ctrl;
                            ListItem selectedListItem = ddl_Resultado.Items.FindByValue(lista1.RECLAMACION);
                            if (selectedListItem != null)
                                selectedListItem.Selected = true;
                            ddl_Resultado.Enabled = false;
                        }
                        i = i + 1;
                    }
                    btnCargarMetas.Visible = false;
                    btnGuardar.Visible = true;
                    lblFechaAplica.Visible = true;
                    lblNumeroReclamacion.Visible = true;
                    txtNumeroReclamacion.Text = reclamacion.ToString();
                    txtNumeroReclamacion.Visible = true;
                    ucFecha.Visible = true;
                    lblInicial.Visible = false;
                    FileUploadMetas.Visible = false;
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "Archivo No Valido";
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }

    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        mpeNuevo.Show();
    }

    protected void btnFinal_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        string Error = "";
        Int64 numero_reclamacion = Convert.ToInt64(txtNumeroReclamacion.Text);
        List<GarantiasComunitarias> lstReclamaciones = new List<GarantiasComunitarias>();
        lstReclamaciones.Clear();

        // Cargando las reclamaciones en una lista
        foreach (GridViewRow row in gvMovGeneral.Rows)
        {
            GarantiasComunitarias eGarantias = new GarantiasComunitarias();            
            eGarantias.FECHARECLAMACION = ucFecha.ToDateTime;
            eGarantias.NUMERO_RADICACION = Convert.ToString(row.Cells[0].Text);
            eGarantias.NITENTIDAD = Convert.ToString(row.Cells[1].Text);
            eGarantias.IDENTIFICACION = Convert.ToString(row.Cells[2].Text);
            eGarantias.FECHAPROXPAGO = Convert.ToString(row.Cells[3].Text);
            eGarantias.CAPITAL = Convert.ToDouble(row.Cells[4].Text);
            eGarantias.VALOR_PAGADO = eGarantias.CAPITAL; 
            eGarantias.CUOTAS_RECLAMAR = Convert.ToString(row.Cells[5].Text);
            eGarantias.SOBRANTE = 0;
            System.Web.UI.WebControls.DropDownList ddl_Resultado = (System.Web.UI.WebControls.DropDownList)row.Cells[6].FindControl("ddlreclamacion");
            eGarantias.RECLAMACION = ddl_Resultado.SelectedValue.ToString();
            lstReclamaciones.Add(eGarantias);
        }

        // Validando las reclamaciones
        servicegrantias.Validar(ucFecha.ToDateTime, lstReclamaciones, (Usuario)Session["Usuario"], ref Error);
        if (Error.Trim() == "")
        {

            // Aplicando las reclamaciones
            Int64 CodOpe = 0;
            servicegrantias.AplicarPago(numero_reclamacion, ucFecha.ToDateTime, lstReclamaciones, (Usuario)Session["Usuario"], ref Error, ref CodOpe);
            if (Error.Trim() == "")
            {
                //mvAplicar.ActiveViewIndex = 1;
                //lblMensajeGrabar.Text = "Reclamaciones Aplicadas Correctamente. Operación No." + CodOpe;
                Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                Usuario pUsuario = new Usuario();
                pUsuario = (Usuario)Session["Usuario"];
                Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = CodOpe;
                Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 3;
                Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = pUsuario.codusuario;
                Navegar("../../../Contabilidad/Comprobante/Nuevo.aspx");
            }
        }
        else
        {
            VerError(Error);
        }
    }

    protected void btnParar_Click(object sender, EventArgs e)
    {
        mpeNuevo.Hide();
        mvAplicar.ActiveViewIndex = 0;
    }

    protected void gvMovGeneral_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}