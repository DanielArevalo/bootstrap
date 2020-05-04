<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Ahorros :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="Fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlPersonaEd.ascx" TagName="ddlPersona" TagPrefix="ctl" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlGiro.ascx" TagName="ctlgiro" TagPrefix="uc3" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlNumeroConDecimales.ascx" TagName="decimales"TagPrefix="uc2" %>

 

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript">
        function ActiveTabChanged(sender, e) {
        }

        var HighlightAnimations = {};

        function Highlight(el) {
            if (HighlightAnimations[el.uniqueID] == null) {
                HighlightAnimations[el.uniqueID] = Sys.Extended.UI.Animation.createAnimation({
                    AnimationName: "color",
                    duration: 0.5,
                    property: "style",
                    propertyKey: "backgroundColor",
                    startValue: "#FFFF90",
                    endValue: "#FFFFFF"
                }, el);
            }
            HighlightAnimations[el.uniqueID].stop();
            HighlightAnimations[el.uniqueID].play();
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvAhorroVista" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="pConsulta" runat="server" Width="100%">
                <table id="tbCriterios" border="0" cellpadding="1" cellspacing="0">
                    <tr>
                        <td style="text-align: left" colspan="5">
                            <strong>Datos Principales</strong>&nbsp;&nbsp;
                            <asp:Label ID="lblConsecutivo" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">Fecha del Retiro<br />
                            <uc1:fecha ID="txtFechaRetiro" runat="server" Enabled="True"
                                Habilitado="True" style="font-size: xx-small; text-align: right"
                                TipoLetra="XX-Small" Width_="80" />
                        </td>
                        <td style="text-align: left" colspan="3">Oficina<br />
                            <asp:TextBox ID="txtOficina" runat="server" CssClass="textbox" Width="400px" Enabled="false" />
                        </td>
                        <td style="text-align: left">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: left">Número de Cuenta<br />
                            <asp:TextBox ID="txtNumeroCuenta" runat="server" CssClass="textbox" />
                            <asp:TextBox ID="txtDigVer" runat="server" CssClass="textbox" Width="20px" Enabled="false" />
                        </td>
                        <td style="text-align: left" colspan="3">Línea de Ahorros<br />
                            <asp:TextBox ID="txtLineaAhorro" runat="server" CssClass="textbox" Width="400px" Enabled="false" />
                        </td>
                        <td style="text-align: left">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: left">Fecha de Apertura<br />
                            <ucFecha:Fecha ID="ucFechaApertura" runat="server" style="text-align: left" Enabled="false"  />
                        </td>
                        <td style="text-align: left">Saldo Total<br />
                            <uc2:decimales ID="txtSaldoTotal" runat="server" CssClass="textbox" Width="120px" Enabled="false"  />
                        </td>
                        <td style="text-align: left">Saldo Disponible<br />
                            <uc2:decimales ID="txtSaldoDisponible" runat="server" CssClass="textbox" Width="120px" Enabled="false"  />
                        </td>

                        <td style="text-align: left">Moneda<br />
                            <asp:TextBox ID="txtMoneda" runat="server" CssClass="textbox" Width="160px" Enabled="false" />
                        </td>
                        <td style="text-align: left">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="5">
                            <strong>Titular</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="5">
                            <ctl:ddlPersona ID="ctlPersona" runat="server" Width="400px" Enabled="false" />
                        </td>
                        <td class="tdD">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="5">
                            <asp:UpdatePanel ID="upTitulares" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvDetMovs" runat="server" AutoGenerateColumns="false"
                                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" PageSize="10" AllowPaging="False"
                                        BorderWidth="1px" GridLines="Both" CellPadding="0" ForeColor="Black"
                                        ShowFooter="True" Style="font-size: xx-small" Width="100%" DataKeyNames="cod_persona">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                            <asp:TemplateField HeaderText="Cod.Persona" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTercero" runat="server" Style="font-size: xx-small; text-align: left" Text='<%# Bind("cod_persona") %>' Width="100"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Identificación" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtIdentificD" runat="server" AutoPostBack="false" Width="100px" Style="font-size: xx-small; text-align: left" Text='<%# Bind("identificacion") %>'
                                                        CommandArgument='<%#Container.DataItemIndex %>' Enabled="false" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombre" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNombreTercero" runat="server" Style="font-size: xx-small; text-align: left" Text='<%# Bind("nombre") %>' Width="400"> </asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle CssClass="altrow" />
                                        <FooterStyle CssClass="gridHeader" />
                                        <HeaderStyle CssClass="gridHeader" />
                                        <RowStyle CssClass="gridItem" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="5">
                            <hr />
                            <strong>Datos del Retiro</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            Número Volante<br />
                            <asp:TextBox ID="txtVolante" runat="server" CssClass="textbox" Width="90px" Enabled="false" />
                        </td>
                        <td style="text-align: left">
                            Valor a Retirar<br />
                            <uc1:decimales ID="txtValorRetiro" runat="server" CssClass="textbox" Width="120px" />
                        </td>
                        <td style="text-align: left">
                        </td>
                        <td style="text-align: left">
                            <br />
                        </td>
                        <td style="text-align: left">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left">
                            <br />
                            <uc3:ctlgiro ID="ctlGiro" runat="server" />
                        </td>
                        <td style="text-align: left">&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="mvFinal" runat="server">
            <uc2:procesoContable ID="ctlproceso" runat="server" />
        </asp:View>
    </asp:MultiView>
    <uc1:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
