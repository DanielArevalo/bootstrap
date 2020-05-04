using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net.Mail;
using System.Text;


//public delegate void ImprimirDelegate(object sender, ImageClickEventArgs evt);

public partial class General_Controles_EnviarCorreo : System.Web.UI.UserControl
{
    public Control PrintControl {private set; get;}
    private Page page;
    private HtmlForm form;
    private StringWriter stringWriter;
    private HtmlTextWriter htmlTxtWriter;
    
    public const string JSCRIPT_PRINT = "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>";

    /*public event ImprimirDelegate PrintClick;
    ImprimirDelegate printDelegate;*/

    public event EventHandler<ImageClickEventArgs> PrintCustomEvent;
    EventHandler<ImageClickEventArgs> handler;

    public General_Controles_EnviarCorreo()
    {
        //Session["ocultarMenu"] = "1";
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
   
    public void PrintWebControl(Control pCtrl, System.Collections.ArrayList ctrl2)
    {        
        string correofrom = "";
        correofrom = ctrl2[1].ToString();
        if (pCtrl is WebControl) {
            Unit width = new Unit(100, UnitType.Percentage);
            ((WebControl)pCtrl).Width = width;
        }
                
        page.Controls.Add(form);
        form.Attributes.Add("runat", "server");
        form.Controls.Add(pCtrl);        
        page.DesignerInitialize();
        page.RenderControl(htmlTxtWriter);
        string strHtml = stringWriter.ToString();
        HttpContext.Current.Response.Clear();

        MailMessage mail = new MailMessage();
        //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587); // Para cuentas de GMAIL
        SmtpClient SmtpServer = new SmtpClient("smtp.live.com", 25);   // Para cuentas de HOTMAIL
        //SmtpClient SmtpServer = new SmtpClient("mail.expinn.com.co", 587);

        mail.From = new MailAddress(correofrom, "PROGRAMACION AGENDA", Encoding.UTF8);           
        mail.Subject = "AGENDA ASIGNADA ";
        foreach (string correo in ctrl2)
        {
            mail.To.Add(correo.ToString());
        }        
        mail.IsBodyHtml = true;
        mail.Body =strHtml;
        //SmtpServer.Credentials = new System.Net.NetworkCredential("fortiz.expinn@gmail.com", "");
        SmtpServer.Credentials = new System.Net.NetworkCredential("expinn@hotmail.com", "3xp1nn2015");
        //SmtpServer.Credentials = new System.Net.NetworkCredential("fortiz@expinn.com.co", "");
        SmtpServer.EnableSsl = true;
        SmtpServer.Send(mail);      
    }

    public void Correo(string correo)
    {   
        //if (correo != null)
        //    Session["correo"] = correo;
    }
    
    protected void ImgBtn_Click1(object sender, ImageClickEventArgs e)
    {
        /*if (printDelegate != null) {
            printDelegate(sender, e);
        }*/

        if (handler != null) {
            handler(sender, e);
        }
    }
}