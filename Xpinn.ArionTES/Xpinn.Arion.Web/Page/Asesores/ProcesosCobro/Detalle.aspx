<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - ProcesosCobro :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI">
            Código proceso cobro<br />
            <asp:TextBox ID="txtCodprocesocobro" runat="server" CssClass="textbox" Enabled="false"/>
            </td>
            <td class="tdD">
            Código proceso precede&nbsp;<br />
            <asp:TextBox ID="txtCodprocesoprecede" runat="server" CssClass="textbox" Enabled="false"/>
            </td>
        </tr>
        <tr>
            <td class="tdI">
            Descripción<br />
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" Enabled="false"/>
            </td>
            <td class="tdD">
                Rango inicial<br />
            <asp:TextBox ID="txtRangoinicial" runat="server" CssClass="textbox" Enabled="false"/>
            </td>
        </tr>
        <tr>
            <td class="tdI">
            Rango final<br />
            <asp:TextBox ID="txtRangofinal" runat="server" CssClass="textbox" Enabled="false"/>
            </td>
            <td class="tdD">
            &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>