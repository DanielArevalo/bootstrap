using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Web.UI.HtmlControls;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;

public partial class Nuevo : GlobalWeb
{
    private Xpinn.Caja.Services.CajaService cajaService = new Xpinn.Caja.Services.CajaService();
    private Xpinn.Caja.Data.CajaData cajaData = new Xpinn.Caja.Data.CajaData();

    private Xpinn.Caja.Services.TipoOperacionService tipoOpeServicio = new Xpinn.Caja.Services.TipoOperacionService();
    private Xpinn.Caja.Entities.TipoOperacion tipoOperacion = new Xpinn.Caja.Entities.TipoOperacion();

    private Xpinn.Caja.Services.TipoTopeService tipoTopeServicio = new Xpinn.Caja.Services.TipoTopeService();
    Xpinn.Caja.Entities.TipoTope tipoTope = new Xpinn.Caja.Entities.TipoTope();

    Xpinn.Caja.Services.OficinaService oficinaService = new Xpinn.Caja.Services.OficinaService();
    Xpinn.Caja.Data.OficinaData oficinaData = new Xpinn.Caja.Data.OficinaData();
    Usuario _usuario;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[cajaService.CodigoCaja + ".ids"] != null)
                VisualizarOpciones(cajaService.CodigoPrograma, "E");
            else
                VisualizarOpciones(cajaService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
         

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
              
                ObtenerOficinaData();
             
                if (Request.UrlReferrer.Segments[4].ToString() == "Oficina/")
                    Session[cajaService.CodigoPrograma + ".ORIGEN"] = Request.UrlReferrer;
                if (Session[cajaService.CodigoCaja + ".ids"] != null)
                {
                    idObjeto = Session[cajaService.CodigoCaja + ".ids"].ToString();
                    Session.Remove(cajaService.CodigoCaja + ".ids");
                    ObtenerDatos(idObjeto);
                    ActualizarTipoOperacion();
                    ActualizarTipoTope();
                  
                }
                else
                {
                    ObtenerDatos(null);
                    radTipoCaja.SelectedValue = "1";
                    ActualizarTipoOperacionList();
                    ActualizarTipoTopeList();
                    DateTime fecha = DateTime.Now;
                    txtFechaCreacion.Text = Convert.ToString(fecha.ToShortDateString());

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

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Caja.Entities.Caja caja = new Xpinn.Caja.Entities.Caja();
            if (pIdObjeto != null)
                caja = cajaService.ConsultarCaja(Convert.ToInt64(pIdObjeto), _usuario);

            if (pIdObjeto != null)
            {
                if (!string.IsNullOrEmpty(caja.cod_caja.ToString()))
                    lblCodigo.Text = caja.cod_caja.ToString();
                if (!string.IsNullOrEmpty(caja.nombre))
                    txtCaja.Text = caja.nombre.Trim().ToString();

                if (!string.IsNullOrWhiteSpace(caja.cod_cuenta_contable))
                {
                    txtCodCuenta.Text = caja.cod_cuenta_contable;
                }

                if (!string.IsNullOrWhiteSpace(caja.desc_cuenta_contable))
                {
                    txtNomCuenta.Text = caja.desc_cuenta_contable;
                }
                if (!string.IsNullOrEmpty(caja.cod_datafono))
                    txtDatafono.Text = caja.cod_datafono.Trim().ToString();
            }

            if (caja.fecha_creacion.ToShortDateString() != "01/01/0001")
                txtFechaCreacion.Text = caja.fecha_creacion.ToShortDateString();
            else
                txtFechaCreacion.Text = DateTime.Now.ToShortDateString();


            if(txtFechaCreacion.Text == "1/01/0001" || (txtFechaCreacion.Text == "01/01/0001"))
            {
                txtFechaCreacion.Enabled = true;
            }
            else
            {
                txtFechaCreacion.Enabled = false;
            }




            if (pIdObjeto != null)
            {
                if (!string.IsNullOrEmpty(caja.estado.ToString()))
                {
                    Radestado.SelectedValue = caja.estado.ToString();

                }

                if (!string.IsNullOrEmpty(caja.esprincipal.ToString()))
                    radTipoCaja.SelectedValue = caja.esprincipal.ToString();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(oficinaService.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

    private void ActualizarTipoOperacionList() 
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

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tipoOpeServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private void ActualizarTipoTopeList()
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

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tipoTopeServicio.GetType().Name + "L", "Actualizar", ex);
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
            //TextBox txtMaxomo;
            decimales txtMinimo;
            decimales txtMaximo;

            int tipoTopes = 0;//captura el valor del codigo de Tipo de Operacion
            int moneda = 0;//captura el valor del codigo de Tipo de Operacion

            foreach (GridViewRow fila in gvTopes.Rows)
            {
                //se captura la opcion chequeda en el grid
                tipoTopes = int.Parse(fila.Cells[0].Text);
                moneda = int.Parse(fila.Cells[2].Text);
                txtMaximo = (decimales)fila.FindControl("txtMaximo");
                txtMinimo = (decimales)fila.FindControl("txtMinimo");
                tipoTope = tipoTopeServicio.ConsultarTipoTopeCaja(tipoTopes, moneda, Convert.ToInt64(idObjeto), _usuario);
                //asignar los valores en los textbox
                txtMaximo.Text = tipoTope.valor_maximo.ToString();
                txtMinimo.Text = tipoTope.valor_minimo.ToString();

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tipoTopeServicio.GetType().Name + "L", "Actualizar", ex);
        }
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

            if (idObjeto != null)
            {
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
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tipoOpeServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }
  
    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        if (Session[oficinaService.CodigoOficina + ".id"] != null)
        {
            Navegar("../../CajaFin/Oficina/Detalle.aspx");
        }
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Xpinn.Caja.Entities.Caja caja = new Xpinn.Caja.Entities.Caja();
            long idoficina = long.Parse(Session[oficinaService.CodigoOficina+".id"].ToString());
            caja = cajaService.ConsultarCajaPrincipal(idoficina, _usuario);
            long esprincip = long.Parse(caja.escajaprincip.ToString());
            long codcajaprincipal = caja.cod_caja_principal;
         
            if (idObjeto != "")
                caja = cajaService.ConsultarCaja(Convert.ToInt64(idObjeto), _usuario);

            //se atrapan los datos del formulario
            caja.cod_caja = lblCodigo.Text.Trim();
            caja.nombre = txtCaja.Text.Trim();

            caja.estado = long.Parse(Radestado.SelectedValue);
            caja.fecha_creacion = Convert.ToDateTime(txtFechaCreacion.Text);
            caja.esprincipal = long.Parse(radTipoCaja.SelectedValue);
            caja.cod_oficina = long.Parse(Session[oficinaService.CodigoOficina + ".id"].ToString());
            caja.cod_cuenta_contable = txtCodCuenta.Text;
            caja.cod_datafono = txtDatafono.Text;
            TextBox txtMinimotex;
            TextBox txtMaximotex;
            decimales txtMinimo;
            decimales txtMaximo;
            foreach (GridViewRow fila in gvTopes.Rows)
            {
                txtMaximo = (decimales)fila.FindControl("txtMaximo");
                txtMinimo = (decimales)fila.FindControl("txtMinimo");
                txtMaximotex = (TextBox)fila.FindControl("txtMaximotex");
                txtMinimotex = (TextBox)fila.FindControl("txtMinimotex");

                txtMinimotex.Text = Convert.ToDecimal(txtMinimo.Text.Replace(".", "")).ToString();
                txtMaximotex.Text = Convert.ToDecimal(txtMaximo.Text.Replace(".", "")).ToString();
                
                caja.valor_maximo =  Convert.ToDecimal(txtMaximo.Text.Replace(".", ""));
                caja.valor_minimo = Convert.ToDecimal(txtMinimo.Text.Replace(".", ""));                
            }


            //esprincip-> Conteo de Caja Principal
            if (esprincip == 1 && caja.esprincipal == 1)
            {   //se comparan las cajas para ver si coinciden como principal
                if (idObjeto =="") idObjeto = "0";

                if (long.Parse(idObjeto) == codcajaprincipal)
                {
                    caja.cod_caja = idObjeto;
                    cajaService.ModificarCaja(caja, gvTopes, gvOperaciones, _usuario);
                    Session[cajaService.CodigoCaja + ".ids"] = idObjeto;
                    if (Session[cajaService.CodigoPrograma + ".ORIGEN"] != null)
                        Navegar("../../CajaFin/Oficina/Detalle.aspx");
                    else
                        Navegar(Pagina.Detalle);
                }
                else
                {
                    VerError("La Oficina ya tiene una Caja Principal Existente.");
                    if (idObjeto == "0")  idObjeto = "";
                }                   
            }
            else
            {
                if (idObjeto != "")
                {
                    caja.cod_caja = idObjeto;
                    cajaService.ModificarCaja(caja, gvTopes, gvOperaciones, _usuario);
                }
                else
                {
                    caja = cajaService.CrearCaja(caja, gvTopes, gvOperaciones, _usuario);
                    idObjeto = caja.cod_caja.ToString();
                }

                Session[cajaService.CodigoCaja + ".ids"] = idObjeto;
                if (Session[cajaService.CodigoPrograma + ".ORIGEN"] != null)
                    Navegar("../../CajaFin/Oficina/Detalle.aspx");
                else
                    Navegar(Pagina.Detalle);
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajaService.GetType().Name + "A", "btnGuardar_Click", ex);
        }
    }

    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        ctlListadoPlan.Motrar(true, "txtCodCuenta", "txtNomCuenta");
    }

    protected void txtCodCuenta_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtCodCuenta.Text))
        {
            // Determinar los datos de la cuenta contable
            Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
            Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
            PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta.Text, _usuario);

            // Mostrar el nombre de la cuenta            
            if (PlanCuentas != null)
                txtNomCuenta.Text = PlanCuentas.nombre;
        }
        else
        {
            txtNomCuenta.Text = string.Empty;
        }
    }


    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

}