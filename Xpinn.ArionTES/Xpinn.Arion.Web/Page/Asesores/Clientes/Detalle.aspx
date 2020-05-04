<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="AseClienteDetalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center; color: #FFFFFF; background-color: #0066FF" colspan="5">
                <strong style="text-align: center">Información Cliente</strong>
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td>
                Primer Nombre<br />
                <asp:TextBox ID="txtPrimerNombre" runat="server" CssClass="textbox" Enabled="false"
                    Width="50%"></asp:TextBox>
            </td>
            <td>
                Segundo Nombre<br />
                <asp:TextBox ID="txtSegundoNombre" runat="server" CssClass="textbox" Enabled="false"
                    Width="50%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Primer Apellido<br />
                <asp:TextBox ID="txtPrimerApellido" runat="server" CssClass="textbox" Enabled="false"
                    Width="50%"></asp:TextBox>
            </td>
            <td>
                Segundo Apellido<br />
                <asp:TextBox ID="txtSegundoApellido" runat="server" CssClass="textbox" Enabled="false"
                    Width="50%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Tipo Documento<br />
                <asp:TextBox ID="txtTipoDoc" runat="server" CssClass="textbox" Enabled="false" Width="50%"></asp:TextBox>
            </td>
            <td>
                Número Documento<br />
                <asp:TextBox ID="txtNumDoc" runat="server" CssClass="textbox" Enabled="false" Width="50%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Teléfono<br />
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" Enabled="false" Width="50%"></asp:TextBox>
            </td>
            <td>
                Dirección<br />
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="textbox" Enabled="false"
                    Width="50%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Zona<br />
                <asp:TextBox ID="txtZonac" runat="server" CssClass="textbox" Enabled="false" Width="50%"></asp:TextBox>
            </td>
            <td>
                Correo Electrónico<br />
                <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Enabled="false" Width="50%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td style="text-align: center; color: #FFFFFF; background-color: #0066FF" colspan="5">
                <strong style="text-align: center">Información Empresa</strong>
            </td>
        </tr>
        <tr>
            <td >
                Razón Social<br />
                <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="textbox" Enabled="false"
                    Width="50%"></asp:TextBox>
            </td>
            <td>
                Sigla<br />
                <asp:TextBox ID="txtSigla" runat="server" CssClass="textbox" Enabled="false" Width="50%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" >
                Actividad Económica<br />
                <asp:TextBox ID="txtActividad" runat="server" CssClass="textbox" Enabled="false"
                    Width="50%"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>
