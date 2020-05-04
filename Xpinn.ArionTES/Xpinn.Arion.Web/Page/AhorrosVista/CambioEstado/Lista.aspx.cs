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

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AhorroVistaServicio.CodigoProgramaCambioEstado, "L");

            Site toolBar = (Site)this.Master;

            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaCambioEstado, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //CargarListar();
                CargarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoProgramaCambioEstado);
                if (Session[AhorroVistaServicio.CodigoProgramaCambioEstado + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaCambioEstado, "Page_Load", ex);
        }
    }

//private void CargarListar()
//{
//    Xpinn.Ahorros.Services.LineaAhorroServices linahorroServicio = new Xpinn.Ahorros.Services.LineaAhorroServices();
//    Xpinn.Ahorros.Entities.LineaAhorro linahorroVista = new Xpinn.Ahorros.Entities.LineaAhorro();
//    ddlLineaAhorro.DataTextField = "descripcion";
//    ddlLineaAhorro.DataValueField = "cod_linea_ahorro";
//    ddlLineaAhorro.DataSource = linahorroServicio.ListarLineaAhorro(linahorroVista, (Usuario)Session["usuario"]);
//    ddlLineaAhorro.DataBind();
//}

    

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoProgramaCambioEstado);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        gvLista.Visible = false;
        lblTotalRegs.Text = ("");
        txtAprobacion_fin.Text = ("");
        LimpiarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoProgramaCambioEstado);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnEliminar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaCambioEstado + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[AhorroVistaServicio.CodigoProgramaCambioEstado + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[AhorroVistaServicio.CodigoProgramaCambioEstado + ".id"] = id;
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
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaCambioEstado, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        VerError("");
        try
        {
            DateTime pFechaAper;
            pFechaAper = txtAprobacion_fin.ToDateTime == null ? DateTime.MinValue : txtAprobacion_fin.ToDateTime;
            string pFiltro = obtFiltro();
            List<Xpinn.Ahorros.Entities.AhorroVista> lstConsulta = new List<Xpinn.Ahorros.Entities.AhorroVista>();
            lstConsulta = AhorroVistaServicio.ListarAhorroVista(pFiltro,pFechaAper, (Usuario)Session["usuario"]);

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

            Session.Add(AhorroVistaServicio.CodigoProgramaCambioEstado + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaCambioEstado, "Actualizar", ex);
        }
    }

    private string obtFiltro()
    {
        String filtro = String.Empty;

        if (txtNumCuenta.Text.Trim() != "")
            filtro += " AND A.NUMERO_CUENTA = '" + txtNumCuenta.Text + "'";

        if (ddlLineaAhorro.Indice != 0)
            filtro += " AND A.COD_LINEA_AHORRO = '" + ddlLineaAhorro.Value + "'";

        if (txtAprobacion_fin.Text.Trim() != "")
            filtro += " AND A.FECHA_APERTURA = '" + txtAprobacion_fin.Text + "'";

        if (ddlEstado_Cuenta.SelectedValue !="-1")
        { 
        if (ddlEstado_Cuenta.SelectedValue != "")
            filtro += " AND A.ESTADO=" + ddlEstado_Cuenta.SelectedValue;
           }
        if (txtIdentificacion.Text.Trim() != "")
            filtro += " AND P.IDENTIFICACION = '" + txtIdentificacion.Text + "'";


        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "WHERE " + filtro;
        }

        return filtro;
    }


    private Xpinn.Ahorros.Entities.AhorroVista ObtenerValores()
    {
        Xpinn.Ahorros.Entities.AhorroVista vAhorroVista = new Xpinn.Ahorros.Entities.AhorroVista();
        if (txtNumCuenta.Text.Trim() != "")
            vAhorroVista.numero_cuenta = Convert.ToString(txtNumCuenta.Text.Trim());
        //numero de cuenta actualizacion

        if (ddlLineaAhorro.Text.Trim() != "")
            vAhorroVista.cod_linea_ahorro = Convert.ToString(ddlLineaAhorro.Text.Trim());
        //linea de ahorro

        if (ddlEstado_Cuenta.SelectedIndex != 0)
            vAhorroVista.estado = Convert.ToInt32(ddlEstado_Cuenta.SelectedValue.Trim());
        //estado de cuenta

        if (txtIdentificacion.Text.Trim() != "")
        //numero de identificacion

        if (txtAprobacion_fin.ToDate.Trim() != "")
            vAhorroVista.fecha_apertura = Convert.ToDateTime(txtAprobacion_fin.ToDate.Trim());
        //fecha de aprobacion
        
        
        return vAhorroVista;
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