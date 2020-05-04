using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Xpinn.Util;
using System.Web.UI.HtmlControls;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;
using System.IO;
using System.Text;

partial class Lista : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.PagoOrdenesService ordenesServicio = new Xpinn.FabricaCreditos.Services.PagoOrdenesService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ordenesServicio.CodigoPrograma, "L");
            Site toolBar = (Site)this.Master;
           
            toolBar.eventoGuardar += btnGuardar_Click;           
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.MostrarGuardar(false);
            mvAhorroVista.ActiveViewIndex = 0;


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ordenesServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarlistas();
                ctlGiros.Visible = false;
                TXTmONTO.Visible = false;
                ctlGiros.Inicializar();
                txtAprobacion_fin.ToDateTime = DateTime.Now;
                mvAhorroVista.ActiveViewIndex = 0;
                // Inicializar datos para orden de servicio
                ctlBusquedaProveedor.cargar = "0";
                ctlBusquedaProveedor.VisibleCtl = true;
              
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ordenesServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs E)
    {

        VerError("");
        if (ctlGiros.ValueFormaDesem != "1")
        {
            if (ctlGiros.ValueCuentaOrigen == "")
            {
                VerError("Ingrese los datos del giro");
                return;
            }
        }
        ctlMensaje.MostrarMensaje("Desea grabar las ordenes de servicio?");

    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, ordenesServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        gvLista.Visible = false;
        lblTotalRegs.Text = ("");

        LimpiarValoresConsulta(pConsulta, ordenesServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnEliminar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ordenesServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[ordenesServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[ordenesServicio.CodigoPrograma + ".id"] = id;
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
            BOexcepcion.Throw(ordenesServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }



    private void Actualizar()
    {
        VerError("");
        try
        {
            string pFiltro = obtFiltro();
            List<Xpinn.FabricaCreditos.Entities.PagoOrdenes> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.PagoOrdenes>();
            lstConsulta = ordenesServicio.ConsultarPagoOrdenes(pFiltro, (Usuario)Session["usuario"]);

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

               

                TXTmONTO.Visible = true;
                ctlGiros.Visible = true;
                btnExportar.Visible = true;
                labelgiro.Visible = true;
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                TXTmONTO.Visible = false;
                ctlGiros.Visible = false;
                btnExportar.Visible = false;
                labelgiro.Visible = false;          
            }
            
           
            Session.Add(ordenesServicio.CodigoPrograma + ".consulta", 1);
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);

            //VisualizarOpciones(ordenesServicio.CodigoPrograma, "L");
            //Site toolBar = (Site)this.Master;
            //toolBar.eventoGuardar += btnGuardar_Click;
              
            Configuracion conf = new Configuracion();
            decimal total = 0;
            decimal calcula = 0;
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                calcula = Convert.ToDecimal(rFila.Cells[12].Text.Replace(conf.ObtenerSeparadorMilesConfig(), ""));
                total += calcula;
            }
            TXTmONTO.Text = Convert.ToString(total);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ordenesServicio.CodigoPrograma, "Actualizar", ex);
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {

        VerError("");
        Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
        Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
        try
        {
            // Validar el proveedor del giro
            Usuario usuap = new Usuario();
            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 ePersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            ePersona.seleccionar = "Identificacion";
            ePersona.identificacion = txtIdentificacionprov.Text;
            ePersona = personaServicio.ConsultarPersona1Param(ePersona, (Usuario)Session["usuario"]);
            if (ePersona == null)
                VerError("El codigo de la persona no existe");

            // CARGAR LAS ORDENES DE PAGO DEL PROVEEDOR MARCADAS
            decimal valorGiro = 0;
            List<PagoOrdenes> lstOrdenes = new List<PagoOrdenes>();
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox chbcreacion = (CheckBox)rFila.FindControl("chbcreacion");
                if (chbcreacion != null)
                {
                    if (chbcreacion.Checked)
                    {
                        ///datos de la orden
                        PagoOrdenes eOrden = new PagoOrdenes();
                        eOrden.idordenservicio = Convert.ToInt64(rFila.Cells[1].Text);
                        eOrden.numero_radicacion = Convert.ToInt64(rFila.Cells[6].Text);
                        eOrden.valor = Convert.ToInt64(rFila.Cells[12].Text.Replace(gSeparadorMiles, ""));
                        eOrden.estado = "1";
                        valorGiro = +eOrden.valor;
                        lstOrdenes.Add(eOrden);
                    }
                }
            }

            // VERIFICAR QUE SE MARCARON LAS ORDENES
            if (lstOrdenes.Count() <= 0)
            {
                VerError("No se seleccionaron ordenes de pago para girar");
                return;
            }

            //GRABACION DEL GIRO A REALIZAR
            string Error = "";
            Usuario pusu = (Usuario)Session["usuario"];
            Xpinn.FabricaCreditos.Entities.Giro pGiro = new Xpinn.FabricaCreditos.Entities.Giro();
            pGiro.idgiro = 0;
            pGiro.cod_persona = Convert.ToInt64(ePersona.cod_persona);
            pGiro.forma_pago = Convert.ToInt32(ctlGiros.ValueFormaDesem);
            pGiro.tipo_acto = 10;
            pGiro.fec_reg = Convert.ToDateTime(txtAprobacion_fin.Texto);
            pGiro.fec_giro = DateTime.Now;
            pGiro.usu_gen = pusu.nombre;
            pGiro.usu_apli = null;
            pGiro.estadogi = 0;
            pGiro.usu_apro = null;
            pGiro.identificacion = txtIdentificacionprov.Text;
            pGiro.cod_banco = ctlGiros.IndiceEntidadOrigen;
            pGiro.num_comp = 0;
            pGiro.numero_radicacion = 0;
            pGiro.valor = Convert.ToInt64(TXTmONTO.Text.Replace(gSeparadorMiles, ""));
            pGiro.num_cuenta = txtIdentificacionprov.Text;
            if (ctlGiros.ValueCuentaOrigen != "")
                pGiro.idctabancaria = Convert.ToInt64(ctlGiros.ValueCuentaOrigen);
            if (ordenesServicio.CrearPagoOrdenes(lstOrdenes, pGiro, ref Error, (Usuario)Session["usuario"]))
            {
                mvAhorroVista.ActiveViewIndex = 1;
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarConsultar(false);
                toolBar.MostrarLimpiar(false);
            }
            else
            {
                VerError(Error);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ordenesServicio.CodigoPrograma, "btnContinuarMen_Click", ex);
        }


    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvLista);
    }

    protected void ExportToExcel(GridView GridView1)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=OrdenesServicio.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.Default;
        StringWriter sw = new System.IO.StringWriter();
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

    protected void txtIdentificacionprov_TextChanged(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            Persona1 pData = new Persona1();
            Persona1Service PersonaService = new Persona1Service();
            if (txtIdentificacionprov.Text != "")
            {
                pData.seleccionar = "Identificacion";
                pData.noTraerHuella = 1;
                pData.identificacion = txtIdentificacionprov.Text;
                pData = PersonaService.ConsultarPersona1Param(pData, (Usuario)Session["usuario"]);
                if (pData.nombres != null || pData.apellidos != null)
                {
                    string nombre = "", apellidos = "";
                    nombre = pData.nombres != null ? pData.nombres.Trim() : "";
                    apellidos = pData.apellidos != null ? pData.apellidos.Trim() : "";
                    txtNombreProveedor.Text = (nombre + " " + apellidos).Trim();
                }
                else
                {
                    txtNombreProveedor.Text = "";
                    VerError("Debe ingresar una identificación existente.");
                }
            }
            else
            {
                txtNombreProveedor.Text = "";
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void btnListadoPersona_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtIdentificacionprov", "txtNombreProveedor");
    }

    private string obtFiltro()
    {
        Configuracion conf = new Configuracion();
        String filtro = String.Empty;

        if (txtIdentificacionprov.Text.Trim() != "")
            filtro += " AND P.IDENTIFICACION = '" + txtIdentificacionprov.Text + "'";
        if (ddloficina.Visible == true)
            if (ddloficina.SelectedIndex != 0)
                filtro += " AND C.COD_OFICINA = " + ddloficina.SelectedValue + "";
        if (txtFechaInicial.TieneDatos)
            if (ordenesServicio.TipoConexion((Usuario)Session["Usuario"]) == "ORACLE")
                filtro += " AND C.FECHA_DESEMBOLSO >= TO_DATE('" + txtFechaInicial.ToDateTime.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
            else
                filtro += " AND C.FECHA_DESEMBOLSO >= '" + txtFechaInicial.ToDateTime.ToString(conf.ObtenerFormatoFecha()) + "' ";
        if (txtFechaFinal.TieneDatos)
            if (ordenesServicio.TipoConexion((Usuario)Session["Usuario"]) == "ORACLE")
                filtro += " AND C.FECHA_DESEMBOLSO <= TO_DATE('" + txtFechaFinal.ToDateTime.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
            else
                filtro += " AND C.FECHA_DESEMBOLSO <= '" + txtFechaFinal.ToDateTime.ToString(conf.ObtenerFormatoFecha()) + "' ";


        if (ctlBusquedaProveedor.TextIdentif != "")
            filtro += " AND S.IDPROVEEDOR = " + ctlBusquedaProveedor.TextIdentif + "";

        return filtro;
    }

    protected void chbcreacion_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            decimal SumSaldo = 0;
            if (gvLista.Rows.Count > 0)
            {
                foreach (GridViewRow rFila in gvLista.Rows)
                {
                    CheckBoxGrid chbcreacion = (CheckBoxGrid)rFila.FindControl("chbcreacion");
                    if (chbcreacion != null)
                        if (chbcreacion.Checked)
                            SumSaldo = SumSaldo + Convert.ToDecimal(rFila.Cells[12].Text.Replace("$", "").Replace(gSeparadorMiles, ""));
                }
            }
            TXTmONTO.Text = SumSaldo.ToString("n0");
        }
        catch
        { }
    }

    protected void cargarlistas()
    {
        Xpinn.FabricaCreditos.Services.OficinaService oficinaService = new Xpinn.FabricaCreditos.Services.OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
        Usuario usuap = (Usuario)Session["usuario"];

        try
        {
            int cod = Convert.ToInt32(usuap.codusuario);
            int consulta = 0;
            consulta = oficinaService.UsuarioPuedeConsultarCreditosOficinas(cod, (Usuario)Session["Usuario"]);
            ddloficina.Visible = true;
            lbloficina.Visible = true;
            if (consulta >= 1)
            {
                ddloficina.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);
                ddloficina.DataTextField = "nombre";
                ddloficina.DataValueField = "codigo";
                ddloficina.DataBind();
                ddloficina.SelectedValue = Convert.ToString(usuap.cod_oficina);
                ddloficina.Enabled = true;
                ddloficina.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            }
            else
            {
                ddloficina.Items.Insert(1, new ListItem(Convert.ToString(usuap.nombre_oficina), Convert.ToString(usuap.cod_oficina)));
                ddloficina.DataBind();
                ddloficina.SelectedValue = Convert.ToString(usuap.cod_oficina);
                ddloficina.Enabled = false;
                ddloficina.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            }
        }
        catch
        {
            ddloficina.Visible = false;
            lbloficina.Visible = false;
        }
    }
}