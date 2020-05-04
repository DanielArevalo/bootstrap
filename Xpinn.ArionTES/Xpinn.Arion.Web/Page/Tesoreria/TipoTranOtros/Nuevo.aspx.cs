using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Caja.Entities;
using Xpinn.Caja.Services;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class Nuevo : GlobalWeb
{
    TipoOperacionService objOpercion = new TipoOperacionService();
    TipoOperacion entiti = new TipoOperacion();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[objOpercion.CodigoPrograma + ".id"] != null)
            {
                VisualizarOpciones(objOpercion.CodigoPrograma, "E");
            }
            else
            {
                VisualizarOpciones(objOpercion.CodigoPrograma, "A");
                txtCodigo.Enabled = false;
            }
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objOpercion.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvAplicar.ActiveViewIndex = 0;

                if (Session[objOpercion.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[objOpercion.CodigoPrograma + ".id"].ToString();
                    Session.Remove(objOpercion.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    txtCodigo.Enabled = false;
                }
                else
                {
                    txtCodigo.Text = "Autogenerado";
                    txtCodigo.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objOpercion.GetType().Name + "L", "Page_Load", ex);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
       // Session["tipo_movimiento"] = null;
        try
        {
            txtCodigo.Text = idObjeto.ToString();
            txtDescripcion.Text = Session["descripcion"].ToString();
            TextBox1.Text = Session["tipo_movimiento"].ToString();
          
            if (TextBox1.Text == "1")
                ddlTipoMovimientos.SelectedValue = "1";
            if (TextBox1.Text == "2")
                ddlTipoMovimientos.SelectedValue = "2";

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objOpercion.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void btnFinal_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    public Boolean ValidarDatos()
    {
        if (txtCodigo.Text.Trim() == "")
        {
            VerError("Ingrese el código correspondiente");
            return false;
        }
        
        if (txtDescripcion.Text.Trim()=="")
        {
            VerError("Debe ingresar la descripción");
            return false;
        }
        //if (Convert.ToInt64(txtCodigo.Text.Trim())<1000)
        //{
        //    VerError("Ingrese Un codigo mayor a 1000");
        //    return false;
        //}
        
         return true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            string msj = idObjeto != "" ? "Modificar" : "Grabar";
            ctlMensaje.MostrarMensaje("Desea " + msj + " los datos ingresados?");
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        VerError("");
        try
        {
            Usuario User = (Usuario)Session["usuario"];
            if (idObjeto != "")
            {
                //MODIFICAR

                TipoOperacion vData = new TipoOperacion
                {
                    tipo_tran = Convert.ToInt64(txtCodigo.Text.Trim()),
                    concepto = txtDescripcion.Text.Trim(),
                    tipo_producto = 7,
                    tipo_movimiento = Convert.ToInt64(this.ddlTipoMovimientos.SelectedValue),
                    cod_caja = 1
                };
                try
                    {
                        objOpercion.ModificaTipoOpServices(vData, (Usuario)Session["usuario"]);
                        Session["Proceso"] = null;
                }
                    catch (Exception ex)
                    {
                        VerError(ex.Message);
                        return;
                    }                    
               // }

            }
            else
            {
                TipoOperacion vData = new TipoOperacion
                {
                    tipo_tran = Convert.ToInt64(0),
                    concepto = txtDescripcion.Text.Trim(),
                    tipo_producto = 7,
                    tipo_movimiento = Convert.ToInt64(this.ddlTipoMovimientos.SelectedValue),
                    cod_caja = 0
                };
                //CREA
                List<TipoOperacion> lsta = objOpercion.validaDatosBusinne((Usuario)Session["usuario"], Convert.ToInt32(0), txtDescripcion.Text.Trim(), 2);
                if (lsta.Count <= 0)
                {
                   try
                        {
                            objOpercion.insertTipoOPBusines(vData, (Usuario)Session["usuario"]);
                            vData.cod_caja = 1;
                            Session["Proceso"] = vData;
                            Navegar("../../Contabilidad/ParametrosCtasOtros/Nuevo.aspx");
                    }
                        catch (Exception ex)
                        {
                            VerError(ex.Message);
                            return;
                        }
                }
                else
                {
                    VerError("Descripcion Duplicada");
                    return;
                }
                
            }

            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            string msj = idObjeto != "" ? "Modificado" : "Grabado";
            mvAplicar.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objOpercion.CodigoPrograma, "btnContinuar_Click", ex);
        }
    }

    protected void ddlTipoMovimientos_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (TextBox1.Text=="1")
            ddlTipoMovimientos.SelectedValue= "1";
        if (TextBox1.Text == "2")
            ddlTipoMovimientos.SelectedValue = "2";
    }

    protected void btnPContable_Click(object sender, EventArgs e)
    {
       
    }
}


