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
using Xpinn.CDATS.Services;
using Xpinn.CDATS.Entities;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;


partial class Nuevo : GlobalWeb
{
    LiquidacionCDATService LiquiService = new LiquidacionCDATService();
    AperturaCDATService AperturaService = new AperturaCDATService();
    PoblarListas Poblar = new PoblarListas();
    AdministracionCDATService objtadministracion = new AdministracionCDATService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[objtadministracion.CodigoProgramaCDAT + ".id"] != null)
                VisualizarOpciones(objtadministracion.CodigoProgramaCDAT, "E");
            else
                VisualizarOpciones(objtadministracion.CodigoProgramaCDAT, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarImprimir(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objtadministracion.CodigoProgramaCDAT, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session["DatosDetalle"] = null;           
            if (!Page.IsPostBack)
            {
                mvPrincipal.ActiveViewIndex = 0;
                cargarDropdown();

                Usuario vUsu = (Usuario)Session["usuario"];
                ddlOficina.SelectedValue = vUsu.cod_oficina.ToString();
                txtFechaCierre.Texto = DateTime.Now.ToShortDateString();

                if (Session[objtadministracion.CodigoProgramaCDAT + ".id"] != null)
                {
                    idObjeto = Session[objtadministracion.CodigoProgramaCDAT + ".id"].ToString();
                    Session.Remove(objtadministracion.CodigoProgramaCDAT + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objtadministracion.CodigoProgramaCDAT, "Page_Load", ex);
        }
    }



    void cargarDropdown()
    { 
        Cdat Data = new Cdat();
        List<Cdat> lstTipoLinea = new List<Cdat>();

        lstTipoLinea = AperturaService.ListarTipoLineaCDAT(Data,(Usuario)Session["usuario"]);
        if (lstTipoLinea.Count > 0)
        {
            ddlTipoLinea.DataSource = lstTipoLinea;
            ddlTipoLinea.DataTextField = "NOMBRE";
            ddlTipoLinea.DataValueField = "COD_LINEACDAT";
            ddlTipoLinea.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlTipoLinea.SelectedIndex = 0;
            ddlTipoLinea.DataBind();
        }
        
        Poblar.PoblarListaDesplegable("Tipomoneda", ddlTipoMoneda,(Usuario)Session["usuario"]);

        ddlTipoCalendario.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlTipoCalendario.Items.Insert(1, new ListItem("Comercial", "1"));
        ddlTipoCalendario.Items.Insert(2, new ListItem("Calendario", "2"));
        ddlTipoCalendario.SelectedIndex = 0;
        ddlTipoCalendario.DataBind();

        Poblar.PoblarListaDesplegable("OFICINA","COD_OFICINA,NOMBRE","ESTADO = 1","1",ddlOficina,(Usuario)Session["usuario"]);        
        Poblar.PoblarListaDesplegable("PERIODICIDAD", "COD_PERIODICIDAD,DESCRIPCION", "", "to_number(cod_periodicidad)", ddlPeriodicidad, (Usuario)Session["usuario"]);

        ctlTasa.Inicializar();
    }


    protected void chkPrincipal_CheckedChanged(object sender, EventArgs e)
    {
        CheckBoxGrid chkPrincipal = (CheckBoxGrid)sender;
        int rowIndex = Convert.ToInt32(chkPrincipal.CommandArgument);

        if (chkPrincipal != null)
        {
            foreach (GridViewRow rFila in gvDetalle.Rows)
            {
                CheckBoxGrid check = (CheckBoxGrid)rFila.FindControl("chkPrincipal");
                check.Checked = false;
                if (rFila.RowIndex == rowIndex)
                {
                    check.Checked = true;
                }
            }
        }
    }


    protected void gvDetalle_PageIndexChanging(object sender, GridViewPageEventArgs e)     
    {     
            gvDetalle.PageIndex = e.NewPageIndex;     
            ObtenerDatos(idObjeto);     
    }

    //protected List<Detalle_CDAT> ObtenerListaDetalle()
    //{
    //    List<Detalle_CDAT> lstDetalle = new List<Detalle_CDAT>();
    //    List<Detalle_CDAT> lista = new List<Detalle_CDAT>();

    //    foreach (GridViewRow rfila in gvDetalle.Rows)
    //    {
    //        Detalle_CDAT eDeta = new Detalle_CDAT();

    //        string pCodigo = rfila.Cells[0].Text;
    //        if (pCodigo != null && pCodigo != "&nbsp;")
    //            eDeta.cod_usuario_cdat = Convert.ToInt64(pCodigo);

    //        string pIdentificacion = rfila.Cells[1].Text;
    //        if (pIdentificacion != null && pIdentificacion != "&nbnsp;")
    //            eDeta.identificacion = pIdentificacion;

    //        string pCod_persona = rfila.Cells[2].Text;
    //        if (pCod_persona != null && pCod_persona != "&nbsp;")
    //            eDeta.cod_persona = Convert.ToInt64(pCod_persona);

    //        CheckBoxGrid chkPrincipal = (CheckBoxGrid)rfila.FindControl("chkPrincipal");
    //        if (chkPrincipal.Checked)
    //            eDeta.principal = 1;
    //        else
    //            eDeta.principal = 0;

    //        lista.Add(eDeta);
    //        Session["DatosDetalle"] = lista;

    //        if (eDeta.cod_persona != 0 && eDeta.cod_persona != null)
    //        {
    //            lstDetalle.Add(eDeta);
    //        }
    //    }

    //    return lstDetalle;
    //}


  
    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            AdministracionCDAT vApe = objtadministracion.getcdatByIdBusiness(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            
            //vApe = AperturaService.ConsultarApertu(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            txtEstado.Text = vApe.estado == 2 ? txtEstado.Text = "activo" : txtEstado.Text = "Bloqueado";

            if (vApe.codigo_cdat != 0) txtCodigo.Text = vApe.codigo_cdat.ToString();

            if (vApe.numero_cdat != "")
            {
                txtNumCDAT.Text = vApe.numero_cdat;
                txtDigVerif.Text = CalcularDigitoVerificacion(txtNumCDAT.Text);
            }

            if (vApe.fecha_apertura != DateTime.MinValue) txtFechaApertura.Text = vApe.fecha_apertura.ToShortDateString();

            if (vApe.cod_lineacdat != null) ddlTipoLinea.SelectedValue = vApe.cod_lineacdat;

            if (vApe.cod_oficina != 0) ddlOficina.SelectedValue = vApe.cod_oficina.ToString();

            if (vApe.valor != 0) txtValor.Text = vApe.valor.ToString();

            if (vApe.cod_moneda != 0) ddlTipoMoneda.SelectedValue = vApe.cod_moneda.ToString();

            if (vApe.plazo != 0) txtPlazo.Text = vApe.plazo.ToString();

            if (vApe.tipo_calendario != 0) ddlTipoCalendario.SelectedValue = vApe.tipo_calendario.ToString();


            //RECUPERAR GRILLA DETALLE 
            List<Detalle_CDAT> lstDetalle = new List<Detalle_CDAT>();

            lstDetalle = AperturaService.ListarDetalleTitulares(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (lstDetalle.Count > 0)
            {
                gvDetalle.Visible = true;
                gvDetalle.DataSource = lstDetalle;
                gvDetalle.DataBind();
            }
            else 
            {
                gvDetalle.Visible = false;
                gvDetalle.DataSource = null;
            }

            if (vApe.tipo_interes != null)
            {
                ctlTasa.FormaTasa = vApe.tipo_interes;
                if (ctlTasa.Indice == 0)//NIGUNA
                {
                }
                else if (ctlTasa.Indice == 1)//FIJO
                {
                    if (vApe.tasa_interes != 0)
                        ctlTasa.Tasa = vApe.tasa_interes;
                    if (vApe.cod_tipo_tasa != 0)
                        ctlTasa.TipoTasa = vApe.cod_tipo_tasa;
                }
                else // HISTORICO
                {
                    if (vApe.tipo_historico != 0)
                        ctlTasa.TipoHistorico = vApe.tipo_historico;
                    if (vApe.desviacion != 0)
                        ctlTasa.Desviacion = vApe.desviacion;
                }
            }

            if (vApe.modalidad_int != 0)
                rblModalidadInt.SelectedValue = vApe.modalidad_int.ToString();

           
            if (vApe.cod_periodicidad_int != 0)
                ddlPeriodicidad.SelectedValue = vApe.cod_periodicidad_int.ToString();
            else
                lblperiodicidad.Visible = false;
                ddlPeriodicidad.Visible = false;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objtadministracion.CodigoProgramaCDAT, "ObtenerDatos", ex);
        }
    }
    
  ////  protected void txtFechaCierre_TextChanged(object sender, EventArgs e)
  //  {
  //      try
  //      {
  //      }
  //      catch (Exception ex)
  //      {
  //          BOexcepcion.Throw("Cierre", "txtFechaCierre_TextChanged", ex);
  //      }
  //  }

    
    Boolean ValidarDatos()
    {
        if (txtFechaCierre.Texto == "")
        {
            VerError("Ingrese la fecha de Cierre");
            return false;
        }

        if (txtNumCDAT.Visible == true)
        {
            if (txtNumCDAT.Text == "")
            {
                VerError("Ingrese el numero de CDAT");
                return false;
            }
        }
        if (txtPlazo.Text == "")
        {
            VerError("Ingrese el Plazo correspondiente");
            return false;
        }
        if (ddlTipoCalendario.SelectedIndex == 0)
        {
            VerError("Seleccione el Tipo de Calendario");
            return false;
        }
        if (ddlOficina.SelectedIndex == 0)
        {
            VerError("Seleccione la Oficina perteneciente al Asesor");
            return false;
        }
        if (ddlPeriodicidad.SelectedIndex == 0)
        {
            VerError("Seleccione la Periodicidad correspondiente");
            return false;
        }

        List<Detalle_CDAT> LstDetalle = new List<Detalle_CDAT>();
        //LstDetalle = ObtenerListaDetalle();
        int cont = 0;
        
        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
        DateTime dtUltCierre;
        try
        {
            dtUltCierre = Convert.ToDateTime(ComprobanteServicio.Consultafecha((Usuario)Session["Usuario"]));
        }
        catch
        {
            VerError("No se encontro la fecha del último cierre contable");
            return false;
        }

        if (Convert.ToDateTime(txtFechaCierre.Texto) <= dtUltCierre)
        {
            VerError("La fecha de Cierre ingresada debe ser mayor a la fecha del último Cierre generado ('"+ dtUltCierre.ToShortDateString() +"')");
            return false;
        }

        //Validando datos del control de Giro

        Int64 COD = Buscar_Titular();
        if (COD == 0)
        {
            VerError("Error al realizar la búsqueda, No se ubico al titular");
            return false;
        }
        return true;
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            if (isValido())
            {
                ctlMensaje.MostrarMensaje("Desea Cambiar el estado?");
            }
        }       
        catch (Exception ex)
        {
            BOexcepcion.Throw(objtadministracion.CodigoProgramaCDAT, "btnGuardar_Click", ex);
        }
    }

    protected Int64 Buscar_Titular()
    {
        Int64 codigo = 0;
        int cont = 0;
        foreach (GridViewRow rFila in gvDetalle.Rows)
        {
            CheckBoxGrid chkPrincipal = (CheckBoxGrid)rFila.FindControl("chkPrincipal");
            if (chkPrincipal.Checked)
                cont++;

            if (cont == 1)
            {
                string cod = "";
                cod = rFila.Cells[2].Text.Replace("&nbsp;","");
                if (cod != "")
                    codigo = Convert.ToInt64(cod);
                break;
            }
        }
        return codigo;
    }

    bool isValido() 
    {
        if (txtFechaCierre.Texto.Trim()=="")
        {
            VerError("Selecciones la fecha ");
            return false;
        }
        if (txtObservacion.Text.Trim()=="")
        {
            VerError("Ingrese una Observacion ");
            return false;
        }
        return true;
    }

    Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
    Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["usuario"];

                NovedadCDAT novedad = new NovedadCDAT();
                novedad.CODIGO_CDAT = Convert.ToInt32(idObjeto);
                novedad.TIPO_NOVEDAD = 102;
                novedad.FECHA_NOVEDAD = Convert.ToDateTime(txtFechaCierre.Texto);
                novedad.OBSERVACIONES = txtObservacion.Text;
                novedad.COD_USUARIO = pUsuario.codusuario;
                novedad.FECHACREA = DateTime.Now;
                int resultado = 0;

               if(txtEstado.Text == "activo")
               {
                novedad.EstadoActual = 2;
                novedad.EstadoSiguiente = 5;
               }
                if (txtEstado.Text == "Bloqueado")
                {
                    novedad.EstadoActual = 5;
                    novedad.EstadoSiguiente = 2;
                }



            resultado = objtadministracion.InsertNovedadAndEstadoCdatSerrvices(novedad, pUsuario, Convert.ToInt16(idObjeto));

                if (resultado != null)
                { 

                }

                switch (resultado)
                {
                    case 5:
                        VerError("CDAT Cambio de estado Activo A " + "Bloqueado");
                        break;
                    case 2:
                        VerError("CDAT Cambio de estado Bloqueado A " + "Activo");
                        break;
                    default:
                        VerError("error verifique los datos o intente mas tarde");
                        break;
                       
                }
                ObtenerDatos(idObjeto);
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarImprimir(false);

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objtadministracion.CodigoProgramaCDAT, "btnContinuarMen_Click", ex);
        }    
    }
    
    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

   
}