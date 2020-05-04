<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlNumero.ascx.cs" Inherits="ctlNumero" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<link rel="stylesheet" type="text/css" href="~/Styles/Styles.css"/>

<script type="text/javascript">
    function blur7(textbox) {
        var str = textbox.value;
        var formateado = "";
        var posDec = 0;

        str = str.replace(/\./g, "");
        posDec = str.indexOf(",");
        if (posDec > 0) {
            strDec = str.substring(posDec + 1, str.length);
            str = str.substring(0, posDec);
        }

        if (str > 0) {
            str = parseInt(str);
            str = str.toString();

            if (str.length > 12)
            { str = str.substring(0, 12); }

            var long = str.length;
            var cen = str.substring(long - 3, long);
            var mil = str.substring(long - 6, long - 3);
            var mill = str.substring(long - 9, long - 6);
            var milmill = str.substring(0, long - 9);

            if (long > 0 && long <= 3)
            { formateado = parseInt(cen); }
            else if (long > 3 && long <= 6)
            { formateado = parseInt(mil) + "." + cen; }
            else if (long > 6 && long <= 9)
            { formateado = parseInt(mill) + "." + mil + "." + cen; }
            else if (long > 9 && long <= 12)
            { formateado = parseInt(milmill) + "." + mill + "." + mil + "." + cen; }
            else
            { formateado = ""; }
            if (posDec > 0) {
                formateado = formateado + "," + strDec;
            }
        }
        else {
            if (strDec > 0) { formateado = "0," + strDec;  }
            else { formateado = ""; }
        }

        document.getElementById(textbox.id).value = formateado;
    }

</script>
<asp:TextBox ID="txtValor" runat="server" style="text-align: right" onblur = "blur7(this)" 
    MaxLength="15" onprerender="txtValor_PreRender" Width="100px"  ontextchanged="txtValor_TextChanged" AutoPostBack="True"
    CssClass="textbox" ></asp:TextBox>
<asp:FilteredTextBoxExtender ID="txtValor_FilteredTextBoxExtender" 
    runat="server" Enabled="True" FilterType="Custom" TargetControlID="txtValor" 
    ValidChars="0123456789,.-">
</asp:FilteredTextBoxExtender>
<asp:RequiredFieldValidator ID="rfvValor" runat="server" ErrorMessage="Campo Requerido"
    ControlToValidate="txtValor" Display="Dynamic" ForeColor="Red" 
    ValidationGroup="vgGuardar" style="font-size: xx-small" />