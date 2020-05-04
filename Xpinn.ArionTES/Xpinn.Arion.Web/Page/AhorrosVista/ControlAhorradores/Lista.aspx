<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" ActiveViewIndex="0" runat="server">
        <asp:View runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table cellpadding="5" cellspacing="0" style="width: 80%">

                    <tr>
                        <td style="height: 15px; text-align: left; font-size: x-small;" colspan="4">
                            <strong>Criterios de Búsqueda:</strong></td>
                        <td style="height: 15px; text-align: left; font-size: x-small; width: 4px;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 15px; text-align: left;">
                            <asp:Panel ID="Panel1" runat="server" Width="130px">
                                <asp:Label ID="LabelFecha_gara" runat="server" Text="Fecha Garantía"></asp:Label>
                                <asp:TextBox ID="txtFechaIni" MaxLength="10" ReadOnly="true" CssClass="textbox"
                                    runat="server" Width="80px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                                    PopupButtonID="Image1"
                                    TargetControlID="txtFechaIni"
                                    Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                                <img id="Image1" alt="Calendario"
                                    src="../../../Images/iconCalendario.png" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" style="text-align: left; width: 28%;"></td>
                        <td class="tdI" style="text-align: left; width: 21%;"></td>
                        <td class="tdI" style="text-align: left; width: 21%;"></td>
                        <td class="tdI" style="text-align: left; width: 2%;"></td>
                        <td class="tdI" style="text-align: left; width: 4px;">&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                            AllowPaging="True" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                            OnPageIndexChanging="gvLista_PageIndexChanging"
                            Style="font-size: xx-small" PageSize="20" Height="10%" OnRowDataBound="gvLista_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Selección">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="numero_cuenta" HeaderText="Num. Cuenta">
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_apertura" HeaderText="Fec. Apertura" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="identificacion" HeaderText="Identificación Tit.">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombres" HeaderText="Nombre Tit.">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nom_linea" HeaderText="Línea">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldo_total" HeaderText="Saldo" DataFormatString="${0:#,##0.00}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="estados" HeaderText="Estado">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="idbeneficiario" HeaderText="Cod. Beneficiario">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="identificacion_ben" HeaderText="Identificación Benf.">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombres_ben" HeaderText="Nombre Benf.">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_nacimiento_ben" HeaderText="Fec. Nac. Benef." DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="edad" HeaderText="Edad Benf.">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="mvFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensaje" runat="server" Text="Operación Realizada Correctamente"
                                Style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;"></td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
