<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="ListaScoring.aspx.cs" Inherits="ListaScoring" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <table border="0" style="height: 84px; width: 80%" cellspan="1">
        <tr>
            <td style="width: 209px; height: 51px;" class="logo">
                <asp:Label ID="lblCodigoPersona"
                    runat="server" Text="Codigo Persona"></asp:Label>
                <br />
                <asp:TextBox ID="txtCodPersona" CssClass="textbox" onkeypress="return isNumber(event)" runat="server" Width="180px">
                </asp:TextBox>
            </td>
            <td style="width: 209px; height: 51px;" class="logo">
                <asp:Label ID="lblIdentificacion"
                    runat="server" Text="Identificación"></asp:Label>
                <br />
                <asp:TextBox ID="txtIdentificacion" CssClass="textbox" onkeypress="return isNumber(event)" runat="server" Width="180px">
                </asp:TextBox>
            </td>
            <td align="left" class="logo"
                style="width: 197px; height: 51px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="LabelMonto" runat="server" Text="Monto"></asp:Label>
                <br />
                <asp:Label ID="LabelInicial" runat="server" Text="Inicial"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtMontoIni" onkeypress="return isNumber(event)" CssClass="textbox" runat="server"
                    Width="100px" />
            </td>
            <td style="width: 73px; height: 51px;">
                <asp:Label ID="LabelPlazo" runat="server" Text="Plazo"></asp:Label>
                <br />
                <asp:TextBox ID="txtPlazoIni" onkeypress="return isNumber(event)" MaxLength="2" CssClass="textbox"
                    runat="server" Width="31px" />
            </td>
        </tr>
        <tr>
            <td style="width: 209px; height: 51px;" class="logo">
                <asp:Label ID="lblNombrePersona"
                    runat="server" Text="Primer Nombre"></asp:Label>
                <br />
                <asp:TextBox ID="txtNombrePersona" CssClass="textbox" onkeypress="return isOnlyLetter(event)" runat="server" Width="180px">
                </asp:TextBox>
            </td>
            <td style="width: 209px; height: 51px;" class="logo">
                <asp:Label ID="lblApellidoPersona"
                    runat="server" Text="Primer Apellido"></asp:Label>
                <br />
                <asp:TextBox ID="txtApellido" CssClass="textbox" onkeypress="return isOnlyLetter(event)" runat="server" Width="180px">
                </asp:TextBox>
            </td>
            <td class="logo" style="width: 197px" align="left">
                <br />
                <asp:Label ID="LabelFinal" runat="server" Text="Final "></asp:Label>
                &nbsp;&nbsp;&nbsp; &nbsp;<asp:TextBox ID="txtMontoMax" onkeypress="return isNumber(event)" CssClass="textbox" runat="server" Width="100px" />
            </td>
            <td style="width: 73px">
                <br />
                <asp:TextBox ID="txtPlazoMax" onkeypress="return isNumber(event)" CssClass="textbox" MaxLength="2" runat="server" Width="31px" />
        </tr>
        <tr>
            <td style="width: 209px; height: 51px;" class="logo">
                <asp:Label ID="lblCodNomina" runat="server" Text="Código de nómina"></asp:Label>
                <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="180px" />
                <br />
        </tr>
    </table>
    <br />
    <br />
    <table border="0" style="height: 84px; width: 891px">
        <tr>
            <td>
                <asp:GridView ID="gvScoring" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                    OnPageIndexChanging="gvScoring_PageIndexChanging" AllowPaging="True" OnRowEditing="gvScoring_RowEditing"
                    PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="idpreanalisis">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_info.jpg" ToolTip="Ver" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación" />
                        <asp:BoundField DataField="cod_persona" HeaderText="Cod. Persona" />
                        <asp:BoundField DataField="nombre_completo" HeaderText="Nombre" />
                        <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina" />
                        <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="${0:#,##0.00}">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="plazo" HeaderText="Plazo" />
                        <asp:BoundField DataField="fecha_score" DataFormatString="{0:d}" HeaderText="Fecha Score" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblNumeroRegistros" runat="server"></asp:Label>
            </td>
        </tr>

    </table>
    <asp:Label ID="lblAvisoNoResultadoGrilla" Visible="false" runat="server" Text="No hay resultados para la busqueda"></asp:Label>
</asp:Content>

