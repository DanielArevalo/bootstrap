<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <br />
    </asp:Panel>

    <table>
        <tr>
            <td style="width: 160px">No. Crédito:<br />
                <asp:TextBox ID="txtCredito" runat="server" CssClass="textbox" Width="90%"></asp:TextBox>
                <br />
                <asp:CompareValidator ID="cvNoCredito" runat="server"
                    ControlToValidate="txtCredito" Display="Dynamic"
                    ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck"
                    SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
            </td>
            <td style="width: 200px">No. Identificación del cliente:<br />
                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox"
                    Width="90%"></asp:TextBox>
                <br />
                <asp:CompareValidator ID="cvIdentificacion" runat="server"
                    ControlToValidate="txtIdentificacion" Display="Dynamic"
                    ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck"
                    SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
            </td>
            <td style="text-align: left;">Código de nómina<br />
                <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="150px" />
            </td>
            <td style="text-align: left; width: 220px">
                <asp:Label ID="lbloficina" Text="Oficina" runat="server" /><br />
                <asp:DropDownList ID="ddloficina" runat="server" CssClass="textbox"
                    Width="90%" />
            </td>
            <td style="text-align: left; width: 150px">Fecha de Solicitud<br />
                <ucFecha:fecha ID="txtFecSolicitud" runat="server" Width="90%" />
            </td>
        </tr>
    </table>
    <hr style="width: 100%" />
    <table style="width: 100%">
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvLista" runat="server" AllowPaging="True"
                    AutoGenerateColumns="False" GridLines="Horizontal"
                    PageSize="20"
                    ShowHeaderWhenEmpty="True" Width="97%"
                    OnPageIndexChanging="gvLista_PageIndexChanging"
                    OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    Style="font-size: small">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select"
                                    ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NumeroCredito" HeaderText="No. Crédito" />
                        <asp:BoundField DataField="Identificacion" HeaderText="Identificación" />
                        <asp:BoundField DataField="Nombres" HeaderText="Nombres" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina" />
                        <asp:BoundField DataField="Monto" HeaderText="Monto" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" />
                        <asp:BoundField DataField="Cuota" HeaderText="Cuota" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" />
                        <asp:BoundField DataField="Plazo" HeaderText="Plazo" />
                        <asp:BoundField DataField="LineaCredito" HeaderText="Línea" />
                        <asp:BoundField DataField="Nom_linea_credito" HeaderText="Nombre de Línea" />
                        <asp:BoundField DataField="oficina" HeaderText="Oficina" />
                        <asp:BoundField DataField="fechasolicitud" HeaderText="Fec Solicitud" DataFormatString="{0:d}" />
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
