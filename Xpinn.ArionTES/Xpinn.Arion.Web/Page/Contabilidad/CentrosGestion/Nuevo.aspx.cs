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
using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;


public partial class Nuevo : GlobalWeb
{

    CentroGestionService CentroGestService = new CentroGestionService();
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[CentroGestService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(CentroGestService.CodigoPrograma, "E");
            else
                VisualizarOpciones(CentroGestService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CentroGestService.GetType().Name + "L", "Page_PreInit", ex);
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
                txtFecha.Text = DateTime.Now.ToShortDateString();

                if (Session[CentroGestService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[CentroGestService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(CentroGestService.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }              
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CentroGestService.CodigoPrograma + "L", "Page_Load", ex);
        }
    }
    

    void CargarDropDown()
    {
        PoblarLista("CENTRO_GESTION", ddlDepende);
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


    protected void ObtenerDatos(string pidObjeto)
    {
        try
        {
            CentroGestion vCentroGestion = new CentroGestion();
            vCentroGestion = CentroGestService.ConsultarCentroGestion(Convert.ToInt64(pidObjeto), (Usuario)Session["usuario"]);

            if (vCentroGestion.centro_gestion != 0 && vCentroGestion.centro_gestion != null)
                txtCodigo.Text = vCentroGestion.centro_gestion.ToString();

            if (vCentroGestion.nombre != "" && vCentroGestion.nombre != null)
                txtNombre.Text = vCentroGestion.nombre.ToString().Trim();

            if (vCentroGestion.nivel != 0 && vCentroGestion.nivel != null)
                txtNivel.Text = vCentroGestion.nivel.ToString().Trim();

            if (vCentroGestion.depende_de != 0 && vCentroGestion.depende_de != null)
                ddlDepende.SelectedValue = vCentroGestion.depende_de.ToString();

            if (vCentroGestion.fechacreacion != DateTime.MinValue && vCentroGestion.fechacreacion != null)
                txtFecha.Text = vCentroGestion.fechacreacion.ToShortDateString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CentroGestService.CodigoPrograma, "ObtenerDatos", ex);
        }
    }



    public Boolean ValidarDatos()
    {
        if (txtNombre.Text == "")
        {
            VerError("Ingrese el Nombre");
            return false;
        }
        if (txtNivel.Text == "")
        {
            VerError("Ingrese el Nivel");
            return false;
        }
        if (txtFecha.Text == "")
        {
            VerError("Ingrese la fecha de creación");
            return false;
        }        
        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {            
            string msj = idObjeto != "" ? "modificar" : "registrar";
             ctlMensaje.MostrarMensaje("Desea " + msj + " los Datos Ingresados?");
        }
    }
    

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            CentroGestion vDatos = new CentroGestion();
            
            if (idObjeto != "")
                vDatos.centro_gestion = Convert.ToInt32(txtCodigo.Text);
            else
                vDatos.centro_gestion = 0;

            Usuario pUsu = (Usuario)Session["usuario"];

            vDatos.nombre = txtNombre.Text;
            vDatos.nivel = Convert.ToInt32(txtNivel.Text);
            vDatos.fechacreacion = Convert.ToDateTime(txtFecha.Text);
            if(ddlDepende.SelectedIndex != 0)
                vDatos.depende_de = Convert.ToInt32(ddlDepende.Text);
            else
                vDatos.depende_de = 0;

            if (idObjeto != "")
            {
                //MODIFICAR
                CentroGestService.Crear_Mod_CentroGestion(vDatos, (Usuario)Session["usuario"],2);
            }
            else
            {
                //CREAR
                CentroGestService.Crear_Mod_CentroGestion(vDatos, (Usuario)Session["usuario"],1);
            }

            lblMsj.Text = idObjeto != "" ? "Se Modificaron correctamente los datos" : "Se Grabaron Correctamente los datos ingresados.";
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            mvAplicar.ActiveViewIndex = 1;            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CentroGestService.CodigoPrograma, "btnContinuar_Click", ex);
        }        
    }


    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    
}
