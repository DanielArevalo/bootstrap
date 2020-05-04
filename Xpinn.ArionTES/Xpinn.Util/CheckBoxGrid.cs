using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.ComponentModel;

/// <summary>
/// Descripción breve de CheckBoxGrid
/// </summary>
namespace Xpinn.Util
{
    public class CheckBoxGrid : CheckBox
    {
        //
        // La creacion del custom control es justamente para agregar esta propiedad 
        // que por defecto no posee el RadioButtonList
        //
        [DefaultValue("")]
        public string CommandArgument
        {
            get
            {
                string s = ViewState["CommandArgument"] as string;
                return s == null ? String.Empty : s;
            }
            set
            {
                ViewState["CommandArgument"] = value;
            }
        }

    }
}