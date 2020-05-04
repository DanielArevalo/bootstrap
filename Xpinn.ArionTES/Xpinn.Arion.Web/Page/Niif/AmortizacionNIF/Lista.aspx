<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Expinn - Costo Amortizado :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:Panel ID="pConsulta" runat="server">
        <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td style="width: 40%; text-align: right;">
                    Fecha de Costeo :
                    <br />
                </td>
                <td style="width: 60%; text-align: left;">
                    <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" MaxLength="1" 
                        Width="148px" />
                </td>
               
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pDatos" runat="server">
        <hr style="width: 100%" />
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:Button ID="btnExportar" runat="server" CssClass="btn8" onclick="btnExportar_Click" Text="Exportar a Excel"/>                            
                    <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False" style="font-size: x-small" ShowFooter="True"
                        AllowPaging="True" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" PageSize="50"
                        OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                        OnRowCommand="gvLista_RowCommand"
                        OnPageIndexChanging="gvLista_PageIndexChanging" 
                        onrowdatabound="gvLista_RowDataBound">
                        <Columns>                                                       
                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnDetalle" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" 
                                        CommandName="Detalle" ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle Amortización" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Id Credito" DataField="numero_radicacion">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Identificación" DataField="identificacion">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Nombre" DataField="nombre">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Monto Inicial" DataField="monto" DataFormatString="{0:n0}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Plazo Inicial" DataField="plazo">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Cuota" DataFormatString="{0:n0}" DataField="cuota">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Tasa" DataField="tasa">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nomtipo_tasa" HeaderText="Tipo de Tasa" />
                            <asp:BoundField HeaderText="Saldo Capital" DataField="saldo_capital" DataFormatString="{0:n0}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Saldo Total" DataField="saldo_total" DataFormatString="{0:n0}" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Plazo Faltante" DataField="plazo_faltante" />
                            <asp:BoundField HeaderText="Tasa Mercado" DataField="tasa_mercado" />
                            <asp:BoundField HeaderText="TIR" DataField="tir" />
                            <asp:BoundField DataField="valor_presente" DataFormatString="{0:n0}" HeaderText="Valor Presente">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Ajuste" DataFormatString="{0:n0}" DataField="valor_ajuste">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>
                    <asp:Label ID="lblTotalRegs" runat="server"/>
                </td>
            </tr>
        </table>
    </asp:Panel>

     <asp:ModalPopupExtender ID="MpeDetalle" runat="server" Enabled="True" PopupDragHandleControlID="Panelf"
        PopupControlID="PanelDetalle" TargetControlID="HiddenField1" CancelControlID="btnCloseAct">
        <Animations>
            <OnHiding>
                <Sequence>                            
                    <StyleAction AnimationTarget="btnCloseAct" Attribute="display" Value="none" />
                    <Parallel>
                        <FadeOut />
                        <Scale ScaleFactor="5" />
                    </Parallel>
                </Sequence>
            </OnHiding>            
        </Animations>
    </asp:ModalPopupExtender>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:Panel ID="PanelDetalle" runat="server" Width="480px" Style="display: none; border: solid 2px Gray" CssClass="modalPopup"  >
        <asp:UpdatePanel ID="upDetalle" runat="server" >
        <ContentTemplate>  
            <table>
                <tr>
                    <td style="font-size: x-small: color: #FFFFFF; background-color: #0066FF; width: 434px">
                        <asp:Panel ID="Panelf" runat="server" Width="475px" style="cursor: move">
                            <strong>Detalle de la Amortización</strong>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 475px">
                        <asp:GridView ID="gvDetalle" runat="server" 
                            AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                            BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" 
                            GridLines="Vertical" PageSize="20" Width="300px" 
                            style="text-align: left; font-size: xx-small">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="NumCuota" HeaderText="No." DataFormatString="{0:N0}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FechaCuota" HeaderText="F.Pago" DataFormatString="{0:d}" >
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Capital" HeaderText="Capital" DataFormatString="{0:N0}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IntCte" HeaderText="Int.Cte" DataFormatString="{0:N0}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IntMora" HeaderText="Int.Mora" DataFormatString="{0:N0}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LeyMiPyme" HeaderText="Ley MiPyme" DataFormatString="{0:N0}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IvaLeyMiPyme" HeaderText="Iva Ley MiPyme" DataFormatString="{0:N0}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Otros" HeaderText="Otros" DataFormatString="{0:N0}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Total" HeaderText="Flujo" DataFormatString="{0:N0}" >
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
                   </td>
               </tr>
               <tr>
                   <td style="width: 475px; background-color: #0066FF">
                        <asp:Button ID="btnCloseAct" runat="server" Text="Cerrar" CssClass="button" onclick="btnCloseAct_Click" CausesValidation="false" Height="20px" />
                   </td>
               </tr>
           </table>
       </ContentTemplate>
       </asp:UpdatePanel>
    </asp:Panel>    
    
    <asp:HiddenField ID="hfNiif" runat="server" />
   
    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>

</asp:Content>