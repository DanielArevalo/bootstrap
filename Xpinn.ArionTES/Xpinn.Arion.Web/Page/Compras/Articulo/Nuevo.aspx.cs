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
using Xpinn.ActivosFijos.Entities;

partial class Nuevo : GlobalWeb
{
    private Xpinn.ActivosFijos.Services.Articuloservices Articuloservices = new Xpinn.ActivosFijos.Services.Articuloservices();
    private Xpinn.ActivosFijos.Services.TipoArticuloservices TipoArticuloservices = new Xpinn.ActivosFijos.Services.TipoArticuloservices();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[Articuloservices.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(Articuloservices.CodigoPrograma, "E");
            else
                VisualizarOpciones(Articuloservices.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Articuloservices.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void ddlTipoArticulo_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        try
        {

            //if (ddlCuenta.SelectedItem != null)
            //    AsignarNumeroCheque(ddlCuenta.SelectedItem.Text);
        }
        catch
        { }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Xpinn.ActivosFijos.Entities.TipoArticulo vTipoArticulo = new Xpinn.ActivosFijos.Entities.TipoArticulo();
                 List<Xpinn.ActivosFijos.Entities.TipoArticulo> lstTipoArticulo = new List<Xpinn.ActivosFijos.Entities.TipoArticulo>();
                lstTipoArticulo = TipoArticuloservices.ListarTipoArticulo(vTipoArticulo, (Usuario)Session["usuario"]);





                if (lstTipoArticulo.Count > 0)
                {
                    ListItem i;
                    foreach (TipoArticulo item in lstTipoArticulo)

                    {
                        i = new ListItem(item.Descripcion, item.IdTipo_Articulo.ToString());
                        ddlTipoArticulo.Items.Add(i);
                    }
                }


                if (Session[Articuloservices.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[Articuloservices.CodigoPrograma + ".id"].ToString();
                    Session.Remove(Articuloservices.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    txtCodigo.Text = Convert.ToString(Articuloservices.ObtenerSiguienteCodigo((Usuario)Session["Usuario"]));
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Articuloservices.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.ActivosFijos.Entities.Articulo vArticulo = new Xpinn.ActivosFijos.Entities.Articulo();

            if (idObjeto != "")
                vArticulo = Articuloservices.ConsultarArticulo(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

                    vArticulo.idarticulo    = Convert.ToInt32(txtCodigo.Text.Trim());
                    vArticulo.descripcion    = Convert.ToString(txtDescripcion.Text.Trim());
                    vArticulo.idtipo_articulo    = Convert.ToInt32(ddlTipoArticulo.SelectedValue .Trim());
                    vArticulo.marca = txtMarca.Text.Trim();
                    vArticulo.serial  = Convert.ToString(txtSerial.Text.Trim());
                    vArticulo.referencia  = txtReferencia.Text.Trim();

            if (idObjeto != "")
            {
                vArticulo.idarticulo = Convert.ToInt32(idObjeto);
                Articuloservices.ModificarArticulo (vArticulo, (Usuario)Session["usuario"]);
            }
            else
            {
                vArticulo = Articuloservices .CrearTipoArticulo (vArticulo, (Usuario)Session["usuario"]);
                idObjeto = vArticulo.idarticulo   .ToString();
            }

            Session[Articuloservices.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Articuloservices.CodigoPrograma, "btnGuardar_Click", ex);
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

             Xpinn.ActivosFijos.Entities.Articulo vArticulo = new Xpinn.ActivosFijos.Entities.Articulo();
            
            vArticulo = Articuloservices.ConsultarArticulo(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vArticulo.idarticulo   .ToString()))
                txtCodigo.Text = HttpUtility.HtmlDecode(vArticulo.idarticulo.ToString().Trim());
            if (!string.IsNullOrEmpty(vArticulo.descripcion  ))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vArticulo.descripcion .ToString().Trim());
            if (!string.IsNullOrEmpty(vArticulo.serial  .ToString()))
                txtSerial  .Text = HttpUtility.HtmlDecode(vArticulo.serial.ToString().Trim());
            if (!string.IsNullOrEmpty(vArticulo.referencia .ToString()))
                txtReferencia .Text = HttpUtility.HtmlDecode(vArticulo.referencia.ToString().Trim());
            if (!string.IsNullOrEmpty(vArticulo.marca ))
                txtMarca.Text = HttpUtility.HtmlDecode(vArticulo.marca.ToString().Trim());
            if (!string.IsNullOrEmpty(vArticulo.idtipo_articulo .ToString()))
                ddlTipoArticulo.SelectedValue  = HttpUtility.HtmlDecode(vArticulo.idtipo_articulo .ToString().Trim());

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Articuloservices.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


}