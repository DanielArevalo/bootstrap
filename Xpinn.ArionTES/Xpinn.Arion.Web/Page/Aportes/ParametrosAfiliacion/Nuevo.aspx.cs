using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;


public partial class Nuevo : GlobalWeb
{

    ParametrosAfiliacionServices ParametroService = new ParametrosAfiliacionServices();
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[ParametroService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ParametroService.CodigoPrograma, "E");
            else
                VisualizarOpciones(ParametroService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;            
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParametroService.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvAplicar.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;
                CargarDropDown();

                //if (Session[ParametroService.CodigoPrograma + ".id"] != null)
                //{
                    //idObjeto = Session[ParametroService.CodigoPrograma + ".id"].ToString();
                    //Session.Remove(ParametroService.CodigoPrograma + ".id");
                    ObtenerDatos();
                //}              
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParametroService.CodigoPrograma + "L", "Page_Load", ex);
        }
    }
    


    void CargarDropDown()
    {
        ddlTipoCalculo.Items.Insert(0, new ListItem("Seleccione un item","0"));
        ddlTipoCalculo.Items.Insert(1, new ListItem("Valor Fijo", "1"));
        ddlTipoCalculo.Items.Insert(2, new ListItem("% SMLMV", "2"));
        ddlTipoCalculo.SelectedIndex = 0;
        ddlTipoCalculo.DataBind();

        PoblarLista("PERIODICIDAD", ddlPeriodicidad);
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


    protected void ObtenerDatos()
    {
        try
        {
            ParametrosAfiliacion vDetalle = new ParametrosAfiliacion();
            vDetalle = ParametroService.ConsultarParametrosAfiliacion(0, (Usuario)Session["usuario"]);

            if (vDetalle.idparametros != 0 && vDetalle.idparametros != null)
                txtCodigo.Text = vDetalle.idparametros.ToString();            

            if (vDetalle.tipo_calculo != 0 && vDetalle.tipo_calculo != null)
                ddlTipoCalculo.SelectedValue = vDetalle.tipo_calculo.ToString().Trim();

            if (vDetalle.valor != 0 && vDetalle.valor != null)
                txtValor.Text = vDetalle.valor.ToString();

            if (vDetalle.numero_cuotas != 0 && vDetalle.numero_cuotas != null)
                txtNumeroCuotas.Text = vDetalle.numero_cuotas.ToString();

            if (vDetalle.cod_periodicidad != 0 && vDetalle.cod_periodicidad != null)
                ddlPeriodicidad.SelectedValue = vDetalle.cod_periodicidad.ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParametroService.CodigoPrograma, "ObtenerDatos", ex);
        }
    }



    public Boolean ValidarDatos()
    {
        if (txtValor.Text == "")
        {
            VerError("Ingrese el valor");
            return false;
        }
        if (txtNumeroCuotas.Text == "")
        {
            VerError("Ingrese el numero de cuotas");
            return false;
        }
        if (ddlTipoCalculo.SelectedIndex == 0)
        {
            VerError("Seleccione el tipo de calculo");
            return false;
        }
        if (ddlPeriodicidad.SelectedIndex == 0)
        {
            VerError("Seleccione la Periodicidad");
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");

        if (ValidarDatos())
        {
            ParametrosAfiliacion vDetalle = new ParametrosAfiliacion();
            vDetalle = ParametroService.ConsultarParametrosAfiliacion(0, (Usuario)Session["usuario"]);
            string msj = vDetalle.idparametros != 0 ? "modificar" : "registrar";
             ctlMensaje.MostrarMensaje("Desea " + msj + " los Datos Ingresados?");
        }
    }



    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            ParametrosAfiliacion vDetalle = new ParametrosAfiliacion();
            vDetalle = ParametroService.ConsultarParametrosAfiliacion(0, (Usuario)Session["usuario"]);
            if (vDetalle.idparametros != 0)
                idObjeto = "1";

            ParametrosAfiliacion pVar = new ParametrosAfiliacion();

            if (idObjeto != "")
                pVar.idparametros = Convert.ToInt32(txtCodigo.Text);
            else
                pVar.idparametros = 0;
            Usuario pUsu = (Usuario)Session["usuario"];

            pVar.cod_empresa = Convert.ToInt32(pUsu.idEmpresa);
            pVar.valor = Convert.ToDecimal(txtValor.Text);
            pVar.numero_cuotas = Convert.ToInt32(txtNumeroCuotas.Text);
            pVar.tipo_calculo = Convert.ToInt32(ddlTipoCalculo.SelectedValue);
            pVar.cod_periodicidad = Convert.ToInt32(ddlPeriodicidad.SelectedValue);

            if (idObjeto != "")
            {
                //MODIFICAR
                ParametroService.CrearParametrosAfiliacion(pVar, (Usuario)Session["usuario"], 2);
            }
            else
            {
                //CREAR
                ParametroService.CrearParametrosAfiliacion(pVar, (Usuario)Session["usuario"], 1);
            }

            lblMsj.Text = idObjeto != "" ? "Se Modificaron correctamente los datos" : "Se Grabaron Correctamente los datos ingresados.";
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            mvAplicar.ActiveViewIndex = 1;            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParametroService.CodigoPrograma, "btnContinuar_Click", ex);
        }        
    }


}
