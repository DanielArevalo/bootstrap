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
    EjecutivoMeta editMeta;
     Int64 IdMeta = 0;
     String operacion = "";
     private void Page_PreInit(object sender, EventArgs evt)
    {
        try
        {
            if (Session[serviceEjeMeta.CodigoProgMetas + ".id"] != null){
                VisualizarOpciones(serviceEjeMeta.CodigoProgMetas, "E");
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

            }
           
                //Session una EntidadEjecutivoMeta
                //if (Session["EditMetaEjecutivo"] != null){
                   // Session.Remove(serviceEjeMeta.CodigoProgMetas + ".id");
                String IdObjeto = Convert.ToString(Session["EditMetaEjecutivo"]);
                if (IdObjeto != null)
                
                {
                    ObtenerDatos(IdObjeto);
                }
                    Session.Remove("EditMetaEjecutivo");
              // }
                    if (IdObjeto =="")
                    {
                        
                    }
        }
        catch (ExceptionBusiness ex){
            VerError(ex.Message);
        }
        catch (Exception ex){
            BOexcepcion.Throw(serviceEjeMeta.GetType().Name + "A", "Page_Load", ex);
        }
    }// end pageLoad

    protected void ObtenerDatos(String pIdObjeto)
    {
        if (pIdObjeto == "")
        {
        }
        else
        {
        try
        {
            EjecutivoMeta entityEjecutivoMeta = new EjecutivoMeta();
            entityEjecutivoMeta = serviceEjeMeta.ConsultarMetas((Usuario)Session["usuario"], pIdObjeto);
           
             IdMeta = entityEjecutivoMeta.IdMeta;
             txtIdmeta.Text = Convert.ToString(IdMeta);
             this.txtNombMeta.Text=entityEjecutivoMeta.NombreMeta;

             String formato="";
             formato = entityEjecutivoMeta.formatoMeta;
             if (formato == "#")
            {
                ddlformatoMeta.SelectedValue ="1";
            }
             if (formato == "%")
             {
                 ddlformatoMeta.SelectedValue = "2";
             }
             if (formato == "$")
             {
                 ddlformatoMeta.SelectedValue = "3";
             }

            //VerAuditoria(aseEntCliente.UsuarioCrea, aseEntCliente.FechaCrea, aseEntCliente.UsuarioEdita, aseEntCliente.FechaEdita);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjeMeta.GetType().Name + "A", "ObtenerDatos", ex);
        }
        }
      
    }
   
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            EjecutivoMeta ejeMeta = new EjecutivoMeta();
            if (Session["operacion"] == "E")
            {
                IdMeta = Convert.ToInt64(txtIdmeta.Text);
                ejeMeta.NombreMeta = txtNombMeta.Text;
                ejeMeta.formatoMeta = ddlformatoMeta.SelectedItem.Text;
                if (IdMeta != 0)
                {
                    ejeMeta.IdMeta = IdMeta;
                    ejeMeta = serviceEjeMeta.ModificarMeta(ejeMeta, (Usuario)Session["usuario"]);
                }
            }

            if (Session["operacion"] == "N")
            {
                ejeMeta.NombreMeta = txtNombMeta.Text;
                ejeMeta.formatoMeta = ddlformatoMeta.SelectedItem.Text;
                ejeMeta = serviceEjeMeta.CrearMeta(ejeMeta, (Usuario)Session["usuario"]);
            }
            idObjeto = ejeMeta.IdEjecutivoMeta.ToString();

            Navegar(Pagina.Lista); 


        }catch (ExceptionBusiness ex){
            VerError(ex.Message);
        }catch (Exception ex){
            BOexcepcion.Throw(serviceEjeMeta.GetType().Name + "A", "btnGuardar_Click", ex);
        }
    }



    

    
}