using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Seguridad.Entities;
using Xpinn.Seguridad.Services;
using Xpinn.Util;

public partial class Page_Nomina_Area_Nuevo : GlobalWeb
{
    AreaService AreaServise = new AreaService();
    EmpresaService _empresaService = new EmpresaService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[AreaServise.CodigoPrograma + ".id"] == null)
            {
                VisualizarOpciones(AreaServise.CodigoPrograma, "A");
            }
            else
            {
                VisualizarOpciones(AreaServise.CodigoPrograma, "D");
            }

            Site toolBar = (Site)Master;
            toolBar.eventoGuardar += btn_Guardar_click;
          
            toolBar.eventoCancelar += (s, evt) => Navegar("Lista.aspx");
            toolBar.eventoRegresar += (s, evt) =>
            {
                Session.Remove(AreaServise.CodigoPrograma + ".id");
                Navegar("Lista.aspx");
            };
            toolBar.MostrarRegresar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AreaServise.CodigoPrograma, "Page_PreInit", ex);
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session[AreaServise.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[AreaServise.CodigoPrograma + ".id"].ToString();
                ObtenerDatos(idObjeto);
            }
            else
            {
                txtIdArea.Text = AreaServise.ConsultaMax((Usuario)Session["usuario"]).ToString();
            }
              

        }
    }
    void btn_Guardar_click(object sender, ImageClickEventArgs e)
    {
        Area pEntitie = new Area();

        Empresa Empresa= ConsultarEmpresa();

        pEntitie.IdArea = Convert.ToInt64(txtIdArea.Text);
        pEntitie.Nombre = txtNombre.Text;
        pEntitie.CodEmpresa = Empresa.cod_empresa;

        if (idObjeto == "")
        {
          Area Entitie  = AreaServise.CrearAreaEntities(pEntitie, Usuario);
            pEntitie.IdArea = Entitie.IdArea;  
        }
        else
        {
            pEntitie = AreaServise.ModificarAreaEntities(pEntitie, Usuario);
        }

        if (pEntitie.IdArea != 0)
        {
            Site toolBar = (Site)Master;
            toolBar.MostrarRegresar(true);
            toolBar.MostrarCancelar(false);
            toolBar.MostrarGuardar(false);
            mvComprobante.ActiveViewIndex = 1;
            Session.Remove(AreaServise.CodigoPrograma + ".id");
        }


    }
    bool ValidarCampos()
    {
        Page.Validate();
        if (!Page.IsValid ||
            string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            VerError("El Nombre del area esta vacio");
        }
        return true;
    }

    Empresa ConsultarEmpresa()
    {
        try
        {
            Empresa empresa = _empresaService.ConsultarEmpresa(Usuario);
            return empresa;
        }
        catch (Exception ex)
        {
            VerError("Error al consultar la nomina, " + ex.Message);
            return null;
        }
    }
    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Area Entidad = AreaServise.ListarArea(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            txtIdArea.Text = Entidad.IdArea.ToString();
            txtNombre.Text = Entidad.Nombre;


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AreaServise.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
}