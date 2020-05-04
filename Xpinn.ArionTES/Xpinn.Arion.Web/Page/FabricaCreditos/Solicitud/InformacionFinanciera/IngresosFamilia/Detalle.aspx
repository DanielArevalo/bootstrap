<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitudSub.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - IngresosFamilia :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td class="tdI">
                Cod_ingreso&nbsp;*<br />
                <asp:TextBox ID="txtCod_ingreso" runat="server" CssClass="textbox" Enabled="false" />
            </td>
            <td class="tdD">
                Ingresos&nbsp;<br />
                <asp:TextBox ID="txtIngresos" runat="server" CssClass="textbox" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Negocio&nbsp;<br />
                <asp:TextBox ID="txtNegocio" runat="server" CssClass="textbox" Enabled="false" />
            </td>
            <td class="tdD">
                Conyuge&nbsp;<br />
                <asp:TextBox ID="txtConyuge" runat="server" CssClass="textbox" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Hijos&nbsp;<br />
                <asp:TextBox ID="txtHijos" runat="server" CssClass="textbox" Enabled="false" />
            </td>
            <td class="tdD">
                Arriendos&nbsp;<br />
                <asp:TextBox ID="txtArriendos" runat="server" CssClass="textbox" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Pension&nbsp;<br />
                <asp:TextBox ID="txtPension" runat="server" CssClass="textbox" Enabled="false" />
            </td>
            <td class="tdD">
                Otros&nbsp;<br />
                <asp:TextBox ID="txtOtros" runat="server" CssClass="textbox" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Cod_persona&nbsp;*<br />
                <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" Enabled="false" />
            </td>
            <td class="tdD">
                &nbsp;
            </td>
    </table>
</asp:Content>
