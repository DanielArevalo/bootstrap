<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Cierre Ahorros :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlLineaAhorro.ascx" TagName="ddlLineaAhorro" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlOficina.ascx" TagName="ddlOficina" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlDestinacion.ascx" TagName="ddlDestinacion" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlPersonaEd.ascx" TagName="ddlPersona" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlModalidad.ascx" TagName="ddlModalidad" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlTasa.ascx" TagName="ctlTasa" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlFormaPago.ascx" TagName="ddlFormaPago" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlPeriodicidad.ascx" TagName="ddlPeriodicidad" TagPrefix="ctl" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<%@ Register Src="~/General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<%@ Register src="../../../General/Controles/ctlBusquedaRapida.ascx" tagname="listadopersonass" tagprefix="uc2" %>

<%@ Register src="../../../General/Controles/ctlDestinacion_ahorros.ascx" tagname="ddlDestinacion_Ahorro" tagprefix="ctl" %>

<%@ Register src="../../../General/Controles/ctlGiro.ascx" tagname="giro" tagprefix="uc3" %>
<script runat="server">

</script>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
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
                <table id="tbCriterios" border="0" cellpadding="1" cellspacing="0" >                    
                    <tr>
                       <td style="text-align:left" colspan="7">
                            <asp:Label ID="lblerror" runat="server" style="color: #FF3300" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left" colspan="7">
                            <strong>Datos de la Cuenta</strong>
                        </td>
                    </tr>                    
                    <tr>
                        <td style="text-align:left">
                            Número de Cuenta<br/>
                            <asp:TextBox ID="txtNumeroCuenta" runat="server" CssClass="textbox" Enabled="False" />
                            <asp:TextBox ID="txtCodPersona" runat="server" CssClass="textbox" Enabled="False" Visible="False" Width="30px" />
                        </td>
                        <td style="text-align:left">
                            &nbsp;
                        </td>
                        <td style="text-align:left" colspan="2">
                            Línea de Ahorros<br />                           
                            <asp:TextBox ID="txtLineaAhorros" runat="server" CssClass="textbox" Enabled="False" Width="300px" />
                        </td>
                        <td style="text-align:left">
                            &nbsp;
                        </td>
                        <td style="text-align:left">
                            Oficina<br />
                            <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" Enabled="False" Width="200px" />
                            &nbsp;&nbsp;&nbsp;&nbsp;<br />
                        </td>
                        <td style="text-align:left">
                            &nbsp;
                        </td>
                       
                    </tr>
                    <tr>
                             <td style="text-align:left">
                                 Fecha de Apertura<br />
                                 <uc1:fecha ID="txtFechaApertura" runat="server" Enabled="False" Habilitado="True" style="font-size: xx-small; text-align: right" TipoLetra="XX-Small" Width_="80" />
                        </td>
                             <td style="text-align:left">&nbsp; </td>
                             <td style="text-align:left">Fecha Ult.Movimiento<br />
                                 <uc1:fecha ID="txtFecUltMov" runat="server" Enabled="False" Habilitado="True" style="font-size: xx-small; text-align: right" TipoLetra="XX-Small" Width_="80" />
                                 <br />
                             </td>
                             <td style="text-align:left">Fec. Ult. Liquidación Int.<br />
                                 <uc1:fecha ID="txtFecUltLiq" runat="server" Enabled="False" Habilitado="True" style="font-size: xx-small; text-align: right" TipoLetra="XX-Small" Width_="80" />
                             </td>
                             <td style="text-align:left">&nbsp; </td>
                             <td style="text-align:left">Modalidad<br />
                                 <asp:DropDownList ID="ddlModalidad" runat="server" AutoPostBack="True" CssClass="textbox" Enabled="False" Width="150px" />
                             </td>
                             <td style="text-align:left">&nbsp; </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Tarjetas asociadas </strong>
                            <br />
                            <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="Num_Tarjeta" HeaderText="Num.Tarjeta" />
                                </Columns>
                            </asp:GridView>
                            <asp:Label ID="tarjeta" runat="server" Text="No tiene Tarjetas asociadas" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left"  colspan="6">
                            <strong>Titulares</strong></td>
                        <td style="text-align:left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="text-align:left">
                            <asp:Panel ID="PanelTitulares" runat="server">
                                <asp:UpdatePanel ID="upTitulares" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvDetalle" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="idcuenta_habiente" GridLines="Horizontal" HeaderStyle-CssClass="gridHeader" onselectedindexchanged="gvDetalle_SelectedIndexChanged" PagerStyle-CssClass="gridPager" PageSize="3" RowStyle-CssClass="gridItem" Style="font-size: x-small">
                                            <Columns>
                                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                                <asp:TemplateField HeaderText="Codigo" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lblcod_cuentahabiente" runat="server" CssClass="textbox" Enabled="false" Text='<%# Bind("idcuenta_habiente") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Identificación">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="width: 309px">
                                                                    <cc1:TextBoxGrid ID="txtIdentificacion" runat="server" AutoPostBack="True" CommandArgument="<%#Container.DataItemIndex %>" OnTextChanged="txtIdentificacion_TextChanged" Text='<%# Bind("identificacion") %>' Width="90px" />
                                                                </td>
                                                                <td>
                                                                    <cc1:ButtonGrid ID="btnListadoPersonas" runat="server" CommandArgument="<%#Container.DataItemIndex %>" CssClass="btnListado" OnClick="btnListadoPersona_Click" Text="..." />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 309px">
                                                                    <uc2:ListadoPersonass ID="ctlListadoPersonas" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cod. Persona">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lblcodigo" runat="server" enabled="false" Text='<%# Bind("cod_persona") %>' Width="45"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nombres">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lblNombre" runat="server" CssClass="textbox" Enabled="false" Text='<%# Bind("nombres") %>' Width="100px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Apellidos">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lblApellidos" runat="server" CssClass="textbox" Enabled="false" Text='<%# Bind("apellidos") %>' Width="100px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ciudad">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lblCiudad" runat="server" CssClass="textbox" Enabled="false" Text='<%# Bind("ciudad") %>' Width="90px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dirección">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lblDireccion" runat="server" CssClass="textbox" Enabled="false" Text='<%# Bind("direccion") %>' Width="100px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Teléfono">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lbltelefono" runat="server" CssClass="textbox" Enabled="false" Text='<%# Bind("telefono") %>' Width="90px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Principal">
                                                    <ItemTemplate>
                                                        <cc1:CheckBoxGrid ID="chkPrincipal" runat="server" AutoPostBack="true" Checked='<%# Convert.ToBoolean(Eval("principal")) %>' CommandArgument="<%#Container.DataItemIndex %>" OnCheckedChanged="chkPrincipal_CheckedChanged" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="gridPager" />
                                            <HeaderStyle CssClass="gridHeader" />
                                            <RowStyle CssClass="gridItem" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </td>
                        <td style="text-align:left">&nbsp; </td>
                    </tr>
                </table>
                <hr />
                <table id="tbLiquidacion" border="0" cellpadding="1" cellspacing="0" 
                    style="width: 89%">                    
                    <tr>
                        <td style="text-align: left">
                        Fec. Cierre<br />
                        <ucFecha:fecha ID="txtFechaCierre" runat="server" CssClass="textbox" />
                    </td>
                                                 <td style="text-align:left">
                                                     <asp:ImageButton ID="btnConsultar" runat="server" Visible="true"
                            ImageUrl="~/Images/btnGenerar.jpg" onclick="btnConsular_Click" />
                      </td>
                        <td style="text-align:left" colspan="3">
                            Motivo de Cierre <br />
                            <asp:TextBox ID="txtMotivo" runat="server" Width="400px" CssClass="textbox"></asp:TextBox><br />
                        </td>
                    </tr>
                </table>
                <table id="tbLiqDet" border="0" cellpadding="1" cellspacing="0" 
                    style="width: 88%">
                    <tr>
                        <td style="text-align:left"  colspan="4">
                            <strong>Liquidación</strong>
                        </td>                        
                    </tr>
                    <tr>
                        <td style="text-align:left">
                            Saldo Total
                        </td>
                        <td style="text-align:left">
                            &nbsp;
                        </td>
                        <td style="text-align:left">
                            <uc1:decimales ID="txtSaldoTotalLiq" runat="server" CssClass="textbox" 
                                Enabled="false" />
                         
                        </td>
                   
                    </tr>
                    <tr>
                        <td style="text-align:left">
                            Más Intereses
                        </td>
                        <td style="text-align:left">
                            &nbsp;
                        </td>
                        <td style="text-align:left">
                         <uc1:decimales ID="txtIntereses" runat="server" CssClass="textbox" 
                                Enabled="false" />
                           
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left">Más Interes Causado</td>
                        <td style="text-align:left">&nbsp;</td>
                        <td style="text-align:left">
                            <uc1:decimales ID="txtInteresesCausado" runat="server" CssClass="textbox" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left">
                           Más Interes Capitalizable</td>
                        <td style="text-align:left">
                            &nbsp;</td>
                        <td style="text-align:left">
                         <uc1:decimales ID="txtInteresescapitalizable" runat="server" CssClass="textbox" 
                                Enabled="false" />
                          
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left">
                            Menos Retención
                        </td>
                        <td style="text-align:left">
                            &nbsp;
                        </td>
                        <td style="text-align:left">
                           <uc1:decimales ID="txtRetencion" runat="server" CssClass="textbox" 
                                Enabled="false" />
                           
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left">Menos Retención causada </td>
                        <td style="text-align:left">&nbsp;</td>
                        <td style="text-align:left">
                            <uc1:decimales ID="txtRetencionCausada" runat="server" CssClass="textbox" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left">
                            Menos GMF
                        </td>
                        <td style="text-align:left">
                            &nbsp;
                        </td>
                        <td style="text-align:left">
                         <uc1:decimales ID="txtGMF" runat="server" CssClass="textbox" 
                                Enabled="false" />
                         
                        </td>
                        <td style="text-align:left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left" colspan="4">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left">                            
                            TOTAL
                        </td>
                        <td style="text-align:left">
                            &nbsp;
                        </td>
                        <td style="text-align:left">
                          <uc1:decimales ID="txtTotal" runat="server" CssClass="textbox" 
                                Enabled="false" />
                        </td>
                        <td style="text-align:left">
                        </td>
                    </tr>
                </table>          
            </asp:Panel>
                       <td style="text-align:left" rowspan="4">
                            <uc3:giro ID="ctlGiro" runat="server" />
                        </td>
         </asp:View>
        <asp:View ID="mvFinal" runat="server">
            <asp:Panel id="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br /><br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensaje" runat="server" 
                                Text="Datos Grabados Correctamente" style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <uc1:mensajeGrabar ID="ctlMensaje" runat="server"/>

</asp:Content>