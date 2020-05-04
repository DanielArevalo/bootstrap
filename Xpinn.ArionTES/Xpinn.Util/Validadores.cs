using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;

namespace Xpinn.Util
{
    public class Validadores
    {
        public Validadores()
        {
        }


        public void LimpiarPanel(Panel plControles)
        {
            try
            {
                foreach (Control control in plControles.Controls)
                {
                    if (control is TextBox)
                        ((TextBox)(control)).Text = "";
                    else if (control is DropDownList)
                        ((DropDownList)(control)).SelectedIndex = 0;
                    else if (control is RadioButton)
                        ((RadioButton)(control)).Checked = false;
                    else if (control is CheckBox)
                        ((CheckBox)(control)).Checked = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsValidEmail(string strMailAddress)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strMailAddress, @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }

        public bool IsValidNumber(string strNumber)
        {
            // Return true if strIn is in valid Number format.
            return System.Text.RegularExpressions.Regex.IsMatch(strNumber, "^[0-9]*$");
        }

    }
}
