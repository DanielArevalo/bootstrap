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
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;

partial class Filtrar : GlobalWeb
{

    PolizasSegurosService polizasSegurosServicio = new PolizasSegurosService();
   
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {

           // VisualizarOpciones(polizasSegurosServicio.GetType().Name, polizasSegurosServicio.GetType().Name);
            VisualizarOpciones(polizasSegurosServicio.CodigoPrograma1.ToString(), "L");
           
            Site toolBar = (Site)this.Master;
          //  toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(polizasSegurosServicio.CodigoPrograma1, "Page_PreInit", ex);
            //BOexcepcion.Throw(polizasSegurosServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
        


    }
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
          Page.Validate();

          if (Page.IsValid)
          {
             // GuardarValoresConsulta(pConsulta, polizasSegurosServicio.CodigoPrograma);
              Actualizar();
          }
    }

      
   


    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
       // LimpiarValoresConsulta(pConsulta, polizasSegurosServicio.GetType().Name);
        LimpiarValoresConsulta(pConsulta, polizasSegurosServicio.CodigoPrograma1);
    }

         
    private void Actualizar()
    {
        try
        {

            List<PolizasSeguros> lstConsulta = new List<PolizasSeguros>();

            PolizasSeguros poliza = new PolizasSeguros();
            poliza.numero_radicacion = Convert.ToInt64("0" + this.TxtNumeroRadicacion.Text);
            poliza.identificacion = Convert.ToInt64("0" + this.txtCedulaCliente.Text);
            poliza.primer_nombre = this.TxtPrimerNombre.Text.ToUpper();
            poliza.segundo_nombre = this.TxtSegundoNombre.Text.ToUpper();
            poliza.primer_apellido = this.TxtPrimerApellido.Text.ToUpper();
            poliza.segundo_apellido = this.TxtSegundoApellido.Text.ToUpper();
            try
            {
                lstConsulta = polizasSegurosServicio.FiltrarCredito(poliza, (Usuario)Session["usuario"],null);//, (UserSession)Session["user"]);
            }
            catch (Exception ex)
            {
                this.Lblerror.Text = ex.Message;
                return;
            }
            Lblerror.Text="";
            gvPolizasSeguros.PageSize = pageSize;
            String emptyQuery = "Fila de datos vacia";
            gvPolizasSeguros.EmptyDataText = emptyQuery;
            gvPolizasSeguros.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvPolizasSeguros.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvPolizasSeguros.DataBind();
                ValidarPermisosGrilla(gvPolizasSeguros);
            }
            else
            {
                gvPolizasSeguros.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
            }

            Session.Add(polizasSegurosServicio.CodigoPrograma1+ ".consulta", 1);

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(polizasSegurosServicio.CodigoPrograma1, "Actualizar", ex);
        }
    }
    
   // private PolizasSeguros  ObtenerValores()
   // {
      //  PolizasSeguros entityPolizasSeguros = new PolizasSeguros();

      //entityPolizasSeguros.cod_poliza = txtCodigoPoliza.Text.Trim();
      // // entityPolizasSeguros.cod_poliza = idOf;
      //  return entityPolizasSeguros;



    protected void gvPolizasSeguros_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
          try
        {
            gvPolizasSeguros.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(polizasSegurosServicio.CodigoPrograma, "gvFiltro_PageIndexChanging", ex);
        }
    }
}

