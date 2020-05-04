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
    CuentasPorPagarService CuentaService = new CuentasPorPagarService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {       
            VisualizarOpciones(CuentaService.CodigoProgramaAnticipo, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            
         ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaAnticipo, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtfecanticipo.ToDateTime = DateTime.Now;
                txtfecFactuta.ToDateTime = DateTime.Now;
                cargarDropdown();
                
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaAnticipo, "Page_Load", ex);
        }
    }

    bool validarIngresoDefechas()
    {
   /*    if (txtIngresoIni.Text != "" && txtIngresoFin.Text != "")
        {
            if (Convert.ToDateTime(txtIngresoIni.Text) > Convert.ToDateTime(txtIngresoFin.Text))
            {
                VerError("Datos erroneos en las Fechas de Ingreso.");
                return false;
            }
        }*/

      /*  if (txtVencimientoIni.Text != "" && txtVencimientoFin.Text != "")
        {
            if (Convert.ToDateTime(txtVencimientoIni.Text) > Convert.ToDateTime(txtVencimientoFin.Text))
            {
                VerError("Datos erroneos en las Fechas de Vencimiento.");
                return false;
            }
        
       }*/
        return true;
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        if (validarIngresoDefechas())
        {
            Page.Validate();
            gvLista.Visible = true;
            if (Page.IsValid)
            {
                Actualizar();
            }
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    void cargarDropdown()
    {

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
            BOexcepcion.Throw(CuentaService.CodigoProgramaAnticipo, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[3].Text;         
        Session[CuentaService.CodigoProgramaAnticipo + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
        Session["ID"] = id;
       ctlMensaje.MostrarMensaje("Desea realizar la eliminación de la cuenta?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            CuentaService.EliminarCUENTAXPAGAR_ANTICIPO(Convert.ToInt32(Session["ID"]), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaAnticipo, "btnContinuarMen_Click", ex);
        }
    }

    
    private void Actualizar()
    {
        try
        {
            List<CuentasPorPagar> lstConsulta = new List<CuentasPorPagar>();
            String filtro = obtFiltro(ObtenerValores());
            DateTime pFechaIni, pFechaFin, pVencIni, pVencFin;
            pFechaIni = txtfecanticipo.ToDateTime == null ? DateTime.MinValue : txtfecanticipo.ToDateTime;
           pFechaFin = txtfecFactuta.ToDateTime == null ? DateTime.MinValue : txtfecFactuta.ToDateTime;
            ///pVencIni = txtVencimientoIni.ToDateTime == null ? DateTime.MinValue : txtVencimientoIni.ToDateTime;
            ///pVencFin = txtVencimientoFin.ToDateTime == null ? DateTime.MinValue : txtVencimientoFin.ToDateTime;

           lstConsulta = CuentaService.ListarAnticipos(ObtenerValores(), pFechaIni, pFechaFin, (Usuario)Session["usuario"], filtro);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;           

            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;                
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();                
               
            }
            else
            {
                panelGrilla.Visible = false;                
                lblTotalRegs.Visible = false;
            }

            Session.Add(CuentaService.CodigoProgramaAnticipo + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaAnticipo, "Actualizar", ex);
        }
    }

    private CuentasPorPagar ObtenerValores()
    {
        CuentasPorPagar vCuentas = new CuentasPorPagar();
       // if (txtCodigo.Text.Trim() != "")
         //   vCuentas.codigo_factura = Convert.ToInt32(txtCodigo.Text.Trim());
        if (txtNumFact.Text.Trim() != "")
            vCuentas.numero_factura = txtNumFact.Text.Trim();
       
      //  if (ddlTipoCuenta.SelectedValue != "0")
        //    vCuentas.idtipo_cta_por_pagar = Convert.ToInt32(ddlTipoCuenta.SelectedValue.Trim());
       // if (txtIdProveedor.Text.Trim() != "")
         //   vCuentas.cod_persona= Convert.ToInt64(txtIdProveedor.Text.Trim());
        if (txtNombre.Text.Trim() != "")
            vCuentas.nombre = txtNombre.Text.Trim().ToUpper();

        return vCuentas;
    }

   
   
    private string obtFiltro(CuentasPorPagar Cuentas)
    {
        Configuracion conf = new Configuracion();

        String filtro = String.Empty;

       if (txtcodigo.Text.Trim() != "")
           filtro += " and c.cod_persona = " + txtcodigo.Text; ;
        if (txtidentificacion.Text.Trim() != "")
            filtro += " and p.identificacion like '%" + txtidentificacion.Text + "%'";       
       

        return filtro;
    }


   
}