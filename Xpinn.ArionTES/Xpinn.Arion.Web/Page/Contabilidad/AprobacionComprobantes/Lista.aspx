<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   
    <script type="text/javascript">

        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: 1000,
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

    <asp:MultiView ID="mvAprobarComprobante" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwConsulta" runat="server">   
            <asp:Panel ID="pConsulta" runat="server" Width="100%">
                <table style="width: 70%;">
                    <tr>
                        <td style="font-size: x-small; text-align:left" colspan="5">
                            <strong>Críterios de Búsqueda</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 180px; text-align:left">
                            Número de Comprobante</td>
                        <td colspan="2" style="text-align:left">
                            <asp:TextBox ID="txtNumComp" runat="server" CssClass="textbox" Width="121px"></asp:TextBox>
                        </td>
                        <td style="text-align:left">
                            Tipo de Comprobante</td>
                        <td style="text-align:left">
                            <asp:DropDownList ID="ddlTipoComprobante" runat="server" CssClass="textbox" 
                                Width="190px" AppendDataBoundItems="True" >
                                <asp:ListItem Value="0">Seleccione un Item</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 180px; text-align:left">
                            Fecha Elaboración
                        </td>
                        <td style="width: 20px; text-align:left">
                            <uc1:fecha ID="txtFechaIni" runat="server"></uc1:fecha>
                        </td>
                        <td style="text-align:left">
                            a
                        </td>
                        <td style="text-align:left">
                            <uc1:fecha ID="txtFechaFin" runat="server"></uc1:fecha>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="Listado" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:Button ID="btnAprobarTodos" runat="server" Text="Aprobar Todos" CssClass="btn8" OnClick="btnAprobarTodos_Click" /><br />
                            <asp:GridView ID="gvLista" runat="server" AllowPaging="True" 
                                AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                                BorderStyle="None" BorderWidth="1px" CellPadding="4"  
                                ForeColor="Black" GridLines="Vertical" PageSize="20" Width="100%"
                                OnSelectedIndexChanged="gvLista_SelectedIndexChanged" 
                                onrowediting="gvLista_RowEditing" DataKeyNames="estado"
                                onpageindexchanging="gvLista_PageIndexChanging" style="font-size: x-small">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                                ToolTip="Detalle" Width="16px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="num_comp" HeaderText="Número" />
                                    <asp:BoundField DataField="tipo_comp" HeaderText="Tipo" />
                                    <asp:BoundField DataField="fecha" DataFormatString="{0:d}" HeaderText="Fecha" />
                                    <asp:BoundField DataField="descripcion_concepto" HeaderText="Concepto" />
                                    <asp:BoundField DataField="ciudad" HeaderText="Ciudad" />
                                    <asp:BoundField DataField="iden_benef" HeaderText="Identificacion" />
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="elaboro" HeaderText="Elaborado por" />
                                    <asp:BoundField DataField="aprobo" HeaderText="Aprobado por" />
                                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                                    <asp:BoundField DataField="totalcom" DataFormatString="{0:N0}" HeaderText="Valor"> <ItemStyle HorizontalAlign="Right" /></asp:BoundField>
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
                            <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                            <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado." Visible="False"/>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwDetalle" runat="server"> 
            <asp:Label ID="lblMensaje" runat="server" style="text-align:left" 
                 Font-Bold="True" ForeColor="#00CC99"></asp:Label> 
            <asp:Panel ID="PanelEncabezado" runat="server" style="width:90%;">
                <asp:Button ID="btnAnterior" runat="server" Text="Ir al Anterior Comprobante" CssClass="btn8" OnClick="btnAnterior_Click" />   &nbsp;&nbsp;
                <asp:Button ID="btnAprobar" runat="server" Text="Aprobar Comprobante" CssClass="btn8" OnClick="btnAprobar_Click" />   &nbsp;&nbsp;                
                <asp:Button ID="btnAnular" runat="server" Text="Anular Comprobante" CssClass="btn8" OnClick="btnAnular_Click" />   &nbsp;&nbsp;
                <asp:Button ID="btnSiguiente" runat="server" Text="Ir al Siguiente Comprobante" CssClass="btn8" OnClick="btnSiguiente_Click" />                                         
                <table >
                    <tr>
                        <td class="logo" style="width: 353px; text-align:left" >
                            <b>Comprobante No.</b>
                        </td>
                        <td style="width: 158px; text-align:left">
                            <asp:TextBox ID="txtNumeroComp" runat="server" CssClass="textbox" 
                                enabled="false" Width="139px"></asp:TextBox>
                        </td>
                        <td style="width: 84px;text-align:left">
                            &nbsp;
                        </td>
                        <td class="logo" style="text-align:left; width: 45px;">
                            Fecha
                        </td>
                        <td class="gridIco" style="width: 141px;text-align:left">
                            <asp:TextBox ID="txtFecha" runat="server" CssClass="textbox" enabled="false" 
                                Width="120px"></asp:TextBox>
                        </td>
                        <td style="text-align:right; width: 166px;" class="gridIco">
                            <asp:Label ID="lblSoporte" runat="server" Text="Soporte  " style="text-align:left"></asp:Label>                                            
                            <asp:TextBox ID="txtNumSop" runat="server" AutoPostBack="True" 
                                CssClass="textbox" Enabled="False" Width="80px"></asp:TextBox>
                        </td>
                        <td class="gridIco" style="width: 141px;text-align:left">
                            &nbsp;
                        </td>
                        <td style="width: 80px; text-align:left;">
                            &nbsp;
                        </td>
                        <td style="width: 80px; text-align:left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="logo" style="width: 353px; text-align:left">
                            <asp:Label ID="lblTipoComp" runat="server" Text="Tipo de Comprobante" style="text-align:left"  Enabled="False"></asp:Label></td>
                        <td style="text-align:left">
                            <asp:DropDownList ID="ddlTipoComp" runat="server" CssClass="textbox" 
                                Width="190px" style="text-align:left" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 84px;text-align:left">
                            &nbsp;</td>
                        <td style="text-align:left; width: 45px;">
                            <asp:Label ID="lblOficina" runat="server" Text="Oficina" style="text-align:left" Enabled="False" Visible="False"></asp:Label>
                            Operación
                        </td>
                        <td style="text-align:left" colspan="5">
                            <asp:TextBox ID="tbxOficina" runat="server" CssClass="textbox" enabled="false" 
                                Width="315px" style="text-align:left" Visible="False"></asp:TextBox>
                            <asp:TextBox ID="txtCod_Ope" runat="server" CssClass="textbox" enabled="false" 
                                Width="120px" style="text-align:left" />
                        </td>
                    </tr>
                    <tr>
                        <td class="logo" style="width: 353px; text-align:left">
                            <asp:Label ID="lblFormaPago" runat="server" Text="Forma de Pago" style="text-align:left"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="textbox"  Enabled="False"
                                Width="190px" style="text-align:left" >
                            </asp:DropDownList>
                        </td>
                        <td style="width: 84px; text-align:left">
                            &nbsp;
                        </td>
                        <td style="text-align:left; width: 45px;">
                            <asp:Label ID="lblEntidad" runat="server" Text="Entidad" style="text-align:left"></asp:Label>
                        </td>
                        <td style="text-align:left" colspan="5">
                            <asp:DropDownList ID="ddlEntidad" runat="server" CssClass="textbox" 
                                Width="320px" style="text-align:left"  Enabled="False">
                            </asp:DropDownList>                                                                                                                          
                        </td>
                    </tr>
                    <tr>
                        <td class="logo" style="width: 353px; text-align:left">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td style="width: 84px; text-align:left">
                            &nbsp;</td>
                        <td style="text-align:left; width: 45px;">
                            <asp:Label ID="lblCuenta" runat="server" Text="Cuenta" style="text-align:left"></asp:Label></td>
                        <td style="text-align:left" colspan="5">
                            <asp:DropDownList ID="ddlCuenta" runat="server" CssClass="textbox" 
                                    style="text-align:left" Width="320px"  Enabled="False">
                            </asp:DropDownList>                                         
                        </td>
                    </tr>
                    <tr>
                        <td class="logo" style="width: 353px; text-align:left">
                            Ciudad
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCiudad" runat="server" CssClass="textbox"  Enabled="False"
                                Width="190px" style="text-align:left">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 84px;text-align:left">
                            &nbsp;</td>
                        <td style="text-align:left; width: 45px;">
                            Concepto</td>
                        <td style="text-align:left" colspan="5">
                            <asp:DropDownList ID="ddlConcepto" runat="server" CssClass="textbox"  Enabled="False"
                                Width="320px" style="text-align:left">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7" style="text-align:left">
                            <strong>Beneficiario</strong></td>
                    </tr>
                    <tr>
                        <td style="width: 108px; text-align:left">
                            Identificación</td>
                        <td style="width: 237px; text-align:left">
                            Tipo Identificación</td>
                        <td style="width: 84px; text-align:left">
                            &nbsp;
                        </td>
                        <td style="text-align:left" colspan="4">
                            Nombres y Apellidos&nbsp; </td>
                    </tr>
                    <tr>
                        <td style="width: 108px; text-align:left">
                            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" 
                                Width="125px" style="text-align:left" ></asp:TextBox>
                        </td>
                        <td style="width: 237px; text-align:left">
                            <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="textbox" 
                                Width="156px" style="text-align:left">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 84px; text-align:left">
                            &nbsp;
                        </td>
                        <td style="text-align:left" colspan="4">
                            <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="474px" 
                                style="text-align:left" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="font-size: xx-small">
                            <asp:GridView ID="gvDetMovs" runat="server" AutoGenerateColumns="false" AllowPaging="True" 
                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" 
                                BorderWidth="1px"  GridLines="Horizontal" 
                                CellPadding="4" ForeColor="Black"  Height="131px" ShowFooter="True" 
                                PageSize="30" style="font-size: xx-small; " Width="110%" 
                                DataKeyNames="codigo" onpageindexchanging="gvDetMovs_PageIndexChanging" >                                                       
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Cod. Cuenta" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCodCuenta" runat="server" style="font-size:xx-small; text-align:left" Text='<%# Bind("cod_cuenta") %>' Width="70"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nom. Cuenta">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNomCuenta" runat="server" style="font-size:xx-small; text-align:left" Text='<%# Bind("nombre_cuenta") %>' Width="120"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Moneda" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMoneda" runat="server" style="font-size:xx-small; text-align:left" Text='<%# Bind("nom_moneda") %>' Width="60"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="C/C" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCentroCosto" runat="server" style="font-size:xx-small; text-align:center" Text='<%# Bind("centro_costo") %>' Width="20"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="C/G" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCentroGestion" runat="server" style="font-size:xx-small; text-align:center" Text='<%# Bind("centro_gestion") %>' Width="10"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Detalle" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDetalle" runat="server" style="font-size:xx-small; text-align:left" Text='<%# Bind("detalle") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tipo Movimiento" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTipo" runat="server" style="font-size:xx-small; text-align:center" Text='<%# Bind("tipo") %>' Width="40"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Valor" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblValor" runat="server" style="font-size:xx-small; text-align:right" Text='<%# Eval("valor", "{0:N2}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cod.Ter" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTercero" runat="server" style="font-size:xx-small; text-align:left" Text='<%# Bind("tercero") %>' Width="40"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Identificación" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIdentificacion" runat="server" style="font-size:xx-small; text-align:left" Text='<%# Bind("identificacion") %>' Width="40"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nombre Tercero" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNombre" runat="server" style="font-size:xx-small; text-align:left" Text='<%# Bind("nom_tercero") %>' > </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="gridHeader" />
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
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td valign="top" rowspan="2" style="text-align:left">
                            <strong>Observaciones</strong>
                        </td>
                        <td valign="top" rowspan="2" style="text-align:left; width:300px" width="70">
                            <asp:TextBox ID="tbxObservaciones" runat="server" Height="57px" 
                                TextMode="MultiLine" Width="460px"></asp:TextBox>
                        </td>
                        <td style="text-align:center">
                            Total Debitos</td>
                        <td style="text-align:center">
                            Total Creditos</td>
                    </tr>
                    <tr>
                        <td style="text-align:left">
                            <uc1:decimales ID="tbxTotalDebitos" runat="server" Enabled="false" 
                                CssClass="textbox" style="text-align:right"></uc1:decimales>                                        
                        </td>                                    
                        <td style="text-align:left">
                            <uc1:decimales ID="tbxTotalCreditos" runat="server" Enabled="false" 
                                CssClass="textbox" style="text-align:right"></uc1:decimales>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <hr />                               
                        </td>
                    </tr>                         
                    <tr>
                        <td style="text-align:left">
                            Elaborado Por
                        </td>
                        <td style="text-align:left">
                            <asp:TextBox ID="txtCodElabora" runat="server" CssClass="textbox" Visible="False" 
                                Width="23px"></asp:TextBox>
                            <asp:TextBox ID="txtElaboradoPor" runat="server" CssClass="textbox" 
                                Width="477px" style="text-align:left"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" Visible="False" 
                                Width="23px" Enabled="False" style="text-align:left"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodAprobo" runat="server" CssClass="textbox" Visible="False" 
                                Width="23px" Enabled="False" style="text-align:left"></asp:TextBox>
                        </td>
                    </tr>
                </table>                                    
            </asp:Panel>                
        </asp:View>
    </asp:MultiView>

    <asp:HiddenField ID="HiddenField1" runat="server" />    

    <asp:ModalPopupExtender ID="mpeVerificar" runat="server" 
        PopupControlID="panelVerificar" TargetControlID="HiddenField1"
        BackgroundCssClass="backgroundColor" >
    </asp:ModalPopupExtender>
   
    <asp:Panel ID="panelVerificar" runat="server" BackColor="White" Style="text-align: right" BorderWidth="1px" Width="500px" >
        <div id="popupcontainer" style="width: 500px">
            <table style="width: 100%;">
                <tr>
                    <td colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                <td colspan="3" style="text-align:center">
                <asp:Label ID="lblAnulación" runat="server" Text= "Motivo de Anulación:" style="text-align:left"/>
                    
                   <asp:DropDownList ID="ddlMotivoAnulacion" runat="server" CssClass="textbox" visible="false" 
                                Width="200px" style="text-align:left">
                            </asp:DropDownList>
                            </td>
                            </tr>
                            <tr>
                            <td colspan="3" style="text-align:center">
                        <asp:Label ID="lblMensajeVerificar" runat="server" Text= "Esta Seguro de Aprobar/Anular el comprobante ?" style="text-align:left">
                         
                         
                        </asp:Label>                       
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

    <uc1:mensajeGrabar ID="ctlMensaje" runat="server"/>

</asp:Content>
