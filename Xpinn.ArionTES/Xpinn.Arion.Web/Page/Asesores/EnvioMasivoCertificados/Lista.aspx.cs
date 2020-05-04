using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Microsoft.Reporting.WebForms;
using System.Web;
using System.Threading.Tasks;

public partial class Lista : GlobalWeb
{
    ExtractoService clienteExtractoServicio = new ExtractoService();

    #region Eventos Iniciales
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones("110127", "L");
            Site toolBar = (Site)this.Master;
            ctlMensaje.eventoClick += btnContinuar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.MostrarCancelar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("110127", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                mvPrincipal.ActiveViewIndex = 0;
                txtFechaGeneracion.Text = DateTime.Now.ToShortDateString();
                btnEnviar.Visible = false;
                panelLista.Visible = false;
                mpeProcesando.Hide();
                mpeFinal.Hide();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("110127", "Page_Load", ex);
        }
    }
    #endregion

    #region Eventos de Botones
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            mvPrincipal.SetActiveView(ViewProceso);
            mpeNuevo.Show();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("EnvioMasivo", "btnEnviar_Click", ex);
        }
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidarDatos())
            {
                IniciarProceso();
                Task.Factory.StartNew(() => { EnviarCorreos(); });
                List<Xpinn.Aportes.Entities.ErroresCargaAportes> lstErrores = new List<Xpinn.Aportes.Entities.ErroresCargaAportes>();
                lstErrores = (List<Xpinn.Aportes.Entities.ErroresCargaAportes>)Session["lsterrores"];
                if (lstErrores != null && lstErrores.Count() > 0)
                {
                    mvPrincipal.SetActiveView(ViewErrores);
                    pErrores.Visible = true;
                    pErroresG.Visible = true;
                    gvErrores.DataSource = lstErrores;
                    gvErrores.DataBind();
                    cpeDemo.CollapsedText = "(Click Aqui para ver " + lstErrores.Count() + " errores...)";
                    cpeDemo.ExpandedText = "(Click Aqui para ocultar listado de errores...)";
                }
                
            }
        }
        catch (Exception ex)
        {
            VerError("Se ha producido un error" + ex.Message);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        mpeNuevo.Hide();
        mpeProcesando.Hide();
        mvPrincipal.SetActiveView(ViewListado);
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        mvPrincipal.SetActiveView(ViewListado);
    }

    protected void btnSalir_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/General/Global/inicio.aspx");
    }
    #endregion

    #region Métodos de proceso
    public void IniciarProceso()
    {
        mvPrincipal.SetActiveView(ViewProceso);
        btnContinuar.Enabled = false;
        btnCancelar.Enabled = false;
        mpeNuevo.Hide();
        mpeProcesando.Show();
        Image1.Visible = true;
        Session["Proceso"] = "INICIO";
        Timer1.Enabled = true;
        Site toolbar = (Site)this.Master;
        toolbar.MostrarConsultar(false);
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        if (Session["Proceso"] != null)
            if (Session["Proceso"].ToString() == "FINAL")
                TerminarProceso();
            else
                mpeProcesando.Show();
        else
            mpeProcesando.Hide();
    }
    public void TerminarProceso()
    {
        mpeProcesando.Hide();
        Image1.Visible = false;
        pProcesando.Visible = false;
        Session.Remove("Proceso");
        Timer1.Enabled = false;
        mpeFinal.Show();
        if (Session["Error"] != null)
        {
            if (Session["Error"].ToString().Trim() != "")
                lblError.Text = Session["Error"].ToString();
            Session.Remove("Error");
        }
    }

    #endregion

    private List<Extracto> ObtenerListado()
    {
        List<Extracto> lstAsociados = new List<Extracto>();
        foreach (GridViewRow fila in gvLista.Rows)
        {
            CheckBox chkEnviar = (CheckBox)fila.FindControl("chkEnviar");
            Extracto pExtracto;
            if (chkEnviar.Checked)
            {
                pExtracto = new Extracto();
                pExtracto.cod_persona = Convert.ToInt64(fila.Cells[1].Text);
                pExtracto.identificacion = fila.Cells[2].Text;
                pExtracto.tipo_identificacion = fila.Cells[3].Text;
                pExtracto.nombres = fila.Cells[4].Text;
                pExtracto.email = fila.Cells[5].Text;
                pExtracto.tipo_persona = fila.Cells[6].Text;
                pExtracto.fec_prox_pago = Convert.ToDateTime(fila.Cells[7].Text);

                lstAsociados.Add(pExtracto);
            }
        }

        if (lstAsociados.Count > 0)
            return lstAsociados;
        else return null;
    }

    protected bool ValidarDatos()
    {
        TiposDocCobranzasServices _tipoDocumentoServicio = new TiposDocCobranzasServices();
        Usuario pUsuario = (Usuario)Session["usuario"];
        Xpinn.Comun.Entities.Empresa empresa = _tipoDocumentoServicio.ConsultarCorreoEmpresa(pUsuario.idEmpresa, pUsuario);
        ParametroCorreo parametroCorreo = (ParametroCorreo)Enum.Parse(typeof(ParametroCorreo), ((int)TipoDocumentoCorreo.EstadoCuenta).ToString());
        TiposDocCobranzas modificardocumento = _tipoDocumentoServicio.ConsultarFormatoDocumentoCorreo((int)parametroCorreo, pUsuario);

        if (string.IsNullOrWhiteSpace(empresa.e_mail) || string.IsNullOrWhiteSpace(empresa.clave_e_mail))
        {
            VerError("La empresa no tiene configurado un email para enviar el correo");
            mvPrincipal.SetActiveView(ViewListado);
            return false;
        }
        else if (string.IsNullOrWhiteSpace(modificardocumento.texto))
        {
            VerError("No esta parametrizado el formato del correo a enviar");
            mvPrincipal.SetActiveView(ViewListado);
            return false;
        }
        else
        {
            List<Extracto> lstClientes = new List<Extracto>();
            lstClientes = ObtenerListado();
            if (lstClientes == null || lstClientes.Count == 0)
            {
                VerError("No ha seleccionado ningún asociado para realizar el envio");
                Session["lstClientes"] = null;
                mvPrincipal.SetActiveView(ViewListado);
            }
            else
                Session["lstClientes"] = lstClientes;
        }
        return true;
    }

    private void Actualizar()
    {
        try
        {
            VerError("");
            String emptyQuery = "Fila de datos vacia";

            string filtro = @" where perafi.estado = 'A' and TRUNC(perafi.FECHA_AFILIACION) <= to_date('" + Convert.ToDateTime(txtFechaGeneracion.Texto).ToShortDateString() + @"', 'dd/MM/yyyy')
                               and per.email is not null and per.email != '0'";

            Persona1Service personaService = new Persona1Service();
            List<Persona1> lstPersonasAfiliadas = personaService.ConsultarPersonasAfiliadas(filtro, Usuario);

            
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstPersonasAfiliadas;
            if (lstPersonasAfiliadas.Count > 0)
            {
                btnEnviar.Visible = true;
                panelLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstPersonasAfiliadas.Count.ToString();
                gvLista.DataBind();
            }
            else
            {
                btnEnviar.Visible = false;
                panelLista.Visible = false;
                VerError("Su consulta no obtuvo ningun resultado.");
                lblTotalRegs.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteExtractoServicio.CodigoProgramaCertifAnual, "Actualizar", ex);
        }
    }

    protected void EnviarCorreos()
    {
        List<Extracto> lstAsociados = new List<Extracto>();
        List<Xpinn.Aportes.Entities.ErroresCargaAportes> lstErrores = new List<Xpinn.Aportes.Entities.ErroresCargaAportes>();
        lstAsociados = (List<Extracto>)Session["lstClientes"];
        int cont = 0;
        try
        {
            foreach (Extracto pExtracto in lstAsociados)
            {
                cont++;
                if (pExtracto.email != null && pExtracto.email != "" && pExtracto.email != "0")
                {                    
                    bool error = GenerarDocumento(pExtracto);
                    if (!error) //Agregar en lista de errores si se produjo un error en el envío
                    {
                        Xpinn.Aportes.Entities.ErroresCargaAportes registro = new Xpinn.Aportes.Entities.ErroresCargaAportes();
                        registro.numero_registro = cont.ToString();
                        registro.datos = "Email";
                        registro.error = "Error en el envio del correo para el asociado código Nro. " + pExtracto.cod_persona;
                        lstErrores.Add(registro);
                    }
                }
                else
                {
                    Xpinn.Aportes.Entities.ErroresCargaAportes registro = new Xpinn.Aportes.Entities.ErroresCargaAportes();
                    registro.numero_registro = cont.ToString();
                    registro.datos = "Email";
                    registro.error = "Error: el cliente código No. " + pExtracto.cod_persona + " no tiene email registrado";
                    lstErrores.Add(registro);
                }
            }
            if (lstErrores.Count == 0)
            {
                Session["lsterrores"] = null;
            }
            //Session["mensajeFinal"] = "Se enviaron " + cont + " estados de cuenta correctamente";
            //Session["lsterrores"] = lstErrores;
            Session["Proceso"] = "FINAL";

        }
        catch (Exception ex)
        {
            VerError("Ha ocurrido un error en el envio" + ex.Message);
            Session["Proceso"] = "FINAL";
            TerminarProceso();
        }
    }    

    protected bool GenerarDocumento(Extracto pExtracto)
    {
        try
        {
            //Información de productos
            Extracto extracto = clienteExtractoServicio.BuscarExtractoAnualPersona(Convert.ToInt32(pExtracto.cod_persona), Convert.ToDateTime(txtFechaGeneracion.Text), Usuario);
            bool exitoso = false;

            if (extracto != null)
            {
                //Definición del reporte 
                ReportViewer reporte = new ReportViewer();
                reporte.ID = "RpviewEstado";
                reporte.Font.Name = "Verdana";
                reporte.Font.Size = 8;
                reporte.Visible = false;
                reporte.Height = 500;
                reporte.WaitMessageFont.Name = "Verdana";
                reporte.EnableViewState = true;

                DataTable tableAporte = tableAporte = CreateDataRow(extracto.lista_extracto_aportes);
                DataTable tableAhorro = tableAhorro = CreateDataRow(extracto.lista_extracto_ahorros);
                DataTable tableCreditos = tableCreditos = CreateDataRow(extracto.lista_extracto_creditos);
                DataTable tableCDAT = tableCDAT = CreateDataRow(extracto.lista_extracto_cdats);
                DataTable tableProgramado = tableProgramado = CreateDataRow(extracto.lista_extracto_programado);

                Xpinn.Seguridad.Entities.Proceso vProceso = new Xpinn.Seguridad.Entities.Proceso();
                Xpinn.Seguridad.Services.ProcesoService ProcesoServicio = new Xpinn.Seguridad.Services.ProcesoService();
                vProceso = ProcesoServicio.ConsultarProceso(2202, (Usuario)Session["usuario"]);

                Usuario pUsu = Usuario;
                reporte.LocalReport.DataSources.Clear();
                ReportParameter[] param = new ReportParameter[13]
                {
                new ReportParameter("NombreCliente", pExtracto.nombres),
                new ReportParameter("Identificacion", pExtracto.identificacion.ToString()),
                new ReportParameter("NombreEntidad", pUsu.empresa),
                new ReportParameter("NITEmpresa", pUsu.nitempresa),
                new ReportParameter("YearPeriodo", Convert.ToDateTime(txtFechaGeneracion.Texto).ToString("yyyy")),
                new ReportParameter("RutaImagen", new Uri(Server.MapPath("~/Images/LogoEmpresa.jpg")).AbsoluteUri),
                new ReportParameter("FechaGeneracion", DateTime.Today.ToShortDateString()),
                new ReportParameter("ReportAporte", extracto.lista_extracto_aportes != null && extracto.lista_extracto_aportes.Count > 0 ? "False" : "True"),
                new ReportParameter("ReportCredito", extracto.lista_extracto_creditos != null && extracto.lista_extracto_creditos.Count > 0 ? "False" : "True"),
                new ReportParameter("ReportAhorro",  extracto.lista_extracto_ahorros != null && extracto.lista_extracto_ahorros.Count > 0 ? "False" : "True"),
                new ReportParameter("ReportProgramado", extracto.lista_extracto_programado != null && extracto.lista_extracto_programado.Count > 0 ? "False" : "True"),
                new ReportParameter("ReportCDAT", extracto.lista_extracto_cdats != null && extracto.lista_extracto_cdats.Count > 0 ? "False" : "True"),
                new ReportParameter("NombreAhorro", vProceso.nombre)
                };

                string rutaReporte = Server.MapPath("~/Page/Reporteador/ExtractoCertificacion/ReportExtracto.rdlc");
                reporte.LocalReport.ReportPath = rutaReporte;
                reporte.LocalReport.EnableExternalImages = true;
                reporte.LocalReport.SetParameters(param);

                if (tableCreditos != null)
                {
                    ReportDataSource rds1 = new ReportDataSource("DataSetCredito", tableCreditos);
                    reporte.LocalReport.DataSources.Add(rds1);
                }

                if (tableAporte != null)
                {
                    ReportDataSource rds2 = new ReportDataSource("DataSetAportes", tableAporte);
                    reporte.LocalReport.DataSources.Add(rds2);
                }

                if (tableAhorro != null)
                {
                    ReportDataSource rds3 = new ReportDataSource("DataSetAhorroVista", tableAhorro);
                    reporte.LocalReport.DataSources.Add(rds3);
                }

                if (tableCDAT != null)
                {
                    ReportDataSource rds4 = new ReportDataSource("DataSetCDAT", tableCDAT);
                    reporte.LocalReport.DataSources.Add(rds4);
                }

                if (tableProgramado != null)
                {
                    ReportDataSource rds5 = new ReportDataSource("DataSetAhorroProgramado", tableProgramado);
                    reporte.LocalReport.DataSources.Add(rds5);
                }

                reporte.ServerReport.DisplayName = pExtracto.identificacion;
                reporte.LocalReport.DisplayName = pExtracto.identificacion;
                reporte.LocalReport.Refresh();
                var bytes = reporte.LocalReport.Render("PDF"); //Reporte en bytes
                if (bytes != null) //Enviar correo si el reporte se generó
                    exitoso = EnviarCorreoAsociado(pExtracto.nombres, pExtracto.email, bytes, pExtracto.identificacion);
                else
                    exitoso = false;
            }
            return exitoso;
        }
        catch (Exception ex)
        {
            VerError("No se ha completado el envio"+ex.Message);
            TerminarProceso();
            return false;
        }
    }

    public DataTable CreateDataRow(List<Extracto> extracto)
    {
        DataTable table = new DataTable();
        table.Columns.Add("NombreLinea");
        table.Columns.Add("SaldoInicial");
        table.Columns.Add("Movimientos");
        table.Columns.Add("Intereses");
        table.Columns.Add("RetencionPracticada");
        table.Columns.Add("InteresPago");
        table.Columns.Add("OtrosPago");
        table.Columns.Add("Abonos");
        table.Columns.Add("SaldoFinal");

        foreach (Extracto fila in extracto)
        {
            DataRow datos = table.NewRow();

            datos[0] = fila.nom_linea;
            datos[1] = fila.saldo_inicial;
            datos[2] = fila.movimiento;
            datos[3] = fila.interes;
            datos[4] = fila.retencion;
            datos[5] = fila.interes_corriente + fila.interes_mora;
            datos[6] = fila.otros_pagos;
            datos[7] = fila.saldo_pagado;
            datos[8] = fila.saldo_final;

            table.Rows.Add(datos);
        }

        if (table.Rows.Count == 0)
        {
            CreateEmptyDataRow(table);
        }

        return table;
    }

    void CreateEmptyDataRow(DataTable table)
    {
        DataRow datos = table.NewRow();

        datos[0] = "";
        datos[1] = "0";
        datos[2] = "0";
        datos[3] = "0";
        datos[4] = "0";
        datos[5] = "0";
        datos[6] = "0";
        datos[7] = "0";
        datos[8] = "0";

        table.Rows.Add(datos);
    }


    protected bool EnviarCorreoAsociado(string nomAsociado, string emailAsociado, byte[] bytes, string ident)
    {
        try
        {
            VerError("");
            Usuario pUsuario = (Usuario)Session["usuario"];
            TiposDocCobranzasServices _tipoDocumentoServicio = new TiposDocCobranzasServices();

            Xpinn.Comun.Entities.Empresa empresa = _tipoDocumentoServicio.ConsultarCorreoEmpresa(pUsuario.idEmpresa, pUsuario);
            ParametroCorreo parametroCorreo = (ParametroCorreo)Enum.Parse(typeof(ParametroCorreo), ((int)TipoDocumentoCorreo.EstadoCuenta).ToString());
            TiposDocCobranzas modificardocumento = _tipoDocumentoServicio.ConsultarFormatoDocumentoCorreo((int)parametroCorreo, pUsuario);

            parametrosFormatoCorreo = new Dictionary<ParametroCorreo, string>();
            parametrosFormatoCorreo.Add(ParametroCorreo.NombreCompletoPersona, nomAsociado);
            modificardocumento.texto = ReemplazarParametrosEnElMensajeCorreo(modificardocumento.texto);

            CorreoHelper correoHelper = new CorreoHelper(emailAsociado, empresa.e_mail, empresa.clave_e_mail);
            bool exitoso = correoHelper.EnviarCorreoArchivoAdjunto(modificardocumento.texto, Correo.Gmail, bytes, "Certificado" + ident + ".pdf", "Certificado Anual", empresa.nombre); //Se adjunta en la clase Correo Helper
            return exitoso;
        }
        catch (Exception ex)
        {
            VerError("Error en el envio del mensaje" + ex.Message);
            return false;
        }

    }

    protected void chkEncabezado_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkEncabezado = (CheckBox)sender;
        if (chkEncabezado != null)
        {
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox chkEnviar = (CheckBox)rFila.FindControl("chkEnviar");
                chkEnviar.Checked = chkEncabezado.Checked;
            }
        }
    }
    
}