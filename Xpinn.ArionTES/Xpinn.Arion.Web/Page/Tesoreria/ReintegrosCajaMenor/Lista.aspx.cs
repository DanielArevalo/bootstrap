using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Text;
using System.IO;
using System.Linq;

partial class Lista : GlobalWeb
{
    private Xpinn.Tesoreria.Services.SoporteCajService SoporteCajServicio = new Xpinn.Tesoreria.Services.SoporteCajService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(SoporteCajServicio.CodigoProgramaReintegro, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarGuardar(false);
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoporteCajServicio.CodigoProgramaReintegro, "Page_PreInit", ex);
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtFecha.ToDateTime = System.DateTime.Now;
                CargarDDL();
                CargarValoresConsulta(pConsulta, SoporteCajServicio.CodigoPrograma);
                if (Session[SoporteCajServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoporteCajServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    #region METODOS DE EVENTOS BOTONES

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        GuardarValoresConsulta(pConsulta, SoporteCajServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, SoporteCajServicio.CodigoPrograma);
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarProcesoContable(DateTime.Now, 136) == false)
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación 136 = Reintegro de Caja Menor");
            return;
        }
        Int64? rpta = 0;
        int tipoOpe = 136;
        if (!panelProceso.Visible && pnlGeneral.Visible)
        {
            rpta = ctlproceso.Inicializar(tipoOpe, DateTime.Now, Usuario);
            if (rpta > 1)
            {
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarConsultar(false);
                toolBar.MostrarLimpiar(false);
                toolBar.MostrarExportar(false);
                // Activar demás botones que se requieran
                pnlGeneral.Visible = false;
                panelProceso.Visible = true;
            }
            else
            {
                // Crear la tarea de ejecución del proceso                
                if (AplicarReintegro(tipoOpe))
                {
                    Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                }
            }
        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (gvLista.Rows.Count > 0)
            ExportToExcel(gvLista);
    }

    #endregion


    #region METODOS DEL CONTROL PROCESO CONTABLE

    private void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void btnCancelarProceso_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(gvLista.Rows.Count > 0 ? true : false);
        toolBar.MostrarConsultar(true);
        toolBar.MostrarLimpiar(true);
        toolBar.MostrarExportar(true);
        pnlGeneral.Visible = true;
        panelProceso.Visible = false;
    }

    #endregion


    #region METODOS GENERALES

    private bool AplicarReintegro(int tipoOpe)
    {
        try
        {
            Xpinn.Tesoreria.Services.AreasCajService areasCajServicio = new Xpinn.Tesoreria.Services.AreasCajService();
            Xpinn.Tesoreria.Entities.AreasCaj areasCaj = new Xpinn.Tesoreria.Entities.AreasCaj();
            List<Xpinn.Tesoreria.Entities.AreasCaj> lstAreasCaj = new List<Xpinn.Tesoreria.Entities.AreasCaj>();
            areasCaj.idarea = Convert.ToInt32(ddlArea.SelectedValue);
            lstAreasCaj = areasCajServicio.ListarAreasCaj(areasCaj, (Usuario)Session["usuario"]);

            Int64 UsuarioCaja = 0;
            Int64 UsuarioGiro= 0;
            foreach (Xpinn.Tesoreria.Entities.AreasCaj row in lstAreasCaj)
            {

                UsuarioCaja = Convert.ToInt64(row.cod_usuario);
                UsuarioGiro = Convert.ToInt64(row.cod_persona);
            }
          
            List<Xpinn.Tesoreria.Entities.SoporteCaj> lstSoportes = null;
            Xpinn.Tesoreria.Entities.Giro vGiro = null;

            // Cargando los recibos de caja de la lista
            if (gvLista.Rows.Count > 0)
            {
                lstSoportes = new List<Xpinn.Tesoreria.Entities.SoporteCaj>();
                Xpinn.Tesoreria.Entities.SoporteCaj eSoporte;
                foreach (GridViewRow row in gvLista.Rows)
                {
                    eSoporte = new Xpinn.Tesoreria.Entities.SoporteCaj();
                    eSoporte.idsoporte = Convert.ToInt64(gvLista.DataKeys[row.RowIndex].Value.ToString());
                    eSoporte = SoporteCajServicio.ConsultarSoporteCaj(eSoporte.idsoporte, Usuario);
                    lstSoportes.Add(eSoporte);
                }
            }

            //Datos de la operación
            Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion()
            {
                cod_ope = 0,
                tipo_ope = tipoOpe,
                cod_caja = 0,
                cod_cajero = 0,
                fecha_oper = DateTime.Now,
                fecha_calc = DateTime.Now,
                observacion = "Reintegro de caja menor",
                cod_proceso = ctlproceso.cod_proceso             
            };

            //Agregado para recuperar datos del giro        
            if (ValidarDatosGiro())
            {
                vGiro = ctlGiro.ConvertirEntidad(ctlGiro.ObtenerEntidadGiro(UsuarioGiro, DateTime.Now,
                    lstSoportes == null || lstSoportes.Count == 0 ? 0 : lstSoportes.Sum(x => x.valor == null ? 0 : Convert.ToDecimal(x.valor)),
                    Usuario));
                vGiro.tipo_acto = 16;
            }

            string Error = string.Empty;
            lstSoportes = SoporteCajServicio.ComprobanteSoporteCaj(lstSoportes, ref vGiro, pOperacion, ref Error, Usuario);
            if (string.IsNullOrEmpty(Error))
            {
                Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
              
                ctlproceso.CargarVariables(pOperacion.cod_ope, Convert.ToInt32(pOperacion.tipo_ope), UsuarioCaja, Usuario);
                
                Session[ComprobanteServicio.CodigoPrograma + ".idgiro"] = vGiro.idgiro;
            }
            else
            {
                VerError(Error);
                return false;
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
            return false;
        }
        return true;
    }

    private void Actualizar()
    {
        try
        {
            Site toolBar = (Site)this.Master;
            List<Xpinn.Tesoreria.Entities.SoporteCaj> lstSoportes = new List<Xpinn.Tesoreria.Entities.SoporteCaj>();
            List<Xpinn.Tesoreria.Entities.SoporteCaj> lstConsulta = SoporteCajServicio.ListarSoporteCaj(ObtenerValores(), (Usuario)Session["usuario"]);
            lstSoportes = lstConsulta.Where(x => x.vale_prov == "0" || x.vale_prov == "2").ToList();
            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstSoportes;

            if (lstSoportes.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                toolBar.MostrarGuardar(true);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                toolBar.MostrarGuardar(false);
            }

            Session.Add(SoporteCajServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoporteCajServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    protected void CargarDDL()
    {
        Xpinn.Tesoreria.Services.AreasCajService areasCajServicio = new Xpinn.Tesoreria.Services.AreasCajService();
        Xpinn.Tesoreria.Entities.AreasCaj areasCaj = new Xpinn.Tesoreria.Entities.AreasCaj();
        ddlArea.DataValueField = "idarea";
        ddlArea.DataTextField = "nombre";
        ddlArea.DataSource = areasCajServicio.ListarAreasCaj(areasCaj, (Usuario)Session["usuario"]);
        ddlArea.DataBind();

        //Agregado para inicializar control para la forma de desembolso
        ctlGiro.Inicializar();
    }

    private Xpinn.Tesoreria.Entities.SoporteCaj ObtenerValores()
    {
        Xpinn.Tesoreria.Entities.SoporteCaj vSoporteCaj = new Xpinn.Tesoreria.Entities.SoporteCaj();

        if (ddlArea.SelectedValue != null)
            if (ddlArea.SelectedValue.Trim() != "")
                vSoporteCaj.idarea = Convert.ToInt32(ddlArea.SelectedItem.Value);
        vSoporteCaj.estado = "1";
        return vSoporteCaj;
    }

    protected void ExportToExcel(GridView GridView1)
    {
        // Ajustar configuración para poder descargar
        if (gvLista.HeaderRow.Cells[4].Text.Contains("@"))
        {
            for (int i = 0; i < gvLista.Rows.Count; i++)
            {
                GridViewRow row = gvLista.Rows[i];
                row.Cells[4].Attributes.Add("style", "mso-number-format:\\@");
            }
            }
        // Exportar los datos
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=RecibosCajaMenor.xls");
        Response.Charset = "UTF-8";
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


    public bool ValidarDatosGiro()
    {
        //Validando datos del control de Giro
        if (ctlGiro.IndiceFormaDesem == 0)
        {
            VerError("Seleccione una forma de desembolso");
            return false;
        }
        else
        {
            if (ctlGiro.IndiceFormaDesem == 2 || ctlGiro.IndiceFormaDesem == 3)
            {
                if (ctlGiro.IndiceEntidadOrigen == 0)
                {
                    VerError("Seleccione un Banco de donde se girará");
                    return false;
                }
                if (ctlGiro.IndiceFormaDesem == 3)
                {
                    if (ctlGiro.IndiceEntidadDest == 0)
                    {
                        VerError("Seleccione la Entidad de destino");
                        return false;
                    }
                    if (ctlGiro.TextNumCuenta == "")
                    {
                        VerError("Ingrese el número de la cuenta");
                        return false;
                    }
                }
            }
        }
        return true;
    }

    #endregion
}