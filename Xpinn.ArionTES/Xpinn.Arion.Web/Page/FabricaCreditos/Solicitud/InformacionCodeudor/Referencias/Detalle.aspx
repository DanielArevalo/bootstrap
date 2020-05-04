<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Referncias :." %>
<%@ Register src="../../../../../General/Controles/direccion.ascx" tagname="direccion" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td class="tdI">
                <asp:TextBox ID="txtCodreferencia" runat="server" CssClass="textbox" Enabled="false"
                    Visible="False" />
            </td>
            <td class="tdD">
                <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" Enabled="false"
                    Visible="False" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Tiporeferencia&nbsp;*<br />
                <asp:RadioButtonList ID="rblTipoReferencia" runat="server" 
                               RepeatDirection="Horizontal">
                               <asp:ListItem Value="1">Familiar</asp:ListItem>
                               <asp:ListItem Value="2">Personal</asp:ListItem>
                               <asp:ListItem Value="3">Comercial</asp:ListItem>
                           </asp:RadioButtonList>
            </td>
            <td class="tdD">
                Nombres&nbsp;*<br />
                <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td class="tdI" colspan="2">
                Dirección Domicilio<uc2:direccion 
                    ID="direccion" runat="server" />
                <br />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Parentesco&nbsp;<br />
                <asp:DropDownList ID="ddlParentesco" runat="server" Enabled="False">
                </asp:DropDownList>
            </td>
            <td class="tdD">
                Celular&nbsp;<br />
                <asp:TextBox ID="txtCelular" runat="server" CssClass="textbox" Enabled="false" />
                <br />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Telefono Domicilio&nbsp;<br />
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" Enabled="false" />
            </td>
            <td class="tdD">
                Telefono Oficina<br />
                <asp:TextBox ID="txtTeloficina" runat="server" CssClass="textbox" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                &nbsp;</td>
            <td class="tdD">
                <asp:Panel ID="Panel1" runat="server" Visible="False">
                    Estado
                    <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" Enabled="false" Visible="False" />
                    <br />
                    Codusuverifica&nbsp;<br />
                    <asp:TextBox ID="txtCodusuverifica" runat="server" CssClass="textbox" 
                        Enabled="false" />
                    <br />
                    Fechaverifica&nbsp;<br />
                    <asp:TextBox ID="txtFechaverifica" runat="server" CssClass="textbox" 
                        Enabled="false" />
                    <br />
                    Calificacion&nbsp;<br />
                    <asp:TextBox ID="txtCalificacion" runat="server" CssClass="textbox" 
                        Enabled="false" />
                    <br />
                    Numero_radicacion&nbsp;<br />
                    <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" 
                        Enabled="false" />
                    <br />
                    Observaciones&nbsp;<br />
                    <asp:TextBox ID="txtObservaciones" runat="server" CssClass="textbox" 
                        Enabled="false" />
                </asp:Panel>
    </table>
</asp:Content>
