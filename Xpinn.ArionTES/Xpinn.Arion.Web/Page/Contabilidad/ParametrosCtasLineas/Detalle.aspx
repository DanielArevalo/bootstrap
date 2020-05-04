<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="ctlmensaje" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlListarCodigo.ascx" TagName="ctlListarCodigo" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <style type="text/css">
        table.tabs {
            border-collapse: separate;
            border-spacing: 0;
            background-color: green 0px;
            font-size: 0.5em;
            width: 100px;
            margin-right: 2px;
            height: 100px;
        }

        th.tabck {
            border: red 0px;
            border-bottom: 0;
            border-radius: 0.0em 0.0em 0 0;
            background-color: green 0px;
            padding: 0.3em;
            text-align: center;
            cursor: pointer;
        }

        tr.filadiv {
            background-color: rgb(255, 255, 255);
        }
        /* El ancho y alto de los div.tabdiv se configuran en cada aplicación */
        div.tabdiv {
            background-color: rgb(255, 255, 255);
            border: 0;
            padding: 0.5em;
            overflow: auto;
            display: none;
            width: 100%;
            height: auto;
        }
        /* Anchos y altos para varios contenedores en la misma página. Esta parte se particulariza para cada contenedor. (IE8 necesita !important) */
        td#tab-0 > div {
            width: 25em !important;
            height: 25em !important;
        }

        .style1 {
            width: 13px;
        }
    </style>
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

        function ToggleHidden(value) {
            $find('<%=Tabs.ClientID%>').get_tabs()[2].set_enabled(value);
        }
    </script>

    <asp:Panel ID="upPrincipal" runat="server" ChildrenAsTriggers="true">

        <table style="text-align: left; width: 80%">
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="lblMensaje" runat="server" Visible="False" ForeColor="Blue" Font-Bold="true"></asp:Label>
                <td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left;">Línea de Crédito<br />
                    <asp:DropDownList ID="ddlLineaCredito" runat="server" Height="25px" Width="95%" CssClass="dropdown" AppendDataBoundItems="True" Enabled="false">
                    </asp:DropDownList>
                </td>
                <td> <br/>                   
                    <asp:CheckBox ID="cbLineaCastigada" runat="server" Text="Linea Castigada" Checked="false" Enabled="false"/>
                </td>
                <td style="text-align: left;">Atributo<br />
                    <asp:DropDownList ID="ddlAtributo" runat="server" Height="25px" Width="95%" CssClass="dropdown" AutoPostBack="True" OnSelectedIndexChanged="ddlAtributo_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="1" cellspacing="0" style="text-align: left; margin-right: 0px; width: 100%;">
            <caption>
                <br />
                <tr>
                    <td class="tdI" colspan="6"><strong>Parámetros de Cuentas</strong>&nbsp;
                            <br />
                    </td>
                    <td class="tdI">&nbsp; </td>
                </tr>
            </caption>
        </table>
        <asp:Panel ID="pTotales" runat="server" Width="1000px">
            <asp:TabContainer ID="Tabs" runat="server" ActiveTabIndex="0" CssClass="CustomTabStyle"
                OnClientActiveTabChanged="ActiveTabChanged" Style="margin-right: 30px; text-align: left" Width="1000px">
                <asp:TabPanel ID="tabOperacion" runat="server" HeaderText="Operación">
                    <HeaderTemplate>Operación</HeaderTemplate>
                    <ContentTemplate>
                        <asp:Label ID="lblCuentaOrden" Text="Cuenta orden" runat="server" Visible="False" Font-Bold="True"/>                             
                        <table>
                            <tr>
                                <td style="width: 200px; text-align: center">Cod. Cuenta Local<br />
                                    <cc1:TextBoxGrid ID="txtCodCuentaOp" runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" OnTextChanged="txtCodCuentaOp_TextChanged" />
                                    <asp:TextBox ID="txtIdParamOp" runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="False" />
                                    <asp:TextBox ID="txtTipoParamOp" runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="False" />
                                    <cc1:ButtonGrid ID="btnPlaCuenLocalOp" runat="server" CssClass="btnListado" Height="26px" Text="..." OnClick="btnListadoPlan_Click" />
                                    <uc1:ListadoPlanCtas ID="ctlListadoPlanContable" runat="server" />
                                </td>
                                <td style="width: 230px; text-align: center">Nombre Cuenta Local<br />
                                    <cc1:TextBoxGrid ID="txtNomCuentaOp" runat="server" CssClass="textbox" Enabled="False" Width="210px" />
                                </td>
                                <td style="width: 230px; text-align: center">Código Cuenta NIIF<br />
                                    <cc1:TextBoxGrid ID="txtCodCuentaNIIFOp" runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" OnTextChanged="txtCodCuentaNIIFOp_TextChanged" />
                                    <cc1:ButtonGrid ID="btnPlanCuenNIIFOp" runat="server" CssClass="btnListado" Height="26px" Text="..." OnClick="btnListadoPlanNIIF_Click" />
                                </td>
                                <td style="width: 250px; text-align: center">Nombre Cuenta NIIF<br />
                                    <cc1:TextBoxGrid ID="txtNomCuentaNIIFOp" runat="server" CssClass="textbox" Enabled="False" Width="210px" />
                                </td>
                            </tr>
                        </table>
                        <asp:Label ID="lblCuentaOrdCon" Text="Cuenta orden por contra" runat="server" Visible="False" Font-Bold="True" />                            
                        <table id="tbOrden" runat="server" visible="False">
                            <tr runat="server">                                
                                <td style="width: 200px; text-align: center" runat="server">Cod. Cuenta Local<br />
                                    <cc1:TextBoxGrid ID="txtCodCuentaOp1" runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" OnTextChanged="txtCodCuentaOp_TextChanged" Visible="False"/>
                                    <asp:TextBox ID="txtIdParamOp1" runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="False" />
                                    <asp:TextBox ID="txtTipoParamOp1" runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="False" />
                                    <cc1:ButtonGrid ID="btnPlaCuenLocalOp1" runat="server" CssClass="btnListado" Height="26px" Text="..." OnClick="btnListadoPlan_Click" />
                                    <uc1:ListadoPlanCtas ID="ctlListadoPlanContable1" runat="server" />
                                </td>
                                <td style="width: 230px; text-align: center" runat="server">Nombre Cuenta Local<br />
                                    <cc1:TextBoxGrid ID="txtNomCuentaOp1" runat="server" CssClass="textbox" Enabled="False" Width="210px" Visible="False" />
                                </td>
                                <td style="width: 230px; text-align: center" runat="server">Código Cuenta NIIF<br />
                                    <cc1:TextBoxGrid ID="txtCodCuentaNIIFOp1" runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" OnTextChanged="txtCodCuentaNIIFOp_TextChanged" Visible="False" />
                                    <cc1:ButtonGrid ID="btnPlanCuenNIIFOp1" runat="server" CssClass="btnListado" Height="26px" Text="..." OnClick="btnListadoPlanNIIF_Click" Visible="False"/>
                                </td>
                                <td style="width: 250px; text-align: center" runat="server">Nombre Cuenta NIIF<br />
                                    <cc1:TextBoxGrid ID="txtNomCuentaNIIFOp1" runat="server" CssClass="textbox" Enabled="False" Width="210px" Visible="False" />
                                </td>
                            </tr>
                        </table>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate><br/>
                                <asp:Panel runat="server" HorizontalAlign="Center">
                                    <asp:Label ID="lblOpeTran" runat="server" Text="Operaciones por transacción" style="text-align:center" Font-Bold="true"/><br/><br/>
                                    <asp:Button ID="btnDetalle" runat="server" CssClass="btn8" OnClick="btnDetalle_Click"  OnClientClick="btnDetalle_Click" Text="+ Adicionar Detalle" />
                                    <asp:GridView ID="gvOperacion" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" OnRowDataBound="gvOperacion_RowDataBound" 
                                        OnRowDeleting="gvOperacion_RowDeleting"
                                        GridLines="Horizontal" ShowHeaderWhenEmpty="True" Width="60%">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Borrar" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                                <ItemStyle CssClass="gI" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cod. Cuenta">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtCuentaOpTran" runat="server" AutoPostBack="True" Style="font-size: xx-small; text-align: center" CssClass="textbox"
                                                        OnTextChanged="txtCuenta_TextChanged" BackColor="#F4F5FF" Width="100px" Text='<%# Bind("cuenta_ope1") %>' CommandArgument='1'>  
                                                    </cc1:TextBoxGrid>
                                                    <cc1:ButtonGrid ID="btnListadoPlan" CssClass="btnListado" runat="server" Text="..." OnClick="btnListadoPlan_Click" CommandArgument='1' /><br>
                                                    <uc1:ListadoPlanCtas ID="ctlListadoPlanContable" runat="server" />
                                                    <asp:TextBox ID="txtIdParamOp1" Text='<%# Bind("IdParamOp1") %>' runat="server" AutoPostBack="True" CssClass="textbox"
                                                                 Width="120px" Visible="false" />
                                                    <asp:TextBox ID="txtTipoParamOp1" Text='<%# Bind("TipoParamOp1") %>' runat="server" AutoPostBack="True" CssClass="textbox" 
                                                                Width="120px" Visible="false" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"/>
                                            </asp:TemplateField >
                                            <asp:TemplateField HeaderText="Tipo Transacción">
                                                <ItemTemplate>
                                                    <ctl:ctlListarCodigo ID="ddlTipoTran" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" Font-Size="X-Small" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" Font-Size="X-Small" />
                                    </asp:GridView>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tabClasificacion" runat="server" HeaderText="Datos">
                    <HeaderTemplate>Clasificación</HeaderTemplate>
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td style="text-align: center; width: 110px">
                                    <asp:Label ID="Label1" runat="server" Visible="False" Width="49px"></asp:Label>
                                </td>
                                <td style="text-align: center;">
                                    <asp:Label ID="lblClasAD" runat="server" Text="Garantía Admisible" Width="269px" CssClass="btn8" Font-Bold="True"></asp:Label>
                                </td>
                                <td style="text-align: center;">
                                    <asp:Label ID="lblClasNoAD" runat="server" Text="Garantía No Admisible" Width="276px" CssClass="btn8" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvClasificacion" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                    GridLines="Horizontal" ShowHeaderWhenEmpty="True" Width="90%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Categoria">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategoriaCl" runat="server" Text='<%# Bind("cod_categoria") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cod. Cuenta Con Libranza">
                                            <ItemTemplate>
                                                <cc1:TextBoxGrid ID="txtCuentaConLibAd" runat="server" AutoPostBack="True" Style="font-size: xx-small; text-align: center" OnTextChanged="txtCuenta_TextChanged"
                                                    BackColor="#F4F5FF" Width="80px" Text='<%# Bind("cuenta_clasif1") %>'  CssClass="textbox" CommandArgument='1'>  
                                                </cc1:TextBoxGrid>
                                                <cc1:ButtonGrid ID="btnListadoPlan1" CssClass="btnListado" runat="server" Text="..." OnClick="btnListadoPlanCl_Click" CommandArgument='1' /><br>
                                                <uc1:ListadoPlanCtas ID="ctlListadoPlanContable1" runat="server" />
                                                <asp:TextBox ID="txtIdParamCl1" Text='<%# Bind("IdParamCl1") %>' runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="false" />
                                                <asp:TextBox ID="txtTipoParamCl1" Text='<%# Bind("TipoParamCl1") %>' runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="false" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="180px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cod. Cuenta Sin Libranza">
                                            <ItemTemplate>
                                                <cc1:TextBoxGrid ID="txtCuentaSinLibAd" runat="server" AutoPostBack="True" Style="font-size: xx-small; text-align: center" OnTextChanged="txtCuenta_TextChanged"
                                                    BackColor="#F4F5FF" Width="80px" Text='<%# Bind("cuenta_clasif2") %>'  CssClass="textbox" CommandArgument='2'>  
                                                </cc1:TextBoxGrid>
                                                <cc1:ButtonGrid ID="btnListadoPlan2" CssClass="btnListado" runat="server" Text="..." OnClick="btnListadoPlanCl_Click" CommandArgument='2' /><br />
                                                <asp:TextBox ID="txtIdParamCl2" Text='<%# Bind("IdParamCl2") %>' runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="false" />
                                                <asp:TextBox ID="txtTipoParamCl2" Text='<%# Bind("TipoParamCl2") %>' runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="false" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="180px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cod. Cuenta Con Libranza">
                                            <ItemTemplate>
                                                <cc1:TextBoxGrid ID="txtCuentaConLibNoAd" runat="server" AutoPostBack="True" Style="font-size: xx-small; text-align: center" OnTextChanged="txtCuenta_TextChanged"
                                                    BackColor="#F4F5FF" Width="80px" Text='<%# Bind("cuenta_clasif3") %>' CssClass="textbox" CommandArgument='3'>  
                                                </cc1:TextBoxGrid>
                                                <cc1:ButtonGrid ID="btnListadoPlan3" CssClass="btnListado" runat="server" Text="..." OnClick="btnListadoPlanCl_Click" CommandArgument='3' /><br />
                                                <asp:TextBox ID="txtIdParamCl3" Text='<%# Bind("IdParamCl3") %>' runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="false" />
                                                <asp:TextBox ID="txtTipoParamCl3" Text='<%# Bind("TipoParamCl3") %>' runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="false" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="180px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cod. Cuenta Sin Libranza">
                                            <ItemTemplate>
                                                <cc1:TextBoxGrid ID="txtCuentaSinLibNoAd" runat="server" AutoPostBack="True" Style="font-size: xx-small; text-align: center" OnTextChanged="txtCuenta_TextChanged"
                                                    BackColor="#F4F5FF" Width="80px" Text='<%# Bind("cuenta_clasif4") %>' CssClass="textbox" CommandArgument='4'>  
                                                </cc1:TextBoxGrid>
                                                <cc1:ButtonGrid ID="btnListadoPlan4" CssClass="btnListado" runat="server"
                                                    Text="..." OnClick="btnListadoPlanCl_Click" CommandArgument='4' /><br />
                                                <asp:TextBox ID="txtIdParamCl4" Text='<%# Bind("IdParamCl4") %>' runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="false" />
                                                <asp:TextBox ID="txtTipoParamCl4" Text='<%# Bind("TipoParamCl4") %>' runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="false" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="180px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cod. Cuenta Orden">
                                            <ItemTemplate>
                                                <cc1:TextBoxGrid ID="txtCuentaOrden" runat="server" AutoPostBack="True" Style="font-size: xx-small; text-align: center" OnTextChanged="txtCuenta_TextChanged"
                                                    BackColor="#F4F5FF" Width="80px" Text='<%# Bind("cuenta_Ord") %>' CssClass="textbox" CommandArgument='5'>  
                                                </cc1:TextBoxGrid>
                                                <cc1:ButtonGrid ID="btnListadoPlan5" CssClass="btnListado" runat="server" Text="..." OnClick="btnListadoPlanCl_Click" CommandArgument='5' /><br />
                                                <asp:TextBox ID="txtIdParamCl5" Text='<%# Bind("IdParamCl5") %>' runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="false" />
                                                <asp:TextBox ID="txtTipoParamCl5" Text='<%# Bind("TipoParamCl5") %>' runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="false" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="180px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cod. Cuenta Orden por Contra">
                                            <ItemTemplate>
                                                <cc1:TextBoxGrid ID="txtCuentaOrdenContra" runat="server" AutoPostBack="True" Style="font-size: xx-small; text-align: center" OnTextChanged="txtCuenta_TextChanged"
                                                    BackColor="#F4F5FF" Width="80px" Text='<%# Bind("cuenta_OrdCon") %>' CssClass="textbox" CommandArgument='6'>  
                                                </cc1:TextBoxGrid>
                                                <cc1:ButtonGrid ID="btnListadoPlan6" CssClass="btnListado" runat="server" Text="..." OnClick="btnListadoPlanCl_Click" CommandArgument='6' /><br />
                                                <asp:TextBox ID="txtIdParamCl6" Text='<%# Bind("IdParamCl6") %>' runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="false" />
                                                <asp:TextBox ID="txtTipoParamCl6" Text='<%# Bind("TipoParamCl6") %>' runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="false" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="180px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" Font-Size="X-Small" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" Font-Size="X-Small" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tabCausacion" runat="server" HeaderText="Causación">
                    <HeaderTemplate>Causación</HeaderTemplate>
                    <ContentTemplate>
                        <asp:UpdatePanel runat="server" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <asp:GridView ID="gvCausacion" runat="server" AllowPaging="false" AutoGenerateColumns="False" HorizontalAlign="Center"
                                    GridLines="Horizontal" ShowHeaderWhenEmpty="True" Width="80%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Tipo de Cuenta">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCuentaCausa" runat="server" Text='<%# Bind("nom_tipo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Código de la Cuenta">
                                            <ItemTemplate>
                                                <cc1:TextBoxGrid ID="txtCodCuentaCausa" runat="server" AutoPostBack="True" Style="font-size: xx-small; text-align: center" OnTextChanged="txtCuenta_TextChanged"
                                                    BackColor="#F4F5FF" Width="120px" Text='<%# Bind("cod_cuenta") %>' CssClass="textbox" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>  
                                                </cc1:TextBoxGrid>
                                                <cc1:ButtonGrid ID="btnListadoPlan7" CssClass="btnListado" runat="server" Text="..." OnClick="btnListadoPlanCa_Click" CommandArgument='1' />
                                                <uc1:ListadoPlanCtas ID="ctlListadoPlanContable2" runat="server" />
                                                <asp:TextBox ID="txtIdParamCa1" Text='<%# Bind("IdParamCa1") %>' runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="false" />
                                                <asp:TextBox ID="txtTipoParamCa1" Text='<%# Bind("TipoParamCa1") %>' runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="false" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" Font-Size="X-Small" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" Font-Size="X-Small" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tabProvision" runat="server" HeaderText="Provisión">
                    <HeaderTemplate>Provisión</HeaderTemplate>
                    <ContentTemplate>
                        <asp:UpdatePanel runat="server" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <asp:GridView ID="gvProvision" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                    GridLines="Horizontal" ShowHeaderWhenEmpty="True" Width="85%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Categoria">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategoriaProv" runat="server" Text='<%# Bind("cod_categoria") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cod. Cuenta Garantia Admisible">
                                            <ItemTemplate>
                                                <cc1:TextBoxGrid ID="txtCuentaProvAd" runat="server" AutoPostBack="True" Style="font-size: xx-small; text-align: center" OnTextChanged="txtCuenta_TextChanged"
                                                    BackColor="#F4F5FF" Width="80px" Text='<%# Bind("cuenta_prov1") %>' CssClass="textbox" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>  
                                                </cc1:TextBoxGrid>
                                                <cc1:ButtonGrid ID="btnListadoPlan8" CssClass="btnListado" runat="server" Text="..." OnClick="btnListadoPlanProv_Click" CommandArgument='1' />
                                                <uc1:ListadoPlanCtas ID="ctlListadoPlanContable3" runat="server" />
                                                <asp:TextBox ID="txtIdParamPr1" Text='<%# Bind("IdParamPr1") %>' runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="false" />
                                                <asp:TextBox ID="txtTipoParamPr1" Text='<%# Bind("TipoParamPr1") %>' runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="false" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="180px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cod. Cuenta Garantia No Admisible">
                                            <ItemTemplate>
                                                <cc1:TextBoxGrid ID="txtCuentaProNoAd" runat="server" AutoPostBack="True" Style="font-size: xx-small; text-align: center" OnTextChanged="txtCuenta_TextChanged"
                                                    BackColor="#F4F5FF" Width="80px" Text='<%# Bind("cuenta_prov2") %>' CssClass="textbox" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>  
                                                </cc1:TextBoxGrid>
                                                <cc1:ButtonGrid ID="btnListadoPlan9" CssClass="btnListado" runat="server" Text="..." OnClick="btnListadoPlanProv_Click" CommandArgument='2' />
                                                <asp:TextBox ID="txtIdParamPr2" Text='<%# Bind("IdParamPr2") %>' runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="false" />
                                                <asp:TextBox ID="txtTipoParamPr2" Text='<%# Bind("TipoParamPr2") %>' runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="false" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="180px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cod. Cuenta Gasto">
                                            <ItemTemplate>
                                                <cc1:TextBoxGrid ID="txtCuentaGasto" runat="server" AutoPostBack="True" CssClass="textbox" Style="font-size: xx-small; text-align: center" OnTextChanged="txtCuenta_TextChanged"
                                                    BackColor="#F4F5FF" Width="80px" Text='<%# Bind("cuenta_prov3") %>'
                                                    CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>  
                                                </cc1:TextBoxGrid>
                                                <cc1:ButtonGrid ID="btnListadoPlan10" CssClass="btnListado" runat="server" Text="..." OnClick="btnListadoPlanProv_Click" CommandArgument='3' />
                                                <asp:TextBox ID="txtIdParamPr3" Text='<%# Bind("IdParamPr3") %>' runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="false" />
                                                <asp:TextBox ID="txtTipoParamPr3" Text='<%# Bind("TipoParamPr3") %>' runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="false" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="180px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cod. Cuenta Ingreso">
                                            <ItemTemplate>
                                                <cc1:TextBoxGrid ID="txtCuentaIngre" runat="server" AutoPostBack="True" CssClass="textbox" Style="font-size: xx-small; text-align: center" OnTextChanged="txtCuenta_TextChanged"
                                                    BackColor="#F4F5FF" Width="80px" Text='<%# Bind("cuenta_prov4") %>'
                                                    CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>  
                                                </cc1:TextBoxGrid>
                                                <cc1:ButtonGrid ID="btnListadoPlan11" CssClass="btnListado" runat="server" Text="..." OnClick="btnListadoPlanProv_Click" CommandArgument='4' />
                                                <asp:TextBox ID="txtIdParamPr4" Text='<%# Bind("IdParamPr4") %>' runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="false" />
                                                <asp:TextBox ID="txtTipoParamPr4" Text='<%# Bind("TipoParamPr4") %>' runat="server" AutoPostBack="True" CssClass="textbox" Width="120px" Visible="false" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="180px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" Font-Size="X-Small" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" Font-Size="X-Small" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </asp:Panel>

    </asp:Panel>
    <uc2:ctlmensaje ID="ctlmensaje" runat="server" />
</asp:Content>

