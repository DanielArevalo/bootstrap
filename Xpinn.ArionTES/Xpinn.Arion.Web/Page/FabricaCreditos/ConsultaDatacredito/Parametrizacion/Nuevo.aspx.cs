using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using AjaxControlToolkit;


public partial class Nuevo : GlobalWeb
{
    Xpinn.FabricaCreditos.Services.PreAnalisisService preAnalisisServicio = new Xpinn.FabricaCreditos.Services.PreAnalisisService();
    string mostrarView = "";     
    string pIdObjetoGrid = "";

    private void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[preAnalisisServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(preAnalisisServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(preAnalisisServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(preAnalisisServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                if (Session[preAnalisisServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[preAnalisisServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(preAnalisisServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    toolBar.MostrarGuardar(true);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(preAnalisisServicio.GetType().Name + "A", "Page_Load", ex);
        }
    }

    private void Guardar()
    {

        Xpinn.FabricaCreditos.Entities.Parametrizar parametrizar = new Xpinn.FabricaCreditos.Entities.Parametrizar();
        ctrlViews();        
        switch (mostrarView)
        {
            case "P":
                try
                {
                    if (pIdObjetoGrid != "")
                        parametrizar = preAnalisisServicio.ConsultarPrograma(Convert.ToInt64(pIdObjetoGrid), (Usuario)Session["usuario"]);

                    parametrizar.aprueba = chklAprueba.Text.Trim();
                    if(txtMaximo.Text.Trim() != "") parametrizar.maximo = Convert.ToInt64(txtMaximo.Text.Trim().Replace(".",""));
                    if (txtMinimo.Text.Trim() != "") parametrizar.minimo = Convert.ToInt64(txtMinimo.Text.Trim().Replace(".", ""));
                    parametrizar.muestra = chklMuestra.Text.Trim();
                    if (txtMensaje.Text.Trim() != "") parametrizar.mensaje = txtMensaje.Text.Trim();

                    if (pIdObjetoGrid != "")
                    {
                        parametrizar.idp = Convert.ToInt64(pIdObjetoGrid);
                        parametrizar.UsuarioEdita = "Admin";  // Modificar por usuario en sesion
                        preAnalisisServicio.ModificarPrograma(parametrizar, (Usuario)Session["usuario"]);
                    }
                    else
                    {
                        parametrizar.UsuarioCrea = "Admin";  // Modificar por usuario en sesion
                        parametrizar = preAnalisisServicio.CrearPrograma(parametrizar, (Usuario)Session["usuario"]);
                        pIdObjetoGrid = parametrizar.idp.ToString();
                    }

                    Session[preAnalisisServicio.CodigoPrograma + ".id"] = "P" + pIdObjetoGrid;//"P" Indica que debe mostrar en el formulario "Detalle", el view donde estan los componentes de Parametría
                    Navegar(Pagina.Lista);
                }
                catch (ExceptionBusiness ex)
                {
                    VerError(ex.Message);
                }
                catch (Exception ex)
                {
                    BOexcepcion.Throw(preAnalisisServicio.GetType().Name + "A", "btnGuardar_Click", ex);
                }
                break;
            
            case "C":
                try
                {
                    if (pIdObjetoGrid != "")
                        parametrizar = preAnalisisServicio.ConsultarCentral(Convert.ToInt64(pIdObjetoGrid), (Usuario)Session["usuario"]);

                    //if (txtCentral.Text.Trim() != "") parametrizar.central = txtCentral.Text.Trim();
                    parametrizar.central = rblCentral.Text.Trim();
                    if (txtValor.Text.Trim() != "") parametrizar.valor = Convert.ToInt64(txtValor.Text.Trim().Replace(".",""));
                    parametrizar.cobra = rblCobra.Text.Trim();
                    if (txtPorcentaje.Text.Trim() != "") parametrizar.porcentaje = Convert.ToInt64(txtPorcentaje.Text.Trim());
                    //if (txtValorIva.Text.Trim() != "") parametrizar.valoriva = Convert.ToInt64(txtValorIva.Text.Trim());

                    if (pIdObjetoGrid != "")
                    {
                        parametrizar.idc = Convert.ToInt64(pIdObjetoGrid);
                        parametrizar.UsuarioEdita = "Admin";  // Modificar por usuario en sesion
                        preAnalisisServicio.ModificarCentral(parametrizar, (Usuario)Session["usuario"]);
                    }
                    else
                    {
                        parametrizar.UsuarioCrea = "Admin";  // Modificar por usuario en sesion
                        parametrizar = preAnalisisServicio.CrearCentral(parametrizar, (Usuario)Session["usuario"]);
                        pIdObjetoGrid = parametrizar.idc.ToString();
                    }

                    Session[preAnalisisServicio.CodigoPrograma + ".id"] = "C" + pIdObjetoGrid; //"C" Indica que debe mostrar en el formulario "Detalle", el view donde estan los componentes de Central
                    Navegar(Pagina.Lista);
                }
                catch (ExceptionBusiness ex)
                {
                    VerError(ex.Message);
                }
                catch (Exception ex)
                {
                    BOexcepcion.Throw(preAnalisisServicio.GetType().Name + "A", "btnGuardar_Click", ex);
                }
                break;

        }

        
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
            Navegar(Pagina.Lista);  
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        //Segun el primer caracter de pIdObjeto obtiene datos para Central o para Parametrizacion.

        string mostrarView = pIdObjeto.Substring(0, 1);     //Toma el primer caracter del parametro para saber cual view debe mostrar
        string pIdObjetoGrid = pIdObjeto.Substring(1, pIdObjeto.Length - 1);
        Xpinn.FabricaCreditos.Entities.Parametrizar parametrizar = new Xpinn.FabricaCreditos.Entities.Parametrizar();
        
        try
        {
            switch (mostrarView)
            {
                case "P":
                    parametrizar = preAnalisisServicio.ConsultarPrograma(Convert.ToInt64(pIdObjetoGrid), (Usuario)Session["usuario"]);
                    mvNuevo.ActiveViewIndex = 1;
                    if (!string.IsNullOrEmpty(Convert.ToString(parametrizar.minimo)))
                        txtMinimo.Text = HttpUtility.HtmlDecode(parametrizar.minimo.ToString());

                    if (!string.IsNullOrEmpty(Convert.ToString(parametrizar.maximo)))
                        txtMaximo.Text = HttpUtility.HtmlDecode(parametrizar.maximo.ToString());

                    if (!string.IsNullOrEmpty(parametrizar.aprueba))
                        chklAprueba.Text = HttpUtility.HtmlDecode(parametrizar.aprueba.Trim().ToString());

                    if (!string.IsNullOrEmpty(parametrizar.muestra))
                        chklMuestra.Text = HttpUtility.HtmlDecode(parametrizar.muestra.Trim().ToString());

                    if (!string.IsNullOrEmpty(parametrizar.mensaje))
                        txtMensaje.Text = HttpUtility.HtmlDecode(parametrizar.mensaje.Trim().ToString());
                    break;

                case "C":
                    parametrizar = preAnalisisServicio.ConsultarCentral(Convert.ToInt64(pIdObjetoGrid), (Usuario)Session["usuario"]);
                    mvNuevo.ActiveViewIndex = 2;
                    if (!string.IsNullOrEmpty(Convert.ToString(parametrizar.central)))
                        //txtCentral.Text = HttpUtility.HtmlDecode(parametrizar.central.ToString());
                        rblCentral.Text = HttpUtility.HtmlDecode(parametrizar.central.Trim().ToString());

                    if (!string.IsNullOrEmpty(Convert.ToString(parametrizar.valor)))
                        txtValor.Text = HttpUtility.HtmlDecode(parametrizar.valor.ToString());

                    if (!string.IsNullOrEmpty(parametrizar.cobra))
                        rblCobra.Text = HttpUtility.HtmlDecode(parametrizar.cobra.Trim().ToString());

                    if (!string.IsNullOrEmpty(Convert.ToString(parametrizar.porcentaje)))
                        txtPorcentaje.Text = HttpUtility.HtmlDecode(parametrizar.porcentaje.ToString());

                    //if (!string.IsNullOrEmpty(Convert.ToString(parametrizar.valoriva)))
                    //    txtValorIva.Text = HttpUtility.HtmlDecode(parametrizar.valoriva.ToString());
                    break;
            }
            //VerAuditoria(parametrizar.UsuarioCrea = "Edit", parametrizar.FechaCrea, parametrizar.UsuarioEdita = "Edit", parametrizar.FechaEdita);
        } 
        catch (Exception ex)
        {
            BOexcepcion.Throw(preAnalisisServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

    protected void btnAceptar_Click(object sender, ImageClickEventArgs e)
    {
        Site toolbar = (Site)Master;
        toolbar.MostrarGuardar(true);
        switch (rblParametria.SelectedItem.Value)
        {
            case "Parámetro":
                idObjeto = "P" + idObjeto;
                ctrlViews();
                break;
            case "Central":
                idObjeto = "C" + idObjeto;
                ctrlViews();
                break;
        }
       
        
    }


    private void ctrlViews()
    {
        try
        {
            if (idObjeto.Length >= 1)    //Impide guardar desde el rbl que selecciona entre parametro y central
            {
                mostrarView = idObjeto.Substring(0, 1);     //Toma el primer caracter del parametro para saber cual view debe mostrar
                pIdObjetoGrid = "";

               if (idObjeto.Length >= 2) //Si el objeto trae el prefijo P o C, y el id de la fila
                    pIdObjetoGrid = idObjeto.Substring(1, idObjeto.Length - 1);//Toma el id SIN el prefijo
                
                switch (mostrarView)
                {
                    case "P":
                        mvNuevo.ActiveViewIndex = 1;                        
                        rfvMensaje.Enabled = true;
                        break;
                    case "C":
                        mvNuevo.ActiveViewIndex = 2;                        
                        rfvMensaje.Enabled = false;
                        break;
                }
                return;
            }
                
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(preAnalisisServicio.GetType().Name + "A", "CtrlViews", ex);
        }
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        Guardar();
    }
   
}