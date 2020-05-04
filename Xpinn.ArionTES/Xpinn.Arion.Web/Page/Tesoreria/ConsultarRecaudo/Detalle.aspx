<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>  
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   

    <script type="text/javascript">

        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: 1200,
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
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <table style="width: 100%">
                <tr>
                    <td style="text-align:left">
                        Empresa Recaudadora<br />
                        <asp:DropDownList ID="ddlEntidad" runat="server" CssClass="dropdown" 
                            Width="323px" Enabled="False">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align:left">
                        <asp:Label ID="lblFechaAplica" runat="server" Visible="True" Text="Fecha de Aplicacion" /><br />
                        <ucFecha:fecha ID="ucFechaAplicacion" runat="server"  Enabled="False" Requerido="False" />
                    </td>
                    <td>
                        <asp:Label ID="lblNumeroLista" runat="server" Visible="True" Text="Número de Aplicación" /><br />
                        <asp:TextBox ID="txtNumeroLista" runat="server" Enabled="false"></asp:TextBox>                    
                    </td>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                        onclick="btnExportar_Click" Text="Exportar a Excel" /><br />
                        <asp:GridView ID="gvLista" runat="server" Width="100%" PageSize="3"
                            ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" 
                            SelectedRowStyle-Font-Size="XX-Small" 
                            Style="font-size: small; margin-bottom: 0px;" >
                            <Columns>
                                <asp:BoundField DataField="identificacion" HeaderText="Cedula/Nit" >
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre" HeaderText="Nombres" >
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tipo_producto" HeaderText="Tipo de Producto" />  
                                <asp:BoundField DataField="numero_producto" HeaderText="Número de Producto" />                                        
                                <asp:BoundField DataField="tipo_aplicacion" HeaderText="Tipo Aplicacion" />
                                <asp:BoundField DataField="num_cuotas" HeaderText="Num.Cuotas" />
                                <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="capital" HeaderText="Capital" DataFormatString="{0:N}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="intcte" HeaderText="Int.Cte" DataFormatString="{0:N}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="intmora" HeaderText="Int.Mora" DataFormatString="{0:N}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="seguro" HeaderText="Seguro" DataFormatString="{0:N}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="leymipyme" HeaderText="Ley MiPyme" DataFormatString="{0:N}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ivaleymipyme" HeaderText="Iva Ley MiPyme" DataFormatString="{0:N}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="otros" HeaderText="Otros" DataFormatString="{0:N}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="devolucion" HeaderText="Devolucion" DataFormatString="{0:N}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                            <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                        </asp:GridView>                                
                        <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </asp:View>        
    </asp:MultiView>
    
    <asp:Label ID="msg" runat="server" Font-Bold="true" ForeColor="Red" />
</asp:Content>