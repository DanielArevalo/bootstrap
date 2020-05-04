<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - CDATS Fusion:." %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas"
    TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow"
    TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlGiro.ascx" TagName="giro" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctltasa.ascx" TagName="tasa" TagPrefix="uc5" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <table style="width: 1039px">
                <tr>
                    <td colspan="4" style="text-align: left">
                        <strong>Criterios de Busqueda :</strong>
                    </td>
                </tr>
                <tr>
                    <td style="width: 149px; text-align: left">
                        Numero CDAT<br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="62%" />
                    </td>
                    <td style="text-align: left; width: 129px">
                        Fecha de Apertura<br />
                        <ucFecha:fecha ID="txtFecha" runat="server" />
                    </td>
                    <td style="text-align: left; width: 129px">
                        Tipo Linea CDAT<br />
                        <asp:DropDownList ID="ddlModalidad" runat="server" Width="95% " CssClass="textbox">
                            <asp:ListItem Value="1">Seleccione Un Item</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 161px; text-align: left">
                        Identificación<br />
                        <asp:TextBox ID="txtidentificacion" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                    <td style="width: 158px; text-align: left">
                        Nombre<br />
                        <asp:TextBox ID="txtnombre" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                    <td style="text-align: left; width: 121px">
                        Oficina<br />
                        <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" Width="93%" AppendDataBoundItems="True" />
                    </td>
                    <td style="text-align: left; width: 128px">
                        Asesor Comercial<br />
                        <asp:DropDownList ID="ddlasesor" runat="server" CssClass="textbox" Width="90%" AppendDataBoundItems="True" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="panelGrilla" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing"
                                PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                RowStyle-CssClass="gridItem" DataKeyNames="codigo_cdat" OnRowDeleting="gvLista_RowDeleting"
                                Style="font-size: xx-small">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="check" Checked="true" runat="server" Style="text-align: right"
                                                TipoLetra="XX-Small" Habilitado="True" AutoPostBack_="True" Enabled="True" Width_="80" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="codigo_cdat" HeaderText="Num cd">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="numero_fisico" HeaderText="Num Fi">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_apertura" HeaderText="Fecha Apertura" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="modalidad" HeaderText="Modalidad">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="codforma_captacion" HeaderText="Forma">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nommoneda" HeaderText="Moneda">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="plazo" HeaderText="Plazo">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tipo_calendario" HeaderText="Tipo">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cod_destinacion" HeaderText="Destina">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombre" HeaderText="Ase">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cod_oficina" HeaderText="Oficina">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="identificacion" HeaderText="Identificacion">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre Titular">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cod_tipo_tasa" HeaderText="Tipo">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="modalidad_int" HeaderText="Modalida">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tasa_efectiva" HeaderText="Tasa">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tasa_nominal" HeaderText="Tipo Tasa">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tipo_calendario" HeaderText="Tipo">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cod_tipo_tasa" HeaderText="+pum">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cod_tipo_tasa" HeaderText=" ">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                            <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwReporte" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="700px">
                <tr>
                    <td style="text-align: left; width: 150px;">
                        Número CDAT<br />
                        <asp:TextBox ID="TextBox11" runat="server" CssClass="textbox" Visible="false" enabled="false"/>
                        <asp:TextBox ID="TextBox22" runat="server" CssClass="textbox" Width="90%" enabled="false"/>
                        <asp:Label ID="Label11" runat="server" Text="Autogenerado" CssClass="textbox" Visible="false" enabled="false" />
                    </td>
                    <td style="text-align: left; width: 150px">
                        Número Pre-Impreso<br />
                        <asp:TextBox ID="txtNumPreImpresos" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                    <td style="text-align: left; width: 200px">
                        Fecha Apertura<br />
                        <uc2:fecha ID="Fecha2" runat="server" cssclass="textbox" enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 280px" colspan="2">
                        Tipo/Linea de CDAT<br />
                        <asp:DropDownList ID="DropDownList11" runat="server" CssClass="textbox" Width="90%" enabled="false"
                            AppendDataBoundItems="True" />
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
                        <uc1:decimales ID="txtValor" runat="server" cssclass="textbox" enabled="false"/>
                    </td>
                    <td style="text-align: left; width: 140px">
                        Moneda<br />
                        <asp:DropDownList ID="DropDownList22" runat="server" CssClass="textbox" Width="90%"  enabled="false"/>
                    </td>
                    <td style="text-align: left; width: 160px">
                        Plazo<br />
                        <asp:TextBox ID="TextBox33" runat="server" CssClass="textbox" Width="60%"  enabled="false"/>
                    </td>
                    <td style="text-align: left; width: 160px">
                        Tipo Calendario<br />
                        <asp:DropDownList ID="DropDownList33" runat="server" CssClass="textbox" Width="90%" enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 280px" colspan="2">
                        Destinación<br />
                        <asp:DropDownList ID="ddlDestinacions" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                    <td style="text-align: left; width: 160px">
                        Asesor Comercial<br />
                        <asp:DropDownList ID="ddlAsesors" runat="server" CssClass="textbox" Width="90%" AppendDataBoundItems="True" />
                    </td>
                    <td style="text-align: left; width: 160px">
                        Oficina<br />
                        <asp:DropDownList ID="DropDownList44" runat="server" CssClass="textbox" Width="90%"
                            AppendDataBoundItems="True" />
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="text-align: left; width: 710px;" colspan="4">
                        <strong>Titulares:</strong><br />
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
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
                                            CommandArgument='<%#Container.DataItemIndex %>' Width="90px" AutoPostBack="True" />
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
                                            CommandArgument='<%#Container.DataItemIndex %>' AutoPostBack="true" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Conjunción">
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
                    <td style="text-align: left; width: 500px" colspan="2">
                        <uc5:tasa ID="Tasa11" runat="server" enabled="false" />
                    </td>
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
                        Periodicidad Intereses<br />
                        <asp:DropDownList ID="DropDownList55" runat="server" CssClass="textbox" Width="80%" enabled="false"
                            AutoPostBack="True" />
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
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
