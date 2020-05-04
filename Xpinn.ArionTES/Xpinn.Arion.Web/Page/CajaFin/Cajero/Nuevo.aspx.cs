using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;

public partial class Nuevo : GlobalWeb
{
    Xpinn.Caja.Data.CajeroData cajeroData = new Xpinn.Caja.Data.CajeroData();
    Xpinn.Caja.Services.CajeroService cajeroService = new Xpinn.Caja.Services.CajeroService();
    Xpinn.Caja.Services.OficinaService oficinaService = new Xpinn.Caja.Services.OficinaService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[cajeroService.CodigoCajero + ".id"] != null)
                VisualizarOpciones(cajeroService.CodigoPrograma,"E");
            else
                VisualizarOpciones(cajeroService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajeroService.GetType().Name + "A", "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //se inicializa el combo de Usuarios, Cajas
                LlenarComboUsuarios(ddlUsuarios);
                LlenarComboCajas(ddlCajas);
                chkEstado.Enabled = false;
                ObtenerDatos("");
                
                chkEstado.Checked = true;
                ddlUsuarios.Enabled = true;
                DateTime fecha = DateTime.Now;
                txtFechaIngreso.Text = Convert.ToString(fecha.ToShortDateString());

                if (Session[cajeroService.CodigoCajero + ".id"] != null)
                {
                    idObjeto = Session[cajeroService.CodigoCajero + ".id"].ToString();
                    
                    Session.Remove(cajeroService.CodigoCajero + ".id");
                    ObtenerDatos(idObjeto);
                    chkEstado.Enabled = true;
                    ddlUsuarios.Enabled = false;
                }
                else
                    idObjeto = "";
                
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajeroService.GetType().Name + "A", "Page_Load", ex);
        }
    }

    protected void LlenarComboUsuarios(DropDownList ddlUsuarios)
    {
        Xpinn.Caja.Services.UsuariosService usuariosServicio = new Xpinn.Caja.Services.UsuariosService();
        Xpinn.Caja.Entities.Usuarios usuarioE = new Xpinn.Caja.Entities.Usuarios();
        usuarioE.cod_oficina = long.Parse(Session[oficinaService.CodigoOficina + ".IdO"].ToString());
        
        if (Session[cajeroService.CodigoCajero + ".id"] != null)
            ddlUsuarios.DataSource = usuariosServicio.ListarUsuariosXOficina2(usuarioE, (Usuario)Session["usuario"]);
        else
            ddlUsuarios.DataSource = usuariosServicio.ListarUsuariosXOficina(usuarioE, (Usuario)Session["usuario"]);
        
        ddlUsuarios.DataTextField = "nombre";
        ddlUsuarios.DataValueField = "codusuario";
        ddlUsuarios.DataBind();
    }

    protected void LlenarComboCajas(DropDownList ddlCajas)
    {
        Xpinn.Caja.Services.CajaService cajaServicio = new Xpinn.Caja.Services.CajaService();
        Xpinn.Caja.Entities.Caja caja = new Xpinn.Caja.Entities.Caja();
        caja.cod_oficina = long.Parse(Session[oficinaService.CodigoOficina + ".IdO"].ToString());
        ddlCajas.DataSource = cajaServicio.ListarComboCajaXOficina(caja, (Usuario)Session["usuario"]);
        ddlCajas.DataTextField = "nombre";
        ddlCajas.DataValueField = "cod_caja";
        ddlCajas.DataBind();
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
      Navegar(Pagina.Lista);
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        Usuario usuari = (Usuario)Session["usuario"];

        if (long.Parse(Session[oficinaService.CodigoOficina + ".IdO"].ToString()) == usuari.cod_oficina)
        {
            if (ddlUsuarios.Items.Count > 0 && ddlCajas.Items.Count > 0)
            {
                try
                {
                    Xpinn.Caja.Entities.Cajero cajero = new Xpinn.Caja.Entities.Cajero();

                    long conteoCajero = 0;

                    if (idObjeto == "")
                    {
                        cajero = cajeroService.ConsultarCajeroRelCaja(long.Parse(ddlUsuarios.SelectedValue), (Usuario)Session["usuario"]);
                        conteoCajero = cajero.conteo;
                    }

                    if (idObjeto != "")
                        cajero = cajeroService.ConsultarCajero(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

                    //se atrapan los datos del formulario
                    cajero.cod_cajero = lblCodigo.Text;
                    cajero.cod_persona = long.Parse(ddlUsuarios.SelectedValue);
                    cajero.cod_caja = long.Parse(ddlCajas.SelectedValue);
                    cajero.fecha_ingreso = Convert.ToDateTime(txtFechaIngreso.Text);

                    if ((chkEstado.Checked == true) && (txtFechaRetiro.Text != ""))
                    {
                        txtFechaRetiro.Text = "01/01/0001";
                        cajero.fecha_retiro = Convert.ToDateTime(txtFechaRetiro.Text);
                    }

                    long checkEstado = 0;

                    if (chkEstado.Checked)
                        checkEstado = 1;
                    else
                        cajero.fecha_retiro = DateTime.Now;

                    cajero.estado = checkEstado;

                    cajero.identificacion = Convert.ToString(txtIdentificacion.Text);

                    if (idObjeto != "")
                    {
                        cajero.IdCajero = long.Parse(idObjeto);
                        cajeroService.ModificarCajero(cajero, (Usuario)Session["usuario"]);
                        Session[cajeroService.CodigoCajero + ".id"] = cajero.cod_cajero;
                        Navegar(Pagina.Detalle);
                    }
                    else
                    {
                        if (conteoCajero == 0)//si el usuario no existe como cajero se ingresa
                        {
                            cajero = cajeroService.CrearCajero(cajero, (Usuario)Session["usuario"]);
                            idObjeto = cajero.cod_cajero.ToString();
                            Session[cajeroService.CodigoCajero + ".id"] = cajero.cod_cajero;
                            Navegar(Pagina.Detalle);
                        }
                        else
                            VerError("El Usuario ya esta asociado a una Caja.");
                    }

                }
                catch (ExceptionBusiness ex)
                {
                    VerError(ex.Message);
                }
                catch (Exception ex)
                {
                    BOexcepcion.Throw(cajeroService.GetType().Name + "A", "btnGuardar_Click", ex);
                }
            }
            else
                VerError("El Combo de Usuarios no cuenta con Usuarios asignados a esta oficina");
        }
        else
            VerError("El Usuario no puede ejecutar esta accion pq no pertenece a la oficina Seleccionada");

        
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Caja.Entities.Cajero cajero = new Xpinn.Caja.Entities.Cajero();
            if (pIdObjeto != "")
                cajero = cajeroService.ConsultarCajero(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (cajero.cod_cajero != "")
            {
                if (!string.IsNullOrEmpty(cajero.cod_cajero))
                    lblCodigo.Text = cajero.cod_cajero.Trim();
            }
            else
                lblCodigo.Text = "";

            if (!string.IsNullOrEmpty(cajero.cod_persona.ToString()))
                ddlUsuarios.SelectedValue = cajero.cod_persona.ToString();
            if (!string.IsNullOrEmpty(cajero.cod_caja.ToString()))
                ddlCajas.SelectedValue = cajero.cod_caja.ToString();

            if (cajero.fecha_ingreso.ToShortDateString() != "01/01/0001")
            {
                txtFechaIngreso.Text = cajero.fecha_ingreso.ToShortDateString();              
            }
            else
            { 
                txtFechaIngreso.Text = DateTime.Now.ToShortDateString();            
            }

            if (txtFechaIngreso.Text== "1/01/0001" ||  (txtFechaIngreso.Text == "01/01/0001"))
            {
                txtFechaIngreso.Enabled = true;
            }
            else
            {
                txtFechaIngreso.Enabled = false;
            }


            if (cajero.fecha_retiro.ToShortDateString() != "01/01/0001")
                txtFechaRetiro.Text = cajero.fecha_retiro.ToShortDateString();
            else
                txtFechaRetiro.Text = "";

            if (!string.IsNullOrEmpty(cajero.estado.ToString()))
            {
                if (cajero.estado == 1)
                    chkEstado.Checked = true;
                else
                    chkEstado.Checked = false;
            }

            if (cajero.identificacion != null)
            {
                txtIdentificacion.Text = cajero.identificacion.ToString();
            }


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajeroService.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

}