using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;
using Xpinn.Util;


public partial class Nuevo : GlobalWeb
{

    EntregaChequesServices entregaServicio = new EntregaChequesServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(entregaServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlValidarBiometria.eventoGuardar += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(entregaServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvAplicar.ActiveViewIndex = 0;
                if (Session[entregaServicio.CodigoPrograma + ".id"] != null)
                {
                    txtFecha.ToDateTime = System.DateTime.Now;
                    idObjeto = Session[entregaServicio.CodigoPrograma + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(entregaServicio.GetType().Name + "L", "Page_Load", ex);
        }
    }



    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            List<EntregaCheques> lstDeta = new List<EntregaCheques>();
            EntregaCheques entregaChe = new EntregaCheques();
            if (pIdObjeto != null)
                entregaChe.cod_benef = Convert.ToInt64(pIdObjeto);
            entregaChe.estado_cheque = 1;
            lstDeta = entregaServicio.ListarEntregaCheques(entregaChe, (Usuario)Session["usuario"]);

            if (lstDeta.Count > 0)
            {
                if (lstDeta[0].cod_benef != 0)
                    txtcodPersona.Text = lstDeta[0].cod_benef.ToString();
                if (lstDeta[0].identificacion != "")
                    txtIdentificacion.Text = lstDeta[0].identificacion;
                if (lstDeta[0].nombre != "")
                    txtNombre.Text = lstDeta[0].nombre.Trim();
                gvDetalle.DataSource = lstDeta;
                gvDetalle.DataBind();
                sumaDeSaldo();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(entregaServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected Boolean ValidarDatos()
    {
        if (txtFecha.Text == "")
        {
            VerError("Ingrese la fecha de legalización");
            return false;
        }

        if (gvDetalle.Rows.Count > 0)
        {
            int val = 0;
            foreach (GridViewRow rFila in gvDetalle.Rows)
            {
                CheckBoxGrid chkTraslador = (CheckBoxGrid)rFila.FindControl("chkTraslador");
                if (chkTraslador.Checked == true)
                    val = 1;
            }
            if (val == 0)
            {
                VerError("Seleccione el registro del giro a legalizar");
                return false;
            }
        }
        else
        {
            VerError("No Existen datos de giros a legalizar, No puede realizar la Grabación");
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");

        if (ValidarDatos())
        {
            ctlMensaje.MostrarMensaje("Desea grabar la legalización de giros?");
        }
    }

    Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
    Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        String codigoPrograma = "";
        string sError = "";
        VerError("");
        try
        {
            codigoPrograma = entregaServicio.CodigoPrograma;
            if (ctlValidarBiometria.IniciarValidacion(Convert.ToInt32(codigoPrograma), "999", Convert.ToInt64(txtcodPersona.Text), txtFecha.ToDateTime, ref sError))
            {
                VerError(sError);
                return;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(entregaServicio.CodigoPrograma, "btnContinuar_Click", ex);
        }
        try
        {
            Usuario usuario = new Usuario();
            usuario = (Usuario)Session["Usuario"];
            foreach (GridViewRow rFila in gvDetalle.Rows)
            {
                CheckBoxGrid chkTraslador = (CheckBoxGrid)rFila.FindControl("chkTraslador");
                if (chkTraslador.Checked)
                {
                    // Actualizar el estado del cheque
                    EntregaCheques entregaChe = new EntregaCheques();
                    entregaChe.identrega = 0;
                    entregaChe.fecha = Convert.ToDateTime(rFila.Cells[1].Text);
                    entregaChe.num_comp = Convert.ToInt64(rFila.Cells[2].Text);
                    try
                    {
                        entregaChe.tipo_comp = Convert.ToInt32(rFila.Cells[3].Text);
                    }
                    catch
                    {
                        entregaChe.tipo_comp = null;
                    }
                    entregaChe.idgiro = null;
                    entregaChe.num_cheque = Convert.ToString(rFila.Cells[6].Text);
                    entregaChe.entidad = Convert.ToInt32(rFila.Cells[4].Text);
                    entregaChe.cod_persona = Convert.ToInt64(txtcodPersona.Text);
                    entregaChe.valor = Convert.ToDecimal(rFila.Cells[8].Text.Replace("$", "").Replace(gSeparadorMiles, ""));
                    entregaChe.idautorizacion = null;
                    entregaChe.fecha_entrega = txtFecha.ToDateTime;
                    entregaChe.cod_usuario = usuario.codusuario;
                    entregaServicio.CrearEntregaCheque(entregaChe, usuario);
                }
            }

            mvAplicar.ActiveViewIndex = 1;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(entregaServicio.CodigoPrograma, "btnContinuar_Click", ex);
        }
    }


    private void sumaDeSaldo()
    {
        try
        {
            decimal SumSaldo = 0;
            if (gvDetalle.Rows.Count > 0)
            {
                foreach (GridViewRow rFila in gvDetalle.Rows)
                {
                    CheckBox chkTraslador = (CheckBox)rFila.FindControl("chkTraslador");
                    if (chkTraslador != null)
                        if (chkTraslador.Checked)
                            SumSaldo = SumSaldo + Convert.ToDecimal(rFila.Cells[8].Text.Replace("$", "").Replace(",",""));
                }
            }
            txtValorAaplicar.Text = SumSaldo.ToString("n");
        }
        catch
        {}
    }


    protected void gvDetalle_RowEditing(object sender, GridViewEditEventArgs e)
    {
        return;
    }

    protected void chkTraslador_CheckedChanged(object sender, EventArgs e)
    {
        sumaDeSaldo();
    }

    protected void gvDetalle_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetalle.PageIndex = e.NewPageIndex;
        ObtenerDatos(idObjeto);
    }
}
