<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>  

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <table style="width: 90%; text-align: center" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="text-align: left; width: 14%">
                        Código<br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="70%"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 25%" >
                        Identificación<br />
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="90%" Enabled="false" OnTextChanged="txtIdentificacion_TextChanged"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNit" runat="server" ControlToValidate="txtIdentificacion"
                            Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                            ValidationGroup="vgGuardar" Style="font-size: xx-small" />
                        <asp:TextBox ID="txtCodEmpresa" runat="server" CssClass="textbox" Visible="False"></asp:TextBox>
                    </td>
                    <td style="text-align: left;" colspan="2" >
                        Nombre<br />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="90%"></asp:TextBox>
                        <asp:Button ID="btnConsultaPersonas" CssClass="btn8" runat="server" Text="..." Height="26px" OnClick="btnConsultaPersonas_Click" />
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre"
                            Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                            ValidationGroup="vgGuardar" Style="font-size: xx-small" />
                    </td>
                    <td style="text-align: left; width: 14%">
                        Código Recaudo<br />
                        <asp:TextBox ID="txtCodigoRecaudoEstructura" runat="server" CssClass="textbox" Width="90%"></asp:TextBox>
                    </td>
                </tr>
                <tr> 
                    <td style="text-align: left;">
                        Numero Planilla<br />
                        <asp:TextBox ID="txtNumeroPlanilla" runat="server" CssClass="textbox" Width="70%"></asp:TextBox>
                    </td>                   
                    <td style="text-align: left; width:300px;" >
                        Dirección<br />
                        <asp:TextBox ID="txtDirec" runat="server" CssClass="textbox" Width="80%"></asp:TextBox>
                    </td>
                    <td style="text-align: left">
                        Teléfono<br />
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" Width="70%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTelefono"
                            Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                            ValidationGroup="vgGuardar" Style="font-size: xx-small" />
                        <asp:CompareValidator ID="CompareValidator1" runat="server" Display="Dynamic" ControlToValidate="txtTelefono"
                            SetFocusOnError="True" ErrorMessage="Ingrese solo Numeros" ForeColor="Red" Type="Integer"
                            Style="font-size: xx-small" Operator="DataTypeCheck" ValidationGroup="vgGuardar" />
                    </td>
                    <td style="text-align: left;">
                        Responsable<br />
                        <asp:TextBox ID="txtResponsable" runat="server" CssClass="textbox" Width="70%"></asp:TextBox>
                    </td>
                    <td style="text-align: left;">
                        Cod.Cuenta<br />
                        <asp:TextBox ID="txtCodCuenta" runat="server" CssClass="textbox" Width="70%"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width: 95%; text-align: center" cellspacing="0" cellpadding="0">
                <tr>                        
                    <td style="text-align: left; width: 12%;">
                        Fecha Convenio<br />
                        <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" MaxLength="1" Width="100px" />
                    </td>                  
                    <td style="text-align: left;">
                        Tipo de Recaudo<br />
                        <asp:RadioButtonList ID="rbTipoRecaudo" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0">Bancario</asp:ListItem>
                            <asp:ListItem Value="1">Pagadurias</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="text-align: left;">
                        Tipos de Novedad<br />
                        <asp:RadioButtonList ID="rblTipo_nove" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0">Todas</asp:ListItem>
                            <asp:ListItem Value="1">Nuevas</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="text-align: left">
                        Estado<br />
                        <asp:RadioButtonList ID="rblEstado" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0">Inactiva</asp:ListItem>
                            <asp:ListItem Value="1">Activa</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="text-align: left">
                        <br />
                        <asp:CheckBox ID="chkAplicarXoficina" runat="server" Text="Aplicar por Oficina" Style="font-size: x-small" />
                    </td>
                    <td style="text-align: left">
                        <br />
                        <asp:CheckBox ID="chkDebitoAutomatico" runat="server" Style="font-size: x-small" Text="Aplica Deb. Automático" />
                        <br />
                        <asp:CheckBox ID="chkDebitoAutomaticoSem" runat="server" Style="font-size: x-small" Text="Aplica Deb. Automático Semestral" />
                    </td>
                </tr>
                <tr>                    
                    <td style="text-align: left; width: 14%;">
                        Días de Novedad<br />
                        <asp:TextBox ID="txtDias_nove" runat="server" CssClass="textbox" Width="60%" MaxLength="7"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="fte5" runat="server" TargetControlID="txtDias_nove"
                            FilterType="Custom, Numbers" ValidChars="+-=/*()." />
                    </td>                                              
                    <td style="text-align: left;">
                        Aplicar Mayores Valores a<br />
                        <asp:DropDownList ID="ddlMayoresValores" runat="server" CssClass="textbox" Width="90%">
                            <asp:ListItem Value="" Text=""/>
                            <asp:ListItem Value="1" Text="Devoluciones"/>
                            <asp:ListItem Value="2" Text="Aportes"/>
                        </asp:DropDownList>
                    </td>    
                    <td style="text-align: left;">
                        Forma de Cobro<br />
                        <asp:DropDownList ID="ddlFormaCobro" runat="server" CssClass="textbox" Width="90%">
                            <asp:ListItem Value="" Text=""/>
                            <asp:ListItem Value="3" Text="Una Cuota Aportes"/>
                            <asp:ListItem Value="2" Text="Todo lo Adeudado"/>
                            <asp:ListItem Value="1" Text="Una Cuota"/>
                        </asp:DropDownList>
                    </td>   
                    <td style="text-align: left;">
                        <asp:CheckBox ID="cbAplicarRefinancidos" runat="server" Text="Aplicar a Créditos Refinanciados" style="font-size: x-small"/>
                        <br />
                        <asp:CheckBox ID="chkManejaAtributos" runat="server" Text="Maneja Atributos" style="font-size: x-small"/>
                         <br />
                        <asp:CheckBox ID="chkDescuentosRetirados" runat="server" Text="Descuenta Retirados" style="font-size: x-small"/>
                        <br />
                        <asp:CheckBox ID="chkMora" runat="server" Text="Aplicar por Mora" style="font-size: x-small"/>                  
                    </td>     
                    <td style="text-align: left;" colspan="2">                        
                        <asp:CheckBox ID="cbAplicar" runat="server" Text="Aplicar Según Novedades" style="font-size: x-small"/>
                        <br />
                        <asp:CheckBox ID="cbDeshabilitarDesc" runat="server" Text="Deshabilitar Desc. a Inhabiles" style="font-size: x-small"/>
                        <br />
                        <asp:CheckBox ID="chkCondicionInicial" runat="server" Text="Mantener Condiciones Iniciales" style="font-size: x-small"/>
                        <br />
                        <asp:CheckBox ID="chkAporteVacaciones" runat="server" Text="Cobrar una cuota de aporte en vacaciones" style="font-size: x-small"/>
                        <br />
                        <asp:CheckBox ID="cbProductosInactivos" runat="server" style="font-size: x-small" Text="Aplica Productos Inactivos" />
                    </td>                   
                    
                    <td style="text-align: left;">
                         
                        <br />                        
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: left; width: 100%">
                        <hr style="width: 100%" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="6">
                        <strong>Programación de Envio de Descuentos</strong>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: left; width: 100%">
                        <div style="overflow: scroll; width: 100%;">
                            <div style="width: 650px;">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnProgramacion" runat="server" CssClass="btn8" OnClick="btnProgramacion_Click"
                                            OnClientClick="btnProgramacion_Click" Text="+ Adicionar Detalle" />
                                        <asp:GridView ID="gvProgramacion" runat="server" PageSize="20" ShowHeaderWhenEmpty="True"
                                            AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small;
                                            margin-bottom: 0px;" OnRowDataBound="gvProgramacion_RowDataBound" OnSelectedIndexChanged="gvProgramacion_SelectedIndexChanged"
                                            OnRowDeleting="gvProgramacion_RowDeleting" DataKeyNames="idprogramacion">
                                            <Columns>
                                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:CommandField>
                                                <asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="idprogramacion" runat="server" Text='<%# Bind("idprogramacion") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tipos de Descuento" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltipo" runat="server" Text='<%# Bind("tipo_lista") %>' Visible="false">
                                                        </asp:Label>
                                                        <cc1:DropDownListGrid ID="ddlTipo" runat="server" CommandArgument="<%#Container.DataItemIndex %>"
                                                            CssClass="textbox" Width="200px">
                                                        </cc1:DropDownListGrid>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Principal">
                                                    <ItemTemplate>
                                                        <cc1:CheckBoxGrid ID="chkPrincipal" runat="server" Checked='<%#Convert.ToBoolean(Eval("principal"))%>' /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Periodicidad">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPeriodicidad" runat="server" Text='<%# Bind("cod_periodicidad") %>'
                                                            Visible="false"></asp:Label>
                                                        <cc1:DropDownListGrid ID="ddlPeriodicidad" runat="server" CommandArgument="<%#Container.DataItemIndex %>"
                                                            CssClass="textbox" Width="200px">
                                                        </cc1:DropDownListGrid>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fecha de Inicio">
                                                    <ItemTemplate>
                                                        <ucFecha:fecha ID="txtfechainicio" runat="server" CssClass="textbox" Enabled="True"
                                                            Habilitado="True" style="font-size: xx-small; text-align: left" Text='<%# Eval("fecha_inicio", "{0:d}") %>'
                                                            TipoLetra="XX-Small" Width_="140px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="center" />
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
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: left; width: 100%">
                        <hr style="width: 100%" />
                        <br />
                    </td>
                </tr>
            </table>
            <table style="width: 800px; text-align: center" cellspacing="0" cellpadding="0">
                <tr>
                    <td colspan="3" style="text-align: left">
                        <table>
                            <tr>
                                <td style="width: 300px; text-align: left">
                                    <strong>Estructuras para Manejo de Novedades</strong>
                                </td>
                                <td style="width: 300px; text-align: left">
                                    <strong>Empresas Excluyentes</strong>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 300px; text-align: left" valign="top">
                                    <div style="overflow: scroll; width: 100%;">
                                        <asp:UpdatePanel ID="upEstructuras" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnEstructura" runat="server" CssClass="btn8" OnClick="btnEstructura_Click"
                                                    OnClientClick="btnEstructura_Click" Text="+ Adicionar Detalle" />
                                                <asp:GridView ID="gvEstructura" runat="server" PageSize="20" ShowHeaderWhenEmpty="True"
                                                    AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small;
                                                    margin-bottom: 0px;" OnRowDataBound="gvEstructura_RowDataBound" OnSelectedIndexChanged="gvEstructura_SelectedIndexChanged"
                                                    OnRowDeleting="gvEstructura_RowDeleting" DataKeyNames="CODEMPARCHIVO">
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                                            <ItemStyle HorizontalAlign="left" />
                                                        </asp:CommandField>
                                                        <asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcodemparchivo" runat="server" Text='<%# Bind("CODEMPARCHIVO") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Estructura">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcod_estructura_carga" runat="server" Text='<%# Bind("cod_estructura_carga") %>'
                                                                    Visible="false"></asp:Label>
                                                                <cc1:DropDownListGrid ID="ddlEstructura" runat="server" AutoPostBack="True" CommandArgument="<%#Container.DataItemIndex %>"
                                                                    CssClass="textbox" Width="300px">
                                                                </cc1:DropDownListGrid>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="center" />
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
                                    <asp:Label ID="Label1" runat="server" Visible="False" />
                                </td>
                                <td style="width: 300px; text-align: left"> 
                                    <div style="overflow: scroll; width: 100%; height: 200px">
                                        <asp:GridView ID="gvEmpresaExcluyente" runat="server" PageSize="20"
                                            ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small"
                                            Style="font-size: small; margin-bottom: 0px;">
                                            <Columns>
                                                <asp:TemplateField HeaderText="idEmpresa" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblidempexcluyente" runat="server" Text='<%# Bind("idempexcluyente") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="codEmpresa" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcodempresa" runat="server" Text='<%# Bind("cod_empresa") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Empresa" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpresa" runat="server" Text='<%# Bind("descripcion") %>' Width="200px"/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Seleccionar">
                                                    <ItemTemplate>
                                                        <cc1:CheckBoxGrid ID="chkSeleccione" runat="server" Checked='<%#Convert.ToBoolean(Eval("seleccionar"))%>' Width="60px"/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="gridHeader" />
                                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                            <RowStyle CssClass="gridItem" />
                                            <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table style="width: 600px; text-align: center" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="text-align: left" colspan="2">
                        <strong>Código de conceptos de Descuento y Prioridades</strong>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left; width: 100%">
                        <div style="overflow: scroll; width: 100%;">
                            <div style="width: 650px;">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnConcepto" runat="server" CssClass="btn8" OnClick="btnConcepto_Click"
                                            OnClientClick="btnConcepto_Click" Text="+ Adicionar Detalle" />
                                        <asp:GridView ID="gvConcepto" runat="server" PageSize="20" ShowHeaderWhenEmpty="True"
                                            AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small;
                                            margin-bottom: 0px;" OnRowDataBound="gvConcepto_RowDataBound" OnSelectedIndexChanged="gvConcepto_SelectedIndexChanged"
                                            OnRowDeleting="gvConcepto_RowDeleting" DataKeyNames="idempconcepto">
                                            <Columns>
                                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:CommandField>
                                                <asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="idempconcepto" runat="server" Text='<%# Bind("idempconcepto") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tipo de Producto">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltipoProd" runat="server" Text='<%# Bind("tipo_producto") %>' Visible="false"></asp:Label>
                                                        <cc1:DropDownListGrid ID="ddlTipoProd" runat="server" AutoPostBack="True" CommandArgument="<%#Container.DataItemIndex %>"
                                                            CssClass="textbox" OnSelectedIndexChanged="ddlTipoProd_SelectedIndexChanged"
                                                            Width="150px">
                                                        </cc1:DropDownListGrid>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Linea de Producto">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLineaProd" runat="server" Text='<%# Bind("cod_linea") %>' Visible="false"></asp:Label>
                                                        <cc1:DropDownListGrid ID="ddlLineaProd" runat="server" CommandArgument="<%#Container.DataItemIndex %>"
                                                            CssClass="textbox" Width="250px">
                                                        </cc1:DropDownListGrid>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Concepto">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtConcepto" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                                            text-align: left" Text='<%# Bind("cod_concepto") %>' Width="100px"></asp:TextBox></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Prioridad">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPrioridad" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                                            text-align: left" Text='<%# Bind("prioridad") %>' Width="100px"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="ftbePrioridad" runat="server" Enabled="True"
                                                            FilterType="Numbers, Custom" TargetControlID="txtPrioridad" ValidChars="-+=" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="center" />
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
                        <asp:Label ID="lblTotalReg1" runat="server" Visible="False" />
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
                        <td style="text-align: center; font-size: large; color:Red">
                            La empresa de Recaudo fue
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                            &nbsp;correctamente</td>
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
     
    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>
    <uc1:listadopersonas ID="ctlBusquedaPersonas" runat="server" />
</asp:Content>