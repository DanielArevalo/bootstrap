<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlFecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="MultiView1" runat="server">
       <asp:View ID="View1" runat="server">
           <div style="text-align: right; width: 97%">
               <asp:ImageButton runat="server" ID="btnCancelar" ImageUrl="~/Images/btnCancelar.jpg"
                   OnClick="btnCancelar_Click" />               
               <asp:ImageButton runat="server" ID="btnGuardar" ImageUrl="~/Images/btnGuardar.jpg"
                   ValidationGroup="vgGuardar" OnClick="btnGuardar_Click" />
           </div>
         <table cellpadding="5" cellspacing="0" style="width: 97%; margin-right: 0px; height: 329px;">       
              <%--<asp:Button ID="btnInforme" runat="server" CssClass="btn8" 
                        onclick="btnInforme0_Click" onclientclick="btnInforme0_Click" 
                        Text="Visualizar informe" />--%>
             <tr>
                 <td style="width: 100%; text-align: center; height: 36px;" colspan="6">
                     <table width="100%">
                         <tr>
                             <td style="width: 30%; text-align: center; height: 36px;">
                                 Fecha de Consignación<br />
                                 <ucFecha:fecha ID="txtFechaConsignacion" Enabled="true" runat="server" CssClass="textbox"
                                     MaxLength="10" Width="100%" Style="text-align: center"></ucFecha:fecha>
                             </td>
                             <td style="height: 36px; width: 35%; text-align: center;">
                                 Oficina<br />
                                 <asp:TextBox ID="txtOficina" runat="server" Enabled="False" CssClass="textbox" Width="150px"></asp:TextBox>
                             </td>
                             <td style="text-align: center; width: 35%; height: 36px;">
                                 Usuario<br />
                                 <asp:TextBox ID="txtCajero" runat="server" CssClass="textbox" Enabled="False" Width="150px"></asp:TextBox>
                             </td>
                         </tr>
                     </table>
                 </td>
             </tr>             
             <tr>
                <td style="background-color: #3599F7; text-align: center; height: 18px" colspan="6">
                    <strong style="color: #FFFFFF">
                    Saldo en Caja</strong>
                </td>
             </tr>
             <tr>
                <td style="text-align: center; height: 36px;width:40%" colspan="3" valign="top">
                    Valor en Efectivo<br/>
                    <asp:TextBox ID="txtValorEfectivo" runat="server" Enabled="false" CssClass="textbox" Width="150px" style="text-align: right"></asp:TextBox>
                    <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtValorEfectivo" Mask="999,999,999,999,999" MessageValidatorTip="true" 
                        OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft" 
                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True">
                    </asp:MaskedEditExtender>
                    <br />
                </td>
                <td style="text-align: center; height: 36px;width:60%" colspan="3" valign="top">
                        Valor en Cheque<br /><asp:TextBox ID="txtValorCheque" runat="server" 
                            Enabled="false" CssClass="textbox" Width="150px" style="text-align: right"></asp:TextBox>
                        <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtValorCheque" Mask="999,999,999,999,999" MessageValidatorTip="true" 
                        OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft" 
                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True">
                    </asp:MaskedEditExtender>
                        <br/>
                </td>
             </tr>
             <tr>
                <td style="background-color: #3599F7; text-align: center; height: 20px" colspan="6">
                    <strong style="color: #FFFFFF">
                    Datos de la Consignación</strong>
                </td>
             </tr>
             <tr>   
                 <td style="width:20%; height: 36px; text-align: center;" valign="top" colspan="2">
                     Banco<br />
                     <asp:DropDownList ID="ddlBancos" runat="server" AutoPostBack="True" CssClass="textbox"
                         OnSelectedIndexChanged="ddlBancos_SelectedIndexChanged" Width="162px">
                     </asp:DropDownList>
                 </td>
                 <td style="width:20%;height: 36px; text-align: center;" valign="top">
                     Cuenta<br />
                     <asp:DropDownList ID="ddlCuenta" runat="server" AutoPostBack="True" CssClass="textbox"
                         Style="text-align: left" Width="167px">
                     </asp:DropDownList>
                 </td>                
                <td style="width:20%;height: 36px; text-align: center;" valign="top">
                    Moneda<br style="font-size: xx-small"/>
                    <asp:DropDownList ID="ddlMonedas" CssClass="textbox"  runat="server" 
                        Width="120px" onselectedindexchanged="ddlMonedas_SelectedIndexChanged" 
                        AutoPostBack="true">
                    </asp:DropDownList> 
                </td>
                <td style="width: 20%; text-align: center;" valign="top">
                    Vr Consignar Cheque
                    <br/>
                    <asp:TextBox ID="txtValorConsigCheque" runat="server" enabled="false" 
                        Width="150px" CssClass="textbox" style="text-align: right"></asp:TextBox>
                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtValorConsigCheque" Mask="999,999,999,999,999" MessageValidatorTip="true" 
                        OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft" 
                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True">
                    </asp:MaskedEditExtender>
                </td>
                <td style="width:20%; text-align: center;" valign="top">
                    Vr Consignar Efectivo<br/>
                    <uc1:decimales ID="txtValorConsigEfecty" runat="server" CssClass="textbox" Width="160px" 
                    MaxLength="9" style="text-align: right"></uc1:decimales>
                </td>             
             </tr>
             <tr> 
                <td style="text-align: left;" valign="top" colspan="3">
                     Observaciones<br />
                        <asp:TextBox ID="txtObservacion" runat="server" TextMode="MultiLine" Height="30px" CssClass="textbox"  
                        Width="363px" style="margin-right: 0px; margin-left: 0px;"></asp:TextBox>
                </td>
                <td style="text-align: center;" valign="top">
                    <br />
                    <asp:Button ID="btnValoEfe" runat="server" Text="Calcular Total a Consignar" CssClass="btn8" 
                        onclick="btnValoEfe_Click" Width="162px" Height="33px" />
                 </td>
                <td style="text-align: center;" valign="top">
                    Total Consignación
                    <br /><asp:TextBox ID="txtValorTotalAConsig" runat="server" enabled="false" Width="150px" CssClass="textbox"  style="text-align: right"></asp:TextBox>
                    <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtValorTotalAConsig" Mask="999,999,999,999,999" MessageValidatorTip="true" 
                        OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft" 
                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True">
                    </asp:MaskedEditExtender>
                </td>
                 <td style="width:260px; text-align: center;" valign="top">
                     &nbsp;</td>
             </tr>
         </table>
         <table style="width: 970px;">
            <tr>
                <td style="width: 900px; text-align: center;" class="align-rt">
                    <strong>Cheques a Consignar</strong>
                </td>
                </tr>
                <tr> 
                <td style="width: 900px" align="center">
                    <div style="overflow: scroll; max-height: 400px; max-width: 100%">
                        <asp:GridView ID="gvConsignacion" runat="server" BackColor="White" BorderColor="#DEDFDE"
                            BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" AutoGenerateColumns="false"
                            GridLines="Vertical" Style="text-align: center">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="cod_movimiento" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                                <asp:BoundField DataField="fec_ope" HeaderText="Fecha Operación" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="num_documento" HeaderText="Núm. Cheque" />
                                <asp:BoundField DataField="cod_banco" HeaderText="Banco" />
                                <asp:BoundField DataField="nom_banco" HeaderText="Nombre Banco" />
                                <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nom_moneda" HeaderText="Moneda" />
                                <asp:TemplateField HeaderText="Recibe">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRecibe" runat="server" OnCheckedChanged="chkRecibe_CheckedChanged"
                                            AutoPostBack="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle BackColor="#F7F7DE" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                    </div>
                    
            </td>
        </tr>
    </table> 
       </asp:View>     
    </asp:MultiView>
</asp:Content>

