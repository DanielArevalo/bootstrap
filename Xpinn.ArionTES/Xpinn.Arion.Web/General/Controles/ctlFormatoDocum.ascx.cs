using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Xpinn.Util;

using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Text;
using iTextSharp.text;
using ListItem = System.Web.UI.WebControls.ListItem;

public delegate void btnImpresion_ActionsDelegate(object sender, EventArgs e);

public partial class ctlFormatoDocum : System.Web.UI.UserControl
{
    public event btnImpresion_ActionsDelegate eventoClick;

    string pError = "";
    Xpinn.Aportes.Services.FormatoDocumentoServices BOFormato = new Xpinn.Aportes.Services.FormatoDocumentoServices();
    //TIPOS DE FORMATOS
    //Tipo 1 = Afiliacion ,  2 = Aprobación 
    public void Inicializar(string pTipo)
    {
        List<Xpinn.Aportes.Entities.FormatoDocumento> lstFormatos = new List<Xpinn.Aportes.Entities.FormatoDocumento>();
        Xpinn.Aportes.Entities.FormatoDocumento pEntidad = new Xpinn.Aportes.Entities.FormatoDocumento();
        pEntidad.tipo = pTipo;
        lstFormatos = BOFormato.ListarFormatoDocumento(pEntidad, (Usuario)Session["usuario"]);
        if (lstFormatos.Count > 0)
        {
            ddlFormatosImp.DataTextField = "descripcion";
            ddlFormatosImp.DataValueField = "cod_documento";
            ddlFormatosImp.DataSource = lstFormatos;
            ddlFormatosImp.DataBind();
        }
        lblTipo.Text = pTipo.Trim();
    }

    protected void btnImpresion_Click(object sender, EventArgs e)
    {
        if (eventoClick != null)
            eventoClick(sender, e);
    }

   public Boolean ImprimirFormato(string pVariable, string pRuta, string origen = null)
    {
        lblErrorFI.Visible = false;
        lblErrorFI.Text = "";
        if (ddlFormatosImp.SelectedItem == null)
        {
            lblErrorFI.Visible = true;
            lblErrorFI.Text = "No existen formatos creados para generar la Impresión, Adiciónelas por el módulo de Gestión Solidaria - Formato de Documentos";
            return false;
        }

        //RECUPERAR NOMBRE DE PL AL QUE EJECUTARA
        string cTipoDocumento = ddlFormatosImp.SelectedValue;
        string cNombreDocumento = ddlFormatosImp.SelectedItem.Text;
        string cDocumentoGenerado = Server.MapPath(pRuta + pVariable.Trim() + "_" + cTipoDocumento + '.' + 'p' + 'd' + 'f');
        Xpinn.Aportes.Entities.FormatoDocumento FormatoDOC = new Xpinn.Aportes.Entities.FormatoDocumento();
        FormatoDOC = BOFormato.ConsultarFormatoDocumento(Convert.ToInt64(cTipoDocumento), (Usuario)Session["Usuario"]);

        List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento> lstDocumentos = new List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento>();
        lstDocumentos = BOFormato.ListarDatosDeDocumentoOtros(Convert.ToInt64(pVariable), FormatoDOC.nombre_pl, (Usuario)Session["usuario"],origen);

        if (FormatoDOC != null)
        {
            if (FormatoDOC.Textos != null)
                ProcessesHTML.ReemplazarEnDocumentoDeWordYGuardarPdf(Encoding.ASCII.GetString(FormatoDOC.Textos), lstDocumentos, cDocumentoGenerado, ref pError);
            else
                ProcessesHTML.ReemplazarEnDocumentoDeWordYGuardarPdf(FormatoDOC.texto, lstDocumentos, cDocumentoGenerado, ref pError);
        }
        return true;
    }

    public void MostrarControl()
    {
        mpeFormatos.Show();
    }

    public void OcultarControl()
    {
        mpeFormatos.Hide();
    }

    protected void btnCancFormat_Click(object sender, ImageClickEventArgs e)
    {
        OcultarControl();
    }

    public string lblErrorText
    {
        set { lblErrorFI.Text = value; }
        get { return lblErrorFI.Text; }
    }

    public bool lblErrorIsVisible
    {
        set { lblErrorFI.Visible = value; }
        get { return lblErrorFI.Visible; }
    }

    public int ddlFormatosIndex
    {
        set { ddlFormatosImp.SelectedIndex = value; }
        get { return ddlFormatosImp.SelectedIndex; }
    }

    public string ddlFormatosValue
    {
        set { ddlFormatosImp.SelectedValue = value; }
        get { return ddlFormatosImp.SelectedValue; }
    }

    public string ddlFormatosItemText
    {
        set { ddlFormatosImp.SelectedItem.Text = value; }
        get { return ddlFormatosImp.SelectedItem.Text; }
    }

    public ListItem ddlFormatosItem
    {
        get { return ddlFormatosImp.SelectedItem; }
    }

}