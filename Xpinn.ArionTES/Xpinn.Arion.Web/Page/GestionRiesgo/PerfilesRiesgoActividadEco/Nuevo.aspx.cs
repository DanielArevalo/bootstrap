using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Services;

public partial class Nuevo : GlobalWeb
{
    ActividadEcoServices _ActividadEco = new ActividadEcoServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[_ActividadEco.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(_ActividadEco.CodigoPrograma, "E");
            else
                VisualizarOpciones(_ActividadEco.CodigoPrograma, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_ActividadEco.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarListas();
                if (Session[_ActividadEco.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[_ActividadEco.CodigoPrograma + ".id"].ToString();
                    Session.Remove(_ActividadEco.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_ActividadEco.CodigoPrograma, "Page_Load", ex);
        }
    }

    private void CargarListas()
    {
        PoblarListas poblar = new PoblarListas();
        poblar.PoblarListaDesplegable("ACTIVIDAD", "CODACTIVIDAD, DESCRIPCION", "", "", ddlActividad, (Usuario)Session["usuario"]);
        //    poblar.PoblarListaDesplegable("GR_SUBPROCESO_ENTIDAD", "COD_SUBPROCESO, DESCRIPCION", "", "1", ddlProcedimiento, (Usuario)Session["usuario"]);

        ddlGrupoact.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlGrupoact.Items.Insert(1, new ListItem("Hoteles y agencias de viaje", "551"));
        ddlGrupoact.Items.Insert(2, new ListItem("Profesionales y casas de cambio.", "661"));
        ddlGrupoact.Items.Insert(3, new ListItem("Empresas o personas que comercialicen productos controlados porla Dirección Nacional de Estupefacientes.", "120"));
        ddlGrupoact.Items.Insert(4, new ListItem("Casas de empeño.", ""));
        ddlGrupoact.Items.Insert(5, new ListItem("Casinos y negocios de apuestas.", "920"));
        ddlGrupoact.Items.Insert(6, new ListItem("Comercializadoras/arrendadoras de vehículos automotores, embarcacionesy aeronaves.", "291"));
        ddlGrupoact.Items.Insert(7, new ListItem("Comercializadoras bajo el esquema de ventas multinivel o piramidal.", ""));
        ddlGrupoact.Items.Insert(8, new ListItem("Comercializadoras de armas, explosivos o municiones.", "252"));
        ddlGrupoact.Items.Insert(9, new ListItem("Constructoras.", "411"));
        ddlGrupoact.Items.Insert(10, new ListItem("Comercializadoras o agencias de bienes raíces.", "466"));
        ddlGrupoact.Items.Insert(11, new ListItem("Estaciones de gasolina.", "061"));
        ddlGrupoact.Items.Insert(12, new ListItem("Comercializadoras de antigüedades, joyas, metales y piedras preciosas,monedas, objetos de arte y sellos postales.", "081"));
        ddlGrupoact.Items.Insert(13, new ListItem("Prestamistas.", "643"));
        ddlGrupoact.Items.Insert(14, new ListItem("Sector transportador.", "492"));
        ddlGrupoact.Items.Insert(15, new ListItem("Trasportadores de dinero o de valores.", ""));
        ddlGrupoact.Items.Insert(16, new ListItem("Empresas ubicadas en zonas francas.", "465"));
        ddlGrupoact.Items.Insert(17, new ListItem("Empresas dedicadas a la transferencia o envío de fondos o remesas.", "649"));
        ddlGrupoact.Items.Insert(18, new ListItem("Operadores cambiarios fronterizos.", ""));
        ddlGrupoact.DataBind();
    }

    private bool ValidarDatos()
    {

        if (ddlGrupoact.SelectedValue == null)
        {
            VerError("Seleccione un grupo de Actividad de riesgo");
            return false;
        }
        if (ddlActividad.SelectedValue == "0" || ddlActividad.SelectedValue == null)
        {
            VerError("Seleccione  una  actividad de Riesgo");
            return false;
        }
        if (ddlValor.SelectedValue == "0" || ddlValor.SelectedValue == null)
        {
            VerError("Ingrese una valoracion para  la actividad");
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidarDatos())
            {
                ActividadEco pActividad = new ActividadEco();
                pActividad.Cod_actividad = Convert.ToString(ddlActividad.SelectedValue.Trim());
                pActividad.descripcion = ddlActividad.SelectedItem.Text;
                pActividad.valoracion = Convert.ToString(ddlValor.SelectedValue.Trim());

                if (hdIdActividad.Value == "")
                    pActividad = _ActividadEco.CrearActividad(pActividad, (Usuario)Session["usuario"]);
                else
                {
                    pActividad.Cod_actividad = Convert.ToString(ddlActividad.SelectedValue.Trim());
                    pActividad = _ActividadEco.ModificarActividad(pActividad, (Usuario)Session["usuario"]);
                }
                Navegar(Pagina.Lista);
            }
        }
        catch (Exception ex)
        {
            VerError("Error al guardar los datos " + ex.Message);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session.Remove(_ActividadEco.CodigoPrograma + ".id");
        Navegar(Pagina.Lista);
    }

    private void ObtenerDatos(string idObjeto)
    {
        ActividadEco pActividad = new ActividadEco();
        pActividad.Cod_actividad = Convert.ToString(idObjeto);

        pActividad = _ActividadEco.ConsultarFactorRiesgo(pActividad, (Usuario)Session["usuario"]);

        if (pActividad != null)
        {
            if (!string.IsNullOrWhiteSpace(pActividad.Id_actividad.ToString()))
                hdIdActividad.Value = pActividad.Id_actividad.ToString();
            if (!string.IsNullOrWhiteSpace(pActividad.Cod_actividad.ToString()))
                ddlActividad.SelectedValue = pActividad.Cod_actividad.ToString();
            if (!string.IsNullOrWhiteSpace(pActividad.valoracion.ToString()))
                ddlValor.SelectedValue = pActividad.valoracion.ToString();
        }
    }

    protected void ddlGrupoact_SelectedIndexChanged(object sender, EventArgs e)
    {
        PoblarListas poblar = new PoblarListas();
        string Cod_activ =Convert.ToString(ddlGrupoact.SelectedValue);
        poblar.PoblarListaDesplegable("ACTIVIDAD", "CODACTIVIDAD, DESCRIPCION", "CODACTIVIDAD like '" + Cod_activ + "%'", "", ddlActividad, (Usuario)Session["usuario"]);
        
    }
}