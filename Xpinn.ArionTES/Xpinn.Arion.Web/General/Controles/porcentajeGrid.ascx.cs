using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public delegate void txtPorcentaje_ActionsDelegate(object sender, EventArgs e);

public partial class porcentajeGrid : System.Web.UI.UserControl
{
    public event txtPorcentaje_ActionsDelegate eventoCambiar;

    protected void Page_Load(object sender, EventArgs e)
    {


    }

    public string Text
    {
        get
        {
            return txtPorcentaje.Text.Trim().ToUpper();
        }
        set
        {
            txtPorcentaje.Text = value;
        }


    }

    protected void txtPorcentaje_PreRender(object sender, EventArgs e)
    {
        string str = txtPorcentaje.Text;
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
            if (str != "" && str.ToLower() != "null" && Convert.ToInt64(str) > 0)
            {

                var strI = Convert.ToInt64(str);  //Convierte a entero y luego a string para quitar ceros a la izquierda
                str = strI.ToString();

                if (str.Length > 9)
                { str = str.Substring(0, 9); }

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
                else if (longi > 6 && longi <= 9)
                {
                    mill = str.Substring(0, longi - 6);
                    mil = str.Substring(longi - 6, 3);
                    cen = str.Substring(longi - 3, 3);
                    formateado = Convert.ToInt64(mill) + "." + mil + "." + cen;
                }
                else
                { formateado = "0"; }
            }
            else { if (str.ToLower() != "null") formateado = "0"; else formateado = ""; }

            txtPorcentaje.Text = formateado.ToString();
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
            txtPorcentaje.Enabled = value;
        }
    }

    protected void txtPorcentaje_TextChanged(object sender, EventArgs e)
    {
        if (eventoCambiar != null)
            eventoCambiar(sender, e);
    }


}