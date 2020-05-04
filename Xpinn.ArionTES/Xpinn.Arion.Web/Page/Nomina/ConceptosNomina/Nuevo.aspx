<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Page_Nomina_ConceptosNomina_Nuevo" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <br />
    <br />
    <asp:Label ID="error" runat="server"></asp:Label>
    <br />
    <br />
    <asp:MultiView ID="mvDatos" ActiveViewIndex="0" runat="server">
        <asp:View runat="server">
            <asp:Panel ID="PnGeneral" runat="server">
                <table border="0">
                    <tr>
                        <td class="tdI" colspan="3" style="color: #FFFFFF; background-color: #0066FF; height: 20px; width: 100%;">
                            <strong>CONCEPTOS NOMINA</strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" colspan="3">
                            <asp:Panel ID="Pn1" runat="server">
                                <table>
                                    <tr>
                                        <td style="width: 300px; text-align: left" colspan="2">Consecutivo : 
                                        </td>
                                        <td style="width: 300px; text-align: left" colspan="2">
                                            <asp:TextBox ID="txtConsecutivo" runat="server" Enabled="false" CssClass="textbox" Width="150px"></asp:TextBox>
                                        </td>
                                        <td style="width: 300px; text-align: left;">Tipo : 
                                        </td>
                                        <td style="width: 300px; text-align: left;">
                                            <asp:DropDownList ID="ddlTipo" runat="server" CssClass="dropdown" Width="100%">
                                                <asp:ListItem Value=" " Text="Seleccione un Item"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Pago"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Descuento"></asp:ListItem>
                                             
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 300px; text-align: left;">Tipo de Concepto:
                                        </td>
                                        <td style="width: 300px; text-align: left;">
                                            <asp:DropDownList ID="ddlTipConcepto" runat="server" Width="150px" CssClass="dropdown">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 300px; text-align: left;" colspan="2">Descripción : 
                                        </td>
                                        <td style="width: 300px; text-align: left;" colspan="2">
                                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                                        </td>
                                        <td style="width: 300px; text-align: left;">Clase de Concepto: 
                                        </td>
                                        <td style="width: 300px; text-align: left;">
                                            <asp:DropDownList ID="ddlClaseConcepto" runat="server" CssClass="dropdown" Width="150px">
                                                <asp:ListItem Value="1" Text="Prestacional"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="No Prestacional"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Otros"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 300px; text-align: left;" colspan="2">Ponderador : 
                                        </td>
                                        <td style="width: 300px; text-align: left;" colspan="2">
                                                          <asp:TextBox ID="txtPonderado" runat="server" CssClass="textbox" Visible="true"></asp:TextBox>
          
                                                   </td>
                                        <td style="width: 300px; text-align: left;">&nbsp;</td>
                                        <td style="width: 300px; text-align: left;">
                                            <asp:Label ID="Mensaje" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 300px; text-align: left;">Unidad de concepto : 
                                        </td>
                                        <td style="width: 300px; text-align: left;">
                                            <asp:RadioButtonList ID="rbdUnidadConcepto" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Text="Valor"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Cantidad"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: left; width: 300px;">

                                            &nbsp;</td>
                                        <td colspan="2" style="width: 300px; text-align: left;">&nbsp;</td>
                                        <td style="width: 300px; text-align: left;">&nbsp;</td>
                                        <td style="width: 300px; text-align: left;">&nbsp;</td>
                                        <td style="width: 300px; text-align: left;">&nbsp;</td>
                                        <td style="width: 300px; text-align: left;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: center">Conceptos Base : </td>
                                        <td style="text-align: center">
                                            <div class="text-left">
                                                Provisiona Extralegal</div>
                                           
                                            <br />
                                            <br />
                                            <asp:RadioButton ID="rbdProvisonExtralegalSi" runat="server" AutoPostBack="true" GroupName="Grup26"  Text="Si" OnCheckedChanged="rbdProvisonExtralegalSi_CheckedChanged" />
                                            <asp:RadioButton ID="rbdProvisonExtralegalNo" runat="server" AutoPostBack="true" GroupName="Grup26" Text="No" OnCheckedChanged="rbdProvisonExtralegalNo_CheckedChanged" />
                                        </td>
                                        <td class="text-left">
                                            <asp:Label ID="lblporcentaje" runat="server" Text="Porcentaje"></asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtPorcentaje" runat="server" CssClass="textbox" Enabled="true" MaxLength="4"></asp:TextBox>
                                        </td>
                                        <td colspan="2" style="text-align: center">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">&nbsp;</td>
                                        <td style="text-align: left">&nbsp;</td>
                                        <td style="text-align: left">
                                            <asp:UpdatePanel ID="upRecoger" runat="server">
                                                <ContentTemplate>
                                                    <asp:HiddenField ID="HiddenField1" runat="server" Visible="false" />
                                                    <asp:TextBox ID="txtConceptos" runat="server" CssClass="textbox" ReadOnly="True" Style="text-align: right" TextMode="SingleLine" Width="145px"></asp:TextBox>
                                                    <asp:PopupControlExtender ID="txtConceptos_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" OffsetY="22" PopupControlID="panelLista" TargetControlID="txtConceptos"></asp:PopupControlExtender>
                                                    <asp:Panel ID="panelLista" runat="server" BackColor="#CCCCCC" BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight" Height="200px" ScrollBars="Auto" Style="display: none" Width="300px">
                                                        <asp:GridView ID="gvConceptosBase" runat="server" AutoGenerateColumns="False" DataKeyNames="consecutivo" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Código"><ItemTemplate><asp:Label ID="lbl_consecutivo" runat="server" Text='<%# Bind("consecutivo") %>'></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="Left" Width="50px" /></asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Descripción"><ItemTemplate><asp:Label ID="lbl_descripcion" runat="server" Text='<%# Bind("descripcion") %>'></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="Left" /></asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="gridIco"><ItemTemplate><cc1:CheckBoxGrid ID="cbListado" runat="server" AutoPostBack="true" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>' OnCheckedChanged="cbListado_CheckedChanged" /></ItemTemplate><HeaderStyle CssClass="gridIco" /></asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="text-align: left">&nbsp;</td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>

         <asp:View ID="mvFinal" runat="server">
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
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Información Grabada Correctamente"></asp:Label>
                            
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

       
         
</asp:Content>

