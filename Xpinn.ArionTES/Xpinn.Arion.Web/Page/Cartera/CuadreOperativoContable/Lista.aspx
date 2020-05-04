<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Cuadre Operativo vs Contable :." %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>

    <script type="text/javascript">
        function pageLoad() {
            $('#<%=gvReporte.ClientID%>').gridviewScroll({
                width: CalcularAncho(),
                height: 500,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });            
        }
        function gvOperacionScroll() {
            <$('#<%=gvOperaciones.ClientID%>').gridviewScroll({
                width: CalcularAncho(),
                height: 500,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });
        }
        function CalcularAncho() {
            if (navigator.platform == 'Win32') {
                return screen.width - 350;
            }
            else {
                return 1000;
            }
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table style="width: 85%;">
            <tr>
                <td style="font-size: x-small; text-align: left" colspan="4">
                    <strong>Críterios de Búsqueda</strong>
                </td>
            </tr>
            <tr>
                <td align="left">Tipo de Producto
                </td>
                <td colspan="2" align="left">
                    <asp:DropDownList ID="ddlTipoProducto" runat="server" AutoPostBack="true" CssClass="textbox" Width="180px" OnSelectedIndexChanged="ddlTipoProducto_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td align="left">
                </td>
                <td colspan="2" align="left">
                    <asp:Label ID="lblDeudor" runat="server" Visible="false" Text="Tipo de Cartera"/>
                    <asp:DropDownList ID="ddlDeudor" runat="server" Visible="false" AutoPostBack="true" CssClass="textbox" Width="180px" OnSelectedIndexChanged="ddlDeudor_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelFecha_gara1" runat="server" Text="Fecha Inicial"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtFechaIni" runat="server" CssClass="dropdown" MaxLength="10"
                        Height="18px" Width="123px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                        PopupButtonID="Image1" TargetControlID="txtFechaIni">
                    </asp:CalendarExtender>
                </td>
                <td>
                    <asp:Label ID="LabelFecha_gara0" runat="server" Text="Fecha Final" maxlength="10"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtFechaFin" runat="server" CssClass="dropdown" MaxLength="10"
                        Height="18px" Width="119px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                        PopupButtonID="Image2" TargetControlID="txtFechaFin">
                    </asp:CalendarExtender>
                </td>
                <td>
                    <asp:Label ID="lblSaldoContable" runat="server" Text="Saldo Contable" maxlength="10"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtSaldoContable" runat="server" ReadOnly="true" CssClass="dropdown" MaxLength="10"
                        Height="18px" Width="119px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblSaldoOperativo" runat="server" Text="Saldo Operativo" maxlength="10"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtSaldoOperativo" runat="server" ReadOnly="true" CssClass="dropdown" MaxLength="10"
                        Height="18px" Width="119px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblDiferencia" runat="server" Text="Diferencia" maxlength="10"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtDiferencia" runat="server" ReadOnly="true" CssClass="dropdown" MaxLength="10"
                        Height="18px" Width="119px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <br />
    <br />
    <asp:Panel ID="pOperacionSC" runat="server"  Visible="false">
        <asp:Panel ID="pEncBusqueda" runat="server" CssClass="collapsePanelHeader" Height="30px" Visible="true">
            <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                    <asp:Label ID="lblMostrarDetalles" runat="server" />
                    <asp:ImageButton ID="imgExpand" runat="server" ImageUrl="~/Images/expand.jpg" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pOperaciones" runat="server" Width="100%" HorizontalAlign="Center">
            <%--<div style="border-style: none; border-width: medium; max-height: 500px;">--%>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:GridView ID="gvOperaciones" runat="server" Width="100%" GridLines="Horizontal" OnSelectedIndexChanged="gvOperaciones_SelectedIndexChanged"
                                AutoGenerateColumns="False" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                RowStyle-CssClass="gridItem" DataKeyNames="cod_ope" Style="font-size: x-small">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEditar" runat="server" CommandName="Select" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Contabilizar" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="cod_ope" HeaderText="Cod. Ope" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="tipo_comp" HeaderText="Tipo Ope" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="nombre" HeaderText="Oficina" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="fecha" HeaderText="Fecha Ope" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="estado" HeaderText="Estado" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="cod_persona" HeaderText="Cod. Usuario" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="usuario" HeaderText="Nom. Usuario" ItemStyle-HorizontalAlign="Center" />
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="lblTotalOpe" runat="server" Visible="False" />
                        </td>
                    </tr>
                </table>
            <%--</div>--%>
        </asp:Panel>
        <asp:CollapsiblePanelExtender ID="cpeDemo" runat="Server" CollapseControlID="pEncBusqueda"
            Collapsed="True" CollapsedImage="~/Images/expand.jpg" CollapsedText="(Click Aqui para Mostrar Detalles...)"
            ExpandControlID="pEncBusqueda" ExpandedImage="~/Images/collapse.jpg" ExpandedText="(Click Aqui para Ocultar Detalles...)"
            ImageControlID="imgExpand" SkinID="CollapsiblePanelDemo" SuppressPostBack="true"
            TargetControlID="pOperaciones" TextLabelID="lblMostrarDetalles" />
        <br />
    </asp:Panel>

    <table style="width: 100%">
        <tr>
            <td>
                <asp:GridView ID="gvReporte" runat="server" Width="100%" AutoGenerateColumns="False" HeaderStyle-CssClass="gridHeader" OnRowDataBound="gvLista_RowDataBound"
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" Style="font-size: x-small" GridLines="Horizontal" AllowPaging="False"
                    OnPageIndexChanging="gvReporte_PageIndexChanging" PageSize="20" OnSelectedIndexChanged="gvReportes_SelectIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Select" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:Button ID="btnGenerarComprobante" runat="server" ToolTip="Generar Comprobante" Text="Generar Comp." Font-Size="XX-Small" Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="cod_atr" HeaderText="Cód.Atr.">
                            <ItemStyle HorizontalAlign="left" Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nom_atr" HeaderText="Atributo">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_ope" HeaderText="Cod.Ope">
                            <ItemStyle HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="num_comp" HeaderText="No.Comp">
                            <ItemStyle HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo_comp" HeaderText="Tipo Comp">
                            <ItemStyle HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nom_tipo_comp" HeaderText="Descripción">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_cuenta" HeaderText="Cod.Cuenta">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}">
                            <ItemStyle HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor_contable" HeaderText="Vr.Contable" DataFormatString="{0:c}">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor_operativo" HeaderText="Vr.Operativo" DataFormatString="{0:c}">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="diferencia" HeaderText="Diferencia" DataFormatString="{0:c}">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo" HeaderText="Tipo de Diferencia">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridPager" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>

