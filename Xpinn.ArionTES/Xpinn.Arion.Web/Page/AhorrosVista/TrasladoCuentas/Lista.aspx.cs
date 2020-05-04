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
using System.Globalization;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    private Xpinn.Ahorros.Services.AhorroVistaServices AhorroVistaServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();
    PoblarListas lista = new PoblarListas();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AhorroVistaServicio.CodigoProgramaTrasladocuenta, "L");

            Site toolBar = (Site)this.Master;

            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaTrasladocuenta, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargar();
                CargarListar();
                CargarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoProgramaTrasladocuenta);
                if (Session[AhorroVistaServicio.CodigoProgramaTrasladocuenta + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaTrasladocuenta, "Page_Load", ex);
        }
    }

    protected void cargar()
    {

        lista.PoblarListaDesplegable("oficina", " COD_OFICINA,NOMBRE", " estado = 1", "1", ddlOficina, (Usuario)Session["usuario"]);
    }

private void CargarListar()
{
    Xpinn.Ahorros.Services.LineaAhorroServices linahorroServicio = new Xpinn.Ahorros.Services.LineaAhorroServices();
    Xpinn.Ahorros.Entities.LineaAhorro linahorroVista = new Xpinn.Ahorros.Entities.LineaAhorro();
    ddlLineaAhorro.DataTextField = "descripcion";
    ddlLineaAhorro.DataValueField = "cod_linea_ahorro";
    ddlLineaAhorro.DataSource = linahorroServicio.ListarLineaAhorro(linahorroVista, (Usuario)Session["usuario"]);
    ddlLineaAhorro.DataBind();
}

    

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoProgramaTrasladocuenta);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        txtAprobacion_fin.Text = ("");
        gvLista.Visible = false;
        lblTotalRegs.Visible = false;
        LimpiarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoProgramaTrasladocuenta);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnEliminar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaTrasladocuenta + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[AhorroVistaServicio.CodigoProgramaTrasladocuenta + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[AhorroVistaServicio.CodigoProgramaTrasladocuenta + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string id = Convert.ToString(e.Keys[0]);
            AhorroVistaServicio.EliminarAhorroVista(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
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
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaTrasladocuenta, "gvLista_PageIndexChanging", ex);
        }
    }

    private string obtFiltro()
    {
        String filtro = String.Empty;

        if (txtNumCuenta.Text.Trim() != "")
            filtro += " AND A.NUMERO_CUENTA = '" + txtNumCuenta.Text + "'";
        if (ddlLineaAhorro.SelectedIndex != 0)
            filtro += " AND A.COD_LINEA_AHORRO = '" + ddlLineaAhorro.SelectedValue + "'";
        if (txtIdentificacion.Text.Trim() != "")
            filtro += " AND P.IDENTIFICACION = '" + txtIdentificacion.Text + "'";
        if (ddlOficina.SelectedIndex != 0)
            filtro += " AND A.COD_OFICINA = " + ddlOficina.SelectedValue;
        filtro += " and rownum between 1 and 100";
        filtro += " AND A.ESTADO = 1";

        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "WHERE " + filtro;
        }

        return filtro;
    }


    private void Actualizar()
    {
        VerError("");
        try
        {
            List<Xpinn.Ahorros.Entities.AhorroVista> lstConsulta = new List<Xpinn.Ahorros.Entities.AhorroVista>();
            DateTime pFechaApe ;
            pFechaApe = txtAprobacion_fin.ToDateTime == null? DateTime.MinValue : txtAprobacion_fin.ToDateTime;
            string pfiltro = obtFiltro();
            lstConsulta = AhorroVistaServicio.ListarAhorroVista(pfiltro,pFechaApe, (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                Session["DTAhorroVista"] = lstConsulta;
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(AhorroVistaServicio.CodigoProgramaTrasladocuenta + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaTrasladocuenta, "Actualizar", ex);
        }
    }


    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvLista);
    }

    protected void ExportToExcel(GridView GridView1)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=AhorroVista.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.Default;
        StringWriter sw = new StringWriter();
        ExpGrilla expGrilla = new ExpGrilla();

        sw = expGrilla.ObtenerGrilla(GridView1, null);

        Response.Write(expGrilla.style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

}