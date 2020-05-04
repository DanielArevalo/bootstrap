using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Util;
using System.Configuration;
using System.Data;
using System.IO;

public partial class AseClienteLista : GlobalWeb
{
    Usuario usuario = new Usuario();
    ClientePotencial aseEntCliente = new ClientePotencial();
    Zona aseEntZona = new Zona();

    ClientePotencialService serviceCliente = new ClientePotencialService();
    ParametricaService serviceParametrica = new ParametricaService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(serviceCliente.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoImportar += btnImportar_Click;
            //toolBar.eventoLimpiar += btnLimpiar_Click;
            ucImprimir.PrintCustomEvent += ucImprimir_Click;
            toolBar.eventoRegresar += ToolBar_eventoRegresar;
            toolBar.MostrarCargar(false);
            toolBar.MostrarRegresar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceCliente.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ObtenerDatosDropDownList();

                CargarValoresConsulta(pConsulta, serviceCliente.GetType().Name);
                if (Session[serviceCliente.GetType().Name + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceCliente.GetType().Name + "L", "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, serviceCliente.GetType().Name);
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, serviceCliente.GetType().Name);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, serviceCliente.GetType().Name);
    }

    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs evt)
    {
        if (evt.CommandName == "EstadoCuenta")
        {
            Int64 idCliente = Convert.ToInt64(evt.CommandArgument.ToString());
            Session[serviceCliente.CodigoPrograma + ".idClienteEstadoCuenta"] = idCliente;
            Navegar("~/Page/Asesores/EstadoCuenta/Lista.aspx");
        }
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnBorrar");
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.SelectedRow.Cells[0].Text;
        Session[serviceCliente.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session[serviceCliente.CodigoPrograma + ".id"] = id;
        Session[serviceCliente.CodigoPrograma + ".from"] = "l";
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long idObjeto = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[0].Text);
            serviceCliente.EliminarCliente(idObjeto, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceCliente.CodigoPrograma + "L", "gvLista_RowDeleting", ex);
        }
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
            BOexcepcion.Throw(serviceCliente.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<ClientePotencial> lstConsulta = new List<ClientePotencial>();

            lstConsulta = serviceCliente.ListarCliente(ObtenerValores(), (Usuario)Session["usuario"]);

            var lstClientes = from c in lstConsulta
                              select new
                              {
                                  c.IdCliente,
                                  c.PrimerNombre,
                                  c.SegundoNombre,
                                  c.PrimerApellido,
                                  c.SegundoApellido,
                                  c.TipoIdentificacion.NombreTipoIdentificacion,
                                  c.NumeroDocumento
                              };
            lstClientes = lstClientes.OrderBy(c => c.IdCliente).ToList();

            gvLista.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            gvLista.DataSource = lstClientes;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
            }

            Session.Add(serviceCliente.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceCliente.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private ClientePotencial ObtenerValores()
    {
        ClientePotencial aseEntCliente = new ClientePotencial();

        if (!string.IsNullOrEmpty(txtPrimerNombre.Text.Trim())) aseEntCliente.PrimerNombre = txtPrimerNombre.Text.Trim();
        if (!string.IsNullOrEmpty(txtPrimerApellido.Text.Trim())) aseEntCliente.PrimerApellido = txtPrimerApellido.Text.Trim();
        if (!string.IsNullOrEmpty(txtNumeIdentificacion.Text.Trim())) aseEntCliente.NumeroDocumento = Convert.ToInt64(txtNumeIdentificacion.Text.Trim());
        if (!string.IsNullOrEmpty(ddlZona.SelectedValue.ToString())) aseEntCliente.Zona.IdZona = Convert.ToInt64(ddlZona.SelectedValue.ToString());

        return aseEntCliente;
    }

    private void ObtenerDatosDropDownList()
    {
        ddlZona.DataSource = serviceParametrica.ListarBarrios(5, (Usuario)Session["Usuario"]);
        ddlZona.DataTextField = "NOMCIUDAD";
        ddlZona.DataValueField = "CODCIUDAD";
        ddlZona.DataBind();
        ddlZona.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }

    protected void ucImprimir_Click(object sender, ImageClickEventArgs evt)
    {
        Session.Remove("imprimirCtrl");
        Session["imprimirCtrl"] = gvLista;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", Imprimir.JSCRIPT_PRINT);
    }

    #region Metodos de importacion

    private void btnImportar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            mvClientes.ActiveViewIndex = 1;
            Site toolBar = (Site)Master;
            toolBar.MostrarImportar(false);
            toolBar.MostrarGuardar(false);
            toolBar.MostrarConsultar(false);
            toolBar.MostrarRegresar(true);
            btnCargar.Visible = true;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    private void ToolBar_eventoRegresar(object sender, ImageClickEventArgs e)
    {
        mvClientes.ActiveViewIndex = 0;
        Site toolBar = (Site)Master;
        toolBar.MostrarImportar(true);
        toolBar.MostrarConsultar(true);
        toolBar.MostrarRegresar(false);
    }

    protected void btnCargar_Click(object sender, EventArgs e)
    {
        VerError("");
        string error = "";
        try
        {
            if (!flpArchivo.HasFile)
            {
                VerError("Seleccione un archivo excel, verifique los datos.");
                return;
            }
            ExcelToGrid CargaEx = new ExcelToGrid();
            //CREACION DEL DATATABLE
            DataTable dt = new DataTable();

            String NombreArchivo = Path.GetFileName(this.flpArchivo.PostedFile.FileName);

            if (!validarExtension(flpArchivo))
            {
                VerError("Ingrese un Archivo valido (Excel).");
                return;
            }
            //GUARDANDO EL ARCHIVO EN EL SERVIDOR
            if (!Directory.Exists(Server.MapPath("Archivos\\")))
                Directory.CreateDirectory(Server.MapPath("Archivos\\"));

            flpArchivo.PostedFile.SaveAs(Server.MapPath("Archivos\\") + NombreArchivo);
            flpArchivo.Dispose();

            //LEENDO LOS DATOS EN EL DATATABLE
            try
            {
                dt = CargaEx.leerExcel(Server.MapPath("Archivos\\") + NombreArchivo, "Datos");
                
            }
            catch
            {
                VerError("No se pudo cargar el archivo o el archivo no contiene datos (Hoja: Datos)");
                return;
            }

            // ELIMINANDO ARCHIVOS GENERADOS
            try
            {
                string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Archivos\\"));
                foreach (string ficheroActual in ficherosCarpeta)
                    File.Delete(ficheroActual);
            }
            catch
            { }

            if (dt.Rows.Count <= 0)
            {
                error = "No se pudo cargar el archivo o el archivo no contiene datos (Hoja: Datos)";
                VerError(error);
                return;
            }
            else
            {                
                List<ClientePotencial> lstClientesImporte = new List<ClientePotencial>();
                List<ErroresCarga> lstErrores = new List<ErroresCarga>();
                string pError = string.Empty;
                ObtenerClientes(ref lstClientesImporte, dt, lstErrores);

                Site toolBar = (Site)this.Master;
                toolBar.MostrarImportar(true);

                ClientePotencialService objClienteService = new ClientePotencialService();
                Usuario pUsuario = new Usuario();
                pUsuario = (Usuario)Session["Usuario"];

                int limpiar = 0;
                if (chkLimpiar.Checked)
                    limpiar = 1; 

                objClienteService.CrearClientes(lstClientesImporte, pUsuario,limpiar, lstErrores);

                panCargueExitoso.Visible = true;
                gvCarguesExitosos.DataSource = lstClientesImporte;
                gvCarguesExitosos.DataBind();
                lblTotalCarguesExitoso.Visible = true;
                lblTotalCarguesExitoso.Text = "<br/> Registros cargados exitosamente  : " + lstClientesImporte.Count;

                if (lstErrores.Count > 0)
                {
                    panErrores.Visible = true;
                    gvErrores.DataSource = lstErrores;
                    gvErrores.DataBind();
                    cpeDemo1.CollapsedText = "(Click Aqui para ver " + lstErrores.Count() + " errores...)";
                    cpeDemo1.ExpandedText = "(Click Aqui para ocultar listado de errores...)";
                }
                else
                {
                    panErrores.Visible = false;
                    lblMensajeExitoso.Visible = lstClientesImporte.Count > 0;
                }
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    bool validarExtension(FileUpload file)
    {
        bool rutaOk = false;
        string extension = Path.GetExtension(file.FileName).ToLower();

        rutaOk = extension == ".xls" || extension == ".xlsx" ? true : false;

        return rutaOk;
    }

    private void ObtenerClientes(ref List<ClientePotencial> lstArchivo, DataTable dt, List<ErroresCarga> pErrores)
    {
        int NumFila = 1;

        //REALIZANDO CARGA DEL BALANCE INICIAL
        //RECORRIENDO LOS DATOS DEL EXCEL LEIDO
        ClientePotencial objCliente;
        ErroresCarga objError = new ErroresCarga();

        foreach (DataRow rData in dt.Rows)
        {
            int NumCol = 0;
            try
            {
                NumFila++;
                objCliente = new ClientePotencial();

                //ORDEN DE CARGA: Primer nombre, Segundo nombre, Número documento,  Telefono, Razón social, Sigla negocio,Dirección , Email,
                //Tipo identificación, Zona, Actividad
                for (NumCol = 0; NumCol <= 15; NumCol++)
                {
                    if (NumCol == 0)
                    {
                        if (rData.ItemArray[NumCol].ToString().Trim() != "" && rData.ItemArray[NumCol].ToString() != "&nbsp;")
                            objCliente.PrimerNombre = rData.ItemArray[NumCol].ToString().Trim();
                    }
                    if (NumCol == 1)
                    {
                        if (rData.ItemArray[NumCol].ToString().Trim() != "" && rData.ItemArray[NumCol].ToString() != "&nbsp;")
                            objCliente.SegundoNombre = rData.ItemArray[NumCol].ToString().Trim();
                    }
                    if (NumCol == 2)
                    {
                        if (rData.ItemArray[NumCol].ToString().Trim() != "" && rData.ItemArray[NumCol].ToString() != "&nbsp;")
                            objCliente.PrimerApellido = rData.ItemArray[NumCol].ToString().Trim();
                    }
                    if (NumCol == 3)
                    {
                        if (rData.ItemArray[NumCol].ToString().Trim() != "" && rData.ItemArray[NumCol].ToString() != "&nbsp;")
                            objCliente.SegundoApellido = rData.ItemArray[NumCol].ToString().Trim();
                    }
                    if (NumCol == 4)
                    {
                        if (rData.ItemArray[NumCol].ToString().Trim() != "" && rData.ItemArray[NumCol].ToString() != "&nbsp;")
                            objCliente.TipoIdentificacion.IdTipoIdentificacion = Convert.ToInt64(rData.ItemArray[NumCol].ToString().Trim());
                    }
                    if (NumCol == 5)
                    {
                        if (rData.ItemArray[NumCol].ToString().Trim() != "" && rData.ItemArray[NumCol].ToString() != "&nbsp;")
                            objCliente.NumeroDocumento = Convert.ToInt64(rData.ItemArray[NumCol].ToString().Trim());
                    }
                    if (NumCol == 6)
                    {
                        if (rData.ItemArray[NumCol].ToString().Trim() != "" && rData.ItemArray[NumCol].ToString() != "&nbsp;")
                            objCliente.Telefono = Convert.ToString(rData.ItemArray[NumCol].ToString().Trim());
                    }
                    if (NumCol == 7)
                    {
                        if (rData.ItemArray[NumCol].ToString().Trim() != "" && rData.ItemArray[NumCol].ToString() != "&nbsp;")
                            objCliente.Email = rData.ItemArray[NumCol].ToString().Trim();
                    }
                    if (NumCol == 8)
                    {
                        if (rData.ItemArray[NumCol].ToString().Trim() != "" && rData.ItemArray[NumCol].ToString() != "&nbsp;")
                            objCliente.Zona.IdZona = Convert.ToInt64(rData.ItemArray[NumCol].ToString().Trim());
                    }
                    if (NumCol == 9)
                    {
                        if (rData.ItemArray[NumCol].ToString().Trim() != "" && rData.ItemArray[NumCol].ToString() != "&nbsp;")
                            objCliente.codasesor = Convert.ToInt64(rData.ItemArray[NumCol].ToString().Trim());
                    }
                    if (NumCol == 10)
                    {
                        if (rData.ItemArray[NumCol].ToString().Trim() != "" && rData.ItemArray[NumCol].ToString() != "&nbsp;")
                            objCliente.Direccion = rData.ItemArray[NumCol].ToString().Trim();
                    }
                    if (NumCol == 11)
                    {
                        if (rData.ItemArray[NumCol].ToString().Trim() != "" && rData.ItemArray[NumCol].ToString() != "&nbsp;")
                            objCliente.RazonSocial = rData.ItemArray[NumCol].ToString().Trim();
                    }
                    if (NumCol == 12)
                    {
                        if (rData.ItemArray[NumCol].ToString().Trim() != "" && rData.ItemArray[NumCol].ToString() != "&nbsp;")
                            objCliente.SiglaNegocio = rData.ItemArray[NumCol].ToString().Trim();
                    }
                    if (NumCol == 13)
                    {
                        if (rData.ItemArray[NumCol].ToString().Trim() != "" && rData.ItemArray[NumCol].ToString() != "&nbsp;")
                            objCliente.Actividad.IdActividad = Convert.ToInt64(rData.ItemArray[NumCol].ToString().Trim());
                    }
                    if (NumCol == 14)
                    {
                        if (rData.ItemArray[NumCol].ToString().Trim() != "" && rData.ItemArray[NumCol].ToString() != "&nbsp;")
                            objCliente.MotivoAfiliacion.IdMotivoAfiliacion = Convert.ToInt64(rData.ItemArray[NumCol].ToString().Trim());
                    }
                    if (NumCol == 15)
                    {
                        if (rData.ItemArray[NumCol].ToString().Trim() != "" && rData.ItemArray[NumCol].ToString() != "&nbsp;")
                            objCliente.MotivoAfiliacion.Observaciones = rData.ItemArray[NumCol].ToString().Trim();
                    }

                }

                objCliente.FechaRegistro = DateTime.Now;
                lstArchivo.Add(objCliente);
            }
            catch (Exception ex)
            {
                objError = new ErroresCarga();
                objError.datos = NumFila.ToString();
                objError.numero_registro = NumFila.ToString();
                objError.error = "Se generó un error en la fila " + NumFila + " en la columna " + (NumCol + 1 ) +" - " + ex.Message;
                pErrores.Add(objError);
            }
        }
    }

    #endregion
}