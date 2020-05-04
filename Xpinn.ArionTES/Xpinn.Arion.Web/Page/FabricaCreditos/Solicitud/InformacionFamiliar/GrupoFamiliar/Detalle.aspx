<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table cellpadding="5" cellspacing="0" style="width: 100%">
        <tr>
            <td class="tdI">
                C&oacute;digo Persona*
                <asp:RequiredFieldValidator ID="rfvCodigo" runat="server" ErrorMessage="Campo Requerido"
                    ControlToValidate="txtCodigoPersona" Display="Dynamic" ForeColor="Red" ValidationGroup="vgGuardar" />&nbsp;<br />
                <asp:TextBox ID="txtCodigoPersona" runat="server" CssClass="textbox" 
                    Enabled="False" />
            </td>
            <td class="tdD">
                Nombres *
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="Campo Requerido"
                    ControlToValidate="txtNombres" Display="Dynamic" ForeColor="Red" ValidationGroup="vgGuardar" /><br />
                <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" 
                    Enabled="False" />
            </td>
        </tr>
        <tr>
            <td>
                Parentesco *<br />
                <asp:DropDownList ID="ddlParentesco" runat="server" 
                    Enabled="False">
                </asp:DropDownList>
            </td>
            <td>
                Sexo *<br />
                <asp:RadioButtonList ID="rblSexo" runat="server" RepeatDirection="Horizontal" 
                    CssClass="check" Enabled="False">
                    <asp:ListItem>M</asp:ListItem>
                    <asp:ListItem>F</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                A cargo<asp:RadioButtonList ID="rblAcargo" runat="server" RepeatDirection="Horizontal"
                    CssClass="check" Enabled="False">
                    <asp:ListItem Value="1">Si</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                Observaciones<br />
                <asp:TextBox ID="txtObservaciones" runat="server" CssClass="textbox" 
                    Enabled="False" />
            </td>
        </tr>
    </table>
</asp:Content>
