using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Collections.Generic;

partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.IngresosFamiliaService IngresosFamiliaServicio = new Xpinn.FabricaCreditos.Services.IngresosFamiliaService();
    private Xpinn.FabricaCreditos.Services.EstadosFinancierosService EstadosFinancierosServicio = new Xpinn.FabricaCreditos.Services.EstadosFinancierosService();
    
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[IngresosFamiliaServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(IngresosFamiliaServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(IngresosFamiliaServicio.CodigoPrograma, "A");

            Site2 toolBar = (Site2)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(IngresosFamiliaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Toma Utilidad Neta del Estado de Resultados.
            List<Xpinn.FabricaCreditos.Entities.EstadosFinancieros> lstEstadosFinancieros = new List<Xpinn.FabricaCreditos.Entities.EstadosFinancieros>();
            lstEstadosFinancieros = EstadosFinancierosServicio.ListarEstadosFinancieros(ObtenerEstadosFinancieros(), (Usuario)Session["usuario"]);
            txtNegocio.Text = lstEstadosFinancieros.Sum(item => item.valor).ToString();
            txtCod_persona.Text = Session["Cod_persona"].ToString(); 
            if (!IsPostBack)
            {
                if (Session[IngresosFamiliaServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[IngresosFamiliaServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(IngresosFamiliaServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(IngresosFamiliaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.IngresosFamilia vIngresosFamilia = new Xpinn.FabricaCreditos.Entities.IngresosFamilia();

            if (idObjeto != "")
                vIngresosFamilia = IngresosFamiliaServicio.ConsultarIngresosFamilia(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (txtCod_ingreso.Text != "") vIngresosFamilia.cod_ingreso = Convert.ToInt64(txtCod_ingreso.Text.Trim());
            if (txtIngresos.Text != "") vIngresosFamilia.ingresos = Convert.ToInt64(txtIngresos.Text.Trim().Replace(@".", ""));
            if (txtNegocio.Text != "") vIngresosFamilia.negocio = Convert.ToInt64(txtNegocio.Text.Trim().Replace(@".", ""));
            if (txtConyuge.Text != "") vIngresosFamilia.conyuge = Convert.ToInt64(txtConyuge.Text.Trim().Replace(@".",""));
            if (txtHijos.Text != "") vIngresosFamilia.hijos = Convert.ToInt64(txtHijos.Text.Trim().Replace(@".", ""));
            if (txtArriendos.Text != "") vIngresosFamilia.arriendos = Convert.ToInt64(txtArriendos.Text.Trim().Replace(@".", ""));
            if (txtPension.Text != "") vIngresosFamilia.pension = Convert.ToInt64(txtPension.Text.Trim().Replace(@".", ""));
            if (txtOtros.Text != "") vIngresosFamilia.otros = Convert.ToInt64(txtOtros.Text.Trim().Replace(@".", ""));
            if (txtCod_persona.Text != "") vIngresosFamilia.cod_persona = Convert.ToInt64(txtCod_persona.Text.Trim());

            if (idObjeto != "")
            {
                vIngresosFamilia.cod_ingreso = Convert.ToInt64(idObjeto);
                IngresosFamiliaServicio.ModificarIngresosFamilia(vIngresosFamilia, (Usuario)Session["usuario"]);
            }
            else
            {
                vIngresosFamilia = IngresosFamiliaServicio.CrearIngresosFamilia(vIngresosFamilia, (Usuario)Session["usuario"]);
                idObjeto = vIngresosFamilia.cod_ingreso.ToString();
            }

            Session[IngresosFamiliaServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(IngresosFamiliaServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Session[IngresosFamiliaServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.IngresosFamilia vIngresosFamilia = new Xpinn.FabricaCreditos.Entities.IngresosFamilia();
            vIngresosFamilia = IngresosFamiliaServicio.ConsultarIngresosFamilia(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vIngresosFamilia.cod_ingreso != Int64.MinValue)
                txtCod_ingreso.Text = HttpUtility.HtmlDecode(vIngresosFamilia.cod_ingreso.ToString().Trim());
            if (vIngresosFamilia.ingresos != Int64.MinValue)
                txtIngresos.Text = HttpUtility.HtmlDecode(vIngresosFamilia.ingresos.ToString().Trim());
            if (vIngresosFamilia.negocio != Int64.MinValue)
                txtNegocio.Text = HttpUtility.HtmlDecode(vIngresosFamilia.negocio.ToString().Trim());
            if (vIngresosFamilia.conyuge != Int64.MinValue)
                txtConyuge.Text = HttpUtility.HtmlDecode(vIngresosFamilia.conyuge.ToString().Trim());
            if (vIngresosFamilia.hijos != Int64.MinValue)
                txtHijos.Text = HttpUtility.HtmlDecode(vIngresosFamilia.hijos.ToString().Trim());
            if (vIngresosFamilia.arriendos != Int64.MinValue)
                txtArriendos.Text = HttpUtility.HtmlDecode(vIngresosFamilia.arriendos.ToString().Trim());
            if (vIngresosFamilia.pension != Int64.MinValue)
                txtPension.Text = HttpUtility.HtmlDecode(vIngresosFamilia.pension.ToString().Trim());
            if (vIngresosFamilia.otros != Int64.MinValue)
                txtOtros.Text = HttpUtility.HtmlDecode(vIngresosFamilia.otros.ToString().Trim());
            if (vIngresosFamilia.cod_persona != Int64.MinValue)
               txtCod_persona.Text = HttpUtility.HtmlDecode(vIngresosFamilia.cod_persona.ToString().Trim());
            
            

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(IngresosFamiliaServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    private Xpinn.FabricaCreditos.Entities.EstadosFinancieros ObtenerEstadosFinancieros()
    {
        Xpinn.FabricaCreditos.Entities.EstadosFinancieros vEstadosFinancieros = new Xpinn.FabricaCreditos.Entities.EstadosFinancieros();
        vEstadosFinancieros.cod_inffin = 18;
        vEstadosFinancieros.filtro = "UtiNet";


        return vEstadosFinancieros;
    }
}