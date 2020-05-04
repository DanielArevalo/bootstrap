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
    CargosService CargosService = new CargosService();
    EmpresaService _empresaService = new EmpresaService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[CargosService.CodigoPrograma + ".id"] == null)
            {
                VisualizarOpciones(CargosService.CodigoPrograma, "A");
            }
            else
            {
                VisualizarOpciones(CargosService.CodigoPrograma, "D");
            }

            Site toolBar = (Site)Master;
            toolBar.eventoGuardar += btn_Guardar_click;
          
            toolBar.eventoCancelar += (s, evt) => Navegar("Lista.aspx");
            toolBar.eventoRegresar += (s, evt) =>
            {
                Session.Remove(CargosService.CodigoPrograma + ".id");
                Navegar("Lista.aspx");
            };
            toolBar.MostrarRegresar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CargosService.CodigoPrograma, "Page_PreInit", ex);
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session[CargosService.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[CargosService.CodigoPrograma + ".id"].ToString();
                ObtenerDatos(idObjeto);
            }
            else
            {
                txtIdCargo.Text = CargosService.ConsultaMax((Usuario)Session["usuario"]).ToString();
            }
              

        }
    }
    void btn_Guardar_click(object sender, ImageClickEventArgs e)
    {
        Cargos pEntitie = new Cargos();

        Empresa Empresa= ConsultarEmpresa();

        pEntitie.IdCargo= Convert.ToInt64(txtIdCargo.Text);
        pEntitie.Nombre = txtNombre.Text;

        if (idObjeto == "")
        {
            Cargos Entitie  = CargosService.CrearCargo(pEntitie, Usuario);
            pEntitie.IdCargo= Entitie.IdCargo;  
        }
        else
        {
            pEntitie = CargosService.ModificarCargo(pEntitie, Usuario);
        }

        if (pEntitie.IdCargo != 0)
        {
            Site toolBar = (Site)Master;
            toolBar.MostrarRegresar(true);
            toolBar.MostrarCancelar(false);
            toolBar.MostrarGuardar(false);
            mvComprobante.ActiveViewIndex = 1;
            Session.Remove(CargosService.CodigoPrograma + ".id");
        }


    }
    bool ValidarCampos()
    {
        Page.Validate();
        if (!Page.IsValid ||
            string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            VerError("El Nombre del cargo esta vacio");
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
            VerError("Error al consultar , " + ex.Message);
            return null;
        }
    }
    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Cargos Entidad = CargosService.ListarCargos(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            txtIdCargo.Text = Entidad.IdCargo.ToString();
            txtNombre.Text = Entidad.Nombre;


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CargosService.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
}