using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class Detalle : GlobalWeb
{
    Xpinn.FabricaCreditos.Services.PreAnalisisService preAnalisisServicio = new Xpinn.FabricaCreditos.Services.PreAnalisisService();

    private void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(preAnalisisServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(preAnalisisServicio.CodigoPrograma + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[preAnalisisServicio.CodigoPrograma + ".id"] != null)
                {

                    idObjeto = Session[preAnalisisServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(preAnalisisServicio.CodigoPrograma + ".id");

                    string mostrarView = idObjeto.Substring(0, 1);     //Toma el primer caracter del parametro para saber cual view debe mostrar
                    string pIdObjetoGrid = idObjeto.Substring(1, idObjeto.Length - 1);

                    switch (mostrarView)
                    {
                        case "P":
                            mvDetalle.ActiveViewIndex = 0;
                            break;
                        case "C":
                            mvDetalle.ActiveViewIndex = 1;
                            break;
                    }

                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(preAnalisisServicio.GetType().Name + "D", "Page_Load", ex);
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string mostrarView = idObjeto.Substring(0, 1);     //Toma el primer caracter del parametro para saber cual view debe mostrar
            string pIdObjetoGrid = idObjeto.Substring(1, idObjeto.Length - 1);

            switch (mostrarView)
            {
                case "P":
                        preAnalisisServicio.EliminarPrograma(Convert.ToInt64(pIdObjetoGrid), (Usuario)Session["usuario"]);
                        break;                    
                case "C":
                        preAnalisisServicio.EliminarCentral(Convert.ToInt64(pIdObjetoGrid), (Usuario)Session["usuario"]);
                        break;                    
            }
            Navegar(Pagina.Lista);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(preAnalisisServicio.CodigoPrograma + "C", "btnEliminar_Click", ex);
        }

    }


    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[preAnalisisServicio.CodigoPrograma + ".id"] = idObjeto;
        Navegar(Pagina.Nuevo);
    }

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            string mostrarView = pIdObjeto.Substring(0, 1);     //Toma el primer caracter del parametro para saber cual view debe mostrar
            string pIdObjetoGrid = pIdObjeto.Substring(1, pIdObjeto.Length-1);
            Xpinn.FabricaCreditos.Entities.Parametrizar parametrizar = new Xpinn.FabricaCreditos.Entities.Parametrizar();
            
            switch (mostrarView)
            {
                case "P":
                        parametrizar = preAnalisisServicio.ConsultarPrograma(Convert.ToInt64(pIdObjetoGrid), (Usuario)Session["usuario"]);
                        mvDetalle.ActiveViewIndex = 0;
                        lblMinimo.Text = parametrizar.minimo.ToString("n0");
                        lblMaximo.Text = parametrizar.maximo.ToString("n0");
                        if (!string.IsNullOrEmpty(parametrizar.aprueba))
                            chklAprueba.SelectedValue = parametrizar.aprueba.Trim().ToString();
                        if (!string.IsNullOrEmpty(parametrizar.muestra))
                            chklMuestra.SelectedValue = parametrizar.muestra.Trim().ToString();
                        lblMensaje.Text = parametrizar.mensaje.ToString();
                        break;

                case "C":
                        parametrizar = preAnalisisServicio.ConsultarCentral(Convert.ToInt64(pIdObjetoGrid), (Usuario)Session["usuario"]);
                        mvDetalle.ActiveViewIndex = 1;
                        if (!string.IsNullOrEmpty(parametrizar.central))
                            rblCentral.SelectedValue = parametrizar.cobra.Trim().ToString();                        
                        lblValor.Text = parametrizar.valor.ToString("n0");
                        if (!string.IsNullOrEmpty(parametrizar.cobra))
                            rblCobra.SelectedValue = parametrizar.cobra.Trim().ToString();
                        lblPorcentaje.Text = parametrizar.porcentaje.ToString();
                        lblValoriva.Text = parametrizar.valoriva.ToString("n0");                        
                    break;
            }
            //VerAuditoria(parametrizar.UsuarioCrea = "Admin", parametrizar.FechaCrea, parametrizar.UsuarioEdita, parametrizar.FechaEdita);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(preAnalisisServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
        
    }  
}