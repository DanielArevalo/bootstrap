<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="ListaOtros.aspx.cs" Inherits="Lista" Title=".: Xpinn - Reportes :."%>
<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" contentplaceholderid="cphMain"  runat="server">

    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   

    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvReporte.ClientID%>').gridviewScroll({
                width: 1000,
                height: 400,
                arrowsize: 20,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"

            });
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
        <asp:Image ID="Image1" ImageUrl="../../../Images/loading.gif" runat="server"/>
        <br />Espere un momento por favor...        
    </asp:Panel>
    <asp:Button ID="btnLoading" runat="server" Style="display: none" />

    <asp:Panel ID="pConsulta" runat="server">
        <table style="padding: 0px; width:100%;">        
            <tr>
                <td align="left" colspan="2">                
                    Consultar Reporte<br />
                    <asp:DropDownList ID="ddlConsultar" runat="server" AutoPostBack="True" CssClass="dropdown" 
                    onselectedindexchanged="ddlConsultar_SelectedIndexChanged" AppendDataBoundItems="True" Width="600" Height="20px">
                        <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                        <asp:ListItem Value="-1">INFORME DE PAGO DE CREDITOS AL CIERRE</asp:ListItem>
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
                <td style="text-align:left" colspan="2" align="left">
                    <asp:MultiView ID="mvParametros" runat="server" >    
                        <asp:View ID="vwNada" runat="server" >    
                        </asp:View>
                        <asp:View ID="vwGridParametros" runat="server" >
                            <strong><asp:Label ID="lblTitParametros" runat="server" Text="Parámetros de Consulta:"></asp:Label></strong>
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
                                    <asp:TemplateField HeaderText="Valor" Visible="True">
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtValor" runat="server" Width="80"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="ftbetxtValor" 
                                                runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtValor" 
                                                ValidChars="." />
                                            <asp:CalendarExtender ID="ceTxtValor" runat="server" DaysModeTitleFormat="dd/MM/yyyy"
                                                Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtValor" TodaysDateFormat="dd/MM/yyyy"></asp:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="rfvTxtValor" runat="server" 
                                                ErrorMessage="Debe ingresar un valor" 
                                                style="font-size: xx-small; color: #FF0000" ControlToValidate="txtValor" Display="Dynamic"></asp:RequiredFieldValidator>
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
                        <asp:View ID="vwFechas" runat="server" >
                            <asp:Label ID="LabelFecha" runat="server" Text="Fecha Cierre"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtFecha" runat="server" cssClass="textbox" Height="18px" 
                                maxlength="10" Width="106px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" DaysModeTitleFormat="dd/MM/yyyy"
                                Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFecha" TodaysDateFormat="dd/MM/yyyy"></asp:CalendarExtender>
                            <br />                        
                            <asp:Label ID="lblMensajeRep" runat="server" style="color: #FF3300" 
                                Width="800px"></asp:Label>
                        </asp:View>
                    </asp:MultiView>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:MultiView ID="mvReporte" runat="server" >        
        <asp:View ID="vGridReporte" runat="server" >
            <div style="text-align:left">
                <asp:Button ID="btnExportar" runat="server" CssClass="btn8" onclick="btnExportar_Click" Text="Exportar a excel" />
                &nbsp;&nbsp;
                <asp:Button ID="btnPDF" runat="server" CssClass="btn8" onclick="btnPDF_Click" Text="Generar PDF" />
            </div>
            <div style="text-align:center; vertical-align: top; width: 100%;">
                <asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label>
            </div>
            <br />
            <asp:GridView ID="gvReporte" runat="server" AutoGenerateColumns="False" 
                HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="False" 
                style="font-size:small" Width="100%" GridLines="Both"  onpageindexchanging="gvReporte_PageIndexChanging" >
                <Columns>
                </Columns>
                <HeaderStyle CssClass="gridHeader" />
                <PagerStyle CssClass="gridPager" />
                <RowStyle CssClass="gridItem" />
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

