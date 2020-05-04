<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:ImageButton runat="server" ID="btnConsultar" ImageUrl="~/Images/btnConsultar.jpg" OnClick="btnConsultar_Click" ImageAlign="Right" />
    <asp:Panel ID="Principal" runat="server">
        <asp:Panel ID="pConsulta" runat="server" style="width: 80%;">
            <table style="width: 80%;">
                <tr>
                    <td colspan="6" style="font-size: x-small">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="font-size: x-small; text-align:left" colspan="6">
                        <strong>Críterios de Búsqueda</strong>
                    </td>
                </tr>
                <tr>
                    <td style="width: 69px; text-align:left">
                        Código</td>
                    <td colspan="2" style="text-align:left">
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="121px"></asp:TextBox>
                    </td>
                    <td style="text-align:right; width: 357px;">
                        Tipo de Presupuesto</td>
                    <td>
                        <asp:DropDownList ID="ddlTipoPresupuesto" runat="server" CssClass="textbox" 
                            Width="197px" AppendDataBoundItems="True" >
                            <asp:ListItem Value="0">Seleccione un Item</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 69px; text-align:left">
                    </td>
                    <td style="width: 20px; text-align:left">
                    </td>
                    <td style="text-align:left">
                    </td>
                    <td style="text-align:left; width: 357px;">
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="Listado" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:GridView ID="gvLista" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                            BorderStyle="None" BorderWidth="1px" CellPadding="4"  
                            ForeColor="Black" GridLines="Vertical" PageSize="20" Width="100%"
                            OnSelectedIndexChanged="gvLista_SelectedIndexChanged" 
                            onrowediting="gvLista_RowEditing" 
                            onpageindexchanging="gvLista_PageIndexChanging" style="font-size: x-small">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                            ToolTip="Detalle" Width="16px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="idpresupuesto" HeaderText="Código" />
                                <asp:BoundField DataField="descripcion" HeaderText="Descripción"> <ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                <asp:BoundField DataField="fecha_elaboracion" DataFormatString="{0:d}" HeaderText="Fecha Elaboración" />
                                <asp:BoundField DataField="fecha_aprobacion" DataFormatString="{0:d}" HeaderText="Fecha Aprobación" />
                                <asp:BoundField DataField="cod_elaboro" HeaderText="Cod.Elaboro" />
                                <asp:BoundField DataField="cod_aprobo" HeaderText="Cod.Aprobo" />
                                <asp:BoundField DataField="tipo_presupuesto" HeaderText="Tipo" />
                                <asp:BoundField DataField="num_periodos" HeaderText="#Periodos" />
                                <asp:BoundField DataField="cod_periodicidad" HeaderText="Periodicidad" />
                                <asp:BoundField DataField="periodo_inicial" DataFormatString="{0:d}" HeaderText="Periodo Inicial" />
                                <asp:BoundField DataField="centro_costo" HeaderText="Centro Costo"> <ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado." Visible="False"/>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
