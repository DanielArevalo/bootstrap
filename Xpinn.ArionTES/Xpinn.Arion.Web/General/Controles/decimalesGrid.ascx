<%@ Control Language="C#" AutoEventWireup="true" CodeFile="decimalesGrid.ascx.cs" Inherits="decimalesGrid" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<script>
    function blur7(textbox) {
        var str = textbox.value;
        var formateado = "";
        str = str.replace(/\./g, "");

        if (str > 0) {
            str = parseInt(str);
            str = str.toString();

            if (str.length > 9)
            { str = str.substring(0, 9); }

            var long = str.length;
            var cen = str.substring(long - 3, long);
            var mil = str.substring(long - 6, long - 3);
            var mill = str.substring(0, long - 6);
                        
            if (long > 0 && long <= 3)
            { formateado = parseInt(cen); }
            else if (long > 3 && long <= 6)
            { formateado = parseInt(mil) + "." + cen; }
            else if (long > 6 && long <= 9)
            { formateado = parseInt(mill) + "." + mil + "." + cen; }
            else 
            {formateado = "0"; }
        }
        else {formateado = "0";}
        document.getElementById(textbox.id).value = formateado;
    }

    
</script>
<asp:TextBox ID="txtValor" runat="server" style="text-align: right" onblur = "blur7(this)" 
    MaxLength="11" onprerender="txtValor_PreRender" Width="150px" 
    Font-Size="Small" BackColor="#F4F5FF" ></asp:TextBox>

<asp:FilteredTextBoxExtender ID="txtValor_FilteredTextBoxExtender" 
    runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtValor" 
    ValidChars=".">
</asp:FilteredTextBoxExtender>


