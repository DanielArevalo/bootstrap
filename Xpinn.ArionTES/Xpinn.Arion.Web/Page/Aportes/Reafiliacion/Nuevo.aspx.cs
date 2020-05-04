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
using Xpinn.Contabilidad.Entities;
using Xpinn.Contabilidad.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Comun.Entities;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Contabilidad.Services.TerceroService TerceroServicio = new Xpinn.Contabilidad.Services.TerceroService();
    private Xpinn.Aportes.Services.AfiliacionServices AfiliacionServicio = new Xpinn.Aportes.Services.AfiliacionServices();
    GrupoLineaAporteServices GrupoLineaService = new GrupoLineaAporteServices();
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[AfiliacionServicio.codigoprogramaReafiliacion + ".id"] != null)
                VisualizarOpciones(AfiliacionServicio.codigoprogramaReafiliacion, "E");
            else
                VisualizarOpciones(AfiliacionServicio.codigoprogramaReafiliacion, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            txtFechaAfili.eventoCambiar += txtFechaAfili_TextChanged;
            toolBar.MostrarGuardar(true);
            toolBar.MostrarConsultar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.codigoprogramaReafiliacion, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {                
                CargarListas();
                Calcular_Valor_Afiliacion();
                if (Session[AfiliacionServicio.codigoprogramaReafiliacion + ".id"] != null)
                {
                    idObjeto = Session[AfiliacionServicio.codigoprogramaReafiliacion + ".id"].ToString();
                    Session.Remove(AfiliacionServicio.codigoprogramaReafiliacion + ".id");
                    ObtenerDatos(idObjeto);
                }
                ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.codigoprogramaReafiliacion, "Page_Load", ex);
        }
    }

    public int CalcularMesesDeDiferencia(DateTime fechaDesde, DateTime fechaHasta)
    {
        return Math.Abs((fechaHasta.Month - fechaDesde.Month) + 12 * (fechaHasta.Year - fechaDesde.Year));
    }

    Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();

    protected DateTime? DeterminarFechaProxPago()
    {
        DateTime? fechainicio;
        try
        {
            // SE AJUSTO PARA QUE EL SISTEMA CALCULE UN PERIODO DESPUES EN BASE A LA FECHA DE REAFILIACION
            string pFormaPago = ddlFormaPago.SelectedValue;
            if (pFormaPago == "1")
                fechainicio = Convert.ToDateTime(txtFechaAfili.Text).AddMonths(1);
            else
                fechainicio = AfiliacionServicio.FechaInicioAfiliacion(txtFechaAfili.ToDateTime, Convert.ToInt64(ddlEmpresa.SelectedValue), (Usuario)Session["Usuario"]);
            if (fechainicio != null)
                return fechainicio;
            else
                return DateTime.Now;
        }
        catch
        { 
           return DateTime.Now;
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            if (txtFechaAfili.Text == "")
            {
                VerError("Ingrese la fecha de reafiliación, verifique los datos.");
                return;
            }
            if (ddlOficina.SelectedIndex == 0)
            {
                VerError("Seleccione la oficina a la cual pertenecerá la re afiliación.");
                ddlOficina.Focus();
                return;
            }

            if (Convert.ToDateTime(txtFechaAfili.Texto) != DateTime.MinValue)
            {
                //Validar fecha de cierre de personas
                Afiliacion pAfiliacion = AfiliacionServicio.ConsultarCierrePersonas((Usuario)Session["usuario"]);

                if (Convert.ToDateTime(txtFechaAfili.Texto) < pAfiliacion.fecha_cierre && pAfiliacion.estadocierre == "D")
                {
                    VerError("No se pueden registrar reafiliaciones en periodos ya cerrados");
                    return;
                }
            }

            Xpinn.Comun.Data.GeneralData DAGeneral = new Xpinn.Comun.Data.GeneralData();
            Xpinn.Comun.Entities.General pEntidad = new Xpinn.Comun.Entities.General();

            //consultar numero de meses para reafiliar
            pEntidad = DAGeneral.ConsultarGeneral(500, (Usuario)Session["usuario"]);
            if (pEntidad.valor != null)
            {
                if (pEntidad.valor != "0" && pEntidad.valor != "")
                {
                    if (txtfecretiro.Text != "")
                    { 
                        int mesdiferencia = 0;
                        mesdiferencia = CalcularMesesDeDiferencia(Convert.ToDateTime(txtfecretiro.Text),Convert.ToDateTime(txtFechaAfili.Text));
                        if (mesdiferencia > 0)
                        {
                            if (mesdiferencia < Convert.ToInt32(pEntidad.valor))
                            {
                                VerError("No se puede generar la grabación debido a que no cumple con los meses permitidos para realifiarce.");
                                return;
                            }
                        }
                    }
                }
            }

            Afiliacion crearreafiliacion = new Afiliacion();
            
            if(txtafiliacionid.Text!="")
            crearreafiliacion.idafiliacion = Convert.ToInt32(txtafiliacionid.Text);
            if (Session["ID"] != null)
                crearreafiliacion.cod_persona = Convert.ToInt32(Session["ID"].ToString());
            if (txtFechaAfili.Text != "")
                crearreafiliacion.fecha_afiliacion = Convert.ToDateTime(txtFechaAfili.Text);

            crearreafiliacion.estado = "A";
            if (txtfecretiro.Text != "")
                crearreafiliacion.fecha_retiro = Convert.ToDateTime(txtfecretiro.Text);
            if (txtValorAfili.Text != "")
            {
                crearreafiliacion.valor = Convert.ToDecimal(txtValorAfili.Text);
                crearreafiliacion.saldo = Convert.ToDecimal(txtValorAfili.Text);
            }
            if (txtFecha1Pago.Text != "")
            {
                crearreafiliacion.fecha_primer_pago = Convert.ToDateTime(txtFecha1Pago.Text);
                crearreafiliacion.fecha_proximo_pago = Convert.ToDateTime(txtFecha1Pago.Text);
            }
            if (txtCuotasAfili.Text != "")
                crearreafiliacion.cuotas = Convert.ToInt32(txtCuotasAfili.Text);
            if (ddlPeriodicidad.SelectedIndex != 0)
                crearreafiliacion.cod_periodicidad = Convert.ToInt32(ddlPeriodicidad.SelectedValue);
            if(ddlFormaPago.SelectedValue != "0")
                crearreafiliacion.forma_pago=Convert.ToInt32(ddlFormaPago.SelectedValue);
            if (ddlEmpresa.SelectedIndex != 0)
                crearreafiliacion.empresa_formapago = Convert.ToInt32(ddlEmpresa.SelectedValue);
            //ultima afiliacion
            if (txtfecafi.Text != "")
                crearreafiliacion.fecha_ultima_afiliacion = Convert.ToDateTime(txtfecafi.Text);
            //retiro
            if (txtfecretiro.Text != "")
                crearreafiliacion.fecha_retiro = Convert.ToDateTime(txtfecretiro.Text);
            if(txtIdentificacion.Text != "")
            {
                crearreafiliacion.identificacion = Convert.ToString(txtIdentificacion.Text);
            }

            crearreafiliacion.cod_oficina = Convert.ToInt64(ddlOficina.SelectedValue);

            List<Afiliacion> lstAportes = new List<Afiliacion>();
            Afiliacion crearAportes;
            
            foreach (GridViewRow fila in gvLista.Rows)
            {
                crearAportes = new Afiliacion();
                CheckBox chkactiva = (CheckBox)fila.FindControl("chkActivar");
                int Cuota = 0;
                crearAportes.numero_aporte = Convert.ToInt64(fila.Cells[0].Text);
                crearAportes.fecha_apertura = Convert.ToDateTime(txtFechaAfili.Text);
                crearAportes.cod_linea_aporte = Convert.ToInt32(fila.Cells[2].Text);
                if (fila.Cells[4].Text != "&nbsp;" && fila.Cells[4].Text.Trim() != "")
                    Cuota = Convert.ToInt32(Math.Round(Convert.ToDecimal(fila.Cells[4].Text), 0));
                crearAportes.cuotas = Convert.ToInt32(Cuota);
                crearAportes.fecha_prox_pago = Convert.ToDateTime(DeterminarFechaProxPago());
                crearAportes.forma_pago = Convert.ToInt32(fila.Cells[6].Text);
                crearAportes.estados = chkactiva.Checked == true ? 1 : 2;
                crearAportes.cod_oficina = Convert.ToInt64(ddlOficina.SelectedValue);
                lstAportes.Add(crearAportes);
            }
                      
          
            

            int cantidad = 0;

            General entidad = ConsultarParametroGeneral(69);
            cantidad = AfiliacionServicio.ConsultarReafilPerm(crearreafiliacion, (Usuario)Session["usuario"]);
            if (cantidad <= Convert.ToInt64(entidad.valor)) {
                string pError = string.Empty;
                AfiliacionServicio.ModificarReafiliacion(crearreafiliacion, lstAportes, ref pError, Usuario);
                if (!string.IsNullOrEmpty(pError))
                {
                    VerError(pError);
                    return;
                }
            }
            else
            {
                VerError("La cantidad de reafiliaciones supera lo permitido.");
                return;
            }

            mvDatos.ActiveViewIndex = 1;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarConsultar(true);

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }       
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Session[AfiliacionServicio.codigoprogramaReafiliacion + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Contabilidad.Entities.Tercero vTercero = new Xpinn.Contabilidad.Entities.Tercero();
            vTercero = TerceroServicio.ConsultarTercero(Convert.ToInt64(pIdObjeto), null, (Usuario)Session["usuario"]);

            if (vTercero.primer_nombre != null || vTercero.segundo_nombre!= null)
                txtNombres.Text = vTercero.primer_nombre+ " " + vTercero.segundo_nombre;
            if (vTercero.primer_apellido != null || vTercero.segundo_apellido!= null)
                txtApellidos.Text = vTercero.primer_apellido+" " + vTercero.segundo_apellido;
            if (vTercero.identificacion != null)
                txtIdentificacion.Text = vTercero.identificacion;
            if (vTercero.tipo_identificacion != null)
                txtTipoidentif.Text = vTercero.tipo_identificacion.ToString();
            if (vTercero.ciucorrespondencia != null)
                txtciudadresidencia.Text = vTercero.ciucorrespondencia.ToString();
            if (vTercero.direccion != null)
                txtDireccion.Text = vTercero.direccion;
            if (vTercero.telefono != null)
                TXTtELEFONO.Text = vTercero.telefono;

            if (vTercero.cod_oficina != null)
                ddlOficina.SelectedValue = vTercero.cod_oficina.ToString();
            Afiliacion consulta = new Afiliacion();
            List<Afiliacion> lstconsulta = new List<Afiliacion>();
            lstconsulta = AfiliacionServicio.ConsultarAportes(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (lstconsulta.Count > 0)
            {

                gvLista.DataSource = lstconsulta;
                gvLista.Visible = true;
                gvLista.DataBind();

            }
            else 
            {
                gvLista.Visible = false;
            
            }

            consulta = AfiliacionServicio.ConsultarAfiliacion(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            if (consulta.idafiliacion != null|| consulta.idafiliacion != 0)
                txtafiliacionid.Text = consulta.idafiliacion.ToString();
            if (consulta.fecha_afiliacion != null && consulta.fecha_afiliacion != DateTime.MinValue)
                txtfecafi.Text = consulta.fecha_afiliacion.ToString();
            if (consulta.estado != null)
                txtestado.Text = consulta.estado;
            if (consulta.fecha_retiro != null && consulta.fecha_retiro != DateTime.MinValue)
                txtfecretiro.Text=consulta.fecha_retiro.ToShortDateString();

            List<GrupoLineaAporte> lstGrupoLineas = GrupoLineaService.ListaGrupoLineaAporte(string.Empty, Usuario);
            if (lstGrupoLineas != null)
                if (lstGrupoLineas.Count > 0)
                    ViewState["DT_LINEAS"] = lstGrupoLineas;
            Session["ID"] = idObjeto;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.codigoprogramaReafiliacion, "ObtenerDatos", ex);
        }
    }

    private void CargarListas()
    {
        try
        {
          

            PoblarLista("periodicidad", ddlPeriodicidad);

            PoblarLista("EMPRESA_RECAUDO", ddlEmpresa);

            PoblarLista("OFICINA", "COD_OFICINA,NOMBRE", "ESTADO = 1", "1", ddlOficina);

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TerceroServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    private List<Xpinn.FabricaCreditos.Entities.Persona1> TraerResultadosLista(string ListaSolicitada)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();    
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
        return lstDatosSolicitud;
    }

    protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove("Iniciar");
        
            Site toolBar = (Site)this.Master;           
            toolBar.MostrarGuardar(true);
        

    }

    protected void rbJuridica_CheckedChanged(object sender, EventArgs e)
    {
       
    }

    protected void rbNatural_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void gvInfoAdicional_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtCadena = (TextBox)e.Row.FindControl("txtCadena");
            TextBox txtNumero = (TextBox)e.Row.FindControl("txtNumero");
            fecha txtctlfecha = (fecha)e.Row.FindControl("txtctlfecha");
            txtCadena.Visible = false;
            txtNumero.Visible = false;
            txtctlfecha.Visible = false;

            DropDownListGrid ddlDropdown = (DropDownListGrid)e.Row.FindControl("ddlDropdown");
            ddlDropdown.Visible = false;

            Label lblopcionaActivar = (Label)e.Row.FindControl("lblopcionaActivar");
            if (lblopcionaActivar != null)
            {
                if (lblopcionaActivar.Text == "1")//CARACTER
                {
                    txtCadena.Visible = true;
                }
                else if (lblopcionaActivar.Text == "2")//FECHA
                {
                    txtctlfecha.Visible = true;
                }
                else if (lblopcionaActivar.Text == "3") //NUMERO
                {
                    txtNumero.Visible = true;
                }
                else if (lblopcionaActivar.Text == "4") // DROPDOWNLIST
                {
                    ddlDropdown.Visible = true;

                    Label lblDropdown = (Label)e.Row.FindControl("lblDropdown");

                    if (lblDropdown.Text != "")
                        Session["Datos"] = lblDropdown.Text;
                    if (ddlDropdown != null)
                    {
                        string[] sDatos;

                        if (lblDropdown.Text != "")
                            sDatos = lblDropdown.Text.Split(',');
                        else
                            sDatos = Session["Datos"].ToString().Split(',');
                        if (sDatos.Count() > 0)
                        {
                            ddlDropdown.Items.Clear();
                            for (int i = 0; i < sDatos.Count(); i++)
                            {
                                ddlDropdown.Items.Insert(i, new ListItem(sDatos[i].ToString(),sDatos[i].ToString()));                                
                            }
                            ddlDropdown.DataBind();                         
                        }
                    }

                    Label lblValorDropdown = (Label)e.Row.FindControl("lblValorDropdown");
                    if (lblValorDropdown.Text != "")
                    {
                        ddlDropdown.SelectedValue = lblValorDropdown.Text;
                    }
                }
            }
        }
    }

    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }

    protected void ddlEstadoAfi_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFormaPago.SelectedItem.Value == "2" || ddlFormaPago.SelectedItem.Text == "Nomina")
        {
            lblEmpresa.Visible = true;
            ddlEmpresa.Visible = true;
        }
        else
        {
            lblEmpresa.Visible = false;
            ddlEmpresa.Visible = false;
        }
    }

    protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            vPersona1.seleccionar = "Identificacion";
            vPersona1.noTraerHuella = 1;
            
            vPersona1 = persona1Servicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);
            if (idObjeto != "")
            {
                if (vPersona1.identificacion != "" && vPersona1.identificacion != null)
                    VerError("ERROR: El NIT ingresado ya existe");
            }
            else
            {
                if (vPersona1.identificacion != "" && vPersona1.identificacion != null)
                    VerError("ERROR: El NIT ingresado ya existe");
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void txtFechaAfili_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txtFechaAfili.Texto) > DateTime.Now)
        {
            txtFechaAfili.Text = DateTime.Now.ToShortDateString();
            VerError("La fecha de re-afiliación no puede ser mayor a la fecha actual");
        }
    }

    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Determinar programación de la pagaduría
        if (ddlEmpresa.SelectedItem != null)
        {
            if (!txtFechaAfili.TieneDatos)
                txtFechaAfili.ToDateTime = System.DateTime.Now;
            try
            {
                DateTime? FechaInicio = AfiliacionServicio.FechaInicioAfiliacion(Convert.ToDateTime(txtFechaAfili.ToDate), Convert.ToInt64(ddlEmpresa.SelectedItem.Value), (Usuario)Session["Usuario"]);
            }
            catch
            { }
        }
    }

    void Calcular_Valor_Afiliacion()
    {
        try
        {
            ParametrosAfiliacion vDetalle = new ParametrosAfiliacion();
            ParametrosAfiliacionServices ParametroService = new ParametrosAfiliacionServices();
            vDetalle = ParametroService.ConsultarParametrosAfiliacion(0, (Usuario)Session["usuario"]);
            decimal SalarioMin = 0, nuevoValor = 0, valor = 0;
            if (vDetalle.valor != 0)
                valor = vDetalle.valor;

            if (vDetalle.tipo_calculo == 2) // si es de tipo % SMLMV
            {
                //RECUPERAR SALARIO MINIMO
                Xpinn.Comun.Entities.General pData = new Xpinn.Comun.Entities.General();
                Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
                pData = ConsultaData.ConsultarGeneral(10, (Usuario)Session["usuario"]);

                if (pData.valor != "" && pData.valor != null)
                    SalarioMin = Convert.ToDecimal(pData.valor);

                nuevoValor = (SalarioMin * valor) / 100;
                txtValorAfili.Text = nuevoValor.ToString("n0");
            }
            else // es de tipo Valor FIJO
            {
                txtValorAfili.Text = valor.ToString("n0");
            }

            if (vDetalle.numero_cuotas != 0)
                txtCuotasAfili.Text = vDetalle.numero_cuotas.ToString();
            if (vDetalle.cod_periodicidad != 0)
                ddlPeriodicidad.SelectedValue = vDetalle.cod_periodicidad.ToString();
            

        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void chkActivar_CheckedChanged(object sender, EventArgs e)
    {
        if (ViewState["DT_LINEAS"] != null)
        {
            List<GrupoLineaAporte> lstLineas = (List<GrupoLineaAporte>)ViewState["DT_LINEAS"];
            if (lstLineas.Count > 0)
            {
                CheckBox chkActivar = (CheckBox)sender;
                bool IsSelected = chkActivar.Checked;
                if (chkActivar != null)
                {
                    GridViewRow rFila = (GridViewRow)chkActivar.NamingContainer;
                    int CodLineaRow = Convert.ToInt32(gvLista.DataKeys[rFila.RowIndex].Values[1].ToString());

                    bool result = lstLineas.Where(z => z.cod_linea_aporte == CodLineaRow).Any();

                    if (result)
                    {
                        List<GrupoLineaAporte> lstFiltroGrupo = lstLineas.Where(x => x.idgrupo ==
                            (lstLineas.Where(z => z.cod_linea_aporte == CodLineaRow).Select(y => new { y.idgrupo }).First().idgrupo)
                            && x.cod_linea_aporte != CodLineaRow).ToList();

                        foreach (GridViewRow gridRow in gvLista.Rows)
                        {
                            if (gridRow.RowIndex != rFila.RowIndex)
                            {
                                int CodLineaLst = Convert.ToInt32(gvLista.Rows[gridRow.RowIndex].Cells[2].Text);
                                if (lstFiltroGrupo.FindIndex(x => x.cod_linea_aporte == CodLineaLst) >= 0)
                                {
                                    ((CheckBox)gridRow.FindControl("chkActivar")).Checked = IsSelected;
                                }
                            }
                        }
                    }
                        
                }
            }
        }
    }
}