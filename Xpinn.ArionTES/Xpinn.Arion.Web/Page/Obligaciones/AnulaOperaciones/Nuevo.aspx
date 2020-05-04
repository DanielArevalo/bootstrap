<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Lista" Title=".: Xpinn - Credito :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvOperacion" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="pConsulta" runat="server">
                        <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                           <tr>
                               <td class="tdI" style="font-size: small; text-align: left" colspan="3">
                                   <strong>DATOS DE LA OPERACION</strong></td>
                           </tr>
                            <tr>
                                <td class="tdI" style="width: 439px; text-align:left">
                                    Codigo Operación<br/>
                                    <asp:TextBox ID="txtoperacion" runat="server" CssClass="textbox" 
                                        MaxLength="128" Width="158px" Enabled="False" />
                                </td>
                                <td class="tdD" style="width: 439px; text-align:left">
                                    Tipo&nbsp; Operación<br />                        
                                    <asp:TextBox ID="txttipooperacion" runat="server" CssClass="textbox" 
                                        MaxLength="128" Width="158px" Enabled="False" />
                                </td>
                                <td class="tdD" style="width: 439px; text-align:left">
                                    Numero Comprobante<br/>                                  
                                    <asp:TextBox ID="txtcomprobante" runat="server" CssClass="textbox" 
                                       MaxLength="128" Width="152px" Enabled="False" />
                               </td>
                               <td class="tdD" style="width: 439px; text-align:left">
                                    Tipo Comprobante&nbsp;<br/>                              
                                    <asp:TextBox ID="txttipocomprobante" runat="server" CssClass="textbox" 
                                        MaxLength="128" Width="158px" Enabled="False" />
                                </td>
                                <td class="tdD" style="width: 439px; text-align:left">
                                    &nbsp;</td>
                                <td class="tdD" style="width: 439px; text-align:left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="tdD" style="width: 439px; text-align:left">              
                                    Usuario<br />            
                                    <asp:TextBox ID="txtusuario" runat="server" CssClass="textbox" 
                                        MaxLength="128" Width="158px" Enabled="False" />             
                                </td>
                                <td class="tdD" colspan="2" style="width: 439px; text-align:left">              
                                    Nombre Usuario<br />                 
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" 
                                      MaxLength="128" Width="319px" Enabled="False" />              
                                </td>    
                                <td class="tdD" style="width: 439px; text-align:left">              
                                    Oficina<br />          
                                    <asp:TextBox ID="txtoficina" runat="server" CssClass="textbox" 
                                        MaxLength="128" Width="158px" Enabled="False" />              
                                </td> 
                                <td class="tdD" style="width: 439px; text-align:left">
                                    &nbsp;</td>
                                <td class="tdD" style="width: 439px; text-align:left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                               <td class="tdI" style="width: 439px; text-align:left"">
                                   Estado<br />                           
                                   <asp:TextBox ID="txtestado" runat="server" CssClass="textbox" 
                                       MaxLength="128" Width="158px" Enabled="False" />
                                   <br/>
                               </td>
                               <td class="tdI" style="width: 439px; text-align:left"">
                                   Núm. Lista<br />                    
                                   <asp:TextBox ID="txtnumlista" runat="server" CssClass="textbox" 
                                       MaxLength="128" Width="158px" Enabled="False" />
                                   <br/>
                               </td>
                               <td class="tdD">
                                   Fecha Operación<br />                          
                                   <asp:TextBox ID="txtfechaope" runat="server" CssClass="textbox" 
                                       MaxLength="128" Width="158px" Enabled="False" />
                               </td>
                               <td class="tdD">
                                   Fecha Real<br />                          
                                   <asp:TextBox ID="txtfechareal" runat="server" CssClass="textbox" 
                                       MaxLength="128" Width="158px" Enabled="False" />
                               </td>
                               <td class="tdD">
                                   &nbsp;
                               </td>
                               <td class="tdD">
                                   &nbsp;
                               </td>
                           </tr>
                            <tr>
                                <td class="tdI" colspan="4" style="text-align:left">
                                    <asp:Label ID="lblAnulacion" runat="server"></asp:Label>
                                </td>
                                <td class="tdD">
                                    &nbsp;</td>
                                <td class="tdD">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="tdI" colspan="3" style="text-align:left">
                                    <strong>DATOS DE LA ANULACION</strong></td>
                                <td class="tdD">
                                    &nbsp;</td>
                                <td class="tdD">
                                    &nbsp;</td>
                                <td class="tdD">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="tdI" style="width: 439px; text-align:left">
                                    Fecha Anulación</td>
                                <td class="tdI" style="width: 439px; text-align:left">
                                    Motivo de Anulación</td>
                                <td class="tdD">
                                    &nbsp;</td>
                                <td class="tdD">
                                    &nbsp;</td>
                                <td class="tdD">
                                    &nbsp;</td>
                                <td class="tdD">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="tdI" style="width: 439px; text-align:left">
                                    <uc1:fecha ID="txtFechaAnulacion" runat="server" ></uc1:fecha>
                                </td>
                                <td class="tdI" style="text-align:left" colspan="2">
                                    <asp:DropDownList ID="ddlMotivoAnulacion" runat="server" Width="244px" 
                                        CssClass="dropdown">
                                    </asp:DropDownList>
                                </td>
                                <td class="tdD">
                                    &nbsp;</td>
                                <td class="tdD">
                                    &nbsp;</td>
                                <td class="tdD">
                                    &nbsp;</td>
                            </tr>
                        </table>
                        </asp:Panel>
                    </td>
                </tr>
                 <tr>
                    <td><hr /></td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <strong><span style="font-size: small">DATOS DE LAS TRANSACCIONES DE LA OPERACION</span></strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvLista" runat="server" Width="100%"  GridLines="Horizontal" AutoGenerateColumns="False" 
                         AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging"  
                            PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" 
                            RowStyle-CssClass="gridItem"  DataKeyNames="NUM_TRAN" style="font-size: x-small" >
                        <Columns>                 
                            <asp:BoundField DataField="COD_OPE" HeaderText="Cod Ope" Visible="False" > <ItemStyle HorizontalAlign="center" /> </asp:BoundField>
                            <asp:BoundField DataField="NUM_TRAN" HeaderText="Num Transacción"> 
                                <ItemStyle HorizontalAlign="Left" /> </asp:BoundField>
                            <asp:BoundField DataField="NUMERO_RADICACION" HeaderText="Radicación"> 
                                <ItemStyle HorizontalAlign="Left" /> </asp:BoundField>
                            <asp:BoundField DataField="COD_LINEA_CREDITO" HeaderText="Cod Línea"> <ItemStyle HorizontalAlign="center" /> </asp:BoundField>
                            <asp:BoundField DataField="NOMBRE_LINEA" HeaderText="Nombre Línea"> <ItemStyle HorizontalAlign="left" /> </asp:BoundField>
                            <asp:BoundField DataField="CLIENTE" HeaderText="Cliente"> <ItemStyle HorizontalAlign="center" /> </asp:BoundField>
                            <asp:BoundField DataField="ATRIBUTO" HeaderText="Atributo"  > 
                                <ItemStyle HorizontalAlign="Left" /> </asp:BoundField>                    
                            <asp:BoundField DataField="TIPO_TRAN" HeaderText="Tipo Transacción"  > <ItemStyle HorizontalAlign="center" /> </asp:BoundField>
                            <asp:BoundField DataField="TIPO_MOV" HeaderText="Tipo Movimiento"  > <ItemStyle HorizontalAlign="center" /> </asp:BoundField>
                            <asp:BoundField DataField="VALOR" HeaderText="Valor" DataFormatString="{0:c}"  > 
                            <ItemStyle HorizontalAlign="Right" /> </asp:BoundField>                                                           
                         </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                    </td>
                </tr>
            </table>    
        </asp:View>
        <asp:View ID="mvFinal" runat="server">
            <asp:Panel id="Panel1" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br /><br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensajeFinal" runat="server" Text="Operación Anulada Correctamente"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Button ID="btnSeguir" runat="server" Text="Continuar" 
                                onclick="btnSeguir_Click" />
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
                        Esta Seguro de Realizar esta Anulacion ?
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
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn8" 
                            Width="182px" onclick="btnCancelar_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>       
        </div>
    </asp:Panel>
</asp:Content>
