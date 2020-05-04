<%@ Page Title=".: Aprobación Icetex :." MaintainScrollPositionOnPostback="true"
    Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <asp:Panel ID="panelGeneral" runat="server">
        <table style="width: 100%">
            <tr>
                <td colspan="6" style="text-align: left">
                    <strong>Datos del Asociado</strong>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 10%">Código
                </td>
                <td style="text-align: left;">                    
                    <asp:Label ID="lblConvocatoria" runat="server" Width="110px" Visible="false" />
                    <asp:Label ID="lblCod_Persona" runat="server" Width="110px"/>
                </td>
                <td style="text-align: left; width: 10%">Identificación
                </td>
                <td style="text-align: left;">
                    <asp:Label ID="lblIdentificacion" runat="server" Width="120px" />
                </td>
                <td style="text-align: left; width: 15%">Nombres y Apellidos
                </td>
                <td style="text-align: left;">
                    <asp:Label ID="lblNombre" runat="server" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td colspan="5" style="text-align: left">
                    <strong>Datos Generales</strong>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">Tipo de Beneficiario<br />
                    <asp:DropDownList ID="ddlTipoBeneficiario" runat="server" CssClass="textbox"
                        Width="250px" />
                </td>
                <td style="width: 100px">&nbsp;</td>
                <td style="text-align: left; width: 200px">Estado<br />
                    <asp:Label ID="lblCodEstado" runat="server" Visible="false" />
                    <asp:Label ID="lblEstado" runat="server" Width="250px" />
                </td>
                <td style="width: 100px">&nbsp;</td>
                <td style="text-align: left; width: 200px">Fecha de Solicitud<br />
                    <ucFecha:fecha ID="txtFecha" runat="server" Enabled="false" />
                </td>
            </tr>
        </table>
        <table style="width: 100%">            
            <tr>
                <td style="text-align: left;">Tipo Documento<br />
                    <asp:DropDownList ID="ddlTipoDoc" runat="server" CssClass="textbox"
                        Style="margin: 0px" Width="180px" />
                </td>
                <td style="text-align: left;">Nro Documento<br />
                    <asp:TextBox ID="txtNroDoc" runat="server" CssClass="textbox" Width="110px" />
                    <asp:FilteredTextBoxExtender ID="ftb13" runat="server" Enabled="True"
                        FilterType="Numbers, Custom" TargetControlID="txtNroDoc" ValidChars="-()." />
                </td>
                <td style="text-align: left;">Primer Apellido<br />
                    <asp:TextBox ID="txtApellido1" runat="server" CssClass="textbox" Width="150px" />
                </td>
                <td style="text-align: left;">Segundo Apellido<br />
                    <asp:TextBox ID="txtApellido2" runat="server" CssClass="textbox" Width="150px" />
                </td>
                <td style="text-align: left;">Primer Nombre<br />
                    <asp:TextBox ID="txtNombre1" runat="server" CssClass="textbox" Width="150px" />
                </td>
                <td style="text-align: left;">Segundo Nombre<br />
                    <asp:TextBox ID="txtNombre2" runat="server" CssClass="textbox" Width="150px" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left;">Dirección<br />
                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="textbox" Width="250px" />
                </td>
                <td style="text-align: left;">Télefono<br />
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" Width="100px" />
                </td>
                <td colspan="2" style="text-align: left;">Correo Electrónico<br />
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Width="250px" />
                </td>
                <td style="text-align: left;">Estrato<br />
                    <asp:TextBox ID="txtEstrato" runat="server" CssClass="textbox" MaxLength="1" Width="80px" />
                    <asp:FilteredTextBoxExtender ID="ftb12"
                        runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtEstrato"
                        ValidChars="" />
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td colspan="6" style="text-align: left">
                    <strong>Datos del Crédito</strong>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 30%">Universidad / I.E.S<br />
                    <asp:DropDownList ID="ddlUniversidad" runat="server" CssClass="textbox"
                        AutoPostBack="True" Style="margin: 0px;" Width="90%" OnSelectedIndexChanged="ddlUniversidad_SelectedIndexChanged" />
                </td>
                <td style="text-align: left; width: 20%">Programa<br />
                    <asp:DropDownList ID="ddlPrograma" runat="server" CssClass="textbox" Style="margin: 0px"
                        Width="90%" />
                </td>
                <td style="text-align: left; width: 20%">Tipo de Programa<br />
                    <asp:DropDownList ID="ddlTipoPrograma" runat="server" CssClass="textbox"
                        Style="margin: 0px;" Width="90%">
                        <asp:ListItem Value="1">Especialización(1 año)</asp:ListItem>
                        <asp:ListItem Value="2">Maestria(2 años)</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: left; width: 15%">Valor de Programa<br />
                    <uc1:decimales id="txtValorPrograma" runat="server" width_="90%" autopostback_="false" />
                </td>
                <td style="text-align: left; width: 15%">Periodos<br />
                    <asp:DropDownList ID="ddlPeriodos" runat="server" CssClass="textbox" Style="margin: 0px;"
                        Width="90%">
                        <asp:ListItem Value="1">1 Semestre</asp:ListItem>
                        <asp:ListItem Value="2">2 Semestre</asp:ListItem>
                        <asp:ListItem Value="3">3 Semestre</asp:ListItem>
                        <asp:ListItem Value="4">4 Semestre</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>

        <table style="width: 100%">
            <tr>
                <td style="text-align: left; width: 70%" colspan="2">
                    <strong>Lista de Check List del Crédito</strong><br />
                    Información de Aprobación&#160;&#160;<asp:DropDownList ID="ddlAprobacion" runat="server" AutoPostBack="true" CssClass="textbox" Width="150px"
                        OnSelectedIndexChanged="ddlAprobacion_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;" colspan="2">
                    <asp:GridView ID="gvCheckList" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" ForeColor="Black" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="codigo" HeaderText="Codigo" Visible="false">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion" HeaderText="Datos">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor" HeaderText="Valor">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="obs_resultado" HeaderText="Resultado">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="2">
                    <asp:Label ID="lblTotalCheck" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 60%; vertical-align: top">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <strong>Documentos</strong>
                            <div style="overflow: scroll; max-height: 350px;">
                                <asp:GridView ID="gvDocumentos" runat="server" AutoGenerateColumns="False" BackColor="White"
                                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" DataKeyNames="codigo,descripcion"
                                    ForeColor="Black" Width="100%" OnRowEditing="gvDocumentos_RowEditing">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_detall.jpg" ShowEditButton="True" />
                                        <asp:BoundField DataField="codigo" HeaderText="Codigo" Visible="false">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descripcion" HeaderText="Datos">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Observación" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtObservacion" runat="server" CssClass="textbox" Width="170px" Text='<%# Bind("observacion") %>' Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="gridHeader" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="text-align: left; width: 40%; vertical-align: top">
                    <strong>Visualizar</strong><br />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Literal ID="ltReport" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="gvDocumentos" EventName="RowEditing" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="panelFinal" runat="server">
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
                <td style="text-align: center; font-size: large;">Datos modificados correctamente
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
