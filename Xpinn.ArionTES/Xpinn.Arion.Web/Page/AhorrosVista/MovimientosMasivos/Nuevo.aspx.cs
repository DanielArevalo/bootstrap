using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Globalization;


partial class Nuevo : GlobalWeb
{
    private Xpinn.Ahorros.Services.AhorroVistaServices ahorrosServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ahorrosServicio.CodigoProgramaMovimientos + ".id"] != null)
                VisualizarOpciones(ahorrosServicio.CodigoProgramaMovimientos, "E");
            else
                VisualizarOpciones(ahorrosServicio.CodigoProgramaMovimientos, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
           
            toolBar.MostrarConsultar(true);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaMovimientos, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                mvAhorroVista.ActiveViewIndex = 0;
                Fecha1.ToDateTime = DateTime.Now;
                
                calcular();

            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaMovimientos, "Page_Load", ex);
        }
    }

    protected void InicializargvBeneficiario()
    {
        List<AhorroVista> lstDeta = new List<AhorroVista>();
        for (int i = gvDetMovs.Rows.Count; i < 5; i++)
        {
            AhorroVista eCuenta = new AhorroVista();
            eCuenta.numero_cuenta = "";
            eCuenta.saldo_total = 0;
            //eCuenta.cod_empresa = -1;
            eCuenta.identificacion = "";
            eCuenta.nombres = "";
            eCuenta.nom_oficina = "";
            eCuenta.nom_linea = "";
            eCuenta.V_Traslado= null;
            eCuenta.valor_gmf= null;
            lstDeta.Add(eCuenta);
        }
        gvDetMovs.DataSource = lstDeta;
        gvDetMovs.DataBind();

        Session["DatosDetalle"] = lstDeta;
    }

    protected void rbTipoArchivo_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (rbTipoArchivo.SelectedItem != null)
        {
           
            VerError("");
            // Determinando el tipo de archivo y el Separador
            int tipo = 0;
            if (rbTipoArchivo.SelectedIndex == 0)
            {
                tipo = 1;
                if (rbTipoArchivo.SelectedIndex == 1) {
                    btnContinuarMen_Click( sender,  e);
                }
            }
            else
            {
                tipo = 1;
                   btnContinuarMen_Click( sender,  e);
            }
            string fic = "";
            if (TxtSaldo_Total.Text != "")
                    fic = TxtSaldo_Total.Text;
            else
            {
                VerError("Ingrese un Valor");
                return;
            }

        }  
        }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        GuardarValoresConsulta(pConsulta, ahorrosServicio.CodigoProgramaMovimientos);
        if (TxtSaldo_Total.Text == "")
        {
            VerError("Seleccione Un valor");
            return;
        }
        if (rbTipoArchivo.SelectedIndex == -1)
        {
            VerError("seleccione un retiro o deposito");
            return;
        }
        Actualizar();
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");

        TxtSaldo_Total.Text = TxtSaldo_Total.Text != "" ? TxtSaldo_Total.Text : "0";
        lblvalor_total.Text = lblvalor_total.Text != "" ? lblvalor_total.Text : "0";
       
        if (Convert.ToDecimal(TxtSaldo_Total.Text) < Convert.ToDecimal(lblvalor_total.Text))
        {
            VerError("El valor total excede el saldo total de la persona");
            return;
        }

        
            ctlMensaje.MostrarMensaje("Desea guardar los datos de el traslado de la cuenta?");
            
       
    }

    protected List<AhorroVista> ObtenerListaDetalle()
    {
        List<AhorroVista> lstBeneficiarios = new List<AhorroVista>();
        List<AhorroVista> lista = new List<AhorroVista>();

        foreach (GridViewRow rfila in gvDetMovs.Rows)
        {
            AhorroVista eBenef = new AhorroVista();

            if (rfila.Cells[1].Text != "&nbsp;")
                eBenef.numero_cuenta = rfila.Cells[1].Text;

            Label lbloficina = (Label)rfila.FindControl("lbloficina");
            if (lbloficina.Text != null)
                eBenef.nom_oficina = Convert.ToString(lbloficina.Text);

            Label lbllinea = (Label)rfila.FindControl("lbllinea");
            if (lbllinea.Text != null)
                eBenef.nom_linea = Convert.ToString(lbllinea.Text);

            Label lblidentificacion = (Label)rfila.FindControl("lblidentificacion");
            if (lblidentificacion.Text != null)
                eBenef.identificacion = Convert.ToString(lblidentificacion.Text);

            Label lblnombre = (Label)rfila.FindControl("lblnombre");
            if (lblnombre.Text != null)
                eBenef.nombres = Convert.ToString(lblnombre.Text);

            Label lblsaldo_total = (Label)rfila.FindControl("lblsaldo_total");
            if (lblsaldo_total.Text != "")
                eBenef.saldo_total = Convert.ToDecimal(lblsaldo_total.Text);

            decimalesGridRow vrTraslado = (decimalesGridRow)rfila.FindControl("vrTraslado");

                eBenef.V_Traslado = Convert.ToDecimal(vrTraslado.Text);

            decimalesGridRow txtGMF = (decimalesGridRow)rfila.FindControl("txtGMF");

                eBenef.valor_gmf = Convert.ToDecimal(txtGMF.Text);

            lista.Add(eBenef);
            Session["DatosDetalle"] = lista;

            if (eBenef.V_Traslado.Value != 0 && eBenef.numero_cuenta.Trim() != null && eBenef.nom_linea != null)
            {
                lstBeneficiarios.Add(eBenef);
            }
        }
        return lstBeneficiarios;
    }
  
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
          
            ///Hago la operacion
            VerError("");
            Usuario vUsuario = new Usuario();
            vUsuario = (Usuario)Session["Usuario"];
            Xpinn.Tesoreria.Entities.Operacion poperacion = new Xpinn.Tesoreria.Entities.Operacion();
            poperacion.cod_ope = 0;
            poperacion.tipo_ope = 76;
            poperacion.cod_caja = 0;
            poperacion.cod_cajero = 0;
            poperacion.observacion = "retiro realizado";
            poperacion.cod_proceso = null;
            poperacion.fecha_oper = Convert.ToDateTime(Fecha1.Text);
            poperacion.fecha_calc = DateTime.Now;
            poperacion.cod_ofi = vUsuario.cod_oficina;
            poperacion.cod_cliente = 0;
            
            
            ObtenerValores();
            List<AhorroVista> lstIngreso = new List<AhorroVista>();
            
            lstIngreso=ObtenerListaDetalle();

            ///Inicializo todo en una entidad ahorrovista
          foreach (GridViewRow rfila in gvDetMovs.Rows)
        {
                AhorroVista ahorro = new AhorroVista();
                AhorroVista eBenef = new AhorroVista();

                ahorro.V_Traslado = Convert.ToDecimal(lblvalor_total.Text.Replace(".", ""));
                ahorro.cod_rbTipoArchivo = Convert.ToString(rbTipoArchivo.SelectedIndex);
                ahorro.numero_cuenta = Convert.ToString(rfila.Cells[1]);

                ahorrosServicio.AplicarRetiroDeposito(ahorro, lstIngreso, poperacion, (Usuario)Session["usuario"]);
          }
            if (Fecha1.Text == "")
            {
                VerError("Ingrese la fecha de apertura");
                return;
            }

                
                //ahorrosServicio.AplicarTraslado(TxtNumeroCuenta.Text, lstCuentas, vOpe, (Usuario)Session["usuario"]);
            

            mvAhorroVista.ActiveViewIndex = 1;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(true);
            toolBar.MostrarConsultar(false);
            
           
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaMovimientos, "btnGuardar_Click", ex);
        }
    }

   
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        VerError("");
        Navegar(Pagina.Nuevo);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Ahorros.Entities.AhorroVista vAhorroVista = new Xpinn.Ahorros.Entities.AhorroVista();
            vAhorroVista = ahorrosServicio.ConsultarAhorroVista(Convert.ToString(pIdObjeto), (Usuario)Session["usuario"]); 

            if (!string.IsNullOrEmpty(vAhorroVista.cod_persona.ToString()))
               Session["COD_PERSONA"] = HttpUtility.HtmlDecode(vAhorroVista.cod_persona.ToString());

           
            if (!string.IsNullOrEmpty(vAhorroVista.fecha_apertura.ToString()))
                Fecha1.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vAhorroVista.fecha_apertura.ToString()));

            //Fecha de apertura

            if (!string.IsNullOrEmpty(vAhorroVista.saldo_total.ToString()))
                
            TxtSaldo_Total.Text = vAhorroVista.saldo_total.ToString("n0");
            //saldo total

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaMovimientos, "ObtenerDatos", ex);
        }
    }

    protected void calcular() {

        decimal valor = 0;
        decimal valo2 = 0;

        foreach (GridViewRow rfila in gvDetMovs.Rows) {

            decimalesGridRow txtTraslado = (decimalesGridRow)rfila.FindControl("txtTraslado");
            decimalesGridRow txtGMF = (decimalesGridRow)rfila.FindControl("txtGMF");

            if (txtGMF.Text == "")
            {
                txtGMF.Text = "0";
            }
            if (txtTraslado.Text == "") {
                txtTraslado.Text = "0";    
            }

            valor += Convert.ToDecimal(txtTraslado.Text);
            valo2 += Convert.ToDecimal(txtGMF.Text);        
        }
        lblvalor_total.Text = valor.ToString("n0");
       txtsaldo_igual.Text = valo2.ToString("n0");
    }

  

    protected void gvDetMovs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        {
 
            try
            {
                ConfirmarEliminarFila(e, "btnEliminar");
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(ahorrosServicio.CodigoProgramaMovimientos + "L", "gvLista_RowDataBound", ex);
            }
        }
    }

    protected void gvDetMovs_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvDetMovs.DataKeys[gvDetMovs.SelectedRow.RowIndex].Value.ToString();
        Session[ahorrosServicio.CodigoProgramaMovimientos + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvDetMovs_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvDetMovs.DataKeys[e.NewEditIndex].Value.ToString();
        Session[ahorrosServicio.CodigoProgramaMovimientos + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvDetMovs_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string conseID = Convert.ToString(gvDetMovs.DataKeys[e.RowIndex].Values[0].ToString());
        ObtenerListaDetalle();

        List<AhorroVista> LstDeta;
        LstDeta = (List<AhorroVista>)Session["DatosDetalle"];
        if (conseID != null)
        {
            try
            {
                foreach (AhorroVista Deta in LstDeta)
                {
                    if (Deta.numero_cuenta == conseID)
                    {
                        string id = Convert.ToString(e.Keys[0]);
                        if (id.Trim() != "")
                            ahorrosServicio.EliminarAhorroVista(id, (Usuario)Session["usuario"]); //OPCION 1 Eliminar detalle
                        LstDeta.Remove(Deta);
                        break;                        
                    }
                }
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
        else
        {
            foreach (AhorroVista Deta in LstDeta)
            {
                if (Deta.numero_cuenta == conseID)
                {
                    LstDeta.Remove(Deta);
                    break;
                }
            }
        }

        gvDetMovs.DataSourceID = null;
        gvDetMovs.DataBind();

        gvDetMovs.DataSource = LstDeta;
        gvDetMovs.DataBind();

        Session["DatosDetalle"] = LstDeta;
    }
    protected void gvDetMovs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvDetMovs.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaMovimientos, "gvDetMovs_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        VerError("");
        try
        {
            List<Xpinn.Ahorros.Entities.AhorroVista> lstConsulta = new List<Xpinn.Ahorros.Entities.AhorroVista>();
            lstConsulta = ahorrosServicio.ConsultarMovimientosMasivos(ObtenerValores(), (Usuario)Session["usuario"]);

            gvDetMovs.PageSize = pageSize;
            gvDetMovs.EmptyDataText = emptyQuery;
            gvDetMovs.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvDetMovs.Visible = true;
                Session["DTAhorroVista"] = lstConsulta;
                gvDetMovs.DataBind();
                ValidarPermisosGrilla(gvDetMovs);
            }
            else
            {
                gvDetMovs.Visible = false;
            }

            Session.Add(ahorrosServicio.CodigoProgramaMovimientos + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaMovimientos, "Actualizar", ex);
        }
    }

    private Xpinn.Ahorros.Entities.AhorroVista ObtenerValores()
    {
        Xpinn.Ahorros.Entities.AhorroVista vAhorroVista = new Xpinn.Ahorros.Entities.AhorroVista();
       
        if (ddlLineaAhorro.Text.Trim() != "")
            vAhorroVista.cod_linea_ahorro = Convert.ToString(ddlLineaAhorro.Text.Trim());
        //linea de ahorro

        if (TxtSaldo_Total.Text != "")
            vAhorroVista.saldo_total = Convert.ToDecimal(TxtSaldo_Total.Text.Trim());
        //Nombre oficina

   
        return vAhorroVista;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvDetMovs);
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

    protected void gvDetMovs_SelectedIndexChanged1(object sender, System.EventArgs e)
    {

    }
    protected void btnAgregar_Click(object sender, System.EventArgs e)
    {

        ObtenerListaDetalle();

        List<AhorroVista> lstDetalle = new List<AhorroVista>();

        if (Session["DatosDetalle"] != null)
        {
            lstDetalle = (List<AhorroVista>)Session["DatosDetalle"];

            for (int i = 1; i <= 1; i++)
            {
                AhorroVista eActi = new AhorroVista();
                eActi.numero_cuenta = "";
                eActi.saldo_total = 0;
                //eCuenta.cod_empresa = -1;
                eActi.identificacion = "";
                eActi.nombres = "";
                eActi.nom_oficina = "";
                eActi.nom_linea = "";
                eActi.V_Traslado = null;
                eActi.valor_gmf = null;
                lstDetalle.Add(eActi);
            }
            gvDetMovs.PageIndex = gvDetMovs.PageCount;
            gvDetMovs.DataSource = lstDetalle;
            gvDetMovs.DataBind();

            Session["DatosDetalle"] = lstDetalle;
        }
                 
    }
    
    }


    

    #region Titulares

    /// <summary>
    /// Método para instar un detalle en blanco para cuando la grilla no tiene datos
    /// </summary>
    /// <param name="consecutivo"></param>
   

    /// <summary>
    /// Método para cambio de página
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    

  



    /// <summary>
    /// Método para borrar un registro de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>


    
       
    #endregion


