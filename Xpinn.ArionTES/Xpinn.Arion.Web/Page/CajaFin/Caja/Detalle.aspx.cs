using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;

public partial class Detalle : GlobalWeb
{
    Xpinn.Caja.Services.CajaService cajaService = new Xpinn.Caja.Services.CajaService();
    Xpinn.Caja.Data.CajaData cajaData = new Xpinn.Caja.Data.CajaData();

    Xpinn.Caja.Services.TipoOperacionService tipoOpeServicio = new Xpinn.Caja.Services.TipoOperacionService();
    Xpinn.Caja.Entities.TipoOperacion tipoOperacion = new Xpinn.Caja.Entities.TipoOperacion();

    Xpinn.Caja.Services.TipoTopeService tipoTopeServicio = new Xpinn.Caja.Services.TipoTopeService();
    Xpinn.Caja.Entities.TipoTope tipoTope = new Xpinn.Caja.Entities.TipoTope();


    Xpinn.Caja.Services.OficinaService oficinaService = new Xpinn.Caja.Services.OficinaService();
    Xpinn.Caja.Data.OficinaData oficinaData = new Xpinn.Caja.Data.OficinaData();
    Usuario _usuario;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(cajaService.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajaService.GetType().Name + "A", "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["usuario"];

            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                ObtenerOficinaData();

                if (Session[cajaService.CodigoCaja + ".ids"] != null)
                {
                    idObjeto = Session[cajaService.CodigoCaja + ".ids"].ToString();
                    Session.Remove(cajaService.CodigoCaja + ".ids");
                    ObtenerDatos(idObjeto);
                    ActualizarTipoOperacion();
                    ActualizarTipoTope();
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajaService.GetType().Name + "A", "Page_Load", ex);
        }
    }


    protected void ObtenerOficinaData()
    {
        Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();

        oficina = oficinaService.ConsultarOficina(long.Parse(Session[oficinaService.CodigoOficina + ".id"].ToString()), _usuario);

        if (!string.IsNullOrEmpty(oficina.cod_oficina.ToString()))
            lblCodOficina.Text = oficina.cod_oficina.ToString();
        if (!string.IsNullOrEmpty(oficina.nombre.ToString()))
            lblOficina.Text = oficina.nombre.ToString();

    }


    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("../../CajaFin/Oficina/Detalle.aspx");
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            cajaService.EliminarCaja(Convert.ToInt64(idObjeto), _usuario);
            Navegar("../../CajaFin/Oficina/Detalle.aspx");
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajaService.CodigoCaja + "C", "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[cajaService.CodigoCaja + ".ids"] = idObjeto;
        Navegar(Pagina.Nuevo);
    }

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }

    private void ActualizarTipoOperacion()
    {
        try
        {
            List<Xpinn.Caja.Entities.TipoOperacion> lstConsulta = new List<Xpinn.Caja.Entities.TipoOperacion>();
            lstConsulta = tipoOpeServicio.ListarTipoTransaccion(_usuario);

            gvOperaciones.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvOperaciones.Visible = true;
                gvOperaciones.DataBind();
            }
            else
            {
                gvOperaciones.Visible = false;
            }

            Session.Add(tipoOpeServicio.GetType().Name + ".consulta", 1);

            //permite traer datos a la grilla de los tipos de operacion relacionados con la caja

            //se inserta las opciones de la grilla en TipoOperacion
            CheckBox chkOperacionPermitida;

            int tipoOper = 0;//captura el valor del codigo de Tipo de Operacion

            foreach (GridViewRow fila in gvOperaciones.Rows)
            {
                //se captura la opcion chequeda en el grid
                tipoOper = int.Parse(fila.Cells[0].Text);
                chkOperacionPermitida = (CheckBox)fila.FindControl("chkPermiso");
                tipoOperacion = tipoOpeServicio.ConsultarTipoOpeCaja(tipoOper, Convert.ToInt64(idObjeto), _usuario);

                if (tipoOperacion.conteo == 1)
                {
                    chkOperacionPermitida.Checked = true;
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tipoOpeServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private void ActualizarTipoTope()
    {
        try
        {
            List<Xpinn.Caja.Entities.TipoTope> lstConsulta = new List<Xpinn.Caja.Entities.TipoTope>();
            lstConsulta = tipoTopeServicio.ListarTipoTope(tipoTope, _usuario);

            gvTopes.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvTopes.Visible = true;
                gvTopes.DataBind();
            }
            else
            {
                gvTopes.Visible = false;
            }

            Session.Add(tipoTopeServicio.GetType().Name + ".consulta", 1);

            //permite traer datos a la grilla de los tipos de operacion relacionados con la caja

            //se inserta las opciones de la grilla en TipoOperacion
            TextBox txtMaximo;
            TextBox txtMinimo;

            int tipoTopes = 0;//captura el valor del codigo de Tipo de Operacion
            int moneda= 0;//captura el valor del codigo de Tipo de Operacion

            foreach (GridViewRow fila in gvTopes.Rows)
            {
                //se captura la opcion chequeda en el grid
                tipoTopes = int.Parse(fila.Cells[0].Text);
                moneda = int.Parse(fila.Cells[2].Text);
                txtMaximo = (TextBox)fila.FindControl("txtMaximo");
                txtMinimo = (TextBox)fila.FindControl("txtMinimo");
                tipoTope = tipoTopeServicio.ConsultarTipoTopeCaja(tipoTopes, moneda, Convert.ToInt64(idObjeto), _usuario);
                //asignar los valores en los textbox
                txtMaximo.Text = tipoTope.valor_maximo.ToString();
                txtMinimo.Text = tipoTope.valor_minimo.ToString();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tipoOpeServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Caja.Entities.Caja caja = new Xpinn.Caja.Entities.Caja();
            if (pIdObjeto != null)
                caja = cajaService.ConsultarCaja(Convert.ToInt64(pIdObjeto), (Usuario) Session["usuario"]);

            if (!string.IsNullOrEmpty(caja.cod_caja.ToString()))
                lblCodigo.Text = caja.cod_caja.ToString();
            if (!string.IsNullOrEmpty(caja.nombre))
                txtCaja.Text = caja.nombre.Trim().ToString();
            if (!string.IsNullOrEmpty(caja.fecha_creacion.ToString()))
                txtFechaCreacion.Text = caja.fecha_creacion.ToShortDateString();
            if (!string.IsNullOrEmpty(caja.estado.ToString()))
            {
                Radestado.SelectedValue = caja.estado.ToString();
            }
            if (!string.IsNullOrEmpty(caja.esprincipal.ToString()))
                radTipoCaja.SelectedValue = caja.esprincipal.ToString();

            if (!string.IsNullOrWhiteSpace(caja.cod_cuenta_contable))
            {
                txtCodCuenta.Text = caja.cod_cuenta_contable;
            }

            if (!string.IsNullOrWhiteSpace(caja.desc_cuenta_contable))
            {
                txtNomCuenta.Text = caja.desc_cuenta_contable;
            }

            if (!string.IsNullOrWhiteSpace(caja.cod_datafono))
            {
                txtNomCuenta.Text = caja.cod_datafono;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(oficinaService.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

}