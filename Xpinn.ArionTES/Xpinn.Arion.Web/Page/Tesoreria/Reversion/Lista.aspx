<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>   
    <asp:MultiView ID="mvReversion" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwTipoComprobante" runat="server">
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td >
                        <hr width="100%" />
                    </td>
                </tr>
                 <tr>
                    <td style="text-align:left"> 
                        Fecha Operación
                        <asp:TextBox ID="txtFecha" enabled="false" CssClass="textbox" runat="server" 
                            Width="180px"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td style="text-align:left"> 
                        Oficina 
                        <asp:TextBox ID="txtOficina" enabled="false" CssClass="textbox" runat="server" 
                            Width="240px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr style="text-align:left">
                    <td><strong>Listado de Operaciones:</strong></td></tr>
                <tr style="text-align:left">
                    <td>
                        <div id="gvDiv">
                        <asp:GridView ID="gvOperacion" runat="server" Width="100%" 
                            AutoGenerateColumns="False" AllowPaging="False" PageSize="20" BackColor="White" 
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                            ForeColor="Black" GridLines="Vertical" style="font-size: x-small">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Anular">
                                   <ItemTemplate>
                                   <asp:CheckBox ID="chkAnula" runat="server" />
                                   </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:BoundField DataField="cod_ope" HeaderText="Cod.Ope">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_oper" DataFormatString="{0:d}" HeaderText="Fecha" />
                                <asp:BoundField DataField="tipo_ope" HeaderText="Tip.Ope."/>
                                <asp:BoundField DataField="nom_tipo_ope" HeaderText="Tipo Operación" />
                                <asp:BoundField DataField="cod_cliente" HeaderText="Cod.Persona" />
                                <asp:BoundField DataField="identificacion" HeaderText="Identificación" />                            
                                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="cod_ofi" HeaderText="Cod.Ofi." />
                                <asp:BoundField DataField="nom_usuario" HeaderText="Nombre Usuario" />
                                <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N0}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>                                
                                <asp:BoundField DataField="observacion" HeaderText="Observación" />
                                <asp:BoundField DataField="cod_proceso" HeaderText="Cod.Pro"/>
                                <asp:BoundField DataField="cod_cuenta" HeaderText="Cod.Cuenta"/>
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
                        </div>
                    </td>
                </tr>
                <tr><td>&#160;</td></tr>
                <tr>
                    <td style="text-align:left">
                        Usuario que Anula
                        <asp:TextBox ID="txtCajero" runat="server" CssClass="textbox" Width="228px" enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">                        
                        <asp:Label ID="lblTitMotivoAnula" runat="server" Text="Motivo de Anulación"></asp:Label>
                        <asp:DropDownList ID="ddlMotivoAnulacion" runat="server" Height="25px" Width="244px">
                        </asp:DropDownList>    
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View1" runat="server">
            <asp:Panel id="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br /><br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Operaciones Reversadas Correctamente"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Button ID="btnSeguir" runat="server" Text="Continuar" onclick="btnSeguir_Click" />
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
                        Esta Seguro de Generar los Comprobantes ?
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

</asp:Content>

