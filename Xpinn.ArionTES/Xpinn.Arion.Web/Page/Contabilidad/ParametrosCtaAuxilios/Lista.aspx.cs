using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;

partial class Lista : GlobalWeb
{
    Xpinn.Contabilidad.Services.ParametroCtasAuxiliosService servicioParCueAuxilios = new Xpinn.Contabilidad.Services.ParametroCtasAuxiliosService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(servicioParCueAuxilios.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicioParCueAuxilios.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                CargarListas();
                CargarValoresConsulta(pConsulta, servicioParCueAuxilios.CodigoPrograma);
              
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicioParCueAuxilios.CodigoPrograma, "Page_Load", ex);
        }
    }

   
    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();

    }

    private void CargarListas()
    {
        try
        {
            
            PoblarLista("TIPO_TRAN", ddltipotransaccion);
            cargarAuxilio();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    private void cargarAuxilio()
    {
        String filtro = "";
        Xpinn.Auxilios.Services.LineaAuxilioServices linahorroServicio = new Xpinn.Auxilios.Services.LineaAuxilioServices();
        Xpinn.Auxilios.Entities.LineaAuxilio linahorroVista = new Xpinn.Auxilios.Entities.LineaAuxilio();
        ddlLineaAux.DataTextField = "DESCRIPCION";
        ddlLineaAux.DataValueField = "COD_LINEA_AUXILIO";
        ddlLineaAux.DataSource = linahorroServicio.ListarLineaAuxilio(linahorroVista, (Usuario)Session["usuario"], filtro);
        ddlLineaAux.DataBind();
        ddlLineaAux.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }


     private string obtFiltro()
    {
        string filtro = "";

        if (ddlLineaAux.SelectedIndex != 0)
            filtro += " and P.COD_LINEA_AUXILIO = " + ddlLineaAux.SelectedValue;
        if (txtCodCuenta.Text != "")
            filtro += " and P.COD_CUENTA = " + txtCodCuenta.Text;       
        if (ddltipotransaccion.SelectedIndex != 0)
            filtro += " and T.TIPO_TRAN = " + ddltipotransaccion.SelectedValue;

        return filtro;
    }


    private void Actualizar()
    {
        try
        {
            List<Xpinn.Contabilidad.Entities.Par_Cue_LinAux> lstConsulta = new List<Xpinn.Contabilidad.Entities.Par_Cue_LinAux>();
            lstConsulta = servicioParCueAuxilios.ListarPar_Cue_LinAux(obtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;               
                gvLista.DataBind();
                Session["DTPARAMETROS"] = lstConsulta;
            }
            else
            {
                gvLista.Visible = false;
                Session["DTPARAMETROS"] = null;
            }
            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();

            Session.Add(servicioParCueAuxilios.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicioParCueAuxilios.CodigoPrograma, "Actualizar", ex);
        }
    }


    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0 && Session["DTPARAMETROS"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.Columns[0].Visible = false;
                gvLista.Columns[1].Visible = false;
                gvLista.DataSource = Session["DTPARAMETROS"];
                gvLista.DataBind();
                gvLista.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvLista);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=Par_Cue_LinApo.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
                gvLista.AllowPaging = true;
                gvLista.DataBind();
            }
            else
            {
                VerError("No existen Datos");
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch
        {
        }
    }


    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {       
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, servicioParCueAuxilios.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, servicioParCueAuxilios.CodigoPrograma);
    }

   

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[servicioParCueAuxilios.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
            Session["ID"] = id;
            ctlMensaje.MostrarMensaje("Desea realizar la Eliminación del Registro Seleccionado?");            
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }        
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            servicioParCueAuxilios.EliminarPar_Cue_LinAux(Convert.ToInt64(Session["ID"].ToString()), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicioParCueAuxilios.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicioParCueAuxilios.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    private Par_Cue_LinAux obtenerValores()
    {
        VerError("");
        Par_Cue_LinAux entidad = new Par_Cue_LinAux();

        try
        {
            if (ddlLineaAux.SelectedIndex != 0)
                entidad.cod_linea_auxilio = ddlLineaAux.SelectedValue;            
            if (ddltipotransaccion.SelectedIndex != 0)
                entidad.tipo_tran = Convert.ToInt32(ddltipotransaccion.SelectedValue);
        }
        catch
        {
            VerError("");
        }
        if (txtCodCuenta.Text != "")
            entidad.cod_cuenta = txtCodCuenta.Text;

        return entidad;
    }


}