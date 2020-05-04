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

public partial class Nuevo : GlobalWeb
{
    Usuario usuario = new Usuario();
    EjecutivoMetaService serviceEjeMeta = new EjecutivoMetaService();
    EjecutivoMeta Serviceejecutivo = new EjecutivoMeta();
    private void Page_PreInit(object sender, EventArgs evt)
    {
        try
        {
            if (Session[serviceEjeMeta.CodigoPrograma + ".id"] != null){
                VisualizarOpciones(serviceEjeMeta.CodigoPrograma, "E");
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
            if (!Page.IsPostBack){
                LlenarComboAsesores();
                LlenarMetas();
                LlenarPeriodicidad();
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
        
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            EjecutivoMeta ejeMeta = new EjecutivoMeta();

            ejeMeta.VlrMeta = txtVlrMeta.Text != "" ? Convert.ToInt64(txtVlrMeta.Text.Trim().Replace(".", "")) : 0;

           
            if (!string.IsNullOrEmpty(DdlMes.SelectedValue))            
                ejeMeta.Mes             = DdlMes.SelectedValue;
           
         
            ejeMeta.IdMeta = Convert.ToInt64(DdlMeta.SelectedValue);
            ejeMeta.IdEjecutivo = Convert.ToInt64(DdlEjecutivo.SelectedValue);

           // ejeMeta.IdEjecutivoMeta = Convert.ToInt64(lblIdEjecMeta.Text);
           
           
            if (!string.IsNullOrEmpty(DdlYear.SelectedValue)) ejeMeta.Year = DdlYear.SelectedValue;
            if (!string.IsNullOrEmpty(DdlVigencia.SelectedValue)) ejeMeta.Vigencia = DdlVigencia.SelectedValue;
          
          
            ejeMeta.Fecha = DateTime.Now;
            ejeMeta = serviceEjeMeta.CrearEjecutivoMeta(ejeMeta, (Usuario)Session["usuario"]);
           
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
          
            if (!string.IsNullOrEmpty(editEjeMeta.Mes))             DdlMes.SelectedValue    = editEjeMeta.Mes.ToString();
            if (!string.IsNullOrEmpty(editEjeMeta.NombreMeta))      DdlMeta.SelectedValue   = editEjeMeta.NombreMeta.ToString();
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

    protected void LlenarComboAsesores()
    {
        List<EjecutivoMeta> lstConsulta = new List<EjecutivoMeta>();

        EjecutivoMeta usuarioAse = new EjecutivoMeta();
        DdlEjecutivo.DataSource = serviceEjeMeta.ListarEjecutivos(usuarioAse, (Usuario)Session["usuario"]);
        DdlEjecutivo.DataTextField = "Nombres";
        DdlEjecutivo.DataValueField = "IdEjecutivo";
        DdlEjecutivo.DataBind();
        DdlEjecutivo.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    protected void LlenarMetas()
    {
        List<EjecutivoMeta> lstConsulta = new List<EjecutivoMeta>();

        EjecutivoMeta usuarioAse = new EjecutivoMeta();
        DdlMeta.DataSource = serviceEjeMeta.ListarMeta((Usuario)Session["usuario"]);
        DdlMeta.DataTextField = "NombreMeta";
        DdlMeta.DataValueField = "IdMeta";
        DdlMeta.DataBind();
        DdlMeta.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    protected void LlenarPeriodicidad()
    {
        List<EjecutivoMeta> lstConsulta = new List<EjecutivoMeta>();

        EjecutivoMeta usuarioAse = new EjecutivoMeta();
        DdlVigencia.DataSource = serviceEjeMeta.ListarPeriodicidad((Usuario)Session["usuario"]);
        DdlVigencia.DataTextField = "DescripcionPeriodicidad";
        DdlVigencia.DataValueField = "IdPeriodicidad";
        DdlVigencia.DataBind();
        DdlVigencia.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }
    protected void DdlYear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}