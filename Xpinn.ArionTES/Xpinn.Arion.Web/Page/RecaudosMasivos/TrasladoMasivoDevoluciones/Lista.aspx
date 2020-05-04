<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Desembolso Masivo :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvDesembolsoMasivo" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwCreditos" runat="server">
            <table style="width: 540px">
                <tr>
                    <td colspan="2" style="font-size: small; text-align: left">
                        <strong>Criterios de Búsqueda</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 140px">Identificación<br />
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                    <td style="width: 400px; text-align: left">Nombre<br />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                    <td style="width: 400px; text-align: left">Código de nómina<br />
                        <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="90%" />
                        <br />
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="updFormaPago" runat="server">
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: left">
                                <strong>Forma Desembolso</strong>
                                <br />
                                <asp:DropDownList ID="ddlForma_Desem" runat="server" CssClass="textbox" Width="200px"
                                    OnSelectedIndexChanged="ddlForma_Desem_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="panelCheque" runat="server">
                                    <table>
                                        <tr>
                                            <td style="text-align: left">Entidad. de Giro<br />
                                                <asp:DropDownList ID="ddlEntidad_giro" runat="server" CssClass="textbox" Width="250px"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlEntidad_giro_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: left">Cuenta de Giro<br />
                                                <asp:DropDownList ID="ddlCuenta_Giro" runat="server" CssClass="textbox" Width="250px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="panelTrans" runat="server">
                                    <table>
                                        <tr>
                                            <td style="text-align: left">Num. Cuenta<br />
                                                <asp:TextBox ID="txtNum_cuenta" runat="server" CssClass="textbox" Width="150px" />
                                            </td>
                                            <td style="text-align: left">Entidad<br />
                                                <asp:DropDownList ID="ddlEntidad" runat="server" CssClass="textbox" Width="250px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: left">Tipo de Cuenta<br />
                                                <asp:DropDownList ID="ddlTipo_cuenta" runat="server" CssClass="textbox" Width="250px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="panelGrilla" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: left">
                                    <strong>Devoluciones</strong><br />
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 40px">
                                    <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                        AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" DataKeyNames="cod_persona"
                                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                        RowStyle-CssClass="gridItem">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="cbSeleccionarEncabezado" runat="server" Checked="false" OnCheckedChanged="cbSeleccionarEncabezado_CheckedChanged"
                                                        AutoPostBack="True" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbSeleccionar" runat="server" Checked="false" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                                <ItemStyle CssClass="gridIco"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="num_devolucion" HeaderText="Nro Traslado">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}"></asp:BoundField>
                                            <asp:BoundField DataField="cod_persona" HeaderText="Cod. Persona">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="concepto" HeaderText="Mótivo Devolución">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:N0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nomestado" HeaderText="Estado">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                        <PagerStyle CssClass="gridPager"></PagerStyle>
                                        <RowStyle CssClass="gridItem"></RowStyle>
                                    </asp:GridView>
                                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlForma_Desem" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlEntidad_giro" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
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
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Desembolsos de Devoluciones Realizados Correctamente"></asp:Label>
                            <br />
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
    <br />
    <br />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
