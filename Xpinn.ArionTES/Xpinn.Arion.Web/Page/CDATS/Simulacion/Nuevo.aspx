<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - CDATS Simulación:." %>

<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales"
    TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc2" %>
<%@ Register Src="../../../General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas"
    TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/ctlPeriodicidad.ascx" TagName="periodicidad"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow"
    TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctltasa.ascx" TagName="tasa" TagPrefix="uc5" %>

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
                                <td colspan="4" style="text-align: left">
                                    <strong>Datos Del CDAT :</strong>
                                </td>
                            </tr>
                            <tr>

                                <td style="text-align: left; width: 140px">
                                    Fecha Simulacion<br />
                                    <uc2:fecha ID="txtFechaApertura" runat="server" CssClass="textbox" />
                                </td>
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
                                    <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Width="60%" />
                                    Días
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers, Custom"
                                        TargetControlID="txtPlazo" ValidChars="" />
                                </td>
                            </tr>
                             <tr>
                                <td colspan="4" style="text-align: left">
                                    Tipo/Linea de CDAT<br />
                                <asp:DropDownList ID="ddlTipoLinea" runat="server" CssClass="textbox" 
                                    Width="90%" AppendDataBoundItems="True" AutoPostBack="true" 
                                    onselectedindexchanged="ddlTipoLinea_SelectedIndexChanged" />
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td colspan="4" style="text-align: left; width: 323px;">
                                    <strong>Tasa Interes :</strong>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 500px" colspan="2">
                                    <asp:Panel ID="panelTasa"  runat="server">
                                    <uc5:tasa ID="ctlTasa" runat="server" />
                                        </asp:Panel>
                                    <%--Tipo Tasa Interes<br />
                                    <asp:RadioButtonList ID="rblTipoTasa" runat="server" RepeatDirection="Horizontal"
                                        AutoPostBack="True" OnSelectedIndexChanged="rblTipoTasa_SelectedIndexChanged">
                                        <asp:ListItem Value="Fijo">Fijo</asp:ListItem>
                                        <asp:ListItem Value="Vari">Variable</asp:ListItem>
                                    </asp:RadioButtonList>--%>
                                </td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="0" cellspacing="0" width="740px">
                            <tr>
                                <td style="text-align: left; width: 150px;">
                                    Periodicidad Intereses<br />
                                    <uc1:periodicidad ID="ddlModalidad" runat="server" AutoPostBack="True" CssClass="textbox"
                                        OnSelectedIndexChanged="ddlModalidad_SelectedIndexChanged" />
                                </td>
                                <td style="text-align: left; width: 292px;">
                                    <br />
                                    <asp:CheckBox ID="chkCapitalizaInt" runat="server" Text="Capitaliza Interes" />
                                </td>
                                <td style="text-align: left; width: 351px;">
                                    <br />
                                    <asp:CheckBox ID="chkCobraReten" runat="server" Text="Cobra Retención" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
                    <%--<asp:UpdatePanel ID="Panelgrilla" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                <ContentTemplate>--%>
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 750px">
                        <tr>
                            <td colspan="6">
                                <br />
                                <hr style="width: 100%" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 740px" colspan="6">
                                <strong>Intereses Del CDAT:</strong><br />
                                <br />
                                <asp:GridView ID="gvDetalle" runat="server" GridLines="Horizontal" AutoGenerateColumns="False"
                                    AllowPaging="True" PageSize="20" OnPageIndexChanging="gvDetalle_PageIndexChanging" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                    RowStyle-CssClass="gridItem" DataKeyNames="codigo_cdat,estado" Width="750px"
                                    HorizontalAlign="Center">
                                    <Columns>
                                        <asp:BoundField DataField="fecha_intereses" DataFormatString="{0:d}" HeaderText="Fecha">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Interes" DataField="intereses_cap" DataFormatString="{0:c}" >
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="retencion_cap" HeaderText="Retencion" DataFormatString="{0:c}" > 
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="capitalizar_int" DataFormatString="{0:c}"  HeaderText="Valor A Capitalizar">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valor" DataFormatString="{0:c}" HeaderText="Valor A Pagar">
                                            <ItemStyle />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                    <PagerStyle CssClass="gridPager"></PagerStyle>
                                    <RowStyle CssClass="gridItem"></RowStyle>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 4px;">
                                &nbsp;</td>
                            <td style="text-align: right; width: 38px;">&nbsp;</td>
                            <td style="text-align: right; width: 178px;">
                                &nbsp;</td>
                            <td style="text-align: right; width: 176px;">
                                &nbsp;</td>
                            <td style="text-align: right; width: 175px;">
                                &nbsp;</td>
                            <td style="text-align: right; width: 175px;">
                                &nbsp;</td>
                            
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 4px;">&nbsp;</td>
                            <td style="text-align: right; width: 38px;">&nbsp;</td>
                            <td style="text-align: right; width: 178px;">&nbsp;</td>
                            <td style="text-align: right; width: 176px;">&nbsp;</td>
                            <td style="text-align: right; width: 175px;">&nbsp;</td>
                            <td style="text-align: right; width: 175px;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 4px;">&nbsp;</td>
                            <td style="text-align: right; width: 38px;">&nbsp;</td>
                            <td style="text-align: right; width: 178px;">
                                <asp:Label ID="lblInteres" runat="server" style="float:left; margin-left:33px" Visible="False" Width="90px">Total Interes</asp:Label>
                                <br />
                             <asp:TextBox ID="txtInteres" runat="server" CssClass="textbox" Enabled="False" Visible ="false" DataFormatString="{0:c}" 
                                                    Width="153px"></asp:TextBox>
                                           
                             
                                  <br />
                            </td>
                            <td style="text-align: right; width: 176px;">
                                <asp:Label ID="lblRetencion" runat="server" style="float:left; margin-left:33px" Visible="False" Width="105px">Total Retención</asp:Label>
                                <asp:TextBox ID="txtTotalRetencion"  runat="server"  DataFormatString="{0:c}" CssClass="textbox" ReadOnly="true" style="float:left; margin-left:29px" Visible="false"></asp:TextBox>
                            </td>
                            <td style="text-align: right; width: 175px;">
                                <asp:Label ID="lblCapitaliza" runat="server" style="float:left; margin-left:33px" Visible="False" Width="105px">Total Capitaliza</asp:Label>
                                <asp:TextBox ID="txtCapitaliza"  runat="server"  DataFormatString="{0:c}" CssClass="textbox" ReadOnly="true" style="float:left; margin-left:29px" Visible="false"></asp:TextBox>
                            </td>
                            <td style="text-align: right; width: 175px;">
                                <asp:Label ID="lblNeto" runat="server" style="float:left; margin-left:33px" Visible="False">Total Valor Neto</asp:Label>
                              
                                   <asp:TextBox ID="txtNeto" runat="server" CssClass="textbox" Enabled="False" DataFormatString="{0:c}"
                                                    Width="153px"></asp:TextBox> 

                            </td>
                        </tr>
                        <tr>
                            
                            <td style="text-align: right; width: 4px;">&nbsp;</td>
                            <td style="text-align: right;">
                                <br />
                            </td>
                        </tr>
                    </table>
                    </asp:Panel> </td> </tr> </table>
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
                            Prorroga
                            <asp:Label ID="lblMsj" runat="server"></asp:Label>
                            Correctamente
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
        <asp:View ID="vwReporte" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="700px">
                <tr>
                    <td style="text-align: left; width: 150px;">
                        Número CDAT<br />
                        <asp:TextBox ID="TextBox11" runat="server" CssClass="textbox" Visible="false" />
                        <asp:TextBox ID="TextBox22" runat="server" CssClass="textbox" Width="90%" />
                        <asp:Label ID="Label11" runat="server" Text="Autogenerado" CssClass="textbox" Visible="false" />
                    </td>
                    <td style="text-align: left; width: 150px">
                        Número Pre-Impreso<br />
                        <asp:TextBox ID="txtNumPreImpresos" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                    <td style="text-align: left; width: 200px">
                        Fecha Apertura<br />
                        <uc2:fecha ID="Fecha2" runat="server" CssClass="textbox" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 280px" colspan="2">
                        Tipo/Linea de CDAT<br />
                        <asp:DropDownList ID="DropDownList11" runat="server" CssClass="textbox" Width="90%"
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
                        <uc1:decimales ID="Decimales1" runat="server" CssClass="textbox" />
                    </td>
                    <td style="text-align: left; width: 140px">
                        Moneda<br />
                        <asp:DropDownList ID="DropDownList22" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                    <td style="text-align: left; width: 160px">
                        Plazo<br />
                        <asp:TextBox ID="TextBox33" runat="server" CssClass="textbox" Width="60%" />
                    </td>
                    <td style="text-align: left; width: 160px">
                        Tipo Calendario<br />
                        <asp:DropDownList ID="DropDownList33" runat="server" CssClass="textbox" Width="90%" />
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
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
