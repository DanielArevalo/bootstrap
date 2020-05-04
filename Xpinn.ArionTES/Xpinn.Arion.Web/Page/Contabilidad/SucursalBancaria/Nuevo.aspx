<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Concepto :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td class="tdI" style="text-align:left">
                Código<br/>
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" />
            </td>
            <td class="tdD">
                <asp:TextBox ID="txtConsecutivo" runat="server" CssClass="textbox" Visible="false" />
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                Nombre<br />
                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" MaxLength="128" 
                    Width="574px" />
                <asp:RequiredFieldValidator ID="rfvNOMBRE" runat="server" 
                    ControlToValidate="txtNombre" Display="Dynamic" 
                    ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                    ValidationGroup="vgGuardar" style="font-size: x-small" />
            </td>
            <td class="tdD" style="text-align:left">
                <br/>
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                Banco<br />
                <asp:DropDownList ID="ddlBanco" runat="server" CssClass="textbox" 
                    Width="320px" style="text-align:left" AutoPostBack="True" AppendDataBoundItems="True">
                    <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvBanco" runat="server" 
                    ControlToValidate="ddlBanco" Display="Dynamic" 
                    ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                    ValidationGroup="vgGuardar" style="font-size: xx-small" 
                    InitialValue="&lt;Seleccione un Item&gt;" />
            </td>
            <td class="tdD" style="text-align:left">
                <br/>
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                Ciudad<br />
                <asp:DropDownList ID="ddlCiudad" runat="server" CssClass="textbox" 
                    Width="320px" style="text-align:left" AutoPostBack="True" AppendDataBoundItems="True">
                    <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvCiudad" runat="server" 
                    ControlToValidate="ddlCiudad" Display="Dynamic" 
                    ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                    ValidationGroup="vgGuardar" style="font-size: xx-small" 
                    InitialValue="&lt;Seleccione un Item&gt;" />
            </td>
            <td class="tdD" style="text-align:left">
                <br/>
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                Dirección<br />
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="textbox" MaxLength="128" 
                    Width="574px" />
                <asp:RequiredFieldValidator ID="rfvDireccion" runat="server" 
                    ControlToValidate="txtDireccion" Display="Dynamic" 
                    ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                    ValidationGroup="vgGuardar" style="font-size: x-small" />
            </td>
            <td class="tdD" style="text-align:left">
                <br/>
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                Código Interno<br/>
                <asp:TextBox ID="txtCodInt" runat="server" CssClass="textbox" />
                <asp:FilteredTextBoxExtender ID="ftbTelefono" 
                    runat="server" Enabled="True" FilterType="Numbers" 
                    TargetControlID="txtCodInt">
                </asp:FilteredTextBoxExtender>
            </td>
            <td class="tdD" style="text-align:left">
                <br/>
            </td>
        </tr>
    </table>
</asp:Content>