<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Credito :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="uc1" %>
<%@ Register src="../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc2" %>
<%@ Register src="../../../General/Controles/PlanPagos.ascx" tagname="planpagos" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI">
                <asp:Panel ID="Panel2" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td class="logo" colspan="3" style="text-align:left">
                                <strong>DATOS DEL DEUDOR</strong>
                                <asp:Label ID="lblCodigo" runat="server" Enabled="False" Visible="False"></asp:Label>
                            </td>
                            <td class="logo" style="text-align:left">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="logo" style="width: 150px; text-align:left">
                                Identificación
                            </td>
                            <td style="text-align:left">
                                Tipo Identificación
                            </td>
                            <td style="text-align:left">
                                Nombre
                            </td>
                            <td style="text-align:left">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="logo" style="width: 150px; text-align:left">
                                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" 
                                    Enabled="false" />
                            </td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtTipo_identificacion" runat="server" CssClass="textbox" Enabled="false" />
                            </td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Enabled="false" Width="377px" />
                            </td>
                            <td style="text-align:left">
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>

    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td colspan="4" style="text-align:left">
                <strong>DATOS DEL CRÉDITO</strong>
            </td>
            <td style="text-align:left">
                &nbsp;</td>
            <td style="text-align:left">
                &nbsp;</td>
            <td style="text-align:left">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align:left; width: 151px;">
                Número Radicación
                <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox"  Enabled="false" />
            </td>
            <td colspan="3" style="text-align:left">
                Línea de crédito<br />
                <asp:TextBox ID="txtLinea_credito" runat="server" CssClass="textbox" 
                    Enabled="false" Width="347px" />
            </td>
            <td style="text-align:left">
                &nbsp;</td>
            <td style="text-align:left">
                &nbsp;</td>
            <td style="text-align:left">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align:left; width: 151px;">
                Monto
                <uc2:decimales ID="txtMonto" runat="server" Enabled="false" />                                
            </td>
            <td style="text-align:left">
                Plazo
                <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Enabled="false" />
            </td>
            <td style="text-align:left">
                Periodicidad
                <asp:TextBox ID="txtPeriodicidad" runat="server" CssClass="textbox" Enabled="false" />
            </td>
            <td style="text-align:left">
                Valor de la Cuota
                <uc2:decimales ID="txtValor_cuota" runat="server" Enabled="false" />                                
            </td>
            <td style="text-align:left">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td style="text-align:left">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td style="text-align:left">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align:left; width: 151px;">
                Forma de Pago
                <asp:TextBox ID="txtForma_pago" runat="server" CssClass="textbox" Enabled="false" />
            </td>
            <td style="text-align: left">         
                Moneda
                <asp:TextBox ID="txtMoneda" runat="server" CssClass="textbox" Enabled="false" />                       
            </td>
            <td style="text-align: left">    
                F.Proximo Pago
                <asp:TextBox ID="txtFechaProximoPago" runat="server" CssClass="textbox" Enabled="false" />                               
            </td>
            <td style="text-align: left">    
                F.Ultimo Pago 
                <asp:TextBox ID="txtFechaUltimoPago" runat="server" CssClass="textbox" Enabled="false" />                              
            </td>
            <td style="text-align: left">     
                &nbsp;</td>
            <td style="text-align: left">     
                &nbsp;</td>
            <td style="text-align: left">     
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align:left; width: 151px;">     
                Saldo Capital
                <uc2:decimales ID="txtSaldoCapital" runat="server" Enabled="false" />                              
            </td>
            <td style="text-align: left">
                Estado
                <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
            </td>
            <td style="text-align: left">   
                F.Aprobación                             
                <asp:TextBox ID="txtFechaAprobacion" runat="server" CssClass="textbox" Enabled="false" />                                         
            </td>
            <td style="text-align: left">                                
            </td>
            <td style="text-align: left">                                
                &nbsp;</td>
            <td style="text-align: left">                                
                &nbsp;</td>
            <td style="text-align: left">                                
                &nbsp;</td>
        </tr>
    </table>

    <asp:UpdatePanel ID="UpdRefinanciacion" runat="server">        
    <ContentTemplate>
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>                    
            <td colspan="2" style="text-align:left">
                <strong>DATOS DEL CASTIGO</strong>
            </td>
        </tr>
        <tr>
            <td style="text-align:left">
                Saldos por Atributo:
                <asp:GridView ID="gvLista" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" OnPageIndexChanging="gvLista_PageIndexChanging" 
                    Width="100%" DataKeyNames="cod_atr" 
                    HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" 
                    RowStyle-CssClass="gridItem" style="font-size: small" 
                    ShowFooter="True">
                    <Columns>
                        <asp:TemplateField HeaderText="Cod.Atr.">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="15px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCodAtr" runat="server" Text='<%# Eval("cod_atr") %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>     
                        <asp:TemplateField HeaderText="Descripción" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                            <ItemTemplate>
                                <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("nom_atr") %>'/>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblNomTotal" runat="server" Text='Total'/>
                            </FooterTemplate>
                        </asp:TemplateField>                                   
                        <asp:TemplateField HeaderText="Valor"  >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" Width="50px" />
                            <ItemTemplate>
                                <asp:Label ID="lblValor" runat="server" Text='<%# Bind("valor", "{0:N}") %>'/>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtTotal" runat="server" Enabled="false" CssClass="textboxAlineadoDerecha" />
                            </FooterTemplate>
                        </asp:TemplateField>                                  
                        <asp:TemplateField HeaderText="Vr.Causado"  >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" Width="50px" />
                            <ItemTemplate>
                                <asp:Label ID="lblValorCausado" runat="server" Text='<%# Bind("causado", "{0:N}") %>'/>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtTotalCausado" runat="server" Enabled="false" CssClass="textboxAlineadoDerecha" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Aplica">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                            <ItemTemplate>
                                <asp:UpdatePanel ID="UpdatePanelchkAplica" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="chkAplica" runat="server" Checked='<%# Eval("Aplica") %>' AutoPostBack="True" oncheckedchanged="chkAplica_CheckedChanged" />
                                    </ContentTemplate></asp:UpdatePanel>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BorderColor="White" BorderStyle="None" BorderWidth="0px" HorizontalAlign="Right"  />
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridPager" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>          
            </td>
            <td style="text-align:left">
                <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                    <tr>
                        <td>
                            Fecha Castigo<br />
                            <uc1:fecha ID="txtFechaCastigo" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Línea<br />
                            <asp:DropDownList ID="ddlLineaCastigo" runat="server" CssClass="dropdown" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="text-align: left">                                
                <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                    <tr>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">                                
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">                                            

                        </td>
                    </tr>
                    </table>
            </td>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table> 
    </ContentTemplate>    
    </asp:UpdatePanel>     
    
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
                        Esta Seguro de Realizar el Castigo ?
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