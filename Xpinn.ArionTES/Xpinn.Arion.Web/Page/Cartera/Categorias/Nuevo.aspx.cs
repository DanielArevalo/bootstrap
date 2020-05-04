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
    private Xpinn.Cartera.Services.CategoriasService categoriaServicio = new Xpinn.Cartera.Services.CategoriasService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[categoriaServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(categoriaServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(categoriaServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(categoriaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[categoriaServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[categoriaServicio.CodigoPrograma + ".id"].ToString();
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
            BOexcepcion.Throw(categoriaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Cartera.Entities.Categorias vCateg = new Xpinn.Cartera.Entities.Categorias();

            if (idObjeto != "")
                vCateg = categoriaServicio.ConsultarCategorias(Convert.ToString(idObjeto), (Usuario)Session["usuario"]);

            vCateg.cod_categoria = Convert.ToString(txtCodigo.Text.Trim());
            vCateg.descripcion = Convert.ToString(txtDescripcion.Text.Trim());
            if (idObjeto != "")
            {
                vCateg.cod_categoria = Convert.ToString(idObjeto);
                categoriaServicio.ModificarCategorias(vCateg, (Usuario)Session["usuario"]);
            }
            else
            {
                vCateg = categoriaServicio.CrearCategorias(vCateg, (Usuario)Session["usuario"]);
                idObjeto = vCateg.cod_categoria.ToString();
            }

            Session[categoriaServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(categoriaServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Xpinn.Cartera.Entities.Categorias vCateg = new Xpinn.Cartera.Entities.Categorias();
            vCateg = categoriaServicio.ConsultarCategorias(Convert.ToString(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vCateg.cod_categoria.ToString()))
                txtCodigo.Text = HttpUtility.HtmlDecode(vCateg.cod_categoria.ToString().Trim());
            if (!string.IsNullOrEmpty(vCateg.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vCateg.descripcion.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(categoriaServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    
 
}