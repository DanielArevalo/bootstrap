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

partial class Lista : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.InformacionNegocioService InformacionNegocioServicio = new Xpinn.FabricaCreditos.Services.InformacionNegocioService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(InformacionNegocioServicio.CodigoPrograma, "L");

            Site1 toolBar = (Site1)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            //toolBar.eventoLimpiar += btnLimpiar_Click;

            ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
            ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();
            ((Label)Master.FindControl("lblCod_Cliente")).Text = Session["Cod_persona"].ToString(); 
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InformacionNegocioServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, InformacionNegocioServicio.CodigoPrograma);
                if (Session[InformacionNegocioServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
           
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InformacionNegocioServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, InformacionNegocioServicio.CodigoPrograma);
       
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, InformacionNegocioServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, InformacionNegocioServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InformacionNegocioServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[InformacionNegocioServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;//gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[InformacionNegocioServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long id = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[0].Text);
            InformacionNegocioServicio.EliminarInformacionNegocio(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InformacionNegocioServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(InformacionNegocioServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.InformacionNegocio> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.InformacionNegocio>();
            lstConsulta = InformacionNegocioServicio.ListarInformacionNegocio(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            gvLista.EmptyDataText = "";
            gvLista.DataSource = lstConsulta;
            gvLista.DataBind();

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                lblTotalRegs.Text = "No se ha registrado información";
            }

            Session.Add(InformacionNegocioServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InformacionNegocioServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.InformacionNegocio ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.InformacionNegocio vInformacionNegocio = new Xpinn.FabricaCreditos.Entities.InformacionNegocio();

        vInformacionNegocio.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());

    if(txtDireccion.Text.Trim() != "")
        vInformacionNegocio.direccion = Convert.ToString(txtDireccion.Text.Trim());
    if(txtTelefono.Text.Trim() != "")
        vInformacionNegocio.telefono = Convert.ToString(txtTelefono.Text.Trim());
    if(txtLocalidad.Text.Trim() != "")
        vInformacionNegocio.localidad = Convert.ToString(txtLocalidad.Text.Trim());
    if(txtNombrenegocio.Text.Trim() != "")
        vInformacionNegocio.nombrenegocio = Convert.ToString(txtNombrenegocio.Text.Trim());
    if(txtDescripcion.Text.Trim() != "")
        vInformacionNegocio.descripcion = Convert.ToString(txtDescripcion.Text.Trim());
    if(txtAntiguedad.Text.Trim() != "")
        vInformacionNegocio.antiguedad = Convert.ToInt64(txtAntiguedad.Text.Trim());
    if(txtPropia.Text.Trim() != "")
        vInformacionNegocio.propia = Convert.ToInt64(txtPropia.Text.Trim());
    if(txtArrendador.Text.Trim() != "")
        vInformacionNegocio.arrendador = Convert.ToString(txtArrendador.Text.Trim());
    if(txtTelefonoarrendador.Text.Trim() != "")
        vInformacionNegocio.telefonoarrendador = Convert.ToString(txtTelefonoarrendador.Text.Trim());
    if(txtCodactividad.Text.Trim() != "")
        vInformacionNegocio.codactividad = Convert.ToInt64(txtCodactividad.Text.Trim());
    if(txtExperiencia.Text.Trim() != "")
        vInformacionNegocio.experiencia = Convert.ToInt64(txtExperiencia.Text.Trim());
    if(txtEmplperm.Text.Trim() != "")
        vInformacionNegocio.emplperm = Convert.ToInt64(txtEmplperm.Text.Trim());
    if(txtEmpltem.Text.Trim() != "")
        vInformacionNegocio.empltem = Convert.ToInt64(txtEmpltem.Text.Trim());
    if(txtFechacreacion.Text.Trim() != "")
        vInformacionNegocio.fechacreacion = Convert.ToDateTime(txtFechacreacion.Text.Trim());
    if(txtUsuariocreacion.Text.Trim() != "")
        vInformacionNegocio.usuariocreacion = Convert.ToString(txtUsuariocreacion.Text.Trim());
    if(txtFecultmod.Text.Trim() != "")
        vInformacionNegocio.fecultmod = Convert.ToDateTime(txtFecultmod.Text.Trim());
    if(txtUsuultmod.Text.Trim() != "")
        vInformacionNegocio.usuultmod = Convert.ToString(txtUsuultmod.Text.Trim());

        return vInformacionNegocio;
    }
    

   

    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        string EstadoCivil = Session["EstadoCivil"].ToString();
        if (EstadoCivil == "1" || EstadoCivil == "3")
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFamiliar/Conyuge/Nuevo.aspx");
        else
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFamiliar/GrupoFamiliar/Lista.aspx");
    }
    
    protected void btnAtras_Click1(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/DatosSolicitud/SolicitudCredito.aspx");
    }
}