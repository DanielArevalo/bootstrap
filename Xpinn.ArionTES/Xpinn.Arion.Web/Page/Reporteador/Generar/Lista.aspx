<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Reportes :." %>

<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlListarCodigo.ascx" TagName="ctlListarCodigo" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">

    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvReporte.ClientID%>').gridviewScroll({
                width: CalcularAncho(),
                height: 400,
                arrowsize: 20,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"

            });
        }

        function CalcularAncho() {
            if (navigator.platform == 'Win32') {
                return screen.width - 300;
            }
            else {
                return 1000;
            }
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="../../../Scripts/PopUp.js" />
        </Scripts>
    </asp:ScriptManager>

    <asp:ModalPopupExtender ID="mpeLoading" runat="server" BehaviorID="idmpeLoading"
        PopupControlID="pnlLoading" TargetControlID="btnLoading" EnableViewState="false"
        BackgroundCssClass="ModalBackground" />
    <asp:Panel ID="pnlLoading" runat="server" Width="200" Height="100" HorizontalAlign="Center"
        CssClass="ModalPopup" EnableViewState="false" Style="display: none">
        <asp:Image ID="Image1" ImageUrl="../../../Images/loading.gif" runat="server" />
        <br />
        Espere un momento por favor...        
    </asp:Panel>
    <asp:Button ID="btnLoading" runat="server" Style="display: none" />

    <asp:Panel ID="pConsulta" runat="server">
        <table style="padding: 0px; width: 100%;">
            <tr>
                <td align="left" colspan="2">Consultar Reporte<br />
                    <asp:DropDownList ID="ddlConsultar" runat="server" AutoPostBack="True" CssClass="textbox"
                        OnSelectedIndexChanged="ddlConsultar_SelectedIndexChanged" AppendDataBoundItems="True" Width="40%">
                        <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblTipoReporte" runat="server" CssClass="lblRol" Visible="False"></asp:Label>
                    <br />
                    <asp:CompareValidator ID="cvConsultar" runat="server"
                        ControlToValidate="ddlConsultar" Display="Dynamic"
                        ErrorMessage="Seleccione un tipo de consulta" ForeColor="Red"
                        Operator="NotEqual" SetFocusOnError="true" Type="Integer"
                        ValidationGroup="vgGuardar" ValueToCompare="0">
                    </asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: left" colspan="2" align="left">
                    <asp:MultiView ID="mvParametros" runat="server">
                        <asp:View ID="vwNada" runat="server">
                        </asp:View>
                        <asp:View ID="vwGridParametros" runat="server">
                            <strong>
                                <asp:Label ID="lblTitParametros" runat="server" Text="Parámetros de Consulta:"></asp:Label></strong>
                            <asp:GridView ID="gvParametros" runat="server" AutoGenerateColumns="False" DataKeyNames="idParametro"
                                Style="font-size: small; text-align: left;"
                                OnRowDataBound="gvParametros_RowDataBound" ShowHeader="False"
                                BorderStyle="None" BorderWidth="0px" GridLines="None" HorizontalAlign="left">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="idParametro" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblidParametro" runat="server" Text='<%# Bind("idParametro") %>' Width="50"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Descripción">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescripcion" runat="server" Enabled="false" Text='<%# Bind("Descripcion") %>' Width="250" />
                                        </ItemTemplate>
                                        <FooterStyle Width="250px" />
                                        <HeaderStyle Width="250px" />
                                        <ItemStyle Width="250px" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tipo" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("TIPO") %>' Width="80"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Lista" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIdLista" runat="server" Text='<%# Bind("IDLISTA") %>' Width="80"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Valor" Visible="True">
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtValor" runat="server" Width="150"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="ftbetxtValor"
                                                runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtValor"
                                                ValidChars="." />
                                            <asp:CalendarExtender ID="ceTxtValor" runat="server" DaysModeTitleFormat="dd/MM/yyyy"
                                                Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtValor" TodaysDateFormat="dd/MM/yyyy">
                                            </asp:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="rfvTxtValor" runat="server"
                                                ErrorMessage="Debe ingresar un valor"
                                                Style="font-size: xx-small; color: #FF0000" ControlToValidate="txtValor" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <ctl:ctlListarCodigo ID="ctlListarValores" Width="180px" Height="19px" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="gridHeader" />
                                <HeaderStyle CssClass="gridHeader" HorizontalAlign="Center" />
                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                <RowStyle CssClass="gridItem" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                <SortedDescendingHeaderStyle BackColor="#575357" />
                            </asp:GridView>
                        </asp:View>
                        <asp:View ID="vwFechas" runat="server">
                            <asp:Label ID="lblMensajeRep" runat="server" Style="color: #FF3300"
                                Width="800px"></asp:Label>
                        </asp:View>
                    </asp:MultiView>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:MultiView ID="mvReporte" runat="server">
        <asp:View ID="vGridReporte" runat="server">
            <div style="text-align: left; width: 100%;">
                <asp:Button ID="btnExportar" runat="server" CssClass="btn8" OnClick="btnExportar_Click" Text="Exportar a excel" />
                &nbsp;&nbsp;
                <asp:Button ID="btnPDF" runat="server" CssClass="btn8" OnClick="btnPDF_Click" Text="Generar PDF" />
                &nbsp;
                <asp:Button ID="btnCombinar" runat="server" CssClass="btn8"
                    OnClick="btnCombinar_Click" Text="Combinar Correspondencia" />
                <asp:Label runat="server" Text="Forma Impresion Horizontal"></asp:Label>
                <asp:CheckBox runat="server" ID="PaginaPDF" />

            </div>
            <div style="text-align: center; vertical-align: top; width: 90%;">
                <asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label>
            </div>
            <br />
            <asp:GridView ID="gvReporte" runat="server" AutoGenerateColumns="False"
                HeaderStyle-CssClass="gridHeader" AllowPaging="true"
                RowStyle-CssClass="gridItem"
                Style="font-size: small" GridLines="Both"
                OnPageIndexChanging="gvReporte_PageIndexChanging"
                OnRowDataBound="gvReporte_RowDataBound">
                <Columns>
                </Columns>
                <HeaderStyle CssClass="gridHeader" />
                <RowStyle CssClass="gridItem" />
                <PagerStyle HorizontalAlign="Left" />
            </asp:GridView>
            <asp:Label ID="lblTotalRegs" runat="server" />
            <br />
        </asp:View>

        <asp:View ID="vCrystalReport" runat="server">
            <br />
            <%--<CR:CrystalReportViewer ID="crvReporte" runat="server" AutoDataBind="true" /> --%>
        </asp:View>

    </asp:MultiView>

</asp:Content>

