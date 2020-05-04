<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Cuadre Histórico :."%>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>

<asp:Content ID="Content1" contentplaceholderid="cphMain"  runat="server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>    
        
    <script type="text/javascript">
        function pageLoad() {
            $('#<%=gvReporte.ClientID%>').gridviewScroll({
                width: 980,
                height: 400,
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
    <asp:Panel ID="pConsulta" runat="server">
        <table style="width:70%;">
            <tr>
                <td style="text-align: left" colspan="4">
                    <strong>Críterios de Búsqueda</strong>
                </td>
            </tr>            
            <tr>
                <td align="left">
                    Fecha Cierre<br />
                    <asp:TextBox ID="txtFechaCierre" runat="server" cssClass="dropdown" maxlength="10" 
                        Height="18px" Width="123px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" 
                        PopupButtonID="Image1" TargetControlID="txtFechaCierre">
                    </asp:CalendarExtender>
                </td>
                <td align="left">
                    Tipo de Producto<br />
                    <asp:DropDownList ID="ddlTipoProducto" runat="server" AutoPostBack="true" CssClass="textbox" Width="180px">
                    </asp:DropDownList>
                </td>
                <td align="left">
                    Atributo<br />
                    <asp:DropDownList ID="ddlAtributo" runat="server" Width="224px" CssClass="textbox"/>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                <hr style="width:100%"/>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <table width="90%">
        <tr>
            <td style="text-align:left">
                <asp:Panel ID="panelGrilla" runat="server">
                    <strong>Listado de Cuadre Histórico</strong>
                    <asp:GridView ID="gvReporte" runat="server" Width="80%" AutoGenerateColumns="False"
                        HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"
                        Style="font-size: x-small" GridLines="Horizontal" AllowPaging="True" OnPageIndexChanging="gvReporte_PageIndexChanging"
                        PageSize="20">
                        <Columns>
                            <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_radicacion" HeaderText="Nro Producto">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo_inicial" HeaderText="Saldo Inicial" DataFormatString="{0:C0}">
                                <ItemStyle HorizontalAlign="right" /><FooterStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="debitos" HeaderText="Débitos" DataFormatString="{0:C0}" FooterStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="right" /><FooterStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="creditos" HeaderText="Créditos" DataFormatString="{0:C0}">
                                <ItemStyle HorizontalAlign="right" /><FooterStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo_operativo" HeaderText="Saldo Final (A)" DataFormatString="{0:C0}">
                                <ItemStyle HorizontalAlign="right" /><FooterStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo_final" HeaderText="Saldo Al Cierre (B)" DataFormatString="{0:C0}">
                                <ItemStyle HorizontalAlign="right" /><FooterStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="diferencia" HeaderText="Diferencia (A)-(B)" DataFormatString="{0:C0}">
                                <ItemStyle HorizontalAlign="right" /><FooterStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="text-align:center">
                <asp:Label ID="lblTotalRegs" runat="server" Visible="false" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado"
                    Visible="false" />
            </td>
        </tr>
    </table>   
    <uc1:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>

