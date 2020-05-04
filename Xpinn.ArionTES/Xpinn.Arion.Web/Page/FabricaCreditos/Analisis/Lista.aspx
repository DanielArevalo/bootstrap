<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <br />
    </asp:Panel>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    
    <table>
        <tr>
            <td>No. Radicación:<br />
                <asp:TextBox ID="txtRadicacion" onkeypress="return isNumber(event)" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                <br />
            </td>
            <td>No. Identificación:<br />
                <asp:TextBox ID="txtIdentificacion" onkeypress="return isNumber(event)" runat="server" CssClass="textbox"
                    Width="140px"></asp:TextBox>
                <br />
            </td>
            <td>Nombres :<br />
                <asp:TextBox ID="txtNombre" runat="server" onkeypress="return isOnlyLetter(event)" CssClass="textbox"
                    Width="200px"></asp:TextBox>
                <br />
            </td>
            <td>Apellidos :<br />
                <asp:TextBox ID="txtApellido" runat="server" onkeypress="return isOnlyLetter(event)" CssClass="textbox"
                    Width="200px"></asp:TextBox>
                <br />
            </td>
            <td style="text-align: left">Código Nómina:
                <br />
                <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="120px" />
                <br />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="text-align: left;">
                <asp:Label ID="lbloficina" Text="Oficina" runat="server" /><br />
                <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" Width="180px" />
            </td>
            <td style="text-align: left;">&nbsp;&nbsp;
            </td>
            <td style="text-align: left;">
                <br />
                <asp:Label ID="lblCheck" Text="Imprimir Analisis" runat="server" Visible="False" />
                <asp:CheckBox ID="ChkReimprimir" runat="server" Visible="False" AutoPostBack="true" OnCheckedChanged="ChkReimprimir_OnCheckedChanged" />
            </td>
            <td style="text-align: left; width: 120px">
                <asp:Label ID="lblEstado" Text="Estado" runat="server" Visible="False" /><br />
                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox" Width="120px" Visible="false" />
            </td>
            <td style="text-align: left;">&nbsp;&nbsp;
            </td>
        </tr>

    </table>
    <hr style="width: 100%" />
    <table style="width: 100%;">
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvLista" runat="server" AllowPaging="True"
                    AutoGenerateColumns="False" GridLines="Horizontal"
                    PageSize="20"
                    ShowHeaderWhenEmpty="True" Width="100%"
                    OnPageIndexChanging="gvLista_PageIndexChanging"
                    OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    Style="font-size: x-small">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select"
                                    ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NumeroCredito" HeaderText="No. Crédito" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Identificacion" HeaderText="Identificación" />
                        <asp:BoundField DataField="Nombres" HeaderText="Nombres" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                        <asp:BoundField DataField="Monto" HeaderText="Monto" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" />
                        <asp:BoundField DataField="Cuota" HeaderText="Cuota" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" />
                        <asp:BoundField DataField="Plazo" HeaderText="Plazo" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="LineaCredito" HeaderText="Línea" />
                        <asp:BoundField DataField="Nom_linea_credito" HeaderText="Nombre de Línea" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="oficina" HeaderText="Oficina" />
                        <asp:BoundField DataField="NomZona" HeaderText="Zona" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="fecha_solicitud" HeaderText="Fec Solicitud" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="Estado" HeaderText="Estado" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
                    Visible="False" />
            </td>
        </tr>
    </table>
    
</asp:Content>
