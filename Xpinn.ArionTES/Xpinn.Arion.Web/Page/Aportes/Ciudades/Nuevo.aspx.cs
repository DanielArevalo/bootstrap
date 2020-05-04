using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class Nuevo : GlobalWeb
{
    CiudadServices CiudadService = new CiudadServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[CiudadService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(CiudadService.CodigoPrograma, "E");
            else
                VisualizarOpciones(CiudadService.CodigoPrograma, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CiudadService.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvAplicar.ActiveViewIndex = 0;
                cargarDropdown();
                
                if (Session[CiudadService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[CiudadService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(CiudadService.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    txtCodigo.Enabled = false;
                }
                else
                {
                    txtCodigo.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CiudadService.GetType().Name + "L", "Page_Load", ex);
        }
    }


    void cargarDropdown()
    {
        ddlTipo.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlTipo.Items.Insert(1, new ListItem("País", "1"));
        ddlTipo.Items.Insert(2, new ListItem("Departamento/Estado", "2"));
        ddlTipo.Items.Insert(3, new ListItem("Ciudad", "3"));
        ddlTipo.Items.Insert(4, new ListItem("Municipio", "4"));
        ddlTipo.Items.Insert(5, new ListItem("Zona", "5"));
        ddlTipo.Items.Insert(6, new ListItem("Barrio", "6"));
        ddlTipo.SelectedIndex = 0;
        ddlTipo.DataBind();

        PoblarLista("CIUDADES", ddlDepende);
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


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Ciudad vDatos = new Ciudad();

            vDatos = CiudadService.ConsultarCiudad(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vDatos.codciudad != 0)
                txtCodigo.Text = HttpUtility.HtmlDecode(vDatos.codciudad.ToString().Trim());
            if (vDatos.nomciudad != "" && vDatos.nomciudad != null)
                txtNombre.Text = vDatos.nomciudad;
            if (vDatos.tipo != 0)
                ddlTipo.SelectedValue = vDatos.tipo.ToString();
            if (vDatos.depende_de != null)
                ddlDepende.SelectedValue = vDatos.depende_de.ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CiudadService.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void btnFinal_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    public Boolean ValidarDatos()
    {
        if (txtCodigo.Text == "")
        {
            VerError("Ingrese el Codigo correspondiente");
            return false;
        }

        if (txtNombre.Text == "")
        {
            VerError("Debe Ingresar el Nombre");
            return false;
        }
        
        if (ddlTipo.SelectedIndex == 0)
        {
            VerError("Seleccione el Tipo");
            return false;
        }

        if (ddlDepende.SelectedIndex == 0 && ddlTipo.SelectedValue != "1")
        {
            VerError("Seleccione a quien dependerá el registro");
            return false;
        }      
        
         return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            string msj = idObjeto != "" ? "Modificar" : "Grabar";
            ctlMensaje.MostrarMensaje("Desea " + msj + " los datos ingresados?");
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario User = (Usuario)Session["usuario"];
            
            Ciudad vData = new Ciudad();
            vData.codciudad = Convert.ToInt64(txtCodigo.Text);
            vData.nomciudad = txtNombre.Text.ToUpper().Trim();
            vData.tipo = Convert.ToInt32(ddlTipo.SelectedValue);
            vData.depende_de = ddlDepende.SelectedValue != "" ? Convert.ToInt64(ddlDepende.SelectedValue) : 0;

            if (idObjeto != "")
            {
                //MODIFICAR
                CiudadService.Crear_Mod_Ciudad(vData, (Usuario)Session["usuario"], 2);
            }
            else
            {
                Ciudad vDatos = new Ciudad();
                vDatos = CiudadService.ConsultarCiudad(Convert.ToInt64(txtCodigo.Text), (Usuario)Session["usuario"]);

                if (vDatos.codciudad != 0 && vDatos.nomciudad != null)
                {
                    VerError("El codigo ingresado ya existe");
                    return;
                }
                else
                {//CREAR
                    CiudadService.Crear_Mod_Ciudad(vData, (Usuario)Session["usuario"], 1);
                }
            }
            string msj = idObjeto != "" ? "Modificado" : "Grabado";
            mvAplicar.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CiudadService.CodigoPrograma, "btnContinuar_Click", ex);
        }
    }    

    protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTipo.SelectedValue == "1")
            ddlDepende.Enabled = false;
    }
}
