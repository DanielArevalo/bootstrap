<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" 
CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - CDATS :." %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlTipoMoneda.ascx" TagName="ddlMoneda" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlTasa.ascx" TagName="ctlTasa" TagPrefix="ctl" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <script type="text/javascript">
        
        
        $(window).load(function () {
            if ($("#chkTasaCierreAnticipado").is(":checked")) {
                    $("#rbCalculoTasa_0").prop("disabled", false);
                    $("#rbCalculoTasa_1").prop("disabled", false);
                    $("#rbCalculoTasa_2").prop("disabled", false);
                    $("#rbCalculoTasa_3").prop("disabled", false);
                } else {
                    $("#rbCalculoTasa_0").prop("disabled", "disabled");
                    $("#rbCalculoTasa_1").prop("disabled", "disabled");
                    $("#rbCalculoTasa_2").prop("disabled", "disabled");
                    $("#rbCalculoTasa_3").prop("disabled", "disabled");
                }
        });

        $(document).ready(function (){
            $("#chkTasaCierreAnticipado").change(function () {
                if ($(this).is(":checked")) {
                    $("#rbCalculoTasa_0").prop("disabled", false);
                    $("#rbCalculoTasa_1").prop("disabled", false);
                    $("#rbCalculoTasa_2").prop("disabled", false);
                    $("#rbCalculoTasa_3").prop("disabled", false);
                } else
                {
                    $("#rbCalculoTasa_0").prop("disabled", "disabled");
                    $("#rbCalculoTasa_1").prop("disabled", "disabled");
                    $("#rbCalculoTasa_2").prop("disabled", "disabled");
                    $("#rbCalculoTasa_3").prop("disabled", "disabled");
  
                }
            });

        });
        
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

    <asp:MultiView ID="mvAhorros" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <br />
            <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="text-align:left" colspan="6">
                        <strong>Datos Principales</strong>&nbsp;&nbsp;
                        <asp:Label ID="lblConsecutivo" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Código<br/>
                        <asp:TextBox ID="txtCodLineaCDAT" runat="server" CssClass="textbox" Width="90px" />
                        <asp:RequiredFieldValidator ID="rfvCodLinea" runat="server" ErrorMessage="Debe ingresar el código de la línea" 
                            ControlToValidate="txtCodLineaCDAT" Display="Dynamic" 
                            ValidationGroup="vgGuardar" Font-Size="X-Small" style="color: Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td style="text-align:left">
                        &nbsp;&nbsp;&nbsp;                      
                    </td>
                    <td style="text-align:left" colspan="3">
                        Nombre<br/>
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" Width="400px" />
                        <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ErrorMessage="Debe ingresar la descripción de la línea" 
                            ControlToValidate="txtDescripcion" Display="Dynamic" 
                            ValidationGroup="vgGuardar" Font-Size="X-Small" style="color: Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td style="text-align:left">
                        &nbsp;&nbsp;&nbsp;                      
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Moneda<br />
                        <ctl:ddlMoneda ID="ddlMoneda" runat="server" Width="100px" />                        
                    </td>
                    <td style="text-align:left">
                        &nbsp;&nbsp;&nbsp;                      
                    </td>
                    <td style="text-align:left">
                        Estado:<br />
                        <asp:CheckBox ID="cbEstado" runat="server" Text="Activa" Width="80px" />
                    </td>
                    <td style="text-align:left">
                        <asp:CheckBox ID="cbInteresPorCuenta" runat="server" 
                            Text="Tasa de Interés por CDAT" Width="97%" />
                    </td>
                    <td style="text-align:left">
                        Porcentaje Retención<br />
                        <asp:TextBox ID="TxtPorcentajeRete" runat="server" CssClass="textbox" 
                            MaxLength="2" />
                    </td>
                    <td style="text-align:left">
                        &nbsp;&nbsp;&nbsp;                      
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Tipo Calendario
                        <br />
                        <asp:DropDownList ID="ddlTipoCalendario" runat="server" AutoPostBack="true" 
                            CssClass="textbox" 
                            />
                    </td>
                    <td style="text-align:left">
                        &nbsp;</td>
                    <td style="text-align:left">
                        Interes Anticipado<br />
                        <asp:CheckBox ID="cbInteresAnticipado" runat="server" Text="Si" Width="80px" />
                    </td>
                    <td style="text-align:left">
                        Capitaliza Interes
                        <br />
                        <asp:CheckBox ID="cbCapitalizaInteres" runat="server" Text="Si" Width="80px" />
                    </td>
                    <td style="text-align:left">
                        # Impreso Autonúmerico
                        <br />
                        <asp:CheckBox ID="cbNumeroImpreso" runat="server" Text="Si" Width="37px" />
                    </td>
                    <td style="text-align:left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align:left">&nbsp;</td>
                    <td style="text-align:left">&nbsp;</td>
                    <td style="text-align:left">
                        <asp:CheckBox ID="cbCambTasaSimulacion" runat="server" Text="Cambiar Tasa En Simulación" Width="97%" />
                    </td>
                    <td style="text-align:left">&nbsp;</td>
                    <td style="text-align:left">&nbsp;</td>
                    <td style="text-align:left">&nbsp;</td>
                </tr>
            </table>
           
            <table>
                <tr>
            <td >
            <table id="Table1" border="0" cellpadding="0" cellspacing="0">
                <tr>                            
                    <td style="text-align:left" colspan="4">
                        <strong>Tasa de Intereses:</strong>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left" colspan="4">
                        <ctl:ctlTasa ID="ctlTasaInteres" runat="server" />
                    </td>
                </tr>
                <tr>                            
                    <td style="text-align:left" colspan="4">
                        <br />
                        <strong>Requisitos:</strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvTopes" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" ForeColor="Black"
                        OnRowDataBound="gvTopes_RowDataBound" ShowFooter="True" Style="font-size: xx-small;
                        margin-right: 0px;" Width="39%" PageSize="2" DataKeyNames="cod_rango" OnRowDeleting="gvTopes_RowDeleting">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField><ItemTemplate><span><asp:Label ID="lblcodrango" runat="server" Text='<%# Bind("cod_rango") %>' Visible="False"></asp:Label></span></ItemTemplate></asp:TemplateField>
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                            <asp:TemplateField HeaderText="Descripcion" ItemStyle-HorizontalAlign="Center"><ItemTemplate><span><asp:Label ID="lblTipoTope" runat="server" Text='<%# Bind("tipo_tope") %>'
                                            Visible="False"></asp:Label><cc1:DropDownListGrid ID="ddlTipoTope" runat="server" AppendDataBoundItems="True" Enabled="false"
                                            AutoPostBack="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
                                            Style="font-size: xx-small; text-align: left" Width="120px"><asp:ListItem Value="1" Text="Monto" /><asp:ListItem Value="2" Text="Plazo (Dias)" /></cc1:DropDownListGrid></span></ItemTemplate><ItemStyle HorizontalAlign="Center" Width="120px" /></asp:TemplateField>
                            <asp:TemplateField HeaderText="Minimo" ItemStyle-HorizontalAlign="Left"><ItemTemplate><span><asp:TextBox ID="txtminimo" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                            text-align: left" Text='<%# Bind("minimo") %>' Width="100px"></asp:TextBox></span></ItemTemplate><ItemStyle HorizontalAlign="Left" Width="100px" /></asp:TemplateField>
                            <asp:TemplateField HeaderText="Maximo" ItemStyle-HorizontalAlign="Right"><ItemTemplate><span><asp:TextBox ID="txtmaximo" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                            text-align: left" Text='<%# Bind("maximo") %>' Width="100px"></asp:TextBox></span></ItemTemplate><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
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
                    </td>
                </tr>
            </table>
            </td>
            <td style="text-align:left;"  VALIGN="TOP">
            <asp:CheckBox ID="chkTasaCierreAnticipado" ClientIDMode="Static" runat="server" Text="Tasa para Cierre Anticipado"  />
    
                 <table id="Table2" border="0"   >
                <tr>
                    <td style="text-align:left" colspan="4">
                    <div>
                        <ctl:ctlTasa ID="ctlTasaInteresAnt" ClientIDMode="Static" runat="server"  />
                    </div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left" colspan="4">
                        
                        Modificar Tasa Prroroga</td>
                </tr>
                <tr>
                    <td style="text-align:left" colspan="4">
                        
                        <asp:CheckBox ID="cbInteresProrroga" runat="server" Text="Puede Cambiar Tasa" Width="97%" style="margin-bottom: 0px" />
                        
                    </td>
                </tr>
            </table>

               </td>
                </tr>
            </table>
           
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
