using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Linq;
using Xpinn.Util;
using Xpinn.Cartera.Services;
using Xpinn.Cartera.Entities;

public partial class Lista : GlobalWeb
{
    ProvisionService ProvisionService = new ProvisionService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ProvisionService.CodigoProgramaProv, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProvisionService.CodigoProgramaProv, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvAplicar.ActiveViewIndex = 0;
                CargarDropDown();
                panelGrid.Visible = false;
                Session["lstProvision"] = null;
                CargarValoresConsulta(pConsulta, ProvisionService.CodigoProgramaProv);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProvisionService.CodigoProgramaProv, "Page_Load", ex);
        }
    }

    protected void CargarDropDown()
    {
        Configuracion conf = new Configuracion();
        Xpinn.Confecoop.Services.ConfecoopService ConfecoopServ = new Xpinn.Confecoop.Services.ConfecoopService();

        string tipo, estado;
        tipo = "S";
        estado = "D";
        ddlFechaCorte.DataSource = ConfecoopServ.ListarFechaCierreGLOBAL(tipo, estado, (Usuario)Session["usuario"]);
        ddlFechaCorte.DataTextField = "fecha";
        ddlFechaCorte.DataTextFormatString = "{0:" + gFormatoFecha + "}";
        ddlFechaCorte.DataValueField = "fecha";
        ddlFechaCorte.DataBind();

        Xpinn.FabricaCreditos.Services.OficinaService oficinaServicio = new Xpinn.FabricaCreditos.Services.OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
        oficina.estado = 1;
        ddloficina.DataTextField = "nombre";
        ddloficina.DataValueField = "codigo";
        ddloficina.DataSource = oficinaServicio.ListarOficinas(oficina, (Usuario)Session["usuario"]);
        ddloficina.DataBind();

        Xpinn.FabricaCreditos.Services.LineasCreditoService BOLinea = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        List<Xpinn.FabricaCreditos.Entities.LineasCredito> lstLineas = new List<Xpinn.FabricaCreditos.Entities.LineasCredito>();
        Xpinn.FabricaCreditos.Entities.LineasCredito pEntidad = new Xpinn.FabricaCreditos.Entities.LineasCredito();
        pEntidad.estado = 1;
        lstLineas = BOLinea.ListarLineasCredito(pEntidad, (Usuario)Session["usuario"]);
        if (lstLineas.Count > 0)
        {
            ddlLinea.DataSource = lstLineas;
            ddlLinea.DataTextField = "nom_linea_credito";
            ddlLinea.DataValueField = "cod_linea_credito";
            ddlLinea.DataBind();
        }

        Xpinn.Cartera.Services.ClasificacionCarteraService BOCartera = new Xpinn.Cartera.Services.ClasificacionCarteraService();
        List<Xpinn.Cartera.Entities.ClasificacionCartera> lstCategoria = new List<Xpinn.Cartera.Entities.ClasificacionCartera>();
        lstCategoria = BOCartera.ListarCategorias((Usuario)Session["usuario"]);
        if (lstCategoria.Count > 0)
        {
            ddlCategoria.DataSource = lstCategoria;
            ddlCategoria.DataTextField = "categoria";
            ddlCategoria.DataValueField = "categoria";
            ddlCategoria.DataBind();
        }
    }

    /// <summary>
    /// Método que permite generar el reporte segùn las condiciones dadas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Page.Validate();
        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, ProvisionService.CodigoProgramaProv);
            Actualizar();
        }
    }



    void Limpiar()
    {
        LimpiarValoresConsulta(pConsulta, ProvisionService.CodigoProgramaProv);
        panelGrid.Visible = false;
        gvLista.DataSource = null;
        gvLista.DataBind();
        lblTotalRegs.Text = "";
        ddlFechaCorte.SelectedIndex = 0;
        VerError("");
        mvAplicar.ActiveViewIndex = 0;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        toolBar.MostrarLimpiar(true);
        toolBar.MostrarConsultar(true);
        toolBar.MostrarCancelar(false);
    }
    /// <summary>
    /// Méotodo para limpiar datos de la conuslta
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        Limpiar();
    }
    
   
    public string obtFiltro()
    {
        string sFiltro = "";

        if (ddlFechaCorte.SelectedItem != null)
        {
            dbConnectionFactory = new ConnectionDataBase();
            if (dbConnectionFactory.TipoConexion() == "ORACLE")
            {
                sFiltro += " and H.FECHA_HISTORICO = TO_DATE('" + ddlFechaCorte.SelectedItem.Text + "','" + gFormatoFecha + "')";
            }
            else
            {
                sFiltro += " and H.FECHA_HISTORICO = '" + ddlFechaCorte.SelectedItem.Text + "'";
            }
        }
        
        if (ddloficina.SelectedText != "")
            sFiltro += " and H.cod_oficina In ( " + ddloficina.SelectedValue + ")";

        if (ddlLinea.SelectedText != "")
        {
            sFiltro += " and H.cod_linea_Credito In (";
            string[] sLineas = ddlLinea.SelectedText.Split(',');
            for (int i = 0; i < sLineas.Count(); i++)
            {
                string[] sDato = sLineas[i].ToString().Split('-');
                if (i > 0)
                    sFiltro += ", ";
                sFiltro += "'" + sDato[0].Trim() + "'";
            }
            sFiltro += ") ";
        }

        if (ddlCategoria.SelectedText != "")
        {
            sFiltro += " and H.COD_CATEGORIA In (";
            string[] sCateg = ddlCategoria.SelectedValue.Split(',');
            for (int i = 0; i < sCateg.Count(); i++)
            {
                string[] sDato = sCateg[i].ToString().Split('-');
                if (i > 0)
                    sFiltro += ", ";
                sFiltro += "'" + sDato[0].Trim() + "'";
            }
            sFiltro += ") ";
        }

        if (txtNumRadicacion.Text != "")
            sFiltro += " and H.NUMERO_RADICACION = " + txtNumRadicacion.Text;

        if(txtIdentificacion.Text != "")
            sFiltro += " and V.IDENTIFICACION = '" + txtIdentificacion.Text.Trim() + "'";

        if (!string.IsNullOrEmpty(sFiltro))
        {
            sFiltro = sFiltro.Substring(4);
            sFiltro = " WHERE " + sFiltro;
        }
        return sFiltro;
    }

    /// <summary>
    /// Método para traer los datos según las condiciones ingresadas
    /// </summary>
    private void Actualizar()
    {
        VerError("");
        try
        {
            // Determinar los filtros
            string pFiltro = "";
            pFiltro = obtFiltro();

            // Determinar el còdigo del usuario y determinar si el usuario es un ejecutivo      
            Usuario usuap = (Usuario)Session["usuario"];

            List<Provision> lstConsulta = new List<Provision>();
            lstConsulta = ProvisionService.ListarProvisiones(pFiltro, (Usuario)Session["usuario"]);
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            Site toolBar = (Site)this.Master;
            if (lstConsulta.Count > 0)
            {
                panelGrid.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count;
                gvLista.DataBind();
                toolBar.MostrarGuardar(true);
            }
            else
            {
                lblTotalRegs.Text = "";
                panelGrid.Visible = false;
                toolBar.MostrarGuardar(false);
            }
            Session.Add(ProvisionService.CodigoProgramaProv + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProvisionService.CodigoProgramaProv, "Actualizar", ex);
        }
    }

    protected Boolean ValidarDatos()
    {
        Validadores Validacion = new Validadores();
        Xpinn.Comun.Services.CiereaService CiereaServ = new Xpinn.Comun.Services.CiereaService();
        List<Xpinn.Comun.Entities.Cierea> lstCierea = new List<Xpinn.Comun.Entities.Cierea>();
        string pFiltro = " WHERE TIPO = 'S' AND ESTADO = 'D' aND cAMPO1 !=' ' AND FECHA = TO_DATE('" + ddlFechaCorte.SelectedItem.Text + "','" + gFormatoFecha + "') ";
        lstCierea = CiereaServ.ConsultaGeneralCierea(pFiltro, (Usuario)Session["usuario"]);
        if (lstCierea.Count > 0)
        {
            foreach (Xpinn.Comun.Entities.Cierea nData in lstCierea)
            {
                if (Validacion.IsValidNumber(nData.campo1))
                {
                    if (nData.campo1 != null)
                    {
                        VerError("No se puede realizar las modificaciones debido a que ya se genero el comprobante de provisiones para la fecha de corte seleccionada.");
                        return false;
                    }
                }                
            }
        }

        if (gvLista.Rows.Count == 0)
        {
            VerError("No existen datos por modificar, Verifique los datos.");
            return false;
        }
        else
        {
            int cont = 0;
            List<Provision> lstProvision = new List<Provision>();
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                string IdProvision = "";
                IdProvision = gvLista.DataKeys[rFila.RowIndex].Value.ToString();
                if (IdProvision != "&nbsp;" && IdProvision != "")
                {
                    Label lblVrProvision = (Label)rFila.FindControl("lblVrProvision");
                    decimalesGridRow txtVrProvision = (decimalesGridRow)rFila.FindControl("txtVrProvision");
                    lblVrProvision.Text = lblVrProvision.Text != "" ? lblVrProvision.Text.Replace(".", "") : "0";
                    txtVrProvision.Text = txtVrProvision.Text != "" ? txtVrProvision.Text.Replace(".", "") : "0";
                    if (lblVrProvision.Text != "" && txtVrProvision.Text != "")
                    {
                        if (lblVrProvision.Text != txtVrProvision.Text)
                        {
                            cont++;
                            //SE ADICIONA A LA LISTA PARA MODIFICAR PORSTERIORMENTE
                            Provision pEntidad = new Provision();
                            pEntidad.idprovision = Convert.ToInt32(IdProvision);
                            pEntidad.fecha_corte = Convert.ToDateTime(ddlFechaCorte.SelectedItem.Text);
                            pEntidad.valor_provision = Convert.ToDecimal(txtVrProvision.Text);
                            pEntidad.vr_provision_anterior = Convert.ToDecimal(lblVrProvision.Text);
                            lstProvision.Add(pEntidad);
                        }
                    }
                }
            }
            if (cont == 0)
            {
                VerError("No se realizaron cambios en los registros.");
                return false;
            }
            Session["lstProvision"] = lstProvision;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        try
        {
            if (ValidarDatos())
                ctlMensaje.MostrarMensaje("Desea realizar la modificación de los registros editados?");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProvisionService.CodigoProgramaProv, "btnGuardar_Click", ex);
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["lstProvision"] != null)
            {
                List<Provision> lstProvision = new List<Provision>();
                lstProvision = (List<Provision>)Session["lstProvision"];

                string pError = "";
                ProvisionService.ModificacionProvision(lstProvision, ref pError, (Usuario)Session["usuario"]);
                if (pError != "")
                {
                    VerError(pError);
                    return;
                }
                lblmsj.Text = lstProvision.Count.ToString();
                Session.Remove("lstProvision");
                mvAplicar.ActiveViewIndex = 1;
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarLimpiar(false);
                toolBar.MostrarConsultar(false);
                toolBar.MostrarCancelar(true);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProvisionService.CodigoProgramaProv, "btnContinuarMen_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Limpiar();
    }
    

}
