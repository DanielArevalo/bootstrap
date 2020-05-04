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
using System.Reflection;
using System.IO;


partial class Lista : GlobalWeb
{
    CuentasPorPagarService CuentaService = new CuentasPorPagarService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {       
            VisualizarOpciones(CuentaService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoExportar += btnExportar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarDropdown();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoPrograma, "Page_Load", ex);
        }
    }

    bool validarIngresoDefechas()
    {
        if (txtIngresoIni.Text != "" && txtIngresoFin.Text != "")
        {
            if (Convert.ToDateTime(txtIngresoIni.Text) > Convert.ToDateTime(txtIngresoFin.Text))
            {
                VerError("Datos erroneos en las Fechas de Ingreso.");
                return false;
            }
        }

        if (txtVencimientoIni.Text != "" && txtVencimientoFin.Text != "")
        {
            if (Convert.ToDateTime(txtVencimientoIni.Text) > Convert.ToDateTime(txtVencimientoFin.Text))
            {
                VerError("Datos erroneos en las Fechas de Vencimiento.");
                return false;
            }
        }
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
        LlenarListasDesplegables(TipoLista.TipoCuentasXpagar, ddlTipoCuenta);

       ddlTipoCuenta.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        //ddlTipoCuenta.Items.Insert(1, new ListItem("Factura", "1"));
        //ddlTipoCuenta.Items.Insert(2, new ListItem("Orden de Pago", "2"));
        //ddlTipoCuenta.Items.Insert(3, new ListItem("Orden de Compra", "3"));
        //ddlTipoCuenta.Items.Insert(4, new ListItem("Orden de Servicio", "4"));
        ///ddlTipoCuenta.Items.Insert(5, new ListItem("Contrato de Servicio", "5"));


        ddlEstado.Items.Insert(0, new ListItem("Seleccione un item","0"));
        ddlEstado.Items.Insert(1, new ListItem("Pendiente", "1"));
        ddlEstado.Items.Insert(2, new ListItem("Pagado", "2"));
        ddlEstado.Items.Insert(3, new ListItem("Anulado", "3"));
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
            BOexcepcion.Throw(CuentaService.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;         
        Session[CuentaService.CodigoPrograma + ".id"] = id;
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
            CuentaService.EliminarCuentasXpagar(Convert.ToInt32(Session["ID"]), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }

    
    private void Actualizar()
    {
        try
        {
            List<CuentasPorPagar> lstConsulta = new List<CuentasPorPagar>();
            String filtro = obtFiltro(ObtenerValores());
            DateTime pFechaIni, pFechaFin, pVencIni, pVencFin;
            pFechaIni = txtIngresoIni.ToDateTime == null ? DateTime.MinValue : txtIngresoIni.ToDateTime;
            pFechaFin = txtIngresoFin.ToDateTime == null ? DateTime.MinValue : txtIngresoFin.ToDateTime;
            pVencIni = txtVencimientoIni.ToDateTime == null ? DateTime.MinValue : txtVencimientoIni.ToDateTime;
            pVencFin = txtVencimientoFin.ToDateTime == null ? DateTime.MinValue : txtVencimientoFin.ToDateTime;

            lstConsulta = CuentaService.ListarCuentasXpagar(ObtenerValores(), pFechaIni, pFechaFin,pVencIni,pVencFin, (Usuario)Session["usuario"], filtro);
            Session["CuentasXpagar"]= lstConsulta;


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

            Session.Add(CuentaService.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoPrograma, "Actualizar", ex);
        }
    }

    private CuentasPorPagar ObtenerValores()
    {
        CuentasPorPagar vCuentas = new CuentasPorPagar();
        if (txtCodigo.Text.Trim() != "")
            vCuentas.codigo_factura = Convert.ToInt32(txtCodigo.Text.Trim());
        if (txtNumFact.Text.Trim() != "")
            vCuentas.numero_factura = txtNumFact.Text.Trim();
       
        if (ddlTipoCuenta.SelectedValue != "0")
            vCuentas.idtipo_cta_por_pagar = Convert.ToInt32(ddlTipoCuenta.SelectedValue.Trim());
        if (txtIdProveedor.Text.Trim() != "")
            vCuentas.cod_persona= Convert.ToInt64(txtIdProveedor.Text.Trim());
        if (txtNombre.Text.Trim() != "")
            vCuentas.nombre = txtNombre.Text.Trim().ToUpper();

        return vCuentas;
    }

   
   
    private string obtFiltro(CuentasPorPagar Cuentas)
    {
        Configuracion conf = new Configuracion();

        String filtro = String.Empty;

        if (txtCodigo.Text.Trim() != "")
            filtro += " and c.CODIGO_FACTURA = " + Cuentas.codigo_factura;
        if (txtNumFact.Text.Trim() != "")
            filtro += " and c.numero_factura like '%" + Cuentas.numero_factura+"%'";       
        if (ddlTipoCuenta.SelectedValue != "0")
            filtro += " and c.IDTIPO_CTA_POR_PAGAR = " + Cuentas.idtipo_cta_por_pagar;
        if (txtIdProveedor.Text.Trim() != "")
            filtro += " and c.COD_PERSONA = " + Cuentas.cod_persona;
        if (txtNombre.Text.Trim() != "")
            filtro += " and p.primer_nombre | | p.segundo_nombre | | p.primer_apellido | | p.segundo_apellido like '%" + Cuentas.nombre + "%'";
        if (ddlEstado.SelectedIndex != 0)
            filtro += " and c.estado = " + ddlEstado.SelectedValue;

        return filtro;
    }


    protected void btnExportar_Click(object sender, EventArgs e)
    {
        List<Xpinn.Tesoreria.Entities.CuentasPorPagar> lstConsulta = (List<Xpinn.Tesoreria.Entities.CuentasPorPagar>)Session["CuentasXpagar"];
        if (Session["CuentasXpagar"] != null)
        {
            string fic = "CuentasX-pagar.csv";
            try
            {
                File.Delete(fic);
            }
            catch
            {
            }
            // Generar el archivo
            bool bTitulos = false;
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("") + fic, true);
            foreach (Xpinn.Tesoreria.Entities.CuentasPorPagar item in lstConsulta)
            {
                string texto = "";
                FieldInfo[] propiedades = item.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                if (!bTitulos)
                {
                    foreach (FieldInfo f in propiedades)
                    {
                        try
                        {
                            texto += f.Name.Split('>').First().Replace("<", "") + ";";
                        }
                        catch { texto += ";"; };
                    }
                    sw.WriteLine(texto);
                    bTitulos = true;
                }
                texto = "";
                int i = 0;
                foreach (FieldInfo f in propiedades)
                {
                    i += 1;
                    object valorObject = f.GetValue(item);
                    // Si no soy nulo
                    if (valorObject != null)
                    {
                        string valorString = valorObject.ToString();
                        if (valorObject is DateTime)
                        {
                            DateTime? fechaValidar = valorObject as DateTime?;
                            if (fechaValidar.Value != DateTime.MinValue)
                            {
                                texto += f.GetValue(item) + ";";
                            }
                            else
                            {
                                texto += "" + ";";
                            }
                        }
                        else
                        {
                            texto += f.GetValue(item) + ";";
                            texto.Replace("\r", "").Replace(";", "");
                        }
                    }
                    else
                    {
                        texto += "" + ";";
                    }
                }
                sw.WriteLine(texto);
            }
            sw.Close();
            System.IO.StreamReader sr;
            sr = File.OpenText(Server.MapPath("") + fic);
            string texo = sr.ReadToEnd();
            sr.Close();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "text/plain";
            HttpContext.Current.Response.Write(texo);
            HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + fic);
            HttpContext.Current.Response.Flush();
            File.Delete(Server.MapPath("") + fic);
            HttpContext.Current.Response.End();

        }
    }



}