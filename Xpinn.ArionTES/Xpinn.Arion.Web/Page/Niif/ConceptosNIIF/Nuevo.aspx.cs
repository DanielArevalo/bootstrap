using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.NIIF.Services;
using Xpinn.NIIF.Entities;
using System.IO;
using System.Text;

public partial class Nuevo : GlobalWeb
{
    Xpinn.NIIF.Services.EstadosFinancierosNIIFService EstadosFinancierosNIFServicio = new Xpinn.NIIF.Services.EstadosFinancierosNIIFService();

    PoblarListas poblar = new PoblarListas();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(EstadosFinancierosNIFServicio.CodigoPrograma, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlmensaje.eventoClick += btnContinuar_Click;
            toolBar.eventoCancelar += (s, evt) => Navegar("Lista.aspx");
            toolBar.eventoRegresar += (s, evt) =>
            {
                Session.Remove(EstadosFinancierosNIFServicio.CodigoPrograma + ".id");
                Navegar("~/Page/Niif/EstadosFinancierosNIIF/Lista.aspx");
            };
            toolBar.MostrarRegresar(false);

            ///  toolBar.MostrarGuardar(true);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstadosFinancierosNIFServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtcodigo.Visible = false;
                lblconcepto.Visible = false;
                CargarLista();
                //TRAER DATOS DE los conceptos base
                Xpinn.Comun.Entities.General pDataM = new Xpinn.Comun.Entities.General();
                Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
                pDataM = ConsultaData.ConsultarGeneral(4202, (Usuario)Session["usuario"]);

                List<EstadosFinancierosNIIF> lstConsultaCuentasNIIf = new List<EstadosFinancierosNIIF>();
                Int32 nivel = Convert.ToInt32(ddlNivel.SelectedValue);
                if (nivel == 0)
                {
                    nivel = 9;
                }
                if (pDataM.valor == "1")
                    lstConsultaCuentasNIIf = EstadosFinancierosNIFServicio.ConsultarCuentasNIIF(nivel, (Usuario)Session["usuario"]);
                else
                    lstConsultaCuentasNIIf = EstadosFinancierosNIFServicio.ConsultarCuentasLocalNIIF(nivel, (Usuario)Session["usuario"]);

                gvCuentasNIIF.PageSize = pageSize;
                gvCuentasNIIF.EmptyDataText = emptyQuery;
                gvCuentasNIIF.DataSource = lstConsultaCuentasNIIf;

                if (lstConsultaCuentasNIIf.Count > 0)
                {
                    gvCuentasNIIF.Visible = true;
                    gvCuentasNIIF.DataBind();

                }
                else
                {
                    gvCuentasNIIF.Visible = false;
                }

              
                if (Session[EstadosFinancierosNIFServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[EstadosFinancierosNIFServicio.CodigoPrograma + ".id"].ToString();
                    txtcodigo.Text = idObjeto;
                    ObtenerDatos(idObjeto);

                }
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(true);

            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstadosFinancierosNIFServicio.CodigoPrograma, "Page_Load", ex);
        }
    }



    private void CargarLista()
    {
        LlenarListasDesplegables(TipoLista.TipoEstadoFinanciero, ddlTipoEstadoFinanciero);
        LlenarListasDesplegables(TipoLista.Concepto_Niif, ddlDepende_De);
        ddlDepende_De.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlTipoEstadoFinanciero.SelectedValue = Convert.ToString(Session[EstadosFinancierosNIFServicio.CodigoPrograma + ".idestadofinanciero"]);



    }
    protected List<EstadosFinancierosNIIF> ObtenerListaDetalle()
    {
        try
        {
            List<EstadosFinancierosNIIF> lstDetalle = new List<EstadosFinancierosNIIF>();
            //lista para adicionar filas sin perder datos
            List<EstadosFinancierosNIIF> lista = new List<EstadosFinancierosNIIF>();

            foreach (GridViewRow rfila in gvCuentasNIIF.Rows)
            {
                EstadosFinancierosNIIF ePogra = new EstadosFinancierosNIIF();
                Label lbl_consecutivo = (Label)rfila.FindControl("lbl_consecutivo");
                CheckBox cbSeleccionar = (CheckBox)rfila.FindControl("cbSeleccionar");
                CheckBox cbCorriente = (CheckBox)rfila.FindControl("cbCorriente");
                CheckBox cbNoCorriente = (CheckBox)rfila.FindControl("cbNoCorriente");
                Label lblcodigo = (Label)rfila.FindControl("lblcodigo");



                if (cbSeleccionar.Checked == true)
                {


                    if (lblcodigo != null)
                        ePogra.codigo = Convert.ToInt32(lblcodigo.Text);


                    if (lbl_consecutivo != null)
                        ePogra.cod_cuenta_niif = Convert.ToString(lbl_consecutivo.Text);

                    if (cbCorriente.Checked == true)
                        ePogra.corriente = 1;
                    else
                        ePogra.corriente = 0;

                    if (cbNoCorriente.Checked == true)
                        ePogra.nocorriente = 1;
                    else
                        ePogra.nocorriente = 0;


                    if (Session[EstadosFinancierosNIFServicio.CodigoPrograma + ".id"] != null)
                    {
                        idObjeto = Session[EstadosFinancierosNIFServicio.CodigoPrograma + ".id"].ToString();

                        ePogra.cod_concepto = Convert.ToInt32(idObjeto);
                    }

                    lista.Add(ePogra);
                    Session["DetalleCarga"] = lista;


                    if (ePogra.cod_cuenta_niif != "")
                    {
                        lstDetalle.Add(ePogra);
                    }


                }


                if (cbSeleccionar.Checked == false && lblcodigo.Text!="0")
                {
                    idObjeto = Session[EstadosFinancierosNIFServicio.CodigoPrograma + ".id"].ToString();

                    ePogra.cod_concepto = Convert.ToInt32(idObjeto);

                    if (lblcodigo != null)
                        ePogra.codigo = Convert.ToInt32(lblcodigo.Text);
                    if (ePogra.codigo > 0)
                    {
                        ePogra.cod_cuenta_niif = "0";
                        ePogra.corriente = 0;
                        ePogra.nocorriente = 0;
                    }

                    lista.Add(ePogra);
                    Session["DetalleCarga"] = lista;


                    if (ePogra.codigo > 0 && ePogra.cod_cuenta_niif == "0")
                    {
                        lstDetalle.Add(ePogra);
                    }

                }


            }
            return lstDetalle;



        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(EstadosFinancierosNIFServicio.CodigoPrograma, "ObtenerListaDetalle", ex);
            return null;
        }
    }

    #region Métodos de carga de datos
    protected void ObtenerDatos(String pIdObjeto)
    {

        ddlTipoEstadoFinanciero.Enabled = false;
        //  ddlDepende_De.Enabled = false;

        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(true);
        txtcodigo.Visible = true;
        lblconcepto.Visible = true;
        try
        {
            Xpinn.Comun.Entities.General pDataM = new Xpinn.Comun.Entities.General();
            Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
            pDataM = ConsultaData.ConsultarGeneral(4202, (Usuario)Session["usuario"]);

            EstadosFinancierosNIIF Entidad = new EstadosFinancierosNIIF();

            Entidad = EstadosFinancierosNIFServicio.ConsultarConceptosNIF(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);


            txtDescripcion.Text = Entidad.descripcion_concepto.ToString();
            
            ddlDepende_De.SelectedValue = Entidad.depende_de.ToString();


            cbTitulo.Checked = Entidad.titulo == 1 ? true : false;



            
            //RECUPERAR DATOS - GRILLA CUENTAS
            List<EstadosFinancierosNIIF> lstDetalle = new List<EstadosFinancierosNIIF>();
            EstadosFinancierosNIIF pDeta = new EstadosFinancierosNIIF();
            pDeta.codigo = Convert.ToInt32(Entidad.cod_concepto);

            //segun el aparemtro determina si va a oplan cuentas niif o a plan cuentas local 
            if (pDataM.valor == "1")
                lstDetalle = EstadosFinancierosNIFServicio.ListarCuentasNIIF(pDeta.codigo, (Usuario)Session["usuario"]);
            else
                lstDetalle = EstadosFinancierosNIFServicio.ListarCuentasLocalNIIF(pDeta.codigo, (Usuario)Session["usuario"]);

            ddlNivel.Visible = false;

            if (lstDetalle.Count > 0)
            {                


                if ((lstDetalle != null) || (lstDetalle.Count != 0))
                {
                    gvCuentasNIIF.DataSource = lstDetalle;
                    gvCuentasNIIF.DataBind();
                }
                Session["DetalleCarga"] = lstDetalle;
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstadosFinancierosNIFServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    #endregion

    #region Eventos de botones

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {

            ctlmensaje.MostrarMensaje("¿Desea registrar la EstadosFinancierosNIIF?");
        }
        catch (Exception ex)
        {
            VerError(EstadosFinancierosNIFServicio.CodigoPrograma + ex.Message);
        }
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        EstadosFinancierosNIIF pEntitie = new EstadosFinancierosNIIF();
        pEntitie.descripcion_concepto = Convert.ToString(txtDescripcion.Text);

        pEntitie.cod_estado_financiero = Convert.ToInt32(ddlTipoEstadoFinanciero.SelectedValue);
        if (ddlDepende_De.SelectedValue == "")
          
            pEntitie.depende_de = Convert.ToInt32(ddlDepende_De.SelectedValue);
        
        pEntitie.cod_concepto = 0;
        
        if(cbTitulo.Checked==true)
          pEntitie.titulo = 1;
        else
            pEntitie.titulo = 0;


        pEntitie.lstDetalle = new List<EstadosFinancierosNIIF>();
        pEntitie.lstDetalle = ObtenerListaDetalle();

        if (idObjeto == "")
        {


            EstadosFinancierosNIIF Entitie = EstadosFinancierosNIFServicio.CrearConceptosNIIF(pEntitie, Usuario);
            pEntitie.cod_concepto = Entitie.cod_concepto;
            lblMensaje.Visible = true;
            lblMensaje.Text = "Concepto Creado Correctamente";
            Site toolBar = (Site)this.Master;
            toolBar.MostrarRegresar(true);
            Navegar("~/Page/Niif/EstadosFinancierosNIIF/Lista.aspx");
        }

        else
        {
            pEntitie.cod_concepto = Convert.ToInt32(txtcodigo.Text);
            pEntitie = EstadosFinancierosNIFServicio.ModificarConceptosNIF(pEntitie, Usuario);
            lblMensaje.Visible = true;
            lblMensaje.Text = "Concepto Modificado Correctamente";
            Site toolBar = (Site)this.Master;
            toolBar.MostrarRegresar(true);
            Navegar("~/Page/Niif/EstadosFinancierosNIIF/Lista.aspx");
        }

    }





    #endregion

    #region Eventos GridView 
    

    #endregion





    protected void cbCorriente_CheckedChanged(object sender, EventArgs e)
    {

        foreach (GridViewRow row in gvCuentasNIIF.Rows)
        {
            CheckBox cbSeleccionar = row.FindControl("cbSeleccionar") as CheckBox;
            CheckBox cbNoCorriente = row.FindControl("cbNoCorriente") as CheckBox;
            CheckBox cbCorriente = row.FindControl("cbCorriente") as CheckBox;


            if (cbCorriente.Checked)
            {
                cbNoCorriente.Checked = false;
            }

        }
    }

    protected void cbNoCorriente_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvCuentasNIIF.Rows)
        {
            CheckBox cbSeleccionar = row.FindControl("cbSeleccionar") as CheckBox;
            CheckBox cbNoCorriente = row.FindControl("cbNoCorriente") as CheckBox;
            CheckBox cbCorriente = row.FindControl("cbCorriente") as CheckBox;

            if (cbNoCorriente.Checked)
            {
                cbCorriente.Checked = false;
            }

        }
    }

    protected void cbSeleccionar_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvCuentasNIIF.Rows)
        {
            CheckBox cbSeleccionar = row.FindControl("cbSeleccionar") as CheckBox;
            CheckBox cbNoCorriente = row.FindControl("cbNoCorriente") as CheckBox;
            CheckBox cbCorriente = row.FindControl("cbCorriente") as CheckBox;

            if (cbSeleccionar.Checked)
            {
                if (cbNoCorriente.Checked)
                {
                    cbCorriente.Checked = false;
                }
                if (cbCorriente.Checked)
                {
                    cbNoCorriente.Checked = false;
                }
            }
        }
    }

    protected void ddlNivel_SelectedIndexChanged(object sender, EventArgs e)
    {

        List<EstadosFinancierosNIIF> lstConsultaCuentasNIIf = new List<EstadosFinancierosNIIF>();
        List<EstadosFinancierosNIIF> lstDetalle = new List<EstadosFinancierosNIIF>();
        Int32 nivel = Convert.ToInt32(ddlNivel.SelectedValue);
        //TRAER DATOS DE los conceptos base
        Xpinn.Comun.Entities.General pDataM = new Xpinn.Comun.Entities.General();
        Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
        pDataM = ConsultaData.ConsultarGeneral(4202, (Usuario)Session["usuario"]);


        if (pDataM.valor == "1")
            lstConsultaCuentasNIIf = EstadosFinancierosNIFServicio.ConsultarCuentasNIIF(nivel, (Usuario)Session["usuario"]);
        else
            lstConsultaCuentasNIIf = EstadosFinancierosNIFServicio.ConsultarCuentasLocalNIIF(nivel, (Usuario)Session["usuario"]);

        gvCuentasNIIF.PageSize = pageSize;
        gvCuentasNIIF.EmptyDataText = emptyQuery;
        gvCuentasNIIF.DataSource = lstConsultaCuentasNIIf;

        if (lstConsultaCuentasNIIf.Count > 0)
        {
            gvCuentasNIIF.Visible = true;
            gvCuentasNIIF.DataBind();

        }
        else
        {
            gvCuentasNIIF.Visible = false;
        }

    }

    protected void btnBorrar_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void ddlDepende_De_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    protected void ddlTipoEstadoFinanciero_SelectedIndexChanged(object sender, EventArgs e)
    {
      
       // List<EstadosFinancierosNIIF> lstConsultaCuentasNIIf = new List<EstadosFinancierosNIIF>();
       // List<EstadosFinancierosNIIF> lstDetalle = new List<EstadosFinancierosNIIF>();
      //  Int32 tipo = Convert.ToInt32(ddlTipoEstadoFinanciero.SelectedValue);
      //  ddlDepende_De.DataSource=  EstadosFinancierosNIFServicio.ConsultarDependeDe(tipo, (Usuario)Session["usuario"]);
     //   ddlDepende_De.DataBind();

    }
}