using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

public delegate void ImprimirDelegate(object sender, ImageClickEventArgs evt);

public partial class Imprimir : System.Web.UI.UserControl
{   
    public Control PrintControl {private set; get;}
    private Page page;
    private HtmlForm form;
    private StringWriter stringWriter;
    private HtmlTextWriter htmlTxtWriter;
    private string _Titulo;
    public const string JSCRIPT_PRINT = "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>";

    /*public event ImprimirDelegate PrintClick;
    ImprimirDelegate printDelegate;*/

    public event EventHandler<ImageClickEventArgs> PrintCustomEvent;
    EventHandler<ImageClickEventArgs> handler;

   

    public Imprimir() {
        page = new Page();
        page.EnableEventValidation = false;
        form = new HtmlForm();
        stringWriter = new StringWriter();
        htmlTxtWriter = new HtmlTextWriter(stringWriter);        
    }

    protected void Page_Load(object sender, EventArgs e){
        //printDelegate = PrintClick;
        handler = PrintCustomEvent;
    }

    public  void PrintWebControl(Control pCtrl) {

        if (pCtrl is WebControl) {
            Unit width = new Unit(100, UnitType.Percentage);
            ((WebControl)pCtrl).Width = width;
        }

        _Titulo = _Titulo != "" && _Titulo != null? "document.title='"+_Titulo+"'" : "";
        
        page.Controls.Add(form);
        form.Attributes.Add("runat", "server");
        if (ControlHeader != null)
            form.Controls.Add(ControlHeader);
        form.Controls.Add(pCtrl);
        page.DesignerInitialize();
        page.RenderControl(htmlTxtWriter);
        string strHtml = stringWriter.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHtml);
        HttpContext.Current.Response.Write("<script>window.print(); "+_Titulo+"</script>");
        HttpContext.Current.Response.End();
    }
    
    protected void ImgBtn_Click(object sender, ImageClickEventArgs e){
        /*if (printDelegate != null) {
            printDelegate(sender, e);
        }*/

        if (handler != null) {
            handler(sender, e);
        }
    }    

    public string Titulo
    {
        get { return _Titulo; }
        set
        {
            _Titulo = value;
        }
    }

    private Control _controlHeader;
    public Control ControlHeader
    {
        get { return _controlHeader; }
        set
        {
            _controlHeader = value;
        }
    }

}