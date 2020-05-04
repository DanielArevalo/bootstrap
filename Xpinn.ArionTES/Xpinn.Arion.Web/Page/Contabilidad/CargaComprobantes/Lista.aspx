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
                <tr style="text-align: left">
                    <td colspan="4">
                        <strong>Escoja el tipo de comprobante a generar</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="3">
                        <asp:RadioButton ID="rbIngreso" runat="server" Text="Comprobante de Ingreso"
                            AutoPostBack="True" OnCheckedChanged="rbIngreso_CheckedChanged" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbEgreso" runat="server" Text="Comprobante de Egreso"
                            AutoPostBack="True" OnCheckedChanged="rbEgreso_CheckedChanged" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbContable" runat="server" Text="Comprobante Contable"
                            AutoPostBack="True" OnCheckedChanged="rbContable_CheckedChanged" />
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="4">
                        <strong>Escoja el tipo de norma a cargar</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="4">
                        <asp:RadioButtonList ID="rblTipoNorma" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="L" Selected="True">Local</asp:ListItem>
                            <asp:ListItem Value="N">Niif</asp:ListItem>
                            <asp:ListItem Value="A">Ambos</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr style="text-align:left"> 
                    <td class="logo" style="text-align: left; font-size: small;" 
                        colspan="3">
                        <strong>
                        <asp:Label ID="lblInicial" runat="server" Text="Seleccion el Archivo con los Datos de Comprobante"></asp:Label>                 
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
                        Para carga del comprobante el sistema cojera desde la 2da Fila del archivo.<br />
                        Estructura del archivo: Cuenta, Centro Costo, 
                        Detalle,Tipo,Valor,Identificación Tercero,Centro Gestion(Opcional)
                    </td>
                </tr>
                <tr style="text-align:left"> 
                    <td class="logo" style="width: 350px; text-align: left; font-size: x-small;" 
                        colspan="2">
                        <asp:FileUpload ID="FileUploadComprobante" runat="server" />      
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="logo" style="width: 160px; text-align: left" colspan="2"> 
                        <asp:Panel ID="pConsulta" runat="server">                
                            <asp:Button ID="btnCargarComp" runat="server" CssClass="btn8" 
                                onclick="btnCargarComp_Click" Text="Cargar Comprobante"   
                                Width="145px" />  
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnPegarComp" runat="server" CssClass="btn8" 
                                onclick="btnPegarComp_Click" Text="Copiar del Clipboard"   
                                Width="145px" />
                        </asp:Panel>
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
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td colspan="2">
                        <div style="overflow: scroll; height: 500px; width: 100%;">
                            <div style="width: 100%;">
                                <asp:UpdatePanel ID="upComprobante" runat="server">
                                <ContentTemplate>
                                <asp:GridView ID="gvLista" runat="server" Width="100%" PageSize="3"
                                    ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" 
                                    SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small; margin-bottom: 0px;" 
                                    onrowdatabound="gvLista_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="cod_cuenta" HeaderText="Cuenta" ><ItemStyle Width="80px" /></asp:BoundField>
                                        <asp:BoundField DataField="centro_costo" HeaderText="Centro Costo" ><ItemStyle Width="50px" /></asp:BoundField>
                                        <asp:BoundField DataField="detalle" HeaderText="Detalle" ><ItemStyle Width="180px" HorizontalAlign="Left" /></asp:BoundField>
                                        <asp:BoundField DataField="tipo" HeaderText="Tipo" ><ItemStyle Width="40px" /></asp:BoundField>
                                        <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N2}" ><ItemStyle Width="140px" HorizontalAlign="Right"/></asp:BoundField>
                                        <asp:BoundField DataField="identificacion" HeaderText="Ident Tercero" ><ItemStyle Width="120px" /></asp:BoundField>
                                        <asp:BoundField DataField="centro_gestion" HeaderText="Centro Gestion" ><ItemStyle Width="40px" /></asp:BoundField>                                        
                                        <asp:BoundField DataField="tercero" HeaderText="Cod Tercero" ><ItemStyle Width="10px" /></asp:BoundField>
                                        <asp:BoundField DataField="nom_tercero" HeaderText="Nombre Tercero" ><ItemStyle Width="10px" /></asp:BoundField>
                                        <asp:BoundField DataField="nombre_cuenta" HeaderText="Nombre Cuenta" ><ItemStyle Width="1px" /></asp:BoundField>
                                        <asp:BoundField DataField="maneja_ter" HeaderText="MT" ><ItemStyle Width="1px" /></asp:BoundField>
                                        <asp:BoundField DataField="impuesto" HeaderText="IM" ><ItemStyle Width="1px" /></asp:BoundField>
                                        <asp:BoundField DataField="cod_cuenta_niif" HeaderText="Cuenta NIIF" ><ItemStyle Width="1px" /></asp:BoundField>
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
                        Esta Seguro de Realizar la carga de los datos</td>
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
    <asp:Label ID="msg" runat="server" Font-Bold="true" ForeColor="Red" />

    <asp:ModalPopupExtender ID="mpePegar" runat="server" 
        PopupControlID="panelClipboard" TargetControlID="HiddenField1"
        BackgroundCssClass="backgroundColor" >
    </asp:ModalPopupExtender>
   
    <asp:Panel ID="panelClipboard" runat="server" BackColor="White" Style="text-align: right" BorderWidth="1px" Width="500px" >
        <div id="Div1" style="width: 500px">
            <table style="width: 100%;">
                <tr>
                    <td colspan="3" 
                        style="font-size: xx-small; text-decoration: underline; color: #00CC99">
                        <em><strong>Pegue el Texto a Cargar Aqui y Presione el Botón de Continuar</strong></em></td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align:center; height: 22px;">                        
                        <asp:TextBox ID="txtPegar" runat="server" BorderWidth="1px" Columns="7" 
                            Height="113px" ontextchanged="txtPegar_TextChanged" TextMode="MultiLine" 
                            Width="485px" BorderColor="#009933"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="text-align:center">
                        <asp:Button ID="btnContinuarPegar" runat="server" Text="Continuar"
                            CssClass="btn8"  Width="182px" onclick="btnContinuarPegar_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancelarPegar" runat="server" Text="Cancelar" CssClass="btn8" 
                            Width="182px" onclick="btnCancelarPegar_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>       
        </div>
    </asp:Panel>
</asp:Content>