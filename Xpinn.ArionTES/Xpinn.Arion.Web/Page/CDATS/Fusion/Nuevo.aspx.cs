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
using Xpinn.CDATS.Entities;
using Xpinn.CDATS.Services;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using System.IO;

public partial class  Nuevo : GlobalWeb
{
   
         AperturaCDATService AperturaService = new AperturaCDATService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AperturaService.codigoprogramafusioncdats, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.codigoprogramafusioncdats, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
               txtFecha.ToDateTime = DateTime.Now;
               cargarDropdown();
               Tasa11.Inicializar();
               sumacdats();
               Site toolBar = (Site)this.Master;
               toolBar.MostrarGuardar(false);
              
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.codigoprogramafusioncdats, "Page_Load", ex);
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        if (txtidentificacion.Text == "")
        {
            VerError("Seleccione una identificación");
            return;
        }
        if (txtFecha.Text == "")
        {
            VerError("Seleccione una Fecha");
            return;
        }
        if (txtnombre.Text == "")
        {
            VerError("Seleccione un Nombre");
            return;
        }
        if (txtCodigo.Text == "")
        {
            VerError("Seleccione un número de Cuenta");
            return;
        }
            Actualizar();
        
    }






    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "  ";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }




    void cargarDropdown()
    {
        Cdat Data = new Cdat();
        List<Cdat> lstTipoLinea = new List<Cdat>();

        lstTipoLinea = AperturaService.ListarTipoLineaCDAT(Data, (Usuario)Session["usuario"]);
        if (lstTipoLinea.Count > 0)
        {
            ddlModalidad.DataSource = lstTipoLinea;
            ddlModalidad.DataTextField = "NOMBRE";
            ddlModalidad.DataValueField = "COD_LINEACDAT";
            ddlModalidad.Items.Insert(0, new ListItem("  ", "0"));
            ddlModalidad.DataBind();

        }


        ddlModalidads.Items.Insert(0, new ListItem("INDIVIDUAL", "IND"));
        ddlModalidads.Items.Insert(1, new ListItem("CONJUNTA", "CON"));
        ddlModalidads.Items.Insert(2, new ListItem("ALTERNA", "ALT"));
        ddlModalidads.SelectedIndex = 0;
        ddlModalidads.DataBind();

        PoblarLista("DESTINACION_CDAT", ddlDestinacions);
        PoblarLista("Formacaptacion_CDAT", ddlFormaCaptacions);

        lstTipoLinea = AperturaService.ListarTipoLineaCDAT(Data, (Usuario)Session["usuario"]);
        if (lstTipoLinea.Count > 0)
        {
            DropDownList11.DataSource = lstTipoLinea;
            DropDownList11.DataTextField = "NOMBRE";
            DropDownList11.DataValueField = "COD_LINEACDAT";
            DropDownList11.Items.Insert(0, new ListItem("  ", "0"));
            DropDownList11.DataBind();

        }


        List<Cdat> lstUsuarios = new List<Cdat>();
        Cdat Data2 = new Cdat();

        lstUsuarios = AperturaService.ListarUsuariosAsesores(Data2, (Usuario)Session["usuario"]);
        if (lstUsuarios.Count > 0)
        {
            ddlAsesors.DataSource = lstUsuarios;
            ddlAsesors.DataTextField = "nombre";
            ddlAsesors.DataValueField = "cod_oficina";
            ddlAsesors.SelectedIndex = 0;
            ddlAsesors.DataBind();
        }

        //PoblarLista("TIPOTASA", ddlFormaCaptacion); //PRUEBA

        PoblarLista("Tipomoneda", DropDownList22);




        DropDownList33.Items.Insert(0, new ListItem("  ", "0"));
        DropDownList33.Items.Insert(1, new ListItem("Comercial", "1"));
        DropDownList33.Items.Insert(2, new ListItem("Calendario", "2"));
        DropDownList33.DataBind();

        //PoblarLista("TIPOTASA", ddlDestinacion); //PRUEBA


        List<Cdat> lstAsesores = new List<Cdat>();



        List<Cdat> lstOficina = new List<Cdat>();


        lstOficina = AperturaService.ListarOficinas(Data, (Usuario)Session["usuario"]);
        if (lstOficina.Count > 0)
        {
            ddlOficina.DataSource = lstOficina;
            ddlOficina.DataTextField = "nombre";
            ddlOficina.DataValueField = "COD_OFICINA";
            ddlOficina.Items.Insert(0, new ListItem("  ", "0"));

            ddlOficina.DataBind();
        }

        if (lstOficina.Count > 0)
        {
            DropDownList44.DataSource = lstOficina;
            DropDownList44.DataTextField = "nombre";
            DropDownList44.DataValueField = "COD_OFICINA";
            DropDownList44.Items.Insert(0, new ListItem("  ", "0"));

            DropDownList44.DataBind();
        }



        PoblarLista("Periodicidad", DropDownList55);


    }




    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.codigoprogramafusioncdats, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[AperturaService.CodigoPrograma + ".id"] = id;
        Session["ADMI"] = "";
        Session["RETURNO"] = "";
        mvPrincipal.ActiveViewIndex = 1;
    }


    protected void gvDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlConjuncion = (DropDownListGrid)e.Row.FindControl("ddlConjuncion");
            if (ddlConjuncion != null)
            {
                ddlConjuncion.Items.Insert(0, new ListItem(" ", "0"));
                ddlConjuncion.Items.Insert(1, new ListItem("Y", "Y"));
                ddlConjuncion.Items.Insert(2, new ListItem("O", "O"));
            }

            Label lblConjuncion = (Label)e.Row.FindControl("lblConjuncion");
            if (lblConjuncion != null)
                ddlConjuncion.SelectedValue = lblConjuncion.Text;
        }
    }

    protected void gvDetalle_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0].ToString());

        //ObtenerValores();

        List<Detalle_CDAT> LstDeta;
        LstDeta = (List<Detalle_CDAT>)Session["DatosDetalle"];

        if (conseID > 0)
        {
            try
            {
                foreach (Detalle_CDAT Deta in LstDeta)
                {

                    AperturaService.EliminarTitularCdat(conseID, (Usuario)Session["usuario"]);
                    LstDeta.Remove(Deta);
                    break;

                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
        }
        else
        {
            LstDeta.RemoveAt((GridView1.PageIndex * GridView1.PageSize) + e.RowIndex);
        }

        GridView1.DataSourceID = null;
        GridView1.DataBind();

        GridView1.DataSource = LstDeta;
        GridView1.DataBind();

        GridView1.DataSourceID = null;
        GridView1.DataBind();

        GridView1.DataSource = LstDeta;
        GridView1.DataBind();

        Session["DatosDetalle"] = LstDeta;
    }



    protected List<Detalle_CDAT> ObtenerListaDetalle()
    {
        List<Detalle_CDAT> lstDetalle = new List<Detalle_CDAT>();
        List<Detalle_CDAT> lista = new List<Detalle_CDAT>();

        foreach (GridViewRow rfila in gvLista.Rows)
        {
            Detalle_CDAT eDeta = new Detalle_CDAT();

            Label lblcodigo = (Label)rfila.FindControl("lblcodigo");
            if (lblcodigo != null)
                eDeta.codigo_cdat = Convert.ToInt64(lblcodigo.Text);

            Label lblidentificacion = (Label)rfila.FindControl("lblidentificacion");
            if (lblidentificacion != null)
                eDeta.identificacion = Convert.ToString(lblidentificacion.Text);

            TextBoxGrid txtIdentificacion = (TextBoxGrid)rfila.FindControl("txtIdentificacion");
            if (txtIdentificacion != null)
                eDeta.identificacion = txtIdentificacion.Text;

            Label lblcod_persona = (Label)rfila.FindControl("lblcod_persona");
         

            Label lblNombre = (Label)rfila.FindControl("lblNombre");
          

            Label lblApellidos = (Label)rfila.FindControl("lblApellidos");
        

            Label lblCiudad = (Label)rfila.FindControl("lblCiudad");
        

            Label lblDireccion = (Label)rfila.FindControl("lblDireccion");
           

            Label lbltelefono = (Label)rfila.FindControl("lbltelefono");
          

            CheckBoxGrid chkPrincipal = (CheckBoxGrid)rfila.FindControl("chkPrincipal");
            




            lista.Add(eDeta);
            Session["DatosDetalle"] = lista;

            if (eDeta.cod_persona != 0 && eDeta.cod_persona != null)
            {
                lstDetalle.Add(eDeta);
                Session["DTAPERTURA"] = lstDetalle; // CAPTURA DATOS PARA IMPRESION
            }
        }

        return lstDetalle;
    }
      


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
        Session["ID"] = id;
        ctlMensaje.MostrarMensaje("Desea realizar la eliminación?");
    }

    Boolean ValidarDatos()
    {
        if (txtCodigo.Visible == true)
        {
            if (txtCodigo.Text == "")
            {
                VerError("Ingrese el numero de CDAT");
                return false;
            }
        }

        if (Fecha2.Text == "")
        {
            VerError("Ingrese la Fecha de Apertura");
            return false;
        }
        if (txtFecha.Text == "")
        {
            VerError("Ingrese la Fecha de Vencimiento");
            return false;
        }

        if (txtValor.Text == "0")
        {
            VerError("Ingrese el Valor");
            return false;
        }
        if (DropDownList22.SelectedIndex == 0)
        {
            VerError("Seleccione el Tipo de Moneda");
            return false;
        }
        if (TextBox33.Text == "")
        {
            VerError("Ingrese el Plazo correspondiente");
            return false;
        }
        if (DropDownList33.SelectedIndex == 0)
        {
            VerError("Seleccione el Tipo de Calendario");
            return false;
        }

        if (ddlOficina.SelectedIndex == 0)
        {
            VerError("Seleccione la Oficina perteneciente al Asesor");
            return false;
        }


        if (DropDownList55.SelectedIndex == 0)
        {
            VerError("Seleccione la Periodicidad correspondiente");
            return false;
        }

        List<Detalle_CDAT> LstDetalle = new List<Detalle_CDAT>();
        LstDetalle = ObtenerListaDetalle();

      


        int plazo;

        plazo = Convert.ToInt32(TextBox33.Text);
        if (plazo == null)
        {
            VerError("El Plazo ingresado no es valido para la Periodicidad Seleccionada");
            return false;
        }

        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            VerError("");
            foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBox check = (CheckBox)rFila.FindControl("check");
            if (check.Checked != false)
            {
                if (ValidarDatos())
                {
                    string msj;

                    if (mvPrincipal.ActiveViewIndex == 0)
                    {
                        mvPrincipal.ActiveViewIndex = 1;
                    }

                    else
                    {

                        msj = "Guardar los datos de ";
                        ctlMensaje.MostrarMensaje("Desea " + msj + "Esta pantalla");
                    }
                }
            }
            }

        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.codigoprogramafusioncdats, "btnGuardar_Click", ex);
        }
    }


    protected void sumacdats()
    {
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBox check = (CheckBox)rFila.FindControl("check");
            if (check != null)
            {   
            txtValor.Text += rFila.Cells[6].Text;
            }
        }

    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {

            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox check = (CheckBox)rFila.FindControl("check");
                if (check != null)
                {
                    if (check.Checked == true)
                    {

                        Usuario vUsuario = new Usuario();
                        vUsuario = (Usuario)Session["Usuario"];
                        Xpinn.Tesoreria.Entities.Operacion operacion = new Xpinn.Tesoreria.Entities.Operacion();
                        operacion.cod_ope = 0;
                        operacion.tipo_ope = 57;
                        operacion.cod_caja = 0;
                        operacion.cod_cajero = 0;
                        operacion.observacion = "operacion realizada";
                        operacion.cod_proceso = null;
                        operacion.fecha_oper = Convert.ToDateTime(txtFecha.Text);
                        operacion.fecha_calc = DateTime.Now;
                        operacion.cod_ofi = vUsuario.cod_oficina;



                        // Datos del cierre del CDAT a renovar
                        AdministracionCDAT traslado_cuenta = new AdministracionCDAT();
                        traslado_cuenta.valor = Convert.ToDecimal(txtValor.Text);
                        traslado_cuenta.numero_cdat = Convert.ToString(txtCodigo.Text);
                        traslado_cuenta.cod_oficina = Convert.ToInt32(ddlOficina.Text);
                        traslado_cuenta.identificacion = Convert.ToString(txtidentificacion.Text);
                        traslado_cuenta.nombres = Convert.ToString(txtnombre.Text);
                        traslado_cuenta.cod_asesor_com = Convert.ToInt32(ddlAsesors.Text);
                        traslado_cuenta.fecha_vencimiento = Convert.ToDateTime(txtFecha.ToDate);

                        
                        
                        // Datos de apertura del CDAT a renovar
                        Cdat vApert = new Cdat();
                        if (idObjeto != "")
                            vApert.codigo_cdat = Convert.ToInt64(txtCodigo.Text);
                        else
                            vApert.codigo_cdat = 0;


                        if (txtCodigo.Visible == true)
                            vApert.numero_cdat = txtCodigo.Text;
                        else
                            vApert.numero_cdat = null;
                        vApert.cod_oficina = Convert.ToInt32(ddlOficina.SelectedValue);
                        if (ddlModalidad.SelectedIndex != 0)
                            vApert.cod_lineacdat = ddlModalidad.SelectedValue;
                        else
                        vApert.cod_lineacdat = null;
                        vApert.fecha_apertura = Convert.ToDateTime(Fecha2.Text);
                        vApert.plazo = Convert.ToInt32(TextBox33.Text);
                        vApert.tipo_calendario = Convert.ToInt32(DropDownList33.SelectedValue);
                        vApert.valor = Convert.ToDecimal(txtValor.Text);
                        vApert.cod_moneda = Convert.ToInt32(DropDownList22.SelectedValue);
                        vApert.fecha_vencimiento = Convert.ToDateTime(txtFecha.Text);
                        vApert.cod_periodicidad_int = Convert.ToInt32(DropDownList55.SelectedValue);
                        if (rblModalidadInts.SelectedItem != null)
                            vApert.modalidad_int = Convert.ToInt32(rblModalidadInts.SelectedValue);
                        else
                            vApert.modalidad_int = 0;
                        vApert.tasa_nominal = 0;
                        vApert.tasa_efectiva = 0;
                        vApert.intereses_cap = 0;
                        vApert.retencion_cap = 0;
                        vApert.tipo_interes = Tasa11.FormaTasa;
                        vApert.tasa_interes = Convert.ToDecimal(Tasa11.TipoTasa);
                        vApert.tipo_historico = Tasa11.TipoHistorico;
                        vApert.desviacion = Convert.ToInt32(Tasa11.Desviacion);
                        vApert.cod_tipo_tasa = Tasa11.TipoTasa;
                        vApert.modalidad = "0";
                        vApert.fecha_intereses = DateTime.MinValue;
                        vApert.numero_fisico = txtNumPreImpresos.Text;
                        vApert.numero_cdat = "0";
                        vApert.estado = 1; // por defecto en estado de APERTURA   
                        vApert.lstDetalle = new List<Detalle_CDAT>();
                        vApert.lstDetalle = ObtenerListaDetalle();

                        AperturaService.RenovacionCdat(vApert, traslado_cuenta, operacion, (Usuario)Session["usuario"]);

                        Site toolBar = (Site)Master;
                        toolBar.MostrarGuardar(false);
                        toolBar.MostrarImprimir(false);
                        toolBar.MostrarGuardar(true);

                        mvPrincipal.ActiveViewIndex = 1;
                    }
                }
            }  
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.codigoprogramafusioncdats, "btnContinuarMen_Click", ex);
        }    
    }

    
    private void Actualizar()
    {
        try
        {
            List<Cdat> lstConsulta = new List<Cdat>();
            String filtro = obtFiltro(ObtenerValores());
            DateTime FechaApe;

            FechaApe = txtFecha.ToDateTime == null ? DateTime.MinValue : txtFecha.ToDateTime;

            lstConsulta = AperturaService.Listardatos(filtro, FechaApe, (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;           

            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;                
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                ObtenerDatos(idObjeto);
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(true);
                
            }
            else
            {
                panelGrilla.Visible = false;                
                lblTotalRegs.Visible = false;
              
            }

            Session.Add(AperturaService.codigoprogramafusioncdats + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.codigoprogramafusioncdats, "Actualizar", ex);
        }
    }




    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {

            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox check = (CheckBox)rFila.FindControl("check");
                if (check != null)
                {
                    if (check.Checked == true)
                    {

                        Cdat vApe = new Cdat();

                        vApe = AperturaService.ConsultarApertura((Usuario)Session["usuario"]);



                        if (vApe.codigo_cdat != 0) txtCodigo.Text = rFila.Cells[1].Text;

                        if (vApe.numero_cdat != "") txtCodigo.Text = rFila.Cells[1].Text;

                        Session["nroCDAT"] = vApe.numero_cdat;



                        if (vApe.cod_oficina != 0) ddlOficina.SelectedValue = vApe.cod_oficina.ToString();

                        if (vApe.cod_lineacdat != "") ddlModalidad.SelectedValue = vApe.cod_lineacdat;



                        if (vApe.fecha_apertura != DateTime.MinValue) txtFecha.Text = vApe.fecha_apertura.ToShortDateString();



                        if (vApe.plazo != 0) TextBox33.Text = vApe.plazo.ToString();

                        if (vApe.tipo_calendario != 0) DropDownList33.SelectedValue = vApe.tipo_calendario.ToString();

                        if (vApe.cod_moneda != 0) DropDownList22.SelectedValue = vApe.cod_moneda.ToString();


                        if (vApe.tipo_interes != null)
                        {
                            Tasa11.FormaTasa = vApe.tipo_interes;
                            if (Tasa11.Indice == 0)//NIGUNA
                            {
                            }
                            else if (Tasa11.Indice == 1)//FIJO
                            {
                                if (vApe.tasa_interes != 0)
                                    Tasa11.Tasa = vApe.tasa_interes;
                                if (vApe.cod_tipo_tasa != 0)
                                    Tasa11.TipoTasa = vApe.cod_tipo_tasa;
                            }
                            else // HISTORICO
                            {
                                if (vApe.tipo_historico != 0)
                                    Tasa11.TipoHistorico = Convert.ToInt32(vApe.tipo_historico);
                                if (vApe.desviacion != 0)
                                    Tasa11.Desviacion = Convert.ToDecimal(vApe.desviacion);
                            }
                        }

                        if (vApe.codigo_cdat != 0) txtCodigo.Text = rFila.Cells[1].Text;
                        if (vApe.codigo_cdat != 0) TextBox22.Text = rFila.Cells[1].Text;

                        if (vApe.codigo_cdat != 0) txtNumPreImpresos.Text = vApe.codigo_cdat.ToString();
                        if (vApe.codigo_cdat != 0) TextBox11.Text = vApe.codigo_cdat.ToString();

                        if (vApe.numero_cdat != "") txtCodigo.Text = rFila.Cells[1].Text;
                        if (vApe.numero_cdat != "") TextBox11.Text = rFila.Cells[1].Text;

            Session["nroCDAT"] = vApe.numero_cdat;



            if (vApe.cod_oficina != 0) ddlOficina.SelectedValue = vApe.cod_oficina.ToString();
            if (vApe.cod_oficina != 0) DropDownList44.SelectedValue = vApe.cod_oficina.ToString();

            if (vApe.cod_lineacdat != "") ddlModalidad.SelectedValue = vApe.cod_lineacdat;
            if (vApe.cod_lineacdat != "") DropDownList11.SelectedValue = vApe.cod_lineacdat;




            if (vApe.fecha_apertura != DateTime.MinValue) txtFecha.Text = vApe.fecha_apertura.ToShortDateString();
            if (vApe.fecha_apertura != DateTime.MinValue) Fecha2.Text = vApe.fecha_apertura.ToShortDateString();




            if (vApe.plazo != 0) TextBox33.Text = vApe.plazo.ToString();
            if (vApe.plazo != 0) TextBox33.Text = vApe.plazo.ToString();

            if (vApe.tipo_calendario != 0) DropDownList33.SelectedValue = vApe.tipo_calendario.ToString();
            DropDownList33.Text = vApe.tipo_calendario.ToString();
            DropDownList33.Text = vApe.tipo_calendario.ToString();



            if (vApe.cod_moneda != 0) DropDownList22.SelectedValue = vApe.cod_moneda.ToString();
            if (vApe.cod_moneda != 0) DropDownList22.SelectedValue = vApe.cod_moneda.ToString();


            //fecha vencimiento no lo recupero
            if (vApe.fecha_vencimiento != DateTime.MinValue) txtFecha.Text = vApe.fecha_vencimiento.ToShortDateString();
            if (vApe.fecha_apertura != DateTime.MinValue) txtFechaEmis.Text = vApe.fecha_apertura.ToShortDateString();




            if (vApe.cod_periodicidad_int != 0)
                DropDownList55.SelectedValue = vApe.cod_periodicidad_int.ToString();
            DropDownList55.SelectedValue = vApe.cod_periodicidad_int.ToString();
            if (vApe.modalidad_int != 0)
                rblModalidadInts.SelectedValue = vApe.modalidad_int.ToString();



            /*Datos que no recupero
             * Tasa nominal,TASA_EFECTIVA,INTERESES_CAP,RETENCION_CAP,FECHA_INTERESES,ESTADO

             * */
            AdministracionCDAT Liquidacion = new AdministracionCDAT();
            sumacdats();
            Liquidacion.fecha_apertura = Convert.ToDateTime(Fecha2.ToDate);
            Liquidacion.numero_cdat = Convert.ToString(TextBox11.Text);
            Liquidacion.valor = Convert.ToDecimal(txtValor.Text);
           


            List<Detalle_CDAT> lstInsertar = new List<Detalle_CDAT>();
            lstInsertar = AperturaService.Liquidar(Liquidacion, (Usuario)Session["usuario"]);



            //RECUPERAR GRILLA DETALLE 
            List<Detalle_CDAT> lstDetalle = new List<Detalle_CDAT>();
            
            lstDetalle = AperturaService.ListarDetalle((Usuario)Session["usuario"]);

            if (lstDetalle.Count > 0)
            {
                
                GridView1.DataSource = lstDetalle;
                GridView1.DataBind();
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(true);
            }
            else
            {
                InicializarDetalle();
            }
        }
      }
    }
  }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.codigoprogramafusioncdats, "ObtenerDatos", ex);
        }
    }
         

    protected void InicializarDetalle()
    {
        List<Detalle_CDAT> lstDetalle = new List<Detalle_CDAT>();
        for (int i = gvLista.Rows.Count; i < 3; i++)
        {
            Detalle_CDAT eApert = new Detalle_CDAT();
            eApert.cod_persona = null;
            eApert.principal = null;
            eApert.conjuncion = "";
            lstDetalle.Add(eApert);
        }
        gvLista.DataSource = lstDetalle;
        gvLista.DataBind();
        GridView1.DataSource = lstDetalle;
        GridView1.DataBind();
        Session["DatosDetalle"] = lstDetalle;
    }

    private Cdat ObtenerValores()
    {
        Cdat vApertu = new Cdat();
        if (txtCodigo.Text.Trim() != "")
            vApertu.codigo_cdat = Convert.ToInt64(txtCodigo.Text);
        if (ddlModalidad.SelectedIndex != 0)
            vApertu.modalidad = ddlModalidad.SelectedValue;
        if (ddlOficina.SelectedIndex != 0)
            vApertu.cod_oficina = Convert.ToInt32(ddlOficina.SelectedValue);
        
        return vApertu;
    }



    private string obtFiltro(Cdat vApertu)
    {
        String filtro = String.Empty;

        vApertu.identificacion = txtidentificacion.Text;
        filtro+="and v_persona.identificacion= " + vApertu.identificacion;


        return filtro;
    }


   
    
}