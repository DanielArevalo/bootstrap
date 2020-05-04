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
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;

public partial class Lista : GlobalWeb
{
    TipoGarantiaService tiposgarantiasservicio = new TipoGarantiaService();
    LineasCreditoService lineascreditoServicio = new LineasCreditoService();
    //  CuentasGarantiaService cuentasgarantiasServicio = new CuentasGarantiaService();
    PlanCuentasService plancuentasServicio = new PlanCuentasService();
    GarantiasRealesService garantiasservicio = new GarantiasRealesService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {           
            VisualizarOpciones(garantiasservicio.CodigoPrograma.ToString(), "L");
            Site1 toolBar = (Site1)this.Master;
           // toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
          //  toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoAdelante += btnAdelante_Click;
           // toolBar.eventoAtras += btnAtras_Click;

            if (Convert.ToString(Session["Nombres"]) != "")
                ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
            if (Convert.ToString(Session["Identificacion"]) != "")
                ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();

            ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;
            btnAdelante.ImageUrl = "~/Images/btnPolizas.jpg";
            btnAdelante.ValidationGroup = "";


        }
        catch (Exception ex)
        {

            BOexcepcion.Throw(garantiasservicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                MostrarDatosGarantiasReales();
            }
            if (Session[garantiasservicio.consecutivo + ".id"] != null)
            {
                idObjeto = Session[garantiasservicio.consecutivo + ".id"].ToString();

                Session.Remove(garantiasservicio.consecutivo + ".id");



            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(garantiasservicio.GetType().Name + "A", "Page_Load", ex);
        }

    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {

        Page.Validate();

        if (Page.IsValid)
        {
           
            GuardarValoresConsulta(pConsulta, garantiasservicio.CodigoPrograma);
            Actualizar();
            
           
        }

    }
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {

        LimpiarValoresConsulta(pConsulta, garantiasservicio.CodigoPrograma);
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, garantiasservicio.GetType().Name);
        Navegar(Pagina.Nuevo);

    }

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }
    private void MostrarDatosGarantias(String consecutivo, GridView gvGarantiasReales, String Var)
    {

        GarantiasReales pGarantiasReales = new GarantiasReales();
        List<GarantiasReales> LstGarantiasReales = new List<GarantiasReales>();

        pGarantiasReales.consecutivo = Convert.ToInt64("0" + consecutivo);

        LstGarantiasReales = garantiasservicio.ListarGarantiasReales(pGarantiasReales, (Usuario)Session["usuario"]);

        if (LstGarantiasReales.Count == 0 )
        {
            //this.creargarantiasinicial(0, "GarantiasReales");
            LstGarantiasReales = (List<GarantiasReales>)Session["GarantiasReales"];

        }

        gvGarantiasReales.DataSource = LstGarantiasReales;
        gvGarantiasReales.DataBind();
        Session[Var] = LstGarantiasReales;
    }

    public void Actualizar()
    {
        try
        {
            List<GarantiasReales> lstConsulta = new List<GarantiasReales>();
            // List<CuentasContaGarantias> lstConsulta = new List<CuentasContaGarantias>();
            lstConsulta = garantiasservicio.ListarGarantiasReales(ObtenerValores(), (Usuario)Session["usuario"]);

            gvGarantiasReales.PageSize = pageSize;
            gvGarantiasReales.EmptyDataText = emptyQuery;
            gvGarantiasReales.DataSource = lstConsulta;


            if (lstConsulta.Count > 0)
            {
                gvGarantiasReales.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvGarantiasReales.DataBind();
                ValidarPermisosGrilla(gvGarantiasReales);
            }
            else
            {
                gvGarantiasReales.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;


            }

            Session.Add(garantiasservicio.CodigoPrograma + ".consulta", 1);
      //      this.creargarantiasinicial(0, "GarantiasReales");
        }
        catch (Exception ex)
        {

            BOexcepcion.Throw(garantiasservicio.CodigoPrograma, "Actualizar", ex);
        }
    }
    private GarantiasReales ObtenerValores()
    {
        GarantiasReales garantia = new GarantiasReales();
        //if (this.txtCodigoGarantia.Text.Trim() != "")
        // {
        //     garantia.consecutivo= Convert.ToInt64("0" + txtCodigoGarantia.Text.Trim());
        //  }
        return garantia;
    }

    protected void LlenarComboPlancuentasd(DropDownList ddlnewctadebito)
    {

        PlanCuentas cuenta = new PlanCuentas();
        List<PlanCuentas> LstPlanCuentas = new List<PlanCuentas>();
        LstPlanCuentas = plancuentasServicio.ListarPlanCuentas(cuenta, (Usuario)Session["usuario"]);
        cuenta.Codigo = 0;
        cuenta.Nombre = "";
        LstPlanCuentas.Add(cuenta);
        ddlnewctadebito.DataSource = LstPlanCuentas;
        ddlnewctadebito.DataTextField = "Nombre";
        ddlnewctadebito.DataValueField = "codigo";
        ddlnewctadebito.DataBind();
        ddlnewctadebito.SelectedValue = "0";
    }

    protected void LlenarComboPlancuentasc(DropDownList ddlnewctacredito)
    {

        PlanCuentas cuenta = new PlanCuentas();
        List<PlanCuentas> LstPlanCuentas = new List<PlanCuentas>();
        LstPlanCuentas = plancuentasServicio.ListarPlanCuentas(cuenta, (Usuario)Session["usuario"]);
        cuenta.Codigo = 0;
        cuenta.Nombre = "";
        LstPlanCuentas.Add(cuenta);
        ddlnewctacredito.DataSource = LstPlanCuentas;
        ddlnewctacredito.DataTextField = "Nombre";
        ddlnewctacredito.DataValueField = "codigo";
        ddlnewctacredito.DataBind();
        ddlnewctacredito.SelectedValue = "0";
    }



    protected void LlenarComboTipoGarantia(DropDownList ddlnewtipogarantia)
    {

        TipoGarantias garantia = new TipoGarantias();
        List<TipoGarantias> LstTipoGarantias = new List<TipoGarantias>();
        LstTipoGarantias = tiposgarantiasservicio.ListarTiposGarantia(garantia, (Usuario)Session["usuario"]);
        garantia.Codigo = 0;
        garantia.Nombre = "";
        LstTipoGarantias.Add(garantia);
        ddlnewtipogarantia.DataSource = LstTipoGarantias;
        ddlnewtipogarantia.DataTextField = "Nombre";
        ddlnewtipogarantia.DataValueField = "codigo";
        ddlnewtipogarantia.DataBind();
        ddlnewtipogarantia.SelectedValue = "0";
    }

    protected void LlenarComboLinea(DropDownList ddlnewlineacredito)
    {

        LineasCredito linea = new LineasCredito();
        List<LineasCredito> LstLineaCredito = new List<LineasCredito>();
        LstLineaCredito = lineascreditoServicio.ListarLineasCredito(linea, (Usuario)Session["usuario"]);
        linea.Codigo = "0";
        linea.nombre = "";
        LstLineaCredito.Add(linea);
        ddlnewlineacredito.DataSource = LstLineaCredito;
        ddlnewlineacredito.DataTextField = "Nombre";
        ddlnewlineacredito.DataValueField = "codigo";
        ddlnewlineacredito.DataBind();
        ddlnewlineacredito.SelectedValue = "0";
    }

    protected List<LineasCredito> ListaLineaCredito()
    {

        LineasCredito linea = new LineasCredito();
        List<LineasCredito> LstLineaCredito = new List<LineasCredito>();
        LstLineaCredito = lineascreditoServicio.ListarLineasCredito(linea, (Usuario)Session["usuario"]);
        linea.Codigo = "0";
        linea.nombre = "";
        LstLineaCredito.Add(linea);
        return LstLineaCredito;

    }
    protected List<TipoGarantias> ListaTipoGarantias()
    {

        TipoGarantias tipo_gara = new TipoGarantias();
        List<TipoGarantias> LstTipoGarantias = new List<TipoGarantias>();
        LstTipoGarantias = tiposgarantiasservicio.ListarTiposGarantia(tipo_gara, (Usuario)Session["usuario"]);
        tipo_gara.Codigo = 0;
        tipo_gara.Nombre = "";
        LstTipoGarantias.Add(tipo_gara);
        return LstTipoGarantias;

    }
    protected List<PlanCuentas> ListaCuentasDebito()
    {

        PlanCuentas cuenta = new PlanCuentas();
        List<PlanCuentas> LstPlanCuentas = new List<PlanCuentas>();
        LstPlanCuentas = plancuentasServicio.ListarPlanCuentas(cuenta, (Usuario)Session["usuario"]);
        cuenta.Codigo = 0;
        cuenta.Nombre = "";
        LstPlanCuentas.Add(cuenta);
        return LstPlanCuentas;

    }
    protected List<PlanCuentas> ListaCuentasCredito()
    {

        PlanCuentas cuenta = new PlanCuentas();
        List<PlanCuentas> LstPlanCuentas = new List<PlanCuentas>();
        LstPlanCuentas = plancuentasServicio.ListarPlanCuentas(cuenta, (Usuario)Session["usuario"]);
        cuenta.Codigo = 0;
        cuenta.Nombre = "";
        LstPlanCuentas.Add(cuenta);
        return LstPlanCuentas;

    }



    private void MostrarDatosLineaCredito()
    {
        List<LineasCredito> LstLineaCredito = new List<LineasCredito>();

        LstLineaCredito = (List<LineasCredito>)Session["LineaCredito"];
        if (LstLineaCredito == null)
        {
           // this.creargarantiasinicial(0, "GarantiasReales");
            LstLineaCredito = (List<LineasCredito>)Session["LineaCredito"];
        }
        gvGarantiasReales.DataSource = LstLineaCredito;
        gvGarantiasReales.DataSource = LstLineaCredito;
        gvGarantiasReales.DataBind();

    }

    private void MostrarDatosTipoGarantias()
    {

        List<TipoGarantias> LstTipoGarantia = new List<TipoGarantias>();


        LstTipoGarantia = (List<TipoGarantias>)Session["TipoGarantia"];
        if (LstTipoGarantia == null)
        {
           // this.creargarantiasinicial(0, "GarantiasReales");
            LstTipoGarantia = (List<TipoGarantias>)Session["TipoGarantia"];
        }
        gvGarantiasReales.DataSource = LstTipoGarantia;
        gvGarantiasReales.DataSource = LstTipoGarantia;
        gvGarantiasReales.DataBind();

    }

    private void MostrarDatosCtasDebito()
    {

        List<PlanCuentas> LstCuentasDebito = new List<PlanCuentas>();


        LstCuentasDebito = (List<PlanCuentas>)Session["PlanCuentas"];
        if (LstCuentasDebito == null)
        {
           // this.creargarantiasinicial(0, "GarantiasReales");
            LstCuentasDebito = (List<PlanCuentas>)Session["PlanCuentas"];
        }
        gvGarantiasReales.DataSource = LstCuentasDebito;
        gvGarantiasReales.DataSource = LstCuentasDebito;
        gvGarantiasReales.DataBind();

    }
    private void MostrarDatosCtasCredito()
    {

        List<PlanCuentas> LstCuentasCredito = new List<PlanCuentas>();


        LstCuentasCredito = (List<PlanCuentas>)Session["PlanCuentas"];
        if (LstCuentasCredito == null)
        {
            //this.creargarantiasinicial(0, "GarantiasReales");
            LstCuentasCredito = (List<PlanCuentas>)Session["PlanCuentas"];
        }
        gvGarantiasReales.DataSource = LstCuentasCredito;
        gvGarantiasReales.DataSource = LstCuentasCredito;
        gvGarantiasReales.DataBind();

    }

    private void MostrarDatosGarantiasReales()
    {

        List<GarantiasReales> LstGarantiasReales = new List<GarantiasReales>();


        LstGarantiasReales = (List<GarantiasReales>)Session["GarantiasReales"];
        if ((LstGarantiasReales == null) || (LstGarantiasReales.Count == 0))
        {
            this.creargarantiasinicial(0, "GarantiasReales");
            LstGarantiasReales = (List<GarantiasReales>)Session["GarantiasReales"];
        }

        gvGarantiasReales.DataSource = LstGarantiasReales;
        gvGarantiasReales.DataBind();
        quitarfilainicialGarantiasReales();
    }

    private void quitarfilainicialGarantiasReales()
    {


        try
        {
            int conseID = Convert.ToInt32(gvGarantiasReales.DataKeys[0].Values[0].ToString());
            if (conseID <= 0)
            {
                ImageButton link = (ImageButton)this.gvGarantiasReales.Rows[0].Cells[0].FindControl("btnEditar");

                link.Enabled = false;

                link.Visible = false;

                this.gvGarantiasReales.Rows[0].Cells[1].Visible = false;
                this.gvGarantiasReales.Rows[0].Cells[2].Visible = false;
                this.gvGarantiasReales.Rows[0].Cells[3].Visible = false;
                this.gvGarantiasReales.Rows[0].Cells[4].Visible = false;
           

            }
        }
        catch 
        {
        }

    }
    private void creargarantiasinicial(int consecutivo, String nombresession)
    {
        GarantiasReales pGarantiasReales = new GarantiasReales();
        List<GarantiasReales> LstGarantiasReales = new List<GarantiasReales>();


        pGarantiasReales.consecutivo = consecutivo;
        pGarantiasReales.Nombre = "";
        pGarantiasReales.TipoGarantia= "";
        pGarantiasReales.ctadebito = "";
        pGarantiasReales.ctacredito = "";


        LstGarantiasReales.Add(pGarantiasReales);

        Session[nombresession] = LstGarantiasReales;

    }

    protected void gvGarantiasReales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Control ctrl = e.Row.FindControl("ddlnewlineacredito");
            Control ctrl2 = e.Row.FindControl("ddlnewtipogarantia");
            Control ctrl3 = e.Row.FindControl("ddlnewctadebito");
            Control ctrl4 = e.Row.FindControl("ddlnewctacredito");

            if (ctrl != null)
            {
                DropDownList dd = ctrl as DropDownList;
                LlenarComboLinea(dd);
            }
            if (ctrl2 != null)
            {
                DropDownList dd2 = ctrl2 as DropDownList;
                LlenarComboTipoGarantia(dd2);
            }
            if (ctrl3 != null)
            {
                DropDownList dd3 = ctrl3 as DropDownList;
                LlenarComboPlancuentasd(dd3);
            }
            if (ctrl4 != null)
            {
                DropDownList dd4 = ctrl4 as DropDownList;
                LlenarComboPlancuentasc(dd4);
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow )
        {            
            List<LineasCredito> LstLineaCredito;
            DropDownList ddlineacreditoedit = (DropDownList)e.Row.Cells[1].FindControl("ddlineacreditoedit");
         
            if (ddlineacreditoedit != null)
            {
                LstLineaCredito = this.ListaLineaCredito();
                ddlineacreditoedit.DataSource = LstLineaCredito;
                ddlineacreditoedit.DataTextField = "Nombre";
                ddlineacreditoedit.DataValueField = "Nombre";
            }
       
            List<TipoGarantias> LstTipoGarantias;         
            DropDownList ddltipogarantiaedit = (DropDownList)e.Row.Cells[1].FindControl("ddltipogarantiaedit");
         
            if (ddltipogarantiaedit != null)
            {
                LstTipoGarantias = ListaTipoGarantias();
                ddltipogarantiaedit.DataSource = LstTipoGarantias;
                ddltipogarantiaedit.DataTextField = "Nombre";
                ddltipogarantiaedit.DataValueField = "Nombre";
            }

            List<PlanCuentas> LstPlanCuentasdeb;
            DropDownList ddlctadebitoedit = (DropDownList)e.Row.FindControl("ddlctadebitoedit");

            if (ddlctadebitoedit != null)
            {
                LstPlanCuentasdeb = ListaCuentasDebito();
                ddlctadebitoedit.DataSource = LstPlanCuentasdeb;
                ddlctadebitoedit.DataTextField = "Nombre";
                ddlctadebitoedit.DataValueField = "Nombre";
            }
                      
            List<PlanCuentas> LstPlanCuentas;
            DropDownList ddlctacreditoedit = (DropDownList)e.Row.FindControl("ddlctacreditoedit");

            if (ddlctacreditoedit != null)
            {
                LstPlanCuentas = ListaCuentasCredito();
                ddlctacreditoedit.DataSource = LstPlanCuentas;
                ddlctacreditoedit.DataTextField = "Nombre";
                ddlctacreditoedit.DataValueField = "Nombre";
            }
        }        
        
    }

    protected void gvGarantiasReales_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {

            DropDownList ddlnewlineacredito = (DropDownList)gvGarantiasReales.FooterRow.FindControl("ddlnewlineacredito");
            DropDownList ddlnewtipogarantia = (DropDownList)gvGarantiasReales.FooterRow.FindControl("ddlnewtipogarantia");
            DropDownList ddlnewctadebito = (DropDownList)gvGarantiasReales.FooterRow.FindControl("ddlnewctadebito");
            DropDownList ddlnewctacredito = (DropDownList)gvGarantiasReales.FooterRow.FindControl("ddlnewctacredito");

            if (ddlnewlineacredito.SelectedItem.Text == "" || ddlnewtipogarantia.SelectedItem.Text == "" || ddlnewctadebito.SelectedItem.Text == "" || ddlnewctacredito.SelectedItem.Text == "")
            {
                String Error = "Por favor diligenciar los datos";
                this.Lblerror.Text = Error;
                quitarfilainicialGarantiasReales();
            }
            else
            {
                this.Lblerror.Text = "";
                GarantiasReales pgarantias = new GarantiasReales();
                pgarantias.consecutivo = 0;
                pgarantias.Nombre = ddlnewlineacredito.SelectedItem.Text;
                pgarantias.TipoGarantia = ddlnewtipogarantia.SelectedItem.Text;
                pgarantias.ctadebito = ddlnewctadebito.SelectedItem.Text;
                pgarantias.ctacredito = ddlnewctacredito.SelectedItem.Text;

                gvGarantiasReales.EditIndex = -1;
                garantiasservicio.CrearGarantiasReales(pgarantias, (Usuario)Session["usuario"]);
                Actualizar();
            }
           
        }
    }



    private void ConsultarGarantias()
    {

        MostrarDatosGarantias("consecutivo", gvGarantiasReales, "GarantiasReales");

    }
    protected void gvGarantiasReales_RowEditing(object sender, GridViewEditEventArgs e)
    {

        int conseID = Convert.ToInt32(gvGarantiasReales.DataKeys[e.NewEditIndex].Values[0].ToString());

        if (conseID != 0)
        {

            gvGarantiasReales.EditIndex = e.NewEditIndex;
            String consecutivo = conseID.ToString();
          


            String garantia = "";
            garantia = this.buscarTipoGarantia(conseID);
            DropDownList ddltipogarantiaedit = new DropDownList();

            if (ddltipogarantiaedit != null)
            {

                ddltipogarantiaedit.SelectedValue = garantia;
                //ddltipogarantiaedit = gvGarantiasReales.Rows[e.NewEditIndex].Cells[1].FindControl("ddltipogarantiaedit") as DropDownList;

                //MostrarDatosTipoGarantias();
                //MostrarDatosGarantias(consecutivo, gvGarantiasReales, "GarantiasReales");
               

            }

            //  if (lbltipogarantia != null)
            //    {
            //    Hashtable ht = new Hashtable();
            //  ht.Add(1, lbltipogarantia.Text);
            //}


            String linea = "";
            linea = this.buscarLineaCredito(conseID);
            DropDownList ddlineacreditoedit = new DropDownList();
            //  ddlineacreditoedit = gvGarantiasReales.Rows[e.NewEditIndex].Cells[1].FindControl("ddlineacreditoedit") as DropDownList;
            if (ddlineacreditoedit != null)
            {

                ddlineacreditoedit.SelectedValue = linea;
               // MostrarDatosLineaCredito();
                //MostrarDatosGarantias(consecutivo, gvGarantiasReales, "GarantiasReales");
              

                // ddlineacreditoedit = gvGarantiasReales.Rows[e.NewEditIndex].Cells[1].FindControl("ddlineacreditoedit") as DropDownList;


            }
            String cuentadebito = "";
            cuentadebito = this.buscarCtasDebito(conseID);
            DropDownList ddlctadebitoedit = new DropDownList();
            // ddlctadebitoedit = gvGarantiasReales.Rows[e.NewEditIndex].Cells[1].FindControl("ddlctadebitoedit") as DropDownList;
            if (ddlctadebitoedit != null)
            {

                ddlctadebitoedit.SelectedValue = cuentadebito;
               // MostrarDatosGarantias(consecutivo, gvGarantiasReales, "GarantiasReales");

              //  MostrarDatosCtasDebito();
               
                
            }
            String cuentacredito = "";
            cuentacredito = this.buscarCtasCredito(conseID);
            DropDownList ddlctacreditoedit = new DropDownList();
            //  ddlctacreditoedit = gvGarantiasReales.Rows[e.NewEditIndex].Cells[1].FindControl("ddlctacreditoedit") as DropDownList;
            if (ddlctacreditoedit != null)
            {

                ddlctacreditoedit.SelectedValue = cuentacredito;
               // MostrarDatosCtasCredito();
                //MostrarDatosGarantias(consecutivo, gvGarantiasReales, "GarantiasReales");
               

            }
            Actualizar();
          
        }
    
    }
   
    private String buscarLineaCredito(Int64 idconse)
    {

        String linea = "";
        List<GarantiasReales> LstLineasCredito = new List<GarantiasReales>();
        LstLineasCredito = (List<GarantiasReales>)Session["GarantiasReales"];
        if (LstLineasCredito != null)
        {
            foreach (GarantiasReales lin in LstLineasCredito)
            {
                if (lin.consecutivo== idconse)
                {
                    linea = lin.Nombre;
                }
            }

        }
        return linea;

    }

    private String buscarCtasDebito(Int64 idconse)
    {

        String cuentadebito = "";
        List<GarantiasReales> LstPlanCuentas = new List<GarantiasReales>();
        LstPlanCuentas = (List<GarantiasReales>)Session["GarantiasReales"];
        if (LstPlanCuentas != null)
        {
            foreach (GarantiasReales ctadeb in LstPlanCuentas)
            {
                if (ctadeb.consecutivo == idconse)
                {
                    cuentadebito = ctadeb.ctadebito;
                }
            }

        }
        return cuentadebito;

    }

    private String buscarCtasCredito(Int64 idconse)
    {

        String cuentacredito = "";
        List<GarantiasReales> LstPlanCuentasCred = new List<GarantiasReales>();
        LstPlanCuentasCred = (List<GarantiasReales>)Session["GarantiasReales"];
        if (LstPlanCuentasCred != null)
        {
            foreach (GarantiasReales ctacred in LstPlanCuentasCred)
            {
                if (ctacred.consecutivo == idconse)
                {
                    cuentacredito = ctacred.ctacredito;
                }
            }

        }
        return cuentacredito;

    }
    private String buscarTipoGarantia(Int64 idconse)
    {

        String tipo_garantia = "";
        List<GarantiasReales> LstTipoGarantias = new List<GarantiasReales>();
        LstTipoGarantias = (List<GarantiasReales>)Session["GarantiasReales"];
        if (LstTipoGarantias != null)
        {
            foreach (GarantiasReales tipo_gara in LstTipoGarantias)
            {
                if (tipo_gara.consecutivo== idconse)
                {
                    tipo_garantia = tipo_gara.TipoGarantia;
                }
            }

        }
        return tipo_garantia;

    }



    protected void txtCodigoGarantia_TextChanged(object sender, EventArgs e)
    {

    }
    protected void gvGarantiasReales_SelectedIndexChanged(object sender, EventArgs e)
    {


    }

    private long buscarultimoconsecutivo(List<GarantiasReales> LstGarantiasReales)
    {
        long conseid = 0;

        if (LstGarantiasReales != null)
        {
            foreach (GarantiasReales garan in LstGarantiasReales)
            {
                if (garan.consecutivo > conseid)
                {
                    conseid = garan.consecutivo;
                }

            }
        }
        conseid++;
        return conseid;

    }

    protected void gvGarantiasReales_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }
    protected void gvGarantiasReales_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DropDownList ddltipogarantiaedit = (DropDownList)gvGarantiasReales.Rows[e.RowIndex].FindControl("ddltipogarantiaedit");
        DropDownList ddlctadebitoedit = (DropDownList)gvGarantiasReales.Rows[e.RowIndex].FindControl("ddlctadebitoedit");
        DropDownList ddlctacreditoedit = (DropDownList)gvGarantiasReales.Rows[e.RowIndex].FindControl("ddlctacreditoedit");
        DropDownList ddlineacreditoedit = (DropDownList)gvGarantiasReales.Rows[e.RowIndex].FindControl("ddlineacreditoedit");
        TextBox txtconsecutivo = (TextBox)gvGarantiasReales.Rows[e.RowIndex].FindControl("txtconsecutivo");
       
 long conseID = Convert.ToInt32(gvGarantiasReales.DataKeys[e.RowIndex].Values[0].ToString());
       

        GarantiasReales pgarantias = new GarantiasReales();
        pgarantias.consecutivo = conseID;
        pgarantias.Nombre = ddlineacreditoedit.SelectedValue.ToString();
        pgarantias.TipoGarantia = ddltipogarantiaedit.SelectedValue.ToString();
        pgarantias.ctadebito = ddlctadebitoedit.SelectedValue.ToString();
        pgarantias.ctacredito = ddlctacreditoedit.SelectedValue.ToString();

        gvGarantiasReales.EditIndex = -1;
        garantiasservicio.ModificarGarantiasReales(pgarantias, (Usuario)Session["usuario"]);
        Actualizar();


    }
    protected void ddltipogarantiaedit_Load(object sender, EventArgs e)
    {

        DropDownList ddlnewtipogarantia;
        ddlnewtipogarantia = (DropDownList)sender;
        this.LlenarComboTipoGarantia(ddlnewtipogarantia);
    }
    protected void gvGarantiasReales_DataBound(object sender, EventArgs e)
    {

    }
    protected void gvGarantiasReales_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvGarantiasReales.EditIndex = -1;
        long conseID = Convert.ToInt32(gvGarantiasReales.DataKeys[e.RowIndex].Values[0].ToString());

        String consecutivo = conseID.ToString();
    
         MostrarDatosGarantias(consecutivo, gvGarantiasReales, "GarantiasReales");
         Actualizar();

    }
   
    
    protected void gvGarantiasReales_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        long conseID = Convert.ToInt32(gvGarantiasReales.DataKeys[e.RowIndex].Values[0].ToString());
        if (conseID != 0)
        {
            AsignarEventoConfirmar();
           garantiasservicio.EliminarGarantiasReales(conseID, (Usuario)Session["usuario"]);
           Actualizar();
        }
  
      }
    protected void gvGarantiasReales_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/PolizasSeguros/Lista.aspx");
    }
    //protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    //{
    //    Response.Redirect("~/Page/FabricaCreditos/Solicitud/PlanPagos/Lista.aspx");
    //}
}
