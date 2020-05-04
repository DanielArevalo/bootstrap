using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Util;

public partial class Detalle : GlobalWeb
{
    Usuario usuario = new Usuario();
    EjecutivoMetaService serviceEjeMeta = new EjecutivoMetaService();
    
    private void Page_PreInit(object sender, EventArgs evt)
    {
        try
        {
            if (Session[serviceEjeMeta.CodigoPrograma + ".id"] != null){
                VisualizarOpciones(serviceEjeMeta.CodigoPrograma, "E");
            }
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
          //  toolBar.eventoEditar += btnEditar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjeMeta.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try{
            if (!Page.IsPostBack){
                //Session una EntidadEjecutivoMeta
                if (Session["EditMetaEjecutivo"] != null){
                    Session.Remove(serviceEjeMeta.CodigoPrograma + ".id");
                    ObtenerDatos((EjecutivoMeta)(Session["EditMetaEjecutivo"]));
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

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
   {
     //  GuardarValoresConsulta(pConsulta, serviceEjecutivoMeta.GetType().Name);
       //Navegar(Pagina.Editar);
   }
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            EjecutivoMeta ejeMeta = new EjecutivoMeta();

            if (!string.IsNullOrEmpty(txtPrimerNombre.Text.Trim()))     ejeMeta.PrimerNombre    = txtPrimerNombre.Text.Trim();
            if (!string.IsNullOrEmpty(txtSegundoNombre.Text.Trim()))    ejeMeta.SegundoNombre   = txtSegundoNombre.Text.Trim();

            ejeMeta.VlrMeta = txtVlrMeta.Text != "" ? Convert.ToInt64(txtVlrMeta.Text.Trim().Replace(".", "")) : 0;

           
            if (!string.IsNullOrEmpty(DdlMes.SelectedValue))            ejeMeta.Mes             = DdlMes.SelectedValue;
            if (!string.IsNullOrEmpty(txtSegundoNombre.Text.Trim())) ejeMeta.SegundoNombre = txtSegundoNombre.Text.Trim();

         
            ejeMeta.IdMeta = Convert.ToInt64(lblIdMeta.Text);
            ejeMeta.IdEjecutivoMeta = Convert.ToInt64(lblIdEjecMeta.Text);
            //Session["Ejecutivo"] = ejeMeta.IdEjecutivo;

            ejeMeta.IdEjecutivo = Convert.ToInt64(Session["Ejecutivo"]);
            if (!string.IsNullOrEmpty(DdlYear.SelectedValue)) ejeMeta.Year = DdlYear.SelectedValue;
            //ejeMeta.Year = DdlYear.SelectedValue;
            ejeMeta.Vigencia = "1";
            ejeMeta.Fecha = DateTime.Now;
            ejeMeta = serviceEjeMeta.ActualizarEjecutivoMeta(ejeMeta, usuario);
           
            idObjeto = ejeMeta.IdEjecutivoMeta.ToString();

            Navegar(Pagina.Lista); 


        }catch (ExceptionBusiness ex){
            VerError(ex.Message);
        }catch (Exception ex){
            BOexcepcion.Throw(serviceEjeMeta.GetType().Name + "A", "btnGuardar_Click", ex);
        }
    }

    protected void ObtenerDatos(EjecutivoMeta editEjeMeta)
    {
        try
        {
            if (!string.IsNullOrEmpty(editEjeMeta.PrimerNombre))    txtPrimerNombre.Text    = editEjeMeta.PrimerNombre;
            if (!string.IsNullOrEmpty(editEjeMeta.SegundoNombre))   txtSegundoNombre.Text   = editEjeMeta.SegundoNombre;
            if (!string.IsNullOrEmpty(editEjeMeta.NombreOficina))   txtOficina.Text         = editEjeMeta.NombreOficina.ToString();
            if (!string.IsNullOrEmpty(editEjeMeta.Mes))             DdlMes.SelectedValue    = editEjeMeta.Mes.ToString();           
            if (!string.IsNullOrEmpty(editEjeMeta.NombreMeta))      txtNombMeta.Text        = editEjeMeta.NombreMeta.ToString();
            if (editEjeMeta.VlrMeta!=0)                             txtVlrMeta.Text         = editEjeMeta.VlrMeta.ToString();
            if (!string.IsNullOrEmpty(editEjeMeta.Year))            DdlYear.SelectedValue   = editEjeMeta.Year.ToString();
            lblIdEjecMeta.Text = editEjeMeta.IdEjecutivoMeta.ToString();
            lblIdMeta.Text = editEjeMeta.IdMeta.ToString();

            //VerAuditoria(aseEntCliente.UsuarioCrea, aseEntCliente.FechaCrea, aseEntCliente.UsuarioEdita, aseEntCliente.FechaEdita);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjeMeta.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }


    protected void DdlYear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}