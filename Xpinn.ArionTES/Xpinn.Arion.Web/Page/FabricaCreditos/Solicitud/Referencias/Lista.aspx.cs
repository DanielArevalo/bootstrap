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
    private Xpinn.FabricaCreditos.Services.RefernciasService RefernciasServicio = new Xpinn.FabricaCreditos.Services.RefernciasService();

    //Listas desplegables:
    String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    List<Xpinn.FabricaCreditos.Entities.Referncias> lstListasDesplegables = new List<Xpinn.FabricaCreditos.Entities.Referncias>();  //Lista de los menus desplegables
    

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(RefernciasServicio.CodigoPrograma, "L");

            Site1 toolBar = (Site1)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            if (Session["Retorno"].ToString() != "1")
            toolBar.eventoAdelante += btnAdelante_Click;
            toolBar.eventoAtras += btnAtras_Click;

            ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
            ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();
            
            ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;
            btnAdelante.ValidationGroup = "";

            if (Session["TipoCredito"].ToString() == "C")     //Si el credito es ordinario deja invisible la primera pesta�a
            {
                btnAdelante.ImageUrl = "~/Images/btnCrearSolicitud.jpg";
            }
            else
            {
                btnAdelante.ImageUrl = "~/Images/btnInformacionFinanciera.jpg";

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RefernciasServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {      
        try
        {
            if (!IsPostBack)
            {               
                CargarValoresConsulta(pConsulta, RefernciasServicio.CodigoPrograma);
                CargarListas();

                if (Session[RefernciasServicio.GetType().Name + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RefernciasServicio.GetType().Name, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, RefernciasServicio.CodigoPrograma);
        Borrar();    
    }

    private void Borrar()
    {
        CargarListas();
        txtCelular.Text = "";
        txtNombres.Text = "";
        txtTelefono.Text = "";
        txtTeloficina.Text = "";
        direccion.Text = "";
        rblTipoReferencia.SelectedValue = "1";
        idObjeto = "";
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, RefernciasServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, RefernciasServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
            ConfirmarEliminarFila(e, "btnBorrar");       

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Cambia el codigo de la columna "Referencias" por texto           

            switch (e.Row.Cells[4].Text)
            {
                case "1":
                    e.Row.Cells[4].Text = "Familiar";   
                    break;
                case "2":
                    e.Row.Cells[4].Text = "Personal";
                    break;
                case "3":
                    e.Row.Cells[4].Text = "Comercial";
                    break;
            }

            
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        //String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        String id = gvLista.SelectedRow.Cells[0].Text;
        Session[RefernciasServicio.CodigoPrograma + ".id"] = id;
        //Navegar(Pagina.Detalle);
        Edicion();
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;//gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[RefernciasServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long id = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[0].Text);
            RefernciasServicio.EliminarReferncias(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RefernciasServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(RefernciasServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.Referncias> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Referncias>();
            lstConsulta = RefernciasServicio.ListarReferncias(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            gvLista.EmptyDataText = "";
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(RefernciasServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RefernciasServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.Referncias ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.Referncias vReferncias = new Xpinn.FabricaCreditos.Entities.Referncias();
        
        vReferncias.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        if (rblTipoReferencia.Text.Trim() != "")
            vReferncias.tiporeferencia = Convert.ToInt64(rblTipoReferencia.SelectedValue);
        if(txtNombres.Text.Trim() != "")
            vReferncias.nombres = Convert.ToString(txtNombres.Text.Trim());
        vReferncias.codparentesco = 0;
        if (rblTipoReferencia.SelectedValue == "1")
        {
            if (ddlParentesco.Text.Trim() != "")
                vReferncias.codparentesco = Convert.ToInt64(ddlParentesco.SelectedValue);
        }
        if(direccion.Text.Trim() != "")
            vReferncias.direccion = Convert.ToString(direccion.Text.Trim());
        if(txtTelefono.Text.Trim() != "")
            vReferncias.telefono = Convert.ToString(txtTelefono.Text.Trim());
        if(txtTeloficina.Text.Trim() != "")
            vReferncias.teloficina = Convert.ToString(txtTeloficina.Text.Trim());
        if(txtCelular.Text.Trim() != "")
            vReferncias.celular = Convert.ToString(txtCelular.Text.Trim());
        if(txtEstado.Text.Trim() != "")
            vReferncias.estado = Convert.ToInt64(txtEstado.Text.Trim());
        if(txtCodusuverifica.Text.Trim() != "")
            vReferncias.codusuverifica = Convert.ToInt64(txtCodusuverifica.Text.Trim());
        if(txtFechaverifica.Text.Trim() != "")
            vReferncias.fechaverifica = Convert.ToDateTime(txtFechaverifica.Text.Trim());
        if(txtCalificacion.Text.Trim() != "")
            vReferncias.calificacion = Convert.ToString(txtCalificacion.Text.Trim());
        if(txtObservaciones.Text.Trim() != "")
            vReferncias.observaciones = Convert.ToString(txtObservaciones.Text.Trim());
        if(txtNumero_radicacion.Text.Trim() != "")
            vReferncias.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text.Trim());

        return vReferncias;
    }


    private void Edicion()
{
    try
    {
        if (Session[RefernciasServicio.CodigoPrograma + ".id"] != null)
            VisualizarOpciones(RefernciasServicio.CodigoPrograma, "E");
        else
            VisualizarOpciones(RefernciasServicio.CodigoPrograma, "A");
       
        //Page_Load
        if (Session[RefernciasServicio.CodigoPrograma + ".id"] != null)
        {
            idObjeto = Session[RefernciasServicio.CodigoPrograma + ".id"].ToString();
             if (idObjeto != null)
                {
                    ObtenerDatos(idObjeto);
                   Session.Remove(RefernciasServicio.CodigoPrograma + ".id");
                }
        
        }
        else
        {
            CargarListas();
        }
    }
    catch (Exception ex)
    {
        BOexcepcion.Throw(RefernciasServicio.GetType().Name + "A", "Page_PreInit", ex);
    }
}


    private void CargarListas()
    {
        try
        {
            ListaSolicitada = "Parentesco";
            TraerResultadosLista();
            ddlParentesco.DataSource = lstListasDesplegables;
            ddlParentesco.DataTextField = "ListaDescripcion";
            ddlParentesco.DataValueField = "ListaId";
            ddlParentesco.DataBind();

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RefernciasServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    private void TraerResultadosLista()
    {
        lstListasDesplegables.Clear();
        lstListasDesplegables = RefernciasServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);

    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
       
        try
        {
            Xpinn.FabricaCreditos.Entities.Referncias vReferncias = new Xpinn.FabricaCreditos.Entities.Referncias();

            if (idObjeto != "")
                vReferncias = RefernciasServicio.ConsultarReferncias(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (txtCodreferencia.Text != "") vReferncias.codreferencia = Convert.ToInt64(txtCodreferencia.Text.Trim());
            vReferncias.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
            if (rblTipoReferencia.Text != "") vReferncias.tiporeferencia = Convert.ToInt64(rblTipoReferencia.SelectedValue);
            vReferncias.nombres = (txtNombres.Text != "") ? Convert.ToString(txtNombres.Text.Trim()) : String.Empty;
            vReferncias.codparentesco = 0;
            if (rblTipoReferencia.SelectedValue == "1")
            {
                if (ddlParentesco.Text != "") vReferncias.codparentesco = Convert.ToInt64(ddlParentesco.SelectedValue);
            }           
            vReferncias.direccion = (direccion.Text != "") ? Convert.ToString(direccion.Text.Trim()) : String.Empty;
            vReferncias.telefono = (txtTelefono.Text != "") ? Convert.ToString(txtTelefono.Text.Trim()) : String.Empty;
            vReferncias.teloficina = (txtTeloficina.Text != "") ? Convert.ToString(txtTeloficina.Text.Trim()) : String.Empty;
            vReferncias.celular = (txtCelular.Text != "") ? Convert.ToString(txtCelular.Text.Trim()) : String.Empty;
            if (txtEstado.Text != "") vReferncias.estado = Convert.ToInt64(txtEstado.Text.Trim());
            if (txtCodusuverifica.Text != "") vReferncias.codusuverifica = Convert.ToInt64(txtCodusuverifica.Text.Trim());
            if (txtFechaverifica.Text != "") vReferncias.fechaverifica = Convert.ToDateTime(txtFechaverifica.Text.Trim());
            vReferncias.calificacion = (txtCalificacion.Text != "") ? Convert.ToString(txtCalificacion.Text.Trim()) : String.Empty;
            vReferncias.observaciones = (txtObservaciones.Text != "") ? Convert.ToString(txtObservaciones.Text.Trim()) : String.Empty;
            if (txtNumero_radicacion.Text != "") vReferncias.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text.Trim());

            if (idObjeto != "")
            {
                vReferncias.codreferencia = Convert.ToInt64(idObjeto);
                RefernciasServicio.ModificarReferncias(vReferncias, (Usuario)Session["usuario"]);
            }
            else
            {
                vReferncias = RefernciasServicio.CrearReferncias(vReferncias, (Usuario)Session["usuario"]);
                idObjeto = vReferncias.codreferencia.ToString();
                
            }

            Session[RefernciasServicio.CodigoPrograma + ".id"] = idObjeto;
         
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RefernciasServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
        Borrar();
        Actualizar();
   
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Referncias vReferncias = new Xpinn.FabricaCreditos.Entities.Referncias();
            vReferncias = RefernciasServicio.ConsultarReferncias(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vReferncias.codreferencia != Int64.MinValue)
                txtCodreferencia.Text = HttpUtility.HtmlDecode(vReferncias.codreferencia.ToString().Trim());
             if (vReferncias.tiporeferencia != Int64.MinValue)
                rblTipoReferencia.SelectedValue = HttpUtility.HtmlDecode(vReferncias.tiporeferencia.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferncias.nombres))
                txtNombres.Text = HttpUtility.HtmlDecode(vReferncias.nombres.ToString().Trim());
            if (vReferncias.codparentesco != Int64.MinValue)
                ddlParentesco.Text = HttpUtility.HtmlDecode(vReferncias.codparentesco.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferncias.direccion))
                direccion.Text = HttpUtility.HtmlDecode(vReferncias.direccion.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferncias.telefono))
                txtTelefono.Text = HttpUtility.HtmlDecode(vReferncias.telefono.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferncias.teloficina))
                txtTeloficina.Text = HttpUtility.HtmlDecode(vReferncias.teloficina.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferncias.celular))
                txtCelular.Text = HttpUtility.HtmlDecode(vReferncias.celular.ToString().Trim());
            if (vReferncias.estado != Int64.MinValue)
                txtEstado.Text = HttpUtility.HtmlDecode(vReferncias.estado.ToString().Trim());
            if (vReferncias.codusuverifica != Int64.MinValue)
                txtCodusuverifica.Text = HttpUtility.HtmlDecode(vReferncias.codusuverifica.ToString().Trim());
            if (vReferncias.fechaverifica != DateTime.MinValue)
                txtFechaverifica.Text = HttpUtility.HtmlDecode(vReferncias.fechaverifica.ToShortDateString());
            if (!string.IsNullOrEmpty(vReferncias.calificacion))
                txtCalificacion.Text = HttpUtility.HtmlDecode(vReferncias.calificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferncias.observaciones))
                txtObservaciones.Text = HttpUtility.HtmlDecode(vReferncias.observaciones.ToString().Trim());
            if (vReferncias.numero_radicacion != Int64.MinValue)
                txtNumero_radicacion.Text = HttpUtility.HtmlDecode(vReferncias.numero_radicacion.ToString().Trim());
           
            ValidarTipoReferencia();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RefernciasServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

  
    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["Retorno"].ToString() == "1")
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/ModificarSolicitud/SolicitudCredito.aspx");
        else
        {
            if (Session["TipoCredito"].ToString() == "C")
            {
               Response.Redirect("~/Page/FabricaCreditos/Solicitud/Patrimonio/Default.aspx");
            }
            else
            {
                Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFamiliar/GrupoFamiliar/Lista.aspx");
            }
        }
    }
    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
      
        if (Session["TipoCredito"].ToString() == "C")
        {
            Session["GarantiaReal"] = "0";
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/DatosSolicitud/SolicitudCredito.aspx");
        }
        else
        {
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/Default.aspx");
        }
    }

    protected void rblTipoReferencia_SelectedIndexChanged(object sender, EventArgs e)
    {
        ValidarTipoReferencia();
    }

    private void ValidarTipoReferencia()
    {
        if (rblTipoReferencia.SelectedValue == "1")
        {


            ddlParentesco.Enabled = true;            
        }
        else
        {
            //ListItem selectedListItem = ddlParentesco.Items.FindByValue("0");
            //if (selectedListItem != null)
            //    selectedListItem.Selected = true;

            ddlParentesco.Enabled = false;        
        }
    }
    protected void ddlParentesco_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}