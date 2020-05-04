<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Procesos :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="5" cellspacing="0" style="width: 31%" >
        <tr>
            <td class="tdI" style="text-align:left">
            Código*&nbsp;<br />
            <asp:TextBox ID="txtCodProceso" runat="server" CssClass="textbox" MaxLength="128" 
                Enabled="False" Width="280px" />
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                Tipo de Proceso*&nbsp;<br />
                <asp:DropDownList ID="ddlTipoProceso" runat="server" CssClass="dropdown" 
                    Height="28px" Width="290px" AutoPostBack="True" 
                    onselectedindexchanged="ddlTipoProceso_SelectedIndexChanged">
                    <asp:ListItem Value="0">Seleccione un Item</asp:ListItem>
                    <asp:ListItem Value="1">Automatico</asp:ListItem>
                    <asp:ListItem Value="2">Manual</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                <asp:Label ID="LblDescripcion" runat="server" Text="Descripción Estado *"></asp:Label>
                <br />
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" Width="290px" Visible="False" />                
                <asp:DropDownList ID="ddlEstados" runat="server" CssClass="dropdown" 
                    Height="28px" Width="290px" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left;">
                Antecesor&nbsp;*&nbsp;
                <asp:RequiredFieldValidator ID="rfvAntecesor" runat="server" 
                    ControlToValidate="ddlAntecesor" ErrorMessage="Campo Requerido" 
                    SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" 
                    Display="Dynamic"/><br />
                <asp:DropDownList ID="ddlAntecesor" runat="server" CssClass="dropdown" Height="28px" Width="290px" AppendDataBoundItems="true" />
                <br />
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                &nbsp;
            </td>
        </tr>
        </table>
</asp:Content>