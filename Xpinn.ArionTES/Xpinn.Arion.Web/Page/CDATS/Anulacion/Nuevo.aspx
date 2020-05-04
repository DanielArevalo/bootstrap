<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - CDATS Prorroga :." %>

<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc2" %>
<%@ Register Src="../../../General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow"
    TagPrefix="uc1" %>
    
<%@ Register Src="~/General/Controles/ctlTasa.ascx" TagName="ctlTasa" TagPrefix="ctl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">


    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">

            <asp:UpdatePanel ID="Panelgrilla" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                <ContentTemplate>
                <asp:Panel ID="PanelBloqueo" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="740px">
                        <tr>
                            <td style="text-align: left; width: 140px;">
                                Número CDAT<br />
                                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" 
                                    Visible="false" />
                                
                                <asp:TextBox ID="txtNumCDAT" runat="server" CssClass="textbox" Width="90%" />
                            </td>
                            <td style="text-align: left; width: 140px">
                                Fecha Apertura<br />
                                <uc2:fecha ID="txtFechaApertura" runat="server" CssClass="textbox" />
                            </td>
                            <td style="text-align: left; width: 140px">
                                Fecha Vencimiento<br />
                                <uc2:fecha ID="txtFechaVenci" runat="server" CssClass="textbox" />
                            </td>
                            <td colspan="2" style="text-align: left; width: 320px">
                                Tipo/Linea de CDAT<br />
                                <asp:DropDownList ID="ddlTipoLinea" runat="server" CssClass="textbox" Width="90%"
                                    AppendDataBoundItems="True" />
                            </td>
                        </tr>
                    </table>

                    <table border="0" cellpadding="0" cellspacing="0" width="740px">
                        <tr>
                            <td style="text-align: left; width: 150px">
                                Valor<br />
                                <uc1:decimales ID="txtValor" runat="server" CssClass="textbox" />
                            </td>
                            <td style="text-align: left; width: 160px">
                                Moneda<br />
                                <asp:DropDownList ID="ddlTipoMoneda" runat="server" CssClass="textbox" Width="90%" />
                            </td>
                            <td style="text-align: left; width: 110px">
                                Plazo<br />
                                <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Width="60%" />Días
                                <asp:FilteredTextBoxExtender ID="fte1" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txtPlazo" ValidChars="" />
                            </td>
                            <td style="text-align: left; width: 160px; margin-left: 40px;">
                                Tipo Calendario<br />
                                <asp:DropDownList ID="ddlTipoCalendario" runat="server" CssClass="textbox" Width="90%" />
                                 
                            </td>
                            <td style="text-align: left; width: 160px">
                                Oficina<br />
                                <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" Width="90%" AppendDataBoundItems="True" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 150px;">
                                Modalidad<br />
                                <asp:DropDownList ID="ddlModalidad" runat="server" CssClass="textbox" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlModalidad_SelectedIndexChanged" />
                            </td>
                            <td style="text-align: left; " colspan="4">
                                <ctl:ctlTasa ID="ctlTasaInteres" runat="server" Width="400px" />
                                <br />
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
            
                    <table border="0" cellpadding="0" cellspacing="0" width="740px">
                        <tr>
                            <td style="text-align: left; width: 740px" colspan="4">
                                <strong>Titulares:</strong><br />
                                <asp:Button ID="btnAddRow" runat="server" CssClass="btn8" Text="+ Adicionar Titular" Visible="false"/>
                                <br />
                                <div style="overflow: scroll; width: 740px;">
                                    <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                        RowStyle-CssClass="gridItem" Style="font-size: x-small" OnRowDataBound="gvDetalle_RowDataBound"
                                        GridLines="Horizontal" DataKeyNames="cod_usuario_cdat" Enabled=false>
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" Visible="false"/>
                                            <asp:TemplateField HeaderText="Codigo" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcodigo" runat="server" Text='<%# Bind("cod_usuario_cdat") %>'></asp:Label></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Identificación">
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <cc1:TextBoxGrid ID="txtIdentificacion" runat="server" Text='<%# Bind("identificacion") %>'
                                                                    CommandArgument='<%#Container.DataItemIndex %>' Width="90px"/>
                                                            </td>
                                                            <td>
                                                                <cc1:ButtonGrid ID="btnListadoPersona" CssClass="btnListado" runat="server" Text="..."
                                                                    CommandArgument='<%#Container.DataItemIndex %>' /><uc1:ListadoPersonas
                                                                        ID="ctlListadoPersona" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" Width="170px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cod. Persona">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblcod_persona" runat="server" Text='<%# Bind("cod_persona") %>'
                                                        CssClass="textbox" Width="80px" Enabled="false" /></ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombres">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblNombre" runat="server" Text='<%# Bind("nombres") %>' CssClass="textbox"
                                                        Width="160px" Enabled="false" /></ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Apellidos">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblApellidos" runat="server" Text='<%# Bind("apellidos") %>' CssClass="textbox"
                                                        Width="160px" Enabled="false" /></ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ciudad">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblCiudad" runat="server" Text='<%# Bind("ciudad") %>' CssClass="textbox"
                                                        Width="120px" Enabled="false" /></ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dirección">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblDireccion" runat="server" Text='<%# Bind("direccion") %>' CssClass="textbox"
                                                        Width="170px" Enabled="false" /></ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Teléfono">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lbltelefono" runat="server" Text='<%# Bind("telefono") %>' CssClass="textbox"
                                                        Width="80px" Enabled="false" /></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Principal">
                                                <ItemTemplate>
                                                    <cc1:CheckBoxGrid ID="chkPrincipal" runat="server" Checked='<%# Convert.ToBoolean(Eval("principal")) %>'
                                                        CommandArgument='<%#Container.DataItemIndex %>' /></ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Conjunción">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblConjuncion" runat="server" Text='<%# Eval("conjuncion")  %>' Visible="false" /><cc1:DropDownListGrid
                                                        ID="ddlConjuncion" runat="server" CssClass="textbox" CommandArgument='<%#Container.DataItemIndex %>'
                                                        Width="120px" />
                                                </ItemTemplate>
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
                        <tr>
                            <td colspan="4">
                                <br />
                                <hr style="width: 100%" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: left">
                                Observaciones<br />
                                <asp:TextBox ID="txtObservacion" runat="server" CssClass="textbox" Width="80%"/>
                            </td>                            
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers><asp:PostBackTrigger ControlID="ddlModalidad" /></Triggers>
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
                            Registro Anulado Correctamente
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
