<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvOperacion.ClientID%>').gridviewScroll({
                width: CalcularAncho(),
                height: 500,
                arrowsize: 30,
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
    </asp:ScriptManager>   
    <asp:MultiView ID="mvReversion" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwTipoComprobante" runat="server">
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td colspan="4" >
                        <hr width="100%" />
                    </td>
                </tr>
                 <tr>
                    <td> 
                        Fecha Inicial&nbsp;
                        <br />
                        <asp:TextBox ID="txtFecha" CssClass="textbox" runat="server" Width="188px" Height="20px" MaxLength="10"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="Image1" TargetControlID="txtFecha">
                    </asp:CalendarExtender>
&nbsp;</td>
                     <td>Fecha Final
                         <br />
                         <asp:TextBox ID="txtFechaFinal" runat="server" CssClass="textbox" Height="20px" MaxLength="10" Width="188px"></asp:TextBox>
                         <asp:CalendarExtender ID="txtFechaFinal_CalendarExtender" runat="server" Format="dd/MM/yyyy" PopupButtonID="Image1" TargetControlID="txtFechaFinal">
                         </asp:CalendarExtender>
                         &nbsp;</td>
                     <td>Oficina
                         <br />
                         <asp:DropDownList ID="ddlOficinas" runat="server" AutoPostBack="True" CssClass="dropdown"  Height="24px" onselectedindexchanged="ddlOficinas_SelectedIndexChanged" Width="180px">
                         </asp:DropDownList>
                         &nbsp; </td>
                     <td>
                         <asp:Label ID="Labelcajeros" runat="server" Text="Cajeros"></asp:Label>
                         <br />
                         <asp:DropDownList ID="ddlCajero" runat="server"  CssClass="dropdown" Height="20px"  Width="220px">
                         </asp:DropDownList>
                     </td>
                </tr>
                 <tr>
                    <td colspan="4"> 
                    </td>
                </tr>
                <tr><td colspan="4">
                    
                        <asp:GridView ID="gvOperacion" runat="server" AllowPaging="False" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" PageSize="20" Width="100%">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>                               
                                <asp:BoundField DataField="cod_movimiento" HeaderText="Cod Oper" />
                                <asp:BoundField DataField="fecha_movimiento" DataFormatString="{0:d}" HeaderText="Fecha Oper" />
                                <asp:BoundField DataField="nom_caja" HeaderText="Caja" />
                                <asp:BoundField DataField="nom_cajero" HeaderText="Cajero" />                                
                                <asp:BoundField DataField="valor_pago" DataFormatString="{0:N0}" HeaderText="Valor Reversión">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>                                
                                <asp:BoundField DataField="nomtipo_pago" HeaderText="Tipo Pago" />
                                <asp:BoundField DataField="identificacion" HeaderText="Identificación Cliente" />
                                 <asp:BoundField DataField="nom_producto" HeaderText="Número Producto" />
                                 <asp:BoundField DataField="tipoproducto" HeaderText="Tipo Producto" />
                                
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
                  
                    </td></tr>
                <tr>
                    <td colspan="4">
                        &nbsp;</td>
                </tr>
                <tr><td colspan="4">&nbsp;</td></tr>
                <tr>
                    <td colspan="4">
                        &nbsp;</td>
                </tr>
            </table>
        </asp:View>
        <br />
    </asp:MultiView>
</asp:Content>

