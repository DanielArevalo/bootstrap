using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.Programado.Services;
using Xpinn.Programado.Entities;


public partial class Nuevo : GlobalWeb
{

    LineasProgramadoServices LineasPrograService = new LineasProgramadoServices();
    MovimientoCuentasServices objeMoviento = new MovimientoCuentasServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[objeMoviento.CodigoProgramaMotivoAp + ".id"] != null)
                VisualizarOpciones(objeMoviento.CodigoProgramaMotivoAp, "E");
            else
                VisualizarOpciones(objeMoviento.CodigoProgramaMotivoAp, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objeMoviento.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lblmsj.Visible = false;

                if (Session[objeMoviento.CodigoProgramaMotivoAp + ".id"] != null)
                {
                    txtCodigo.Enabled = false;
                    idObjeto = Session[objeMoviento.CodigoProgramaMotivoAp + ".id"].ToString();
                    cargarCampos(Convert.ToInt64(idObjeto));
                    Session.Remove(objeMoviento.CodigoProgramaMotivoAp + ".id");
                }
                else
                {
                    txtCodigo.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objeMoviento.GetType().Name + "L", "Page_Load", ex);
        }
    }


    void cargarCampos(Int64 pIdObjeto) 
    {
        try
        {
            MotivoProgramadoE Entidad =  objeMoviento.getMotivoPByIdServices((Usuario)Session["usuario"],pIdObjeto);
            txtCodigo.Text = Entidad.Codigo.ToString();
            if(Entidad.Descripcion != string.Empty)
            txtDescripcion.Text = Entidad.Descripcion.ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objeMoviento.GetType().Name + "L", "Page_Load", ex);
        }
    }
  

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (txtDescripcion.Text.Trim()!="")
        {
            string msj = idObjeto != "" ? "modificar" : "grabar";
            ctlMensaje.MostrarMensaje("Desea " + msj + " los datos ingresados?");
        }
        else
        {
            VerError("Ingrese la Descripcion");
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            if (idObjeto != null && idObjeto!="") 
            {
                idObjeto = txtCodigo.Text;

                objeMoviento.updateMotivoProgramadoServices(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"], txtDescripcion.Text);
                Navegar(Pagina.Lista);
            }
            else
            {
                objeMoviento.creaMotivoProgramadoServices(txtDescripcion.Text, (Usuario)Session["usuario"]);
                Navegar(Pagina.Lista);
            }            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objeMoviento.CodigoProgramaMotivoAp, "btnContinuar_Click", ex);
        }
    }    

}
