<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>  
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
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
                        <ucFecha:fecha ID="ucFechaAplicacion" runat="server"  Enabled="true"/>
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
                        <div style="overflow: scroll; height: 500px; width: 100%;">
                            <div style="width: 100%;">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                <asp:GridView ID="gvMovGeneral" runat="server" Width="100%" PageSize="3"
                                    ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" 
                                    SelectedRowStyle-Font-Size="XX-Small" 
                                        Style="font-size: small; margin-bottom: 0px;" 
                                        onrowdatabound="gvMovGeneral_RowDataBound" >
                                    <Columns>
                                        <asp:BoundField DataField="iddetalle" HeaderText="Id." />  
                                        <asp:BoundField DataField="cod_cliente" HeaderText="Cod.Cli." />  
                                        <asp:BoundField DataField="identificacion" HeaderText="Cedula/Nit" >
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre" HeaderText="Nombres" >
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_producto" HeaderText="Tipo de Producto" />  
                                        <asp:BoundField DataField="numero_producto" HeaderText="Número de Producto" />                                                                                
                                        <asp:TemplateField HeaderText="Tipo Aplicacion" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <cc1:DropDownListGrid ID="ddlTipoAplicacion" runat="server" 
                                                    style="font-size:xx-small; text-align:center" Width="100px" CssClass="dropdown"
                                                    SelectedValue='<%# Bind("tipo_aplicacion") %>' 
                                                    CommandArgument='<%#Container.DataItemIndex %>' >
                                                    <asp:ListItem Value="Por Valor">Por Valor</asp:ListItem>
                                                    <asp:ListItem Value="Pago Total">Pago Total</asp:ListItem>
                                                    <asp:ListItem Value="Por Valor a Capital"></asp:ListItem>
                                                </cc1:DropDownListGrid>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="num_cuotas" HeaderText="Num.Cuotas" />
                                        <asp:BoundField DataField="valor" HeaderText="Valor a Aplicar" DataFormatString="{0:N}" >
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>                                        
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" />
                                    <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                                </asp:GridView>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
                <asp:Panel id="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br /><br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Recaudos Aplicados Correctamente"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Button ID="btnFinal" runat="server" Text="Continuar" 
                                onclick="btnFinal_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <asp:HiddenField ID="HiddenField1" runat="server" />    

    <asp:ModalPopupExtender ID="mpeNuevo" runat="server" 
        PopupControlID="panelActividadReg" TargetControlID="HiddenField1"
        BackgroundCssClass="backgroundColor" >
    </asp:ModalPopupExtender>
   
    <asp:Panel ID="panelActividadReg" runat="server" BackColor="White" Style="text-align: right" BorderWidth="1px" Width="500px" >
        <div id="popupcontainer" style="width: 500px">
            <table style="width: 100%;">
                <tr>
                    <td colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align:center">
                        Esta Seguro de Realizar la Aplicación de los Recaudos ?
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="text-align:center">
                        <asp:Button ID="btnContinuar" runat="server" Text="Continuar"
                            CssClass="btn8"  Width="182px" onclick="btnContinuar_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnParar" runat="server" Text="Cancelar" CssClass="btn8" 
                            Width="182px" onclick="btnParar_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>       
        </div>
    </asp:Panel>

    <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtCodigo').focus();
        }
        window.onload = SetFocus;
    </script>
    <asp:Label ID="msg" runat="server" Font-Bold="true" ForeColor="Red" />
</asp:Content>