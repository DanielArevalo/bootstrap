using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class Lista : GlobalWeb
{
    private Usuario usuario = new Usuario();
    private Comprobante entityCargaComprobante = new Comprobante();
    private ComprobanteService servicecargacomprobante = new ComprobanteService();
    private bool bGrabar = false;
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(servicecargacomprobante.CodigoProgramaCarga, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicecargacomprobante.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvAplicar.ActiveViewIndex = 0;
                btnCargarComp.Visible = true;
                rbContable.Checked = true;
                rbContable_CheckedChanged(null, null);
                CargarValoresConsulta(pConsulta, servicecargacomprobante.GetType().Name);
                if (Session[servicecargacomprobante.GetType().Name + ".consulta"] != null)
                    Actualizar();
            }
            msg.Text = "";
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicecargacomprobante.GetType().Name + "L", "Page_Load", ex);
        }

    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, servicecargacomprobante.GetType().Name);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, servicecargacomprobante.GetType().Name);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicecargacomprobante.CodigoProgramaCarga + "L", "gvLista_RowDeleting", ex);
        }
    }

    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs evt)
    {
        if (evt.CommandName == "Editar")
        {
            String[] tmp = evt.CommandArgument.ToString().Split('|');
            Comprobante ejeMeta = new Comprobante();
        }
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicecargacomprobante.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            Session.Add(servicecargacomprobante.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicecargacomprobante.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private Comprobante ObtenerValores()
    {
        return null;
    }

    protected void btnCargarComp_Click(object sender, EventArgs e)
    {
        if (Session["LSTCARGACOMPROBANTE"] != null)
            Session.Remove("LSTCARGACOMPROBANTE");        
        VerError("");
        string error = "";
        try
        {
            if (FileUploadComprobante.HasFile)
            {
                List<DetalleComprobante> lstcargacomprobante = new List<DetalleComprobante>();
                Stream stream = FileUploadComprobante.FileContent;
                if (servicecargacomprobante.CargarArchivo(stream, rblTipoNorma.SelectedValue, bGrabar, ref lstcargacomprobante, ref error, (Usuario)Session["usuario"]))
                {

                    if (error.Trim() != "")
                    {
                        VerError(error);
                        return;
                    }
                    Actualizar();
                    Label1.Visible = true;
                    Label1.Text = "Su Archivo " + FileUploadComprobante.FileName + " Se ha Cargado";
                    
                    Int64 operacion = 1;

                    if (bGrabar)
                        lstcargacomprobante = servicecargacomprobante.ConsultarCargaComprobanteDetalle(operacion, (Usuario)Session["Usuario"]);
                    else
                        Session["LSTCARGACOMPROBANTE"] = lstcargacomprobante; 
                    gvLista.DataSource = lstcargacomprobante;
                    gvLista.DataBind();
                    btnCargarComp.Visible = false;
                    lblInicial.Visible = false;
                    FileUploadComprobante.Visible = false;
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarGuardar(true);
                    toolBar.eventoGuardar += btnGuardar_Click;

                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "Archivo No Valido";
                }
            }
        }
        catch (Exception ex)
        {
            VerError(ex.ToString());
        }

    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        mpeNuevo.Show();
    }

    protected void btnFinal_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        List<DetalleComprobante> lstCargaComprobante = new List<DetalleComprobante>();
        lstCargaComprobante.Clear();

        // Cargando el detalle del comprobante
        int contador = 1;
        foreach (GridViewRow row in gvLista.Rows)
        {
            DetalleComprobante eCargaComprobante = new DetalleComprobante();
            if (row.Cells[0].Text != "" && row.Cells[0].Text != "&nbsp;")
                eCargaComprobante.cod_cuenta = Convert.ToString(row.Cells[0].Text);
            if (row.Cells[1].Text != "" && row.Cells[1].Text != "&nbsp;")
                eCargaComprobante.centro_costo = Convert.ToInt64(row.Cells[1].Text);
            eCargaComprobante.detalle = Convert.ToString(row.Cells[2].Text);
            eCargaComprobante.tipo = Convert.ToString(row.Cells[3].Text);
            if (row.Cells[4].Text != "" && row.Cells[4].Text != "&nbsp;")
                eCargaComprobante.valor = Convert.ToDecimal(row.Cells[4].Text);
            if (row.Cells[5].Text.Trim() != "" && row.Cells[5].Text.Trim() != "&nbsp;")                
                eCargaComprobante.identificacion = Convert.ToString(row.Cells[5].Text);            
            if (row.Cells[6].Text.Trim() != "" && row.Cells[6].Text.Trim() != "&nbsp;")
                eCargaComprobante.centro_gestion = Convert.ToInt64(row.Cells[6].Text);
            // Si los datos no se graban en una tabla temporal entonces completar la información faltante.
            if (!bGrabar)
            {
                //entidad.base_comp = Convert.ToDecimal(resultado["BASE_COMP"]); else entidad.base_comp = 0;
                //entidad.porcentaje = Convert.ToDecimal(resultado["PORCENTAJE"]); else entidad.porcentaje = 0;
                //entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                //entidad.nombre_cuenta_nif = Convert.ToString(resultado["NOM_CUENTA_NIIF"]);

                if (row.Cells[12].Text.Trim() != "" && row.Cells[12].Text.Trim() != "&nbsp;")
                    eCargaComprobante.cod_cuenta_niif = Convert.ToString(row.Cells[12].Text);

                // Código de Operación
                eCargaComprobante.operacion = "1";
                // Código del tercero
                if (row.Cells[7].Text.Trim() != "" && row.Cells[7].Text.Trim() != "&nbsp;")
                    eCargaComprobante.tercero = Convert.ToInt64(row.Cells[7].Text);
                // Nombre del tercero
                if (row.Cells[8].Text.Trim() != "" && row.Cells[8].Text.Trim() != "&nbsp;")
                    eCargaComprobante.nom_tercero = Convert.ToString(row.Cells[8].Text);
                if (row.Cells[10].Text.Trim() != "" && row.Cells[10].Text.Trim() != "&nbsp;")
                    eCargaComprobante.maneja_ter = Convert.ToInt32(row.Cells[10].Text);
                if (row.Cells[11].Text.Trim() != "" && row.Cells[11].Text.Trim() != "&nbsp;")
                    eCargaComprobante.impuesto = Convert.ToInt32(row.Cells[11].Text);

                if (rblTipoNorma.SelectedValue == "L")
                {
                    if (eCargaComprobante.cod_cuenta != null)
                    {
                        // Datos de la cuenta contable
                        if (row.Cells[9].Text.Trim() != "" && row.Cells[9].Text.Trim() != "&nbsp;")
                            eCargaComprobante.nombre_cuenta = Convert.ToString(row.Cells[9].Text);
                    }
                    if (eCargaComprobante.cod_cuenta_niif != null)
                    {
                        // Datos de la cuenta contable
                        if (row.Cells[9].Text.Trim() != "" && row.Cells[9].Text.Trim() != "&nbsp;")
                            eCargaComprobante.nombre_cuenta_nif = Convert.ToString(row.Cells[9].Text);
                    }
                }
                else
                {
                    if (eCargaComprobante.cod_cuenta_niif != null)
                    {
                        // Datos de la cuenta contable
                        if (row.Cells[9].Text.Trim() != "" && row.Cells[9].Text.Trim() != "&nbsp;")
                            eCargaComprobante.nombre_cuenta_nif = Convert.ToString(row.Cells[9].Text);
                    }
                }
                eCargaComprobante.moneda = 1;
            }
            if ((eCargaComprobante.valor != 0 && eCargaComprobante.valor != null) || contador != 1)
                lstCargaComprobante.Add(eCargaComprobante);
            contador += 1; 
        }

        // Validando el detalle
        string Error = "";
        bool bCarga = true;
        bCarga = servicecargacomprobante.Validar(lstCargaComprobante, (Usuario)Session["Usuario"], ref Error);
        if (Error.Trim() == "" || bCarga == false)
        {      
            Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            if (rbIngreso.Checked == true)
                Session["Comprobantecarga"] = "1";
            else if (rbEgreso.Checked == true)
                Session["Comprobantecarga"] = "5";
            else
                Session["Comprobantecarga"] = "2";
            if (!bGrabar)
                Session["LSTCARGACOMPROBANTE"] = lstCargaComprobante;
            Session["TipoNormaCarga"] = rblTipoNorma.SelectedValue; 
            Session["Codoper"] = null;
            Session["numerocheque"] = null;
            Session["entidad"] = null; 
            Session["cuenta"] = null;            
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] = 2;
            Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = null;
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = null;
            Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = pUsuario.codusuario;
            Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");      
        }
        else
        {
            if (Error.Trim() =="")
                Error = "Error en detalle de comprobante";
            VerError(Error);
        }
    }

    protected void btnParar_Click(object sender, EventArgs e)
    {
        mpeNuevo.Hide();
        mvAplicar.ActiveViewIndex = 0;
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void rbIngreso_CheckedChanged(object sender, EventArgs e)
    {
        if (rbIngreso.Checked == true)
        {
            rbEgreso.Checked = false;
            rbContable.Checked = false;
        }

    }

    protected void rbEgreso_CheckedChanged(object sender, EventArgs e)
    {
        if (rbEgreso.Checked == true)
        {
            rbIngreso.Checked = false;
            rbContable.Checked = false;
        }
    }

    protected void rbContable_CheckedChanged(object sender, EventArgs e)
    {
        if (rbContable.Checked == true)
        {
            rbIngreso.Checked = false;
            rbEgreso.Checked = false;
        }
    }

    protected void txtPegar_TextChanged(object sender, EventArgs e)
    {
        mpePegar.Hide();
        string error = "";
        if (servicecargacomprobante.CargarTexto(txtPegar.Text, rblTipoNorma.SelectedValue, ref error, (Usuario)Session["usuario"]))
        {
            if (error.Trim() != "")
            {
                VerError(error);
                return;
            }
            Actualizar();
            Label1.Visible = true;
            Label1.Text = "Sus datos han sido cargados";

            List<DetalleComprobante> lstcargacomprobante = new List<DetalleComprobante>();
            Int64 operacion = 1;
            if (rblTipoNorma.SelectedIndex == 0 || rblTipoNorma.SelectedIndex == 2)
                lstcargacomprobante = servicecargacomprobante.ConsultarCargaComprobanteDetalle(operacion, (Usuario)Session["Usuario"]);
            else
                lstcargacomprobante = servicecargacomprobante.ConsultarCargaComprobanteNiifDetalle(operacion, (Usuario)Session["Usuario"]);
            gvLista.DataSource = lstcargacomprobante;
            gvLista.DataBind();
            btnCargarComp.Visible = false;
            btnPegarComp.Visible = false;
            lblInicial.Visible = false;
            FileUploadComprobante.Visible = false;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarExportar(true);
            toolBar.eventoExportar += btnExportar_Click;
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Archivo No Valido";
            Site toolBar = (Site)this.Master;
            toolBar.MostrarExportar(true);
            toolBar.eventoExportar += btnExportar_Click;
        }        
    }

    protected void btnPegarComp_Click(object sender, EventArgs e)
    {
        mpePegar.Show();
    }

    protected void btnCancelarPegar_Click(object sender, EventArgs e)
    {
        mpePegar.Hide();
    }

    protected void btnContinuarPegar_Click(object sender, EventArgs e)
    {
        txtPegar_TextChanged(null, null);
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        List<DetalleComprobante> lstCargaComprobante = new List<DetalleComprobante>();
        lstCargaComprobante.Clear();
        
        // Limpiar archivo
        string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath(""));
        foreach (string ficheroActual in ficherosCarpeta)
            File.Delete(ficheroActual);

        // Cargando el detalle del comprobante
        int contador = 1;
        string texto = "";
        foreach (GridViewRow row in gvLista.Rows)
        {
            texto = "";
            if (row.Cells[0].Text != "" && row.Cells[0].Text != "&nbsp;")
                texto = texto + Convert.ToString(row.Cells[0].Text);
            if (row.Cells[1].Text != "" && row.Cells[1].Text != "&nbsp;")
                texto = texto + Convert.ToInt64(row.Cells[1].Text);
            texto = texto + Convert.ToString(row.Cells[2].Text);
            texto = texto + Convert.ToString(row.Cells[3].Text);
            if (row.Cells[4].Text != "" && row.Cells[4].Text != "&nbsp;")
                texto = texto + Convert.ToDecimal(row.Cells[4].Text);
            if (row.Cells[5].Text.Trim() != "" && row.Cells[5].Text.Trim() != "&nbsp;")
                texto = texto + Convert.ToString(row.Cells[5].Text);
            if (row.Cells[6].Text.Trim() != "" && row.Cells[6].Text.Trim() != "&nbsp;")
                texto = texto + Convert.ToInt64(row.Cells[6].Text);
            // Si los datos no se graban en una tabla temporal entonces completar la información faltante.
            if (!bGrabar)
            {
                if (row.Cells[12].Text.Trim() != "" && row.Cells[12].Text.Trim() != "&nbsp;")
                    texto = texto + Convert.ToString(row.Cells[12].Text);
                // Código del tercero
                if (row.Cells[7].Text.Trim() != "" && row.Cells[7].Text.Trim() != "&nbsp;")
                    texto = texto + Convert.ToInt64(row.Cells[7].Text);
                // Nombre del tercero
                if (row.Cells[8].Text.Trim() != "" && row.Cells[8].Text.Trim() != "&nbsp;")
                    texto = texto + Convert.ToString(row.Cells[8].Text);
                if (row.Cells[10].Text.Trim() != "" && row.Cells[10].Text.Trim() != "&nbsp;")
                    texto = texto + Convert.ToInt32(row.Cells[10].Text);
                if (row.Cells[11].Text.Trim() != "" && row.Cells[11].Text.Trim() != "&nbsp;")
                    texto = texto + Convert.ToInt32(row.Cells[11].Text);
            }
                        
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("") + ".txt", true);
            sw.WriteLine(texto);
            sw.Close();
            contador += 1;
        }
    }



    }