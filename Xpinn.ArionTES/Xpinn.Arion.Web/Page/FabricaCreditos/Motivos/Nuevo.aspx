<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
 <asp:Panel ID="pConsulta" runat="server">
        <br />
        <br />
        <br />
    <table cellpadding="5" cellspacing="0" style="width: 100%">
    <tr>
        <td>
            Descripción del motivo: &nbsp;<br />
            <asp:TextBox ID="txtMotivo" runat="server" CssClass="textbox" />
            &nbsp;<asp:RequiredFieldValidator ID="rfvMotivo" runat="server" 
                ControlToValidate="txtMotivo" Display="Dynamic" 
                ErrorMessage="Especifique una descripción del motivo" ForeColor="Red" 
                ValidationGroup="vgGuardar"><strong>*</strong></asp:RequiredFieldValidator>
        </td>
        <td>
            &nbsp; Tipo de motivo:<br />
            <asp:DropDownList ID="ddlTipo" runat="server" CssClass="dropdown" Width="200px">
                <asp:ListItem Value="0">&lt;Seleccione un tipo&gt;</asp:ListItem>
                <asp:ListItem Value="1">Aplazamiento</asp:ListItem>
                <asp:ListItem Value="2">Negación</asp:ListItem>
            </asp:DropDownList>
            &nbsp;<asp:CompareValidator ID="cvTipo" runat="server" ControlToValidate="ddlTipo" 
                Display="Dynamic" ErrorMessage="Seleccione un tipo de motivo" ForeColor="Red" 
                Operator="GreaterThan" SetFocusOnError="true" 
                Text="&lt;strong&gt;*&lt;/strong&gt;" Type="Integer" 
                ValidationGroup="vgGuardar" ValueToCompare="0"></asp:CompareValidator>
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

