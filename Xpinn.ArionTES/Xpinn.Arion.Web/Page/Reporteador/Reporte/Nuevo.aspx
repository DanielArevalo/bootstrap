<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Reporte :." MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>  
    <%-- <script type="text/javascript" src="../../../Scripts/jquery-1.8.2.js"></script>   --%> 
    <script type="text/javascript" src="../../../Scripts/jquery.tablednd.js"></script>
    <style>
        #WindowLoad
        {
            position:fixed;
            top:0px;
            left:0px;
            z-index:3200;
            filter:alpha(opacity=65);
            -moz-opacity:65;
            opacity:0.65;
            background:#999;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#gvColumnas").tableDnD();
        })
    </script> 
   
    <script type="text/javascript">

        function click7(TextBox) {
        }

        function ActiveTabChanged(sender, e) {
            $('#<%=gvColumnas.ClientID%>').tableDnD();
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

        function pageLoad() {
            $('#<%=gvUsuarios.ClientID%>').gridviewScroll({
                width: 315,
                height: 200
            });
        }

    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvReporte" runat="server" ActiveViewIndex="0">

       <asp:View ID="vwTipoReporte" runat="server">
            <table cellpadding="5" cellspacing="0" style="width: 100%">                    
                <tr>
                    <td style="text-align: center;color: #FFFFFF; background-color: #0066FF">
                        TIPO DE REPORTE
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr style="text-align: center" >
                    <td>
                        Escoja el Tipo de Reporte
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align:center">
                        <asp:RadioButton ID="rbGenerador" runat="server" Text="Generador de Consultas" 
                            AutoPostBack="True" oncheckedchanged="rbGenerador_CheckedChanged" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align:center">
                        <asp:RadioButton ID="rbSql" runat="server" Text="Sentencia SQL" 
                            AutoPostBack="True" oncheckedchanged="rbSql_CheckedChanged" />
                        </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align:center">
                        <asp:RadioButton ID="rbCrystal" runat="server" Text="Crystal Reports" 
                            AutoPostBack="True" oncheckedchanged="rbCrystal_CheckedChanged" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align:center">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align:center">
                        <asp:ImageButton ID="imgAceptar" runat="server" 
                            ImageUrl="~/Images/btnAceptar.jpg" onclick="imgAceptar_Click" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align:center">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </asp:View>

        <asp:View ID="vwEncabezado" runat="server">
            <table id="tbEncabezado" border="0" cellpadding="0" cellspacing="0" width="70%">
                <tr>
                    <td style="text-align:left; width: 120px;">
                        Código<br/>
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="100px" 
                            Enabled="False" />
                    </td>
                    <td style="text-align:left">
                        Tipo de Reporte<br />
                        <asp:DropDownList ID="ddlTipoReporte" runat="server" CssClass="dropdown" Width="320px" Height="25px">
                            <asp:ListItem Value="0">Seleccione un Item</asp:ListItem>
                            <asp:ListItem Value="1">Generador de Consultas</asp:ListItem>
                            <asp:ListItem Value="2">Sentencia SQL</asp:ListItem>
                            <asp:ListItem Value="3">Crystal Reports</asp:ListItem>
                        </asp:DropDownList>
                        <asp:ImageButton ID="btnSQL" runat="server" ImageUrl="~/Images/sql.jpg" 
                            onclick="btnSQL_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="tdI" colspan="2" style="text-align:left">
                        Descripción<br />
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" 
                            Width="720px" Height="40px" TextMode="MultiLine" />
                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWEtxtDescripcion" runat="server"
                            TargetControlID="txtDescripcion"
                            WatermarkText="Digite la descripción del reporte"
                            WatermarkCssClass="watermarked" />
                        <br/>
                    </td>
                </tr>
                <tr>
                    <td class="tdI" colspan="2" style="text-align:left">
                        <asp:MultiView ID="mvDetalle" runat="server" ActiveViewIndex="0">
                            <asp:View ID="vwConsulta" runat="server">
                                <ajaxToolkit:TabContainer runat="server" ID="TabsConsulta" 
                                    OnClientActiveTabChanged="ActiveTabChanged" ActiveTabIndex="0" Width="99%" 
                                    style="margin-right: 5px" CssClass="CustomTabStyle" >
                                    <ajaxToolkit:TabPanel runat="server" ID="tabTablas" HeaderText="Tablas" >
                                        <HeaderTemplate>
                                            Tablas                                                
                                        </HeaderTemplate>             
                                        <ContentTemplate>
                                            <table style="width: 90%;">
                                                <tr>
                                                    <td style="text-align: left; width: 360px;">
                                                        TABLAS<br />
                                                        <br />
                                                        <asp:ListBox ID="lstRepTablas" runat="server" Height="290px" Width="320px" ></asp:ListBox>
                                                        <ajaxToolkit:ListSearchExtender ID="lseRepTablas" runat="server"
                                                            TargetControlID="lstRepTablas" PromptCssClass="ListSearchExtenderPrompt" 
                                                            IsSorted="True" Enabled="True">
                                                        </ajaxToolkit:ListSearchExtender>
                                                    </td>
                                                    <td style="text-align: left; vertical-align: top;">
                                                        TABLAS SELECCIONADAS<br />
                                                        <br />
                                                        <asp:ListBox ID="lstTablas" runat="server" Width="380px"></asp:ListBox>
                                                        <br /><br />
                                                        ENCADENAMIENTOS<br />
                                                        <div style="background-attachment: scroll">
                                                        <asp:GridView ID="gvEncadenamientos" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                                                            ForeColor="Black" GridLines="Vertical" Height="73px" 
                                                            OnRowCommand="gvEncadenamientos_RowCommand" OnRowDataBound="gvEncadenamientos_RowDataBound"
                                                            PageSize="20" ShowFooter="True" Style="font-size: xx-small" Width="380px">
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <Columns>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <cc1:ImageButtonGrid ID="btnEliminar" runat="server" CommandName="Deleted" CommandArgument='<%#Container.DataItemIndex %>' ImageUrl="~/Images/gr_elim.jpg"
                                                                            ToolTip="Eliminar" Width="16px" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:ImageButton ID="btnNuevo" runat="server" CommandName="Add" ImageUrl="~/Images/gr_nuevo.jpg"
                                                                            ToolTip="Adicionar" Width="16px" />
                                                                    </FooterTemplate>
                                                                    <ControlStyle Width="16px" />
                                                                    <FooterStyle Width="16px" />
                                                                    <HeaderStyle Width="16px" />
                                                                    <ItemStyle Width="16px" />
                                                                </asp:TemplateField>                                                
                                                                <asp:TemplateField HeaderText="idEncadenamiento" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblidEncadenamiento" runat="server" Text='<%# Bind("idEncadenamiento") %>' Width="50"></asp:Label>
                                                                    </ItemTemplate><ItemStyle Width="50px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Descripción">
                                                                    <ItemTemplate>
                                                                        <table id="tbEncadenamiento" border="0" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td style="width: 150px; text-align:left">
                                                                                    <asp:Label ID="lblTabla1" runat="server" Enabled="false" Text='<%# Bind("Tabla1") %>' style="font-size: x-small" />.
                                                                                    <asp:Label ID="lblColumna1" runat="server" Enabled="false" Text='<%# Bind("Columna1") %>' Width="75" style="font-size: x-small" />
                                                                                </td>
                                                                                <td style="width: 20px; text-align:left">
                                                                                    <asp:Label ID="lblOperador" runat="server" Enabled="false" Text=' = ' />
                                                                                </td>
                                                                                <td style="width: 150px; text-align:left">
                                                                                    <asp:Label ID="lblTabla2" runat="server" Enabled="false" Text='<%# Bind("Tabla2") %>' style="font-size: x-small" />.
                                                                                    <asp:Label ID="lblColumna2" runat="server" Enabled="false" Text='<%# Bind("Columna2") %>' Width="75" style="font-size: x-small" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <table id="tbEncadenamiento" border="0" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td style="width: 150px; text-align:left">
                                                                                    <asp:DropDownList ID="ddlTabla1" runat="server" Width="150px" 
                                                                                        CssClass="dropdown" AutoPostBack="True" 
                                                                                        onselectedindexchanged="ddlTabla1_SelectedIndexChanged" AppendDataBoundItems="True">
                                                                                        <asp:ListItem Value="0" Text=" "></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td style="width: 20px; text-align:center">                                                                                    
                                                                                </td>
                                                                                <td style="width: 150px; text-align:left">
                                                                                    <asp:DropDownList ID="ddlTabla2" runat="server" Width="150px" 
                                                                                        CssClass="dropdown" AutoPostBack="True" 
                                                                                        onselectedindexchanged="ddlTabla2_SelectedIndexChanged" AppendDataBoundItems="True">
                                                                                        <asp:ListItem Value="0" Text=" "></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </tr>
                                                                            <tr>
                                                                                <td style="width: 150px; text-align:left">
                                                                                    <asp:DropDownList ID="ddlColumna1" runat="server" Width="150px" 
                                                                                        CssClass="dropdown"></asp:DropDownList>
                                                                                </td>
                                                                                <td style="width: 20px; text-align:center"> 
                                                                                    <asp:Label ID="lblOperadorF" runat="server" Enabled="false" Text=' = ' />
                                                                                </td>
                                                                                <td style="width: 150px; text-align:left"> 
                                                                                    <asp:DropDownList ID="ddlColumna2" runat="server" Width="150px" 
                                                                                        CssClass="dropdown"></asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </FooterTemplate>
                                                                    <FooterStyle Width="320px" />
                                                                    <HeaderStyle Width="320px" />
                                                                    <ItemStyle Width="320px" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle CssClass="gridHeader" />
                                                            <HeaderStyle CssClass="gridHeader" HorizontalAlign="Center" />
                                                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                            <RowStyle CssClass="gridItem" />
                                                        </asp:GridView>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel runat="server" ID="tabCondiciones" HeaderText="Condiciones" >
                                        <HeaderTemplate>
                                            Condiciones                                                
                                        </HeaderTemplate>             
                                        <ContentTemplate>
                                            CONDICIONES<br />                                            
                                            <asp:GridView ID="gvCondiciones" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                                                ForeColor="Black" GridLines="Vertical" Height="63px" 
                                                OnRowCommand="gvCondiciones_RowCommand" OnRowDataBound="gvCondiciones_RowDataBound"
                                                PageSize="20" ShowFooter="True" Style="font-size: xx-small" Width="600px">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField ShowHeader="False">
                                                        <ItemTemplate>
                                                            <cc1:ImageButtonGrid ID="btnEliminar" runat="server" CommandName="Deleted" CommandArgument='<%#Container.DataItemIndex %>' ImageUrl="~/Images/gr_elim.jpg"
                                                                ToolTip="Eliminar" Width="16px" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:ImageButton ID="btnNuevo" runat="server" CommandName="Add" ImageUrl="~/Images/gr_nuevo.jpg"
                                                                ToolTip="Adicionar" Width="16px" />
                                                        </FooterTemplate>
                                                        <ControlStyle Width="16px" />
                                                        <FooterStyle Width="16px" />
                                                        <HeaderStyle Width="16px" />
                                                        <ItemStyle Width="16px" />
                                                    </asp:TemplateField>                                                
                                                    <asp:TemplateField HeaderText="idCondicion" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblidCondicion" runat="server" Text='<%# Bind("idcondicion") %>' Width="50"></asp:Label>
                                                        </ItemTemplate><ItemStyle Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Descripción">
                                                        <ItemTemplate>
                                                            <table id="tbCondicion" cellpadding="0" cellspacing="0" >
                                                                <tr>
                                                                    <td style="text-align:left; ">
                                                                        <asp:Label ID="lblandor" runat="server" Enabled="false" Text='<%# Bind("andor") %>' Width="25" style="font-size: x-small" />
                                                                    </td>
                                                                    <td style="text-align:left; ">
                                                                        <asp:Label ID="lblparentesisizq" runat="server" Enabled="false" Text='<%# Bind("parentesisizq") %>' Width="10" style="font-size: x-small" />
                                                                    </td>
                                                                    <td style="text-align:left; width: 213px;">
                                                                        <asp:Label ID="lblValor1" runat="server" Enabled="false" Text='<%# Bind("Valor1") %>' Width="120" style="font-size: x-small" />
                                                                        <asp:Label ID="lblTabla1" runat="server" Enabled="false" Text='<%# Bind("Tabla1") %>' style="font-size: x-small" />.
                                                                        <asp:Label ID="lblColumna1" runat="server" Enabled="false" Text='<%# Bind("Columna1") %>' Width="120" style="font-size: x-small" />
                                                                    </td>
                                                                    <td style="text-align:left">
                                                                        <asp:Label ID="lblOperador" runat="server" Enabled="false" Text='<%# Bind("operador") %>' Width="120" />
                                                                    </td>
                                                                    <td style="text-align:left">
                                                                        <asp:Label ID="lblTabla2" runat="server" Enabled="false" Text='<%# Bind("Tabla2") %>' style="font-size: x-small" />
                                                                        <asp:Label ID="lblColumna2" runat="server" Enabled="false" Text='<%# Bind("Columna2") %>' Width="120" style="font-size: x-small" />
                                                                        <asp:Label ID="lblValor2" runat="server" Enabled="false" Text='<%# Bind("Valor2") %>' Width="120" style="font-size: x-small" />
                                                                        <asp:Label ID="lblidlista" runat="server" Enabled="false" Text='<%# Bind("idlista") %>' Width="10" style="font-size: x-small" />
                                                                        <asp:Label ID="lblLista" runat="server" Enabled="false" Text='<%# Bind("nomlista") %>' Width="80" style="font-size: x-small" />
                                                                    </td>
                                                                    <td style="text-align:left">
                                                                        <asp:Label ID="lblparentesisder" runat="server" Enabled="false" Text='<%# Bind("parentesisder") %>' Width="10" style="font-size: x-small" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <table id="tbCondicion" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td style="width: 220px; text-align:left">
                                                                        &nbsp;</td>
                                                                    <td style="width: 150px; text-align:left">
                                                                        <asp:RadioButton ID="rbColumna0" runat="server" AutoPostBack="True" 
                                                                            Checked="True" oncheckedchanged="rbColumna0_CheckedChanged" 
                                                                            style="font-size: xx-small" Text="Columna" />
                                                                        <asp:RadioButton ID="rbValor0" runat="server" AutoPostBack="True" 
                                                                            Checked="False" oncheckedchanged="rbValor0_CheckedChanged" 
                                                                            style="font-size: xx-small" Text="Valor" />
                                                                    </td>
                                                                    <td style="width: 100px; text-align:left">
                                                                    </td>
                                                                    <td style="text-align:left" colspan="2">                                                                        
                                                                        <asp:RadioButton ID="rbColumna" runat="server" Text="Columna" AutoPostBack="True" 
                                                                            oncheckedchanged="rbColumna_CheckedChanged" style="font-size: xx-small" Checked="True" />
                                                                        <asp:RadioButton ID="rbValor" runat="server" Text="Valor" AutoPostBack="True" 
                                                                            oncheckedchanged="rbValor_CheckedChanged" style="font-size: xx-small"  Checked="False" />
                                                                        <asp:RadioButton ID="rbParametro" runat="server" Text="Paràmetro" AutoPostBack="True" 
                                                                            oncheckedchanged="rbParametro_CheckedChanged" style="font-size: xx-small"  Checked="False" />
                                                                        <asp:RadioButton ID="rbLista" runat="server" Text="Lista" AutoPostBack="True" 
                                                                            oncheckedchanged="rbLista_CheckedChanged" style="font-size: xx-small"  Checked="False" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 180px; text-align:left" rowspan="2">
                                                                        <asp:UpdatePanel ID="UpdatePanelIzq" runat="server" UpdateMode="Always">
                                                                            <ContentTemplate>
                                                                                <cc1:ImageButtonGrid ID="btnAndOr" runat="server" 
                                                                                    ImageUrl="~/Images/And.jpg" onclick="btnAndOr_Click" />&nbsp;
                                                                                <cc1:ImageButtonGrid ID="btnParentesisIzq" runat="server" 
                                                                                    ImageUrl="~/Images/ParentesisIzqIna.jpg" onclick="btnParentesisIzq_Click" />
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>
                                                                    <td style="width: 150px; text-align:left">
                                                                        <asp:DropDownList ID="ddlTablaCondicion1" runat="server" 
                                                                            AppendDataBoundItems="True" AutoPostBack="True" CssClass="dropdown" 
                                                                            onselectedindexchanged="ddlTablaCondicion1_SelectedIndexChanged" Width="150px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="width: 100px; text-align:left">
                                                                    </td>
                                                                    <td style="width: 180px; text-align:left">                                                            
                                                                        <asp:DropDownList ID="ddlTablaCondicion2" runat="server" Width="150px" CssClass="dropdown" AutoPostBack="True" 
                                                                            onselectedindexchanged="ddlTablaCondicion2_SelectedIndexChanged" AppendDataBoundItems="True">
                                                                        </asp:DropDownList>
                                                                        <asp:Label ID="lblTitParametro" runat="server" Enabled="false" Text='Descripciòn Paràmetro' style="font-size: xx-small" visible="false"/>
                                                                    </td>
                                                                    <td rowspan="2" style="width: 260px; text-align:left">
                                                                        <asp:UpdatePanel ID="UpdatePanelDer" runat="server" UpdateMode="Always">
                                                                            <ContentTemplate>
                                                                                <cc1:ImageButtonGrid ID="btnParentesisDer" runat="server" 
                                                                                    ImageUrl="~/Images/ParentesisDerIna.jpg" onclick="btnParentesisDer_Click" />
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 150px; text-align:left">
                                                                        <asp:TextBox ID="txtValor1" runat="server" CssClass="dropdown" Height="16" 
                                                                            Visible="False" Width="150px"></asp:TextBox>
                                                                        <asp:DropDownList ID="ddlColumna1" runat="server" Autopostback="true" CssClass="dropdown" 
                                                                            Width="150px" OnSelectedIndexChanged="ddlColumna1_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="width: 100px; text-align:left">
                                                                        <asp:DropDownList ID="ddlOperador" runat="server" Width="100px" CssClass="dropdown">
                                                                            <asp:ListItem Value="1" Text="Es igual a"></asp:ListItem>
                                                                            <asp:ListItem Value="2" Text="Es diferente a"></asp:ListItem>
                                                                            <asp:ListItem Value="3" Text="Es menor a"></asp:ListItem>
                                                                            <asp:ListItem Value="4" Text="Es menor o igual a"></asp:ListItem>
                                                                            <asp:ListItem Value="5" Text="Es mayor a"></asp:ListItem>
                                                                            <asp:ListItem Value="6" Text="Es mayor o igual a"></asp:ListItem>
                                                                            <asp:ListItem Value="7" Text="Conjunto"></asp:ListItem>
                                                                            <asp:ListItem Value="8" Text="No conjunto"></asp:ListItem>
                                                                            <asp:ListItem Value="9" Text="Nulo"></asp:ListItem>
                                                                            <asp:ListItem Value="10" Text="No nulo"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="width: 260px; text-align:left">
                                                                        <asp:TextBox ID="txtValor2" runat="server" Visible="False" CssClass="dropdown" Width="145px" Height="16"></asp:TextBox>
                                                                        <asp:DropDownList ID="ddlColumna2" runat="server" Width="150px" CssClass="dropdown"></asp:DropDownList>
                                                                        <asp:DropDownList ID="ddlLista" runat="server" Width="150px" CssClass="dropdown" Visible="False" ></asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </FooterTemplate>
                                                        <FooterStyle Width="600px" />
                                                        <HeaderStyle Width="400px" />
                                                        <ItemStyle Width="400px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="gridHeader" />
                                                <HeaderStyle CssClass="gridHeader" HorizontalAlign="Center" />
                                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                <RowStyle CssClass="gridItem" />
                                            </asp:GridView>
                                       </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel runat="server" ID="tabColumnas" HeaderText="Columnas" >
                                        <HeaderTemplate>
                                            Columnas                                                
                                        </HeaderTemplate>             
                                        <ContentTemplate>
                                            COLUMNAS<br />                                            
                                            <asp:GridView ID="gvColumnas" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                                                ForeColor="Black" GridLines="Vertical" Height="63px" 
                                                OnRowCommand="gvColumnas_RowCommand" OnRowDataBound="gvColumnas_RowDataBound"
                                                PageSize="20" ShowFooter="True" Style="font-size: xx-small" Width="920px" 
                                                onrowediting="gvColumnas_RowEditing" 
                                                onrowcancelingedit="gvColumnas_RowCancelingEdit" 
                                                onrowupdating="gvColumnas_RowUpdating">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField ShowHeader="False">
                                                        <ItemTemplate>
                                                            <cc1:ImageButtonGrid ID="btnEliminar" runat="server" CommandName="Deleted" CommandArgument='<%#Container.DataItemIndex %>' ImageUrl="~/Images/gr_elim.jpg"
                                                                ToolTip="Eliminar" Width="16px" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:ImageButton ID="btnNuevo" runat="server" CommandName="Add" ImageUrl="~/Images/gr_nuevo.jpg"
                                                                ToolTip="Adicionar" Width="16px" />
                                                        </FooterTemplate>
                                                        <ControlStyle Width="16px" />
                                                        <FooterStyle Width="16px" />
                                                        <HeaderStyle Width="16px" />
                                                        <ItemStyle Width="16px" />
                                                    </asp:TemplateField>                                                
                                                    <asp:TemplateField HeaderText="idCondicion" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblidColumna" runat="server" Text='<%# Bind("idcolumna") %>' Width="50"></asp:Label>
                                                        </ItemTemplate><ItemStyle Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Descripción">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="popupMenu" ID="PopupMenu" runat="server">
                                                                <div style="border:1px outset white;padding:2px;">
                                                                    <div><asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" Text="Edit" /></div>                                                                
                                                                </div>
                                                            </asp:Panel>
                                                            <ajaxToolkit:HoverMenuExtender ID="hme2" runat="Server"
                                                                HoverCssClass="popupHover" PopupControlID="PopupMenu" 
                                                                PopupPosition="Center" TargetControlID="panelEdit" PopDelay="25" />  
                                                            <asp:Panel ID="panelEdit" runat="server">                                                      
                                                                <asp:Label ID="lblOrden" runat="server" Enabled="false" Text='<%# Bind("orden") %>' Width="15" style="font-size: x-small" />
                                                                <asp:Label ID="lblTipo" runat="server" Enabled="false" Text='<%# Bind("tipo") %>' Width="50" style="font-size: x-small" />
                                                                <asp:Label ID="lblFormula" runat="server" Enabled="false" Text='<%# Bind("formula") %>' Width="50" style="font-size: x-small" />
                                                                <asp:Label ID="lblTabla" runat="server" Enabled="false" Text='<%# Bind("tabla") %>' Width="160" style="font-size: xx-small" />
                                                                <asp:Label ID="lblColumna" runat="server" Enabled="false" Text='<%# Bind("columna") %>' Width="210" style="font-size: xx-small" />
                                                                <asp:Label ID="lblTitulo" runat="server" Enabled="false" Text='<%# Bind("titulo") %>' Width="105" style="font-size: x-small"/>
                                                                <asp:Label ID="lblFormato" runat="server" Enabled="false" Text='<%# Bind("formato") %>' Width="100" style="font-size: x-small" />
                                                                <asp:Label ID="lblTipoDato" runat="server" Enabled="false" Text='<%# Bind("tipodato") %>' Width="75" style="font-size: x-small" Visible="False" />
                                                                <asp:Label ID="lblAlineacion" runat="server" Enabled="false" Text='<%# Bind("alineacion") %>' Width="50" style="font-size: x-small" />
                                                                <asp:Label ID="lblAncho" runat="server" Enabled="false" Text='<%# Bind("ancho") %>' Width="50" style="font-size: x-small; text-align:center" />
                                                                <asp:CheckBox ID="cbTotal" runat="server" Enabled="false" Checked='<%# Eval("total") %>' Width="25" style="font-size: x-small" />
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Panel ID="panelEdit" runat="server">
                                                            <table id="tbColumna" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td style="text-align:left" colspan="2">
                                                                        <asp:RadioButton ID="rbCOLColumnaE" runat="server" Text="Columna" 
                                                                            AutoPostBack="True" oncheckedchanged="rbCOLColumna_CheckedChanged" 
                                                                            style="font-size: xx-small" Checked="True" />
                                                                        <asp:RadioButton ID="rbCOLFormulaE" runat="server" Text="Formula" AutoPostBack="True" 
                                                                            oncheckedchanged="rbCOLFormula_CheckedChanged" style="font-size: xx-small" />
                                                                        <asp:RadioButton ID="rbCOLConstanteE" runat="server" Text="Constante" AutoPostBack="True" 
                                                                            oncheckedchanged="rbCOLConstante_CheckedChanged" style="font-size: xx-small" />
                                                                    </td>
                                                                    <td style="width: 150px; text-align:left" colspan="2">     
                                                                        <span style="font-size: x-small"><asp:Label ID="lblFormulaE" Text="Formula"  runat="server" Visible="False"></asp:Label></span>
                                                                        <asp:DropDownList ID="ddlCOLFormulaE" runat="server" 
                                                                            AppendDataBoundItems="True" CssClass="dropdown" Width="100px" Visible="False">
                                                                            <asp:ListItem>Count</asp:ListItem>
                                                                            <asp:ListItem>Sum</asp:ListItem>
                                                                            <asp:ListItem>Avg</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="width: 100px; text-align:left; font-size: x-small;" colspan="3">
                                                                            &nbsp;
                                                                            <asp:CheckBox ID="cbTotalE" runat="server" style="font-size: x-small" 
                                                                                Text="Totalizar" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 100px; text-align:left">
                                                                        <span style="font-size: x-small">Tabla</span><br />
                                                                        <asp:DropDownList ID="ddlCOLTablaE" runat="server" Width="100px" 
                                                                            CssClass="dropdown" DataSource="<%# ListaTablas() %>" DataTextField="nombre" DataValueField="nombre"   
                                                                            AppendDataBoundItems="True" SelectedValue='<%# Bind("tabla") %>' Enabled="False">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="width: 130px; text-align:left">
                                                                        <span style="font-size: x-small">Columna </span><br />
                                                                        <asp:DropDownList ID="ddlCOLColumnaE" runat="server" CssClass="dropdown" DataSource="<%# ListaColumnas() %>" DataTextField="nombre" DataValueField="nombre" 
                                                                            onselectedindexchanged="ddlCOLColumnaE_SelectedIndexChanged" SelectedValue='<%# Bind("columna") %>'
                                                                            Width="130px" AutoPostBack="True" >
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="txtCOLColumnaE" runat="server" CssClass="dropdown" Height="16px" Width="125px" Text='<%# Bind("columna") %>' Visible="false"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 100px; text-align:left">
                                                                        <span style="font-size: x-small">Tipo de Dato</span><br />
                                                                        <asp:TextBox ID="txtTipoDatoE" runat="server" CssClass="dropdown" Height="16px" Width="100px" Enabled="False" Text='<%# Bind("tipodato") %>'></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 150px; text-align:left; font-size: x-small;">
                                                                        <span style="font-size: x-small">Título</span><br />
                                                                        <asp:TextBox ID="txtTituloE" runat="server" CssClass="dropdown" Height="16px" Width="150px" Text='<%# Bind("titulo") %>'></asp:TextBox>
                                                                    </td>
                                                                    <td style="text-align:left; font-size: x-small;">
                                                                        Formato<br />
                                                                        <asp:DropDownList ID="ddlCOLFormatoE" runat="server" AppendDataBoundItems="True"  DataSource="<%# ListaFormatos() %>" DataTextField="descripcion" DataValueField="formato"
                                                                            AutoPostBack="True" CssClass="dropdown" Width="80px" Text='<%# Bind("formato") %>'>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="width: 70px; text-align:left; font-size: x-small;">
                                                                        Alineación<br />
                                                                        <asp:DropDownList ID="ddlCOLAlineacionE" runat="server" AppendDataBoundItems="True" 
                                                                            AutoPostBack="True" CssClass="dropdown" Width="70px" SelectedValue='<%# Bind("alineacion") %>'>
                                                                            <asp:ListItem Value="Izquierda" Text="Izquierda"></asp:ListItem>
                                                                            <asp:ListItem Value="Centro" Text="Centro"></asp:ListItem>
                                                                            <asp:ListItem Value="Derecha" Text="Derecha"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="width: 30px; text-align:left; font-size: x-small;">                                                                    
                                                                        Ancho<br />
                                                                        <asp:TextBox ID="txtAnchoE" runat="server" CssClass="dropdown" Height="16px" 
                                                                            Width="30px" Text='<%# Bind("ancho") %>'></asp:TextBox>
                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtAnchoE_FilteredTextBoxExtender" 
                                                                            runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtAnchoE" ValidChars=".">
                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>                                                                
                                                            </table>
                                                            </asp:Panel>
                                                            <ajaxToolkit:HoverMenuExtender ID="hme1" runat="Server"
                                                                TargetControlID="panelEdit" PopupControlID="PopupMenu"
                                                                HoverCssClass="popupHover" PopupPosition="Right" />
                                                             <asp:Panel ID="PopupMenu" runat="server" CssClass="popupMenu" Width="80">
                                                                <div style="border:1px outset white">
                                                                    <asp:LinkButton ID="LinkButton1" runat="server"
                                                                        CausesValidation="True" CommandName="Update" Text="Update" />
                                                                    <br />
                                                                    <asp:LinkButton ID="LinkButton2" runat="server"
                                                                        CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                                                                </div>
                                                            </asp:Panel>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <table id="tbColumna" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td style="text-align:left" colspan="2">
                                                                        <asp:RadioButton ID="rbCOLColumna" runat="server" Text="Columna" 
                                                                            AutoPostBack="True" oncheckedchanged="rbCOLColumna_CheckedChanged" 
                                                                            style="font-size: xx-small" Checked="True" />
                                                                        <asp:RadioButton ID="rbCOLFormula" runat="server" Text="Formula" AutoPostBack="True" 
                                                                            oncheckedchanged="rbCOLFormula_CheckedChanged" style="font-size: xx-small" />
                                                                        <asp:RadioButton ID="rbCOLConstante" runat="server" Text="Constante" AutoPostBack="True" 
                                                                            oncheckedchanged="rbCOLConstante_CheckedChanged" style="font-size: xx-small" />
                                                                    </td>
                                                                    <td style="width: 150px; text-align:left" colspan="2">     
                                                                        <span style="font-size: x-small"><asp:Label ID="lblFormula" Text="Formula" Visible="False" runat="server"></asp:Label></span>
                                                                        <asp:DropDownList ID="ddlCOLFormula" runat="server" AppendDataBoundItems="True" 
                                                                            CssClass="dropdown" Width="100px" Visible="False">
                                                                            <asp:ListItem>Count</asp:ListItem>
                                                                            <asp:ListItem>Sum</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="width: 100px; text-align:left; font-size: x-small;" colspan="3">
                                                                            &nbsp;
                                                                            <asp:CheckBox ID="cbTotal" runat="server" style="font-size: x-small" 
                                                                                Text="Totalizar" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 100px; text-align:left">
                                                                        <span style="font-size: x-small">Tabla</span><br />
                                                                        <asp:DropDownList ID="ddlCOLTabla" runat="server" Width="100px" 
                                                                            CssClass="dropdown" AutoPostBack="True" 
                                                                            onselectedindexchanged="ddlCOLTabla_SelectedIndexChanged" 
                                                                            AppendDataBoundItems="True">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="width: 130px; text-align:left">
                                                                        <span style="font-size: x-small">Columna </span><br />
                                                                        <asp:DropDownList ID="ddlCOLColumna" runat="server" CssClass="dropdown" 
                                                                            onselectedindexchanged="ddlCOLColumna_SelectedIndexChanged" 
                                                                            Width="130px" AutoPostBack="True">
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="txtCOLColumna" runat="server" CssClass="dropdown" Height="16px" Width="125px" Text='<%# Bind("columna") %>' Visible="false"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 100px; text-align:left">
                                                                        <span style="font-size: x-small">Tipo de Dato</span><br />
                                                                        <asp:TextBox ID="txtTipoDato" runat="server" CssClass="dropdown" Height="16px" Width="100px" Enabled="False"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 150px; text-align:left; font-size: x-small;">
                                                                        <span style="font-size: x-small">Título</span><br />
                                                                        <asp:TextBox ID="txtTitulo" runat="server" CssClass="dropdown" Height="16px" Width="150px"></asp:TextBox>
                                                                    </td>
                                                                    <td style="text-align:left; font-size: x-small;">
                                                                        Formato<br />
                                                                        <asp:DropDownList ID="ddlCOLFormato" runat="server" AppendDataBoundItems="True" 
                                                                            AutoPostBack="True" CssClass="dropdown" Width="80px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="width: 70px; text-align:left; font-size: x-small;">
                                                                        Alineación<br />
                                                                        <asp:DropDownList ID="ddlCOLAlineacion" runat="server" AppendDataBoundItems="True" 
                                                                            AutoPostBack="True" CssClass="dropdown" Width="70px">
                                                                            <asp:ListItem Value="1" Text="Izquierda"></asp:ListItem>
                                                                            <asp:ListItem Value="2" Text="Centro"></asp:ListItem>
                                                                            <asp:ListItem Value="3" Text="Derecha"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="width: 30px; text-align:left; font-size: x-small;">                                                                    
                                                                        Ancho<br />
                                                                        <asp:TextBox ID="txtAncho" runat="server" CssClass="dropdown" Height="16px" 
                                                                            Width="30px"></asp:TextBox>
                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtAncho_FilteredTextBoxExtender" 
                                                                            runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtAncho" ValidChars=".">
                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>                                                                
                                                            </table>
                                                        </FooterTemplate>
                                                        <FooterStyle Width="400px" />
                                                        <HeaderStyle Width="400px" />
                                                        <ItemStyle Width="400px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="False">
                                                        <ItemTemplate>
                                                            <cc1:ImageButtonGrid ID="btnSubir" runat="server" CommandName="Up" CommandArgument='<%#Container.DataItemIndex %>' ImageUrl="~/Images/up.gif"
                                                                ToolTip="Subir" Width="16px" Height="8" />
                                                            <cc1:ImageButtonGrid ID="btnBajar" runat="server" CommandName="Down" CommandArgument='<%#Container.DataItemIndex %>' ImageUrl="~/Images/down.gif"
                                                                ToolTip="Bajar" Width="16px" Height="8" />
                                                        </ItemTemplate>
                                                        <ControlStyle Width="16px" />
                                                        <FooterStyle Width="16px" />
                                                        <HeaderStyle Width="16px" />
                                                        <ItemStyle Width="16px" />
                                                    </asp:TemplateField>    
                                                </Columns>
                                                <FooterStyle CssClass="gridHeader" />
                                                <HeaderStyle CssClass="gridHeader" HorizontalAlign="Center" />
                                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                <RowStyle CssClass="gridItem" />
                                            </asp:GridView>                                            
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel runat="server" ID="tabOrden" HeaderText="Orden y Grupos" >
                                        <HeaderTemplate>
                                            Orden y Grupos                                                
                                        </HeaderTemplate>             
                                        <ContentTemplate>
                                            <table id="tbOrden" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 150px; text-align:left">
                                                        ORDEN
                                                        <br />
                                                        <asp:GridView ID="gvOrden" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                                                            ForeColor="Black" GridLines="Vertical" Height="63px" 
                                                            OnRowCommand="gvOrden_RowCommand" OnRowDataBound="gvOrden_RowDataBound"
                                                            PageSize="20" ShowFooter="True" Style="font-size: xx-small" Width="350px">
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <Columns>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <cc1:ImageButtonGrid ID="btnEliminar" runat="server" CommandName="Deleted" CommandArgument='<%#Container.DataItemIndex %>' ImageUrl="~/Images/gr_elim.jpg"
                                                                            ToolTip="Eliminar" Width="16px" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:ImageButton ID="btnNuevo" runat="server" CommandName="Add" ImageUrl="~/Images/gr_nuevo.jpg"
                                                                            ToolTip="Adicionar" Width="16px" />
                                                                    </FooterTemplate>
                                                                    <ControlStyle Width="16px" />
                                                                    <FooterStyle Width="16px" />
                                                                    <HeaderStyle Width="16px" />
                                                                    <ItemStyle Width="16px" />
                                                                </asp:TemplateField>                                                
                                                                <asp:TemplateField HeaderText="idOrden" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblidOrden" runat="server" Text='<%# Bind("idorden") %>' Width="50"></asp:Label>
                                                                    </ItemTemplate><ItemStyle Width="50px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Descripción">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTabla" runat="server" Enabled="false" Text='<%# Bind("tabla") %>' style="font-size: x-small" />.
                                                                        <asp:Label ID="lblColumna" runat="server" Enabled="false" Text='<%# Bind("columna") %>' Width="150" style="font-size: x-small" />
                                                                        <asp:Label ID="lblOrden" runat="server" Enabled="false" Text='<%# Bind("orden") %>' Width="75" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <table id="tbOrden" border="0" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td style="width: 150px; text-align:left">
                                                                                    <asp:DropDownList ID="ddlORDTabla" runat="server" Width="120px" CssClass="dropdown" AutoPostBack="True" 
                                                                                        onselectedindexchanged="ddlORDTabla_SelectedIndexChanged" AppendDataBoundItems="True">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td style="width: 100px; text-align:left">
                                                                                    <asp:DropDownList ID="ddlORDColumna" runat="server" Width="120px" CssClass="dropdown"></asp:DropDownList>
                                                                                </td>
                                                                                <td style="width: 100px; text-align:left">
                                                                                    <asp:DropDownList ID="ddlOrden" runat="server" Width="80px" CssClass="dropdown">
                                                                                        <asp:ListItem Value="1" Text="Ascendente"></asp:ListItem>
                                                                                        <asp:ListItem Value="2" Text="Descendente"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </FooterTemplate>
                                                                    <FooterStyle Width="400px" />
                                                                    <HeaderStyle Width="400px" />
                                                                    <ItemStyle Width="400px" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle CssClass="gridHeader" />
                                                            <HeaderStyle CssClass="gridHeader" HorizontalAlign="Center" />
                                                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                            <RowStyle CssClass="gridItem" />
                                                        </asp:GridView>
                                                    </td>
                                                    <td style="width: 10px; text-align:left">
                                                    </td>
                                                    <td style="width: 150px; text-align:left">
                                                        GRUPOS
                                                        <br />
                                                        <asp:GridView ID="gvGrupo" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                                                            ForeColor="Black" GridLines="Vertical" Height="63px" 
                                                            OnRowCommand="gvGrupo_RowCommand" OnRowDataBound="gvGrupo_RowDataBound"
                                                            PageSize="20" ShowFooter="True" Style="font-size: xx-small" Width="350px">
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <Columns>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <cc1:ImageButtonGrid ID="btnEliminar" runat="server" CommandName="Deleted" CommandArgument='<%#Container.DataItemIndex %>' ImageUrl="~/Images/gr_elim.jpg"
                                                                            ToolTip="Eliminar" Width="16px" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:ImageButton ID="btnNuevo" runat="server" CommandName="Add" ImageUrl="~/Images/gr_nuevo.jpg"
                                                                            ToolTip="Adicionar" Width="16px" />
                                                                    </FooterTemplate>
                                                                    <ControlStyle Width="16px" />
                                                                    <FooterStyle Width="16px" />
                                                                    <HeaderStyle Width="16px" />
                                                                    <ItemStyle Width="16px" />
                                                                </asp:TemplateField>                                                
                                                                <asp:TemplateField HeaderText="idGrupo" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblidGrupo" runat="server" Text='<%# Bind("idgrupo") %>' Width="50"></asp:Label>
                                                                    </ItemTemplate><ItemStyle Width="50px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Descripción">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTabla" runat="server" Enabled="false" Text='<%# Bind("tabla") %>' style="font-size: x-small" />.
                                                                        <asp:Label ID="lblColumna" runat="server" Enabled="false" Text='<%# Bind("columna") %>' Width="150" style="font-size: x-small" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <table id="tbGrupo" border="0" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td style="width: 150px; text-align:left">
                                                                                    <asp:DropDownList ID="ddlGRUTabla" runat="server" Width="120px" CssClass="dropdown" AutoPostBack="True" 
                                                                                        onselectedindexchanged="ddlGRUTabla_SelectedIndexChanged" AppendDataBoundItems="True">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td style="width: 100px; text-align:left">
                                                                                    <asp:DropDownList ID="ddlGRUColumna" runat="server" Width="120px" CssClass="dropdown"></asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </FooterTemplate>
                                                                    <FooterStyle Width="400px" />
                                                                    <HeaderStyle Width="400px" />
                                                                    <ItemStyle Width="400px" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle CssClass="gridHeader" />
                                                            <HeaderStyle CssClass="gridHeader" HorizontalAlign="Center" />
                                                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                            <RowStyle CssClass="gridItem" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel runat="server" ID="tabTitulos" HeaderText="Formato" >
                                        <HeaderTemplate>
                                            Formato                                                
                                        </HeaderTemplate>             
                                        <ContentTemplate>
                                            TITULOS
                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="font-size: x-small">
                                                        <asp:CheckBox ID="cbNumerar" runat="server" Text="Numerar Columnas" />
                                                    </td>
                                                    <td style="font-size: x-small">                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: x-small" colspan="2">
                                                        <strong>Encabezado</strong></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="txtEncabezado" runat="server" Height="50px" TextMode="MultiLine" 
                                                            Width="610px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: x-small" colspan="2">
                                                        <strong>Pie de Página</strong></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="txtPiePagina" runat="server" Height="50px" TextMode="MultiLine" 
                                                            Width="610px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <strong><span style="font-size: x-small">Plantillas</span></strong><br />                                                        
                                                        <br />
                                                        <asp:GridView ID="gvPlantillas" runat="server" AutoGenerateColumns="False" 
                                                            DataKeyNames="idplantilla" Style="font-size: x-small"
                                                            OnRowCommand="gvPlantillas_RowCommand" 
                                                            OnRowDataBound="gvPlantillas_RowDataBound" ShowFooter="True">
                                                            <Columns>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <cc1:ImageButtonGrid ID="btnEliminar" runat="server" CommandName="Deleted" CommandArgument='<%#Container.DataItemIndex %>' ImageUrl="~/Images/gr_elim.jpg"
                                                                            ToolTip="Eliminar" Width="16px" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:ImageButton ID="btnNuevo" runat="server" CommandName="Add" ImageUrl="~/Images/gr_nuevo.jpg"
                                                                            ToolTip="Adicionar" Width="16px" />
                                                                    </FooterTemplate>
                                                                    <ControlStyle Width="16px" />
                                                                    <FooterStyle Width="16px" />
                                                                    <HeaderStyle Width="16px" />
                                                                    <ItemStyle Width="16px" />
                                                                </asp:TemplateField>   
                                                                <asp:TemplateField HeaderText="ID.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblidPlantilla" runat="server" Text='<%# Bind("idplantilla") %>' Width="30"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="30px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Descripción">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# Bind("descripcion") %>' Width="200"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="txtDescripcion" runat="server" Width="200px" CssClass="dropdown" Height="16px" Font-Size="XX-Small"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <HeaderStyle Width="200px" />
                                                                    <ItemStyle Font-Size="X-Small" Width="200px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Archivo">
                                                                    <ItemTemplate>                                                                        
                                                                        <asp:HyperLink ID="hlArchivo" runat="server" Text='<%# Bind("archivo") %>' Width="400">Descargar</asp:HyperLink>
                                                                        <asp:Label ID="lblArchivo" runat="server" Text='<%# Bind("archivo") %>' Width="400" Visible="False"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>                                                                        
                                                                        <asp:FileUpload ID="fuArchivo" runat="server" Font-Size="XX-Small" />
                                                                    </FooterTemplate>
                                                                    <HeaderStyle Width="550px" />
                                                                    <ItemStyle Font-Size="X-Small" Width="400px" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="gridHeader" HorizontalAlign="Center" />
                                                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                            <RowStyle CssClass="gridItem" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel runat="server" ID="tabPermisos" HeaderText="Usuarios" >
                                        <HeaderTemplate>
                                            Usuarios                                                
                                        </HeaderTemplate>             
                                        <ContentTemplate>
                                            USUARIOS<br />
                                            <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                            <asp:Panel ID="panelUsuarios" runat="server" ScrollBars="Vertical" Height="300px">
                                            <asp:GridView ID="gvUsuariosRep" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                                ForeColor="Black" GridLines="Vertical" 
                                                Style="font-size: xx-small" Width="700px" EnablePersistedSelection="True">
                                                <AlternatingRowStyle BackColor="White" />                                                
                                                <Columns>
                                                    <asp:BoundField DataField="codusuario" HeaderText="Cod.Usuario" >
                                                        <ItemStyle HorizontalAlign="Left" Width="40px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" >
                                                        <ItemStyle HorizontalAlign="Left" Width="520px" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Autorizar">
                                                        <HeaderTemplate>
                                                            <cc1:CheckBoxGrid ID="chkAutorizarH1" runat="server" Checked='false' CommandArgument='<%#Container.DataItemIndex %>'/>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>                                                                                                                     
                                                            <cc1:CheckBoxGrid ID="chkAutorizar1" runat="server" Checked='<%# Eval("Autorizar") %>' CommandArgument='<%#Container.DataItemIndex %>'/>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="gridHeader" />
                                                <HeaderStyle CssClass="gridHeader" HorizontalAlign="Center" />
                                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                <RowStyle CssClass="gridItem" />
                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                                <SortedDescendingHeaderStyle BackColor="#575357" />
                                            </asp:GridView>
                                            </asp:Panel>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel runat="server" ID="TabPerfiles" HeaderText="Perfiles" >
                                        <HeaderTemplate>
                                            Perfiles                                                
                                        </HeaderTemplate>             
                                        <ContentTemplate>
                                            PERFILES<br />
                                            <asp:UpdatePanel ID="UpdatePanelPerfil" runat="server">
                                            <ContentTemplate>
                                            <asp:Panel ID="panel1" runat="server" ScrollBars="Vertical" Height="300px">
                                            <asp:GridView ID="gvPerfiles" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                                ForeColor="Black" GridLines="Vertical" 
                                                Style="font-size: xx-small" Width="700px" EnablePersistedSelection="True">
                                                <AlternatingRowStyle BackColor="White" />                                                
                                                <Columns>
                                                    <asp:BoundField DataField="codperfil" HeaderText="Cod.Perfil" >
                                                        <ItemStyle HorizontalAlign="Left" Width="40px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nombreperfil" HeaderText="Nombre" >
                                                        <ItemStyle HorizontalAlign="Left" Width="520px" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Autorizar">
                                                        <HeaderTemplate>
                                                            <cc1:CheckBoxGrid ID="chkAutorizarH2" runat="server" Checked='false' CommandArgument='<%#Container.DataItemIndex %>'/>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>                                                                                                                     
                                                            <cc1:CheckBoxGrid ID="chkAutorizar2" runat="server" Checked='<%# Eval("Autorizar") %>' CommandArgument='<%#Container.DataItemIndex %>'/>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="gridHeader" />
                                                <HeaderStyle CssClass="gridHeader" HorizontalAlign="Center" />
                                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                <RowStyle CssClass="gridItem" />
                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                                <SortedDescendingHeaderStyle BackColor="#575357" />
                                            </asp:GridView>
                                            </asp:Panel>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                </ajaxToolkit:TabContainer>
                            </asp:View>
                            <asp:View ID="vwSentencia" runat="server">
                                <asp:Label ID="lblSubTit" runat="server" Text="Sentencia SQL"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtSql" runat="server" CssClass="textbox" MaxLength="128" 
                                    Width="720px" Height="55px" TextMode="MultiLine" />
                                <ajaxToolkit:TextBoxWatermarkExtender ID="TBEWtxtSql" runat="server"
                                    TargetControlID="txtSql"
                                    WatermarkText="Digite la sentencia SELECT del reporte"
                                    WatermarkCssClass="watermarked" />
                            </asp:View>
                            <asp:View ID="vwCrystal" runat="server">
                                <asp:Label ID="lblURlCrystal" runat="server" Text='URL Reporte Crystal Report' 
                                    Width="308px"></asp:Label><br />
                                <asp:TextBox ID="txtURLCrystal" runat="server" CssClass="textbox" Enabled="False" 
                                    Width="720px" />
                            </asp:View>
                        </asp:MultiView>
                    </td>
                </tr>
            </table>
            <hr />            
            <table id="tbParametros" border="0" cellpadding="5" cellspacing="0" width="100%">
                <tr>
                    <td class="tdI" style="text-align:left; width: 408px; vertical-align:top">
                        <strong><asp:Label ID="lblTitParametros" runat="server" Text="Parámetros del Reporte"></asp:Label></strong><br />
                        <asp:GridView ID="gvParametros" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="idParametro"
                            ForeColor="Black" GridLines="Vertical" Height="10px" OnRowCancelingEdit="gvParametros_RowCancelingEdit"
                            OnRowCommand="gvParametros_RowCommand" OnRowDataBound="gvParametros_RowDataBound"
                            OnRowDeleting="gvParametros_RowDeleting" OnRowEditing="gvParametros_RowEditing"
                            OnRowUpdating="gvParametros_RowUpdating" PageSize="5" ShowFooter="True" Style="font-size: xx-small">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField ShowHeader="False">
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="btnActualizar" runat="server" CommandName="Update" ImageUrl="~/Images/gr_guardar.jpg"
                                            ToolTip="Actualizar" Width="16px" />
                                        <asp:ImageButton ID="btnCancelar" runat="server" CommandName="Cancel" ImageUrl="~/Images/gr_cancelar.jpg"
                                            ToolTip="Cancelar" Width="16px" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:ImageButton ID="btnNuevo" runat="server" CausesValidation="False" CommandName="AddNew"
                                            ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" />
                                    </FooterTemplate><ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                            ToolTip="Editar" Width="16px" />
                                    </ItemTemplate><ItemStyle Width="16px" />
                                </asp:TemplateField>
                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" 
                                    ShowDeleteButton="True"  ><ItemStyle Width="16px" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="idParametro" Visible="false">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtidParametro" runat="server" Style="margin-top: 0px" Width="50"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblidParametro" runat="server" Text='<%# Bind("idParametro") %>' Width="50"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripción">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDescripcion" runat="server" Text='<%# Bind("Descripcion") %>' Style="margin-top: 0px" Width="150"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtDescripcionF" runat="server" Width="150" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescripcion" runat="server" Enabled="false" Text='<%# Bind("Descripcion") %>' Width="150" />
                                    </ItemTemplate>
                                    <FooterStyle Width="150px" />
                                    <HeaderStyle Width="150px" />
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tipo de Dato">
                                    <EditItemTemplate>
                                        <cc1:DropDownListGrid ID="ddlTipo" runat="server" CssClass="dropdown" AutoPostBack="true" 
                                            SelectedValue='<%# Bind("NOMTIPO") %>' Style="text-align: center" Width="80" 
                                            onselectedindexchanged="ddlTipo_SelectedIndexChanged" CommandArgument='<%#Container.DataItemIndex %>' >
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Texto</asp:ListItem>
                                            <asp:ListItem>Fecha</asp:ListItem>
                                            <asp:ListItem>Numero</asp:ListItem>
                                            <asp:ListItem>Lista</asp:ListItem>
                                        </cc1:DropDownListGrid>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlTipoF" runat="server" CssClass="dropdown" AutoPostBack="true"  Width="80"
                                            onselectedindexchanged="ddlTipoF_SelectedIndexChanged">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Texto</asp:ListItem>
                                            <asp:ListItem>Fecha</asp:ListItem>
                                            <asp:ListItem>Numero</asp:ListItem>
                                            <asp:ListItem>Lista</asp:ListItem>
                                        </asp:DropDownList></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("NOMTIPO") %>' Width="80"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlListaPar" runat="server" Width="150px" 
                                            CssClass="dropdown" Visible="False"  ></asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlListaParF" runat="server" Width="150px" 
                                            CssClass="dropdown" Visible="False"  ></asp:DropDownList>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblListaPar" runat="server" Text='<%# Bind("IDLISTA") %>' Width="80"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="gridHeader" />
                            <HeaderStyle CssClass="gridHeader" HorizontalAlign="Center" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                    </td>
                    <td class="tdI" style="text-align:left; vertical-align:top">
                        <strong><asp:Label ID="lblTitUsuarios" runat="server" Text="Usuarios que Pueden Generar el Reporte"></asp:Label></strong><br />                        
                        <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="codusuario"
                            ForeColor="Black" GridLines="Vertical" 
                            ageSize="1000" Style="font-size: xx-small"   >
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="codusuario" HeaderText="Cod.Usuario" />
                                <asp:BoundField DataField="nombre" HeaderText="Nombre" ><ItemStyle HorizontalAlign="Left" Width="180px" /></asp:BoundField>
                                <asp:TemplateField HeaderText="Autorizar">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAutorizarH" runat="server" Checked='false' />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAutorizar" runat="server" Checked='<%# Eval("Autorizar") %>'  />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="gridHeader" />
                            <HeaderStyle CssClass="gridHeader" HorizontalAlign="Center" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
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
            <hr />
            <table id="tbUsuario" border="0" cellpadding="5" cellspacing="0" width="100%">
                <tr>
                    <td class="tdI" style="width: 151px; height: 95px;">
                        Fecha Elaboración<br />
                        <uc1:fecha ID="txtFechaElabora" runat="server" Enabled="False"></uc1:fecha>
                    </td>
                    <td class="tdI" style="text-align:left; height: 95px;">
                        Elaborado Por<br />
                        <asp:TextBox ID="txtElaborador" runat="server" CssClass="textbox" 
                            Width="435px" Enabled="False" />
                    </td>
                    <td class="tdD" style="height: 95px">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdI" colspan="3" style="text-align:center">
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
                                Text="Reporte Grabado Correctamente" style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Button ID="btnFinal" runat="server" Text="Finalizar" 
                                onclick="btnFinalClick" />&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>

    </asp:MultiView>

    <asp:HiddenField ID="HiddenField2" runat="server" />

    <ajaxToolkit:ModalPopupExtender ID="mpeGrabar" runat="server" 
        PopupControlID="panelActividadReg" TargetControlID="HiddenField2"
        BackgroundCssClass="backgroundColor" >
    </ajaxToolkit:ModalPopupExtender>
   
    <asp:Panel ID="panelActividadReg" runat="server" BackColor="White" Style="text-align: right" BorderWidth="1px" Width="500px" >
        <div id="Div1" style="width: 500px">
            <table style="width: 100%;">
                <tr>
                    <td colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align:center">
                        Esta Seguro de Grabar el Reporte ?
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="text-align:center">
                        <asp:Button ID="btnContinuar" runat="server" Text="Continuar"
                            CssClass="btn8"  Width="182px" onclick="btnContinuar_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnParar" runat="server" Text="Cancelar" CssClass="btn8" 
                            Width="182px" onclick="btnParar_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>       
        </div>
    </asp:Panel>

    <asp:HiddenField ID="HiddenField1" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mpeLinkSQL" runat="server" TargetControlID="HiddenField1"
    PopupControlID="panelSQL" BackgroundCssClass="backgroundColor" 
    CancelControlID="CancelButtonSQL" PopupDragHandleControlID="panelTitSQL" />
    <ajaxToolkit:DragPanelExtender ID="dpeSQL" runat="server" TargetControlID="panelSQL" DragHandleID="panelTitSQL" />
    <div style="height: 100%; width: 100%; float: left; padding: 1px;" >
        <asp:Panel ID="panelSQL" runat="server" Width="600px" CssClass="modalPopup"
            Style="z-index: 20;" Height="16px">
            <asp:Panel ID="panelTitSQL" runat="server" Width="100%" Height="20px"
                BorderStyle="Solid" BorderWidth="2px" BorderColor="black">
                <div class="dragMe">SQL</div>
            </asp:Panel>
            <asp:Panel  ID="panelDetSQL" runat="server" Width="100%" Height="185px" 
                BorderWidth="2px" BorderColor="black" BorderStyle="Solid" >
                <div>
                    <asp:TextBox ID="txtMostrarSQL" runat="server" TextMode="MultiLine" 
                        ReadOnly="True" Height="150px" Width="98%"></asp:TextBox>
                </div>
                <p style="margin: 0px; padding: 0px; text-align: center; line-height: 15px; height: 15px;">
                    <asp:Button ID="OkButtonSQL" runat="server" Text="OK" onclick="OkButtonSQL_Click" />
                    <asp:Button ID="CancelButtonSQL" runat="server" Text="Cancel" />
                </p>
            </asp:Panel>
        </asp:Panel>
    </div>
    <script>
        window.onbeforeunload = function (e) {
            var e = e || window.event;
            //e.returnValue = 'sali pext';
            if (window.name == "*ukn*") {
                window.name = "*ukn*";
            }
        }
        $(document).ready(function () {
            //PREGUNTO SI LA PAGINA VIENE DE SER DUPLIACADA
            preventDuplicateTab();
        });
        function preventDuplicateTab() {
            if (sessionStorage.createTS) {
                if (!window.name) {
                    window.name = "*uknd*";
                    sessionStorage.createTS = Date.now();
                    reCargar();
                } else {
                    if (window.name == "*uknd*") {
                        bloquearPantalla();
                    }
                }
            } else {
                sessionStorage.createTS = Date.now();
                reCargar();
            }
        }
        function reCargar() {
            var currentUrl = window.location.href;
            var url = new URL(currentUrl);
            var desP = url.pathname;
            var p = url.search;
            var dirt = desP + p;
            window.location.href = dirt;
        }
        function bloquearPantalla() {
            var mensaje = "";
            mensaje = "Ya tiene una pagina abierta, para evitar el cruce de información por favor cierre esta pagina";

            //centrar el titulo
            height = 20;//El div del titulo, para que se vea mas arriba (H)
            var ancho = 0;
            var alto = 0;

            //obtenemos el ancho y alto de la ventana de nuestro navegador, compatible con todos los navegadores
            if (window.innerWidth == undefined) ancho = window.screen.width;
            else ancho = window.innerWidth;
            if (window.innerHeight == undefined) alto = window.screen.height;
            else alto = window.innerHeight;

            //operación necesaria para centrar el div que muestra el mensaje
            var heightdivsito = alto / 2 - parseInt(height) / 2;//Se utiliza en el margen superior, para centrar
            imgCentro = "<div style='text-align:center;height:" + alto + "px;'><div  style='color:#000;margin-top:" + heightdivsito + "px; font-size:27px;font-weight:bold'>" + mensaje + "</div></div>";

            //creamos el div que bloquea grande-
            div = document.createElement("div");
            div.id = "WindowLoad"
            div.style.width = ancho + "px";
            div.style.height = alto + "px";
            $("body").append(div);

            //creamos un input text para que el foco se plasme en este y el usuario no pueda escribir en nada de atras
            input = document.createElement("input");
            input.id = "focusInput";
            input.type = "text"

            //asignamos el div que bloquea
            $("#WindowLoad").append(input);

            //asignamos el foco y ocultamos el input text
            $("#focusInput").focus();
            $("#focusInput").hide();

            //centramos el div del texto
            $("#WindowLoad").html(imgCentro);
        }
    </script>

</asp:Content>