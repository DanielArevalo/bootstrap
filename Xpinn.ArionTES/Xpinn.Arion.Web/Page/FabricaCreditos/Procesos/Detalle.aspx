<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Procesos :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" style="width: 31%" >
        <tr>
            <td style="text-align:left">
            Código*&nbsp;<br />
            <asp:TextBox ID="txtCodProceso" runat="server" CssClass="textbox" MaxLength="128" 
                    Enabled="False" Width="287px" />
            </td>
        </tr>
        <tr>
            <td style="text-align:left">
            Descripción&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" 
                    Width="290px" Enabled="False" />
            </td>
        </tr>
        <tr>
            <td style="text-align:left">
                Tipo de Proceso*&nbsp;<asp:RequiredFieldValidator ID="rfvTipoProceso" 
                    runat="server" ControlToValidate="ddlTipoProceso" 
                    ErrorMessage="Campo Requerido" SetFocusOnError="True" 
                    ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                            <asp:DropDownList ID="ddlTipoProceso" runat="server" CssClass="dropdown" 
                                Height="32px" Width="290px" Enabled="False">
                                <asp:ListItem Value="1">Automatico</asp:ListItem>
                                <asp:ListItem Value="2">Manual</asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align:left; height: 58px;">
                Antecesor&nbsp;*&nbsp;
                <asp:RequiredFieldValidator ID="rfvAntecesor" runat="server" 
                    ControlToValidate="ddlAntecesor" ErrorMessage="Campo Requerido" 
                    SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" 
                    Display="Dynamic"/><br />
                            <asp:DropDownList ID="ddlAntecesor" runat="server" CssClass="dropdown" 
                                Height="19px" Width="290px" Enabled="False">
                            </asp:DropDownList>
                <br />
            </td>
        </tr>
        <tr>
            <td style="text-align:left">
                &nbsp;</td>
        </tr>
        </table>
</asp:Content>