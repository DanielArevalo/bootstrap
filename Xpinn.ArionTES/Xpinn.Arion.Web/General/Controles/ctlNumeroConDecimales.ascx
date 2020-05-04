<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlNumeroConDecimales.ascx.cs" Inherits="ctlNumeroConDecimales" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<script type="text/javascript">
    function Blur(textbox) {
        var str = textbox.value;
        var posDec = 0;
        var strDec = "";
        var formateado = "";
        str = str.replace(/\./g, "");
        posDec = str.indexOf(",");
        if (posDec > 0) {
            strDec = str.substring(posDec + 1, str.length);
            str = str.substring(0, posDec);
        }

        if (str != 0) {
            str = parseFloat(str);
            str = str.toString();

            if (str.length > 10)
            { str = str.substring(0, 10); }

            var long = str.length;
            var cen = str.substring(long - 3, long);
            var mil = str.substring(long - 6, long - 3);
            var mill = str.substring(0, long - 6);

            if (long > 0 && long <= 3)
            { formateado = parseInt(cen); }
            else if (long > 3 && long <= 6)
            { formateado = parseInt(mil) + "." + cen; }
            else if (long > 6 && long <= 10)
            { formateado = parseInt(mill) + "." + mil + "." + cen; }
            else
            { formateado = ""; }

            if (posDec > 0 && formateado != "") {
                formateado = formateado + "," + strDec;
            }
        }
        else { formateado = ""; }
        document.getElementById(textbox.id).value = formateado;
    }
</script>

<asp:TextBox ID="txtValor" runat="server" Style="font-size: xx-small;text-align: right" AutoPostBack="True"
    onblur="Blur(this)" OnPreRender="txtValor_PreRender" Width="80px" Enabled="True" CssClass="textbox" />
<asp:FilteredTextBoxExtender ID="txtValor_FilteredTextBoxExtender" runat="server"
    Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtValor" ValidChars=".,">
</asp:FilteredTextBoxExtender>
<asp:RequiredFieldValidator ID="rfvValor" runat="server" ErrorMessage="Campo Requerido"
    ControlToValidate="txtValor" Display="Dynamic" ForeColor="Red" 
    ValidationGroup="vgGuardar" style="font-size: xx-small" />