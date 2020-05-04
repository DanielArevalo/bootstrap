<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlProcesoContable.ascx.cs" Inherits="ctlProcesoContable" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<style type="text/css">
    .ocultar
    {
        display: none;
    }
</style>

<script type="text/javascript">

    function deshabilitar(boton) {
        //document.getElementById(boton).disabled = true;
        document.getElementById(boton).style.display = 'none';
        document.getElementById('<%= btnClone.ClientID %>').style.display = 'inline';
    }
</script>

<asp:Panel ID="PanelProceso" runat="server">
    <table style="width: 100%;">
        <tr>
            <td style="text-align: left" colspan="3">
                <asp:Label ID="lblError" runat="server" style="text-align: left" Width="100%" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td style="text-align: center">
                <br />
                <br />
                <br />
                <br />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td style="text-align: center">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td style="text-align: center">
                Por favor seleccione el tipo de comprobante y concepto deseado para el proceso
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td style="text-align: center">
                <asp:Label ID="lblProceso" runat="server" Text=""></asp:Label>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td style="text-align: center">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td style="text-align: center">
                <asp:ListBox ID="lstProcesos" runat="server" Width="396px" Height="143px"></asp:ListBox>
                <br />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td style="text-align: center; font-weight: 700;">
                <asp:ImageButton ID="btnCancelar" runat="server" ImageUrl="~/Images/btnCancelar.jpg"
                    OnClick="btnCancelar_Click" />
                &#160;&#160;&#160;&#160;
                <asp:ImageButton ID="imgAceptarProceso" runat="server" ImageUrl="~/Images/btnAceptar.jpg"
                    OnClick="imgAceptarProceso_Click" OnClientClick="deshabilitar(this.id)" />
                <asp:ImageButton ID="btnClone" runat="server" CssClass="ocultar" Enabled="false" ImageUrl="~/Images/btnAceptar.jpg" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Panel>