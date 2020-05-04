<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - CDATS Apertura :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>

<%@ Register Src="~/General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlTasa.ascx" TagName="ctlTasa" TagPrefix="ctl" %>

<%@ Register src="../../../General/Controles/ctlGiro.ascx" tagname="giro" tagprefix="uc3" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                
                    <table border="0" cellpadding="0" cellspacing="0" width="700px">
                      <tr>
                        <td style="text-align: left; width: 280px" colspan="2">
                            <asp:Label ID="lblError" runat="server" style="text-align: right" Visible="False"  ForeColor="Red"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                            <td style="text-align: left; width: 280px" colspan="2">                             
                                Tipo/Linea de CDAT<br />
                                <asp:DropDownList ID="ddlTipoLinea" runat="server" CssClass="textbox" 
                                    Width="90%" AppendDataBoundItems="True" AutoPostBack="true" 
                                    onselectedindexchanged="ddlTipoLinea_SelectedIndexChanged" />
                            </td>
                            <td style="text-align: left; width: 140px">
                                Modalidad<br />
                                <asp:DropDownList ID="ddlModalidad" runat="server" CssClass="textbox" 
                                    Width="90%" AutoPostBack="True" 
                                    onselectedindexchanged="ddlModalidad_SelectedIndexChanged"/>
                            </td>
                            <td style="text-align: left; width: 140px">
                                Forma Captación<br />
                                <asp:DropDownList ID="ddlFormaCaptacion" runat="server" CssClass="textbox" 
                                    Width="90%" AppendDataBoundItems="True"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 150px;">
                                Número CDAT<br />
                                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Visible="false"/>
                                <asp:TextBox ID="txtNumCDAT" runat="server" CssClass="textbox" Width="90%"/>
                                <asp:Label ID="lblNumDV" runat="server" Text ="Autogenerado" CssClass="textbox" Visible="false"/>
                            </td>
                            <td style="text-align: left; width: 150px">
                               Número Pre-Impreso<br />
                                <asp:TextBox ID="txtNumPreImpreso" runat="server" CssClass="textbox" Width="90%" />                                
                            </td>
                            <td style="text-align: left; width: 200px">
                                Fecha Apertura<br />
                                <uc2:fecha ID="txtFechaApertura" runat="server" CssClass="textbox" />
                            </td>
                            <td style="text-align: left; width: 200px">
                                <asp:Label ID="lbltipocalendario" runat="server" Text="Tipo Calendario" 
                                    Visible="False" />
                                <br />
                                <asp:DropDownList ID="ddlTipoCalendario" runat="server" AutoPostBack="true" CssClass="textbox" 
                                    onselectedindexchanged="ddlTipoCalendario_SelectedIndexChanged" 
                                    Width="90%" Enabled="False" Visible="false" />
                            </td>
                        </tr>
                        
                        <tr>
                            <td style="text-align: left; width: 140px">
                                Valor<br />
                                <uc1:decimales ID="txtValor" runat="server" CssClass="textbox"  />
                            </td>
                            <td style="text-align: left; width: 140px">
                                Moneda<br />
                                <asp:DropDownList ID="ddlTipoMoneda" runat="server" CssClass="textbox" Width="90%"/>
                            </td>
                            <td style="text-align: left; width: 160px">                               
                                <asp:Label ID="lblplazo" runat="server" Text="Plazo" 
                                    Visible="False" />
                                <br />
                                <asp:TextBox ID="txtPlazo" runat="server" AutoPostBack="true" 
                                    CssClass="textbox" ontextchanged="txtPlazo_TextChanged" Width="60%" 
                                    Visible="False" />
                            </td>
                            <td style="text-align: left; width: 160px">   
                                Fecha Vencimiento<br />
                                <uc2:fecha ID="txtfechaVenci" runat="server" CssClass="textbox" 
                                    enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 280px" colspan="2">
                                <asp:DropDownList ID="ddlDestinacion" runat="server" CssClass="textbox"  
                                    Width="90%" Visible="False"/>
                            </td>
                            <td style="text-align: left; width: 160px">
                                Asesor Comercial<br />
                                <asp:DropDownList ID="ddlAsesor" runat="server" CssClass="textbox" Width="90%" 
                                    AppendDataBoundItems="True"/>
                            </td>
                            <td style="text-align: left; width: 160px">
                                Oficina<br />
                                <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" Width="90%" 
                                    AppendDataBoundItems="True" />
                            </td>
                        </tr>
                        
                    </table>
    <%--                </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="Panelgrilla" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="text-align: left;" colspan="4">
                                <strong>Titulares:</strong><br />
                                <asp:Button ID="btnAddRow" runat="server" CssClass="btn8" OnClick="btnAddRow_Click"
                                    OnClientClick="btnAddRow_Click" Text="+ Adicionar Titular" />                                                        
                                <br />
                                <div style="overflow: scroll; width: 730px;">
                                    <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                        RowStyle-CssClass="gridItem" Style="font-size: x-small" OnRowDataBound="gvDetalle_RowDataBound"
                                        GridLines="Horizontal" DataKeyNames="cod_usuario_cdat" OnRowDeleting="gvDetalle_RowDeleting">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                            <asp:TemplateField HeaderText="Identificación">
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <cc1:TextBoxGrid ID="txtIdentificacion" runat="server" AutoPostBack="True" 
                                                                    CommandArgument="<%#Container.DataItemIndex %>" 
                                                                    OnTextChanged="txtIdentificacion_TextChanged" 
                                                                    Text='<%# Bind("identificacion") %>' Width="90px" />
                                                            </td>
                                                            <td>
                                                                <cc1:ButtonGrid ID="btnListadoPersona" runat="server" 
                                                                    CommandArgument="<%#Container.DataItemIndex %>" CssClass="btnListado" 
                                                                    OnClick="btnListadoPersona_Click" Text="..." />
                                                                <uc1:ListadoPersonas ID="ctlListadoPersona" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" Width="170px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Principal">
                                                <ItemTemplate>
                                                    <cc1:CheckBoxGrid ID="chkPrincipal" runat="server" AutoPostBack="true" 
                                                        Checked='<%# Convert.ToBoolean(Eval("principal")) %>' 
                                                        CommandArgument="<%#Container.DataItemIndex %>" 
                                                        OnCheckedChanged="chkPrincipal_CheckedChanged" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombres">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblNombre" runat="server" CssClass="textbox" Enabled="false" 
                                                        Text='<%# Bind("nombres") %>' Width="160px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Apellidos">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblApellidos" runat="server" CssClass="textbox" 
                                                        Enabled="false" Text='<%# Bind("apellidos") %>' Width="160px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ciudad">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblCiudad" runat="server" CssClass="textbox" Enabled="false" 
                                                        Text='<%# Bind("ciudad") %>' Width="120px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dirección">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblDireccion" runat="server" CssClass="textbox" 
                                                        Enabled="false" Text='<%# Bind("direccion") %>' Width="170px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Teléfono">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lbltelefono" runat="server" CssClass="textbox" Enabled="false" 
                                                        Text='<%# Bind("telefono") %>' Width="80px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Conjunción">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblConjuncion" runat="server" Text='<%# Eval("conjuncion")  %>' 
                                                        Visible="false" />
                                                    <cc1:DropDownListGrid ID="ddlConjuncion" runat="server" 
                                                        CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox" 
                                                        Width="120px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cod. Persona">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblcod_persona" runat="server" CssClass="textbox" 
                                                        Enabled="false" Text='<%# Bind("cod_persona") %>' Width="80px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Codigo" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcodigo" runat="server" Text='<%# Bind("cod_usuario_cdat") %>'></asp:Label></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                        <PagerStyle CssClass="gridPager"></PagerStyle>
                                        <RowStyle CssClass="gridItem"></RowStyle>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                      </table>
                         <asp:Accordion ID="AccordionNomina" runat="server" SelectedIndex="-1" FadeTransitions="false" SuppressHeaderPostbacks="true"
                                FramesPerSecond="50" Width="779px" TransitionDuration="200" HeaderCssClass="accordionCabecera"
                                ContentCssClass="accordionContenido" Height="117px" Style="margin-right: 6px; font-size: xx-small;">
                                <Panes>
                                    <asp:AccordionPane ID="AccordionPane4" runat="server" ContentCssClass=""
                                        HeaderCssClass="">
                                        <Header>
                                            <center>DATOS ADICIONALES CDATS</center>
                                        </Header>
                                        <Content>
                                            <table>
                        <tr> 
                            <td style="text-align: left;" colspan="4">
                                <asp:CheckBox ID="cbInteresCuenta" runat="server" Text="Interés por Cuenta" 
                                    oncheckedchanged="cbInteresCuenta_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="panelTasa" runat="server">
                                    <ctl:ctlTasa ID="ctlTasaInteres" runat="server" Width="400px" />
                                </asp:Panel>
                            </td>
                            <td style="text-align: left; width: 170px; vertical-align: top;">
                                Modalidad Int<br />
                                <asp:RadioButtonList ID="rblModalidadInt" Enabled="false" runat="server" 
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Selected="True">Vencido</asp:ListItem>
                                    <asp:ListItem Value="2">Anticipado</asp:ListItem>
                                </asp:RadioButtonList>
                              
                            </td>
                            <td style="text-align: left; width: 150px; vertical-align: top;">
                                
                                </td>
                            <tr>
                                <td style="text-align: left; width: 350px;">
                                    <asp:CheckBox ID="chkLiquidaVencimiento" runat="server" 
                                        oncheckedchanged="chkLiquidaVencimiento_CheckedChanged" Text="Liquida interes al vencimiento" Checked="false"/>
                                    <br />
                                    <asp:Label ID="lbLiquida" runat="server" Text="Periodicidad Intereses"></asp:Label><br />
                                    <asp:DropDownList ID="ddlPeriodicidad" runat="server" AutoPostBack="True" 
                                        CssClass="textbox" 
                                        OnSelectedIndexChanged="ddlPeriodicidad_SelectedIndexChanged" Width="80%" />
                                    <asp:TextBox ID="txtDiasValida" runat="server" CssClass="textbox" 
                                        Visible="False" />
                                </td>
                                <td style="text-align: left; width: 135px;">
                                    <asp:CheckBox ID="chkDesmaterial" runat="server" 
                                        oncheckedchanged="chkDesmaterial_CheckedChanged" Text="Desmaterializado" />
                                    <br />
                                    Fecha Emisión<br />
                                    <uc2:fecha ID="txtFechaEmi" runat="server" />
                                </td>
                                <td style="text-align: left; width: 145px;">
                                    <asp:CheckBox ID="chkCapitalizaInt" runat="server" Text="Capitaliza Interes" />
                                    <uc2:fecha ID="txtfechaInteres" runat="server" />
                                </td>
                                <td style="text-align: left; width: 135px;">
                                    <asp:CheckBox ID="chkCobraReten" runat="server" Text="Cobra Retención" />
                                </td>
                            </tr>
                            <tr>
                            <td style="text-align: left; width: 350px;" > 
                                  
                              <asp:CheckBox ID="chkCuenta" runat="server" Text="Maneja Cuenta de Ahorros" AutoPostBack ="true" OnCheckedChanged="cbSeleccionar_CheckedChanged" />

                              <asp:DropDownList ID="ddlnumAhorros" runat="server" CssClass="textbox" Width="90%"  Visible ="false"  />
                              
                            </td>
              
                            </td>
                        </tr>
                        </tr>
                    </table>
                                            </Content>
                                    </asp:AccordionPane>
                                    <asp:AccordionPane ID="AccordionPane1" runat="server" ContentCssClass=""
                                        HeaderCssClass="">
                                        <Header>
                                            <center>BENEFICIARIOS</center>
                                        </Header>
                                        <Content>
                                             <table  cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                      <td style="text-align: left">

                                                        <asp:CheckBox Text="Desea tener beneficiarios" ID="chkBeneficiario" AutoPostBack="true" OnCheckedChanged="chkBeneficiario_CheckedChanged" runat="server" /><br />
                                                        <br />
                                                        <asp:UpdatePanel ID="upBeneficiarios" Visible="false" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Button ID="btnAddRowBeneficio" AutoPostBack="true" runat="server" CssClass="btn8" TabIndex="49" OnClick="btnAddRowBeneficio_Click" Text="+ Adicionar Detalle" />
                                                                <asp:GridView ID="gvBeneficiarios"
                                                                    runat="server" AllowPaging="True" TabIndex="50" AutoGenerateColumns="false" BackColor="White"
                                                                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" DataKeyNames="idbeneficiario"
                                                                    ForeColor="Black" GridLines="Both" OnRowDataBound="gvBeneficiarios_RowDataBound"
                                                                    OnRowDeleting="gvBeneficiarios_RowDeleting" PageSize="10" ShowFooter="True" Style="font-size: xx-small"
                                                                    Width="80%">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                                                        <asp:TemplateField HeaderText="Identificación" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                                    Text='<%# Bind("identificacion_ben") %>' Width="100px"> </asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Nombres y Apellidos" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                                    Text='<%# Bind("nombre_ben") %>' Width="260px"> </asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="260px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Fecha Nacimiento" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <uc1:fecha OneventoCambiar="FechaNacimiento_Changed" ID="txtFechaNacimientoBen" runat="server" CssClass="textbox" style="font-size: xx-small; text-align: left"
                                                                                    Text='<%# Eval("fecha_nacimiento_ben", "{0:" + FormatoFecha() + "}") %>' />
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Edad" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtEdadBen" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                                    Text='<%# Bind("edad") %>' Width="100px"> </asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Sexo" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList runat="server" ID="ddlsexo">
                                                                                    <asp:ListItem Value="F" Text="Femenino" />
                                                                                    <asp:ListItem Value="M" Text="Masculino" />
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Parentesco" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblidbeneficiario" runat="server" Text='<%# Bind("idbeneficiario") %>'
                                                                                    Visible="false"></asp:Label><asp:Label ID="lblParentezco" runat="server" Text='<%# Bind("parentesco") %>'
                                                                                        Visible="false"></asp:Label><asp:Label ID="lblSexo" runat="server" Text='<%# Bind("sexo") %>'
                                                                                            Visible="false"></asp:Label><cc2:DropDownListGrid ID="ddlParentezco" runat="server"
                                                                                                AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
                                                                                                Style="font-size: xx-small; text-align: left" Width="120px"></cc2:DropDownListGrid>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="% Ben." ItemStyle-HorizontalAlign="Right">
                                                                            <ItemTemplate>
                                                                                <uc1:decimalesGridRow ID="txtPorcentaje" runat="server" AutoPostBack_="True" CssClass="textbox"
                                                                                    Enabled="True" Habilitado="True" Text='<%# Eval("porcentaje_ben") %>' Width_="80" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
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
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                       </Content>
                                    </asp:AccordionPane>

                                
                                     </Panes>
                            </asp:Accordion>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="ddlModalidad" />
                </Triggers>
            </asp:UpdatePanel>                 
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            Apertura del CDAT
                            <asp:Label ID="lblMsj" runat="server"></asp:Label>
                            Correctamente 
                            <br />
                            Número de CDAT&nbsp;<asp:Label ID="lblgenerado" runat="server"></asp:Label>
                            <br />
                            <asp:Button ID="btnImprime" runat="server" Text="Desea Imprimir ?" 
                                CssClass="btn8" onclick="btnImprime_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwMostrar" runat="server">
            <br />
            <asp:Button ID="btnRegresar" runat="server" CssClass="btn8" Text="Regresar a la administración de CDATS" 
                onclick="btnRegresarAdministracion_Click" ValidationGroup="vgGuardar" Width="350px" Height="30px" />
            <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx" height="700px"
                runat="server" style="border-style: groove; float: left;"></iframe>
        </asp:View>
    </asp:MultiView>

    <asp:MultiView ID="mvReporte" runat="server" ActiveViewIndex="0">
    <asp:View ID="vwReporte" runat="server">
            <asp:Button ID="btnDatos" runat="server" CssClass="btn8" 
                onclick="btnDatos_Click" Text="Visualizar Datos" />
            <br /><br />
            <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
                <tr>
                    <td>
                        <hr width="100%" />
                        &nbsp;
                        </td>
                </tr>
                <tr>
                    <td>                        
                        <rsweb:ReportViewer ID="rvReporte" runat="server" Font-Names="Verdana" 
                            Font-Size="8pt" InteractiveDeviceInfos="(Colección)" 
                            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Width="100%">
                            <LocalReport ReportPath="Page\CDATS\Apertura\rptApertura.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>                    
                    </td>
                </tr>
            </table>
        </asp:View>
        </asp:MultiView>




    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
