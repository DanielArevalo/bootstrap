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
    IdentificacionServices identificacionServicio = new IdentificacionServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[identificacionServicio.CodigoProgramaC + ".id"] != null)
                VisualizarOpciones(identificacionServicio.CodigoProgramaC, "E");
            else
                VisualizarOpciones(identificacionServicio.CodigoProgramaC, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaC, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[identificacionServicio.CodigoProgramaC + ".id"] != null)
                {
                    idObjeto = Session[identificacionServicio.CodigoProgramaC + ".id"].ToString();
                    Session.Remove(identificacionServicio.CodigoProgramaC + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaC, "Page_Load", ex);
        }
    }
    
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDescripcionCargo.Text.Trim() != "")
            {
                Identificacion pCargo = new Identificacion();
                pCargo.descripcion = txtDescripcionCargo.Text;

                if (idObjeto == "")
                    pCargo = identificacionServicio.CrearCargo(pCargo, (Usuario)Session["usuario"]);
                else
                {
                    pCargo.cod_cargo = Convert.ToInt64(idObjeto);
                    pCargo = identificacionServicio.ModificarCargo(pCargo, (Usuario)Session["usuario"]);
                }
                Navegar(Pagina.Lista);
            }
            else
                VerError("Ingrese la descripción");
        }catch(Exception ex)
        {
            VerError("Error al guardar los datos "+ex.Message);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session.Remove(identificacionServicio.CodigoProgramaC + ".id");
        Navegar(Pagina.Lista);
    }

    private void ObtenerDatos(string idObjeto)
    {
        Identificacion pCargo = new Identificacion();
        pCargo.cod_cargo = Convert.ToInt64(idObjeto);

        pCargo = identificacionServicio.ConsultarCargosEntidad(pCargo, (Usuario)Session["usuario"]);

        if (pCargo != null)
        {
            if(!string.IsNullOrWhiteSpace(pCargo.cod_cargo.ToString()))
                txtCodCargo.Text = pCargo.cod_cargo.ToString();
            if (!string.IsNullOrWhiteSpace(pCargo.descripcion.ToString()))
                txtDescripcionCargo.Text = pCargo.descripcion.ToString();
        }
    }
}