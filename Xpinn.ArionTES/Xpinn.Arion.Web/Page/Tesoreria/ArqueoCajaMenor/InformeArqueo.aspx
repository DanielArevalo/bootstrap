<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="InformeArqueo.aspx.cs" 
    Inherits="InformeArqueo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="BarcodeX" Namespace="Fath" TagPrefix="bcx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwConsulta" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table style="width: 80%;">
                    <tr>
                        
                        <td colspan="3" style="text-align: left">
                            <br>
                            <strong>Detalle del efectivo</strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" style="text-align:left; width: 219px;">
                            Denominación Billete&nbsp;*&nbsp;<br />
                            <asp:DropDownList ID="ddlBilletes" runat="server" AppendDataBoundItems="True" 
                                CssClass="textbox" style="font-size:xx-small; text-align:left" 
                                Width="135px" AutoPostBack="True" >
                                <asp:ListItem Value="1000"/>
                                <asp:ListItem Value="2000"/>
                                <asp:ListItem Value="5000"/>
                                <asp:ListItem Value="10000"/>
                                <asp:ListItem Value="20000"/>
                                <asp:ListItem Value="50000"/>
                                <asp:ListItem Value="100000"/>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvBilletes" runat="server" ControlToValidate="ddlBilletes" ErrorMessage="Campo Requerido" 
                                SetFocusOnError="True" ValidationGroup="vgAgregarBillete" ForeColor="Red" Display="Dynamic" Font-Size="X-Small" />
                        </td>
                        <td class="tdD" style="text-align: left; width: 213px;" colspan="2">Cantidad<br />
                            <asp:TextBox ID="txtCantB" runat="server" CssClass="textbox"/>
                            <asp:RequiredFieldValidator ID="rfvCantB" runat="server" ControlToValidate="txtCantB" ErrorMessage="Campo Requerido" 
                                SetFocusOnError="True" ValidationGroup="vgAgregarBillete" ForeColor="Red" Display="Dynamic" Font-Size="X-Small" />
                        </td>
                        <td style="width: 219px;">
                            <asp:Button ID="btnAgregarB" runat="server" Text="Agregar" 
                                CssClass="btn8" onclick="btnAgregarB_Click" width="80%"/>
                        </td>

                    </tr>
                    <tr>
                        <td class="tdI" style="text-align:left; width: 219px;">
                            Denominación Moneda&nbsp;*&nbsp;<br />
                            <asp:DropDownList ID="ddlMonedas" runat="server" AppendDataBoundItems="True" 
                                CssClass="textbox" style="font-size:xx-small; text-align:left" 
                                Width="135px" AutoPostBack="True" >
                                <asp:ListItem Value="50"/>
                                <asp:ListItem Value="100"/>
                                <asp:ListItem Value="200"/>
                                <asp:ListItem Value="500"/>
                                <asp:ListItem Value="1000"/>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvMonedas" runat="server" ControlToValidate="ddlBilletes" ErrorMessage="Campo Requerido" 
                                SetFocusOnError="True" ValidationGroup="vgAgregarMoneda" ForeColor="Red" Display="Dynamic" Font-Size="X-Small" />
                        </td>
                        <td class="tdD" style="text-align: left; width: 213px;" colspan="2">Cantidad<br />
                            <asp:TextBox ID="txtCantM" runat="server" CssClass="textbox" />
                            <asp:RequiredFieldValidator ID="rfvCantM" runat="server" ControlToValidate="txtCantM" ErrorMessage="Campo Requerido" 
                                SetFocusOnError="True" ValidationGroup="vgAgregarMoneda" ForeColor="Red" Display="Dynamic" Font-Size="X-Small" />
                        </td>
                        <td style="width: 219px;">
                            <asp:Button ID="btnagregarM" runat="server" Text="Agregar" 
                                CssClass="btn8" onclick="btnagregarM_Click" Width="80%" />                            
                        </td>
                        
                    </tr>                      
                    <tr>
                        <td style="text-align: center" colspan="2">
                            <br /> 
                            <%--<strong>Efectivo ingresado</strong>--%>
                            <br /> 
                            <br /> 
                            <asp:GridView ID="gvEfectivoCajaMenor" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                GridLines="Horizontal" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                OnRowDeleting="gvEfectivoCajaMenor_RowDeleting"
                                RowStyle-CssClass="gridItem" Width="102%" Height="16px" RowStyle-Font-Size="Small" Caption="Efectivo Ingresado">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" />           
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="denominacion" HeaderText="Denominación" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="cantidad" HeaderText="Cantidad" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="total" HeaderText="Total" DataFormatString="{0:N0}" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="tipo" HeaderText="Tipo" HeaderStyle-HorizontalAlign="Center" />
                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridPager" />
                            </asp:GridView>
                        </td>
                    </tr> 
                                        
                    </table>
                </asp:Panel>
            </asp:View>
           <asp:View ID="vReportearqueo" runat="server">
            <table>
                <tr>
                    <td style="width: 100%">
                        <br />
                        <br />
                        <rsweb:ReportViewer ID="rvArqueoCaj" runat="server" Width="100%" Font-Names="Verdana"
                            Font-Size="8pt" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="14pt" Height="500px" AsyncRendering="false">
                            <LocalReport ReportPath="Page\Tesoreria\ArqueoCajaMenor\ArqueoCajaMenor.rdlc">                               
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
            </table>
        </asp:View>
        </asp:MultiView>
    <asp:HiddenField ID="hdFileName" runat="server" />
    <asp:HiddenField ID="hdFileNameThumb" runat="server" />
</asp:Content>

