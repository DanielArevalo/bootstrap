using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using System.Collections.Generic;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Seguridad.Services.PerfilService PerfilServicio = new Xpinn.Seguridad.Services.PerfilService();
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();

    List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();

    String ListaSolicitada = null;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[PerfilServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(PerfilServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(PerfilServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PerfilServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarListas();
                if (Session[PerfilServicio.CodigoPrograma + ".id"] != null)
                {
                  
                    idObjeto = Session[PerfilServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(PerfilServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);

                }
                else { txtCodperfil.Text = PerfilServicio.ObtenerSiguienteCodigo((Usuario)Session["usuario"]).ToString(); }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PerfilServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Seguridad.Entities.Perfil vPerfil = new Xpinn.Seguridad.Entities.Perfil();

            if (idObjeto != "")
                vPerfil = PerfilServicio.ConsultarPerfil(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vPerfil.codperfil = Convert.ToInt64(txtCodperfil.Text.Trim());
            vPerfil.nombreperfil = Convert.ToString(txtNombreperfil.Text.Trim());
            //agregado
            vPerfil.cod_periodicidad = Convert.ToInt32(ddlPeriodicidad.SelectedValue);
            vPerfil.es_administrador = Convert.ToInt32(cbAdmin.Checked);
            //agregar restricciones clave
            vPerfil.caracter = Convert.ToBoolean(cbCaracter.Checked);
            vPerfil.longitud = Convert.ToInt32(txtLongitud.Text);
            vPerfil.numero = Convert.ToBoolean(cbNumero.Checked);
            vPerfil.mayuscula = Convert.ToBoolean(cbMayuscula.Checked);

            if (idObjeto != "")
            {
                vPerfil.codperfil = Convert.ToInt64(idObjeto);
                PerfilServicio.ModificarPerfil(vPerfil, (Usuario)Session["usuario"]);
            }
            else
            {
                vPerfil = PerfilServicio.CrearPerfil(vPerfil, (Usuario)Session["usuario"]);
                idObjeto = vPerfil.codperfil.ToString();
            }

            Session[PerfilServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PerfilServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Session[PerfilServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Seguridad.Entities.Perfil vPerfil = new Xpinn.Seguridad.Entities.Perfil();
            vPerfil = PerfilServicio.ConsultarPerfil(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vPerfil.codperfil != Int64.MinValue)
                txtCodperfil.Text = HttpUtility.HtmlDecode(vPerfil.codperfil.ToString().Trim());
            if (!string.IsNullOrEmpty(vPerfil.nombreperfil))
                txtNombreperfil.Text = HttpUtility.HtmlDecode(vPerfil.nombreperfil.ToString().Trim());
            if (vPerfil.cod_periodicidad != int.MinValue)
                ddlPeriodicidad.SelectedValue = HttpUtility.HtmlDecode(vPerfil.cod_periodicidad.ToString().Trim());
            if (vPerfil.es_administrador != int.MinValue)
                cbAdmin.Checked = vPerfil.es_administrador == 1 ? true : false;
            if (!string.IsNullOrEmpty(vPerfil.nombreperfil))
                txtLongitud.Text = HttpUtility.HtmlDecode(vPerfil.longitud.ToString().Trim());

            cbCaracter.Checked = vPerfil.caracter;
            cbNumero.Checked = vPerfil.numero;
            cbMayuscula.Checked = vPerfil.mayuscula;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PerfilServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }



    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
    }

    
    private void CargarListas()
    {
        PoblarLista("periodicidad", ddlPeriodicidad);      
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


}