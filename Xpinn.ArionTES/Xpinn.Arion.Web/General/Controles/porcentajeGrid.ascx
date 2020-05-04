<%@ Control Language="C#" AutoEventWireup="true" CodeFile="porcentajeGrid.ascx.cs" Inherits="porcentajeGrid" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<script type="text/javascript">
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
            { formateado = "0"; }
        }
        else { formateado = "0"; }
        document.getElementById(textbox.id).value = formateado;
    }


</script>

<asp:TextBox ID="txtPorcentaje" runat="server" style="text-align: right" onblur = "blur7(this)" 
    MaxLength="3" onprerender="txtPorcentaje_PreRender" Width="50px" ontextchanged="txtPorcentaje_TextChanged" AutoPostBack="True"
    CssClass="textbox" ></asp:TextBox>

<asp:FilteredTextBoxExtender ID="txtPorcentaje_FilteredTextBoxExtender" 
    runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtPorcentaje" 
    ValidChars=".">
</asp:FilteredTextBoxExtender>

<asp:MaskedEditExtender ID="txtPorcentaje_MaskedEditExtender"  TargetControlID="txtPorcentaje" Mask="99.99" runat="server"
    MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
    MaskType="Number" InputDirection="RightToLeft" AcceptNegative="Left" DisplayMoney="None" ErrorTooltipEnabled="True"/>

<asp:MaskedEditValidator runat="server"
    ControlExtender="txtPorcentaje_MaskedEditExtender" ControlToValidate="txtPorcentaje" IsValidEmpty="False" 
    MaximumValue="100" EmptyValueMessage="Se requiere un número" InvalidValueMessage="El número es invalido"
    MaximumValueMessage="Number > 100" MinimumValueMessage="Number < -100"  MinimumValue="-100" 
    EmptyValueBlurredText="*" InvalidValueBlurredMessage="*" MaximumValueBlurredMessage="*" 
    MinimumValueBlurredText="*" Display="Dynamic" TooltipMessage="Ingrese un número entre -100 a 100"/>



