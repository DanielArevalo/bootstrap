<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>  
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <table style="width: 100%">
                <tr style="text-align:left"> 
                    <td class="logo" style="text-align: left; font-size: small;" 
                        colspan="3">
                        <strong>
                        <asp:Label ID="lblInicial" runat="server" Text="Seleccion el Archivo con los Datos de Reclamaciones"></asp:Label>                 
                        </strong>
                        <br /><asp:Label ID="Label1" runat="server" BackColor="White" ForeColor="#359AF2" 
                            Text="Label" Visible="False"></asp:Label>     
                        &nbsp;     
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr style="text-align:left">
                    <td class="logo" colspan="4" style="text-align: left; font-size: xx-small;">
                        Estructura del archivo: Número Radicación, NIT ENTIDAD, Fecha de Reclamación (día/mes/año),Número de Cuotas,Valor a Reclamar,Tipo de Reclamacion (1=Cuotas, 2=Total),Identificación del Deudor
                    </td>
                </tr>
                <tr style="text-align:left"> 
                    <td class="logo" style="width: 350px; text-align: left; font-size: x-small;" 
                        colspan="2">
                        <asp:FileUpload ID="FileUploadMetas" runat="server" />      
                    </td>
                    <td>
                        &nbsp;                         
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="logo" style="width: 160px; text-align: left"> 
                        <asp:Panel ID="pConsulta" runat="server">                
                            <asp:Button ID="btnCargarMetas" runat="server" CssClass="btn8" UseSubmitBehavior="False"
                                onclick="btnCargarMetas_Click" Text="Cargar Reclamaciones"   
                                Width="145px" />  
                            <asp:Button ID="btnGuardar" runat="server" CssClass="btn8" UseSubmitBehavior="False"
                                onclick="btnGuardar_Click" Text="Aplicar Reclamaciones" Width="145px" />  
                        </asp:Panel>
                    </td>                                    
                    <td class="logo" style="width: 179px; text-align: left"> 
                     </td>                                    
                    <td>                                                                                            
                    </td>
                 </tr>                              
            </table>
            &nbsp;
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                            Visible="False" />
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="text-align:left">
                        <asp:Label ID="lblFechaAplica" runat="server" Visible="False" Text="Fecha de Aplicacion" /><br />
                        <ucFecha:fecha ID="ucFecha" runat="server"  Enabled="true"/>
                    </td>
                    <td style="text-align:left">
                        <asp:Label ID="lblNumeroReclamacion" runat="server" Visible="False" Text="Número de Reclamación" /><br />
                        <asp:TextBox ID="txtNumeroReclamacion" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td style="text-align:left">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div style="overflow: scroll; height: 500px; width: 100%;">
                            <div style="width: 100%;">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                <asp:GridView ID="gvMovGeneral" runat="server" Width="100%" PageSize="3"
                                    ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" 
                                    SelectedRowStyle-Font-Size="XX-Small" 
                                        Style="font-size: small; margin-bottom: 0px;" 
                                        onrowdatabound="gvMovGeneral_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="NUMERO_RADICACION" HeaderText="Número del Crédito" />
                                        <asp:BoundField DataField="NITENTIDAD" HeaderText="Nit de la Entidad" />
                                        <asp:BoundField DataField="IDENTIFICACION" HeaderText="Cedula/Nit" />
                                        <asp:BoundField DataField="fechaproxpago" HeaderText="Fecha Próximo Pago" />
                                        <asp:BoundField DataField="CAPITAL" HeaderText="Valor" 
                                            DataFormatString="{0:N}" />
                                        <asp:BoundField DataField="CUOTAS_RECLAMAR" HeaderText="Cuota Reclamación" />
                                        <asp:TemplateField HeaderText="Reclamacion">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlreclamacion" runat="server" AutoPostBack="true" 
                                                    DataValueField='<%# Bind("reclamacion") %>'  >
                                                    <asp:ListItem Value="0"> Seleccione un Item</asp:ListItem>
                                                    <asp:ListItem Value="1">Reclamación en cuotas</asp:ListItem>
                                                    <asp:ListItem Value="2">Reclamación Total</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                        <asp:Label ID="Label2" runat="server" Text="Su consulta no obtuvo ningun resultado."
                            Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
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
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Reclamaciones Aplicadas Correctamente"></asp:Label>
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
                        Esta Seguro de Realizar la Aplicación de las Reclamaciones ?
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