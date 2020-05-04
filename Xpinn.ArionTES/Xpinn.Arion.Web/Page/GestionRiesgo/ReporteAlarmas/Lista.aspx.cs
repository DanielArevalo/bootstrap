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
using Xpinn.Riesgo.Services;
using Xpinn.Riesgo.Entities;


partial class Lista : GlobalWeb
{
   
    private Xpinn.Riesgo.Services.SarlaftAlertaServices alarmaServicio = new Xpinn.Riesgo.Services.SarlaftAlertaServices();
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {       

            VisualizarOpciones(alarmaServicio.CodigoPrograma, "L");
            
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(alarmaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Site toolBar = (Site)this.Master;              
                toolBar.MostrarGuardar(false);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(alarmaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }
 

     protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        ctlMensaje.MostrarMensaje("Desea guardar los datos registrados?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        SarlaftAlertaServices servicioAlerta = new SarlaftAlertaServices();
        Usuario usuario = (Usuario)Session["Usuario"];
        List<Xpinn.Riesgo.Entities.SarlaftAlerta> lstConsulta = new List<Xpinn.Riesgo.Entities.SarlaftAlerta>();
        if (Session["lstConsulta"] == null)
            return;
        lstConsulta = (List<Xpinn.Riesgo.Entities.SarlaftAlerta>)Session["lstConsulta"];

        int codigo = 0;
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBox cbModificado = (CheckBox)rFila.FindControl("cbModificado");
            if (cbModificado != null)
                if (cbModificado.Checked == true)
                {
                    Int64 idAlerta = ConvertirStringToInt(gvLista.DataKeys[codigo].Value.ToString());
                    SarlaftAlerta eAlerta = new SarlaftAlerta();
                    eAlerta = lstConsulta[codigo];
                    if (eAlerta != null)
                    {
                        TextBoxGrid txtGestion = (TextBoxGrid)rFila.FindControl("txtGestion");
                        DropDownListGrid ddlestado = (DropDownListGrid)rFila.FindControl("ddlestado");
                        if (txtGestion != null)
                        {
                            eAlerta.consulta = txtGestion.Text;
                        }
                        if(ddlestado != null)
                        {
                            eAlerta.estado = ddlestado.SelectedValue;
                        }
                        string estado = rFila.Cells[10].Text;
                        if (estado != "GESTIONADA") //Grabar solamente si la Alerta no ha sido gestionada
                            servicioAlerta.ModificarSarlaftAlerta(eAlerta, usuario);
                    }
                    // Actualizar contador
                    codigo += 1;
                    // Activar botón para grabar
                    if (codigo == 1)
                    { 
                        Site toolBar = (Site)this.Master;
                        toolBar.MostrarConsultar(true);
                        toolBar.MostrarGuardar(false);
                    }                    
                }
        }
        Actualizar();    
    }

    protected void MostrarMensaje(string mensaje)
    {
        Page.Controls.Add(new LiteralControl(
        "<script language='javascript'>" +
        "window.alert('" + mensaje + "')</script>"));
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        Page.Validate();
        gvLista.Visible = true;
        Actualizar();   
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session["idalerta"] = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        e.NewEditIndex = -1;
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
            BOexcepcion.Throw(alarmaServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    
    private void Actualizar()
    {
        try
        {
            List<Xpinn.Riesgo.Entities.SarlaftAlerta> lstConsulta = new List<Xpinn.Riesgo.Entities.SarlaftAlerta>();
            SarlaftAlerta eAlerta = new SarlaftAlerta();
            DateTime? pFechaIni = txtFecIni.Text != "" ? Convert.ToDateTime(txtFecIni.Text) : DateTime.Now;
            DateTime? pFechaFin = txtFecFin.Text != "" ? Convert.ToDateTime(txtFecFin.Text) : DateTime.Now;
            int pOrdenar = Convert.ToInt32(ConvertirStringToInt(ddlOrdenar.SelectedValue));
            lstConsulta = alarmaServicio.ListarSarlaftAlerta(eAlerta, pFechaIni, pFechaFin, pOrdenar, (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                Session["lstConsulta"] = lstConsulta;
                pListado.Visible = true;
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();                
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                pListado.Visible = false;
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }            
                                    
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
            Session.Add(alarmaServicio.CodigoPrograma + ".consulta", 1);

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(alarmaServicio.CodigoPrograma, "Actualizar", ex);
        }
    }


    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DropDownListGrid ddltipocuenta = (DropDownListGrid)e.Row.FindControl("ddltipocuenta");
            //if (ddltipocuenta != null)
            //{
            //    Xpinn.Tesoreria.Services.EmpresaRecaudoServices serviciosempresarecaudo = new Xpinn.Tesoreria.Services.EmpresaRecaudoServices();
            //    List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> lstConsulta = new List<Xpinn.Tesoreria.Entities.EmpresaRecaudo>();
            //    Xpinn.Tesoreria.Entities.EmpresaRecaudo pData = new Xpinn.Tesoreria.Entities.EmpresaRecaudo();
            //    lstConsulta = serviciosempresarecaudo.ListarEmpresaRecaudo(pData, (Usuario)Session["usuario"]);
            //    if (lstConsulta.Count > 0)
            //    {
                       
            //        ddltipocuenta.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            //        ddltipocuenta.Items.Insert(1, new ListItem("AHORROS", "1"));
            //        ddltipocuenta.Items.Insert(2, new ListItem("CORRIENTE", "2"));

            //        ddltipocuenta.SelectedIndex = 0;
            //        ddltipocuenta.DataBind();
            //    }
            //}
        }
    }
    

   private void ObtenerDatos(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtCuentaBancaria = (TextBox)e.Row.FindControl("txtCuentaBancaria");
            //Xpinn.Auxilios.Services.DesembolsoAuxilioServices desembolsoservice = new Xpinn.Auxilios.Services.DesembolsoAuxilioServices();
            //CuentasBancarias codigo = new CuentasBancarias();
            //string filtro = " AND PRINCIPAL=1";
            //foreach (GridViewRow rFila in gvLista.Rows)
            //{
            //    if (rFila.Cells[13].Text != "")
            //    {
            //        codigo.cod_persona = Convert.ToInt64(rFila.Cells[13].Text);
            //        codigo = desembolsoservice.ConsultarCuentasBancarias(codigo, filtro, (Usuario)Session["usuario"]);
            //    }
            //}
        }
    }

    

    protected void gvOperacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }

    protected void ddlOrdenar_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Actualizar();
    }

    protected void txtGestion_TextChanged(object sender, EventArgs e)
    {
        VerError("");
        TextBoxGrid txtGestion = (TextBoxGrid)sender;
        int rowIndex = Convert.ToInt32(txtGestion.CommandArgument);
        if (rowIndex >= 0)
        {
            CheckBox cbModificado = (CheckBox)gvLista.Rows[rowIndex].FindControl("cbModificado");
            if (cbModificado != null)
                cbModificado.Checked = true;
            gvLista.Rows[rowIndex].BackColor = System.Drawing.Color.LightGreen;
        }
    }


}