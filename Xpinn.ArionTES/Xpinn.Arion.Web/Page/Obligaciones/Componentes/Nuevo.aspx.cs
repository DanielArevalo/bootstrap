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

partial class Nuevo : GlobalWeb
{
    private Xpinn.Obligaciones.Services.ComponenteService ComponenteServicio = new Xpinn.Obligaciones.Services.ComponenteService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ComponenteServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ComponenteServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(ComponenteServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComponenteServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[ComponenteServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[ComponenteServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ComponenteServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComponenteServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Obligaciones.Entities.Componente vComponente = new Xpinn.Obligaciones.Entities.Componente();

            if (idObjeto != "")
                vComponente = ComponenteServicio.ConsultarComponente(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vComponente.CODCOMPONENTE = Convert.ToInt64(txtCodigo.Text.Trim());
            vComponente.NOMBRE = Convert.ToString(txtNombre.Text.Trim());
            if (chkCausa.Checked == true)
                vComponente.CAUSA = 1;
            else
                vComponente.CAUSA = 0;

            if (idObjeto != "")
            {
                vComponente.CODCOMPONENTE = Convert.ToInt64(idObjeto);
                ComponenteServicio.ModificarComponente(vComponente, (Usuario)Session["usuario"]);
            }
            else
            {
                vComponente = ComponenteServicio.CrearComponente(vComponente, (Usuario)Session["usuario"]);
                idObjeto = vComponente.CODCOMPONENTE.ToString();
            }

            Session[ComponenteServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComponenteServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Session[ComponenteServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Obligaciones.Entities.Componente vComponente = new Xpinn.Obligaciones.Entities.Componente();
            vComponente = ComponenteServicio.ConsultarComponente(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            txtCodigo.Text = HttpUtility.HtmlDecode(vComponente.CODCOMPONENTE.ToString().Trim());
            if (!string.IsNullOrEmpty(vComponente.NOMBRE))
                txtNombre.Text = HttpUtility.HtmlDecode(vComponente.NOMBRE.ToString().Trim());
            if (vComponente.CAUSA.ToString() == "1")
                chkCausa.Checked = true;
            else
                chkCausa.Checked = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComponenteServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


}