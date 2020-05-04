using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Util;

public partial class Page_Aportes_Personas_Tabs_Laboral : GlobalWeb //System.Web.UI.Page
{
    Persona1Service ServicePersona = new Persona1Service();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            cargarListas();
            if (Session[Usuario.codusuario + "cod_per"] != null)
            {
                cargar_datos();
            }
        }
        if (Session["pensionado"] != null && Session["pensionado"].ToString() == "3")
        {
            string scriptdatos = @"
function datos(){
           var elements = window.parent.document.formularios.iframeLaboral.contentDocument.getElementsByClassName('datoEmpleado');
            for(var i = 0; i < elements.length; i++)
            {
                elements[i].style.display = 'none';
            }
 }     datos();   ";

            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "form", scriptdatos, true);            
        }
    }
    

    public void cargarListas()
    {
        try
        {
            String ListaSolicitada = null;
            List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
            List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosFiltrado = new List<Xpinn.FabricaCreditos.Entities.Persona1>();

            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];

                //Listar tipo empresa
                //ddlTipoEmpresa.Items.Insert(0, new ListItem("Seleccione un item", "0"));
                ddlTipoEmpresa.Items.Insert(1, new ListItem("Pública", "1"));
                ddlTipoEmpresa.Items.Insert(2, new ListItem("Privada", "2"));
                ddlTipoEmpresa.Items.Insert(3, new ListItem("Mixta", "3"));
                ddlTipoEmpresa.SelectedIndex = 0;
                ddlTipoEmpresa.DataBind();

                //Listar Actividades Negocio
                ListaSolicitada = "Actividad_Negocio";
                lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
                ddlActividadE0.DataSource = lstDatosSolicitud;
                ddlActividadE0.DataTextField = "ListaDescripcion";
                ddlActividadE0.DataValueField = "ListaIdStr";
                ddlActividadE0.DataBind();

                //Listar Cargos
                ListaSolicitada = "TipoCargo";
                lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
                ddlCargo.DataSource = lstDatosSolicitud;
                ddlCargo.DataTextField = "ListaDescripcion";
                ddlCargo.DataValueField = "ListaId";
                ddlCargo.AppendDataBoundItems = true;
                //ddlCargo.Items.Insert(0, new ListItem("Seleccione un item", "0"));
                ddlCargo.SelectedIndex = 0;
                ddlCargo.DataBind();

                //Listar Cargos
                ListaSolicitada = "TipoContrato";
                lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
                ddlTipoContrato.DataSource = lstDatosSolicitud;
                ddlTipoContrato.DataTextField = "ListaDescripcion";
                ddlTipoContrato.DataValueField = "ListaId";
                ddlTipoContrato.DataBind();

                //Listar Sector
                ListaSolicitada = "Sector";
                lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
                ddlSector.DataSource = lstDatosSolicitud;
                ddlSector.DataTextField = "ListaDescripcion";
                ddlSector.DataValueField = "ListaId";
                ddlSector.DataBind();

                //Listar Escalafon
                EscalafonSalarialService escalafon = new EscalafonSalarialService();
                EscalafonSalarial escalaf = new EscalafonSalarial();
                List<EscalafonSalarial> lista = new List<EscalafonSalarial>();
                lista = escalafon.ListarEscalafonSalarial("", escalaf, (Usuario)Session["usuario"]);
                ddlescalafon.DataSource = lista;
                ddlescalafon.DataTextField = "grado";
                ddlescalafon.DataValueField = "idescalafon";
                //ddlescalafon.Items.Insert(0, new ListItem("Seleccione un item", "0"));
                ddlescalafon.SelectedIndex = 0;
                ddlescalafon.DataBind();

                //Listar Ciudades - Ubicacion de la empresa
                ListaSolicitada = "Ciudades";
                lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
                ddlCiu0.DataSource = lstDatosSolicitud;
                ddlCiu0.DataTextField = "ListaDescripcion";
                ddlCiu0.DataValueField = "ListaId";
                ddlCiu0.DataBind();

                //Carga la lista de CIIU
                ListaSolicitada = "Actividad2";
                lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
                ViewState["DTACTIVIDAD" + pUsuario.codusuario] = lstDatosSolicitud;
                gvActEmpresa.DataSource = lstDatosSolicitud;
                gvActEmpresa.DataBind();

                //Listar Empresa Recaudo
                PersonaEmpresaRecaudoServices perempresaServicio = new PersonaEmpresaRecaudoServices();
                List<PersonaEmpresaRecaudo> lstEmpresaRecaudo = new List<PersonaEmpresaRecaudo>();
                lstEmpresaRecaudo = perempresaServicio.ListarEmpresaRecaudo(false, (Usuario)Session["Usuario"]);
                lstEmpresaRecaudo = lstEmpresaRecaudo.OrderBy(x => x.nom_empresa).ToList();
                gvEmpresaRecaudo.DataSource = lstEmpresaRecaudo;
                gvEmpresaRecaudo.DataBind();
                lblempresas.Visible = false;
                Session["EmpresaRecaudo"] = lstEmpresaRecaudo;

        }
        catch (Exception ex)
        {
            lblerror.Text = ex.Message;
        }
    }
    public void cargarActEco(GridViewRow rFila, string codigo)
    {
        CheckBoxGrid chkPrincipal = rFila.FindControl("chkPrincipal") as CheckBoxGrid;
        chkPrincipal.Checked = true;
        Label lblDescripcion = (Label)rFila.FindControl("lbl_descripcion");
        txtCIIUEmpresa.Text = lblDescripcion.Text;
        hfActEmpresa.Value = codigo;
    }
    public void cargar_datos()
    {
        Persona1 Entidad = new Persona1();
        if (Session[Usuario.codusuario + "cod_per"] != null)
        {

            string id = (string)Session[Usuario.codusuario + "cod_per"];

            //if (Session["Persona"] != null)
            //
            //    Entidad = (Persona1)Session["Persona"];
            //}
            //else
            //{
            if (Convert.ToInt64(id) != 0)
            {
                Entidad.cod_persona = Convert.ToInt64(id);
                Entidad.seleccionar = "Cod_persona";
                Entidad.soloPersona = 1;
                Entidad = ServicePersona.ConsultarPersona1Param(Entidad, (Usuario)Session["usuario"]);
            }
            if (Entidad != null)
            {
                PersonaEmpresaRecaudoServices infoEmpresaRecaudo = new PersonaEmpresaRecaudoServices();
                List<PersonaEmpresaRecaudo> LstEmpresaRecaudo = new List<PersonaEmpresaRecaudo>();
                LstEmpresaRecaudo = infoEmpresaRecaudo.ListarPersonaEmpresaRecaudo(Convert.ToInt64(id), (Usuario)Session["usuario"]);
                if (LstEmpresaRecaudo.Count > 0)
                {
                    lblempresas.Visible = true;
                    gvEmpresaRecaudo.DataSource = LstEmpresaRecaudo;
                    gvEmpresaRecaudo.DataBind();
                }
                else
                {
                    lblempresas.Visible = false;
                }

                foreach (GridViewRow rFila in gvEmpresaRecaudo.Rows)
                {
                    CheckBoxGrid chkSeleccionar = (CheckBoxGrid)rFila.FindControl("chkSeleccionar");
                    if (chkSeleccionar != null && chkSeleccionar.Checked == true)
                    {
                        chkSeleccionar_CheckedChanged(chkSeleccionar, null);
                    }
                }

                    if (Entidad.tipo_empresa != 0 && Entidad.tipo_empresa != null)
                        ddlTipoEmpresa.SelectedValue = Entidad.tipo_empresa.ToString();
                    if (!string.IsNullOrEmpty(Entidad.empresa))
                        txtEmpresa.Text = HttpUtility.HtmlDecode(Entidad.empresa.ToString().Trim());
                    if (Entidad.nit_empresa != 0 && Entidad.nit_empresa != null)
                        txtNitEmpresa.Text = HttpUtility.HtmlDecode(Entidad.nit_empresa.ToString().Trim());
                    //Cargar actividad empresa
                    Label lblCodigo;
                    foreach (GridViewRow rFila in gvActEmpresa.Rows)
                    {
                        lblCodigo = (Label)rFila.FindControl("lbl_codigo");
                        //Identificar la actividad principal
                        try
                        {
                            if (Convert.ToInt32(lblCodigo.Text) == Convert.ToInt32(Entidad.act_ciiu_empresa))
                            {
                                cargarActEco(rFila, Convert.ToString(Convert.ToInt32(lblCodigo.Text)));
                            }
                        }
                        catch (Exception ex)
                        {
                            if(ex.Message== "La cadena de entrada no tiene el formato correcto.")
                            {
                                if (lblCodigo.Text == Entidad.act_ciiu_empresa)
                                {
                                    cargarActEco(rFila, lblCodigo.Text);
                                }
                            }
                        }
                    }

                    if (Entidad.ActividadEconomicaEmpresa > 0)
                        ddlActividadE0.SelectedValue = Entidad.ActividadEconomicaEmpresa.ToString();
                    if (Entidad.codcargo != 0)
                        ddlCargo.SelectedValue = Entidad.codcargo.ToString();
                    if (Entidad.codtipocontrato != 0)
                        ddlTipoContrato.SelectedValue = Entidad.codtipocontrato.ToString();
                    if (Entidad.fecha_ingresoempresa != DateTime.MinValue)
                    {
                        txtFechaIngreso.Text = Entidad.fecha_ingresoempresa.ToString("yyyy-MM-dd");
                        txtAntiguedadlugarEmpresa.Text = Convert.ToString(GetMon(Convert.ToDateTime(txtFechaIngreso.Text)));
                    }
                    if (Entidad.antiguedadlugarempresa != Int64.MinValue && Entidad.antiguedadlugarempresa != 0)
                        txtAntiguedadlugarEmpresa.Text = Entidad.antiguedadlugarempresa.ToString().Trim();
                    if (Entidad.sector != null && Entidad.sector != 0)
                        ddlSector.SelectedValue = Entidad.sector.ToString();
                    if (!string.IsNullOrEmpty(Entidad.cod_nomina_empleado))
                        txtCodigoEmpleado.Text = Entidad.cod_nomina_empleado.ToString().Trim();
                    if (Entidad.idescalafon != 0)
                        ddlescalafon.SelectedValue = Entidad.idescalafon.ToString();
                    if (Entidad.empleado_entidad != null && Entidad.empleado_entidad != 0)
                        chkEmpleadoEntidad.Checked = true;
                    if (Entidad.jornada_laboral != null)
                        rblJornadaLaboral.SelectedValue = Entidad.jornada_laboral.ToString();
                    if (Entidad.ubicacion_empresa != 0 && Entidad.ubicacion_empresa != null)
                        ddlTipoUbicEmpresa.SelectedValue = Entidad.ubicacion_empresa.ToString();
                    if (Entidad.direccionempresa != "" && Entidad.direccionempresa != null)
                        txtDireccionEmpresa.Text = Entidad.direccionempresa.ToString().Trim();
                    if (Entidad.ciudad != null && Entidad.ciudad != 0)
                        ddlCiu0.SelectedValue = Entidad.ciudad.ToString();
                    if (Entidad.CelularEmpresa != "")
                        txtTelCell0.Text = Entidad.CelularEmpresa;
                    //if (Entidad.email_empresa != "" && Entidad.email_empresa != null)
                    //    txtWebEmp.Text = Entidad.email_empresa.ToString().Trim();
                    if (!string.IsNullOrEmpty(Entidad.telefonoempresa))
                        txtTelefonoempresa.Text = Entidad.telefonoempresa.ToString().Trim();
                }
            
        }
    }
    private List<Xpinn.FabricaCreditos.Entities.Persona1> TraerResultadosLista(string ListaSolicitada)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = persona1Servicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
        return lstDatosSolicitud;
    }
    protected void imgBuscar2_Click(object sender, ImageClickEventArgs e)
    {
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        if (ViewState["DTACTIVIDAD" + pUsuario.codusuario] != null)
        {
            List<Persona1> lstActividad = (List<Persona1>)ViewState["DTACTIVIDAD" + pUsuario.codusuario];
            if (lstActividad != null)
            {
                if (!string.IsNullOrEmpty(txtBuscarCodigo2.Text.Trim()) && !string.IsNullOrEmpty(txtBuscarDescripcion2.Text.Trim()))
                    lstActividad = lstActividad.Where(x => x.ListaIdStr.Contains(txtBuscarCodigo2.Text) || x.ListaDescripcion.Contains(txtBuscarDescripcion2.Text)).ToList();
                else if (!string.IsNullOrEmpty(txtBuscarCodigo2.Text.Trim()))
                    lstActividad = lstActividad.Where(x => x.ListaIdStr.Contains(txtBuscarCodigo2.Text)).ToList();
                else if (!string.IsNullOrEmpty(txtBuscarCodigo2.Text.Trim()))
                    lstActividad = lstActividad.Where(x => x.ListaDescripcion.Contains(txtBuscarDescripcion2.Text)).ToList();
                gvActEmpresa.DataSource = lstActividad;
                gvActEmpresa.DataBind();
            }
        }
        MostrarModal2();
    }
    private void MostrarModal2()
    {
        var ahh = PopupControlExtender3.ClientID;
        var script = @"Sys.Application.add_load(function() { $find('" + ahh + "').showPopup(); });";
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPopup", script, true);
    }
    protected void gvActEmpresa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkPrincipal = (CheckBox)e.Row.FindControl("chkPrincipal");
            Label lbl_codigo = (Label)e.Row.FindControl("lbl_codigo");
            Label lblDescripcion = (Label)e.Row.FindControl("lbl_descripcion");
            chkPrincipal.Attributes.Add("onclick", "MostrarCIIUPrincipalEmp('" + lbl_codigo.Text + "','" + lblDescripcion.Text + "')");
        }
    }
    protected List<PersonaEmpresaRecaudo> ObtenerListaEmpresaRecaudo()
    {
        List<PersonaEmpresaRecaudo> lstEmpresaRecaudo = new List<PersonaEmpresaRecaudo>();
        List<PersonaEmpresaRecaudo> lista = new List<PersonaEmpresaRecaudo>();

        foreach (GridViewRow rfila in gvEmpresaRecaudo.Rows)
        {
            PersonaEmpresaRecaudo eActi = new PersonaEmpresaRecaudo();
            Label lblidempresarecaudo = (Label)rfila.FindControl("lblidempresarecaudo");
            if (lblidempresarecaudo != null)
                if (lblidempresarecaudo.Text != "")
                    eActi.idempresarecaudo = Convert.ToInt64(lblidempresarecaudo.Text);
            Label lblcodempresa = (Label)rfila.FindControl("lblcodempresa");
            if (lblcodempresa != null)
                eActi.cod_empresa = Convert.ToInt64(lblcodempresa.Text);
            Label lblDescripcion = (Label)rfila.FindControl("lblDescripcion");
            if (lblDescripcion != null)
                eActi.descripcion = lblDescripcion.Text;
            CheckBox chkSeleccionar = (CheckBox)rfila.FindControl("chkSeleccionar");
            if (chkSeleccionar != null)
                eActi.seleccionar = chkSeleccionar.Checked;

            lstEmpresaRecaudo.Add(eActi);
            lista.Add(eActi);
            Session["EmpresaRecaudo"] = lista;
        }
        return lstEmpresaRecaudo;
    }
    protected void ordenarPagadurias(object sender, EventArgs e)
    {
        PersonaEmpresaRecaudoServices perempresaServicio = new PersonaEmpresaRecaudoServices();
        List<PersonaEmpresaRecaudo> lstEmpresaRecaudo = new List<PersonaEmpresaRecaudo>();
        if (ckOrdenarPagadurias.Checked)
        {
            lstEmpresaRecaudo = perempresaServicio.ListarEmpresaRecaudo(true, (Usuario)Session["Usuario"]);
        }
        else
        {
            lstEmpresaRecaudo = perempresaServicio.ListarEmpresaRecaudo(false, (Usuario)Session["Usuario"]);
        }
        gvEmpresaRecaudo.DataSource = lstEmpresaRecaudo;
        gvEmpresaRecaudo.DataBind();
        lblempresas.Visible = false;
        Session["EmpresaRecaudo"] = lstEmpresaRecaudo;
    }
    protected void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
    {
        CheckBoxGrid chkSeleccionar = (CheckBoxGrid)sender;
        int nItem = Convert.ToInt32(chkSeleccionar.CommandArgument);

        foreach (GridViewRow rFila in gvEmpresaRecaudo.Rows)
        {
            //RECUPERANDO CAMPO SELECCIONADO           
            if (rFila.RowIndex == nItem)
            {
                //RECUPERANDO EL CODIGO DE LA EMPRESA
                Label lblcodempresa = (Label)rFila.FindControl("lblcodempresa");
                if (lblcodempresa.Text != "")
                {
                    int cont = 0;
                    //CONSULTANDO LAS EMPRESAS EXCLUYENTES ===> (COD_EMPRESA) 
                    List<Xpinn.Tesoreria.Entities.EmpresaExcluyente> lstExcluyente = new List<Xpinn.Tesoreria.Entities.EmpresaExcluyente>();
                    Xpinn.Tesoreria.Services.EmpresaExcluyenteServices EmpresaExclu = new Xpinn.Tesoreria.Services.EmpresaExcluyenteServices();
                    lstExcluyente = EmpresaExclu.ListarEmpresaExcluyente(Convert.ToInt32(lblcodempresa.Text), (Usuario)Session["usuario"]);
                    if (lstExcluyente.Count > 0)
                    {
                        foreach (Xpinn.Tesoreria.Entities.EmpresaExcluyente Excluyente in lstExcluyente)
                        {
                            //Filtrando solo excluyentes de la empresa seleccionada
                            if (Excluyente.cod_empresa_excluye != null && Excluyente.cod_empresa_excluye != 0)
                            {
                                //Validando que las excluyentes no esten seleccionadas
                                foreach (GridViewRow valida in gvEmpresaRecaudo.Rows)
                                {
                                    Label lblcod_empresa = (Label)valida.FindControl("lblcodempresa");
                                    CheckBoxGrid check = (CheckBoxGrid)valida.FindControl("chkSeleccionar");

                                    if (lblcodempresa.Text != "")
                                        if (Excluyente.cod_empresa_excluye == Convert.ToInt32(lblcod_empresa.Text))
                                        {
                                            cont++;
                                            if (chkSeleccionar.Checked)
                                                check.Visible = false;
                                            else
                                                check.Visible = true;
                                            if (check.Checked)
                                            {
                                                check.Checked = false;
                                                break;
                                            }
                                        }
                                }
                            }
                        }
                    }
                    //CONSULTANDO LAS EMPRESAS EXCLUYENTES ===> (COD_EMPRESA_EXCLUYE)
                    if (cont == 0) // SI NO SE ENCONTRARON REGISTROS POR COD_EMPRESA
                    {
                        lstExcluyente = EmpresaExclu.ListarEmpresaExcluyenteINV(Convert.ToInt32(lblcodempresa.Text), (Usuario)Session["usuario"]);
                        if (lstExcluyente.Count > 0)
                        {
                            foreach (Xpinn.Tesoreria.Entities.EmpresaExcluyente INVExcluye in lstExcluyente)
                            {
                                //Filtrando solo excluyentes de la empresa seleccionada
                                if (INVExcluye.cod_empresa_excluye != null && INVExcluye.cod_empresa_excluye != 0)
                                {
                                    //Validando que las excluyentes no esten seleccionadas
                                    foreach (GridViewRow valida in gvEmpresaRecaudo.Rows)
                                    {
                                        Label lblcod_empresa = (Label)valida.FindControl("lblcodempresa");
                                        CheckBoxGrid check = (CheckBoxGrid)valida.FindControl("chkSeleccionar");

                                        if (lblcodempresa.Text != "")
                                            if (INVExcluye.cod_empresa == Convert.ToInt32(lblcod_empresa.Text))
                                            {
                                                if (chkSeleccionar.Checked)
                                                    check.Visible = false;
                                                else
                                                    check.Visible = true;
                                                if (check.Checked)
                                                {
                                                    check.Checked = false;
                                                    break;
                                                }
                                            }
                                    }
                                }
                            }
                        }

                    }

                }
                break;
            }
        }
    }

    public static int GetMon(DateTime fechaingre)
    {
        return (int)Math.Floor((DateTime.Now - fechaingre).TotalDays / 12);
    }
    public void btnGuardarLaboral_click(object sender, EventArgs e)
    {
        if(Session["pensionado"] != null && Session["pensionado"].ToString() == "3")
        {
            Session["gvEmpresaRecaudo"] = null;
            Usuario pUsuario = (Usuario)Session["usuario"];
            Persona1 Entidad = new Persona1();

            Entidad.lstEmpresaRecaudo = new List<PersonaEmpresaRecaudo>();
            Entidad.lstEmpresaRecaudo = ObtenerListaEmpresaRecaudo();
            Entidad.ocupacionApo = Convert.ToInt32(Session["pensionado"].ToString());
            Session["gvEmpresaRecaudo"] = gvEmpresaRecaudo;

            // Guarda/Modifica
            if (Session[Usuario.codusuario + "cod_per"] != null)
            {
                Entidad.fecultmod = Convert.ToDateTime(DateTime.Now.ToString(GlobalWeb.gFormatoFecha));
                Entidad.usuultmod = pUsuario.nombre;
                try
                {
                    Entidad.cod_persona = Convert.ToInt64(Session[Usuario.codusuario + "cod_per"].ToString());
                    ServicePersona.TabLaboral(Entidad, pUsuario);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Guardado", "alert('Guardo la Información Laboral')", true);
                }
                catch (Exception ex)
                {
                    lblerror.Text = ("Error al modificar los datos de la información Laboral. " + ex.Message);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Error", "alert('Error al modificar los datos de la información Laboral.')", true);
                    return;
                }
                Session[Usuario.codusuario + "cod_per"] = Entidad.cod_persona;
            }
            else
            {
                lblerror.Text = ("No se encontro se pudo realizar el registro o actualizacion de los datos laborales");
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Error2", "alert('No se pudo realizar el registro o actualizacion de los datos laborales.')", true);
                return;
            }            
        }
        else
        {
            Session["gvEmpresaRecaudo"] = null;
            Usuario pUsuario = (Usuario)Session["usuario"];
            Persona1 Entidad = new Persona1();

            try
            {
                Entidad.tipo_empresa = Convert.ToInt32(ddlTipoEmpresa.SelectedValue.ToString());
                Entidad.empresa = txtEmpresa.Text.ToString();
                if (txtNitEmpresa.Text != "")
                    Entidad.nit_empresa = Convert.ToInt64(txtNitEmpresa.Text.ToString());
                Entidad.act_ciiu_empresa = hfActEmpresa.Value != null && hfActEmpresa.Value != "" ? hfActEmpresa.Value : null;
                Entidad.ActividadEconomicaEmpresaStr = ddlActividadE0.SelectedValue.ToString();
                Entidad.lstEmpresaRecaudo = new List<PersonaEmpresaRecaudo>();
                Entidad.lstEmpresaRecaudo = ObtenerListaEmpresaRecaudo();
                Session["gvEmpresaRecaudo"] = gvEmpresaRecaudo;

                Entidad.codcargo = Convert.ToInt64(ddlCargo.SelectedValue.ToString());
                Entidad.codtipocontrato = Convert.ToInt64(ddlTipoContrato.SelectedValue.ToString());
                Entidad.fecha_ingresoempresa = Convert.ToDateTime(txtFechaIngreso.Text);
                Entidad.antiguedadlugarempresa = txtAntiguedadlugarEmpresa.Text != "" ? Convert.ToInt64(txtAntiguedadlugarEmpresa.Text.ToString()) : 0;
                Entidad.sector = Convert.ToInt64(ddlSector.SelectedValue.ToString());
                Entidad.cod_nomina_empleado = txtCodigoEmpleado.Text.ToString();
                if (ddlescalafon.SelectedValue == "") { Entidad.idescalafon = 0; }
                else
                {
                    Entidad.idescalafon = Convert.ToInt32(ddlescalafon.SelectedValue);
                }
                Entidad.empleado_entidad = chkEmpleadoEntidad.Checked ? 1 : 0;
                Entidad.jornada_laboral = Convert.ToInt32(rblJornadaLaboral.SelectedValue);
                Entidad.ubicacion_empresa = Convert.ToInt32(ddlTipoUbicEmpresa.SelectedValue);
                Entidad.direccionempresa = txtDireccionEmpresa.Text.ToString();
                Entidad.ciudad = Convert.ToInt64(ddlCiu0.SelectedValue);
                Entidad.CelularEmpresa = txtTelCell0.Text.ToString();
                //Entidad.email_empresa = txtWebEmp.Text.ToString();
                Entidad.telefonoempresa = txtTelefonoempresa.Text.ToString();

                // Guarda/Modifica
                if (Session[Usuario.codusuario + "cod_per"] != null)
                {
                    Entidad.fecultmod = Convert.ToDateTime(DateTime.Now.ToString(GlobalWeb.gFormatoFecha));
                    Entidad.usuultmod = pUsuario.nombre;
                    try
                    {
                        Entidad.cod_persona = Convert.ToInt64(Session[Usuario.codusuario + "cod_per"].ToString());
                        ServicePersona.TabLaboral(Entidad, pUsuario);
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Guardado", "alert('Guardo la Información Laboral')   ", true);
                    }
                    catch (Exception ex)
                    {
                        lblerror.Text = ("Error al modificar los datos de la información Laboral. " + ex.Message);
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Error", "alert('Error al modificar los datos de la información Laboral.')   ", true);
                        return;
                    }
                    Session[Usuario.codusuario + "cod_per"] = Entidad.cod_persona;
                }
                else
                {
                    lblerror.Text = ("No se encontro se pudo realizar el registro o actualizacion de los datos laborales");
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Error2", "alert('No se pudo realizar el registro o actualizacion de los datos laborales.')    ", true);
                    return;
                }
            } catch(Exception)
            {
                lblerror.Text = ("No se realizó el registro o actualizacion de los datos laborales");
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Error2", "alert('No se realizó el registro o actualizacion de los datos laborales.')", true);
                return;
            }            
        }                 
    }
}
