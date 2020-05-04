<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Fábrica de Créditos :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlSeleccionarPersona.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlValidarBiometria.ascx" TagName="validarBiometria" TagPrefix="uc1" %>

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
    <script type="text/javascript">
        function blur7(textbox) {
            var str = textbox.value;
            var formateado = "";
            str = str.replace(/\./g, "");
            if (str > 0) {
                str = parseInt(str);
                str = str.toString();

                if (str.length > 12)
                { str = str.substring(0, 12); }

                var long = str.length;
                var cen = str.substring(long - 3, long);
                var mil = str.substring(long - 6, long - 3);
                var mill = str.substring(long - 9, long - 6);
                var milmill = str.substring(0, long - 9);

                if (long > 0 && long <= 3)
                { formateado = parseInt(cen); }
                else if (long > 3 && long <= 6)
                { formateado = parseInt(mil) + "." + cen; }
                else if (long > 6 && long <= 9)
                { formateado = parseInt(mill) + "." + mil + "." + cen; }
                else if (long > 9 && long <= 12)
                { formateado = parseInt(milmill) + "." + mill + "." + mil + "." + cen; }
                else
                { formateado = "0"; }
            }
            else { formateado = "0"; }
            document.getElementById(textbox.id).value = formateado;
        }    

</script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPreAnalisis" runat="server" ActiveViewIndex="0">
        <asp:View ID="ViewTitular" runat="server">
            <table style="width: 100%">
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <strong style="text-align: left">Seleccione la Persona a la Cual se le Realizará Pre-Analisis</strong>
                        <asp:Panel ID="pBusqueda" runat="server" Height="70px">
                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td style="text-align: left;">
                                        <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwDatos" runat="server">
            <br />
            <asp:Panel ID="panelGeneral" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="700px">
                    <tr>
                        <td style="text-align: left;">
                            Identificación<br />
                            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="120px"
                                Enabled="false" />
                            <asp:Label ID="lblCodPersona" runat="server" Visible="false" />
                        </td>
                        <td style="text-align: left;">
                            Tipo Identificación<br />
                            <asp:TextBox ID="txtTipoIdentificacion" runat="server" CssClass="textbox" Width="100px"
                                Enabled="false" />
                        </td>
                        <td style="text-align: left;">
                            Oficina<br />
                            <asp:TextBox ID="txtOficina" runat="server" CssClass="textbox" Width="140px" Enabled="false" />
                        </td>
                        <td style="text-align: left;">
                            Estado Actual<br />
                            <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" Width="100px" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;" colspan="2">
                            Apellidos<br />
                            <asp:TextBox ID="txtApellidos" runat="server" CssClass="textbox" Width="90%" Enabled="false" />
                        </td>
                        <td style="text-align: left;" colspan="2">
                            Nombres<br />
                            <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="90%" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;" colspan="3">
                            Direccion<br />
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="textbox" Width="90%" Enabled="false" />
                        </td>
                        <td style="text-align: left;">
                            Telefono<br />
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" Width="90%" Enabled="false" />
                        </td>
                    </tr>
                </table>
                <hr />
                <strong>Datos de Pre-Análisis de Crédito</strong>
                <asp:UpdatePanel ID="upCalculos" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 50%; vertical-align: top" />
                                    <table border="0" cellpadding="1" cellspacing="0">
                                        <tr>
                                            <td style="text-align: left">
                                                Saldo Disponible
                                            </td>
                                            <td style="text-align: left">
                                                &nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSaldoDisponible" runat="server" AutoPostBack="True" CssClass="textbox" Width="150px" style="text-align: right" TabIndex="1"
                                                 onblur = "blur7(this)" OnTextChanged="txtSaldoDisponible_TextChanged" onprerender="txtSaldoDisponible_PreRender"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                Cuota de Crédito Cancelado
                                            </td>
                                            <td style="text-align: left">
                                                &nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                            <asp:TextBox ID="txtCuotaCreditoCancelado" runat="server" CssClass="textbox" Width="150px" style="text-align: right" TabIndex="2"
                                                 onblur = "blur7(this)" OnTextChanged="txtCuotaCreditoCancelado_TextChanged" onprerender="txtCuotaCreditoCancelado_PreRender" AutoPostBack="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                Cuota de Servicios
                                            </td>
                                            <td style="text-align: left">
                                                &nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCuotaServicios" runat="server" CssClass="textbox" Width="150px" style="text-align: right" TabIndex="3"
                                                 onblur = "blur7(this)" OnTextChanged="txtCuotaServicios_TextChanged" onprerender="txtCuotaServicios_PreRender" AutoPostBack="True" />                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                Pago a Terceros
                                            </td>
                                            <td style="text-align: left">
                                                &nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPagoTerceros" runat="server" CssClass="textbox" Width="150px" style="text-align: right" TabIndex="4"
                                                 onblur = "blur7(this)" OnTextChanged="txtPagoTerceros_TextChanged" onprerender="txtPagoTerceros_PreRender" AutoPostBack="True" />                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                Cuota de Otros
                                            </td>
                                            <td style="text-align: left">
                                                &nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                            <asp:TextBox ID="txtCuotaOtros" runat="server" CssClass="textbox" Width="150px" style="text-align: right" TabIndex="5"
                                                 onblur = "blur7(this)" OnTextChanged="txtCuotaOtros_TextChanged" onprerender="txtCuotaOtros_PreRender" AutoPostBack="True" />                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                Ingresos Adicionales
                                            </td>
                                            <td style="text-align: left">
                                                &nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtIngresosAdicionales" runat="server" CssClass="textbox" Width="150px" style="text-align: right" TabIndex="6"
                                                 onblur = "blur7(this)" OnTextChanged="txtIngresosAdicionales_TextChanged" onprerender="txtIngresosAdicionales_PreRender" AutoPostBack="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <hr />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                SUB TOTAL
                                            </td>
                                            <td style="text-align: left">
                                                &nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <uc2:decimales ID="txtSubTotal" runat="server" CssClass="textbox" Width="150px" Enabled="false"
                                                    BackColor="#FFFFCC" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                Deducciones
                                            </td>
                                            <td style="text-align: left">
                                                <asp:DropDownList ID="txtnumeroSMLMV" AutoPostBack="true" runat="server" CssClass="textbox"
                                                    Width="80px" Enabled="true" OnSelectedIndexChanged="txtSalariominimo_TextChanged"
                                                    TabIndex="7">
                                                    <asp:ListItem Text="SMLMV" Value="1" />
                                                    <asp:ListItem Text="50%" Value="2" />
                                                    <asp:ListItem Text="Otro" Value="3" />
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <%--<uc2:decimales ID="txtMenosSMLMV" runat="server" CssClass="textbox" Width="150px"
                                                    Enabled="false" />--%>
                                                <asp:TextBox ID="txtMenosSMLMV" runat="server" CssClass="textbox" Width="150px" style="text-align: right" TabIndex="8"
                                                 onblur = "blur7(this)" OnTextChanged="txtMenosSMLMV_TextChanged" onprerender="txtMenosSMLMV_PreRender" AutoPostBack="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                Total Disponible
                                            </td>
                                            <td style="text-align: left">
                                                &nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <uc2:decimales ID="txtDisponible" runat="server" CssClass="textbox" Width="150px"
                                                    Enabled="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 50%; vertical-align: top">
                                    <table border="0" cellpadding="1" cellspacing="0">
                                        <tr>
                                            <td style="text-align: left;width:180px">
                                                Aportes a la Fecha
                                            </td>
                                            <td style="text-align:left;width:180px">
                                                <uc2:decimales ID="txtAportes" runat="server" CssClass="textbox" Width="150px" Enabled="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                Créditos a la Fecha
                                            </td>
                                            <td style="text-align:left;">
                                                <uc2:decimales ID="txtCreditos" runat="server" CssClass="textbox" Width="150px" Enabled="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                Capitalización
                                            </td>
                                            <td style="text-align:left;">
                                                <asp:TextBox ID="txtCapitaliza" runat="server" CssClass="textbox" Width="150px" Enabled="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <span>Datos del Crédito Requerido:</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <hr />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                Monto Solicitado
                                            </td>
                                            <td style="text-align:left;">
                                                <asp:TextBox ID="txtMonto" runat="server" CssClass="textbox" Width="150px" style="text-align: right" TabIndex="8"
                                                     onblur = "blur7(this)" OnTextChanged="txtMonto_TextChanged" onprerender="txtMonto_PreRender" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                Número de Cuotas
                                            </td>
                                            <td style="text-align:left;">
                                                <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Width="150px" style="text-align: right" TabIndex="9"
                                                 onblur = "blur7(this)" OnTextChanged="txtPlazo_TextChanged" onprerender="txtPlazo_PreRender" />                                                
                                                <asp:FilteredTextBoxExtender ID="ftbePlazo" runat="server" FilterType="Numbers" TargetControlID="txtPlazo" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                Periodicidad
                                            </td>
                                            <td style="text-align:left;">
                                                <asp:DropDownList ID="ddlPeriodicidad" runat="server" CssClass="textbox" Width="180px" TabIndex="10" />                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">                                                
                                            </td>
                                            <td style="text-align: left">
                                                <asp:CheckBox ID="cbeducativo" Text="Crédito Educativo" runat="server" 
                                                    Enabled="true" TabIndex="11" AutoPostBack="True" 
                                                    oncheckedchanged="cbeducativo_CheckedChanged" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">   
                                                Créditos a Recoger                                             
                                            </td>
                                            <td style="text-align: left">
                                                <asp:UpdatePanel ID="upRecoger" runat="server">
                                                    <ContentTemplate>
                                                        <asp:HiddenField ID="hfValue" runat="server" Visible="false" />
                                                        <asp:TextBox ID="txtRecoger" CssClass="textbox" runat="server" Width="145px" ReadOnly="True" style="text-align: right" ></asp:TextBox>
                                                        <asp:PopupControlExtender ID="txtRecoger_PopupControlExtender" runat="server"
                                                            Enabled="True" ExtenderControlID="" TargetControlID="txtRecoger" 
                                                            PopupControlID="panelLista" OffsetY="22">
                                                        </asp:PopupControlExtender>
                                                        <asp:Panel ID="panelLista" runat="server" Height="120px" Width="300px" 
                                                            BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight"
                                                            ScrollBars="Auto" BackColor="#CCCCCC" Style="display: none">
                                                            <asp:GridView ID="gvRecoger" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="False" 
                                                                PageSize="5" ShowHeaderWhenEmpty="True" DataKeyNames="numero_credito" Style="font-size: xx-small" >
                                                                <Columns>
                                                                    <asp:BoundField DataField="numero_credito" HeaderText="No.Radicación" />
                                                                    <asp:BoundField DataField="linea_credito" HeaderText="Línea" />
                                                                    <asp:BoundField DataField="valor_total" HeaderText="Vr.Total" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" />
                                                                    <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                                                        <ItemTemplate>
                                                                            <cc1:CheckBoxGrid ID="cbListado" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>'
                                                                                OnCheckedChanged="cbListado_SelectedIndexChanged" AutoPostBack="true" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <FooterStyle CssClass="gridHeader" />
                                                                <RowStyle CssClass="gridItem" />
                                                                <HeaderStyle CssClass="gridHeader" />
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </ContentTemplate>  
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="txtRecoger" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center" colspan="2">
                                    <asp:Button ID="btnCalcularCupos" runat="server" CssClass="btn8" OnClick="btnCalcularCupos_Click" TabIndex="12"
                                        OnClientClick="btnCalcularCupos_Click" Text="Calcular Cupos" Height="22px" />
                                </td>
                            </tr>
                        </table>
                        <center><strong><asp:Label ID="lblMsj" runat="server" Style="color: #339966;" /></strong></center>
                        <hr />
                        <strong><asp:Label ID="lblTitulo" Text="Datos de Crédito a Solicitar" runat="server" /></strong>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvCreditos" runat="server" Width="90%" AutoGenerateColumns="False"
                                        AllowPaging="True" PageSize="5" ShowHeaderWhenEmpty="True" OnRowCommand="gvCreditos_RowCommand"
                                        DataKeyNames="educativo" Style="font-size: x-small" OnPageIndexChanging="gvCreditos_PageIndexChanging"
                                        OnRowDataBound="gvCreditos_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                                <ItemTemplate>
                                                    <cc1:CheckBoxGrid ID="chkSeleccionar" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>'
                                                        OnCheckedChanged="chkSeleccionar_CheckedChanged" AutoPostBack="true" Checked='<%# Convert.ToBoolean(Eval("idpreanalisis")) %>' />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Línea de Crédito">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblinea_credito" Text='<%#Eval("cod_linea_credito")%>' runat="server"
                                                        Width="30px" /><asp:Label ID="lbnomlinea_credito" Text='<%#Eval("nom_linea_credito")%>'
                                                            runat="server" Width="90px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Monto Máximo">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbmontomaximo" Text='<%# String.Format("{0:N}", Eval("monto_maximo")) %>'
                                                        runat="server" Width="90px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Plazo Máximo">
                                                <ItemTemplate>
                                                    <asp:Label ID="Plazo" Text='<%# String.Format("{0:N0}", Eval("plazo")) %>' runat="server"
                                                        Width="60px" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Cupo Dispónible">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbmonto" Text='<%# String.Format("{0:N}", Eval("monto")) %>' runat="server"
                                                        Width="90px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Cuota Estimada">
                                                <ItemTemplate>
                                                    <asp:Label ID="Cuota" Text='<%# String.Format("{0:N}", Eval("cuota_credito")) %>'
                                                        runat="server" Width="80px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Tasa Int.Cte.">
                                                <ItemTemplate>
                                                    <asp:Label ID="Tasa" Text='<%# String.Format("{0:N}", Eval("tasa")) %>' runat="server"
                                                        Width="60px" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Reciprocidad">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblreciprocidad" Text='<%# Eval("reciprocidad") %>' runat="server"
                                                        Width="30px" Visible="false" /><asp:CheckBox ID="cbreciprocidad" runat="server" Enabled="false"
                                                            Checked='<%#Convert.ToBoolean(Eval("reciprocidad")) %>' />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Refinanciar">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrefinancia" Text='<%# Eval("check") %>' runat="server" Width="30px"
                                                        Visible="false" /><asp:CheckBox ID="cbrefinancia" runat="server" Enabled="false"
                                                            Checked='<%#Convert.ToBoolean(Eval("check")) %>' />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Saldo Actual">
                                                <ItemTemplate>
                                                    <asp:Label ID="saldo" Text='<%# String.Format("{0:N}", Eval("saldo_capital")) %>'
                                                        runat="server" Width="90px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                            </asp:TemplateField>                                            
                                            <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Auxilio">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmanejaauxilio" Text='<%# Eval("maneja_auxilio") %>'
                                                        runat="server" style="font-size: xx-small" Visible="False" />
                                                    <asp:Label ID="lblporcentajeauxilio" Text='<%# String.Format("{0:N2}", Eval("porcentaje_auxilio")) %>'
                                                        runat="server" style="font-size: xx-small" />
                                                    <asp:Label ID="lblsimbolo" Text="%" runat="server" style="font-size: xx-small" Width="10px" />&nbsp;                                                    
                                                    <asp:Label ID="lblvalorauxilio" Text='<%# String.Format("{0:N}", Eval("valor_auxilio")) %>'
                                                        runat="server" style="font-size: xx-small" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="140px"/>
                                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pagerstyle" />
                                        <PagerTemplate>
                                            &nbsp;
                                            <asp:Button ID="btnPrimero" runat="server" CommandName="Page" ToolTip="Prim. Pag"
                                                CommandArgument="First" CssClass="pagfirst" />
                                            <asp:Button ID="btnAnterior" runat="server" CommandName="Page" ToolTip="Pág. anterior"
                                                CommandArgument="Prev" CssClass="pagprev" />
                                            <asp:Button ID="btnSiguiente" runat="server" CommandName="Page" ToolTip="Sig. página"
                                                CommandArgument="Next" CssClass="pagnext" />
                                            <asp:Button ID="btnUltimo" runat="server" CommandName="Page" ToolTip="Últ. Pag" CommandArgument="Last"
                                                CssClass="paglast" />
                                        </PagerTemplate>
                                        <FooterStyle CssClass="gridHeader" />
                                        <HeaderStyle CssClass="gridHeader" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers><asp:AsyncPostBackTrigger ControlID="btnCalcularCupos" EventName="Click" /></Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
            <center>
            <asp:Button ID="btnSolicitar" runat="server" CssClass="btn8" OnClick="btnSolicitar_Click"
                OnClientClick="btnSolicitar_Click" Text="Solicitar Crédito" Height="22px" Visible="false" />
            </center>
        </asp:View>
        <asp:View ID="vwReporte" runat="server">
            <br />
            <br />
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td><asp:Button ID="btnDatos" runat="server" CssClass="btn8" OnClick="btnDatos_click" Text="ver Datos" />
                         <asp:Button ID="btnimprimir" runat="server" CssClass="btn8" OnClick="btnImprime_Click" Text="Imprimir" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="rvReporte" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt"
                            Width="100%"><LocalReport ReportPath="Page\FabricaCreditos\Preanalisis\ReportePreanalisis.rdlc"><DataSources><rsweb:ReportDataSource /></DataSources></LocalReport></rsweb:ReportViewer>
                    </td>
                </tr>
                <tr>
                    <td>
                        <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx" height="500px"
                            runat="server" style="border-style: groove; float: left;"></iframe>
                    </td>
                </tr>
            </table>
        </asp:View>        
    </asp:MultiView>
    <uc1:validarBiometria ID="ctlValidarBiometria" runat="server" />

    <uc1:mensajegrabar ID="ctlMensaje" runat="server" />
    <script type='text/javascript'>
        function Forzar() {
            __doPostBack('', '');
        }
    </script>
    <script type="text/javascript">
        window.onload = function () {
            if (typeof window.event == 'undefined') {
                document.onkeypress = function (e) {
                    var test_var = e.target.nodeName.toUpperCase();
                    if (e.target.type) var test_type = e.target.type.toUpperCase();
                    if ((test_var == 'INPUT' && test_type == 'TEXT') || test_var == 'TEXTAREA') {
                        return e.keyCode;
                    } else if (e.keyCode == 8) {
                        e.preventDefault();
                    }
                }
            } else {
                document.onkeydown = function () {
                    var test_var = event.srcElement.tagName.toUpperCase();
                    if (event.srcElement.type) var test_type = event.srcElement.type.toUpperCase();
                    if ((test_var == 'INPUT' && test_type == 'TEXT') || test_var == 'TEXTAREA') {
                        return event.keyCode;
                    } else if (event.keyCode == 8) {
                        event.returnValue = false;
                    }
                }
            }
        }
    
    </script>
</asp:Content>
