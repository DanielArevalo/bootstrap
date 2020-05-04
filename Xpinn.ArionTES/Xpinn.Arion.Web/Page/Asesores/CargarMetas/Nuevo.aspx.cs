using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Util;

public partial class Nuevo : GlobalWeb
{
    Usuario usuario = new Usuario();
    EjecutivoMetaService serviceEjeMeta = new EjecutivoMetaService();
    
    private void Page_PreInit(object sender, EventArgs evt)
    {
        try
        {
            if (Session[serviceEjeMeta.CodigoPrograma2 + ".id"] != null){
                VisualizarOpciones(serviceEjeMeta.CodigoPrograma2, "E");
            }
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjeMeta.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try{
            if (!Page.IsPostBack)
            {
          
                //Session una EntidadEjecutivoMeta
                if (Session["EditMetaEjecutivo"] != null){
                    Session.Remove(serviceEjeMeta.CodigoPrograma2 + ".id");                  
                    Session.Remove("EditMetaEjecutivo");
                }
            }
        }
        catch (ExceptionBusiness ex){
            VerError(ex.Message);
        }
        catch (Exception ex){
            BOexcepcion.Throw(serviceEjeMeta.GetType().Name + "A", "Page_Load", ex);
        }
    }// end pageLoad
    /// <summary>
    /// LLenar el dropdownlist que permite filtras por oficinas
    /// </summary>
    /// <param name="ddlOficinas"></param>
   
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            EjecutivoMeta ejeMeta = new EjecutivoMeta();

         
            ejeMeta.NombreMeta = txtNombMeta.Text;
            ejeMeta.formatoMeta = ddlformatoMeta.SelectedItem.Text;          
            ejeMeta = serviceEjeMeta.CrearMeta(ejeMeta, usuario);

            idObjeto = ejeMeta.IdEjecutivoMeta.ToString();

           // Navegar(Pagina.Lista); 


        }catch (ExceptionBusiness ex){
            VerError(ex.Message);
        }catch (Exception ex){
            BOexcepcion.Throw(serviceEjeMeta.GetType().Name + "A", "btnGuardar_Click", ex);
        }
    }



    

    
}