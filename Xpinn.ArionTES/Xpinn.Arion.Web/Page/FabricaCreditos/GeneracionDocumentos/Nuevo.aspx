<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Credito :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="~/General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvDocumentos" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwGenerar" runat="server">
            <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                <tr>
                    <td class="tdI">
                        <asp:Panel ID="Panel1" runat="server">
                            <table style="width:100%">
                                <tr>
                                    <td style="width: 154px; text-align:left">
                                        <strong>Número Radicación</strong>
                                    </td>
                                    <td style="text-align:left">
                                        <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox"  Enabled="false" />
                                        <strong>
                                        Proceso:&nbsp;<asp:DropDownList ID="ddlProceso" runat="server" CssClass="dropdown" 
                                            Enabled="False" Height="25px" Width="186px" Visible="False">
                                        </asp:DropDownList>
                                        </strong>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <hr />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="tdI">
                        <asp:Panel ID="Panel2" runat="server">
                            <table style="width:90%;">
                                <tr>
                                    <td class="logo" colspan="3" style="text-align:left">
                                        <strong>DATOS DEL DEUDOR</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left">
                                        Identificación
                                    </td>
                                    <td style="text-align:left">
                                        Tipo Identificación
                                    </td>
                                    <td style="text-align:left">
                                        Nombre
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left">
                                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Enabled="false" />
                                    </td>
                                    <td style=" text-align:left">
                                        <asp:TextBox ID="txtTipo_identificacion" runat="server" CssClass="textbox" Enabled="false" />
                                    </td>
                                    <td style="text-align:left">
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Enabled="false" Width="350px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="tdI">
                        <asp:Panel ID="Panel3" runat="server">
                            <table style="width:70%;">
                                <tr>
                                    <td colspan="5">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" style="text-align:left">
                                        <strong>DATOS DEL CRÉDITO</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left" colspan="2">
                                        Línea de crédito
                                    </td>
                                    <td style="text-align:left">
                                        Monto
                                    </td>
                                    <td style="text-align:left">
                                        Plazo
                                    </td>
                                    <td style="text-align:left">
                                        Periodicidad
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left" colspan="2">
                                        <asp:TextBox ID="txtLinea_credito" runat="server" CssClass="textbox" 
                                            Enabled="false" Width="252px" />
                                    </td>
                                    <td style="text-align:left">
                                        <uc2:decimales ID="txtMonto" runat="server" CssClass="textbox" Enabled="false" />                                
                                    </td>
                                    <td style="text-align:left">
                                        <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Enabled="false" />
                                    </td>
                                    <td style="text-align:left">
                                        <asp:TextBox ID="txtPeriodicidad" runat="server" CssClass="textbox" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left">
                                        Valor de la Cuota
                                    </td>
                                    <td style="text-align:left">
                                        Forma de Pago
                                    </td>
                                    <td style="text-align: left">
                                        Estado
                                    </td>
                                    <td style="text-align: left">
                                
                                    </td>
                                    <td style="text-align: left">
                                
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left">
                                        <uc2:decimales ID="txtValor_cuota" runat="server" CssClass="textbox" Enabled="false" />                                
                                    </td>
                                    <td style="text-align:left">
                                        <asp:TextBox ID="txtForma_pago" runat="server" CssClass="textbox" Enabled="false" />
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
                                    </td>                            
                                    <td style="text-align: left">
                                    </td>                           
                                    <td style="text-align: left">
                                    </td>
                                </tr>
                            </table>
                            <table style="width:80%;">
                                <tr>
                                    <td colspan="5" style="text-align:left">
                                        <strong>DATOS DEL PROVEEDOR</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left">
                                        Identificación
                                    </td>
                                    <td style="text-align:left" colspan="3">
                                        Nombre
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left">
                                        <asp:TextBox ID="txtIdenProv" runat="server" CssClass="textbox" Enabled="false" />
                                    </td>
                                    <td style="text-align:left" colspan="3">
                                        <asp:TextBox ID="txtNomProv" runat="server" CssClass="textbox" Enabled="false" Width="400px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="tdI">
                        <asp:Label ID="lbDocumentos" runat="server" Text="DOCUMENTOS" 
                            style="font-weight: 700"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdI">
                        <asp:GridView ID="gvLista" runat="server" Width="100%"  GridLines="Horizontal" 
                            AutoGenerateColumns="False" AllowPaging="True" 
                            OnPageIndexChanging="gvLista_PageIndexChanging" 
                            HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" 
                            RowStyle-CssClass="gridItem" DataKeyNames="cod_linea_credito" >
                            <Columns>
                                <asp:BoundField DataField="cod_linea_credito" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" >
                                    <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                                    <ItemStyle CssClass="gridColNo"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                            ToolTip="Detalle" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="tipo_documento" HeaderText="Tipo documento" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="140px"/>
                                <asp:BoundField DataField="descripcion_documento" HeaderText="Descripción"/> 
                                <asp:BoundField DataField="requerido" HeaderText="Requerido"/>  
                                <asp:TemplateField HeaderText="Referencia">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtReferencia" runat="server"></asp:TextBox> 
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvResultado" runat="server" 
                                            ControlToValidate="txtReferencia" ErrorMessage="Campo Requerido" 
                                            SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" 
                                            Display="Dynamic"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Ruta" DataField="ruta" />
                                <asp:TemplateField HeaderText="Seleccionar">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbx" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                            <PagerStyle CssClass="gridPager"></PagerStyle>
                            <RowStyle CssClass="gridItem"></RowStyle>
                        </asp:GridView>
                        <br />
                        <asp:GridView ID="gvLista2" runat="server" Width="100%" GridLines="Horizontal" 
                            AutoGenerateColumns="False" AllowPaging="True" 
                            OnPageIndexChanging="gvLista_PageIndexChanging" 
                            HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" 
                            RowStyle-CssClass="gridItem" DataKeyNames="iddocumento" 
                            onrowdatabound="gvLista2_RowDataBound" >                             
                            <Columns>
                                <asp:BoundField DataField="iddocumento" HeaderStyle-CssClass="gridColNo" 
                                        ItemStyle-CssClass="gridColNo" >                                    
                                    <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                                    <ItemStyle CssClass="gridColNo"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                            ToolTip="Detalle" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnImprimir" runat="server" CommandName="Print" ImageUrl="~/Images/gr_imp.gif"
                                            ToolTip="Imprimir" Width="16px" onclick="btnImprimir_Click"  />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="tipo_documento" HeaderText="Tipo documento" HeaderStyle-Width="100px"/>
                                <asp:BoundField DataField="descripcion_documento" HeaderText="Descripción" HeaderStyle-Width="200px"/> 
                                <asp:BoundField DataField="referencia" HeaderText="Referencia" HeaderStyle-Width="100px"/>  
                                <asp:TemplateField HeaderText="Generar" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkGenerar" runat="server" Checked="True" Enabled="false" />                                
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ruta" HeaderText="Ruta" HeaderStyle-CssClass="gridColNo" 
                                    ItemStyle-CssClass="gridColNo">
                                    <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                                    <ItemStyle ForeColor="#CCCCCC" Width="1px" Wrap="False" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                            <PagerStyle CssClass="gridPager"></PagerStyle>
                            <RowStyle CssClass="gridItem"></RowStyle>
                        </asp:GridView>
                        <br />
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                        <br />
                        <br />
                        <asp:Button ID="btnGenerar" runat="server" CssClass="btn8" Text="Generar" 
                            onclick="btnGenerar_Click" ValidationGroup="vgGuardar" Width="94px" Height="32px" />
                        &nbsp;
                        <br />
                        <asp:Label ID="lblInfo" runat="server" Font-Bold="True"></asp:Label> 
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>        
            </table>
        </asp:View>
        <asp:View ID="vwMostrar" runat="server">
            <br />
            <asp:Button ID="btnRegresar" runat="server" CssClass="btn8" Text="Regresar a la Generación de Documentos" 
                onclick="btnRegresar_Click" ValidationGroup="vgGuardar" Width="350px" Height="30px" />
            <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx" height="700px"
                runat="server" style="border-style: groove; float: left;"></iframe>
        </asp:View>
    </asp:MultiView>
    
</asp:Content>