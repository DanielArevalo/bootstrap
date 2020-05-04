using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public delegate void txtValor_ActionsDelegate(object sender, EventArgs e);

public partial class decimales : System.Web.UI.UserControl
{
    public event txtValor_ActionsDelegate eventoCambiar;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string Text
    {
        get
        {
            return txtValor.Text.Trim().ToUpper();
        }
        set
        {
            txtValor.Text = value;
        }
    }

    protected string ConvertirACulture (string pdato, string pNumberDecimalSeparator)
    {
        if (pNumberDecimalSeparator == ".")
        {
            pdato = pdato.Replace(",", "");
        }
        else
        {
            pdato = pdato.Replace(".", "");
            pdato = pdato.Replace(",", ".");
        }
        return pdato;
    }

    protected void txtValor_PreRender(object sender, EventArgs e)
    {      
        string str = txtValor.Text;
        string strOri = txtValor.Text;
        string formateado = "";


        str = ConvertirACulture(str, System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

        try
        {
            strOri = ConvertirACulture(str, System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator);
            if (str != "" &&  str.ToLower() != "null" && Convert.ToInt64(str) > 0)
            {

                var strI = Convert.ToInt64(str);  //Convierte a entero y luego a string para quitar ceros a la izquierda
                str = strI.ToString();

                if (str.Length > 12)
                { str = str.Substring(0, 12); }

                int longi = str.Length;
                string milmill = "";
                string mill = "";
                string mil = "";
                string cen = "";


                if (longi > 0 && longi <= 3)
                {
                    cen = str.Substring(0, longi);
                    formateado = Convert.ToInt64(cen).ToString();
                }
                else if (longi > 3 && longi <= 6)
                {
                    mil = str.Substring(0, longi - 3);
                    cen = str.Substring(longi - 3, 3);
                    formateado = Convert.ToInt64(mil) + "." + cen;
                }
                else if (longi > 6 && longi <= 9)
                {
                    mill = str.Substring(0, longi - 6);
                    mil = str.Substring(longi - 6, 3);
                    cen = str.Substring(longi - 3, 3);
                    formateado = Convert.ToInt64(mill) + "." + mil + "." + cen;
                }
                else if (longi > 9 && longi <= 12)
                {
                    milmill = str.Substring(0, longi - 9);
                    mill = str.Substring(longi - 9, 3);
                    mil = str.Substring(longi - 6, 3);
                    cen = str.Substring(longi - 3, 3);
                    formateado = Convert.ToInt64(milmill) + "." + mill + "." + mil + "." + cen;
                }
                else
                { formateado = "0"; }
            }
            else { if (str.ToLower() != "null") formateado = "0"; else formateado = "0"; }

            txtValor.Text = formateado.ToString();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    public Boolean Enabled
    {
        set
        {
            txtValor.Enabled = value;
        }
    }

    protected void txtValor_TextChanged(object sender, EventArgs e)
    {
        if (eventoCambiar != null)
            eventoCambiar(sender, e);
    }

    public bool AutoPostBack_
    {
        set { txtValor.AutoPostBack = value; }
        get { return txtValor.AutoPostBack; }
    }

    public Unit Width
    {
        set { txtValor.Width = value; }
        get { return txtValor.Width; }
    }

    public short TabIndex
    {
        set { txtValor.TabIndex = value; }
        get { return txtValor.TabIndex; }
    }

}