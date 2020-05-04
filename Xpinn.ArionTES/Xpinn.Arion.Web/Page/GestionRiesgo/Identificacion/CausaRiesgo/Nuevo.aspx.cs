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
            if (Session[identificacionServicio.CodigoProgramaCa + ".id"] != null)
                VisualizarOpciones(identificacionServicio.CodigoProgramaCa, "E");
            else
                VisualizarOpciones(identificacionServicio.CodigoProgramaCa, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaCa, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarListas();
                if (Session[identificacionServicio.CodigoProgramaCa + ".id"] != null)
                {
                    idObjeto = Session[identificacionServicio.CodigoProgramaCa + ".id"].ToString();
                    Session.Remove(identificacionServicio.CodigoProgramaCa + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaCa, "Page_Load", ex);
        }
    }

    private void CargarListas()
    {
        PoblarListas poblar = new PoblarListas();
        poblar.PoblarListaDesplegable("GR_AREA_FUNCIONAL", "COD_AREA, DESCRIPCION", "", "1", ddlArea, (Usuario)Session["usuario"]);
        poblar.PoblarListaDesplegable("GR_CARGO_ORGANIGRAMA", "COD_CARGO, DESCRIPCION", "", "1", ddlPotencialResponsable, (Usuario)Session["usuario"]);
    }

    private bool ValidarDatos()
    {
        VerError("");
        if(txtDescripcionCausa.Text == "" || txtDescripcionCausa == null)
        {
            VerError("Ingrese la descripción de la causa");
            return false;
        }
        if(ddlArea.SelectedValue == "0")
        {
            VerError("Ingrese el area de la causa");
            return false;
        }
        if (ddlPotencialResponsable.SelectedValue == "0")
        {
            VerError("Ingrese el potencial de la causa");
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
                Identificacion pCausa = new Identificacion();
                pCausa.descripcion = txtDescripcionCausa.Text;
                pCausa.cod_area = Convert.ToInt64(ddlArea.SelectedValue);
                pCausa.cod_cargo = Convert.ToInt64(ddlPotencialResponsable.SelectedValue);

                if (idObjeto== "")
                    pCausa = identificacionServicio.CrearCausa(pCausa, (Usuario)Session["usuario"]);
                else
                {
                    pCausa.cod_causa = Convert.ToInt64(idObjeto);
                    pCausa = identificacionServicio.ModificarCausa(pCausa, (Usuario)Session["usuario"]);
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
        Session.Remove(identificacionServicio.CodigoProgramaCa + ".id");
        Navegar(Pagina.Lista);
    }

    private void ObtenerDatos(string idObjeto)
    {
        Identificacion pCausa = new Identificacion();
        pCausa.cod_causa = Convert.ToInt64(idObjeto);

        pCausa = identificacionServicio.ConsultarCausa(pCausa, (Usuario)Session["usuario"]);

        if (pCausa != null)
        {
            if(!string.IsNullOrWhiteSpace(pCausa.cod_causa.ToString()))
                txtCodigoCausa.Text = pCausa.cod_causa.ToString();
            if (!string.IsNullOrWhiteSpace(pCausa.descripcion.ToString()))
                txtDescripcionCausa.Text = pCausa.descripcion.ToString();
            if (!string.IsNullOrWhiteSpace(pCausa.cod_area.ToString()))
                ddlArea.SelectedValue = pCausa.cod_area.ToString();
            if (!string.IsNullOrWhiteSpace(pCausa.cod_cargo.ToString()))
                ddlPotencialResponsable.SelectedValue = pCausa.cod_cargo.ToString();
        }
    }
}