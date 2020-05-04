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
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;

partial class Lista : GlobalWeb
{

    AnulacionServices Anulacionservicio = new AnulacionServices();
    PoblarListas poblar = new PoblarListas();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(Anulacionservicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Anulacionservicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarCombos();
                ucFechaInicial.Visible = true;
                ucFechaFinal.Visible = true;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Anulacionservicio.CodigoPrograma, "Page_Load", ex);
        }
    }
  
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        gvLista.DataSourceID = null;
        gvLista.DataBind();
        gvLista.Visible = false;
        lblTotalRegs.Visible = false;
        LimpiarValoresConsulta(pConsulta, Anulacionservicio.CodigoPrograma);
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[Anulacionservicio.CodigoPrograma + ".id"] = id;
        Navegar("~/Page/Tesoreria/AnulacionOperaciones/Nuevo.aspx");
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session[Anulacionservicio.CodigoPrograma + ".id"] = id;
      
        String cod_radica = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        String nombre = gvLista.Rows[e.NewEditIndex].Cells[4].Text;
        String identificacion = gvLista.Rows[e.NewEditIndex].Cells[3].Text;

        Session["Cod_persona"] = id;
        Session["Cod_radicacion"] = cod_radica;
        Session["Nombres"] = nombre;
        Session["Identificacion"] = identificacion;
        Navegar(Pagina.Nuevo);
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
            BOexcepcion.Throw(Anulacionservicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }
    private void Actualizar()
    {
        try
        {            
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            List<AnulacionOperaciones> lstConsulta = new List<AnulacionOperaciones>();
            lstConsulta = Anulacionservicio.listaranulaciones(obtFiltro(), (Usuario)Session["Usuario"]);

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(Anulacionservicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Anulacionservicio.CodigoPrograma, "Actualizar", ex);
        }
    }
    private string obtFiltro()
    {
        String filtro = String.Empty;
        if (txtoperacion.Text.Trim() != "")
            filtro += " and COD_OPE = " + txtoperacion.Text;
        if (txtcomprobante.Text.Trim() != "")
            filtro += " and NUM_COMP = " + txtcomprobante.Text;
        if (ddlcomprobantes.SelectedValue != "0")
            filtro += " and TIPO_COMP like '%" + ddlcomprobantes.SelectedValue + "%'";
        if (ddloperacion.SelectedIndex != 0)
            filtro += " and TIPO_OPE = " + ddloperacion.SelectedValue;
        else
            filtro += " and TIPO_OPE = 119";
        if (ucFechaInicial.TieneDatos)
        {
            string sFecha = Convert.ToDateTime(ucFechaInicial.ToDateTime.ToString()).ToString(GlobalWeb.gFormatoFecha);
            filtro += " and FECHA_OPER >= TO_DATE('" + sFecha + "', 'dd/MM/yyyy') ";
        }
        if (ucFechaFinal.TieneDatos)
        {
            string sFecha = Convert.ToDateTime(ucFechaFinal.ToDateTime.ToString()).ToString(GlobalWeb.gFormatoFecha);
            filtro += " and FECHA_OPER <= TO_DATE('" + sFecha + "', 'dd/MM/yyyy') ";
        }
        if (txtNumLista.Text != "")
            filtro += " and NUM_LISTA = " + txtNumLista.Text.Trim() ;

        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "where " + filtro;
        }
        return filtro;
    }

 
    protected void LlenarCombos()
    {

        poblar.PoblarListaDesplegable("TIPO_COMP", "TIPO_COMP, DESCRIPCION", "", "DESCRIPCION", ddlcomprobantes, Usuario);
        poblar.PoblarListaDesplegable("TIPO_OPE", "TIPO_OPE, DESCRIPCION", "", "DESCRIPCION", ddloperacion, Usuario);

    }



}

 