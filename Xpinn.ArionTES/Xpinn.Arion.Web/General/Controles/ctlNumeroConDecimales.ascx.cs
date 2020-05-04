using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public delegate void txtValorCambio_ActionsDelegate(object sender, EventArgs e);

public partial class ctlNumeroConDecimales : System.Web.UI.UserControl
{
    public event txtValorCambio_ActionsDelegate eventoCambiar;

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
                var strI = Convert.ToInt64(str);  //Convierte a entero y luego a string para quitar ceros a la izquierda
                str = strI.ToString();

                if (str.Length > 10)
                { str = str.Substring(0, 10); }

                int longi = str.Length;
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
                else if (longi > 6 && longi <= 10)
                {
                    mill = str.Substring(0, longi - 6);
                    mil = str.Substring(longi - 6, 3);
                    cen = str.Substring(longi - 3, 3);
                    formateado = Convert.ToInt64(mill) + "." + mil + "." + cen;
                }
                else
                { formateado = ""; }

                if (posDec > 0 && formateado != "")
                {
                    formateado = formateado + "," + strDec;
                }

            }
            else { formateado = ""; }
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

    public Boolean Enabled
    {
        set { Habilitado = value; }
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