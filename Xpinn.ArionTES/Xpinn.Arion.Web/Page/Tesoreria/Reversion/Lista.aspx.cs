using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;

public partial class Lista : GlobalWeb
{
    Xpinn.Tesoreria.Services.OperacionServices operacionServicio = new Xpinn.Tesoreria.Services.OperacionServices();
    Xpinn.Tesoreria.Entities.Operacion operacion = new Xpinn.Tesoreria.Entities.Operacion();

    protected void Page_PreInit(object sender, System.EventArgs e)
    
    {
        try
        {
            VisualizarOpciones(operacionServicio.CodigoProgramaReversa, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(operacionServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            if (!IsPostBack)
            {
                mvReversion.ActiveViewIndex = 0;                            
                LlenarComboMotivosAnu(ddlMotivoAnulacion);
                ObtenerDatos();
                Actualizar();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(operacionServicio.GetType().Name + "A", "Page_Load", ex);
        }
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("../../../General/Global/inicio.aspx");
    }

    protected void LlenarComboMotivosAnu(DropDownList ddlMotivoAnu)
    {
        Xpinn.Caja.Services.TipoMotivoAnuService motivoAnuService = new Xpinn.Caja.Services.TipoMotivoAnuService();
        Xpinn.Caja.Entities.TipoMotivoAnu motivoAnu = new Xpinn.Caja.Entities.TipoMotivoAnu();
        ddlMotivoAnu.DataSource = motivoAnuService.ListarTipoMotivoAnu(motivoAnu, (Usuario)Session["usuario"]);
        ddlMotivoAnu.DataTextField = "descripcion";
        ddlMotivoAnu.DataValueField = "tipo_motivo";
        ddlMotivoAnu.DataBind();
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        mpeNuevo.Show();
    }

    protected void ObtenerDatos()
    {
        try
        {
            Usuario lusuario = (Usuario)Session["Usuario"];
            txtFecha.Text = System.DateTime.Now.ToLongDateString();
            txtOficina.Text = lusuario.nombre_oficina;
            txtCajero.Text = lusuario.nombre;

            Session["Oficina"] = lusuario.cod_oficina;
            Session["Cajero"] = lusuario.codusuario;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(operacionServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

    public void Actualizar()
    {
        try
        {
            List<Xpinn.Tesoreria.Entities.Operacion> lstConsulta = new List<Xpinn.Tesoreria.Entities.Operacion>();
            Xpinn.Tesoreria.Entities.Operacion operacion = new Xpinn.Tesoreria.Entities.Operacion();
            operacion.num_comp = -2;
            operacion.tipo_comp = -2;
            lstConsulta = operacionServicio.ListarOperacion(operacion, (Usuario)Session["usuario"]);
            if (lstConsulta.Count > 0)
            {
                gvOperacion.DataSource = lstConsulta;
                lblTitMotivoAnula.Visible = true;
                ddlMotivoAnulacion.Visible = true;
                gvOperacion.Visible = true;
                gvOperacion.DataBind();
            }
            else
            {
                gvOperacion.Visible = false;
                lblTitMotivoAnula.Visible = false;
                ddlMotivoAnulacion.Visible = false;
            }

            Session.Add(operacionServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(operacionServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

     protected void btnSeguir_Click(object sender, EventArgs e)
     {
        Navegar(Pagina.Lista);
     }

     protected void btnContinuar_Click(object sender, EventArgs e)
     {
         VerError("");
         try
         {
             CheckBox chkAnula;
             int contador = 0;

             foreach (GridViewRow fila in gvOperacion.Rows)
             {
                 chkAnula = (CheckBox)fila.FindControl("chkAnula");
                 if (chkAnula.Checked == true)
                 {
                     contador += 1;
                 }
             }
             if (contador == 0)
             {
                 VerError("Debe seleccionar las operaciones a reversar");
                 return;
             }

             if (operacionServicio.ReversarOperacion(gvOperacion, (Usuario)Session["usuario"]) == true)
                 mvReversion.ActiveViewIndex = 1;

         }
         catch (Exception ex)
         {
             BOexcepcion.Throw(operacionServicio.GetType().Name + "A", "ObtenerDatos", ex);
         }
     }

     protected void btnParar_Click(object sender, EventArgs e)
     {
         mpeNuevo.Hide();
     }
}