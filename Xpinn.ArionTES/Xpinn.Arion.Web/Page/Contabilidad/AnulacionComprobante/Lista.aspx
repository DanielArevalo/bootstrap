<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales"
    TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: 1000,
                height: 500,
                freezesize: 3,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });
        }        
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAprobarComprobante" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwConsulta" runat="server">
            <asp:Panel ID="pConsulta" runat="server" Width="100%">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: left; width: 123px;">
                            Fecha Anulación
                        </td>
                        <td style="text-align: left">
                            <uc1:fecha ID="txtFechaIni" runat="server"></uc1:fecha>
                        </td>
                        <td style="text-align: left; width: 126px;">
                            Motivo Anulación
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlMotivoAnulacion" runat="server" CssClass="textbox" AppendDataBoundItems="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%;">
                    <caption>
                        <hr colspan="5" />
                        <tr>
                            <td style="font-size: x-small; text-align: left" colspan="5">
                                <strong>Críterios de Búsqueda</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 181px;">
                                Número de Comprobante
                            </td>
                            <td colspan="2" style="text-align: left">
                                <asp:TextBox ID="txtNumComp" runat="server" CssClass="textbox" Width="121px"></asp:TextBox>
                            </td>
                            <td style="text-align: left; width: 158px;">
                                Tipo de Comprobante
                            </td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="ddlTipoComprobante" runat="server" AppendDataBoundItems="True"
                                    CssClass="textbox">
                                    <asp:ListItem Value="0">Seleccione un Item</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </caption>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="Listado" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <br />
                            <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                CellPadding="4" ForeColor="Black" GridLines="Vertical" PageSize="20" Width="100%"
                                OnSelectedIndexChanged="gvLista_SelectedIndexChanged" OnRowEditing="gvLista_RowEditing"
                                DataKeyNames="estado" OnPageIndexChanging="gvLista_PageIndexChanging" Style="font-size: x-small">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                                ToolTip="Detalle" Width="16px" onclick="btnInfo_Click" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="num_comp" HeaderText="Número" />
                                    <asp:BoundField DataField="tipo_comp" HeaderText="Tipo" />
                                    <asp:BoundField DataField="fecha" DataFormatString="{0:d}" HeaderText="Fecha" />
                                    <asp:BoundField DataField="descripcion_concepto" HeaderText="Concepto" />
                                    <asp:BoundField DataField="ciudad" HeaderText="Ciudad" />
                                    <asp:BoundField DataField="iden_benef" HeaderText="Identificacion" />
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="elaboro" HeaderText="Elaborado por" />
                                    <asp:BoundField DataField="aprobo" HeaderText="Aprobado por" />
                                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                                    <asp:BoundField DataField="totalcom" DataFormatString="{0:N0}" HeaderText="Valor">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
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
                            <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                            <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
                                Visible="False" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:ModalPopupExtender ID="mpeVerificar" runat="server" PopupControlID="panelVerificar"
        TargetControlID="HiddenField1" BackgroundCssClass="backgroundColor">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelVerificar" runat="server" BackColor="White" Style="text-align: right"
        BorderWidth="1px" Width="500px">
        <div id="popupcontainer" style="width: 500px">
        </div>
    </asp:Panel>
    <uc1:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
