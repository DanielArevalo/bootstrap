<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>



<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/ctlseleccionmultipledropdown.ascx" TagName="dropdownmultiple" TagPrefix="ucDrop" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%">
        <tr>
            <td style="text-align: left; font-size: large; color: #359AF2; font-weight: bold;" colspan="3">
                REPORTE DE CDAT</td>
            <td style="text-align: center; width: 467px;">&nbsp;</td>
            <td class="logo" style="width: 194px">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left; font-size: small;" colspan="3">
                <strong>Criterios de Generación:</strong></td>
            <td style="text-align: center; width: 467px;">&nbsp;</td>
            <td class="logo" style="width: 194px">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left; width: 185px;" class="logo">
                <asp:Label ID="lblFechaInicial" runat="server" Text="Fecha Inicial Apertura:"></asp:Label>
                <strong>
                <ucFecha:fecha ID="ucFechaInicial" runat="server" style="text-align: center" />
                </strong><br />
            </td>
            <td style="text-align: left; width: 165px;">
                <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha Final Apertura:"></asp:Label><strong>
                <ucFecha:fecha ID="ucFechaFinal" runat="server" style="text-align: center" />
                </strong><br />
            </td>

            <td style="text-align: left; width: 165px;" dir="ltr">&nbsp;</td>
            <%--<td style="text-align: left">Categoria:<br />
                <ucDrop:dropdownmultiple ID="ddlCategoria" runat="server" Height="24px" Width="165px"></ucDrop:dropdownmultiple>
            </td>
            <td style="text-align: left">Linea de Crédito:<br />
                <ucDrop:dropdownmultiple ID="ddlLinea" runat="server" Height="24px" Width="200px"></ucDrop:dropdownmultiple>
            </td>--%>
           
        </tr>
        <tr>
            <td style="text-align: left; " colspan="2">
                <asp:Label ID="lblFechaFinal0" runat="server" Text="Oficina:"></asp:Label><strong><ucDrop:dropdownmultiple ID="ddloficina" runat="server" Height="10px" Width="200px"></ucDrop:dropdownmultiple>
                </strong>
            </td>

            <td style="text-align: left; width: 165px;" dir="ltr">&nbsp;</td>
           
        </tr>
    </table>


    <asp:Panel ID="Principal" runat="server">
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
                            onpageindexchanging="gvLista_PageIndexChanging" Font-Size="X-Small">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                          <%--      <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                            ToolTip="Detalle" Width="16px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                        ToolTip="Editar" Width="16px" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>                                
                                <asp:BoundField DataField="nombre" HeaderText="Cod.Oficina" />
                                <asp:BoundField DataField="numero_cdat" DataFormatString="{0:N0}" HeaderText="Número CDAT"> <ItemStyle HorizontalAlign="Right" /></asp:BoundField> 
                                <asp:BoundField DataField="identificacion" HeaderText="Identificacion" />
                                <asp:BoundField DataField="nombres" HeaderText="Apellidos y Nombres" />
                                <asp:BoundField DataField="fecha_apertura" DataFormatString="{0:d}" HeaderText="Fecha Apertura" /> 
                                <asp:BoundField DataField="fecha_vencimiento" DataFormatString="{0:d}" HeaderText="Fecha Final" />                                
                                <asp:BoundField DataField="tasa_interes" DataFormatString="{0:N0}" HeaderText="Tasa"> <ItemStyle HorizontalAlign="Right" /></asp:BoundField> 
                                <asp:BoundField DataField="plazo" DataFormatString="{0:N0}" HeaderText="Plazo"> <ItemStyle HorizontalAlign="Right" /></asp:BoundField> 
                                <asp:BoundField DataField="valor" DataFormatString="{0:N0}" HeaderText="Valor"> <ItemStyle HorizontalAlign="Right" /></asp:BoundField> 
                                <asp:BoundField DataField="intereses" DataFormatString="{0:N0}" HeaderText="Int. Mes"> <ItemStyle HorizontalAlign="Right" /></asp:BoundField>    
                                <asp:BoundField DataField="retencion" DataFormatString="{0:N0}" HeaderText="Retención Mes"> <ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                <asp:BoundField DataField="intereses_cau" DataFormatString="{0:N0}" HeaderText="Causado Mes"> <ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                <asp:BoundField DataField="valor_acumulado" DataFormatString="{0:N0}" HeaderText="Acumulado"> <ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                <asp:BoundField DataField="fecha_intereses" DataFormatString="{0:d}" HeaderText="Fecha Pago Int" />                                                                  
                                                              
                                
                                                                
                                
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
