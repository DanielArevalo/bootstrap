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
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;

partial class Lista : GlobalWeb
{
    
    GeoreferenciaService georefereniaservicio = new GeoreferenciaService();
    


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(georefereniaservicio.CodigoPrograma , "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(georefereniaservicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarComboOficinas(ddlOficinas);
                CargarValoresConsulta(pConsulta, georefereniaservicio.CodigoPrograma);
                if (Session[georefereniaservicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(georefereniaservicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();

        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, georefereniaservicio.CodigoPrograma);
            Actualizar();
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        gvLista.DataSourceID = null;
        gvLista.DataBind();
        gvLista.Visible = false;
        lblTotalRegs.Visible = false;
        LimpiarValoresConsulta(pConsulta, georefereniaservicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //try
        //{
        //    ConfirmarEliminarFila(e, "btnEliminar");
        //}
        //catch (Exception ex)
        //{
        //    BOexcepcion.Throw(ReferenciaServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        //}
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[georefereniaservicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        String cod_radica = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        String nombre = gvLista.Rows[e.NewEditIndex].Cells[4].Text;
        String identificacion = gvLista.Rows[e.NewEditIndex].Cells[3].Text;
        Session["Cod_persona"] = id;
        Session["Cod_radicacion"] = cod_radica;
        Session["Nombres"] = nombre;
        Session["Identificacion"] = identificacion;
        Navegar("~/Page/FabricaCreditos/Solicitud/Georeferenciacion/Nuevo.aspx");
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long id = Convert.ToInt64(gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(georefereniaservicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(georefereniaservicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Georeferencia> lstConsulta = new List<Georeferencia>();
            String filtro = obtFiltro(ObtenerValores());
            lstConsulta = georefereniaservicio.ListarGeoreferenciacion(ObtenerValores(), (Usuario)Session["usuario"], filtro);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
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

            Session.Add(georefereniaservicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(georefereniaservicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private string obtFiltro(Georeferencia vReferencia)
    {
        String filtro = String.Empty;
        if (txtNumero_radicacion.Text.Trim() != "")
            filtro += " and numero_radicacion= " + vReferencia.numero_radicacion;
        if (txtIdentificacion.Text.Trim() != "")
            filtro += " and identificacion like '%" + vReferencia.identificacion + "%'";
        
        if (txtNombres.Text.Trim() != "")
            filtro += " and nombres like '%" + vReferencia.nombres + "%'";
        if (Txtmonto_solicitado.Text.Trim() != "")
            filtro += " and MONTO_SOLICITADO like '%" + vReferencia.monto + "%'";
        if (txtLinea_credito.Text.Trim() != "")
            filtro += " and cod_linea_credito= '" + vReferencia.linea_credito + "'";
        if (ddlOficinas.SelectedIndex != 0)
            filtro += " and oficina= '" + vReferencia.oficina + "'";
        if (txtCodigoNomina.Text != "")
            filtro += " and cod_nomina like '%" + txtCodigoNomina.Text + "%'";

        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "where " + filtro;
        }
        return filtro;
    }

    private Xpinn.FabricaCreditos.Entities.Georeferencia ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.Georeferencia vReferencia = new Xpinn.FabricaCreditos.Entities.Georeferencia();

        if (txtNumero_radicacion.Text.Trim() != "")
            vReferencia.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text.Trim());
        if (txtIdentificacion.Text.Trim() != "")
            vReferencia.identificacion = txtIdentificacion.Text.Trim();       
        if (txtNombres.Text.Trim() != "")
            vReferencia.nombres = Convert.ToString(txtNombres.Text.Trim());
        if (ddlOficinas.SelectedIndex != 0)
            vReferencia.oficina = ddlOficinas.SelectedItem.Text;
        if (txtLinea_credito.Text.Trim() != "")
            vReferencia.linea_credito = Convert.ToString(txtLinea_credito.Text.Trim());

        return vReferencia;
    }

    protected void LlenarComboOficinas(DropDownList ddlOficinas)
    {
        OficinaService oficinaService = new OficinaService();
        Oficina oficina = new Oficina();
        ddlOficinas.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);
        ddlOficinas.DataTextField = "nombre";
        ddlOficinas.DataValueField = "codigo";
        ddlOficinas.DataBind();
        ddlOficinas.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    protected void txtNumero_radicacion_TextChanged(object sender, EventArgs e)
    {

    }
}

 