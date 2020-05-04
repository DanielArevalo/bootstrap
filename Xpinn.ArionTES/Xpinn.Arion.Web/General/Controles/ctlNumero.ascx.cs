using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public delegate void txtValorNum_ActionsDelegate(object sender, EventArgs e);

public partial class ctlNumero : System.Web.UI.UserControl
{
    public event txtValorNum_ActionsDelegate eventoCambiar;

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
        string str = txtValor.Text;
        string formateado = "";

        string s = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
        if (s == ",")
            str = str.Replace(",", "");
        else
        {
            str = str.Replace(",", "");
            str = str.Replace(".", ",");
        }

        try
        {
            if (str != "" && str.ToLower() != "null" && Convert.ToInt64(str) > 0)
            {

                var strI = Convert.ToInt64(str);  //Convierte a entero y luego a string para quitar ceros a la izquierda
                str = strI.ToString();

                if (str.Length > 15)
                { str = str.Substring(0, 15); }
                
                int longi = str.Length;
                string milmill = "";
                string mill = "";
                string mil = "";
                string cen = "";
                string deci = "";

                if (longi > 0 && longi <= 1)
                {
                    deci = str.Substring(0, longi);
                    formateado = Convert.ToInt64(deci).ToString();
                }
                if (longi > 0 && longi <= 3)
                {
                    cen = str.Substring(0, longi);
                    deci = str.Substring(longi - 0, 3);
                    formateado = Convert.ToInt64(cen) + "," + deci;
                }
                else if (longi > 3 && longi <= 6)
                {
                    mil = str.Substring(0, longi - 3);
                    cen = str.Substring(longi - 3, 3);
                    deci = str.Substring(longi - 0, 3);
                    formateado = Convert.ToInt64(mil) + "," + cen;
                }
                else if (longi > 6 && longi <= 9)
                {
                    mill = str.Substring(0, longi - 6);
                    mil = str.Substring(longi - 6, 3);
                    cen = str.Substring(longi - 3, 3);
                    deci = str.Substring(longi - 0, 3);
                    formateado = Convert.ToInt64(mill) + "," + mil + "," + cen;
                }
                else if (longi > 9 && longi <= 12)
                {
                    milmill = str.Substring(0, longi - 9);
                    mill = str.Substring(longi - 9, 3);
                    mil = str.Substring(longi - 6, 3);
                    cen = str.Substring(longi - 3, 3);
                    deci = str.Substring(longi - 0, 3);
                    formateado = Convert.ToInt64(milmill) + "," + mill + "," + mil + "," + cen + "," + deci;
                }
                else
                { formateado = ""; }
            }
            else { if (str.ToLower() != "null") formateado = ""; else formateado = ""; }

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

    public bool AutoPostBack_
    {
        set { txtValor.AutoPostBack = value; }
        get { return txtValor.AutoPostBack; }
    }

}