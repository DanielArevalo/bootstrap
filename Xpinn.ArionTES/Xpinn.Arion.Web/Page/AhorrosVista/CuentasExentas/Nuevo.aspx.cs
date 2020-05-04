using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Ahorros.Services;
using Xpinn.Ahorros.Entities;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Data;



public partial class Nuevo : GlobalWeb
{

    CuentasExentasServices ExentaServicio = new CuentasExentasServices();
    PoblarListas Poblar = new PoblarListas();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ExentaServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarExportar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExentaServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["Cuentas"] = null;
                mvAplicar.ActiveViewIndex = 0;
                CargarDropDown();          
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExentaServicio.GetType().Name + "L", "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        // GuardarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoPrograma);
        if (ddlTipoProducto.SelectedIndex == 0)
        {
            VerError("Seleccione el producto");
            ddlTipoProducto.Focus();
            return;
        }
        if (txtIdentificacion.Text=="" && txtCuenta.Text=="" )
        {
            VerError("Digite Identificacion o numero de cuenta");
            txtIdentificacion.Focus();
            txtCuenta.Focus();
            return;
        }
        ObtenerDatos();
        if (gvCuentas.Rows.Count == 0)
        {
            VerError("No hay cuentas exentas grabadas");
            return;
        }
        else
            VerError("");

    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");        
        gvCuentas.DataSource = null;
        gvCuentas.DataBind();
        LimpiarValoresConsulta(pConsulta, ExentaServicio.CodigoPrograma);
    }

    void CargarDropDown()
    {
        Poblar.PoblarListaDesplegable("tipoproducto", " cod_tipo_producto, descripcion ", " cod_tipo_producto in(1,2,3,5,9) ", " 1 ", ddlTipoProducto, (Usuario)Session["usuario"]);
    }

    protected void InicializarCuentasExentas()
    {
        List<CuentasExenta> lstCuentas = new List<CuentasExenta>();
        if (lstCuentas.Count>0)
        {
            gvCuentas.DataSource = lstCuentas;
        }
        else
        {
            gvCuentas.DataSource = null;
        }
        gvCuentas.DataBind();
        Session["Cuentas"] = lstCuentas;
    }


    protected void btnAdicionar_Click(object sender, EventArgs e)
    {
        ObtenerListaCuentasExentas();
        List<CuentasExenta> lstCuentas = new List<CuentasExenta>();
        if (Session["Cuentas"] != null)
        {
            lstCuentas = (List<CuentasExenta>)Session["Cuentas"];
        }
            for (int i = 1; i <= 1; i++)
            {
                CuentasExenta eExent = new CuentasExenta();
                eExent.idexenta = -1;
                eExent.cod_tipo_cuenta = 0;
                eExent.tipo_cuenta = null;
                eExent.numero_cuenta = "";
                eExent.nom_linea = "";
                eExent.cod_usuario = null;
                eExent.identificacion = "";
                eExent.nombre = "";
                eExent.nom_oficina = "";
                eExent.fecha = null;
                eExent.monto = null;
                lstCuentas.Add(eExent);
            }
            gvCuentas.PageIndex = gvCuentas.PageCount;
            gvCuentas.DataSource = lstCuentas;
            gvCuentas.DataBind();

            Session["Cuentas"] = lstCuentas;
        
    }


    protected void btnNumeroCuenta_Click(object sender, EventArgs e)
    {
        ButtonGrid btnNumeroCuenta = (ButtonGrid)sender;
        if (btnNumeroCuenta != null)
        {
            int rowIndex = Convert.ToInt32(btnNumeroCuenta.CommandArgument);

            DropDownListGrid ddlTipoCuenta = (DropDownListGrid)gvCuentas.Rows[rowIndex].FindControl("ddlTipoCuenta");
            if (ddlTipoCuenta == null)
                return;
            if (ddlTipoCuenta.SelectedIndex == 0)
                return;
            ctlCuentasProductos ctlListadoProd = (ctlCuentasProductos)gvCuentas.Rows[rowIndex].FindControl("ctlListadoProductos");
            TextBoxGrid txtNumeroCuenta = (TextBoxGrid)gvCuentas.Rows[rowIndex].FindControl("txtNumeroCuenta");

            Label lblLinea = (Label)gvCuentas.Rows[rowIndex].FindControl("lblLinea");
            Label lblCodPersona = (Label)gvCuentas.Rows[rowIndex].FindControl("lblCodPersona");
            Label lblIdentificacion = (Label)gvCuentas.Rows[rowIndex].FindControl("lblIdentificacion");
            Label lblNombre = (Label)gvCuentas.Rows[rowIndex].FindControl("lblNombre");
            Label lblOficina = (Label)gvCuentas.Rows[rowIndex].FindControl("lblOficina"); 
            ctlListadoProd.Mostrar(true, ddlTipoCuenta.SelectedValue, "txtNumeroCuenta", "lblCodPersona", "lblIdentificacion", "lblNombre", "lblLinea", "lblOficina");
        }
    }

    
    protected void gvCuentas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlTipoCuenta = (DropDownListGrid)e.Row.FindControl("ddlTipoCuenta");
            if (ddlTipoCuenta != null)
            {
                ddlTipoCuenta.Items.Insert(0, new ListItem("Seleccione un item", "0"));
                ddlTipoCuenta.Items.Insert(1, new ListItem("Aportes", "1"));
                ddlTipoCuenta.Items.Insert(2, new ListItem("Créditos", "2"));
                ddlTipoCuenta.Items.Insert(3, new ListItem("Ahorros Vista", "3"));
                ddlTipoCuenta.Items.Insert(4, new ListItem("CDATS", "5"));
                ddlTipoCuenta.Items.Insert(5, new ListItem("Ahorros Programado", "9"));
                ddlTipoCuenta.SelectedIndex = 0;
                ddlTipoCuenta.DataBind();
            }

            Label lblCodTipoCuenta = (Label)e.Row.FindControl("lblCodTipoCuenta");
            if (lblCodTipoCuenta != null)
                if (!string.IsNullOrEmpty(lblCodTipoCuenta.Text))
                    ddlTipoCuenta.SelectedValue = lblCodTipoCuenta.Text;
        }
    }


    protected void gvCuentas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvCuentas.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaCuentasExentas();

        List<CuentasExenta> lstCuentas = new List<CuentasExenta>();
        lstCuentas = (List<CuentasExenta>)Session["Cuentas"];
        try
        {
            foreach (CuentasExenta acti in lstCuentas)
            {
                if (acti.idexenta == conseID)
                {
                    if (conseID > 0)
                        ExentaServicio.EliminarCuentasExentas(conseID, (Usuario)Session["usuario"]);
                    lstCuentas.Remove(acti);
                    break;
                }
            }
            Session["Cuentas"] = lstCuentas;

            gvCuentas.DataSourceID = null;
            gvCuentas.DataBind();
            gvCuentas.DataSource = lstCuentas;
            gvCuentas.DataBind();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }


    protected List<CuentasExenta> ObtenerListaCuentasExentas()
    {
        try
        {
            List<CuentasExenta> lstCuentas = new List<CuentasExenta>();
            List<CuentasExenta> lista = new List<CuentasExenta>();

            foreach (GridViewRow rfila in gvCuentas.Rows)
            {
                CuentasExenta eExent = new CuentasExenta();

                Label lblidexenta = (Label)rfila.FindControl("lblidexenta");
                if (lblidexenta != null && lblidexenta.Text != "")
                    eExent.idexenta = Convert.ToInt64(lblidexenta.Text);
                else
                    eExent.idexenta = -1;

                DropDownListGrid ddlTipoCuenta = (DropDownListGrid)rfila.FindControl("ddlTipoCuenta");
                if (ddlTipoCuenta.SelectedValue != null)
                    if (ddlTipoCuenta.SelectedValue != "")
                        eExent.tipo_cuenta = Convert.ToInt32(ddlTipoCuenta.SelectedValue);

                TextBoxGrid txtNumeroCuenta = (TextBoxGrid)rfila.FindControl("txtNumeroCuenta");
                if (txtNumeroCuenta != null)
                    if (!string.IsNullOrEmpty(txtNumeroCuenta.Text))
                        eExent.numero_cuenta = txtNumeroCuenta.Text;

                Label lblLinea = (Label)rfila.FindControl("lblLinea");
                if (lblLinea.Text != "")
                    eExent.nom_linea = lblLinea.Text;

                Label lblCodigoUsuario = (Label)rfila.FindControl("lblCodigoUsuario");
                if (lblCodigoUsuario.Text != "")
                    eExent.cod_usuario = Convert.ToInt64(lblCodigoUsuario.Text);

                Label lblCodPersona = (Label)rfila.FindControl("lblCodPersona");
                if (lblCodPersona.Text != "")
                    eExent.cod_persona = Convert.ToInt64(lblCodPersona.Text);

                Label lblIdentificacion = (Label)rfila.FindControl("lblIdentificacion");
                if (lblIdentificacion.Text != "")
                    eExent.identificacion = lblIdentificacion.Text;
                
                Label lblNombre = (Label)rfila.FindControl("lblNombre");
                if (lblNombre.Text != "")
                    eExent.nombre = lblNombre.Text;

                Label lblOficina = (Label)rfila.FindControl("lblOficina");
                if (lblOficina.Text != "")
                    eExent.nom_oficina = lblOficina.Text;

                fecha txtFecha = (fecha)rfila.FindControl("txtFecha_Exenta");
                if (txtFecha.Text != "")
                    eExent.fecha_exenta = Convert.ToDateTime(txtFecha.Text);

                decimales txtMonto = (decimales)rfila.FindControl("txtMonto");
                if (txtMonto.Text != "")
                    eExent.monto = Convert.ToDecimal(txtMonto.Text.Replace(".",""));

                lista.Add(eExent);
                Session["Cuentas"] = lista;

                if (eExent.tipo_cuenta != null && eExent.tipo_cuenta != 0 && eExent.numero_cuenta != null)
                {
                    lstCuentas.Add(eExent);
                }
            }
            return lstCuentas;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExentaServicio.CodigoPrograma, "ObtenerListaCuentasExentas", ex);
            return null;
        }
    }


    protected void ObtenerDatos()
    {
        try
        {
            //RECUPERAR DATOS - GRILLA CUENTAS EXENTO
            Usuario pUsu = (Usuario)Session["usuario"];
            List<CuentasExenta> lstCuentas = new List<CuentasExenta>();
            CuentasExenta pData = new CuentasExenta();
            if(txtCuenta.Text!="")
            pData.numero_cuenta = txtCuenta.Text;
            if(ddlTipoProducto.SelectedValue!="")
            pData.tipo_cuenta = Convert.ToInt32(ddlTipoProducto.SelectedValue);
            if(txtIdentificacion.Text!="")
                pData.identificacion = txtIdentificacion.Text;
            lstCuentas = ExentaServicio.ListarCuentaExenta(pData, (Usuario)Session["usuario"]);
            
            if (lstCuentas.Count > 0)
            {
                if ((lstCuentas != null) || (lstCuentas.Count != 0))
                {
                    gvCuentas.DataSource = lstCuentas;
                    gvCuentas.DataBind();
                }
                Session["Cuentas"] = lstCuentas;
            }
            else
            {
               InicializarCuentasExentas();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExentaServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
    protected void btnFinal_Click(object sender, EventArgs e)
    {
        Navegar("~/General/Global/inicio.aspx");
    }
    
    
    public Boolean ValidarDatos()
    {
        List<CuentasExenta> lstCuentas = new List<CuentasExenta>();
        if (Session["Cuentas"] != null)
        {
            lstCuentas = (List<CuentasExenta>)Session["Cuentas"];
        }

        foreach (CuentasExenta nCuenta in lstCuentas)
        {
        
        }
        return true;   
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            ctlMensaje.MostrarMensaje("Esta seguro de registrar los datos ingresados?");
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            CuentasExenta eExenta = new CuentasExenta();

            eExenta.lstCuentas = new List<CuentasExenta>();
            eExenta.lstCuentas = ObtenerListaCuentasExentas();

            //CREAR O MODIFICAR
            ExentaServicio.CrearCuentaExenta(eExenta, (Usuario)Session["usuario"]);
            mvAplicar.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExentaServicio.CodigoPrograma, "btnContinuar_Click", ex);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //
    }
    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=CuentasExentasGMF.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.Default;
        StringWriter sw = new StringWriter();
        ExpGrilla expGrilla = new ExpGrilla();

        sw = ObtenerGrilla(gvLista);
        Response.Write(expGrilla.style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();

    }

    public StringWriter ObtenerGrilla(GridView GridView1)
    {
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            for (int i = 0; i < GridView1.Columns.Count; i++)
            {
                string s = GridView1.Columns[i].HeaderStyle.CssClass;
                if (s == "gridIco")
                    GridView1.Columns[i].Visible = false;
                else
                    GridView1.Columns[i].HeaderStyle.CssClass = "gridHeader";
            }

            GridView1.HeaderRow.BackColor = Color.White;

            foreach (GridViewRow row in GridView1.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = GridView1.RowStyle.BackColor;
                    }
                    cell.CssClass = "gridItem";
                    List<Control> lstControls = new List<Control>();

                    //Add controls to be removed to Generic List
                    foreach (Control control in cell.Controls)
                    {
                        lstControls.Add(control);
                    }

                    //Loop through the controls to be removed and replace then with Literal
                    foreach (Control control in lstControls)
                    {
                        switch (control.GetType().Name)
                        {
                            case "Label":
                                cell.Controls.Add(new Literal { Text = (control as Label).Visible ? (control as Label).Text : "" });
                                break;
                            case "TextBox":
                                cell.Controls.Add(new Literal { Text = (control as TextBox).Text });
                                break;
                            case "TextBoxGrid":
                                cell.Controls.Add(new Literal { Text = (control as TextBoxGrid).Text });
                                break;
                            case "general_controles_decimales_ascx":
                                cell.Controls.Add(new Literal { Text = (control as decimales).Text });
                                break;
                            case "general_controles_fecha_ascx":
                                cell.Controls.Add(new Literal { Text = (control as fecha).Text });
                                break;
                            case "DropDownListGrid":
                                cell.Controls.Add(new Literal { Text = (control as DropDownListGrid).SelectedItem.Text });
                                break;
                        }
                        cell.Controls.Remove(control);
                    }
                }
            }

            GridView1.RenderControl(hw);

            return sw;
        }
    }
    void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        mvAplicar.ActiveViewIndex = 0;
        Site toolBar1 = (Site)this.Master;
        toolBar1.MostrarRegresar(true);
        toolBar1.MostrarGuardar(true);
        toolBar1.MostrarConsultar(true);
    }
    protected void btnConsultarTodas_Click(object sender, EventArgs e)
    {
        mvAplicar.ActiveViewIndex =2;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarExportar(true);
       
        Site toolBar1 = (Site)this.Master;
        toolBar1.MostrarRegresar(true);
        toolBar1.MostrarGuardar(false);
        toolBar1.MostrarConsultar(false);
   
        List<CuentasExenta> lstCuentas = new List<CuentasExenta>();
        CuentasExenta pData = new CuentasExenta();
        lstCuentas = ExentaServicio.ListarCuentaExenta(pData, (Usuario)Session["usuario"]);

        if (lstCuentas.Count > 0)
        {
            if ((lstCuentas != null) || (lstCuentas.Count != 0))
            {
                gvLista.DataSource = lstCuentas;
                gvLista.Visible = true;
                gvLista.DataBind();
            }
        }
    }
} 
