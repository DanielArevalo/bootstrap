<%@ Page Language="C#" MaintainScrollPositionOnPostback="true"  MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlListarCodigo.ascx" TagName="ctlListarCodigo" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <br />
    <br />

    <asp:MultiView ID="mvComprobante" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwTipoComprobante" runat="server">
            <div id="gvDiv" style="width: 100%;">
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="text-align: left">
                            Fecha<br />
                            <uc1:fecha ID="txtFecha" runat="server"></uc1:fecha>
                        </td>
                        <td style="text-align: left">
                            Almacèn<br />
                            <asp:DropDownList ID="ddlAlmacen" runat="server" Height="25px" Width="280px" CssClass="dropdown"
                                AppendDataBoundItems="True">
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvMovimiento" runat="server" Width="100%" ShowFooter="true"
                                AutoGenerateColumns="False" AllowPaging="True" PageSize="200" BackColor="White"
                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                                ForeColor="Black" Style="font-size: x-small" 
                                OnPageIndexChanging="gvMovimiento_PageIndexChanging"
                                OnRowDataBound="gvMovimiento_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="id_movimiento" HeaderText="Id." ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="fecha_movimiento" DataFormatString="{0:d}" HeaderText="Fecha Movimiento" ItemStyle-Width="100px" />
                                    <asp:BoundField DataField="tipo_movimiento" HeaderText="Tipo.Mov." ItemStyle-Width="60px" />
                                    <asp:BoundField DataField="nom_tipo_movimiento" HeaderText="Descripciòn" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="id_almacen" HeaderText="Id.Alm" ItemStyle-Width="40px" />
                                    <asp:BoundField DataField="almacenname" HeaderText="Nombre Almacen" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="cod_persona" HeaderText="Cod.Per." ItemStyle-Width="40px" />
                                    <asp:BoundField DataField="id_persona" HeaderText="Identific." ItemStyle-Width="80px" />
                                    <asp:BoundField DataField="nombre_persona" HeaderText="Nombre" ItemStyle-Width="180px" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="total" HeaderText="Total" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="total_impuesto" HeaderText="Total Impuesto" DataFormatString="{0:N0}"  ItemStyle-HorizontalAlign="Right"/>
                                    <asp:BoundField DataField="total_costo" HeaderText="Total Costo" DataFormatString="{0:N0}"  ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="centro_costo" HeaderText="C/C"/>
                                    <asp:TemplateField HeaderText="Seleccionar">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkseleccionarHeader" runat="server" Width="40px" Checked="true"
                                                OnCheckedChanged="chkseleccionarHeader_CheckedChanged" AutoPostBack="True" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkseleccionar" runat="server" Checked='<%# Eval("seleccionar") %>' Width="40px" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Proceso">
                                        <ItemTemplate>
                                            <ctl:ctlListarCodigo ID="ddlProceso" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Retenciòn">
                                        <ItemTemplate>
                                            <asp:GridView ID="gvRetencion" runat="server" ShowHeader="false" AutoGenerateColumns="False" >
                                                <Columns>
                                                    <asp:BoundField DataField="nombre" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="False" />
                                                    <asp:BoundField DataField="porcentaje_calculo" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="valor" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right"/>
                                                    <asp:BoundField DataField="cod_cuenta" ItemStyle-HorizontalAlign="Left" />
                                                </Columns>
                                                <RowStyle CssClass="gridItem" />
                                            </asp:GridView>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="observaciones" HeaderText="Observaciones" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Left" /> 
                                    <asp:BoundField DataField="numero_factura" HeaderText="Num.Factura" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Left" /> 
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
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblTotalReg" runat="server" Visible="false" />
                            <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado" Visible="false" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:View>
        <asp:View ID="mvFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 100%; text-align: center">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;" colspan="2"></td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;             
                        </td>
                        <td style="text-align: left; font-size: large;">
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Comprobantes Generados Correctamente"></asp:Label><br />
                            <asp:GridView ID="gvOperacion" runat="server" AllowPaging="False"
                                AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE"
                                BorderStyle="None" BorderWidth="0px" CellPadding="0" ForeColor="Black"
                                OnPageIndexChanging="gvOperacion_PageIndexChanging" OnSelectedIndexChanged="gvOperacion_SelectedIndexChanged" 
                                PageSize="5" Style="font-size: x-small;" Width="40%">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                                ToolTip="Detalle" Width="16px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="id_movimiento" HeaderText="Id.Movimiento">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="num_comp" HeaderText="Num.Comp">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
<%--                                    <asp:HyperLinkField DataNavigateUrlFields="num_comp"
                                        DataNavigateUrlFormatString="../../../Contabilidad/Comprobante/Nuevo.aspx?num_comp={0}"
                                        DataTextField="num_comp" HeaderText="Num.Comp" Target="_blank"
                                        Text="Número de Comprobante" />--%>
                                    <asp:BoundField DataField="tipo_comp" HeaderText="Tipo.Comp" />
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
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <asp:HiddenField ID="HiddenField1" runat="server" />

    <asp:ModalPopupExtender ID="mpeNuevo" runat="server"
        PopupControlID="panelActividadReg" TargetControlID="HiddenField1"
        BackgroundCssClass="backgroundColor">
    </asp:ModalPopupExtender>

    <asp:Panel ID="panelActividadReg" runat="server" BackColor="White" Style="text-align: right" BorderWidth="1px" Width="500px">
        <div id="popupcontainer" style="width: 500px">
            <table style="width: 100%;">
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">Esta Seguro de Generar los Comprobantes ?
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td style="text-align: center">
                        <asp:Button ID="btnContinuar" runat="server" Text="Continuar"
                            CssClass="btn8" Width="182px" OnClick="btnContinuar_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnParar" runat="server" Text="Cancelar" CssClass="btn8"
                            Width="182px" OnClick="btnParar_Click" />
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>

</asp:Content>

