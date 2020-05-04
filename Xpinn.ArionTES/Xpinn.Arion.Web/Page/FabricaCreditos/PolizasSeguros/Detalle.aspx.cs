using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;


public partial class Detalle : GlobalWeb
{

    PolizasSegurosService polizasSegurosServicio = new PolizasSegurosService();

    PlanesSegurosService planSegurosServicio = new PlanesSegurosService();

    PlanesSegurosAmparosService planSegurosAmparosServicio= new PlanesSegurosAmparosService();

    TomadorPolizaService tomadorPolizaServicio = new TomadorPolizaService();
    
    BeneficiariosService beneficiariosPolizasServicio = new BeneficiariosService();
   
    FamiliaresPolizasService familiaresPolizasServicio  = new FamiliaresPolizasService();
        
  
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(polizasSegurosServicio.CodigoPrograma.ToString(), "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(polizasSegurosServicio.CodigoPoliza + "A", "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
            {
            if (!IsPostBack)
            {
                LlenarComboTipoPlan(this.ddlTipoPlanSegVida);
                LlenarComboTipoPlan(this.ddlTipoPlanAcc);

                if (Session[polizasSegurosServicio.CodigoPoliza + ".id"] != null)
                {
                    idObjeto = Session[polizasSegurosServicio.CodigoPoliza + ".id"].ToString();
                    Session.Remove(polizasSegurosServicio.CodigoPoliza + ".id");
                    ObtenerDatos(idObjeto);
                   
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(polizasSegurosServicio.GetType().Name + "D", "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
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
        Session["operacion"] = "";
        Navegar(Pagina.Editar);
    }

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }
    
           
    protected void ObtenerDatos(String pIdObjeto)    {
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
                TxtNumPoliza.Text = polizasSeguros.num_poliza.ToString();

                if (!string.IsNullOrEmpty(polizasSeguros.oficina))
                    TxtOficina.Text = polizasSeguros.oficina.ToString();

                TxtIdenAsesor.Text = polizasSeguros.ident_asesor.ToString();

                if (!string.IsNullOrEmpty(polizasSeguros.nombre_asesor))
                    TxtNomAsesor.Text = polizasSeguros.nombre_asesor.ToString();

                TxtValorPrimaMensual.Text = polizasSeguros.valor_prima.ToString();
                TxtVigenPolDesde.Text = polizasSeguros.fec_ini_vig.ToString("MM/dd/yyyy");
                TxtVigenPolHasta.Text = polizasSeguros.fec_fin_vig.ToString("MM/dd/yyyy");
                TxtFechaNac.Text = polizasSeguros.fechanacimiento.ToString("MM/dd/yyyy");

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

                polizasSegurosvida = polizasSegurosServicio.ConsultarPolizasSegurosVida(Convert.ToInt64(polizasSeguros.cod_poliza.ToString()), "Vida Grupo", (Usuario)Session["usuario"]);

                if (polizasSegurosvida != null)
                {

                    this.ddlTipoPlanSegVida.SelectedValue = polizasSegurosvida.tipo_plan.ToString();
                    if (polizasSegurosvida.individual == "1")
                    {
                        this.chkprimaindividual.Checked = true;
                        this.txtPrimaindividual.Text = polizasSegurosvida.valor_prima.ToString();
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


                if (polizasSegurosvida!=null)
                {
                    this.ddlTipoPlanAcc.SelectedValue = polizasSegurosvida.tipo_plan.ToString();
                    if (polizasSegurosvida.individual == "1")
                    {
                        this.chkprimaacc.Checked = true;
                        this.txtPrimaacc.Text = polizasSegurosvida.valor_prima.ToString();
                    }
                    if (polizasSegurosvida.individual == "0")
                    {
                        this.chkprimaopfam.Checked = true;
                        this.txtPrimaopfam.Text = polizasSegurosvida.valor_prima.ToString();
                    }
                    this.MostrarDatosAmparos("Accidentes Personales", this.ddlTipoPlanAcc, this.gvAccPers);
                }

                if (!string.IsNullOrEmpty(polizasSeguros.tipo_plan_s.ToString()))               

                    this.TxtValorPrimaMensual.Text = polizasSeguros.valor_prima_mensual.ToString();
                
              
                ListarDatosBeneficiarios(polizasSeguros.cod_poliza);
                ListarDatosFamiliares(polizasSeguros.cod_poliza);
                ObtenerDatosTomadorPoliza();
            }
            

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(polizasSegurosServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

    private void ConsultarFamiliares()
    {
        String cod_poliza = Convert.ToInt64(this.lblcodpoliza.Text).ToString();
        MostrarDatosFamiliares("cod_poliza", gvFamiliares, "FamiliaresPolizas");

    }

    private void ConsultarBeneficiarios()
    {
        String cod_poliza = Convert.ToInt64(this.lblcodpoliza.Text).ToString();
        MostrarDatosBeneficiarios("cod_poliza", gvBeneficiarios, "Beneficiarios");
    }

    private void MostrarDatosBeneficiarios(String cod_poliza, GridView gvBeneficiarios, String Var)
    {

        Beneficiarios pBeneficiarios = new Beneficiarios();
        List<Beneficiarios> LstBeneficiarios = new List<Beneficiarios>();
        pBeneficiarios.cod_poliza = Convert.ToInt64("cod_poliza");
        LstBeneficiarios = beneficiariosPolizasServicio.ListarBeneficiarios(pBeneficiarios, (Usuario)Session["usuario"]);
        gvBeneficiarios.DataSource = LstBeneficiarios;
        gvBeneficiarios.DataBind();
        Session[Var] = LstBeneficiarios;
    }

    private void MostrarDatosFamiliares(String cod_poliza, GridView gvFamiliares, String Var)
    {

        FamiliaresPolizas pFamiliares = new FamiliaresPolizas();
        List<FamiliaresPolizas> LstFamiliares = new List<FamiliaresPolizas>();
        pFamiliares.cod_poliza = Convert.ToInt64("cod_poliza");
        LstFamiliares = familiaresPolizasServicio.ListarFamiliares(pFamiliares, (Usuario)Session["usuario"]);
        gvFamiliares.DataSource = LstFamiliares;
        gvFamiliares.DataBind();
        Session[Var] = LstFamiliares;
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
   
    protected void LlenarComboTipoPlan(DropDownList ddlNombrePlan)
    {
        PlanesSegurosService planesSegurosService = new PlanesSegurosService();
        PlanesSeguros planesSeguros = new PlanesSeguros();
       
        List<PlanesSeguros> LstplanesSeguros = new List<PlanesSeguros>();

        Usuario usuario = new Usuario();
        usuario = (Usuario)Session["usuario"];
        LstplanesSeguros = planesSegurosService.ListarPlanesSeguros(planesSeguros, usuario);
        planesSeguros.tipo_plan = 0;
        planesSeguros.descripcion = "";
        LstplanesSeguros.Add(planesSeguros);
        ddlNombrePlan.DataSource = LstplanesSeguros;
        ddlNombrePlan.DataTextField = "descripcion";
        ddlNombrePlan.DataValueField = "tipo_plan";
        ddlNombrePlan.DataBind();
        ddlNombrePlan.SelectedValue = "0";
    }

    protected void ddlTipoPlanSegVida_SelectedIndexChanged(object sender, EventArgs e)
    {
          this.MostrarDatosAmparos("Vida Grupo", ddlTipoPlanSegVida, gvVidaGrupo);
    }

    protected void ddlTipoPlanAcc_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.MostrarDatosAmparos("Accidentes Personales", ddlTipoPlanAcc, gvAccPers);  
    }

    private void MostrarDatosAmparos(String Tipo,DropDownList ddlTipo,GridView gvAmparos)
    {
        PlanesSegurosAmparos pPlanesSegurosAmparos = new PlanesSegurosAmparos();
        List<PlanesSegurosAmparos> LstPlanesSegurosAmparos = new List<PlanesSegurosAmparos>();
        pPlanesSegurosAmparos.tipo_plan = Convert.ToInt64("0" + ddlTipo.SelectedValue);
        pPlanesSegurosAmparos.tipo = Tipo;

        LstPlanesSegurosAmparos = planSegurosAmparosServicio.ListarPlanesSegurosAmparos(pPlanesSegurosAmparos, (Usuario)Session["usuario"]);
        gvAmparos.DataSource = LstPlanesSegurosAmparos;
        gvAmparos.DataBind();
   
    }

    private void ListarDatosBeneficiarios(Int64 cod_poliza)
    {
        Beneficiarios beneficiarios = new Beneficiarios();
        beneficiarios.cod_poliza = cod_poliza;

        List<Beneficiarios> LstBeneficiariosData = new List<Beneficiarios>();

        LstBeneficiariosData = beneficiariosPolizasServicio.ListarBeneficiarios(beneficiarios, (Usuario)Session["usuario"]);

        this.gvBeneficiarios.DataSource = LstBeneficiariosData;
        gvBeneficiarios.DataBind();
        Session["Beneficiarios"] = LstBeneficiariosData;

    }

    private void ListarDatosFamiliares(Int64 cod_poliza)
    {
        FamiliaresPolizas familiares = new FamiliaresPolizas();
        familiares.cod_poliza = cod_poliza;
        
        List<FamiliaresPolizas> LstFamiliaresPolizasData = new List<FamiliaresPolizas>();

        LstFamiliaresPolizasData = familiaresPolizasServicio.ListarFamiliares(familiares, (Usuario)Session["usuario"]);

        this.gvFamiliares.DataSource = LstFamiliaresPolizasData;
        gvFamiliares.DataBind();
        Session["Familiares"] = LstFamiliaresPolizasData;
        
    }

    protected void gvFamiliares_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void gvFamiliares_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvFamiliares.EditIndex = e.NewEditIndex;
        long codpoliza = Convert.ToInt64(lblcodpoliza.Text);
        this.ListarDatosFamiliares(codpoliza);
    }

    protected void gvFamiliares_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvFamiliares.DataKeys[e.RowIndex].Values[0].ToString());

        try
        {

            familiaresPolizasServicio.EliminarFamiliares(conseID, (Usuario)Session["usuario"]);
         
            long codpoliza = Convert.ToInt64(lblcodpoliza.Text);
            this.ListarDatosFamiliares(codpoliza);

        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(familiaresPolizasServicio.consecutivo + "L", "gvFamiliares_RowDeleting", ex);
        }
    }

    protected void gvFamiliares_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
            
    }

    protected void gvFamiliares_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txtnombresyapellidos = (TextBox)gvFamiliares.Rows[e.RowIndex].FindControl("txtnombresyapellidos");
        TextBox txtdocumentoidentidad = (TextBox)gvFamiliares.Rows[e.RowIndex].FindControl("txtdocumentoidentidad");
        TextBox txtsexo = (TextBox)gvFamiliares.Rows[e.RowIndex].FindControl("txtsexo");
        TextBox txtparentesco = (TextBox)gvFamiliares.Rows[e.RowIndex].FindControl("txtparentesco");
        TextBox txtfechanac = (TextBox)gvFamiliares.Rows[e.RowIndex].FindControl("txtfechanac");
        TextBox txtactividad = (TextBox)gvFamiliares.Rows[e.RowIndex].FindControl("txtactividad");
        
        
        long conseID = Convert.ToInt32(gvFamiliares.DataKeys[e.RowIndex].Values[0].ToString());


        FamiliaresPolizas familiares = new FamiliaresPolizas();

        familiares.consecutivo = conseID;
        familiares.cod_poliza = Convert.ToInt64(lblcodpoliza.Text);
        familiares.nombres = txtnombresyapellidos.Text;
        familiares.identificacion = Convert.ToString(txtdocumentoidentidad.Text); 
        familiares.sexo = txtsexo.Text;
        familiares.parentesco = txtparentesco.Text;
        familiares.fecha_nacimiento =Convert.ToDateTime(txtfechanac.Text);
        familiares.actividad = txtactividad.Text;


        gvFamiliares.EditIndex = -1;

        familiaresPolizasServicio.ModificarFamiliares(familiares, (Usuario)Session["usuario"]);


        this.ListarDatosFamiliares(familiares.cod_poliza);
      
    }

    protected void gvFamiliares_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvFamiliares.EditIndex = -1;
        long codpoliza = Convert.ToInt64(lblcodpoliza.Text);
        this.ListarDatosFamiliares(codpoliza);


    }

    protected void gvFamiliares_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }

    protected void gvBeneficiarios_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvBeneficiarios.EditIndex = e.NewEditIndex;
        long codpoliza = Convert.ToInt64(lblcodpoliza.Text);
        this.ListarDatosBeneficiarios(codpoliza);
    }

    protected void gvBeneficiarios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvBeneficiarios.EditIndex = -1;
        long codpoliza = Convert.ToInt64(lblcodpoliza.Text);
        this.ListarDatosBeneficiarios(codpoliza);

    }
    protected void gvBeneficiarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvBeneficiarios.DataKeys[e.RowIndex].Values[0].ToString());

        try
        {

            beneficiariosPolizasServicio.EliminarBeneficiarios(conseID, (Usuario)Session["usuario"]);
            // Actualizar();
            long codpoliza = Convert.ToInt64(lblcodpoliza.Text);
            this.ListarDatosBeneficiarios(codpoliza);

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
        TextBox txtparentesco = (TextBox)gvBeneficiarios.Rows[e.RowIndex].FindControl("txtparentesco");
        TextBox txtfechanac = (TextBox)gvBeneficiarios.Rows[e.RowIndex].FindControl("txtfechanac");
        TextBox txtporcentaje = (TextBox)gvBeneficiarios.Rows[e.RowIndex].FindControl("txtporcentaje");


        long conseID = Convert.ToInt32(gvBeneficiarios.DataKeys[e.RowIndex].Values[0].ToString());


        Xpinn.FabricaCreditos.Entities.Beneficiarios beneficiarios = new Xpinn.FabricaCreditos.Entities.Beneficiarios();

        beneficiarios.consecutivo = conseID;
        beneficiarios.cod_poliza = Convert.ToInt64(lblcodpoliza.Text);
        beneficiarios.nombres = txtnombresyapellidos.Text;
        beneficiarios.identificacion = Convert.ToString(txtdocumentoidentidad.Text);
        beneficiarios.parentesco = txtparentesco.Text;
        beneficiarios.fecha_nacimiento = Convert.ToDateTime(txtfechanac.Text);
        beneficiarios.porcentaje = Convert.ToInt64(txtporcentaje.Text);


        gvBeneficiarios.EditIndex = -1;

        beneficiariosPolizasServicio.ModificarBeneficiarios(beneficiarios, (Usuario)Session["usuario"]);


        this.ListarDatosBeneficiarios(beneficiarios.cod_poliza);

    }

    protected void gvBeneficiarios_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void gvBeneficiarios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtnewnombresyapellidos = (TextBox)gvBeneficiarios.FooterRow.FindControl("txtnewnombresyapellidos");
            TextBox txtnewdocumentoidentidad = (TextBox)gvBeneficiarios.FooterRow.FindControl("txtnewdocumentoidentidad");
            TextBox txtnewparentesco = (TextBox)gvBeneficiarios.FooterRow.FindControl("txtnewparentesco");
            TextBox txtnewfechanac = (TextBox)gvBeneficiarios.FooterRow.FindControl("txtnewfechanac");
            TextBox txtnewporcentaje = (TextBox)gvBeneficiarios.FooterRow.FindControl("txtnewporcentaje");

            Beneficiarios beneficiarios = new Beneficiarios();

            beneficiarios.consecutivo = 0;
            beneficiarios.cod_poliza = Convert.ToInt64(lblcodpoliza.Text);
            beneficiarios.nombres = txtnewnombresyapellidos.Text;
            beneficiarios.identificacion = txtnewdocumentoidentidad.Text;
            beneficiarios.parentesco = txtnewparentesco.Text;
            beneficiarios.fecha_nacimiento = Convert.ToDateTime(txtnewfechanac.Text);
            beneficiarios.porcentaje = Convert.ToInt64(txtnewporcentaje.Text);


            gvBeneficiarios.EditIndex = -1;

            beneficiariosPolizasServicio.CrearBeneficiarios(beneficiarios, (Usuario)Session["usuario"]);


            this.ListarDatosBeneficiarios(beneficiarios.cod_poliza);
        }
    }

    protected void gvFamiliares_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtnewnombresyapellidosf = (TextBox)gvFamiliares.FooterRow.FindControl("txtnewnombresyapellidosf");
            TextBox txtnewdocumentoidentidadf = (TextBox)gvFamiliares.FooterRow.FindControl("txtnewdocumentoidentidadf");
            TextBox txtnewsexof = (TextBox)gvFamiliares.FooterRow.FindControl("txtnewsexof");
            TextBox txtnewparentescof = (TextBox)gvFamiliares.FooterRow.FindControl("txtnewparentescof");
            TextBox txtnewfechanacf = (TextBox)gvFamiliares.FooterRow.FindControl("txtnewfechanacf");
            TextBox txtnewactividadf = (TextBox)gvFamiliares.FooterRow.FindControl("txtnewactividadf");

            Xpinn.FabricaCreditos.Entities.FamiliaresPolizas familiares = new Xpinn.FabricaCreditos.Entities.FamiliaresPolizas();

            familiares.consecutivo = 0;
            familiares.cod_poliza = Convert.ToInt64(lblcodpoliza.Text);
            familiares.nombres = txtnewnombresyapellidosf.Text;
            familiares.identificacion = txtnewdocumentoidentidadf.Text;
            familiares.sexo = txtnewsexof.Text;
            familiares.parentesco = txtnewparentescof.Text;
            familiares.fecha_nacimiento = Convert.ToDateTime(txtnewfechanacf.Text);
            familiares.actividad = txtnewactividadf.Text;


            gvFamiliares.EditIndex = -1;

            familiaresPolizasServicio.CrearFamiliares(familiares, (Usuario)Session["usuario"]);


            this.ListarDatosFamiliares(familiares.cod_poliza);
        }
    }

    protected void btnInforme_Click(object sender, EventArgs e)
    {

    }
}