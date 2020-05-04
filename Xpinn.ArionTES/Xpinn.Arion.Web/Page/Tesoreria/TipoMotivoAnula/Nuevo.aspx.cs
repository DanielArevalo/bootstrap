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
    private Xpinn.Caja.Services.TipoMotivoAnuService perfilServicio = new Xpinn.Caja.Services.TipoMotivoAnuService();
    TipoMotivoAnu entiti = new TipoMotivoAnu();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[perfilServicio.CodigoPrograma + ".id"] != null)
            {
                VisualizarOpciones(perfilServicio.CodigoPrograma, "E");
            }
            else
            {
                VisualizarOpciones(perfilServicio.CodigoPrograma, "A");
                txtCodigo.Enabled = false;
            }
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvAplicar.ActiveViewIndex = 0;

                if (Session[perfilServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[perfilServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(perfilServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    txtCodigo.Enabled = false;
                }
                else
                    txtCodigo.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.GetType().Name + "L", "Page_Load", ex);
        }
    }


    

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            txtCodigo.Text = idObjeto.ToString();
            txtDescripcion.Text = Session["descripcion"].ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.CodigoPrograma, "ObtenerDatos", ex);
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
        
        if (txtDescripcion.Text.Trim()=="")
        {
            VerError("Debe ingresar la descripción");
            return false;
        }
        
        
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
        if (idObjeto == "")
        {
            //CREA
            TipoMotivoAnu datos = new TipoMotivoAnu();
            if (txtDescripcion.Text != "")
                datos.descripcion = txtDescripcion.Text;
          
                
            perfilServicio.CrearTipoMotivoAnus(datos,(Usuario)Session["usuario"]);
        }
        else
        { 
        //MODIFICA
            TipoMotivoAnu datos = new TipoMotivoAnu();
            datos.tipo_motivo = Convert.ToInt64(idObjeto);

            if (!string.IsNullOrWhiteSpace(txtDescripcion.Text))
                datos.descripcion = txtDescripcion.Text;
            perfilServicio.ModificarTipoMotivoAnus(datos,(Usuario)Session["usuario"]);
        }
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        string msj = idObjeto != "" ? "Modificado" : "Grabado";
        mvAplicar.ActiveViewIndex = 1;
    }
    

}


