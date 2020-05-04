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
            <div id="gvDiv">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="text-align: left">Fecha
                            <br />
                            <uc1:fecha ID="txtFecha" runat="server"></uc1:fecha>
                        </td>
                        <td style="text-align: left">Oficina
                             <br />
                            <asp:TextBox ID="txtOficina" Enabled="false" CssClass="textbox" runat="server" Width="210px"></asp:TextBox>
                        </td>
                        <td style="text-align: left">Cod. Operación<br />
                            <asp:TextBox ID="txtCod_Ope" CssClass="textbox" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td style="text-align: left">Tipo de Operación<br />
                            <asp:DropDownList ID="ddlTipoOpe" runat="server" Height="25px" Width="280px" CssClass="dropdown"
                                AppendDataBoundItems="True">
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <hr width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvMovimiento" runat="server" Width="100%"
                                AutoGenerateColumns="False" AllowPaging="True" PageSize="20" BackColor="White"
                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                                ForeColor="Black" GridLines="Vertical"
                                OnPageIndexChanging="gvMovimiento_PageIndexChanging"
                                Style="font-size: x-small" OnRowDataBound="gvMovimiento_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="cod_ope" HeaderText="Cod.Ope">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_oper" DataFormatString="{0:d}" HeaderText="Fecha" />
                                    <asp:BoundField DataField="tipo_ope" HeaderText="Tip.Ope." />
                                    <asp:BoundField DataField="nom_tipo_ope" HeaderText="Tipo Operación" />
                                    <asp:BoundField DataField="cod_cliente" HeaderText="Cod.Persona" />
                                    <asp:BoundField DataField="identificacion" HeaderText="Identificación" />
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="cod_ofi" HeaderText="Cod.Ofi." />
                                    <asp:BoundField DataField="nom_usuario" HeaderText="Nombre Usuario" />
                                    <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
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
                                    <asp:BoundField DataField="observacion" HeaderText="Observación" />
                                    <asp:BoundField DataField="cod_cuenta" HeaderText="Cod.Cuenta" />
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
                                GridLines="Vertical" OnPageIndexChanging="gvOperacion_PageIndexChanging"
                                PageSize="5" Style="font-size: x-small;" Width="40%">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="cod_ope" HeaderText="Cod.Ope">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:HyperLinkField DataNavigateUrlFields="num_comp"
                                        DataNavigateUrlFormatString="../../Contabilidad/Comprobante/Lista.aspx?num_comp={0}"
                                        DataTextField="num_comp" HeaderText="Num.Comp" Target="_blank"
                                        Text="Número de Comprobante" />
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

