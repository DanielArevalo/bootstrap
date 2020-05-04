<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="AseEjecutivosDetalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table style="width: 80%">
        <tr>
            <td style="text-align:left" colspan="2">
                Código
                <br />
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
            </td>
            <td style="text-align:left; width: 132px;">
                Tipo Documento
                <br />
                <asp:TextBox ID="txtTipoDoc" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
            </td>
            <td style="text-align:left">
                Número Documento<br />
                <asp:TextBox ID="txtNumeDoc" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
            </td>
            <td style="text-align:left">
            </td>
        </tr>
        <tr>
            <td style="width: 448px; text-align:left" colspan="2">
                Primer Nombre
                <br />
                <asp:TextBox ID="txtPrimerNombre" runat="server" CssClass="textbox" 
                    Enabled="false" Width="420px"></asp:TextBox>
            </td>
            <td style="text-align:left" colspan="2">
                Segundo Nombre<br />
                <asp:TextBox ID="txtSegundoNombre" runat="server" CssClass="textbox" 
                    Enabled="false" Width="420px" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 448px; text-align:left" colspan="2">
                Primer Apellido
                <br />
                <asp:TextBox ID="txtPrimerApellido" runat="server" CssClass="textbox" 
                    Enabled="false" Width="420px"></asp:TextBox>
            </td>
            <td style="text-align:left" colspan="2">
                Segundo Apellido<br />
                <asp:TextBox ID="txtSegundoApellido" runat="server" CssClass="textbox" 
                    Enabled="false" Width="420px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 448px; text-align:left" colspan="2">
                Dirección&nbsp; 
                de Residencia<br />
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="textbox" 
                    Enabled="false" Width="420px"></asp:TextBox>
            </td>
            <td style="text-align:left" colspan="2">
                Barrio<br />
                <asp:TextBox ID="txtBarrio" runat="server" CssClass="textbox" Enabled="false" 
                    Width="420px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 448px; text-align:left">
                Teléfono Residencia
                <br />
                <asp:TextBox ID="txtTeleResi" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
            </td>
            <td style="width: 448px; text-align:left">
                Teléfono Celular<br />
                <asp:TextBox ID="txtCelular" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
            </td>
            <td style="text-align:left" colspan="2">
                Correo Electrónico<br />
                <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Enabled="false" 
                    Width="420px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 448px; text-align:left" colspan="2">
                Zona
                <br />
                <asp:TextBox ID="txtZona" runat="server" CssClass="textbox" Enabled="false" 
                    Width="420px"></asp:TextBox>
            </td>
            <td style="text-align:left" colspan="2">
                Estado
                <br />
                <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 448px; text-align:left" colspan="2">
                Oficina
                <br />
                <asp:TextBox ID="txtOficina" runat="server" CssClass="textbox" Enabled="false" 
                    Width="420px"></asp:TextBox>
            </td>
            <td style="text-align:left" colspan="2">
                Fecha Ingreso<br />
                <asp:TextBox ID="txtFechaIngreso" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>