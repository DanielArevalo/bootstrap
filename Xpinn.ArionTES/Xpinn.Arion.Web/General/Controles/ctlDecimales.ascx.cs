using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public delegate void txtValorCambiado_ActionsDelegate(object sender, EventArgs e);

public partial class ctlDecimales : System.Web.UI.UserControl
{
    public event txtValorCambiado_ActionsDelegate eventoCambiar;

    protected void Page_Load(object sender, EventArgs e)
    {
        rfvValor.Enabled = false;
    }

    public string Text
    {
        get { return txtValor.Text.Trim().ToUpper(); }
        set { txtValor.Text = value; }
    }

    protected void txtValor_TextChanged(object sender, EventArgs e)
    {
        if (eventoCambiar != null)
            eventoCambiar(sender, e);
    }

    protected void txtValor_PreRender(object sender, EventArgs e)
    {
        TextBox txtValor = (TextBox)sender;
        string str = txtValor.Text;
        string strDec = "";
        int posDec = 0;
        string formateado = "";

        string s = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        if (s == ".")
            str = str.Replace(",", "");
        else
        {
            str = str.Replace(".", "");
            str = str.Replace(",", ".");
        }

        try
        {
            posDec = s == "." ? str.IndexOf(",") : str.IndexOf(".");
            if (posDec > 0)
            {
                strDec = str.Substring(posDec + 1, str.Length - (posDec + 1));
                str = str.Substring(0, posDec);
            }
            if (str != "" && (Convert.ToInt64(str) > 0 || (Convert.ToInt64(str) == 0 && Convert.ToInt64(strDec) > 0)))
            {
                var strI = Convert.ToInt64(str);  
                str = strI.ToString();
                if (posDec > 0)
                {
                    formateado = str + "," + strDec;
                }
                else
                {
                    formateado = str + ",0";
                }
            }
            else
            {
                formateado = "0," + strDec;
            }
            txtValor.Text = formateado.ToString();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    public Boolean Habilitado
    {
        set { txtValor.Enabled = value; }
    }

    public bool Requerido
    {
        set { rfvValor.Enabled = value; }
    }

    public Int64 Largo
    {
        set { rfvValor.Attributes.Add("Width", value.ToString()); }
    }

}