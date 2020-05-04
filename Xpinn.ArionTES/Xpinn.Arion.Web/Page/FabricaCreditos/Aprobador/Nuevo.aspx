<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:Panel ID="pConsulta" runat="server">
       <table style="width: 100%;">
            <tr>
                <td colspan="2">
                    <br />
                </td>
            </tr>            
            <tr>
                <td>
                    <asp:Label ID="lblTitOFicina" runat="server" Text="Oficina" Enabled="False" Width="250px" />
                    <asp:Label ID="lblNombreOficina" runat="server" CssClass="textbox" Enabled="False" Width="250px" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Línea de crédito<br />
                    <asp:DropDownList ID="ddlLinea" runat="server" CssClass="dropdown" 
                        Width="250px">
                    </asp:DropDownList>
                    &nbsp;<asp:CompareValidator ID="cvLinea" runat="server" ControlToValidate="ddlLinea" 
                        Display="Dynamic" ErrorMessage="Seleccione una linea de crédito" 
                        ForeColor="Red" Operator="GreaterThan" SetFocusOnError="true" 
                        Text="&lt;strong&gt;*&lt;/strong&gt;" Type="Integer" 
                        ValidationGroup="vgGuardar" ValueToCompare="0">
                    </asp:CompareValidator>
                </td>
                <td>
                    &nbsp; Usuario aprobador<br />
                    <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="dropdown" Width="250px">
                    </asp:DropDownList>
                    &nbsp;<asp:CompareValidator ID="cvUsuario" runat="server" 
                        ControlToValidate="ddlUsuario" Display="Dynamic" 
                        ErrorMessage="Seleccione un usuario" ForeColor="Red" Operator="GreaterThan" 
                        SetFocusOnError="true" Text="&lt;strong&gt;*&lt;/strong&gt;" Type="Integer" 
                        ValidationGroup="vgGuardar" ValueToCompare="-1">
            </asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Nivel<br />
                    <asp:DropDownList ID="ddlNivel" runat="server" CssClass="dropdown" 
                        Width="250px">
                        <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                        <asp:ListItem Value="1">Nivel 1</asp:ListItem>
                        <asp:ListItem Value="2">Nivel 2</asp:ListItem>
                        <asp:ListItem Value="3">Nivel 3</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;<asp:CompareValidator ID="cvNivel" runat="server" ControlToValidate="ddlNivel" 
                        Display="Dynamic" ErrorMessage="Seleccione un nivel" ForeColor="Red" 
                        Operator="GreaterThan" SetFocusOnError="true" 
                        Text="&lt;strong&gt;*&lt;/strong&gt;" Type="Integer" 
                        ValidationGroup="vgGuardar" ValueToCompare="0">
            </asp:CompareValidator>
                </td>
                <td>
                    Aprueba<br />
                    <asp:CheckBox ID="chkAprueba" runat="server" Width="250px" />
                </td>
            </tr>
            <tr>
                <td>
                    Monto mínimo<br />
                    <asp:TextBox ID="txtMinimo" runat="server" CssClass="textbox" Width="250px" />
                    &nbsp;<asp:RequiredFieldValidator ID="rfvMinimo" runat="server" 
                        ControlToValidate="txtMinimo" Display="Dynamic" 
                        ErrorMessage="Especifique monto mínimo" ForeColor="Red" 
                        ValidationGroup="vgGuardar"><strong>*</strong></asp:RequiredFieldValidator>
                    &nbsp;<asp:CompareValidator ID="cvMinimo" runat="server" 
                        ControlToValidate="txtMinimo" Display="Dynamic" 
                        ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" 
                        SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar"><strong>*</strong></asp:CompareValidator>
                </td>
                <td>
                    Monto máximo<br />
                    <asp:TextBox ID="txtMaximo" runat="server" CssClass="textbox" Width="250px" />
                    &nbsp;<asp:RequiredFieldValidator ID="rfvMaximo" runat="server" 
                        ControlToValidate="txtMaximo" Display="Dynamic" 
                        ErrorMessage="Especifique un monto máximo" ForeColor="Red" 
                        ValidationGroup="vgGuardar"><strong>*</strong></asp:RequiredFieldValidator>
                    &nbsp;<asp:CompareValidator ID="cvMaximo" runat="server" 
                        ControlToValidate="txtMaximo" Display="Dynamic" 
                        ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" 
                        SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar"><strong>*</strong></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        DisplayMode="BulletList" ForeColor="Red" HeaderText="Errores:" 
                        ShowMessageBox="false" ShowSummary="true" ValidationGroup="vgGuardar" />
                </td>
            </tr>
        </table>

    </asp:Panel>
    </asp:Content>

