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
using Xpinn.Comun.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Services;

partial class Nuevo : GlobalWeb
{
    private static string NAME_CACHE = "EstadoCuenta";
    private Xpinn.Comun.Services.GDocumentalService GDocumentalService = new Xpinn.Comun.Services.GDocumentalService();
    private EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[GDocumentalService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(GDocumentalService.CodigoPrograma, "E");
            else
                VisualizarOpciones(GDocumentalService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GDocumentalService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ObtenerDatos();
                Xpinn.Comun.Entities.GestionDocumental vGestionDocumental = new Xpinn.Comun.Entities.GestionDocumental();
                List<Xpinn.Comun.Entities.GestionDocumental> lstGd = new List<Xpinn.Comun.Entities.GestionDocumental>();                
                lstGd = GDocumentalService.ListarGDocumental(vGestionDocumental, (Usuario)Session["usuario"]);

                int padre = 0;
                foreach (Xpinn.Comun.Entities.GestionDocumental  row in lstGd)
                {
                    TreeNode nuevoNodo = new TreeNode();
                    nuevoNodo.Text = row.Descripcion.ToString().Trim();
                    TreeNode nodePadre = null;
                    
                    // si el parámetro nodoPadre es nulo es porque es la primera llamada, son los Nodos
                    // del primer nivel que no dependen de otro nodo.
                    if (nodePadre == null)
                    {
                        TreeView1.Nodes.Add(nuevoNodo);

                        foreach (Xpinn.Comun.Entities.GestionDocumental row23 in lstGd)
                        {

                            TreeNode nodo = new TreeNode();
                            nodo.Text = "Archivo Adjunto No " + padre;
                            TreeView1.Nodes[padre].ChildNodes.Add(nodo);
                        }
                        padre++;
                    }
                   
                }


            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GDocumentalService.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.ActivosFijos.Entities.TipoArticulo vTipoArticulo = new Xpinn.ActivosFijos.Entities.TipoArticulo();

            if (idObjeto != "")
                //vTipoArticulo = GDocumentalService.ConsultarTipoArticulo(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            //vTipoArticulo.IdTipo_Articulo   = Convert.ToInt32(txtCodigo.Text.Trim());
            //vTipoArticulo.Descripcion   = Convert.ToString(txtDescripcion.Text.Trim());
            //vTipoArticulo.Dias_Periodicidad   = Convert.ToInt32(txtDias.Text.Trim());

            if (idObjeto != "")
            {
                vTipoArticulo.IdTipo_Articulo   = Convert.ToInt32(idObjeto);
                //GDocumentalService.ModificarTipoArticulo(vTipoArticulo, (Usuario)Session["usuario"]);
            }
            else
            {
                //vTipoArticulo = GDocumentalService.CrearTipoArticulo (vTipoArticulo, (Usuario)Session["usuario"]);
                idObjeto = vTipoArticulo.IdTipo_Articulo  .ToString();
            }

            Session[GDocumentalService.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GDocumentalService.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos()
    {
        try
        {
            Producto producto = new Producto();
            if (Session[MOV_GRAL_CRED_PRODUC] != null)
            {
               
                //*******************************************************************************************************//
                // Traer la información del estado de cuenta (datos de persona y sus productos)
                //*******************************************************************************************************//
                producto = (Producto)(Session[MOV_GRAL_CRED_PRODUC]);

                String nameCache = NAME_CACHE + producto.Persona.IdPersona.ToString();
                object cacheValue = System.Web.HttpRuntime.Cache.Get(nameCache);
                DateTime timeExpiration = DateTime.Now.AddSeconds(60);

                if (cacheValue == null)
                {
                    producto.Persona = serviceEstadoCuenta.ConsultarPersona(producto.Persona.IdPersona, (Usuario)Session["usuario"]);
                    System.Web.HttpRuntime.Cache.Add(nameCache, producto, null, timeExpiration, TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
                }
                else
                {
                    producto = (Producto)System.Web.HttpRuntime.Cache.Get(nameCache);
                    Session[MOV_GRAL_CRED_PRODUC] = producto;
                }

                //*******************************************************************************************************//
                // Mostrar la información del estado de cuenta
                //*******************************************************************************************************//
                try
                {
                     txtCodigo.Text = HttpUtility.HtmlDecode(producto.Persona.IdPersona.ToString());
                    if (!string.IsNullOrEmpty(producto.Persona.PrimerApellido)) txtPrimerNombre.Text = HttpUtility.HtmlDecode(producto.Persona.PrimerNombre.Trim().ToString());
                    if (!string.IsNullOrEmpty(producto.Persona.PrimerNombre)) txtPrimerApellido.Text = HttpUtility.HtmlDecode(producto.Persona.PrimerApellido.Trim().ToString());
                    if (!producto.Persona.NumeroDocumento.Equals(0)) txtNumDoc.Text = HttpUtility.HtmlDecode(producto.Persona.NumeroDocumento.ToString());
                    if (!string.IsNullOrWhiteSpace(producto.Persona.TipoIdentificacion.NombreTipoIdentificacion)) txtTipoDoc.Text = HttpUtility.HtmlDecode(producto.Persona.TipoIdentificacion.NombreTipoIdentificacion.Trim().ToString());
                    if (!string.IsNullOrEmpty(producto.Persona.Direccion)) txtDireccion.Text = HttpUtility.HtmlDecode(producto.Persona.Direccion);
                    //if (!string.IsNullOrEmpty(producto.Persona.Email)) txtEmail.Text = HttpUtility.HtmlDecode(producto.Persona.Email);
                 
                    if (!string.IsNullOrEmpty(producto.Persona.TipoCliente)) txtTipoCliente.Text = HttpUtility.HtmlDecode(producto.Persona.TipoCliente);
                    //if (!string.IsNullOrEmpty(producto.Persona.Oficina.NombreOficina)) txtOficina.Text = HttpUtility.HtmlDecode(producto.Persona.Oficina.NombreOficina);
                    //if (!string.IsNullOrEmpty(producto.Persona.Ciudad.nomciudad)) txtCiudad.Text = HttpUtility.HtmlDecode(producto.Persona.Ciudad.nomciudad);
                    //if (!string.IsNullOrEmpty(producto.Persona.Asesor.PrimerNombre)) txtEjecutivo.Text = HttpUtility.HtmlDecode(producto.Persona.Asesor.PrimerNombre);
                  
                    string cadena = "", newPagaduria = "";
                    
                    if (cadena != "")
                        newPagaduria = cadena.Substring(0, cadena.Length - 3);

                   
                }
                catch (Exception ex)
                {
                    BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "A", "ObtenerDatos", ex);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GDocumentalService.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


}