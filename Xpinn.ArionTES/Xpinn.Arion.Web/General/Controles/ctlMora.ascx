<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlMora.ascx.cs" Inherits="ctlMora" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<style>
    .modalBackground {
        background-color: black;
        opacity: 0.7;
    }

    .pnlBackGround {
        background-color: white;
        width: 820px;
    }
</style>

<table>
    <tr>
        <%--<td style="text-align: left">Identificación</td>--%>
        <td></td>
        <td style="text-align: left">Total Adeudado
        </td>
        <td style="text-align: left">Fecha Corte
        </td>
    </tr>
    <tr>
        <td style="text-align: left">
            <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="30px" Visible="false"
                Style="text-align: left" />
            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="110px" AutoPostBack="true"
                Style="text-align: left" Visible="false" />
            <asp:RequiredFieldValidator ID="rfvIdentificacion" runat="server" ErrorMessage="Debe ingresar la identificación"
                ControlToValidate="txtIdentificacion" Font-Size="XX-Small" ValidationGroup="vgGuardar" Style="color: Red"
                Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
        <td style="text-align: left">
            <asp:TextBox ID="txtMora" runat="server" CssClass="textbox" Width="110px" Style="text-align: left" Enabled="true" />
        </td>
        <td style="text-align: left">
            <asp:TextBox ID="txtFechaCorte" runat="server" CssClass="textbox" Width="110px" Style="text-align: left" Enabled="true" />

            <asp:CalendarExtender ID="ceFechaCorte" runat="server"
                DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy"
                TargetControlID="txtFechaCorte" TodaysDateFormat="dd/MM/yyyy">
            </asp:CalendarExtender>
            <asp:RequiredFieldValidator ID="rfvFechaPago" runat="server" ErrorMessage="Debe ingresar la fecha"
                ControlToValidate="txtFechaCorte" Font-Size="XX-Small" ValidationGroup="vgConsultar" Style="color: Red"
                Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
        <td style="text-align: left">
            <asp:Button ID="btnImpMoras" runat="server" CssClass="btn8" Height="26px" OnClick="btnImpMora_Click" onchange="formaternumber(this.value)" Text="Consultar" ValidationGroup="vgConsultar" />
        </td>
        <td style="text-align: left">
            <asp:Button ID="Imprimir" runat="server" CssClass="btn8" Height="26px" OnClick="btnImprimir_Click2" Text="Imprimir" ValidationGroup="vgConsultar" />
        </td>
    </tr>
</table>

<asp:HiddenField ID="HiddenField1" runat="server" />
<asp:ModalPopupExtender ID="mpeMostrar" runat="server" PopupControlID="panelAgregarLink"
    TargetControlID="HiddenField1" BackgroundCssClass="modalBackground">
</asp:ModalPopupExtender>
<asp:Panel ID="panelAgregarLink" runat="server" BackColor="White" CssClass="pnlBackGround">
    <asp:UpdatePanel ID="updEmergente" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <asp:Button ID="btnClose" runat="server" Text="Cerrar" CssClass="btn8" OnClick="btnClose_Click" />
                <br />
                <rsweb:ReportViewer ID="RpviewInfo1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                    Height="500px" WaitMessageFont-Names="Verdana" ShowPrintButton="true"
                    WaitMessageFont-Size="14px" Width="820px" BackColor="White">
                    <LocalReport ReportPath="Page\Asesores\EstadoCuenta\RptMoras.rdlc" runat="server">
                    </LocalReport>
                </rsweb:ReportViewer>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Panel>
<script>
    $(document).ready(function () { formaternumber($("#cphMain_ctlMora_txtMora").val()); });

    // Formatear Numero Regional 
    $("#cphMain_ctlMora_txtMora").change(function () {
        formaternumber(this.value);
    });
    function formaternumber(number) {
        
        var id = document.getElementById("txtMora");
        $("#cphMain_ctlMora_txtMora").val(new Intl.NumberFormat('es-MX').format(parseFloat(number.replace(/\,/g, ""))));
    }
</script>
