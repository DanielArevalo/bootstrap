<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Tercero :." %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="Fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register src="../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlFormaPago.ascx" TagName="Forma" TagPrefix="uc3" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlFormatoDocum.ascx" TagName="FormatoDocu" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvDatos" runat="server" ActiveViewIndex="0">       
        <asp:View ID="vwEscoger" runat="server"  EnableTheming="True">
            <asp:Panel id="PanelTipoComprobante" runat="server">
                <table cellpadding="5" cellspacing="0" style="width: 100%">
                    <tr style="height: 20px" colspan="2">
                        <td style="text-align: center; color: #FFFFFF; background-color: #0066FF; height: 20px">
                            TIPO DE PERSONA
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr style="text-align: center" >
                        <td>
                            Escoja el tipo de persona a crear
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            <asp:RadioButton ID="rbJuridica" runat="server" Text="Juridica" 
                                AutoPostBack="True" oncheckedchanged="rbJuridica_CheckedChanged" />
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            <asp:RadioButton ID="rbNatural" runat="server" Text="Natural" 
                                AutoPostBack="True" oncheckedchanged="rbNatural_CheckedChanged"  />
                         </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            <asp:ImageButton ID="imgAceptar" runat="server" 
                                ImageUrl="~/Images/btnAceptar.jpg" onclick="imgAceptar_Click" />
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwDetalleCliente" runat="server"  EnableTheming="True">
            <asp:Panel ID="pConsulta" runat="server" >
                <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" >
                    <tr>
                        <td class="logo" style="text-align:left" colspan="4">
                            &nbsp;</td>
                        <td class="tdD">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="logo" style="text-align:left">
                            Código<br/>
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="140px" />
                            &nbsp
                        </td>
                        <td style="text-align:left">
                            Nit<br/>
                            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" />                    
                            <asp:Label ID="lblRayita" runat="server" Text="-"></asp:Label>
                            <asp:TextBox ID="txtDigitoVerificacion" runat="server" CssClass="textbox" 
                                Width="30px" />                            
                            <asp:FilteredTextBoxExtender ID="ftbeIdentificacion" runat="server" 
                                Enabled="True" FilterType="Numbers" TargetControlID="txtIdentificacion">
                            </asp:FilteredTextBoxExtender>
                        </td>
                        <td class="logo" colspan="2" style="text-align:left">
                            &nbsp;                           
                        </td>
                        <td class="tdD">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="tdI" colspan="4" style="text-align:left">
                            Razón Social<br />
                            <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="textbox" MaxLength="128" style="text-transform :uppercase"
                                Width="574px" />                            
                        </td>
                        <td class="tdI">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="tdI" colspan="4" style="text-align:left">
                            Sigla<br />
                            <asp:TextBox ID="txtSigla" runat="server" CssClass="textbox" MaxLength="128" style="text-transform :uppercase"
                                Width="574px" />                            
                        </td>
                        <td class="tdI">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="tdI" colspan="4" style="text-align:left">
                            Dirección<br />
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="textbox" MaxLength="128" style="text-transform :uppercase"
                                Width="574px" />                            
                        </td>
                        <td class="tdI">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="logo" colspan="2" style="text-align:left">
                            Ciudad<br/>
                            <asp:DropDownList ID="ddlCiudad" runat="server" Width="340px" 
                                CssClass="dropdown" Height="25px" AppendDataBoundItems="True">
                                <asp:ListItem Value="">Seleccione un Item</asp:ListItem>
                            </asp:DropDownList>                            
                        </td>
                        <td style="text-align:left">
                            Telefóno<br/>
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" MaxLength="12"/>
                            <asp:FilteredTextBoxExtender ID="ftb12" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                TargetControlID="txtTelefono" ValidChars="-()" />                            
                        </td>
                        <td style="text-align:left">
                            &nbsp;</td>
                        <td class="tdD">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="logo" colspan="2" style="text-align:left">
                            E-Mail<br/>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Width="340px" />
                            
                            <asp:RegularExpressionValidator ID="revTxtEmail" runat="server" 
                                ControlToValidate="txtEmail" ErrorMessage="E-Mail no valido!" ForeColor="Red" 
                                style="font-size: x-small" 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                ValidationGroup="vgGuardar" Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
                        <td style="text-align:left">
                            Fecha Creación/Expedic.<br/>                   
                           <ucFecha:Fecha ID="txtFecha" runat="server" />
                        </td>
                        <td style="text-align:left">
                            &nbsp;</td>
                        <td class="tdD">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="tdI" colspan="4" style="text-align:left">
                            Actividad<br/>
                            <asp:DropDownList ID="ddlActividad" runat="server" Width="574px" 
                                CssClass="dropdown" Height="25px" AppendDataBoundItems="True">
                                <asp:ListItem Value="0">Seleccione un Item</asp:ListItem>
                            </asp:DropDownList><br />
                            <asp:RequiredFieldValidator ID="rfvActividad" runat="server" ErrorMessage="Seleccione Actividad" ControlToValidate="ddlActividad" 
                                Display="Dynamic" ValidationGroup="vgGuardar" InitialValue="Seleccione un Item" SetFocusOnError="True" ForeColor="#CC3300" Font-Size="XX-Small"></asp:RequiredFieldValidator>                    
                        </td>
                        <td class="tdI">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="tdI" colspan="4" style="text-align:left">
                            Regimen<br/>
                            <asp:DropDownList ID="ddlRegimen" runat="server" Width="574px" 
                                CssClass="dropdown" Height="25px">
                                <asp:ListItem Value="">Seleccione un Item</asp:ListItem>
                                <asp:ListItem Value="C">COMUN</asp:ListItem>
                                <asp:ListItem Value="S">SIMPLIFICADO</asp:ListItem>
                                <asp:ListItem Value="E">ESPECIAL</asp:ListItem>
                                <asp:ListItem Value="GCA">GRAN CONTRIBUYENTE AUTORETENEDOR</asp:ListItem>
                                <asp:ListItem Value="GCNA">GRAN CONTRIBUYENTE NO AUTORETENEDOR</asp:ListItem>
                            </asp:DropDownList>                            
                        </td>
                        <td class="tdI">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align:left">
                         Tipo de Creación<br />
                            <asp:DropDownList id="ddlTipoActoCrea" runat="server" CssClass="textbox" Width="90%"/>
                                                    
                        </td>
                        <td style="text-align: left" colspan="4">
                            <table>
                                <tr>
                                    <td style="width:180px">
                                        Num Acto de Creación<br />
                                        <asp:TextBox ID="txtNumActoCrea" runat="server" CssClass="textbox" Width="90%" />
                                    </td>
                                    <td style="width:180px">
                                        Celular<br />
                                        <asp:TextBox ID="txtcelular" runat="server" CssClass="textbox" />
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                            FilterType="Numbers, Custom" TargetControlID="txtcelular" ValidChars="-()" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                    <td colspan="5">
                    <br />
                    </td>
                    </tr>
                        <tr>
                            <td colspan="5" style="text-align: center; color: #FFFFFF; background-color: #0066FF;
                            height: 20px">
                                Información Adicional
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <%--OnRowDataBound="gvInfoAdicional_RowDataBound"--%>
                                <asp:GridView ID="gvInfoAdicional" runat="server" AllowPaging="True" OnRowDataBound="gvInfoAdicional_RowDataBound"
                                    AutoGenerateColumns="false" BackColor="White" BorderColor="#DEDFDE" 
                                    BorderStyle="None" BorderWidth="0px" CellPadding="0" DataKeyNames="" 
                                    ForeColor="Black" GridLines="Both" PageSize="10" ShowFooter="False" 
                                    ShowHeader="False" ShowHeaderWhenEmpty="False" Width="80%">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblidinfadicional" runat="server" 
                                                    Text='<%# Bind("idinfadicional") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="codigo" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcod_infadicional" runat="server" 
                                                    Text='<%# Bind("cod_infadicional") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripcion" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldescripcion" runat="server" Text='<%# Bind("descripcion") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" Width="120px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Control" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblopcionaActivar" runat="server" Text='<%# Bind("tipo") %>' 
                                                    Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtCadena" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                                text-align: left" Text='<%# Bind("valor") %>' Visible="false" Width="280px"></asp:TextBox>
                                                <asp:TextBox ID="txtNumero" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                                text-align: left" Text='<%# Bind("valor") %>' Visible="false" Width="150px">
                                            </asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="ftb1" runat="server" Enabled="True" 
                                                    FilterType="Numbers, Custom" TargetControlID="txtNumero" ValidChars="" />
                                                <uc1:fecha ID="txtctlfecha" runat="server" cssclass="textbox" enabled="True" 
                                                    habilitado="True" style="font-size: xx-small; text-align: left" 
                                                    text='<%# Eval("valor", "{0:d}") %>' tipoletra="xx-Small" visible="false" />
                                                <asp:Label ID="lblValorDropdown" runat="server" Text='<%# Bind("valor") %>' 
                                                    Visible="false"></asp:Label>
                                                <asp:Label ID="lblDropdown" runat="server" Text='<%# Bind("items_lista") %>' 
                                                    Visible="false"></asp:Label>
                                                <cc1:DropDownListGrid ID="ddlDropdown" runat="server" 
                                                    appenddatabounditems="True" commandargument="<%#Container.DataItemIndex %>" 
                                                    cssclass="textbox" style="font-size: xx-small; text-align: left" 
                                                    visible="false" width="160px">
                                                </cc1:DropDownListGrid>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="gridHeader" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <RowStyle CssClass="gridItem" />
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                    <SortedAscendingHeaderStyle BackColor="#848384" />
                                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                    <SortedDescendingHeaderStyle BackColor="#575357" />
                                </asp:GridView>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" style="text-align: center; color: #FFFFFF; background-color: #0066FF;
                            height: 20px">
                                Afiliación
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <table>
                                    <tr>
                                        <td style="text-align: left; width: 160px">
                                            Fecha de Afiliación<br>
                                            <asp:TextBox ID="txtcodAfiliacion" runat="server" Width="100px" CssClass="textbox"
                                                Style="text-align: right" Visible="false" /><uc1:fecha ID="txtFechaAfili" runat="server"
                                                    Enabled="True" style="width: 140px" />
                                        </td>
                                        <td style="text-align: left; width: 180px">
                                            Estado<br />
                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlEstadoAfi" runat="server" Width="160px" CssClass="textbox"
                                                        AutoPostBack="true" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlEstadoAfi_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="text-align: left; width: 160px">
                                            <asp:UpdatePanel ID="updp10" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    Fecha de Rétiro<br />
                                                    <asp:Panel ID="panelFecha" runat="server">
                                                        <uc1:fecha ID="txtFechaRetiro" runat="server" Enabled="True" style="width: 140px" />
                                                    </asp:Panel>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlEstadoAfi" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>
                                            Forma de Pago<br/>
                                            <asp:DropDownList ID="ddlFormaPago" runat="server" Width="95%" 
                                            CssClass="textbox" AutoPostBack="True" 
                                            onselectedindexchanged="ddlFormaPago_SelectedIndexChanged">
                                            <asp:ListItem Value="1">Caja</asp:ListItem>
                                            <asp:ListItem Value="2">Nomina</asp:ListItem>
                                        </asp:DropDownList>                                           
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEmpresa" runat="server" Text="Empresa" /><br/>
                                            <asp:DropDownList ID="ddlEmpresa" runat="server" Width="180px" CssClass="textbox">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;">
                                            Valor<br />
                                            <uc2:decimales ID="txtValorAfili" runat="server" style="text-align: right;" />
                                        </td>
                                        <td style="text-align: left;">
                                            Fecha de 1er Pago<br>
                                            <uc1:fecha ID="txtFecha1Pago" runat="server" style="width: 140px" />
                                            
                                        </td>
                                        <td style="text-align: left;">
                                            Nro Cuotas<br>
                                            <asp:TextBox ID="txtCuotasAfili" runat="server" Width="100px" CssClass="textbox"
                                                Style="text-align: right" /><asp:FilteredTextBoxExtender ID="txtValor_FilteredTextBoxExtender"
                                                    runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtCuotasAfili"
                                                    ValidChars="" />
                                        </td>
                                        <td style="text-align: left;">
                                           Periodicidad<br>
                                            <asp:DropDownList ID="ddlPeriodicidad" runat="server" Width="180px" CssClass="textbox">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left;">
                                            &nbsp;  
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <asp:Panel ID="panelFinal" runat="server" Visible="false" Height="600px">
        <div style="text-align: left">
            <asp:Button ID="btnVerData" runat="server" CssClass="btn8" Text="Cerrar Informe"
                OnClick="btnVerData_Click" Width="280px" Height="30px" />
        </div>
        <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx"
            height="100%" runat="server" style="border-style: groove; float: left;"></iframe>
    </asp:Panel>

    <uc4:FormatoDocu ID="ctlFormatos" runat="server" />
    <%--<script type='text/javascript'>
        function Forzar() {
            __doPostBack('', '');
        }
    </script>--%>
</asp:Content>