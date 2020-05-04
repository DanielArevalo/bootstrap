using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.IO;


public partial class Nuevo : GlobalWeb
{
    PolizasSegurosService polizasSegurosServicio = new PolizasSegurosService();
    PlanesSegurosService planSegurosServicio = new PlanesSegurosService();
    PlanesSegurosAmparosService planSegurosAmparosServicio = new PlanesSegurosAmparosService();
    TomadorPolizaService tomadorPolizaServicio = new TomadorPolizaService();
    BeneficiariosService beneficiariosPolizasServicio = new BeneficiariosService();
    FamiliaresPolizasService familiaresPolizasServicio = new FamiliaresPolizasService();
    ParentescoService parentescoServicio = new ParentescoService();
    String operacion = "";
    long ValorPoliza = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {
                LlenarComboTipoPlan(this.ddlTipoPlanSegVida);
                LlenarComboTipoPlan(this.ddlTipoPlanAcc);
                MostrarDatosBeneficiarios();
                MostrarDatosFamiliares();
            }


            if (Session[polizasSegurosServicio.CodigoPoliza + ".id"] != null)
            {
                idObjeto = Session[polizasSegurosServicio.CodigoPoliza + ".id"].ToString();

                Session.Remove(polizasSegurosServicio.CodigoPoliza + ".id");
                ObtenerDatos(idObjeto);


            }

            operacion = (String)Session["operacion"];
            if (operacion == null)
            {
                operacion = "";
            }

            String numradica = "";
            numradica = Request["idRadica"];
            if (numradica != null)
            {
                this.TxtNumRadicacion.Text = numradica;
                this.ObtenerDatosCredito(numradica);
            }
            

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(polizasSegurosServicio.GetType().Name + "A", "Page_Load", ex);
        }


    }
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[polizasSegurosServicio.CodigoPoliza + ".id"] != null)
                VisualizarOpciones(polizasSegurosServicio.CodigoPrograma.ToString(), "E");
            else
                VisualizarOpciones(polizasSegurosServicio.CodigoPrograma.ToString(), "A");


            Site toolBar = (Site)this.Master;

            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(polizasSegurosServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        Session["Beneficiarios"] = null;
        Session["FamiliaresPolizas"] = null;
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Session[polizasSegurosServicio.CodigoPoliza + ".id"] = idObjeto;
            Session["Beneficiarios"] = null;
            Session["FamiliaresPolizas"] = null;
            Session["operacion"] = "";
            Navegar(Pagina.Lista);
        }
    }


    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
       ConsultarFamiliares();
       ConsultarBeneficiarios();
       Navegar("~/Page/FabricaCreditos/PolizasSeguros/Filtrar.aspx");
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            polizasSegurosServicio.EliminarPolizasSeguros(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(polizasSegurosServicio.CodigoPoliza + "C", "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[polizasSegurosServicio.CodigoPoliza + ".id"] = idObjeto;
        Navegar(Pagina.Nuevo);
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {

        if (VerificarDatosVacios())
        {
            if (VerificarPolizavacia())
            {
            if (VerificarDatosVaciosCombos())
            {

                if (VerificarFechas())
                {
                    if (VerificarPlaSegVida())
                    {

                        if (this.VerificarFechaNamiento())
                        {

                            if (this.VerificarValorPrima())
                            {

                                if (this.validarreglasbeneficiariosGrabar())
                                {
                                    this.grabar();
                                }
                            }
                        }
                    }
                    }
                }
            }
        }

        quitarfilainicialbeneficiarios();
        quitarfilainicialfamiliares();
    }

 
    protected void grabar()
    {

        try
        {
            PolizasSeguros polizasSeguros = new PolizasSeguros();
            PolizasSegurosVida polizasSegurosVida = new PolizasSegurosVida();

            if (idObjeto != "")
            {
                polizasSeguros = polizasSegurosServicio.ConsultarPolizasSeguros(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            }

            polizasSeguros.cod_poliza = 0;
            polizasSeguros.numero_radicacion = (Int64.Parse(TxtNumRadicacion.Text));
            polizasSeguros.num_poliza = (Int64.Parse(this.txtnumpolizafisica.Text.ToString()));
            polizasSeguros.cod_asegurado = (Int64.Parse(Txtcodasegurado.Text));
            polizasSeguros.icodigo = (Int64.Parse(Txtcodasesor.Text));            

            if (TxtVigenPolDesde.Text != "") polizasSeguros.fec_ini_vig = Convert.ToDateTime(TxtVigenPolDesde.Text.Trim());
            if (TxtVigenPolHasta.Text != "") polizasSeguros.fec_fin_vig = Convert.ToDateTime(TxtVigenPolHasta.Text.Trim());
           
            Txtindividual.Text = polizasSeguros.individual.ToString();
            if (VerificarValorPrima())
            {
                polizasSeguros.valor_prima_mensual = (Int64.Parse(TxtValorPrimaMensual.Text));
            }
            polizasSeguros.valor_prima_total = polizasSegurosServicio.CalcularMeses(polizasSeguros.fec_ini_vig, polizasSeguros.fec_fin_vig) * polizasSeguros.valor_prima_mensual;

            if ((chkprimaindividual.Checked) || (this.chkprimaacc.Checked) && TxtValorPrimaMensual.Text == "0")
            {
                polizasSeguros.individual = 1;

            }
            if ((chkprimacony.Checked) || (this.chkprimaopfam.Checked) && TxtValorPrimaMensual.Text == "0")
            {
                polizasSeguros.individual = 0;

            }
            if (idObjeto != "")
            {
                polizasSeguros.cod_poliza = Convert.ToInt64(idObjeto);

                polizasSegurosServicio.ModificarPolizasSeguros(polizasSeguros, (Usuario)Session["usuario"]);
                polizasSegurosVida.cod_poliza = Convert.ToInt64(idObjeto);
                polizasSegurosVida.tipo = "Vida Grupo";
                polizasSegurosVida.tipo_plan = Int64.Parse(this.ddlTipoPlanSegVida.SelectedValue);
                if (chkprimaindividual.Checked)
                {
                    polizasSegurosVida.individual = "1";
                    polizasSegurosVida.valor_prima = Convert.ToInt64(txtPrimaindividual.Text);

                }
                if (this.chkprimacony.Checked)
                {
                    polizasSegurosVida.individual = "0";
                    polizasSegurosVida.valor_prima = Convert.ToInt64(this.txtPrimacony.Text);

                }

                polizasSegurosServicio.ModificarPolizasSegurosVida(polizasSegurosVida, (Usuario)Session["usuario"]);

                polizasSegurosVida.tipo = "Accidentes Personales";
                polizasSegurosVida.cod_poliza = Convert.ToInt64(idObjeto);
                polizasSegurosVida.tipo_plan = Int64.Parse(this.ddlTipoPlanAcc.SelectedValue);
                if (this.chkprimaacc.Checked)
                {
                    polizasSegurosVida.individual = "1";
                    polizasSegurosVida.valor_prima = Convert.ToInt64(this.txtPrimaacc.Text);

                }
                if (this.chkprimaopfam.Checked)
                {
                    polizasSegurosVida.individual = "0";
                    polizasSegurosVida.valor_prima = Convert.ToInt64(this.txtPrimaopfam.Text);

                }
                polizasSegurosServicio.ModificarPolizasSegurosVida(polizasSegurosVida, (Usuario)Session["usuario"]);
                if (polizasSegurosVida.tipo_plan > 0)
                {

                    polizasSegurosVida = polizasSegurosServicio.CrearPolizasSegurosVida(polizasSegurosVida, (Usuario)Session["usuario"]);
                }
            }

            else
            {

                if (polizasSeguros.valor_prima_mensual > 0)
                {



                    polizasSeguros = polizasSegurosServicio.CrearPolizasSeguros(polizasSeguros, (Usuario)Session["usuario"]);
                    polizasSegurosVida.cod_poliza = polizasSeguros.cod_poliza;

                    if (polizasSeguros != null)
                    {
                        if (polizasSeguros.cod_poliza > 0)
                        {
                            GuardarFamiliares(polizasSeguros.cod_poliza);
                            GuardarBeneficiarios(polizasSeguros.cod_poliza);

                        }
                    }
                    polizasSegurosVida.tipo = "Vida Grupo";
                    polizasSegurosVida.tipo_plan = Int64.Parse(this.ddlTipoPlanSegVida.SelectedValue);
                    if (chkprimaindividual.Checked)
                    {
                        polizasSegurosVida.individual = "1";
                        polizasSegurosVida.valor_prima = Convert.ToInt64(txtPrimaindividual.Text);

                    }
                    if (this.chkprimacony.Checked)
                    {
                        polizasSegurosVida.individual = "0";
                        polizasSegurosVida.valor_prima = Convert.ToInt64(this.txtPrimacony.Text);

                    }

                    if (polizasSegurosVida.tipo_plan > 0)
                    {

                        polizasSegurosVida = polizasSegurosServicio.CrearPolizasSegurosVida(polizasSegurosVida, (Usuario)Session["usuario"]);
                    }

                    polizasSegurosVida.tipo = "Accidentes Personales";
                    polizasSegurosVida.tipo_plan = Int64.Parse(this.ddlTipoPlanAcc.SelectedValue);
                    if (this.chkprimaacc.Checked)
                    {
                        polizasSegurosVida.individual = "1";
                        polizasSegurosVida.valor_prima = Convert.ToInt64(this.txtPrimaacc.Text);

                    }
                    if (this.chkprimaopfam.Checked)
                    {
                        polizasSegurosVida.individual = "0";
                        polizasSegurosVida.valor_prima = Convert.ToInt64(this.txtPrimaopfam.Text);

                    }

                    if (polizasSegurosVida.tipo_plan > 0)
                    {

                        polizasSegurosVida = polizasSegurosServicio.CrearPolizasSegurosVida(polizasSegurosVida, (Usuario)Session["usuario"]);

                    }

                }
            }

            Session["Beneficiarios"] = null;
            Session["FamiliaresPolizas"] = null;

            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(polizasSegurosServicio.GetType().Name, "btnGuardar_Click", ex);
        }
        
     

    }


    protected void gvBeneficiarios_RowEditing(object sender, GridViewEditEventArgs e)
    {

        // quitarfilainicialbeneficiarios();
        int conseID = Convert.ToInt32(gvBeneficiarios.DataKeys[e.NewEditIndex].Values[0].ToString());
        if (conseID != 0)
        {
            gvBeneficiarios.EditIndex = e.NewEditIndex;
            // this.MostrarDatosBeneficiarios();
            //if (!operacion.Equals("N"))
            //{
            this.ConsultarBeneficiarios();
            //}

            String parentesco = "";
            parentesco = this.buscarParentesco(conseID);
            DropDownList ddlparentescoedit = new DropDownList();
            ddlparentescoedit = gvBeneficiarios.Rows[e.NewEditIndex].Cells[1].FindControl("ddlparentescoedit") as DropDownList;
            if (ddlparentescoedit != null)
            {
                ddlparentescoedit.SelectedValue = parentesco;
            }                     
        }
        else
        {
            e.Cancel = true;
        }
        //quitarfilainicialbeneficiarios();
    }

    protected void gvBeneficiarios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvBeneficiarios.EditIndex = -1;     
        long codpoliza = Convert.ToInt64("0" + lblcodpoliza.Text);
        ConsultarBeneficiarios();     
    }

    private void quitarfilainicialbeneficiarios()
    {
        
        try
        {
            int conseID = Convert.ToInt32(gvBeneficiarios.DataKeys[0].Values[0].ToString());
            if (conseID <= 0)
            {
                ImageButton link = (ImageButton)this.gvBeneficiarios.Rows[0].Cells[0].FindControl("btnEditar");

                link.Enabled = false;

                link.Visible = false;

                this.gvBeneficiarios.Rows[0].Cells[1].Text = "";
                this.gvBeneficiarios.Rows[0].Cells[2].Text = "";
                this.gvBeneficiarios.Rows[0].Cells[3].Visible = false;
                this.gvBeneficiarios.Rows[0].Cells[4].Visible = false;
                this.gvBeneficiarios.Rows[0].Cells[5].Text = "";
                this.gvBeneficiarios.Rows[0].Cells[5].Visible = false;
                this.gvBeneficiarios.Rows[0].Cells[6].Visible = false;
                //this.gvBeneficiarios.Rows[0].Cells[7].Visible = false;
                // this.gvBeneficiarios.Rows[0].Cells[8].Visible = false;


            }
        }
        catch 
        {
        }

    }

    private void quitarfilainicialfamiliares()
    {


        try
        {
            int conseID = Convert.ToInt32(gvFamiliarespolizas.DataKeys[0].Values[0].ToString());
            if (conseID <= 0)
            {
                ImageButton link = (ImageButton)this.gvFamiliarespolizas.Rows[0].Cells[0].FindControl("btnEditar");

                link.Enabled = false;

                link.Visible = false;

                this.gvFamiliarespolizas.Rows[0].Cells[1].Text = "";
                this.gvFamiliarespolizas.Rows[0].Cells[2].Text = "";
                this.gvFamiliarespolizas.Rows[0].Cells[3].Visible = false;
                this.gvFamiliarespolizas.Rows[0].Cells[4].Visible = false;
                this.gvFamiliarespolizas.Rows[0].Cells[5].Visible = false;
                this.gvFamiliarespolizas.Rows[0].Cells[6].Visible = false;
                this.gvFamiliarespolizas.Rows[0].Cells[7].Visible = false;
                //  this.gvFamiliarespolizas.Rows[0].Cells[8].Visible = false;



            }
        }
        catch 
        {
        }

    }

    protected void gvBeneficiarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvBeneficiarios.DataKeys[e.RowIndex].Values[0].ToString());

        if (conseID != 0)
        {

            try
            {

                if (this.operacion.Equals("N"))
                {
                    this.eliminarGrupo_Beneficiarios(conseID);

                }

                else
                {

                    if (gvBeneficiarios.Rows.Count > 1)
                    {
                        beneficiariosPolizasServicio.EliminarBeneficiarios(conseID, (Usuario)Session["usuario"]);
                    }

                    this.ConsultarBeneficiarios();
                }

            }

            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(beneficiariosPolizasServicio.consecutivo + "L", "gvBeneficiarios_RowDeleting", ex);
            }

        }
        else
        {
            e.Cancel = true;
        }


        //this.quitarfilainicialbeneficiarios();
    }
    private void eliminarGrupo_Beneficiarios(int conseID)
    {
        List<Beneficiarios> LstBeneficiarios;
        LstBeneficiarios = (List<Beneficiarios>)Session["Beneficiarios"];
        foreach (Beneficiarios beneficiario in LstBeneficiarios)
        {
            if (beneficiario.consecutivo == conseID)
            {
                LstBeneficiarios.Remove(beneficiario);
                break;
            }
        }
        Session["Beneficiarios"] = LstBeneficiarios;
        MostrarDatosBeneficiarios();

    }
    private void eliminarGrupo_Familiares(int conseID)
    {
        List<FamiliaresPolizas> LstFamiliares;
        LstFamiliares = (List<FamiliaresPolizas>)Session["FamiliaresPolizas"];
        foreach (FamiliaresPolizas familiar in LstFamiliares)
        {
            if (familiar.consecutivo == conseID)
            {
                LstFamiliares.Remove(familiar);
                break;
            }
        }
        Session["FamiliaresPolizas"] = LstFamiliares;

        MostrarDatosFamiliares();
    }

    protected void gvBeneficiarios_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void gvBeneficiarios_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }
    protected void gvBeneficiarios_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

      
        TextBox txtnombresyapellidos = (TextBox)gvBeneficiarios.Rows[e.RowIndex].FindControl("txtnombresyapellidos");
        TextBox txtdocumentoidentidad = (TextBox)gvBeneficiarios.Rows[e.RowIndex].FindControl("txtdocumentoidentidad");
        // TextBox txtparentesco = (TextBox)gvBeneficiarios.Rows[e.RowIndex].FindControl("txtparentesco");
        TextBox txtfechanac = (TextBox)gvBeneficiarios.Rows[e.RowIndex].FindControl("txtfechanac");
        TextBox txtporcentaje = (TextBox)gvBeneficiarios.Rows[e.RowIndex].FindControl("txtporcentaje");
        DropDownList ddlparentescoedit = (DropDownList)gvBeneficiarios.Rows[e.RowIndex].FindControl("ddlparentescoedit");
        /// AjaxControlToolkit.CalendarExtender txtcalendar = (AjaxControlToolkit.CalendarExtender)gvBeneficiarios.Rows[e.RowIndex].FindControl("txtfechanac_CalendarExtender");


            if (txtfechanac.Text == "__/__/____" || txtfechanac.Text == "00/00/0000")
            {
                String Error = "Fecha no valida";
                this.Lblerror.Text = Error;
            }
            else

            {
                long conseID = Convert.ToInt32(gvBeneficiarios.DataKeys[e.RowIndex].Values[0].ToString());

                Beneficiarios beneficiarios = new Beneficiarios();
                beneficiarios.consecutivo = conseID;
                beneficiarios.cod_poliza = Convert.ToInt64("0" + lblcodpoliza.Text);
                beneficiarios.nombres = txtnombresyapellidos.Text;
                beneficiarios.identificacion = Convert.ToString(txtdocumentoidentidad.Text);
                beneficiarios.parentesco = ddlparentescoedit.SelectedValue.ToString();
                String format = "dd/MM/yyyy";

                beneficiarios.fecha_nacimiento = DateTime.ParseExact(txtfechanac.Text, format, CultureInfo.InvariantCulture);
   

               // beneficiarios.fecha_nacimiento = Convert.ToDateTime(txtfechanac.Text);

                beneficiarios.porcentaje = Convert.ToInt64(txtporcentaje.Text);

            
                this.Lblerror.Text = "";

                if (beneficiarios.porcentaje == 0)
                {
                    String Error = "El porcentaje no puede ser 0";
                    this.Lblerror.Text = Error;
                    quitarfilainicialbeneficiarios();
                    quitarfilainicialfamiliares();
                }
                DateTime Fechanacimiento = DateTime.ParseExact(txtfechanac.Text, format, CultureInfo.InvariantCulture);
   
                //DateTime Fechanacimiento = DateTime.Parse(txtfechanac.Text);
                Int64 edad = polizasSegurosServicio.CalcularEdad2(Fechanacimiento);
                PolizasSeguros polizasSeguros = new PolizasSeguros();
                polizasSeguros = polizasSegurosServicio.ConsultarParametroEdadBeneficiarios((Usuario)Session["Usuario"]);
                Int64 edadmaxima = polizasSeguros.edad_maxima;


               
               // DateTime dt1 = new DateTime();
                DateTime dt1 = DateTime.ParseExact(txtfechanac.Text, format, CultureInfo.InvariantCulture);
   
               // dt1 = DateTime.Parse(txtfechanac.Text);
                if (dt1.Date > DateTime.Now.Date)
                {
                    String Error1 = "No puede grabar un beneficiario con fecha de nacimiento superior a la actual";
                    this.Lblerror.Text = Error1;
                    quitarfilainicialbeneficiarios();
                    quitarfilainicialfamiliares();

                }
                else
                {
               
                if (edad >= edadmaxima)
                {
                    String Error = "Edad máxima del beneficiario.";
                    this.Lblerror.Text = Error;
                    quitarfilainicialbeneficiarios();
                    quitarfilainicialfamiliares();
                }
                else
                {
                    gvBeneficiarios.EditIndex = -1;

                    if (operacion.Equals("N"))
                    {
                        ActualizarGrupo_Beneficiarios(beneficiarios);
                    }
                    else
                    {

                        beneficiariosPolizasServicio.ModificarBeneficiarios(beneficiarios, (Usuario)Session["usuario"]);
                        this.ConsultarBeneficiarios();

                    }

                    }
                }
                quitarfilainicialbeneficiarios();

            }
       
        
    }
    private void ActualizarGrupo_Beneficiarios(Beneficiarios beneficiarios)
    {
        List<Beneficiarios> LstBeneficiarios;
        LstBeneficiarios = (List<Beneficiarios>)Session["Beneficiarios"];
        foreach (Beneficiarios beneficiario in LstBeneficiarios)
        {
            if (beneficiario.consecutivo == beneficiarios.consecutivo)
            {
                beneficiario.consecutivo = beneficiarios.consecutivo;
                beneficiario.identificacion = beneficiarios.identificacion;
                beneficiario.nombres = beneficiarios.nombres;
                beneficiario.parentesco = beneficiarios.parentesco;
                beneficiario.fecha_nacimiento = beneficiarios.fecha_nacimiento;
                beneficiario.porcentaje = beneficiarios.porcentaje;

                break;
            }
        }
        Session["Beneficiarios"] = LstBeneficiarios;
        MostrarDatosBeneficiarios();
    }
    private void ActualizarGrupo_Familiares(FamiliaresPolizas familiares)
    {
        List<FamiliaresPolizas> LstFamiliaresPolizas;
        LstFamiliaresPolizas = (List<FamiliaresPolizas>)Session["FamiliaresPolizas"];
        foreach (FamiliaresPolizas familiar in LstFamiliaresPolizas)
        {
            if (familiar.consecutivo ==  familiares.consecutivo)
            {
                familiar.consecutivo = familiares.consecutivo;
                familiar.identificacion = familiares.identificacion;
                familiar.nombres = familiares.nombres;
                familiar.parentesco = familiares.parentesco;
                familiar.sexo = familiares.sexo;
                familiar.fecha_nacimiento = familiares.fecha_nacimiento;
                familiar.actividad = familiares.actividad;

                break;
            }
        }
        Session["FamiliaresPolizas"] = LstFamiliaresPolizas;
        MostrarDatosFamiliares();
    }


    protected void gvBeneficiarios_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private Boolean VerificarFechasGrillasben()
    {
        Boolean continuar = true;
        TextBox txtnewfechanac = (TextBox)gvBeneficiarios.FooterRow.FindControl("txtnewfechanac");

        if (txtnewfechanac.Text == "__/__/____")
        {

            String Error = "Fecha no valida";
            this.Lblerror.Text = Error;
            continuar = false;
        }
        
        
       // DateTime dt1 = new DateTime();
         String format = "dd/MM/yyyy";                         

        DateTime dt1 = DateTime.ParseExact(txtnewfechanac.Text, format, CultureInfo.InvariantCulture);
        if (dt1.Date > DateTime.Now.Date)
        {
            String Error1 = "No puede grabar un beneficiario con fecha de nacimiento superior a la actual";
            this.Lblerror.Text = Error1;
            quitarfilainicialbeneficiarios();
            quitarfilainicialfamiliares();
            continuar = false;
        }


        else
        {
            this.Lblerror.Text = "";
        }
        return continuar;

    }
    
    private Boolean VerificarFechasGrillasfam()
    {
        Boolean continuar = true;
        TextBox txtnewfechanacf = (TextBox)gvFamiliarespolizas.FooterRow.FindControl("txtnewfechanacf");
        if (txtnewfechanacf.Text == "__/__/____")
        {

            String Error = "Fecha no valida";
            this.Lblerror.Text = Error;
            continuar = false;
        }
        String format = "dd/MM/yyyy";

        DateTime dt1 = DateTime.ParseExact(txtnewfechanacf.Text, format, CultureInfo.InvariantCulture);
   

        if (dt1.Date > DateTime.Now.Date)
        {
            String Error1 = "No puede grabar un familiar con fecha de nacimiento superior a la actual";
            this.Lblerror.Text = Error1;
            quitarfilainicialbeneficiarios();
            quitarfilainicialfamiliares();
            continuar = false;
        }


        else
        {
            this.Lblerror.Text = "";
        }
        return continuar;

    }
    protected void gvBeneficiarios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtnewnombresyapellidos = (TextBox)gvBeneficiarios.FooterRow.FindControl("txtnewnombresyapellidos");
            TextBox txtnewdocumentoidentidad = (TextBox)gvBeneficiarios.FooterRow.FindControl("txtnewdocumentoidentidad");
            DropDownList ddlparentesco = (DropDownList)gvBeneficiarios.FooterRow.FindControl("ddlparentesco");
            TextBox txtnewfechanac = (TextBox)gvBeneficiarios.FooterRow.FindControl("txtnewfechanac");
            TextBox txtnewporcentaje = (TextBox)gvBeneficiarios.FooterRow.FindControl("txtnewporcentaje");

            List<Beneficiarios> LstBeneficiarios = new List<Beneficiarios>();

            LstBeneficiarios = (List<Beneficiarios>)Session["Beneficiarios"];

         
          // DateTime t1 = DateTime.Parse(txtnewfechanac.Text.ToString());
          
            if (txtnewnombresyapellidos.Text.Trim().Length == 0 || txtnewdocumentoidentidad.Text.Trim().Length == 0 || txtnewporcentaje.Text.Trim().Length == 0 || ddlparentesco.SelectedItem.Text == "")
            {
                String Error = "Debe diligenciar los datos de los beneficiarios";
                this.Lblerror.Text = Error;             
            
                if (txtnewfechanac.Text == "__/__/____")
                {
                    String Error1 = "Fecha no valida";
                    this.Lblerror.Text = Error1;
                }
                  quitarfilainicialbeneficiarios();
                  quitarfilainicialfamiliares();
            }

            else
            {

                if (VerificarFechasGrillasben())
                {

                       Beneficiarios beneficiariospolizas = new Beneficiarios();
                        beneficiariospolizas.consecutivo = this.buscarultconsebenef(LstBeneficiarios) + 1;
                        beneficiariospolizas.cod_poliza = -99;
                        beneficiariospolizas.nombres = txtnewnombresyapellidos.Text;
                        beneficiariospolizas.identificacion = txtnewdocumentoidentidad.Text;
                        beneficiariospolizas.parentesco = ddlparentesco.SelectedItem.Text;
                        if (VerificarFechaNamientoBenef())
                        {
                        String Fechanacimiento = txtnewfechanac.Text.ToString();
                        String format = "dd/MM/yyyy";
                        DateTime fecha_nacimiento = DateTime.ParseExact(Fechanacimiento, format, CultureInfo.InvariantCulture);
                        beneficiariospolizas.fecha_nacimiento = fecha_nacimiento;
                        // beneficiariospolizas.fecha_nacimiento = Convert.ToDateTime(txtnewfechanac.Text);
                        beneficiariospolizas.porcentaje = Convert.ToInt64(txtnewporcentaje.Text);


                        if (beneficiariospolizas.porcentaje == 0)
                        {
                            String Error = "EL porcentaje no puede ser 0";
                            this.Lblerror.Text = Error;

                        }


                        if (this.validarreglasbeneficiarios(beneficiariospolizas))
                        {

                            gvBeneficiarios.EditIndex = -1;

                            if (!this.operacion.Equals("N"))
                            {
                                beneficiariospolizas.cod_poliza = Convert.ToInt64(lblcodpoliza.Text);

                                beneficiariosPolizasServicio.CrearBeneficiarios(beneficiariospolizas, (Usuario)Session["usuario"]);
                            }
                            LstBeneficiarios.Add(beneficiariospolizas);
                            MostrarDatosBeneficiarios();
                        }
                    }
                }
            }

        }
    }


    protected void gvFamiliarespolizas_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvFamiliarespolizas_RowEditing(object sender, GridViewEditEventArgs e)
    {
       
        int conseID = Convert.ToInt32(gvFamiliarespolizas.DataKeys[e.NewEditIndex].Values[0].ToString());
        if (conseID != 0)
        {
            gvFamiliarespolizas.EditIndex = e.NewEditIndex;                    
            this.ConsultarFamiliares();
        }
        
        String parentesco = "";
        parentesco = this.buscarParentescofam(conseID);
        DropDownList ddlparentescoeditfam = (DropDownList)gvFamiliarespolizas.Rows[e.NewEditIndex].Cells[1].FindControl("ddlparentescoeditfam");
         if (ddlparentescoeditfam != null)
        {
            ddlparentescoeditfam.SelectedValue = parentesco;
        }
        String genero = "";
        genero = this.buscarGenero(conseID);
        DropDownList ddlSexoedit = (DropDownList)gvFamiliarespolizas.Rows[e.NewEditIndex].Cells[1].FindControl("ddlSexoedit");

        if (ddlSexoedit != null)
        {
            ddlSexoedit.SelectedValue = genero;
        }


        else
        {

            e.Cancel = true;
        }

    }
    protected void gvFamiliarespolizas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvFamiliarespolizas.DataKeys[e.RowIndex].Values[0].ToString());

        if (conseID != 0)
        {
          try
            {
                if (this.operacion.Equals("N"))
                {
                    eliminarGrupo_Familiares(conseID);
                }
                else
                {

                    familiaresPolizasServicio.EliminarFamiliares(conseID, (Usuario)Session["usuario"]);

                  
                }

                ConsultarFamiliares();



            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(this.familiaresPolizasServicio.consecutivo + "L", "gvFamiliares_RowDeleting", ex);
            }

        }

        else
        {
            e.Cancel = true;
        }
     
    }
    protected void gvFamiliarespolizas_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }
    protected void gvFamiliarespolizas_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txtnombresyapellidos = (TextBox)gvFamiliarespolizas.Rows[e.RowIndex].FindControl("txtnombresyapellidos");
        TextBox txtdocumentoidentidad = (TextBox)gvFamiliarespolizas.Rows[e.RowIndex].FindControl("txtdocumentoidentidad");
        TextBox txtfechanacf = (TextBox)gvFamiliarespolizas.Rows[e.RowIndex].FindControl("txtfechanacf");
        TextBox txtactividad = (TextBox)gvFamiliarespolizas.Rows[e.RowIndex].FindControl("txtactividad");
        DropDownList ddlparentescoeditfam = (DropDownList)gvFamiliarespolizas.Rows[e.RowIndex].FindControl("ddlparentescoeditfam");
        DropDownList ddlsexoedit = (DropDownList)gvFamiliarespolizas.Rows[e.RowIndex].FindControl("ddlsexoedit");

         if (txtfechanacf.Text == "__/__/____" || txtfechanacf.Text == "00/00/0000")
         {

             String Error = "Fecha no valida";
             this.Lblerror.Text = Error;

         }
         else
         {
             long conseID = Convert.ToInt32(gvFamiliarespolizas.DataKeys[e.RowIndex].Values[0].ToString());
             FamiliaresPolizas familiares = new FamiliaresPolizas();
             familiares.consecutivo = conseID;
             familiares.cod_poliza = Convert.ToInt64("0" + lblcodpoliza.Text);
             familiares.nombres = Convert.ToString(txtnombresyapellidos.Text);
             familiares.identificacion = Convert.ToString(txtdocumentoidentidad.Text);
             familiares.sexo = ddlsexoedit.SelectedValue.ToString();
             familiares.parentesco = ddlparentescoeditfam.SelectedValue.ToString();

             String format = "dd/MM/yyyy";

             familiares.fecha_nacimiento = DateTime.ParseExact(txtfechanacf.Text, format, CultureInfo.InvariantCulture);
   

            // familiares.fecha_nacimiento = Convert.ToDateTime(txtfechanacf.Text);
             familiares.actividad = Convert.ToString(txtactividad.Text);

             this.Lblerror.Text = "";

             DateTime Fechanacimiento = DateTime.ParseExact(txtfechanacf.Text, format, CultureInfo.InvariantCulture);
   
             //DateTime Fechanacimiento = DateTime.Parse(txtfechanacf.Text);
             Int64 edad = polizasSegurosServicio.CalcularEdad2(Fechanacimiento);
             PolizasSeguros polizasSeguros = new PolizasSeguros();
             polizasSeguros = polizasSegurosServicio.ConsultarParametroEdadBeneficiarios((Usuario)Session["Usuario"]);
             Int64 edadmaxima = polizasSeguros.edad_maxima;


            
             //DateTime dt1 = new DateTime();
             DateTime dt1 = DateTime.ParseExact(txtfechanacf.Text, format, CultureInfo.InvariantCulture);
   
             //dt1 = DateTime.Parse(txtfechanacf.Text);
             if (edad >= edadmaxima)
             {
                 String Error = "Edad máxima del familiar.";
                 this.Lblerror.Text = Error;
                 quitarfilainicialbeneficiarios();
                 quitarfilainicialfamiliares();
             }

             else
             {
                 if (dt1.Date > DateTime.Now.Date)
                 {
                     String Error1 = "No puede grabar un familiar con fecha de nacimiento superior a la actual";
                     this.Lblerror.Text = Error1;

                     quitarfilainicialbeneficiarios();
                     quitarfilainicialfamiliares();

                 }


                 else
                 {
                     gvFamiliarespolizas.EditIndex = -1;
                     if (operacion.Equals("N"))
                     {
                         ActualizarGrupo_Familiares(familiares);
                     }
                     else
                     {
                         this.familiaresPolizasServicio.ModificarFamiliares(familiares, (Usuario)Session["usuario"]);
                         ConsultarFamiliares();

                     }
                 }
             }
        quitarfilainicialfamiliares();
    }
    }

    private Boolean validarreglasbeneficiarios(Beneficiarios beneficiariospolizas)
    {
        Boolean result = true;
        long totalPorcentaje = 0;
        int contar = 0;
        Boolean existecony = false;
        List<Beneficiarios> LstBeneficiarios = new List<Beneficiarios>();
        LstBeneficiarios = (List<Beneficiarios>)Session["Beneficiarios"];
        this.Lblerror.Text = "";
        if (LstBeneficiarios != null)
        {
            //primer regla suma de porcentajes...
            foreach (Beneficiarios beneficiario in LstBeneficiarios)
            {
                if (beneficiario.consecutivo > 0)
                {
                    totalPorcentaje = beneficiario.porcentaje + totalPorcentaje;
                    if (beneficiario.parentesco.Equals("conyuge"))
                    {
                        existecony = true;

                    }
                    contar++;
                }
            }
            //valido si lo que voy a ingresar es un conyuge  y este conyuge ya existe en la lista anterior
            if (beneficiariospolizas.parentesco.Equals("conyuge") && existecony)
            {
                String Error = "Ya registro conyuge";
                this.Lblerror.Text = Error;
                result = false;
                quitarfilainicialbeneficiarios();
                quitarfilainicialfamiliares();
            }
            else
            {

                //valido que la suma del porcentaje no sea diferente a 100
                if ((totalPorcentaje + beneficiariospolizas.porcentaje) > 100)
                {
                    String Error = "Supera el 100% del porcentaje";
                    this.Lblerror.Text = Error;
                    result = false;
                    quitarfilainicialbeneficiarios();
                    quitarfilainicialfamiliares();
                }
                else
                {
                    //Valido que la cantidad no sea mayor a cuatro de los beneficiarios
                    if (contar >= 4)
                    {
                        String Error = "Solo puedo registrar cuatro beneficiarios";
                        this.Lblerror.Text = Error;
                        result = false;
                        quitarfilainicialbeneficiarios();
                        quitarfilainicialfamiliares();

                    }
                }
            }
        }
        return result;
    }
    private Boolean validarreglasfamiliares(FamiliaresPolizas familiarespolizas)
    {
        Boolean result = true;
        //long total = 0;
        int contar = 0;
       
        Boolean existecony = false;
        List<FamiliaresPolizas> LstFamiliaresPolizas = new List<FamiliaresPolizas>();
        LstFamiliaresPolizas = (List<FamiliaresPolizas>)Session["FamiliaresPolizas"];
        this.Lblerror.Text = "";
        if (LstFamiliaresPolizas != null)
        {

            //primer regla suma de porcentajes...
            foreach (FamiliaresPolizas familiar in LstFamiliaresPolizas)
            {
                if (familiar.consecutivo> 0)
                {
                    if (familiar.parentesco.Equals("conyuge"))
                    {
                        existecony = true;

                    }
                    contar++;
                }
            }

            
            //valido si lo que voy a ingresar es un conyuge  y este conyuge ya existe en la lista anterior
            if (familiarespolizas.parentesco.Equals("conyuge") && existecony)
            {
                String Error = "Ya registro conyuge";
                this.Lblerror.Text = Error;
                result = false;
                quitarfilainicialbeneficiarios();
                quitarfilainicialfamiliares();
            }
            
                else
                {
                    //Valido que la cantidad no sea mayor a cuatro de los beneficiarios
                    if (contar >= 4)
                    {
                        String Error = "Solo puedo registrar cuatro familiares";
                        this.Lblerror.Text = Error;
                        result = false;
                        quitarfilainicialbeneficiarios();
                        quitarfilainicialfamiliares();

                    }
                }
            
        }
        return result;
    }


    private Boolean validarreglasbeneficiariosGrabar()
    {
        Boolean result = true;
        long totalPorcentaje = 0;
        int contar = 0;
        List<Beneficiarios> LstBeneficiarios = new List<Beneficiarios>();
        LstBeneficiarios = (List<Beneficiarios>)Session["Beneficiarios"];
        this.Lblerror.Text = "";
        if (LstBeneficiarios != null)
        {
            //primer regla suma de porcentajes...
            foreach (Beneficiarios beneficiario in LstBeneficiarios)
            {
                if (beneficiario.consecutivo > 0)
                {
                    totalPorcentaje = beneficiario.porcentaje + totalPorcentaje;
                    contar++;
                }
            }
            //valido que la suma del porcentaje no sea diferente a 100
            if ((totalPorcentaje) < 100)
            {
                String Error = "Inferior el 100% del porcentaje";
                this.Lblerror.Text = Error;
                result = false;
            }

        }
        quitarfilainicialbeneficiarios();
        quitarfilainicialfamiliares();
        return result;

    }
    /* private Boolean validarreglasbeneficiariosporcentaje(Beneficiarios beneficiariospolizas)
      {
          Boolean result = true;
          long totalPorcentaje = 0;
       
          List<Beneficiarios> LstBeneficiarios = new List<Beneficiarios>();

          LstBeneficiarios = (List<Beneficiarios>)Session["Beneficiarios"];
          this.Lblerror.Text = "";
          if (LstBeneficiarios != null)
          {
                 //valido que la suma del porcentaje no sea diferente a 100
                  if ((totalPorcentaje + beneficiariospolizas.porcentaje) > 100)
                  {
                      String Error = "No ingreso  el 100% del porcentaje";
                      this.Lblerror.Text = Error;
                      result = false;
                  }
        
      

      }
          return result;
      }
   */

    protected void gvFamiliarespolizas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvFamiliarespolizas.EditIndex = -1;
        long codpoliza = Convert.ToInt64("0" + lblcodpoliza.Text);
        ConsultarFamiliares();
       
    }
    protected void gvFamiliarespolizas_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void ObtenerDatosimprimir(String pIdObjeto)
    {
        try
        {
            PolizasSeguros polizasSeguros = new PolizasSeguros();          

            polizasSeguros.cod_poliza = Int32.Parse(pIdObjeto);

            polizasSeguros = polizasSegurosServicio.ConsultarPolizasSeguros(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            ValorPoliza = polizasSeguros.valor_prima_total;

        
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(polizasSegurosServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            PolizasSeguros polizasSeguros = new PolizasSeguros();
            PolizasSegurosVida polizasSegurosvida = null;

            Beneficiarios beneficiarios = new Beneficiarios();
            FamiliaresPolizas familiares = new FamiliaresPolizas();

            polizasSeguros.cod_poliza = Int32.Parse(pIdObjeto);
            polizasSeguros = polizasSegurosServicio.ConsultarPolizasSeguros(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            
            this.ddlTipoPlanSegVida.SelectedValue = "";
            this.ddlTipoPlanAcc.SelectedValue = "";
         

            if (!string.IsNullOrEmpty(polizasSeguros.cod_poliza.ToString()))
            {
                lblcodpoliza.Text = polizasSeguros.cod_poliza.ToString();
                TxtIdentificacion.Text = polizasSeguros.identificacion.ToString();
                TxtTipoIden.Text = polizasSeguros.tipo_iden.ToString();
                TxtNombres.Text = polizasSeguros.nombre_deudor.ToString();
                TxtNumRadicacion.Text = polizasSeguros.numero_radicacion.ToString();
                txtnumpolizafisica.Text = polizasSeguros.num_poliza.ToString();

                if (!string.IsNullOrEmpty(polizasSeguros.oficina))
                    TxtOficina.Text = polizasSeguros.oficina.ToString();

                if (!string.IsNullOrEmpty(polizasSeguros.codoficina))
                    Txtcodoficina.Text = polizasSeguros.codoficina.ToString();
                

                TxtIdenAsesor.Text = polizasSeguros.ident_asesor.ToString();

                if (!string.IsNullOrEmpty(polizasSeguros.nombre_asesor))
                    TxtNomAsesor.Text = polizasSeguros.nombre_asesor.ToString();

                TxtValorPrimaMensual.Text = polizasSeguros.valor_prima.ToString("0,0", CultureInfo.InvariantCulture);


                if (polizasSeguros.fec_ini_vig != DateTime.MinValue) TxtVigenPolDesde.Text = polizasSeguros.fec_ini_vig.ToShortDateString();
                if (polizasSeguros.fec_fin_vig != DateTime.MinValue) TxtVigenPolHasta.Text = polizasSeguros.fec_fin_vig.ToShortDateString();

                //if (polizasSeguros.fec_ini_vig != null) TxtVigenPolDesde.ToDateTime = polizasSeguros.fec_ini_vig;


                //TxtVigenPolDesde.Text = polizasSeguros.fec_ini_vig.ToString("MM-dd-yyyy");
                // TxtVigenPolHasta.Text = polizasSeguros.fec_fin_vig.ToString("MM-dd-yyyy");
                if (polizasSeguros.fechanacimiento != DateTime.MinValue) TxtFechaNac.Text = polizasSeguros.fechanacimiento.ToShortDateString();


                //TxtFechaNac.Text = polizasSeguros.fechanacimiento.ToString("MM/dd/yyyy");

                if (!string.IsNullOrEmpty(polizasSeguros.estado_civil))
                    TxtEstadoCivil.Text = polizasSeguros.estado_civil.ToString();

                if (!string.IsNullOrEmpty(polizasSeguros.actividad))
                    TxtOcupacion.Text = polizasSeguros.actividad.ToString();

                if (!string.IsNullOrEmpty(polizasSeguros.sexo))
                    TxtSexo.Text = polizasSeguros.sexo.ToString();

                if (!string.IsNullOrEmpty(polizasSeguros.direccion))
                    this.TxtDireccion.Text = polizasSeguros.direccion.ToString();

                if (!string.IsNullOrEmpty(polizasSeguros.email))
                    TxtEmail.Text = polizasSeguros.email.ToString();

                if (!string.IsNullOrEmpty(polizasSeguros.ciudad_residencia))
                    TxtCiudad.Text = polizasSeguros.ciudad_residencia.ToString();

                if (!string.IsNullOrEmpty(polizasSeguros.celular))
                    TxtCelular.Text = polizasSeguros.celular.ToString();

                if (!string.IsNullOrEmpty(polizasSeguros.telefono))
                    TxtTelefono.Text = polizasSeguros.telefono.ToString();

                if (!string.IsNullOrEmpty(polizasSeguros.nombre_deudor))
                   TxtNomAsegu.Text = polizasSeguros.nombre_deudor.ToString();


                TxtIdenAsegur.Text = polizasSeguros.identificacion.ToString();
                Txtcodasegurado.Text = polizasSeguros.cod_asegurado.ToString();
                Txtcodasesor.Text = polizasSeguros.icodigo.ToString();
                Txtindividual.Text = polizasSeguros.individual.ToString();
               // Txtplan.Text = polizasSeguros.tipo_plan_s.ToString();

                polizasSegurosvida = polizasSegurosServicio.ConsultarPolizasSegurosVida(Convert.ToInt64(polizasSeguros.cod_poliza.ToString()), "Vida Grupo", (Usuario)Session["usuario"]);

                if (polizasSegurosvida != null)
                {

                    this.ddlTipoPlanSegVida.SelectedValue = polizasSegurosvida.tipo_plan.ToString();
                    if (polizasSegurosvida.individual == "1")
                    {
                        this.chkprimaindividual.Checked = true;
                        this.txtPrimaindividual.Text = polizasSegurosvida.valor_prima.ToString();
                        this.Txtplan.Text = polizasSegurosvida.tipo_plan.ToString();
                    }
                    if (polizasSegurosvida.individual == "0")
                    {
                        this.chkprimacony.Checked = true;
                        this.txtPrimacony.Text = polizasSegurosvida.valor_prima.ToString();
                       
                    }
                    this.MostrarDatosAmparos("Vida Grupo", this.ddlTipoPlanSegVida, this.gvVidaGrupo);
                }
                polizasSegurosvida = null;

                polizasSegurosvida = polizasSegurosServicio.ConsultarPolizasSegurosVida(Convert.ToInt64(polizasSeguros.cod_poliza.ToString()), "Accidentes Personales", (Usuario)Session["usuario"]);


                if (polizasSegurosvida != null)
                {
                    this.ddlTipoPlanAcc.SelectedValue = polizasSegurosvida.tipo_plan.ToString();
                    if (polizasSegurosvida.individual == "1")
                    {
                        this.chkprimaacc.Checked = true;
                        this.txtPrimaacc.Text = polizasSegurosvida.valor_prima.ToString();
                        this.Txtplan.Text = polizasSegurosvida.tipo_plan.ToString();
                    }
                    if (polizasSegurosvida.individual == "0")
                    {
                        this.chkprimaopfam.Checked = true;
                        this.txtPrimaopfam.Text = polizasSegurosvida.valor_prima.ToString();
                       
                    }
                    this.MostrarDatosAmparos("Accidentes Personales", this.ddlTipoPlanAcc, this.gvAccPers);
                }

                //if (!string.IsNullOrEmpty(polizasSeguros.tipo_plan_s.ToString()))
                //ddlTipoPlanS.SelectedValue = polizasSeguros.tipo_plan_s.ToString();

                this.TxtValorPrimaMensual.Text = polizasSeguros.valor_prima_mensual.ToString();
               
                ListarDatosBeneficiarios(polizasSeguros.cod_poliza);
                ListarDatosFamiliares(polizasSeguros.cod_poliza);
                // ConsultarFamiliares();
                // ConsultarBeneficiarios();
                ObtenerDatosTomadorPoliza();
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(polizasSegurosServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

    protected void ObtenerDatosCredito(String pIdObjeto)
    {
        try
        {
            PolizasSeguros polizasSeguros = new PolizasSeguros();
            Beneficiarios beneficiarios = new Beneficiarios();
            FamiliaresPolizas familiares = new FamiliaresPolizas();


            // try
            {

                polizasSeguros = polizasSegurosServicio.ConsultarCredito(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
                PolizasSeguros polseg;
                PolizasSeguros lista;
                polseg = new PolizasSeguros();
                polseg.numero_radicacion = polizasSeguros.numero_radicacion;
                polizasSeguros.numero_radicacion = Int32.Parse(pIdObjeto);


                if (!string.IsNullOrEmpty(polizasSeguros.numero_radicacion.ToString()))
                {
                    this.Lblerror.Text = "";
                    TxtNumRadicacion.Text = polizasSeguros.numero_radicacion.ToString();

                    if (!string.IsNullOrEmpty(polizasSeguros.identificacion.ToString())) TxtIdentificacion.Text = polizasSeguros.identificacion.ToString();
                    if (!string.IsNullOrEmpty(polizasSeguros.tipo_iden.Trim().ToString())) TxtTipoIden.Text = polizasSeguros.tipo_iden.Trim().ToString();             
                    if (!string.IsNullOrEmpty(polizasSeguros.nombre_deudor)) TxtNombres.Text = polizasSeguros.nombre_deudor.ToString();
                    //if (!string.IsNullOrEmpty(polizasSeguros.num_poliza.ToString())) txtnumpolizafisica.Text = polizasSeguros.num_poliza.ToString();
                    if (!string.IsNullOrEmpty(polizasSeguros.oficina)) TxtOficina.Text = polizasSeguros.oficina.ToString();
                    if (!string.IsNullOrEmpty(polizasSeguros.ident_asesor.ToString()))  TxtIdenAsesor.Text = polizasSeguros.ident_asesor.ToString();
                    if (!string.IsNullOrEmpty(polizasSeguros.nombre_asesor)) TxtNomAsesor.Text = polizasSeguros.nombre_asesor.ToString();
                    TxtVigenPolDesde.Text = polizasSeguros.fec_ini_vig.ToString("dd/MM/yyyy");
                    TxtVigenPolHasta.Text = polizasSeguros.fec_fin_vig.ToString("dd/MM/yyyy");
                    if (polizasSeguros.fechanacimiento != DateTime.MinValue) TxtFechaNac.Text = polizasSeguros.fechanacimiento.ToString("dd/MM/yyyy");
                    if (!string.IsNullOrEmpty(polizasSeguros.nombre_asesor)) TxtNomAsesor.Text = polizasSeguros.nombre_asesor.ToString();
                    if (!string.IsNullOrEmpty(polizasSeguros.estado_civil)) TxtEstadoCivil.Text = polizasSeguros.estado_civil.ToString();
                    if (!string.IsNullOrEmpty(polizasSeguros.actividad)) TxtOcupacion.Text = polizasSeguros.actividad.ToString();
                    if (!string.IsNullOrEmpty(polizasSeguros.sexo)) TxtSexo.Text = polizasSeguros.sexo.ToString();
                    if (!string.IsNullOrEmpty(polizasSeguros.direccion)) TxtDireccion.Text = polizasSeguros.direccion.ToString();
                    if (!string.IsNullOrEmpty(polizasSeguros.email))  TxtEmail.Text = polizasSeguros.email.ToString();
                    if (!string.IsNullOrEmpty(polizasSeguros.ciudad_residencia)) TxtCiudad.Text = polizasSeguros.ciudad_residencia.ToString();
                    if (!string.IsNullOrEmpty(polizasSeguros.celular))TxtCelular.Text = polizasSeguros.celular.ToString();
                    if (!string.IsNullOrEmpty(polizasSeguros.telefono)) TxtTelefono.Text = polizasSeguros.telefono.ToString();
                    if (!string.IsNullOrEmpty(polizasSeguros.nombre_deudor)) TxtNomAsegu.Text = polizasSeguros.nombre_deudor.ToString();
                    if (!string.IsNullOrEmpty(polizasSeguros.identificacion.ToString())) TxtIdenAsegur.Text = polizasSeguros.identificacion.ToString();
                    if (!string.IsNullOrEmpty(polizasSeguros.identificacion.ToString())) Txtcodasegurado.Text = polizasSeguros.cod_asegurado.ToString();
                    if (!string.IsNullOrEmpty(polizasSeguros.ident_asesor.ToString()))  Txtcodasesor.Text = polizasSeguros.icodigo.ToString();
                    ObtenerDatosTomadorPoliza();
                    lista = polizasSegurosServicio.ConsultarPolizasSegurosValidacion(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
                    if (lista.numero_radicacion == polizasSeguros.numero_radicacion)
                    {

                        String Error = "ESTE CREDITO YA TIENE UNA POLIZA REGISTRADA";
                        this.limpiarcampos();
                        this.Lblerror.Text = Error;

                    }
                    else
                    {
                        this.Lblerror.Text = "";
                    }
                    quitarfilainicialbeneficiarios();
                    quitarfilainicialfamiliares();
                }

            }
        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(polizasSegurosServicio.GetType().Name + "A", "ObtenerDatosCredito", ex);

        }


    }
    private void limpiarcampos()
    {
        TxtIdentificacion.Text = "";
        TxtTipoIden.Text = "";
        TxtNombres.Text = "";
        TxtNumRadicacion.Text = "";
       // TxtNumPolizaFisica.Text = " ";
        TxtOficina.Text = "";
        TxtIdenAsesor.Text = ""; ;
        TxtNomAsesor.Text = "";
        TxtVigenPolDesde.Text = "";
        TxtVigenPolHasta.Text = "";
        TxtFechaNac.Text = "";
        TxtEstadoCivil.Text = "";
        TxtOcupacion.Text = "";
        TxtSexo.Text = "";
        TxtDireccion.Text = "";
        TxtEmail.Text = "";
        TxtCiudad.Text = "";
        TxtCelular.Text = "";
        TxtTelefono.Text = "";
        TxtNomAsegu.Text = "";
        TxtIdenAsegur.Text = "";
        Txtcodasegurado.Text = "";
        Txtcodasesor.Text = "";
        TxtTomadorSeg.Text = "";
        TxtNit.Text = "";
        TxtValorPrimaMensual.Text = "";
    }
    protected void ObtenerDatosTomadorPoliza()
    {
        try
        {
            TomadorPoliza tomadorPoliza = new TomadorPoliza();
            tomadorPoliza = tomadorPolizaServicio.ConsultarTomadorPolizas((Usuario)Session["usuario"]);
            TxtTomadorSeg.Text = tomadorPoliza.razonsocial.ToString();
            TxtNit.Text = tomadorPoliza.identificacion.ToString();

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tomadorPolizaServicio.GetType().Name + "A", "ObtenerDatosTomadorPoliza", ex);
        }
    }
    private void MostrarDatosAmparos(String Tipo, DropDownList ddlTipo, GridView gvAmparos)
    {
        PlanesSegurosAmparos pPlanesSegurosAmparos = new PlanesSegurosAmparos();
        List<PlanesSegurosAmparos> LstPlanesSegurosAmparos = new List<PlanesSegurosAmparos>();
        pPlanesSegurosAmparos.tipo_plan = Convert.ToInt64("0" + ddlTipo.SelectedValue);
        pPlanesSegurosAmparos.tipo = Tipo;

        LstPlanesSegurosAmparos = planSegurosAmparosServicio.ListarPlanesSegurosAmparos(pPlanesSegurosAmparos, (Usuario)Session["usuario"]);
        gvAmparos.DataSource = LstPlanesSegurosAmparos;
        gvAmparos.DataBind();
        ObtenerDatosPlanesSeguros(ddlTipo.SelectedValue, Tipo);

    }
    private void MostrarDatosParentesco(String Tipo, DropDownList ddlparentescoedit, GridView gvBeneficiarios)
    {
        ParentescoPolizas pParentesco = new ParentescoPolizas();
        List<ParentescoPolizas> LstParentesco = new List<ParentescoPolizas>();
        pParentesco.parentesco = ddlparentescoedit.SelectedValue;
        LstParentesco = polizasSegurosServicio.ListarParentesco(pParentesco, (Usuario)Session["usuario"]);
        gvBeneficiarios.DataSource = LstParentesco;
        gvBeneficiarios.DataBind();
        // ObtenerDatosPlanesSeguros(ddlparentescoed.SelectedValue, Tipo);

    }




    private void ListarDatosBeneficiarios(Int64 cod_poliza)
    {
        Beneficiarios beneficiarios = new Beneficiarios();
        beneficiarios.cod_poliza = cod_poliza;


        List<Beneficiarios> LstBeneficiariosData = new List<Beneficiarios>();

        LstBeneficiariosData = this.beneficiariosPolizasServicio.ListarBeneficiarios(beneficiarios, (Usuario)Session["usuario"]);

        if (LstBeneficiariosData == null)
        {
            crearBeneficiariosinicial(0, "Beneficiarios");
            LstBeneficiariosData = (List<Beneficiarios>)Session["Beneficiarios"];


        }
        if (LstBeneficiariosData.Count == 0)
        {
            crearBeneficiariosinicial(0, "Beneficiarios");
            LstBeneficiariosData = (List<Beneficiarios>)Session["Beneficiarios"];


        }
        else
        {
            Session["Beneficiarios"] = LstBeneficiariosData;
        }
        this.gvBeneficiarios.DataSource = LstBeneficiariosData;
        gvBeneficiarios.DataBind();

        quitarfilainicialfamiliares();
        quitarfilainicialbeneficiarios();
    }
    private void ListarDatosFamiliares(Int64 cod_poliza)
    {
        FamiliaresPolizas familiares = new FamiliaresPolizas();
        familiares.cod_poliza = cod_poliza;

        List<FamiliaresPolizas> LstFamiliaresData = new List<FamiliaresPolizas>();

        LstFamiliaresData = this.familiaresPolizasServicio.ListarFamiliares(familiares, (Usuario)Session["usuario"]);
        if (LstFamiliaresData.Count == 0)
        {
            this.crearFamiliaresinicial(0, "FamiliaresPolizas");
            LstFamiliaresData = (List<FamiliaresPolizas>)Session["FamiliaresPolizas"];
        }
        else
        {
            Session["FamiliaresPolizas"] = LstFamiliaresData;
        }
        this.gvFamiliarespolizas.DataSource = LstFamiliaresData;
        gvFamiliarespolizas.DataBind();
        quitarfilainicialfamiliares();
        quitarfilainicialbeneficiarios();
    }

    private void MostrarDatosParentesco()
    {

        List<Parentesco> LstParentesco = new List<Parentesco>();


        LstParentesco = (List<Parentesco>)Session["Beneficiarios"];
        if (LstParentesco == null)
        {
            crearBeneficiariosinicial(0, "Beneficiarios");
            LstParentesco = (List<Parentesco>)Session["Beneficiarios"];
        }

        gvBeneficiarios.DataSource = LstParentesco;
        gvBeneficiarios.DataBind();


    }
    private void MostrarDatosParentesfam()
    {

        List<FamiliaresPolizas> LstFamiliaresPolizas = new List<FamiliaresPolizas>();


        LstFamiliaresPolizas = (List<FamiliaresPolizas>)Session["FamiliaresPolizas"];
        if (LstFamiliaresPolizas == null)
        {
            crearFamiliaresinicial(0, "FamiliaresPolizas");
            LstFamiliaresPolizas = (List<FamiliaresPolizas>)Session["FamiliaresPolizas"];
        }

        gvFamiliarespolizas.DataSource = LstFamiliaresPolizas;
        gvFamiliarespolizas.DataBind();


    }
    private void MostrarDatosGenero()
    {

        List<Genero> LstGenero = new List<Genero>();


        LstGenero = (List<Genero>)Session["Genero"];
        if (LstGenero == null)
        {
            //  crearBeneficiariosinicial(0, "Genero");
            LstGenero = (List<Genero>)Session["Genero"];
        }

        gvBeneficiarios.DataSource = LstGenero;
        gvBeneficiarios.DataBind();


    }
    private void MostrarDatosBeneficiarios()
    {

        List<Beneficiarios> LstBeneficiarios = new List<Beneficiarios>();


        LstBeneficiarios = (List<Beneficiarios>)Session["Beneficiarios"];
        if ((LstBeneficiarios == null) || (LstBeneficiarios.Count == 0))
        {
            crearBeneficiariosinicial(0, "Beneficiarios");
            LstBeneficiarios = (List<Beneficiarios>)Session["Beneficiarios"];
        }

        gvBeneficiarios.DataSource = LstBeneficiarios;
        gvBeneficiarios.DataBind();
        quitarfilainicialbeneficiarios();
        quitarfilainicialfamiliares();


    }
    private String buscarParentesco(Int64 idconse)
    {

        String parentesco = "";
        List<Beneficiarios> LstBeneficiarios = new List<Beneficiarios>();
        LstBeneficiarios = (List<Beneficiarios>)Session["Beneficiarios"];
        if (LstBeneficiarios != null)
        {
            foreach (Beneficiarios ben in LstBeneficiarios)
            {
                if (ben.consecutivo == idconse)
                {
                    parentesco = ben.parentesco;
                }
            }

        }
        return parentesco;


    }
    private String buscarGenero(Int64 idconse)
    {

        String genero = "";
        List<FamiliaresPolizas> LstGenero = new List<FamiliaresPolizas>();
        LstGenero = (List<FamiliaresPolizas>)Session["FamiliaresPolizas"];
        if (LstGenero != null)
        {
            foreach (FamiliaresPolizas fam in LstGenero)
            {
                if (fam.consecutivo== idconse)
                {
                    genero = fam.sexo;
                }
            }

        }
        return genero;


    }
    private String buscarParentescofam(Int64 idconse)
    {

        String parentesco = "";
        List<FamiliaresPolizas> LstFamiliaresPolizas = new List<FamiliaresPolizas>();


        LstFamiliaresPolizas = (List<FamiliaresPolizas>)Session["FamiliaresPolizas"];
        if (LstFamiliaresPolizas != null)
        {
            foreach (FamiliaresPolizas fam in LstFamiliaresPolizas)
            {
                if (fam.consecutivo == idconse)
                {
                    parentesco = fam.parentesco;
                }
            }

        }
        return parentesco;


    }


    private void MostrarDatosBeneficiarios(String cod_poliza, GridView gvBeneficiarios, String Var)
    {

        Beneficiarios pBeneficiarios = new Beneficiarios();
        List<Beneficiarios> LstBeneficiarios = new List<Beneficiarios>();

        pBeneficiarios.cod_poliza = Convert.ToInt64("0" + lblcodpoliza.Text);

        LstBeneficiarios = beneficiariosPolizasServicio.ListarBeneficiarios(pBeneficiarios, (Usuario)Session["usuario"]);


        if (LstBeneficiarios.Count == 0)
        {
            crearBeneficiariosinicial(0, "Beneficiarios");
            LstBeneficiarios = (List<Beneficiarios>)Session["Beneficiarios"];

        }

        gvBeneficiarios.DataSource = LstBeneficiarios;

        gvBeneficiarios.DataBind();

        Session[Var] = LstBeneficiarios;
    }
    private void MostrarDatosBeneficiariosNuevo(String cod_poliza, GridView gvBeneficiarios, String Var)
    {

        Beneficiarios pBeneficiarios = new Beneficiarios();
        List<Beneficiarios> LstBeneficiarios = new List<Beneficiarios>();

        pBeneficiarios.cod_poliza = Convert.ToInt64("0" + lblcodpoliza.Text);

        if (Convert.ToInt64(pBeneficiarios.cod_poliza) > 0)
        {
            LstBeneficiarios = beneficiariosPolizasServicio.ListarBeneficiarios(pBeneficiarios, (Usuario)Session["usuario"]);
        }
        else
        {
            LstBeneficiarios = (List<Beneficiarios>)Session["Beneficiarios"];
        }
        if (LstBeneficiarios.Count == 0)
        {
            crearBeneficiariosinicial(0, "Beneficiarios");
            LstBeneficiarios = (List<Beneficiarios>)Session["Beneficiarios"];

        }

        gvBeneficiarios.DataSource = LstBeneficiarios;

        gvBeneficiarios.DataBind();

        Session[Var] = LstBeneficiarios;
    }
    private void MostrarDatosFamiliares(String cod_poliza, GridView gvFamiliarespolizas, String Var)
    {

        FamiliaresPolizas pFamiliares = new FamiliaresPolizas();
        List<FamiliaresPolizas> LstFamiliares = new List<FamiliaresPolizas>();
        //pPlanesSegurosAmparos.tipo_plan = Convert.ToInt64("0" + tipo_plan);
        // pFamiliares.cod_poliza = Convert.ToInt64("0" + cod_poliza);
        pFamiliares.cod_poliza = Convert.ToInt64("0" + lblcodpoliza.Text);
        //pFamiliares.cod_poliza = Convert.ToInt64("0" + cod_poliza);

        LstFamiliares = familiaresPolizasServicio.ListarFamiliares(pFamiliares, (Usuario)Session["usuario"]);

        if (LstFamiliares.Count == 0)
        {
            crearFamiliaresinicial(0, "FamiliaresPolizas");
            LstFamiliares = (List<FamiliaresPolizas>)Session["FamiliaresPolizas"];

        }
        gvFamiliarespolizas.DataSource = LstFamiliares;
        gvFamiliarespolizas.DataBind();
        Session[Var] = LstFamiliares;
    }

    private void MostrarDatosFamiliaresNuevo(String cod_poliza, GridView gvFamiliarespolizas, String Var)
    {

        FamiliaresPolizas pFamiliares = new FamiliaresPolizas();
        List<FamiliaresPolizas> LstFamiliares = new List<FamiliaresPolizas>();
        pFamiliares.cod_poliza = Convert.ToInt64("0" + lblcodpoliza.Text);
        if (Convert.ToInt64(pFamiliares.cod_poliza) > 0)
        {
            LstFamiliares = familiaresPolizasServicio.ListarFamiliares(pFamiliares, (Usuario)Session["usuario"]);
        }
        else
        {
            LstFamiliares = (List<FamiliaresPolizas>)Session["FamiliaresPolizas"];

        }

        if (LstFamiliares.Count == 0)
        {
            crearFamiliaresinicial(0, "FamiliaresPolizas");
            LstFamiliares = (List<FamiliaresPolizas>)Session["FamiliaresPolizas"];
        }
        gvFamiliarespolizas.DataSource = LstFamiliares;
        gvFamiliarespolizas.DataBind();
        Session[Var] = LstFamiliares;
    }

    private void ConsultarFamiliares()
    {
        String cod_poliza = Convert.ToInt64("0" + lblcodpoliza.Text).ToString();
        if (cod_poliza != "0")
        {
            MostrarDatosFamiliares("cod_poliza", gvFamiliarespolizas, "FamiliaresPolizas");
        }
        else 
        {
            MostrarDatosFamiliaresNuevo("cod_poliza", gvFamiliarespolizas, "FamiliaresPolizas");
        }
       
        quitarfilainicialfamiliares();
        quitarfilainicialbeneficiarios();

    }

    private void ConsultarBeneficiarios()
    {
        //  crearBeneficiariosinicial(0, "Beneficiarios");
        String cod_poliza = Convert.ToInt64("0" + lblcodpoliza.Text).ToString();
        if (cod_poliza != "0")
        {
            MostrarDatosBeneficiarios("cod_poliza", gvBeneficiarios, "Beneficiarios");
        }
        else
        {
            MostrarDatosBeneficiariosNuevo("cod_poliza", gvBeneficiarios, "Beneficiarios");
        }
        quitarfilainicialfamiliares();
        quitarfilainicialbeneficiarios();

    }
    private void MostrarDatosFamiliares()
    {

        List<FamiliaresPolizas> LstFamiliares = new List<FamiliaresPolizas>();
        //gvFamiliarespolizas.DataSource = null;
        LstFamiliares = (List<FamiliaresPolizas>)Session["FamiliaresPolizas"];
        if ((LstFamiliares == null) || (LstFamiliares.Count == 0))
        {
            crearFamiliaresinicial(0, "FamiliaresPolizas");
            LstFamiliares = (List<FamiliaresPolizas>)Session["FamiliaresPolizas"];
        }

        gvFamiliarespolizas.DataSource = LstFamiliares;
        gvFamiliarespolizas.DataBind();
        quitarfilainicialfamiliares();
        quitarfilainicialbeneficiarios();

    }

    protected void LlenarComboTipoPlan(DropDownList ddlNombrePlan)
    {
        PlanesSegurosService planesSegurosServicio = new PlanesSegurosService();
        PlanesSeguros planesSeguros = new PlanesSeguros();
        List<PlanesSeguros> LstplanesSeguros = new List<PlanesSeguros>();
       
        LstplanesSeguros = planesSegurosServicio.ListarPlanesSeguros(planesSeguros, (Usuario)Session["usuario"]);
        planesSeguros.tipo_plan = 0;
        planesSeguros.descripcion = "";
        LstplanesSeguros.Add(planesSeguros);
        ddlNombrePlan.DataSource = LstplanesSeguros;
        ddlNombrePlan.DataTextField = "descripcion";
        ddlNombrePlan.DataValueField = "tipo_plan";
        ddlNombrePlan.DataBind();
        ddlNombrePlan.SelectedValue = "0";
    }

    protected List<ParentescoPolizas> ListaParentesco()
    {
        // ParentescoService parentescoservicio = new ParentescoService();
        ParentescoPolizas parentesco = new ParentescoPolizas();
        List<ParentescoPolizas> LstParentesco = new List<ParentescoPolizas>();
        LstParentesco = polizasSegurosServicio.ListarParentesco(parentesco, (Usuario)Session["usuario"]);
        parentesco.codparentesco = 0;
        parentesco.parentesco = "";
        LstParentesco.Add(parentesco);
        return LstParentesco;

    }
    protected List<ParentescoPolizas> ListaParentescofam()
    {
        // ParentescoService parentescoservicio = new ParentescoService();
        ParentescoPolizas parentesco = new ParentescoPolizas();
        List<ParentescoPolizas> LstParentesco = new List<ParentescoPolizas>();
        LstParentesco = polizasSegurosServicio.ListarParentescofamiliares(parentesco, (Usuario)Session["usuario"]);
        parentesco.codparentesco = 0;
        parentesco.parentesco = "";
        LstParentesco.Add(parentesco);
        return LstParentesco;

    }
    protected List<Genero> ListaGenero()
    {

        Genero genero;
        List<Genero> LstGenero = new List<Genero>();
        genero = new Genero();
        genero.sexo = "";
        LstGenero.Add(genero);
        genero = new Genero();
        genero.sexo = "MASCULINO";
        LstGenero.Add(genero);
        genero = new Genero();
        genero.sexo = "FEMENINO";
        LstGenero.Add(genero);

        // genero.sexo = "FEMENINO";
        //LstGenero.Add(genero);



        return LstGenero;

    }
    protected void LlenarComboParentescofam(DropDownList ddlparentescofam)
    {
        // ParentescoService parentescoservicio = new ParentescoService();
        ParentescoPolizas parentesco = new ParentescoPolizas();
        List<ParentescoPolizas> LstParentesco = new List<ParentescoPolizas>();
        LstParentesco = polizasSegurosServicio.ListarParentescofamiliares(parentesco, (Usuario)Session["usuario"]);
        parentesco.codparentesco = 0;
        parentesco.parentesco = "";
        LstParentesco.Add(parentesco);
        ddlparentescofam.DataSource = LstParentesco;
        ddlparentescofam.DataTextField = "parentesco";
        ddlparentescofam.DataValueField = "parentesco";
        ddlparentescofam.DataBind();
        ddlparentescofam.SelectedValue = "";
    }
    protected void LlenarComboParentesco(DropDownList ddlparentesco)
    {
        // ParentescoService parentescoservicio = new ParentescoService();
        ParentescoPolizas parentesco = new ParentescoPolizas();
        List<ParentescoPolizas> LstParentesco = new List<ParentescoPolizas>();
        LstParentesco = polizasSegurosServicio.ListarParentesco(parentesco, (Usuario)Session["usuario"]);
        parentesco.codparentesco = 0;
        parentesco.parentesco = "";
        LstParentesco.Add(parentesco);
        ddlparentesco.DataSource = LstParentesco;
        ddlparentesco.DataTextField = "parentesco";
        ddlparentesco.DataValueField = "parentesco";
        ddlparentesco.DataBind();
        ddlparentesco.SelectedValue = "";
    }
    protected void LlenarComboParentescoedit(DropDownList ddlparentescoedit)
    {
        // ParentescoService parentescoservicio = new ParentescoService();
        Parentesco parentesco = new Parentesco();
        List<Parentesco> LstParentesco = new List<Parentesco>();
        LstParentesco = parentescoServicio.ListarParentesco(parentesco, (Usuario)Session["usuario"]);
        parentesco.codparentesco = 0;
        parentesco.descripcion = "";
        LstParentesco.Add(parentesco);
        ddlparentescoedit.DataSource = LstParentesco;
        ddlparentescoedit.DataTextField = "descripcion";
        ddlparentescoedit.DataValueField = "codparentesco";
        ddlparentescoedit.DataBind();
        ddlparentescoedit.SelectedValue = "0";
    }

    protected void LlenarComboGenero(DropDownList ddlSexo)
    {
        Genero gen = new Genero();

        List<Genero> LstGenero = new List<Genero>();
        // Usuario usuario = new Usuario();
        // LstParentesco = parentescoServicio.ListarParentesco(parentesco, (Usuario)Session["usuario"]);
        //gen.codparentesco = 0;    
        //gen.sexo = "";
        // LstGenero.Add(gen);


        gen.sexo = "M";
        LstGenero.Add(gen);
        gen.sexo = "F";
        LstGenero.Add(gen);


        ddlSexo.DataSource = LstGenero;
        ddlSexo.DataTextField = "sexo";
        //ddlparentescoed.DataValueField = "codparentesco";
        ddlSexo.DataBind();
        ddlSexo.SelectedValue = "0";

    }




    protected void TxtNumRadicacion_TextChanged(object sender, EventArgs e)
    {
        // ObtenerDatosCredito(idObjeto);
    }
    private void crearBeneficiariosinicial(int consecutivo, String nombresession)
    {
        Beneficiarios pBeneficiarios = new Beneficiarios();
        List<Beneficiarios> LstBeneficiarios = new List<Beneficiarios>();


        pBeneficiarios.consecutivo = consecutivo;
        pBeneficiarios.cod_poliza = -99;
        pBeneficiarios.nombres = "";
        pBeneficiarios.identificacion = "";
        pBeneficiarios.parentesco = "";
        //pBeneficiarios.fecha_nacimiento=null;
        pBeneficiarios.porcentaje = 0;


        LstBeneficiarios.Add(pBeneficiarios);

        Session[nombresession] = LstBeneficiarios;

    }

    private void crearFamiliaresinicial(int consecutivo, String nombresession)
    {
        FamiliaresPolizas pFamiliares = new FamiliaresPolizas();
        List<FamiliaresPolizas> LstFamiliares = new List<FamiliaresPolizas>();


        pFamiliares.consecutivo = consecutivo;
        pFamiliares.cod_poliza = -99;
        pFamiliares.nombres = "";
        pFamiliares.identificacion = "";
        pFamiliares.parentesco = "";
        // pBeneficiarios.fecha_nacimiento ="".ToString();
        pFamiliares.sexo = "";
        pFamiliares.actividad = "";


        LstFamiliares.Add(pFamiliares);

        Session[nombresession] = LstFamiliares;

    }
    private long buscarultconsebenef(List<Beneficiarios> LstBeneficiarios)
    {
        long conseid = 0;
        if (LstBeneficiarios != null)
        {
            foreach (Beneficiarios beneficiariopol in LstBeneficiarios)
            {
                if (beneficiariopol.consecutivo > conseid)
                {
                    conseid = beneficiariopol.consecutivo;
                }
            }
        }
        return conseid;

    }
    private long buscarultconsefam(List<FamiliaresPolizas> LstFamiliares)
    {
        long conseid = 0;
        if (LstFamiliares != null)
        {
            foreach (FamiliaresPolizas familiarespol in LstFamiliares)
            {
                if (familiarespol.consecutivo > conseid)
                {
                    conseid = familiarespol.consecutivo;
                }
            }
        }
        return conseid;

    }


    private void GuardarFamiliares(long conseid)
    {


        List<FamiliaresPolizas> LstFamiliares = new List<FamiliaresPolizas>();

        LstFamiliares = (List<FamiliaresPolizas>)Session["FamiliaresPolizas"];

        foreach (FamiliaresPolizas familiar in LstFamiliares)
        {
            familiar.cod_poliza = conseid;
            if (familiar.nombres != null)
            {
                if (familiar.nombres.Trim().Length > 0)
                {
                    this.familiaresPolizasServicio.CrearFamiliares(familiar, (Usuario)Session["usuario"]);
                }

            }
        }
    }
    private void GuardarBeneficiarios(long conseid)
    {


        List<Beneficiarios> LstBeneficiarios = new List<Beneficiarios>();

        LstBeneficiarios = (List<Beneficiarios>)Session["Beneficiarios"];

        foreach (Beneficiarios beneficiario in LstBeneficiarios)
        {
            beneficiario.cod_poliza = conseid;
            if (beneficiario.nombres != null)
            {
                if (beneficiario.nombres.Trim().Length > 0)
                {
                    this.beneficiariosPolizasServicio.CrearBeneficiarios(beneficiario, (Usuario)Session["usuario"]);
                }

            }
        }
    }

    private Boolean VerificarDatosVacios()
    {
        Boolean continuar = true;
        if (TxtIdentificacion.Text == "" )
        {

            String Error = "Se debe ingresar datos para guardar la poliza";
            this.Lblerror.Text = Error;
            continuar = false;
            this.quitarfilainicialbeneficiarios();
            this.quitarfilainicialfamiliares();
        }


        return continuar;

    }
    private Boolean VerificarPolizavacia()
    {
        Boolean continuar = true;
        if (txtnumpolizafisica.Text == "")
        {

            String Error = "Falta el numero de póliza";
            this.Lblerror.Text = Error;
            continuar = false;
            this.quitarfilainicialbeneficiarios();
            this.quitarfilainicialfamiliares();
        }


        return continuar;

    }
    
    private Boolean VerificarDatosVaciosCombos()
    {
        Boolean continuar = true;

        if (this.ddlTipoPlanSegVida.SelectedItem.Text == "" && ddlTipoPlanAcc.SelectedItem.Text == "")
        {

            String Error = "Por favor escoja un plan";
            this.Lblerror.Text = Error;

            continuar = false;
            this.quitarfilainicialbeneficiarios();
            this.quitarfilainicialfamiliares();
        }
        else
        {
            this.Lblerror.Text = "";
        }

        return continuar;

    }

    private Boolean VerificarPlaSegVida()
    {
        Boolean continuar = true;
        if (this.ddlTipoPlanSegVida.SelectedValue == "0" && ddlTipoPlanAcc.SelectedValue == "0")
        {

            String Error = "Debe seleccionar un plan vida grupo o accidentes.";

            this.Lblerror.Text = Error;
            continuar = false;
            this.quitarfilainicialbeneficiarios();
            this.quitarfilainicialfamiliares();

        }
        else
        {
            this.Lblerror.Text = "";
        }
        return continuar;

    }
    private Boolean VerificarValorPrima()
    {
        Boolean continuar = true;


        if ((TxtValorPrimaMensual.Text == "0" || TxtValorPrimaMensual.Text == "") && (ddlTipoPlanSegVida.SelectedItem.Text != "ANULADO" && ddlTipoPlanAcc.SelectedItem.Text != "ANULADO"))
        {

            String Error = "Debe seleccionar el valor del  plan seleccionado.";
            this.Lblerror.Text = Error;
            continuar = false;
            this.quitarfilainicialbeneficiarios();
            this.quitarfilainicialfamiliares();


        }
        else
        {
            this.Lblerror.Text = "";
        }


        return continuar;
    }


    private Boolean VerificarCheckboxGrupo()
    {
        Boolean continuar = true;

        if (this.chkprimaindividual.Checked == false && chkprimacony.Checked == false)
        {

            String Error = "Debe seleccionar el valor del  plan seleccionado.";
            this.Lblerror.Text = Error;
            continuar = false;
            this.quitarfilainicialbeneficiarios();
            this.quitarfilainicialfamiliares();

        }
        else
        {
            this.Lblerror.Text = "";
        }


        return continuar;

    }
    private Boolean VerificarCheckboxGrupo2()
    {
        Boolean continuar = true;

        if (this.chkprimaopfam.Checked == false && this.chkprimaacc.Checked == false)
        {
            String Error = "Debe seleccionar el valor del  plan seleccionado.";
            this.Lblerror.Text = Error;

            continuar = false;
            this.quitarfilainicialbeneficiarios();
            this.quitarfilainicialfamiliares();


        }
        else
        {
            this.Lblerror.Text = "";
        }
        return continuar;

    }

    private Boolean VerificarFechaNamiento()
    {
       // Boolean continuar = true;
       // String format = "dd/MM/yyyy";

       // DateTime Fechanacimiento = DateTime.ParseExact(TxtFechaNac.Text, format, CultureInfo.InvariantCulture);


       // DateTime Fechanacimiento = DateTime.Parse(TxtFechaNac.Text.ToString());
       // String edad = Convert.ToString(polizasSegurosServicio.CalcularEdad2(Fechanacimiento).ToString());
       // Int64 edad2 = Int32.Parse(edad.ToString());

   
       // PolizasSeguros polizasSeguros = new PolizasSeguros();
       // polizasSeguros = polizasSegurosServicio.ConsultarParametroEdad();
       // Int64 edadmaxima = polizasSeguros.edad_maxima;
       //Int64 edadmaxima = polizasSeguros.valor_maximo;()
       // Int64 edad = edad2.ToString();

       // if (edad2 >= edadmaxima)
       // {
       //     String Error = "Edad máxima para grabar poliza.";
       //     this.Lblerror.Text = Error;
       //     quitarfilainicialbeneficiarios();
       //     quitarfilainicialfamiliares();
       //     continuar = false;
       // }
       // else
       // {
       //     this.Lblerror.Text = "";
       // }
       // return continuar;

        Boolean continuar = true;
        String format = "dd/MM/yyyy";

         DateTime Fechanacimiento = DateTime.ParseExact(TxtFechaNac.Text, format, CultureInfo.InvariantCulture);

       // DateTime Fechanacimiento = DateTime.Parse(TxtFechaNac.Text.ToString());
        Int64 edad = Convert.ToInt32(polizasSegurosServicio.CalcularEdad2(Fechanacimiento));
        PolizasSeguros polizasSeguros = new PolizasSeguros();
        polizasSeguros = polizasSegurosServicio.ConsultarParametroEdad((Usuario)Session["Usuario"]);
        Int64 edadmaxima = polizasSeguros.edad_maxima;
        // Int64 edadmaxima = polizasSeguros.valor_maximo;()
        if (edad >= edadmaxima)
        {
            String Error = "Edad máxima para grabar poliza.";
            this.Lblerror.Text = Error;
            quitarfilainicialbeneficiarios();
            quitarfilainicialfamiliares();
            continuar = false;
        }
        else
        {
            this.Lblerror.Text = "";
        }
        return continuar;

    }
    private Boolean VerificarFechaNamientoBenef()
    {
        Boolean continuar = true;
        TextBox txtnewfechanac = (TextBox)gvBeneficiarios.FooterRow.FindControl("txtnewfechanac");
        //  String Fechanacimiento1 = txtnewfechanac.Text;

        //String Fechanacimiento1 =txtnewfechanac.Text.ToString();
        // String format = "dd/MM/aa";
        //DateTime Fechanacimiento = DateTime.ParseExact(Fechanacimiento1, format,CultureInfo.InvariantCulture);
        String format = "dd/MM/yyyy";

        DateTime Fechanacimiento = DateTime.ParseExact(txtnewfechanac.Text, format, CultureInfo.InvariantCulture);


        //DateTime Fechanacimiento = DateTime.Parse(txtnewfechanac.Text);
        Int64 edad = polizasSegurosServicio.CalcularEdad2(Fechanacimiento);
        //  Beneficiarios beneficiarios = new Beneficiarios();
        PolizasSeguros polizasSeguros = new PolizasSeguros();
        polizasSeguros = polizasSegurosServicio.ConsultarParametroEdadBeneficiarios((Usuario)Session["Usuario"]);
        Int64 edadmaxima = polizasSeguros.edad_maxima;
         //Int64 edadmaxima = polizasSeguros.valor_maximo;

       
        //DateTime dt1 = new DateTime();
        //dt1 = DateTime.Parse(txtnewfechanac.Text.ToString());

        //String format = "dd/MM/yyyy";

        DateTime dt1 = DateTime.ParseExact(txtnewfechanac.Text, format, CultureInfo.InvariantCulture);


        //dt1=DateTime.Parse(txtnewfechanac.Text);
        if (dt1.Date > DateTime.Now.Date)
        {
            String Error1 = "No puede grabar un beneficiario con fecha de nacimiento superior a la actual";
            this.Lblerror.Text = Error1;
            quitarfilainicialbeneficiarios();
            quitarfilainicialfamiliares();
            continuar = false;    
       
        }
        else
        {
            this.Lblerror.Text = "";
        }
        if (edad >= edadmaxima)
        {
            String Error = "Edad máxima del beneficiario.";
            this.Lblerror.Text = Error;
            quitarfilainicialbeneficiarios();
            quitarfilainicialfamiliares();
            continuar = false;
        }
        return continuar;

    }
    private Boolean VerificarFechaNamientofam()
    {
        Boolean continuar = true;
        TextBox txtnewfechanacf = (TextBox)gvFamiliarespolizas.FooterRow.FindControl("txtnewfechanacf");
        String format = "dd/MM/yyyy";

        DateTime Fechanacimiento = DateTime.ParseExact(txtnewfechanacf.Text, format, CultureInfo.InvariantCulture);

       // DateTime Fechanacimiento = DateTime.Parse(txtnewfechanacf.Text);
        Int64 edad = polizasSegurosServicio.CalcularEdad2(Fechanacimiento);
            PolizasSeguros polizasSeguros = new PolizasSeguros();
        polizasSeguros = polizasSegurosServicio.ConsultarParametroEdadBeneficiarios((Usuario)Session["Usuario"]);
        Int64 edadmaxima = polizasSeguros.edad_maxima;
      


        //DateTime dt1 = new DateTime();
        //dt1 = DateTime.Parse(txtnewfechanac.Text.ToString());
        DateTime dt1 = DateTime.ParseExact(txtnewfechanacf.Text, format, CultureInfo.InvariantCulture);

        
        if (dt1.Date > DateTime.Now.Date)
        {
            String Error1 = "No puede grabar un familiar con fecha de nacimiento superior a la actual";
            this.Lblerror.Text = Error1;
            quitarfilainicialbeneficiarios();
            quitarfilainicialfamiliares();
            continuar = false;

        }
        else
        {
            this.Lblerror.Text = "";
        }
        if (edad >= edadmaxima)
        {
            String Error = "Edad máxima del familiar.";
            this.Lblerror.Text = Error;
            quitarfilainicialbeneficiarios();
            quitarfilainicialfamiliares();
            continuar = false;
        }
        return continuar;

    }
   
    private Boolean VerificarFechas()
    {
        Boolean continuar = true;

        if ((TxtVigenPolDesde.Text == "__/__/____") || (TxtVigenPolHasta.Text == "__/__/____"))
        {
            String Error = "Fecha no valida";
            this.Lblerror.Text = Error;
            continuar = false;
            quitarfilainicialbeneficiarios();
            quitarfilainicialfamiliares();
        }
        else
        {
            this.Lblerror.Text = "";
        }
        return continuar;


    }
    private Boolean VerificarDatosBenef()
    {
        Boolean continuar = true;
        Boolean benefpol = false;


        List<Beneficiarios> LstBeneficiarios = new List<Beneficiarios>();
        LstBeneficiarios = (List<Beneficiarios>)Session["Beneficiarios"];

        if (LstBeneficiarios != null)
        {
            foreach (Beneficiarios beneficiarios in LstBeneficiarios)
            {
                if (beneficiarios.identificacion != null)
                {
                    if (beneficiarios.identificacion.Trim().Length > 0)
                    {
                        benefpol = true;
                        break;

                    }

                }
            }
        }

        if (benefpol == false)
        {
            continuar = false;
        }

        return continuar;
    }

    private Boolean VerificarDatosFam()
    {
        Boolean continuar = true;
        Boolean fampol = false;


        List<FamiliaresPolizas> LstFamiliares = new List<FamiliaresPolizas>();
        LstFamiliares = (List<FamiliaresPolizas>)Session["FamiliaresPolizas"];

        if (LstFamiliares != null)
        {
            foreach (FamiliaresPolizas familiares in LstFamiliares)
            {
                if (familiares.identificacion != null)
                {
                    if (familiares.identificacion.Trim().Length > 0)
                    {
                        fampol = true;
                        break;

                    }

                }
            }
        }

        if (fampol == false)
        {
            continuar = false;
        }

        return continuar;
    }

    protected void gvFamiliarespolizas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtnewnombresyapellidosf = (TextBox)gvFamiliarespolizas.FooterRow.FindControl("txtnewnombresyapellidosf");
            TextBox txtnewdocumentoidentidadf = (TextBox)gvFamiliarespolizas.FooterRow.FindControl("txtnewdocumentoidentidadf");
            DropDownList ddlsexo = (DropDownList)gvFamiliarespolizas.FooterRow.FindControl("ddlsexo");
            DropDownList ddlparentescofam = (DropDownList)gvFamiliarespolizas.FooterRow.FindControl("ddlparentescofam");
            TextBox txtnewfechanacf = (TextBox)gvFamiliarespolizas.FooterRow.FindControl("txtnewfechanacf");
            TextBox txtnewactividadf = (TextBox)gvFamiliarespolizas.FooterRow.FindControl("txtnewactividadf");


            List<FamiliaresPolizas> LstFamiliares = new List<FamiliaresPolizas>();

            LstFamiliares = (List<FamiliaresPolizas>)Session["FamiliaresPolizas"];

            if (txtnewnombresyapellidosf.Text.Trim().Length == 0 || txtnewdocumentoidentidadf.Text.Trim().Length == 0 || txtnewactividadf.Text.Trim().Length == 0 || ddlparentescofam.SelectedItem.Text == "" || ddlsexo.SelectedItem.Text == "")
            {
                String Error = "Debe diligenciar los datos de los Familiares";
                this.Lblerror.Text = Error;

                if (txtnewfechanacf.Text == "__/__/____")
                {
                    String Error1 = "Fecha no valida";
                    this.Lblerror.Text = Error1;


                }
                quitarfilainicialbeneficiarios();
                quitarfilainicialfamiliares();

            }

            else
            {

                if (VerificarFechasGrillasfam())
                {

                    FamiliaresPolizas familiarespolizas = new FamiliaresPolizas();
                    familiarespolizas.consecutivo = this.buscarultconsefam(LstFamiliares) + 1;
                    familiarespolizas.cod_poliza = -99;
                    familiarespolizas.nombres = txtnewnombresyapellidosf.Text;
                    familiarespolizas.identificacion = txtnewdocumentoidentidadf.Text;
                    familiarespolizas.sexo = ddlsexo.SelectedItem.Text;
                    familiarespolizas.parentesco = ddlparentescofam.SelectedItem.Text;
                    //if (familiarespolizas.fecha_nacimiento != DateTime.MinValue) txtnewfechanacf.Text = familiarespolizas.fecha_nacimiento.ToShortDateString();
                    if (this.VerificarFechaNamientofam())
                    {
                        String Fechanacimiento = txtnewfechanacf.Text.ToString();
                        String format = "dd/MM/yyyy";
                        DateTime fecha_nacimiento = DateTime.ParseExact(Fechanacimiento, format, CultureInfo.InvariantCulture);
                        familiarespolizas.fecha_nacimiento = fecha_nacimiento;



                        familiarespolizas.actividad = txtnewactividadf.Text;

                        if (this.validarreglasfamiliares(familiarespolizas))
                        {

                            gvFamiliarespolizas.EditIndex = -1;
                            if (!this.operacion.Equals("N"))
                            {
                                familiarespolizas.cod_poliza = Convert.ToInt64(lblcodpoliza.Text);
                                this.familiaresPolizasServicio.CrearFamiliares(familiarespolizas, (Usuario)Session["usuario"]);
                            }
                            LstFamiliares.Add(familiarespolizas);

                            //  ConsultarFamiliares();
                            MostrarDatosFamiliares();
                        }
                    }
                }
            }
        }
    }
    protected void ddlTipoPlanSegVida_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlTipoPlanSegVida.SelectedItem.Text == "")
        {
            this.Lblerror.Text = "Debe seleccionar un plan ";

        }

        else
        {
            Lblerror.Text = "";
            this.MostrarDatosAmparos("Vida Grupo", ddlTipoPlanSegVida, gvVidaGrupo);
            quitarfilainicialbeneficiarios();
            quitarfilainicialfamiliares();
            this.TotalizarPrimas();

        }


    }
    protected void ddlTipoPlanAcc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTipoPlanAcc.SelectedItem.Text == "")
        {
            this.Lblerror.Text = "Debe seleccionar un plan ";
        }

        else
        {
            Lblerror.Text = "";

            this.MostrarDatosAmparos("Accidentes Personales", ddlTipoPlanAcc, gvAccPers);
            quitarfilainicialbeneficiarios();
            quitarfilainicialfamiliares();
            this.TotalizarPrimas();

        }
    }

    protected void ObtenerDatosPlanesSeguros(String pIdObjeto, string tipo)
    {

        try
        {
            PlanesSeguros planesSeguros = new PlanesSeguros();
            //  if ((pIdObjeto != null) && (pIdObjeto !="0") )
            //{
            planesSeguros.tipo_plan = Int64.Parse(pIdObjeto);
            planesSeguros = planSegurosServicio.ConsultarPlanesSeguros(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            PlanesSegurosAmparos planesSegurosAmparos = new PlanesSegurosAmparos();



            if (!string.IsNullOrEmpty(planesSeguros.tipo_plan.ToString()))
            {
                if (tipo.Equals("Vida Grupo"))
                {
                    txtPrimaindividual.Text = planesSeguros.prima_individual.ToString();
                    txtPrimacony.Text = planesSeguros.prima_conyuge.ToString();

                }
                if (tipo.Equals("Accidentes Personales"))
                {
                    txtPrimaacc.Text = planesSeguros.prima_accidentes_individual.ToString();
                    txtPrimaopfam.Text = planesSeguros.prima_accidentes_familiar.ToString();
                }
            }

            //}

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(planSegurosServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

    protected void TotalizarPrimas()
    {
        long total = 0;
        if (chkprimaindividual.Checked)
        {
            total = Convert.ToInt64("0" + this.txtPrimaindividual.Text);
        }
        if (chkprimacony.Checked)
        {
            total = Convert.ToInt64("0" + this.txtPrimacony.Text) + total;
        }
        if (chkprimaacc.Checked)
        {
            total = Convert.ToInt64("0" + this.txtPrimaacc.Text) + total;
        }
        if (this.chkprimaopfam.Checked)
        {
            total = Convert.ToInt64("0" + this.txtPrimaopfam.Text) + total;
        }
        this.TxtValorPrimaMensual.Text = total.ToString();

    }
    protected void chkprimaindividual_CheckedChanged(object sender, EventArgs e)
    {

        if (chkprimaindividual.Checked)
        {
            chkprimacony.Checked = false;

        }

        TotalizarPrimas();
        quitarfilainicialbeneficiarios();
        quitarfilainicialfamiliares();
    }

    protected void chkprimaopfam_CheckedChanged(object sender, EventArgs e)
    {
        if (chkprimaopfam.Checked)
        {

            chkprimaacc.Checked = false;


        }
        TotalizarPrimas();
        quitarfilainicialbeneficiarios();
        quitarfilainicialfamiliares();
    }


    protected void chkprimacony_CheckedChanged(object sender, EventArgs e)
    {
        if (chkprimacony.Checked)
        {
            chkprimaindividual.Checked = false;


        }
        TotalizarPrimas();
        quitarfilainicialbeneficiarios();
        quitarfilainicialfamiliares();

    }
    protected void chkprimaacc_CheckedChanged(object sender, EventArgs e)
    {
        if (chkprimaacc.Checked)
        {
            chkprimaopfam.Checked = false;


        }
        TotalizarPrimas();
        quitarfilainicialbeneficiarios();
        quitarfilainicialfamiliares();
    }
    protected void BuscarCredito_Click(object sender, EventArgs e)
    {
        Response.Redirect("Filtrar.aspx", false); 
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void txtPrimaopfam_TextChanged(object sender, EventArgs e)
    {

    }
    protected void gvAccPers_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void ddlparentesco_SelectedIndexChanged(object sender, EventArgs e)
    {
        // this.MostrarDatosParentesfam();

    }


    protected void gvBeneficiarios_DataBound(object sender, EventArgs e)
    {


    }
    protected void gvBeneficiarios_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Control ctrl = e.Row.FindControl("ddlparentesco");
            if (ctrl != null)
            {
                DropDownList dd = ctrl as DropDownList;

                LlenarComboParentesco(dd);
            }
        }


        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            List<ParentescoPolizas> LstParentesco;

            DropDownList ddlparentescoedit = (DropDownList)e.Row.FindControl("ddlparentescoedit");

            if (ddlparentescoedit != null)
            {
                LstParentesco = this.ListaParentesco();
                ddlparentescoedit.DataSource = LstParentesco;
                ddlparentescoedit.DataTextField = "parentesco";
                ddlparentescoedit.DataValueField = "parentesco";
                ddlparentescoedit.SelectedValue = "";
                // ddlparentescoedit.DataBind();
            }
        }


    }
    protected void gvFamiliarespolizas_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Control ctrl = e.Row.FindControl("ddlparentescofam");
            if (ctrl != null)
            {
                DropDownList dd = ctrl as DropDownList;

                this.LlenarComboParentescofam(dd);
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            List<ParentescoPolizas> LstParentescofam;

            DropDownList ddlparentescoeditfam = (DropDownList)e.Row.FindControl("ddlparentescoeditfam");

            if (ddlparentescoeditfam != null)
            {
                LstParentescofam = this.ListaParentescofam();
                ddlparentescoeditfam.DataSource = LstParentescofam;
                ddlparentescoeditfam.DataTextField = "parentesco";
                ddlparentescoeditfam.DataValueField = "parentesco";
                ddlparentescoeditfam.SelectedValue = "";
                //ddlparentescoeditfam.DataBind();
            }



            List<Genero> LstGenero;
            DropDownList ddlSexoedit = (DropDownList)e.Row.FindControl("ddlSexoedit");
            if (ddlSexoedit != null)
            {
                LstGenero = this.ListaGenero();
                ddlSexoedit.DataSource = LstGenero;
                ddlSexoedit.DataTextField = "Sexo";
                ddlSexoedit.DataValueField = "Sexo";
                ddlSexoedit.SelectedValue = "";
                // ddlSexoedit.DataBind();
            }
        }

    }

    protected void ddlparentescoed_DataBound(object sender, EventArgs e)
    {
        MostrarDatosParentesco();
    }
    protected void ddlparentescoed_DataBinding(object sender, EventArgs e)
    {

    }
    protected void ddlparentescoedit_Load(object sender, EventArgs e)
    {
        DropDownList ddlparentesco;
        ddlparentesco = (DropDownList)sender;
        this.LlenarComboParentesco(ddlparentesco);
    }
    protected void ddlSexoedit_Load(object sender, EventArgs e)
    {
        DropDownList ddlSexo;
        ddlSexo = (DropDownList)sender;
        this.LlenarComboGenero(ddlSexo);
    }
    protected void ddlSexoedit_DataBound(object sender, EventArgs e)
    {
        this.MostrarDatosGenero();
    }
    protected void ddlSexo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this.MostrarDatosGenero();
    }
    protected void ddlSexo_Load(object sender, EventArgs e)
    {
        //    this.MostrarDatosGenero();

    }
    protected void ddlparentescoedit_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlparentescoedit_Load1(object sender, EventArgs e)
    {

    }
    protected void ddlparentescoeditfam_DataBound(object sender, EventArgs e)
    {
        MostrarDatosParentesfam();
    }
    protected void ddlparentescofam_Load(object sender, EventArgs e)
    {
        DropDownList ddlparentescofam;
        ddlparentescofam = (DropDownList)sender;
        this.LlenarComboParentescofam(ddlparentescofam);
    }
    protected void ddlparentescofam_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void TxtNomAsegu_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtTelefono_TextChanged(object sender, EventArgs e)
    {

    }
    public String vacios(String texto)
    {
        if (String.IsNullOrEmpty(texto))
        {
            return "0";
        }
        else
        {
            return texto;
        }
    }
    protected void btnInforme_Click(object sender, EventArgs e)
    {
       String pIdObjeto= lblcodpoliza.Text;
     
      this.ObtenerDatosimprimir(pIdObjeto);
     
        //DataTable dtplanacc;
        ReportParameter[] param = new ReportParameter[26];

        param[0] = new ReportParameter("PFecha", Convert.ToString(DateTime.Now.ToShortDateString()));
        param[1] = new ReportParameter("PTomadorSeguro", vacios(TxtTomadorSeg.Text));
        param[2] = new ReportParameter("PNitTomadorSeguro", vacios(TxtNit.Text));
        param[3] = new ReportParameter("POficina", vacios(TxtOficina.Text));
        param[4] = new ReportParameter("PEjecutivo", vacios(TxtNomAsesor.Text));
        param[5] = new ReportParameter("PCedulaEjecutivo", vacios(TxtIdenAsesor.Text));
        param[6] = new ReportParameter("PVigenciaDesde", vacios(TxtVigenPolDesde.Text));
        param[7] = new ReportParameter("PVigenciaHasta", vacios(TxtVigenPolHasta.Text));
        param[8] = new ReportParameter("PAsegurado", vacios(TxtNombres.Text));
        param[9] = new ReportParameter("PCedulaAsegurado", vacios(TxtIdentificacion.Text));
        param[10] = new ReportParameter("PFechaNacimiento", vacios(TxtFechaNac.Text));
        param[11] = new ReportParameter("PEstadoCivil", vacios(TxtEstadoCivil.Text));
        param[12] = new ReportParameter("PSexo", vacios(TxtSexo.Text));
        param[13] = new ReportParameter("POcupacion", vacios(TxtOcupacion.Text));
        param[14] = new ReportParameter("PActividades", vacios(TxtActividades.Text));
        param[15] = new ReportParameter("PDireccionResidencia", vacios(TxtDireccion.Text));
        param[16] = new ReportParameter("PTelefono", vacios(TxtTelefono.Text));
        param[17] = new ReportParameter("PCiudad", vacios(TxtCiudad.Text));
        param[18] = new ReportParameter("PDireccionElectronica", vacios(TxtEmail.Text));
        param[19] = new ReportParameter("PCelular", vacios(TxtCelular.Text));
        param[20] = new ReportParameter("PValorPrima", vacios(TxtValorPrimaMensual.Text));
        param[21] = new ReportParameter("PCertificado", vacios(this.TxtNumRadicacion.Text));
        param[22] = new ReportParameter("PCodoficina", vacios(this.Txtcodoficina.Text));
        param[23] = new ReportParameter("PPlan", vacios(this.Txtplan.Text));
        param[24] = new ReportParameter("PValorTotal", Convert.ToInt64(ValorPoliza).ToString("$0,0"));
        param[25] = new ReportParameter("ImagenReport", ImagenReporte());
        
        mvReporte.Visible = true;
        ReportViewPoliza.LocalReport.EnableExternalImages = true;
        ReportViewPoliza.LocalReport.SetParameters(param);
        
        ReportViewPoliza.LocalReport.DataSources.Clear();
        ReportDataSource rdsbene = new ReportDataSource("DataSetBeneficiarios", CrearDataTableBeneficiarios());
        ReportDataSource rdsfam = new ReportDataSource("DataSetFamiliares", CrearDataTableFamiliares());
        ReportDataSource rdsplan = new ReportDataSource("DataSetPlan", CrearDataTablePlanSegVida());
        ReportDataSource rdsplanacc = new ReportDataSource("DataSetPlanacc", CrearDataTablePlanSegAcc());

        ReportViewPoliza.LocalReport.DataSources.Add(rdsbene);
        ReportViewPoliza.LocalReport.DataSources.Add(rdsfam);
        ReportViewPoliza.LocalReport.DataSources.Add(rdsplan);
        ReportViewPoliza.LocalReport.DataSources.Add(rdsplanacc);

        
        
        
        ReportViewPoliza.LocalReport.Refresh();        
        mvReporte.ActiveViewIndex = 0;
        mvReporte.Visible = true;

          Warning[] warnings;
            String[] streamids;
            String mimetype;
            String encoding;
            String extension;
            string _sSuggestedName = String.Empty;
            /* ReportViewer ReportViewer2;
             LocalReport objRDLC = new LocalReport();
             ReportViewer2.LocalReport.ReportEmbeddedResource = "ReporteHabeasData.rdlc";
             ReportViewer2.LocalReport.DisplayName = _sSuggestedName;
             objRDLC.DataSources.Clear();*/

            byte[] bytes = this.ReportViewPoliza.LocalReport.Render("PDF", null, out mimetype, out  encoding, out extension, out streamids, out  warnings);


            System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                  
            string ruta = Server.MapPath("~/Archivos/Polizas/");

         
         
            if (Directory.Exists(ruta))
            {
                String Fecha = Convert.ToString(DateTime.Now.ToString("MMddyyyy"));
               

              
                String fileName = "poliza-" + TxtNumRadicacion.Text + "-" + Fecha  + ".pdf";
                string savePath = ruta + fileName;

                FileStream fs = new FileStream(savePath, FileMode.Create);

                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                FileStream archivo = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                FileInfo file = new FileInfo(savePath);
                Response.Clear();
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/pdf";
                Response.TransmitFile(file.FullName);
                Response.End();
                          }
            
        }
    public DataTable CrearDataTableBeneficiarios()
    {
        List<Beneficiarios> lstBeneficiarios = new List<Beneficiarios>();
        Beneficiarios beneficiario = new Beneficiarios();
        beneficiario.cod_poliza = Convert.ToInt64(this.lblcodpoliza.Text);
        lstBeneficiarios = beneficiariosPolizasServicio.ListarBeneficiarios(beneficiario, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();

        table.Columns.Add("nombres_apellidos");
        table.Columns.Add("documento");
        table.Columns.Add("parentesco");
        table.Columns.Add("fecha_nac");
        table.Columns.Add("porcentaje");

        foreach (Beneficiarios item in lstBeneficiarios)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = item.nombres;
            datarw[1] = item.identificacion;
            datarw[2] = item.parentesco;
            datarw[3] = item.fecha_nacimiento.ToShortDateString();
            datarw[4] = item.porcentaje;           
            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTableFamiliares()
    {
        List<FamiliaresPolizas> LstFamiliaresPolizas = new List<FamiliaresPolizas>();
        FamiliaresPolizas familiares = new FamiliaresPolizas();
        familiares.cod_poliza = Convert.ToInt64(this.lblcodpoliza.Text);
        LstFamiliaresPolizas = familiaresPolizasServicio.ListarFamiliares(familiares, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();

        table.Columns.Add("nombres_apellidosf");
        table.Columns.Add("documentof");
        table.Columns.Add("sexof");
        table.Columns.Add("parentescof");
        table.Columns.Add("fecha_nacf");
        table.Columns.Add("actividadf");

        foreach (FamiliaresPolizas item in LstFamiliaresPolizas)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = item.nombres;
            datarw[1] = item.identificacion;
            datarw[2] = item.sexo;
            datarw[2] = item.parentesco;
            datarw[3] = item.fecha_nacimiento.ToShortDateString();
            datarw[4] = item.actividad;
            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTablePlanSegVida()
    {
        PlanesSegurosAmparosService planSegurosAmparosServicio = new PlanesSegurosAmparosService();
        //PlanesSegurosService planesSegurosServicio = new PlanesSegurosService();
        PlanesSegurosAmparos pPlanesSegurosAmparos = new PlanesSegurosAmparos();
        List<PlanesSegurosAmparos> LstplanesSegurosAmparos = new List<PlanesSegurosAmparos>();
        pPlanesSegurosAmparos.tipo_plan = Convert.ToInt64("0" + ddlTipoPlanSegVida.SelectedValue);
        pPlanesSegurosAmparos.tipo = "Vida Grupo";
        LstplanesSegurosAmparos = planSegurosAmparosServicio.ListarPlanesSegurosAmparos(pPlanesSegurosAmparos, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();

        table.Columns.Add("amparo");
        table.Columns.Add("valor_cubierto");


        foreach (PlanesSegurosAmparos item in LstplanesSegurosAmparos)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = item.descripcion;
            datarw[1] = item.valor_cubierto.ToString("0,0", CultureInfo.InvariantCulture);          
            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTablePlanSegAcc()
    {
        PlanesSegurosAmparosService planSegurosAmparosServicio = new PlanesSegurosAmparosService();
        //PlanesSegurosService planesSegurosServicio = new PlanesSegurosService();
        PlanesSegurosAmparos pPlanesSegurosAmparos = new PlanesSegurosAmparos();
        List<PlanesSegurosAmparos> LstplanesSegurosAmparos = new List<PlanesSegurosAmparos>();
        pPlanesSegurosAmparos.tipo_plan = Convert.ToInt64("0" + ddlTipoPlanAcc.SelectedValue);
        pPlanesSegurosAmparos.tipo = "Accidentes Personales";
        LstplanesSegurosAmparos = planSegurosAmparosServicio.ListarPlanesSegurosAmparos(pPlanesSegurosAmparos, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();

        table.Columns.Add("amparoacc");
        table.Columns.Add("valor_cubiertoacc");
        table.Columns.Add("valor_cubierto_conyugeacc");
        table.Columns.Add("valor_cubierto_hijosacc");



        foreach (PlanesSegurosAmparos item in LstplanesSegurosAmparos)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = item.descripcion;
            datarw[1] = item.valor_cubierto.ToString("0,0", CultureInfo.InvariantCulture); 
            datarw[2] = item.valor_cubierto_conyuge.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[3] = item.valor_cubierto_hijos.ToString("0,0", CultureInfo.InvariantCulture); 

            table.Rows.Add(datarw);
        }
        return table;
    }
    
}