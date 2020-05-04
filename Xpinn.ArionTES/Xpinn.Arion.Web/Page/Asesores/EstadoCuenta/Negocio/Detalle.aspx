<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="PageAsesoresEstadoCuentaNegocioDetalle" %>

<%@ Register Src="~/General/Controles/imprimir.ascx" TagName="imprimir" TagPrefix="ucImprimir" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table style="width:100%">
        <tr>
            <td>
                <ucImprimir:imprimir ID="ucImprimir" runat="server"  />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <table style="width: 100%"  runat="server" id="contentTable">
        <tr>
            <td style="text-align: center; width: 38%">
                Número Identificación<br />
                <asp:TextBox ID="txtNumeroIdentificacio" runat="server" CssClass="textbox" 
                    Enabled="False"></asp:TextBox>
            </td>
            <td style="text-align: center; width: 802px">
                Tipo Identificación
                <br />
                <asp:TextBox ID="txtTipoIdentificacion" runat="server" CssClass="textbox" 
                    Enabled="False"></asp:TextBox>
                <br />
            </td>
            <td style="text-align: center; width: 36%">
                Nombre Negocio
                <br />
                <asp:TextBox ID="txtNombreNegocio" runat="server" CssClass="textbox" 
                    Enabled="False" Width="140px"></asp:TextBox>
                <br />
            </td>
        </tr>
        <tr>
            <td style="text-align: center; width: 38%">
                Descripción<br />
                <asp:TextBox ID="txtDescripNegocio" runat="server" CssClass="textbox" 
                    Enabled="False" Width="140px"></asp:TextBox>
            </td>
            <td style="width: 802px; text-align: center">
                Barrio
                <br />
                <asp:TextBox ID="txtBarrioNegocio" runat="server" CssClass="textbox" 
                    Enabled="False" Width="140px"></asp:TextBox>
            </td>
            <td style="width: 36%; text-align: center">
                Teléfono<br />
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: center; width: 38%">
                Dirección<br />
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="textbox" 
                    Enabled="False"></asp:TextBox>
            </td>
            <td style="text-align: center; width: 802px">
                Antigüedad<br />
                <asp:TextBox ID="txtAntiguedad" runat="server" CssClass="textbox" style="text-align: center"
                    Enabled="False"></asp:TextBox>
                <br />
            </td>
            <td style="text-align: center; width: 36%">
                Propio/Arrendado<br />
                <asp:CheckBox ID="ChkPropio" runat="server" Enabled="False" />
                &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="ChkArrend" runat="server" Enabled="False" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center; width: 38%">
                Actividad<br />
                <asp:TextBox ID="txtActividad" runat="server" CssClass="textbox" 
                    Enabled="False"></asp:TextBox>
            </td>
            <td style="text-align: center; width: 802px">
                Arrendador<br />
                <asp:TextBox ID="txtArrendador" runat="server" CssClass="textbox" style="text-align: center"
                    Enabled="False"></asp:TextBox>
            </td>
            <td style="text-align: center; width: 36%">
                Teléfono
                Arrendador<br />
                <asp:TextBox ID="txttelefArrendador" runat="server" CssClass="textbox" 
                    Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: center; width: 38%">
                Experiencia&nbsp;
                Actividad<br />
                <asp:TextBox ID="txtExperActividad" runat="server" CssClass="textbox" style="text-align: center"
                    Enabled="False"></asp:TextBox>
            </td>
            <td style="text-align: center; width: 802px">
                Empleados Temp<br />
                <asp:TextBox ID="txtEmpleadosTempo" runat="server" CssClass="textbox" style="text-align: center"
                    Enabled="False" Width="138px"></asp:TextBox>
            </td>
            <td style="text-align: center; width: 370px">
                Empleados Permanentes<br />
                <asp:TextBox ID="txtEmpleadosPerma" runat="server" CssClass="textbox"  style="text-align: center"
                    Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: center; width: 38%">
                &nbsp;</td>
            <td style="text-align: center; width: 802px">
                &nbsp;</td>
            <td style="text-align: center; width: 36%">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>