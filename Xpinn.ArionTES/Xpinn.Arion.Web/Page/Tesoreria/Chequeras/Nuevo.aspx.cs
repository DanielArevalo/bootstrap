using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Data;
using Microsoft.Reporting.WebForms;

public partial class Nuevo : GlobalWeb
{
    ChequeraService ChequeraServ = new ChequeraService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ChequeraServ.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ChequeraServ.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvAplicar.ActiveViewIndex = 0;
                      
                txtCodigo.Enabled = false;
                txtBanco.Enabled = false;

                Chequera cheq = new Chequera();
                List<Chequera> lstcheque = new List<Chequera>();
                lstcheque = ChequeraServ.ListarCuentasBancarias(cheq, (Usuario)Session["usuario"]);
                ddlCuenta.DataSource = lstcheque;
                ddlCuenta.DataTextField = "num_cuenta";
                ddlCuenta.DataValueField = "idctabancaria";
                ddlCuenta.Items.Insert(0, new ListItem("Seleccione un item", "0"));
                ddlCuenta.SelectedIndex = 0;
                ddlCuenta.DataBind();

                if (Session[ChequeraServ.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[ChequeraServ.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ChequeraServ.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    Session["TEXTO"] = "modificar";
                    lblmsj.Text = "modificado";
                }
                else
                {
                    txtFecha.Text = DateTime.Today.ToShortDateString();
                    txtSigCheque.Enabled = false;
                    txtCodigo.Text = Convert.ToString(ChequeraServ.ObtenerSiguienteCodigo((Usuario)Session["usuario"]));

                    Session["TEXTO"] = "grabar";
                    lblmsj.Text = "grabado";
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ChequeraServ.GetType().Name + "L", "Page_Load", ex);
        }

    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Chequera vRecaudos = new Chequera();
            vRecaudos.idchequera = Convert.ToInt32(pIdObjeto);

            vRecaudos = ChequeraServ.ConsultarChequera(vRecaudos, (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vRecaudos.idchequera.ToString()))
                txtCodigo.Text = HttpUtility.HtmlDecode(vRecaudos.idchequera.ToString().Trim());
            if (!string.IsNullOrEmpty(vRecaudos.idctabancaria.ToString()))
                ddlCuenta.SelectedValue = HttpUtility.HtmlDecode(vRecaudos.idctabancaria.ToString().Trim());
            if (vRecaudos.nombrebanco != null)
                txtBanco.Text = HttpUtility.HtmlDecode(vRecaudos.nombrebanco.ToString().Trim());
            if (vRecaudos.prefijo != null)
                txtPrefijo.Text = HttpUtility.HtmlDecode(vRecaudos.prefijo.ToString().Trim());

            if (vRecaudos.cheque_ini != null)
                txtChqInicial.Text = HttpUtility.HtmlDecode(vRecaudos.cheque_ini.ToString().Trim());

            if (vRecaudos.cheque_fin != null)
                txtChqFinal.Text = HttpUtility.HtmlDecode(vRecaudos.cheque_fin.ToString().Trim());

            if (vRecaudos.fecha_entrega != null)
                txtFecha.Text = HttpUtility.HtmlDecode(vRecaudos.fecha_entrega.ToString(gFormatoFecha).Trim());

            if(vRecaudos.estado != null || vRecaudos.estado != 0)
                rblEstado.SelectedValue = HttpUtility.HtmlDecode(vRecaudos.estado.ToString().Trim());
           
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ChequeraServ.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void btnFinal_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    public Boolean ValidarDatos()
    {
        if (ddlCuenta.SelectedValue == "0")
        {
            VerError("Seleccione una cuenta Bancaria");
            return false;
        }

        if (txtChqInicial.Text == "") 
        {
            VerError("Debe Ingresar un Nro de Cheque Inicial");
            return false;
        }
        if (txtChqFinal.Text == "") 
        {
            VerError("Debe Ingresar un Nro de Cheque final");
            return false;
        }  

        if (Convert.ToInt32(txtChqInicial.Text) > Convert.ToInt32(txtChqFinal.Text))
        {
            VerError("Debe Ingresar un Nro de Cheque final mayor al cheque inicial");
            return false;
        }

        if (rblEstado.SelectedIndex == null)
        {
            VerError("Seleccione un estado");
            return false;
        }

        if (idObjeto != "")
        {
            if (txtSigCheque.Text == "")
            {
                VerError("Ingrese el Nro del Siguiente Cheque");
                return false;
            }
        }
        
        if (txtFecha.Text == "" || txtFecha.ToDate == DateTime.MinValue.ToShortDateString())
        {
            VerError("Seleccione una fecha");
            return false;
        }
        
         return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            ctlMensaje.MostrarMensaje("Esta seguro de " + Session["TEXTO"].ToString() + " los datos ingresados?");
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario User = (Usuario)Session["usuario"];

            Chequera eEmpre = new Chequera();
            if (txtCodigo.Text != "")
                eEmpre.idchequera = Convert.ToInt32(txtCodigo.Text);
            else
                eEmpre.idchequera = 0;
            eEmpre.idctabancaria = Convert.ToInt32(ddlCuenta.SelectedValue);
            if (txtPrefijo.Text != "")
                eEmpre.prefijo = txtPrefijo.Text;
            else
                eEmpre.prefijo = null;

            eEmpre.cheque_ini = Convert.ToInt32(txtChqInicial.Text);
            eEmpre.cheque_fin = Convert.ToInt32(txtChqFinal.Text);
            eEmpre.fecha_entrega = Convert.ToDateTime(txtFecha.Text);
            if (txtSigCheque.Text != "")
                eEmpre.num_sig_che = Convert.ToInt32(txtSigCheque.Text);
            else
                eEmpre.num_sig_che = Convert.ToInt32(txtChqInicial.Text);
            eEmpre.estado = Convert.ToInt32(rblEstado.SelectedValue);


            if (idObjeto != "")
            {
                //MODIFICAR
                eEmpre.fecultmod = Convert.ToDateTime(DateTime.Today.ToShortDateString());
                eEmpre.usuariomod = User.nombre;
                ChequeraServ.ModificarChequera(eEmpre, (Usuario)Session["usuario"]);
            }
            else
            {
                //CREAR
                eEmpre.fechacreacion = Convert.ToDateTime(DateTime.Today.ToShortDateString());
                eEmpre.usuariocreacion = User.nombre;
                ChequeraServ.CrearChequera(eEmpre, (Usuario)Session["usuario"]);
            }
            Session[ChequeraServ.CodigoPrograma + ".id"] = idObjeto;
             
            mvAplicar.ActiveViewIndex = 1;

            //Navegar("~/Page/RecaudosMasivos/Empresas/Lista.aspx");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ChequeraServ.CodigoPrograma, "btnContinuar_Click", ex);
        }
    }

    protected void ddlCuenta_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCuenta.SelectedIndex != 0)
        {
            Chequera cheq = new Chequera();
            cheq.idctabancaria = Convert.ToInt32(ddlCuenta.SelectedValue);

            cheq = ChequeraServ.ConsultarBanco(cheq, (Usuario)Session["usuario"]);
            txtBanco.Text = cheq.nombrebanco;
        }
        else
        {
            txtBanco.Text = "";
        }
    }

    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        toolBar.MostrarCancelar(false);

        RptReporte.LocalReport.EnableExternalImages = true;
   
        ReportDataSource rds = new ReportDataSource("DataSet1", CrearDataTable(idObjeto));
        RptReporte.LocalReport.DataSources.Clear();
        RptReporte.LocalReport.DataSources.Add(rds);

        RptReporte.LocalReport.Refresh();
        RptReporte.Visible = true;
        mvAplicar.ActiveViewIndex = 2;
        
      


    }
    public DataTable CrearDataTable(String pIdObjeto)
    {
        DataRow drDatos;
        DataTable dtDatos = new DataTable();
        List<Chequera> lstConsulta = new List<Chequera>();
        Chequera cheq = new Chequera();
        cheq.idchequera = Convert.ToInt32(txtCodigo.Text);

        lstConsulta = ChequeraServ.ConsultarChequeraReporte(cheq, (Usuario)Session["usuario"]);

        //CREAR TABLA GENERAL;


        dtDatos.Columns.Add("fecha");
        dtDatos.Columns.Add("numcheque");
        dtDatos.Columns.Add("cedula");
        dtDatos.Columns.Add("nombre");
        dtDatos.Columns.Add("valor");
        dtDatos.Columns.Add("TipComprobante");
        dtDatos.Columns.Add("NumComprebante");

      

        foreach (Chequera rfila in lstConsulta) {
            drDatos = dtDatos.NewRow();
            drDatos["fecha"] = rfila.fechacreacion;
            drDatos["numcheque"] = rfila.num_sig_che;
            drDatos["cedula"] = rfila.Cedula;
            drDatos["nombre"] = rfila.nombrepersona;
            drDatos["valor"] = rfila.valor;
            drDatos["TipComprobante"] = rfila.TipoCom;
            drDatos["NumComprebante"] = rfila.NumComp;

            dtDatos.Rows.Add(drDatos);
        }
        dtDatos.AcceptChanges();

        return dtDatos;


    }
}
