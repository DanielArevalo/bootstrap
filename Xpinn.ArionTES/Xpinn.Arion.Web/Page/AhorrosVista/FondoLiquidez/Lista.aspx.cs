using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using Xpinn.Util;
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Globalization;


partial class Lista : GlobalWeb
{
    private Xpinn.Ahorros.Services.AhorroVistaServices AhorroVistaServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AhorroVistaServicio.CodigoProgramaFondoLiquidez, "L");

            Site toolBar = (Site)this.Master;

            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarExportar(false);
     
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaFondoLiquidez, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {                
                CargarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoProgramaFondoLiquidez);
                panelTotales.Visible = false; 
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaFondoLiquidez, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {

        ///mensaje al guardar
        ctlMensaje.MostrarMensaje("Desea guardar los datos del fondo de liquidez?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            ///Hago la operacion
            VerError("");
            Usuario vUsuario = new Usuario();
            vUsuario = (Usuario)Session["Usuario"];
            List<AhorroVista> lstIngreso = new List<AhorroVista>();
            lstIngreso = ObtenerListaDetalle();                       
            AhorroVistaServicio.enviarfondoliquidez(lstIngreso,(Usuario)Session["usuario"]);

            panelTotales.Visible = true;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(false);
            toolBar.MostrarConsultar(true);
        }        
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaFondoLiquidez, "btnGuardar_Click", ex);
        }
    }

    protected List<Xpinn.Ahorros.Entities.AhorroVista> ObtenerListaDetalle()
    {
        List<Xpinn.Ahorros.Entities.AhorroVista> lstDatos = new List<Xpinn.Ahorros.Entities.AhorroVista>();

        foreach (GridViewRow rfila in gvLista.Rows)
        {
            /// Cargo los datos de la grilla a la entidad provision_ahorro por eBenef
            Xpinn.Ahorros.Entities.AhorroVista ahorro = new Xpinn.Ahorros.Entities.AhorroVista();

            ahorro.fecha = Convert.ToDateTime(rfila.Cells[1].Text);
            ahorro.saldo_total = Convert.ToDecimal(rfila.Cells[2].Text.Replace(gSeparadorMiles, ""));

            ahorro.Anio = Convert.ToDecimal(txtAño.Text);
            ahorro.idfondoliq = Convert.ToInt64(txtFondo.Text.Replace(".", ""));
            ahorro.Mes = Convert.ToInt32(ddlmes.SelectedValue);
            ahorro.pdias = Convert.ToInt64(txtDias.Text);
            ahorro.saldo_promedio = Convert.ToInt32(txtPromedio.Text.Replace(".", ""));
            ahorro.valor_fondo = Convert.ToInt32(txtFondos.Text.Replace(".", ""));
            lstDatos.Add(ahorro);
        }
        return lstDatos;
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        ///pone en 0 los datos nulos cuando consulta y verifica las variables  
        if (txtAño.Text == "")
        {
            VerError("Ingrese un año valido");
            return;           
        }
        if (txtFondo.Text == "")
        {
            VerError("Ingrese un fondo valido");
            return;
        }
        if (ddlmes.SelectedIndex == 0) 
        {
            VerError("Seleccione el mes");
            return; 
        }

        GuardarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoProgramaFondoLiquidez);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        txtAño.Text = ("");
        txtFondo.Text = ("");
        ddlmes.SelectedIndex = 0;
        gvLista.Visible = false;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        toolBar.MostrarExportar(false);
        lblTotalRegs.Visible = false;
        panelTotales.Visible = false;
        LimpiarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoProgramaFondoLiquidez);
    }


   protected void calcularpromedio()
   {

       decimal valo2 = 0;
       decimal mes = 0,suma = 0;

       foreach (GridViewRow rfila in gvLista.Rows)
       {
           suma = rfila.Cells[2].Text != "&nbsp;" ? Convert.ToDecimal(rfila.Cells[2].Text) : 0;

           mes += Convert.ToDecimal(suma);
       }

       valo2 = Convert.ToDecimal(mes) / Convert.ToInt32(txtDias.Text);
       txtPromedio.Text = valo2.ToString("n0");
   }

   protected void calcularfondo()
   {

       decimal valo3 = 0;

       valo3 = Convert.ToDecimal(txtFondo.Text)/100* Convert.ToDecimal(txtPromedio.Text);

       txtFondos.Text = valo3.ToString("n0");

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
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaFondoLiquidez, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        VerError("");
        try
        {
            ///carga todo a la entidad provision_ahorro por la variable ahorro

            Xpinn.Ahorros.Entities.AhorroVista ahorro = new Xpinn.Ahorros.Entities.AhorroVista();
            ahorro.Mes = Convert.ToInt32(ddlmes.SelectedValue);
            ahorro.Anio = Convert.ToDecimal(txtAño.Text);
            ahorro.Fondo = Convert.ToString(txtFondo.Text);

            List<Xpinn.Ahorros.Entities.AhorroVista> lstConsulta = new List<Xpinn.Ahorros.Entities.AhorroVista>();

            ///ingresa a la capa de bussines por listar provision
            lstConsulta = AhorroVistaServicio.FondoLiquidez(ahorro, (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {

                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(true);
                toolBar.MostrarExportar(true);
                gvLista.Visible = true;
                txtDias.Text = lstConsulta.Count.ToString();
                Session["DTAhorroVista"] = lstConsulta;
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
                calcularpromedio();
                calcularfondo();
                panelTotales.Visible = true;
            }
            else
            {
                txtDias.Text = "0";
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarExportar(false);
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                panelTotales.Visible = false;
            }

            Session.Add(AhorroVistaServicio.CodigoProgramaFondoLiquidez + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaFondoLiquidez, "Actualizar", ex);
        }
    }

    private Xpinn.Ahorros.Entities.AhorroVista ObtenerValor()
    {
        Xpinn.Ahorros.Entities.AhorroVista vAhorroVista = new Xpinn.Ahorros.Entities.AhorroVista();
        if (txtAño.Text.Trim() != "")
            vAhorroVista.Anio = Convert.ToDecimal(txtAño);
        //numero de cuenta actualizacion

        if (txtFondo.Text.Trim() != "")
            vAhorroVista.Fondo = Convert.ToString(txtFondo.Text.Trim());
        //linea de ahorro

        if (ddlmes.SelectedIndex != 0)
            vAhorroVista.Mes = Convert.ToInt32(ddlmes.SelectedValue.Trim());
        //estado de cuenta

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
        Response.AddHeader("content-disposition", "attachment;filename=FondoLiquidez.xls");
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