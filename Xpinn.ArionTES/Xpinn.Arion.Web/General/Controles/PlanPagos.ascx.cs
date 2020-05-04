using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using System.Data;
using Microsoft.CSharp;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Globalization;

public partial class cPlanPagos : System.Web.UI.UserControl
{
    private CreditoPlanService creditoPlanServicio = new CreditoPlanService();
    private DatosPlanPagosService datosServicio = new DatosPlanPagosService();
    public Credito datosCred = new Credito();
    public Boolean bNuevo = false;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        UPPlanExcel.Visible = false;
    }


    public void TablaPlanPagos()
    {       
        Int32 anchocolumna = 100;
        Int32 longitud = 0;

        List<DatosPlanPagos> lstConsulta = new List<DatosPlanPagos>();
        if (bNuevo == true)
            lstConsulta = datosServicio.ListarDatosPlanPagosNue(datosCred, (Usuario)Session["usuario"]);
        else
            lstConsulta = datosServicio.ListarDatosPlanPagos(datosCred, (Usuario)Session["usuario"]);
        Session["PlanPagos"] = lstConsulta;

        gvPlanPagos.DataSource = lstConsulta;
        gvPlanPagos0.DataSource = lstConsulta;

        ///////////////////////////////////////////////////////////////////////////////////////
        // Ajustar valores para la grilla que se usa para mostrar en pantalla
        ///////////////////////////////////////////////////////////////////////////////////////

        // Ajustar información de la grila para mostrar en pantalla
        if (lstConsulta.Count > 0)
        {
            // Mostrando la grilla y validar permisos
            gvPlanPagos.Visible = true;
            gvPlanPagos.DataBind();
            gvPlanPagos.Columns[1].ItemStyle.Width = 90;
            // Ocultando las columnas que no deben mostrarse
            List<Atributos> lstAtr = new List<Atributos>();
            lstAtr = datosServicio.GenerarAtributosPlan((Usuario)Session["usuario"]);
            Session["AtributosPlanPagos"] = lstAtr;
            for (int i = 4; i <= 18; i++)
            {
                gvPlanPagos.Columns[i].Visible = false;
                int j = 0;
                foreach (Atributos item in lstAtr)
                {
                    if (j == i - 4)
                        gvPlanPagos.Columns[i].HeaderText = item.nom_atr;
                    j = j + 1;
                }
            }
            // Establecer el ancho de las columnas de valores
            for (int i = 2; i < 20; i++)
            {
                gvPlanPagos.Columns[i].ItemStyle.Width = anchocolumna;
            }
            // Ajustando el tamaño de la grilla
            longitud = 0;
            for (int i = 0; i < 20; i++)
            {
                if (gvPlanPagos.Columns[i].Visible == true)
                    longitud = longitud + Convert.ToInt32(gvPlanPagos.Columns[i].ItemStyle.Width.Value);
            }
            if (longitud + anchocolumna > 800)
                gvPlanPagos.Width = longitud + anchocolumna;
            else
                gvPlanPagos.Width = 800;
            gvPlanPagos.Height = lstConsulta.Count * 10;
            // Mostrando las columnas que tienen valores
            foreach (DatosPlanPagos ItemPlanPagos in lstConsulta)
            {
                if (ItemPlanPagos.int_1 != 0) { gvPlanPagos.Columns[4].Visible = true; }
                if (ItemPlanPagos.int_2 != 0) { gvPlanPagos.Columns[5].Visible = true; }
                if (ItemPlanPagos.int_3 != 0) { gvPlanPagos.Columns[6].Visible = true; }
                if (ItemPlanPagos.int_4 != 0) { gvPlanPagos.Columns[7].Visible = true; }
                if (ItemPlanPagos.int_5 != 0) { gvPlanPagos.Columns[8].Visible = true; }
                if (ItemPlanPagos.int_6 != 0) { gvPlanPagos.Columns[9].Visible = true; }
                if (ItemPlanPagos.int_7 != 0) { gvPlanPagos.Columns[10].Visible = true; }
                if (ItemPlanPagos.int_8 != 0) { gvPlanPagos.Columns[11].Visible = true; }
                if (ItemPlanPagos.int_9 != 0) { gvPlanPagos.Columns[12].Visible = true; }
                if (ItemPlanPagos.int_10 != 0) { gvPlanPagos.Columns[13].Visible = true; }
                if (ItemPlanPagos.int_11 != 0) { gvPlanPagos.Columns[14].Visible = true; }
                if (ItemPlanPagos.int_12 != 0) { gvPlanPagos.Columns[15].Visible = true; }
                if (ItemPlanPagos.int_13 != 0) { gvPlanPagos.Columns[16].Visible = true; }
                if (ItemPlanPagos.int_14 != 0) { gvPlanPagos.Columns[17].Visible = true; }
                if (ItemPlanPagos.int_15 != 0) { gvPlanPagos.Columns[18].Visible = true; }
            }
            gvPlanPagos.DataBind();
        }
        else
        {
            gvPlanPagos.Visible = true;
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        // Ajustar valores para la grilla que se usa para descargar los datos a excel.
        ///////////////////////////////////////////////////////////////////////////////////////
        if (lstConsulta.Count > 0)
        {
            gvPlanPagos0.Visible = true;
            gvPlanPagos0.DataBind();
            gvPlanPagos0.Columns[1].ItemStyle.Width = 90;
            // Ocultando las columnas que no deben mostrarse
            List<Atributos> lstAtr = new List<Atributos>();
            lstAtr = datosServicio.GenerarAtributosPlan((Usuario)Session["usuario"]);
            for (int i = 4; i <= 18; i++)
            {
                gvPlanPagos0.Columns[i].Visible = false;
                int j = 0;
                foreach (Atributos item in lstAtr)
                {
                    if (j == i - 4)
                        gvPlanPagos0.Columns[i].HeaderText = item.nom_atr;
                    j = j + 1;
                }
            }
            // Establecer el ancho de las columnas de valores
            for (int i = 2; i < 20; i++)
            {
                gvPlanPagos0.Columns[i].ItemStyle.Width = anchocolumna;
            }
            // Ajustando el tamaño de la grilla
            longitud = 0;
            for (int i = 0; i < 20; i++)
            {
                longitud = longitud + Convert.ToInt32(gvPlanPagos0.Columns[i].ItemStyle.Width.Value);
            }
            gvPlanPagos0.Width = longitud / 2;
            foreach (DatosPlanPagos ItemPlanPagos in lstConsulta)
            {
                if (ItemPlanPagos.int_1 != 0) { gvPlanPagos0.Columns[4].Visible = true; }
                if (ItemPlanPagos.int_2 != 0) { gvPlanPagos0.Columns[5].Visible = true; }
                if (ItemPlanPagos.int_3 != 0) { gvPlanPagos0.Columns[6].Visible = true; }
                if (ItemPlanPagos.int_4 != 0) { gvPlanPagos0.Columns[7].Visible = true; }
                if (ItemPlanPagos.int_5 != 0) { gvPlanPagos0.Columns[8].Visible = true; }
                if (ItemPlanPagos.int_6 != 0) { gvPlanPagos0.Columns[9].Visible = true; }
                if (ItemPlanPagos.int_7 != 0) { gvPlanPagos0.Columns[10].Visible = true; }
                if (ItemPlanPagos.int_8 != 0) { gvPlanPagos0.Columns[11].Visible = true; }
                if (ItemPlanPagos.int_9 != 0) { gvPlanPagos0.Columns[12].Visible = true; }
                if (ItemPlanPagos.int_10 != 0) { gvPlanPagos0.Columns[13].Visible = true; }
                if (ItemPlanPagos.int_11 != 0) { gvPlanPagos0.Columns[14].Visible = true; }
                if (ItemPlanPagos.int_12 != 0) { gvPlanPagos0.Columns[15].Visible = true; }
                if (ItemPlanPagos.int_13 != 0) { gvPlanPagos0.Columns[16].Visible = true; }
                if (ItemPlanPagos.int_14 != 0) { gvPlanPagos0.Columns[17].Visible = true; }
                if (ItemPlanPagos.int_15 != 0) { gvPlanPagos0.Columns[18].Visible = true; }
            }
            gvPlanPagos0.DataBind();
        }
        else
        {
            gvPlanPagos0.Visible = false;
        }
    }


    private Boolean Actualizar()
    {
        try
        {
            String emptyQuery = "Fila de datos vacia";
            List<DatosPlanPagos> lstConsultaCreditos = new List<DatosPlanPagos>();
            lstConsultaCreditos.Clear();
            //if (bNuevo == true)
            //    lstConsultaCreditos = datosServicio.ListarDatosPlanPagosNue(datosCred, (Usuario)Session["usuario"]);
            //else
            //    lstConsultaCreditos = datosServicio.ListarDatosPlanPagos(datosCred, (Usuario)Session["usuario"]);
            lstConsultaCreditos = (List<DatosPlanPagos>)Session["PlanPagos"];
            gvPlanPagos.EmptyDataText = emptyQuery;
            gvPlanPagos.DataSource = lstConsultaCreditos;
            if (lstConsultaCreditos.Count > 0)
            {
                gvPlanPagos.DataBind();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch 
        {           
            return false;
        }
    }

    protected void gvPlanPagos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvPlanPagos.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch 
        {
            return;
        }
    }



    public string ExportarPlanPagosExcel()
    {
        try
        {
            if (gvPlanPagos0.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvPlanPagos0.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvPlanPagos0);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
                Response.Charset = "UTF-8";
                Response.Write(sb.ToString());
                Response.End();
            }
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }



}