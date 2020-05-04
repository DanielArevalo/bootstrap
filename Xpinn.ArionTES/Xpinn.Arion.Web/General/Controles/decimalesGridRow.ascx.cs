using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public delegate void txtValor__ActionsDelegate(object sender, EventArgs e);

public partial class decimalesGridRow : System.Web.UI.UserControl
{
    public event txtValor__ActionsDelegate eventoCambiar;

    public void CambiarAncho(int pAncho)
    {
        txtValor.Width = pAncho;
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

    protected void txtValor_PreRender(object sender, EventArgs e)
    {
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
            //posDec = str.IndexOf(",");
            posDec = s == "." ? str.IndexOf(",") : str.IndexOf(".");
            if (posDec > 0)
            {
                strDec = str.Substring(posDec + 1, str.Length - (posDec + 1));
                str = str.Substring(0, posDec);
            }            
            if (str != "" && Convert.ToInt64(str) > 0)
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

                if (posDec > 0)
                {                    
                    formateado = formateado + "," + strDec;
                }

            }
            else { formateado = "0"; }
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


    public bool Habilitado
    {
        get { return txtValor.Enabled; }
        set { txtValor.Enabled = value; }
    }

    public string Validador
    {
        get { return txtValor.ValidationGroup; }
        set { txtValor.ValidationGroup = value; }
    }

    public bool Requerido
    {
        set { rfvValor.Enabled = value; }
    }

    public FontUnit TipoLetra
    {
        get { return txtValor.Font.Size; }
        set { txtValor.Font.Size = value; }
    }

    public bool AutoPostBack_
    {
        get { return txtValor.AutoPostBack; }
        set { txtValor.AutoPostBack = value; }
    }

    public Unit Width_
    {
        get { return txtValor.Width; }
        set { txtValor.Width = value; }
    }

    public Decimal Valor
    {        
        get {
            decimal valorDec = 0;
            string strEnt = txtValor.Text.Replace(".", "");
            string strDec = "";
            int posDec = strEnt.IndexOf(",");
            if (posDec > 0)
            {
                try
                {
                    strDec = strEnt.Substring(posDec+1, strEnt.Length-posDec-1);
                    strEnt = strEnt.Substring(0, posDec);
                    if (strEnt != "" && Convert.ToDecimal(strEnt) > 0)
                        valorDec = Convert.ToDecimal(strEnt);
                    if (strDec != "" && Convert.ToDecimal(strDec) > 0 && Convert.ToDecimal(Math.Pow(10, strDec.Length)) != 0)
                        valorDec += Convert.ToDecimal(strDec) / Convert.ToDecimal(Math.Pow(10, strDec.Length));
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
            else
            {
                valorDec = Convert.ToDecimal(strEnt);
            }
            return valorDec;
        }        
    }

}