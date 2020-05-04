<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - CDATS Renovacion :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas"TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlGiro.ascx" TagName="giro" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlTasa.ascx" TagName="ctlTasa" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlTasa.ascx" TagName="ctlTasaRenova" TagPrefix="ctl1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
<%--         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>--%>
            <table border="0" cellpadding="0" cellspacing="0" width="700px">
                <tr>
                    <td style="text-align: left; width: 200px">
                        Fecha<br />
                        <uc2:fecha ID="txtFecha" runat="server" CssClass="textbox" Enabled="true" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 150px;">
                        Número CDAT<br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Visible="false" Enabled="false" />
                        <asp:TextBox ID="txtNumCDAT" runat="server" CssClass="textbox" Width="90%" Enabled="false" />
                        <asp:Label ID="lblNumDV" runat="server" Text="Autogenerado" CssClass="textbox" Visible="false"
                            Enabled="false" />
                    </td>
                    <td style="text-align: left; width: 200px">
                        Fecha Apertura<br />
                        <uc2:fecha ID="txtFechaApertura" runat="server" CssClass="textbox" Enabled="false" />
                    </td>
                    <td style="text-align: left; width: 280px" colspan="2">
                        Tipo/Linea de CDAT<br />
                        <asp:DropDownList ID="ddlTipoLinea" runat="server" CssClass="textbox" Enabled="false"
                            Width="90%" AppendDataBoundItems="True" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 160px">
                        Oficina<br />
                        <asp:DropDownList ID="ddlOficina" runat="server" Enabled="false" CssClass="textbox"
                            Width="90%" AppendDataBoundItems="True" />
                    </td>
                    <td style="text-align: left; width: 140px">
                        Valor<br />
                        <uc1:decimales ID="txtValor" runat="server" CssClass="textbox" Enabled="false" />
                    </td>
                    <td style="text-align: left; width: 140px">
                        Moneda<br />
                        <asp:DropDownList ID="ddlTipoMoneda" Enabled="false" runat="server" CssClass="textbox"
                            Width="90%" />
                    </td>
                    <td style="width: 180px; text-align: left">
                        Plazo<br />
                        <asp:TextBox ID="txtPlazo" runat="server" Enabled="false" CssClass="textbox" Width="100px" />dias
                    </td>
                    <td style="text-align: left; width: 160px">
                        Tipo Calendario<br />
                        <asp:DropDownList ID="ddlTipoCalendario" Enabled="false" runat="server" CssClass="textbox"
                            Width="90%" />
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="text-align: left;" colspan="4">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="text-align: left; width: 710px;" colspan="4">
                                    <strong>Titulares:</strong><br />
                                    <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                        PageSize="3" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                        RowStyle-CssClass="gridItem" Style="font-size: x-small; margin-right: 0px;" OnRowDataBound="gvDetalle_RowDataBound"
                                        GridLines="Horizontal" DataKeyNames="cod_usuario_cdat" OnRowDeleting="gvDetalle_RowDeleting"
                                        Width="710px">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                            <asp:TemplateField HeaderText="Codigo" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcodigo" runat="server" Text='<%# Bind("cod_usuario_cdat") %>'></asp:Label></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Codigo">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblcod_persona" runat="server" Text='<%# Bind("cod_persona") %>'
                                                        CssClass="textbox" Width="80px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Principal">
                                                <ItemTemplate>
                                                    <cc1:CheckBoxGrid ID="chkPrincipal" runat="server" AutoPostBack="true" Checked='<%# Convert.ToBoolean(Eval("principal")) %>'
                                                        CommandArgument="<%#Container.DataItemIndex %>" OnCheckedChanged="chkPrincipal_CheckedChanged" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Identificación">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtIdentificacion" runat="server" Text='<%# Bind("identificacion") %>'
                                                        CommandArgument='<%#Container.DataItemIndex %>' Width="90px" AutoPostBack="True"
                                                        OnTextChanged="txtIdentificacion_TextChanged" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombres">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblNombre" runat="server" Text='<%# Bind("nombres") %>' CssClass="textbox"
                                                        Width="120px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Apellidos">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblApellidos" runat="server" Text='<%# Bind("apellidos") %>' CssClass="textbox"
                                                        Width="120px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ciudad">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblCiudad" runat="server" Text='<%# Bind("ciudad") %>' CssClass="textbox"
                                                        Width="120px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dirección">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblDireccion" runat="server" Text='<%# Bind("direccion") %>' CssClass="textbox"
                                                        Width="170px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Teléfono">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lbltelefono" runat="server" Text='<%# Bind("telefono") %>' CssClass="textbox"
                                                        Width="80px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Conjunción" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblConjuncion" runat="server" Text='<%# Eval("conjuncion")  %>' Visible="false" />
                                                    <cc1:DropDownListGrid ID="ddlConjuncion" runat="server" CssClass="textbox" CommandArgument='<%#Container.DataItemIndex %>'
                                                        Width="70px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                        <PagerStyle CssClass="gridPager"></PagerStyle>
                                        <RowStyle CssClass="gridItem"></RowStyle>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="text-align: left; vertical-align: top; width: 535px; height: 121px;">
                        <asp:Panel ID="paneltasaActual" runat="server">
                            <ctl:ctlTasa ID="ctlTasa" runat="server" Width="400px" />
                        </asp:Panel>
                    </td>
                    <td style="text-align: left; vertical-align: top; width: 535px; height: 121px;">
                        Modalidad Int<br />
                        <asp:RadioButtonList ID="rblModalidadInt" runat="server" RepeatDirection="Horizontal"
                            Enabled="false">
                            <asp:ListItem Value="1" Enabled="false">Vencido</asp:ListItem>
                            <asp:ListItem Value="2" Enabled="false">Anticipado</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="text-align: left; vertical-align: top; height: 121px;">
                        <asp:DropDownList ID="ddlPeriodicidad" runat="server" Visible="False" CssClass="textbox"
                            Width="120px" Enabled="false" AutoPostBack="True" />
                        <asp:TextBox ID="txtDiasValida" runat="server" CssClass="textbox" Visible="False" />
                    </td>
                </tr>
            </table>
 
            <table>
                <tr>
                    <td style="text-align: left; background-color: #3399FF;" colspan="6">
                        <strong style="color: #FFFFFF">Liquidación de Intereses</strong><br />
                    </td>
                    <td style="text-align: left; background-color: #3399FF;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left;">
                        Interés<br />
                        <uc1:decimales ID="txtinteres" runat="server" CssClass="textbox"   Enabled="false" />
                        
                    </td>
                    <td style="text-align: left;">
                        Retención<br />
                          <uc1:decimales ID="txtretencion" runat="server" CssClass="textbox"   Enabled="false" />
                      
                    </td>
                    <td style="text-align: left;">
                        MenosGMF<br />
                            <uc1:decimales ID="txtmenosgmf" runat="server" CssClass="textbox"   Enabled="false" />
             
                    </td>
                    <td style="text-align: left;">
                        Total A Pagar<br />
                         <uc1:decimales ID="txttotalapagar" runat="server" CssClass="textbox"   Enabled="false" />
             
                      
                    </td>
                    <td style="text-align: left;">
                        <br />
                    </td>
                    <td style="text-align: left;">
                        Valor A Renovar<br />
                         <uc1:decimales ID="txtvalorarenovar" runat="server" CssClass="textbox"   Enabled="false" />
             

                    </td>
                    <td style="text-align: left;">
                        <asp:CheckBox ID="cbCapitalizaInteres" runat="server" Text="Capitaliza Interés" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;" colspan="5">
                        <strong>Datos Del Giro</strong><br />
                        <asp:giro ID="giro" runat="server" CssClass="textbox" />
                    </td>
                </tr>
            </table>
<%--            </ContentTemplate>                       
            </asp:UpdatePanel>--%>      
        </asp:View>
        <asp:View ID="vwReporte" runat="server">
<%--         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
           <ContentTemplate>--%>
            <table border="0" cellpadding="0" cellspacing="0" width="700px">
                <tr>
                    <td style="text-align: left; width: 150px;">
                        <asp:Label ID="lblError" runat="server" Visible="False" ForeColor="Red" Style="text-align: right" />
                    </td>
                    <td style="text-align: left; width: 150px">
                        &nbsp;
                    </td>
                    <td style="text-align: left; width: 200px">
                        &nbsp;
                    </td>
                    <td style="text-align: left; width: 200px">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 150px;">
                        Número CDAT<br />
                        <asp:TextBox ID="TextBox11" runat="server" CssClass="textbox" Visible="false" />
                        <asp:TextBox ID="TextBox22" runat="server" CssClass="textbox" Width="90%" />
                        <asp:Label ID="Label11" runat="server" CssClass="textbox" Text="Autogenerado" Visible="false" />
                    </td>
                    <td style="text-align: left; width: 150px">
                        Número Pre-Impreso<br />
                        <asp:TextBox ID="txtNumPreImpresos" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                    <td style="text-align: left; width: 200px">
                        Fecha Apertura<br />
                        <uc2:fecha ID="txtfechaApRenova" runat="server" CssClass="textbox" />
                    </td>
                    <td style="text-align: left; width: 200px">
                        Tipo Calendario<br />
                        <asp:DropDownList ID="ddlcalendarioRenova" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 280px" colspan="2">
                        Tipo/Linea de CDAT<br />
                        <asp:DropDownList ID="ddlLineaRenova" runat="server" CssClass="textbox" Width="90%"
                            AppendDataBoundItems="True" OnSelectedIndexChanged="ddlLineaRenova_SelectedIndexChanged" />
                    </td>
                    <td style="text-align: left; width: 140px">
                        Modalidad<br />
                        <asp:DropDownList ID="ddlModalidads" runat="server" CssClass="textbox" Width="90%"
                            AutoPostBack="True" />
                    </td>
                    <td style="text-align: left; width: 140px">
                        Forma Captación<br />
                        <asp:DropDownList ID="ddlFormaCaptacions" runat="server" CssClass="textbox" Width="90%"
                            AppendDataBoundItems="True" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 140px">
                        Valor<br />
                        <uc1:decimales ID="txvalorrenovado" runat="server" CssClass="textbox" />
                    </td>
                    <td style="text-align: left; width: 140px">
                        Moneda<br />
                        <asp:DropDownList ID="ddlmonedarenovado" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                    <td style="text-align: left; width: 160px">
                        Plazo<br />
                        <asp:TextBox ID="txtplazorenova" runat="server" CssClass="textbox" Width="60%" AutoPostBack="True"
                            OnTextChanged="txtplazorenova_TextChanged" />
                    </td>
                    <td style="text-align: left; width: 160px">
                        Fecha Vencimiento<br />
                        <uc2:fecha ID="txtfechaVenciRemova" runat="server" CssClass="textbox" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 280px" colspan="2">
                        Destinación<br />
                        <asp:DropDownList ID="ddlDestinacions" runat="server" CssClass="textbox" Width="90%"
                            Visible="False" />
                    </td>
                    <td style="text-align: left; width: 160px">
                        Asesor Comercial<br />
                        <asp:DropDownList ID="DdlAsesorRenova" runat="server" CssClass="textbox" Width="90%"
                            AppendDataBoundItems="True" />
                    </td>
                    <td style="text-align: left; width: 160px">
                        Oficina<br />
                        <asp:DropDownList ID="ddloficinarenova" runat="server" CssClass="textbox" Width="90%"
                            AppendDataBoundItems="True" />
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="text-align: left; width: 710px;" colspan="4">
                        <strong>Titulares:</strong><br />
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            PageSize="3" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                            RowStyle-CssClass="gridItem" Style="font-size: x-small; margin-right: 0px;" OnRowDataBound="gvDetalle_RowDataBound"
                            GridLines="Horizontal" DataKeyNames="cod_usuario_cdat" OnRowDeleting="gvDetalle_RowDeleting"
                            Width="710px">
                            <Columns>
                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                <asp:TemplateField HeaderText="Codigo" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcodigo" runat="server" Text='<%# Bind("cod_usuario_cdat") %>'></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Codigo">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblcod_persona" runat="server" Text='<%# Bind("cod_persona") %>'
                                            CssClass="textbox" Width="80px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Identificación">
                                    <ItemTemplate>
                                        <cc1:TextBoxGrid ID="txtIdentificacion" runat="server" Text='<%# Bind("identificacion") %>'
                                            CommandArgument='<%#Container.DataItemIndex %>' Width="90px" AutoPostBack="True"
                                            OnTextChanged="txtIdentificacion_TextChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombres">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblNombre" runat="server" Text='<%# Bind("nombres") %>' CssClass="textbox"
                                            Width="120px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Apellidos">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblApellidos" runat="server" Text='<%# Bind("apellidos") %>' CssClass="textbox"
                                            Width="120px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ciudad">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblCiudad" runat="server" Text='<%# Bind("ciudad") %>' CssClass="textbox"
                                            Width="120px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dirección">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblDireccion" runat="server" Text='<%# Bind("direccion") %>' CssClass="textbox"
                                            Width="170px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Teléfono">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lbltelefono" runat="server" Text='<%# Bind("telefono") %>' CssClass="textbox"
                                            Width="80px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Principal">
                                    <ItemTemplate>
                                        <cc1:CheckBoxGrid ID="chkPrincipal" runat="server" Checked='<%# Convert.ToBoolean(Eval("principal")) %>'
                                            CommandArgument='<%#Container.DataItemIndex %>' AutoPostBack="true" OnCheckedChanged="chkPrincipal_CheckedChanged" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Conjunción" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblConjuncion" runat="server" Text='<%# Eval("conjuncion")  %>' Visible="false" />
                                        <cc1:DropDownListGrid ID="ddlConjuncion" runat="server" CssClass="textbox" CommandArgument='<%#Container.DataItemIndex %>'
                                            Width="70px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                            <PagerStyle CssClass="gridPager"></PagerStyle>
                            <RowStyle CssClass="gridItem"></RowStyle>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="text-align: left; width: 170px; vertical-align: top">
                        Modalidad Int<br />
                        <asp:RadioButtonList ID="rblModalidadInts" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Vencido</asp:ListItem>
                            <asp:ListItem Value="2">Anticipado</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="text-align: left; width: 150px; vertical-align: top">
                        Fecha Emisión<br />
                        <uc2:fecha ID="txtFechaEmis" runat="server" />
                    </td>
                </tr>
                <tr>
                <td style="text-align: left; width: 350px;">
                 <asp:CheckBox ID="cbInteresCuenta" runat="server" Text="Interés por Cuenta" />
                  </td>
                    <td style="text-align: left; width: 350px;">                       
                          <asp:Panel ID="panelTasa" runat="server">                
                            <ctl1:ctlTasaRenova ID="ctlTasaInteresRenova" runat="server" Width="400px" />
                         </asp:Panel>
                    </td>
                   
                    <td style="text-align: left; width: 350px;">
                        Periodicidad Intereses<br />
                        <asp:DropDownList ID="ddlPeriodicidadRenova" runat="server" AutoPostBack="True" CssClass="textbox"
                            Enabled="true" Width="120px" OnSelectedIndexChanged="ddlPeriodicidadRenova_SelectedIndexChanged" />
                        <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox" Visible="False" />
                    </td>
                    <td style="text-align: left; width: 135px;">
                        <asp:CheckBox ID="chkDesmaterials" runat="server" Text="Desmaterializado" />
                    </td>
                    <td style="text-align: left; width: 145px;">
                        <asp:CheckBox ID="chkCapitalizaInts" runat="server" Text="Capitaliza Interes" />
                    </td>
                    <td style="text-align: left; width: 135px;">
                        <asp:CheckBox ID="chkCobraRetens" runat="server" Text="Cobra Retención" />
                    </td>
                </tr>
            </table>
<%--             </ContentTemplate>                       
            </asp:UpdatePanel>  --%>
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
    </asp:MultiView>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
