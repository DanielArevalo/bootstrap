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
    private Xpinn.Contabilidad.Services.TipoComprobanteService TipoCompServicio = new Xpinn.Contabilidad.Services.TipoComprobanteService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[TipoCompServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(TipoCompServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(TipoCompServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoCompServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[TipoCompServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[TipoCompServicio.CodigoPrograma + ".id"].ToString();
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
            BOexcepcion.Throw(TipoCompServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Contabilidad.Entities.TipoComprobante vTipoComp = new Xpinn.Contabilidad.Entities.TipoComprobante();

            if (idObjeto != "")
                vTipoComp = TipoCompServicio.ConsultarTipoComprobante(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vTipoComp.tipo_comprobante = Convert.ToInt64(txtTipoComp.Text.Trim());
            vTipoComp.descripcion = Convert.ToString(txtDescripcion.Text.Trim());
            if (cbLocal.Checked == true)
                vTipoComp.tipo_norma = 0;
            else if (cbNif.Checked == true)
                vTipoComp.tipo_norma = 1;
            else if (cbAmbos.Checked == true)
                vTipoComp.tipo_norma = 2;

            if (idObjeto != "")
            {
                vTipoComp.tipo_comprobante = Convert.ToInt64(idObjeto);
                TipoCompServicio.ModificarTipoComprobante(vTipoComp, (Usuario)Session["usuario"]);
            }
            else
            {
                vTipoComp = TipoCompServicio.CrearTipoComprobante(vTipoComp, (Usuario)Session["usuario"]);
                idObjeto = vTipoComp.tipo_comprobante.ToString();
            }

            Session[TipoCompServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoCompServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Contabilidad.Entities.TipoComprobante vTipoComp = new Xpinn.Contabilidad.Entities.TipoComprobante();
            vTipoComp = TipoCompServicio.ConsultarTipoComprobante(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            cbLocal.Checked = false;
            cbNif.Checked = false;
            cbLocal.Checked = false;

            if (!string.IsNullOrEmpty(vTipoComp.tipo_comprobante.ToString()))
                txtTipoComp.Text = HttpUtility.HtmlDecode(vTipoComp.tipo_comprobante.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipoComp.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vTipoComp.descripcion.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipoComp.tipo_norma.ToString().Trim()))
            {                
                if (vTipoComp.tipo_norma == 1)
                    cbNif.Checked = true;
                else if (vTipoComp.tipo_norma == 2)
                    cbAmbos.Checked = true;
                else
                    cbLocal.Checked = true;                
            }
            cbAmbos_CheckedChanged(cbNif, null);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoCompServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void cbLocal_CheckedChanged(object sender, EventArgs e)
    {
        if (cbLocal.Checked)
        {
            cbNif.Checked = false;
            cbAmbos.Checked = false;
        }
        mostrarTipos();
    }

    protected void cbNif_CheckedChanged(object sender, EventArgs e)
    {
        if (cbNif.Checked)
        {
            cbLocal.Checked = false;
            cbAmbos.Checked = false;
        }
        mostrarTipos();
    }

    protected void cbAmbos_CheckedChanged(object sender, EventArgs e)
    {
        if (cbAmbos.Checked)
        {
            cbLocal.Checked = false;
            cbNif.Checked = false;
        }
        mostrarTipos();
    }

    protected void mostrarTipos()
    {
        if (cbLocal.Checked == false && cbNif.Checked == false && cbAmbos.Checked == false)
        {
            cbLocal.Checked = true;
        }
    }

 
}