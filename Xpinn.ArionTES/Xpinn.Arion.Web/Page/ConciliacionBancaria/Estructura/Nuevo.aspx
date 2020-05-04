<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>  
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <table style="width: 80%" cellspacing="2" cellpadding="2">
                <tr>
                    <td style="text-align:left; width: 15%">
                        Código<br />
                        &nbsp;<asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="70%"></asp:TextBox> 
                     </td>
                    <td style="text-align:left; width: 65%">
                        Nombre<br />
                        &nbsp;<asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="60%"></asp:TextBox> 
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre"
                                Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="vgGuardar" Style="font-size: xx-small" />                       
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:left;">
                        <table width="100%">
                            <tr>
                                <td style="text-align: left; width: 50%">
                                    Bancos<br />
                                    <asp:DropDownList ID="ddlBancos" runat="server" CssClass="textbox" Width="90%" />
                                </td>
                                <td style="text-align: left; width: 50%">
                                    Estado<br />
                                    <asp:RadioButtonList ID="rblEstado" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1">Activo</asp:ListItem>
                                        <asp:ListItem Value="2">Inactivo</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="2">
                        <table width="100%">
                            <tr>
                                <td style="text-align: left; width: 20%">
                                    Tipo de Archivo:
                                </td>
                                <td style="text-align: left; width: 30%">
                                    <asp:RadioButtonList ID="rblTipoArchivo" runat="server" RepeatDirection="Horizontal"
                                        AutoPostBack="True" OnSelectedIndexChanged="rblTipoArchivo_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Excel</asp:ListItem>
                                        <asp:ListItem Value="1">Texto</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="text-align: left; width: 50%">
                                    Formato de fecha
                                    <br />
                                    <asp:DropDownList ID="ddlFormatoFecha" runat="server" CssClass="textbox" 
                                        Width="50%" />
                                </td>
                            </tr>
                        </table>                        
                    </td>                    
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td rowspan="3" style="text-align: left; width: 15%">
                        <strong>Tipo de Datos</strong>
                        &nbsp;
                        <asp:RadioButtonList ID="rblTipoDato" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="rblTipoDato_SelectedIndexChanged">
                            <asp:ListItem Value="0">Delimitados</asp:ListItem>
                            <asp:ListItem Value="1">De Ancho Fijo</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td rowspan="3" style="text-align: left; width: 30%">
                        <strong>Delimitador</strong><br />
                        <asp:RadioButtonList ID="chkSeparaCampo" runat="server" 
                            RepeatDirection="Horizontal" RepeatColumns="3">
                            <asp:ListItem Value="0">Tabulación</asp:ListItem>
                            <asp:ListItem Value="1">Coma</asp:ListItem>
                            <asp:ListItem Value="2">Punto y Coma</asp:ListItem>
                            <asp:ListItem Value="3">Espacio</asp:ListItem>
                            <asp:ListItem Value="4">Otro</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="text-align: left; width: 10%; vertical-align: top">
                        #Filas de Encabezado&nbsp;
                    </td>
                    <td style="text-align: left; width: 10%; vertical-align: top">
                        #Filas Totales
                    </td>
                    <td style="text-align: left; width: 35%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 15%; vertical-align: top">
                        <asp:TextBox ID="txtEncabezado" runat="server" CssClass="textbox" Width="30px" MaxLength="10"></asp:TextBox>
                        <asp:filteredtextboxextender id="fte5" runat="server" targetcontrolid="txtEncabezado"
                            filtertype="Custom, Numbers" validchars="+-=/*()." />                           
                    </td>
                    <td style="text-align: left; width: 15%; vertical-align: top">
                        <asp:TextBox ID="txtTotales" runat="server" CssClass="textbox" Width="30px"  
                            MaxLength="10"></asp:TextBox>
                        <asp:filteredtextboxextender id="fte6" runat="server" targetcontrolid="txtTotales"
                            filtertype="Custom, Numbers" validchars="+-=/*()." />
                    </td>
                </tr>                
                <tr>
                    <td style="text-align: left; width: 20%">                        
                        Separador de Decimales:
                        <br />
                        <asp:TextBox ID="txtSepDecimales" runat="server" CssClass="textbox" 
                            MaxLength="4" Width="40px" />
                    </td>
                    <td style="text-align: left; width: 15%">

                        Separador de Miles:<br />
                        <asp:TextBox ID="txtSepMiles" runat="server" CssClass="textbox" MaxLength="4" 
                            Width="40px"/>

                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <%--<tr>
                    <td style="text-align: left; width: 15%">
                        Formato de fecha
                        <br />
                        <asp:DropDownList ID="ddlFormatoFecha" runat="server" CssClass="textbox" Width="90%">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 20%">
                        Separador de Decimales:
                        <br />
                        <asp:TextBox ID="txtSepDecimales" runat="server" CssClass="textbox" Width="40px"
                            MaxLength="4">.</asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 20%">
                        Separador de Miles:<br />
                        <asp:TextBox ID="txtSepMiles" runat="server" CssClass="textbox" Width="40px" MaxLength="4">,</asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 45%">
                        &nbsp;
                    </td>
                </tr>--%>
                <tr>
                    <td colspan="4" style="text-align: left; width: 100%">
                        <hr style="width: 100%" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="4">
                        <strong>Campo de cada Registro :</strong>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: left; width: 100%">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnAgregar" runat="server" CssClass="btn8" 
                                    OnClick="btnAgregar_Click" Text="+ Adicionar Detalle" />
                                <asp:GridView ID="gvDetalle" runat="server" PageSize="20" ShowHeaderWhenEmpty="True"
                                    AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small;
                                    margin-bottom: 0px;" OnRowDataBound="gvDetalle_RowDataBound" OnRowDeleting="gvDetalle_RowDeleting" 
                                    DataKeyNames="iddetestructura">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                        <ItemStyle HorizontalAlign="center" />
                                        </asp:CommandField>
                                        <asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcod_estructura_detalle" runat="server" Text='<%# Bind("iddetestructura") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre de campo" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcodigo_campo" runat="server" Text='<%# Bind("codigo_campo") %>'
                                                    Visible="false">
                                                </asp:Label>
                                                <cc1:DropDownListGrid ID="ddlcodigo_campo" runat="server" CommandArgument="<%#Container.DataItemIndex %>"
                                                    CssClass="textbox" Width="160px">
                                                </cc1:DropDownListGrid>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Num Columna">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtnumero_columna" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                                    text-align: left" Text='<%# Bind("numero_columna") %>' Width="90px" ></asp:TextBox>
                                                <asp:filteredtextboxextender id="fte1" runat="server" targetcontrolid="txtnumero_columna"
                                                    filtertype="Custom, Numbers" validchars="+-=/*()." />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Posición Inicial">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtposicion_inicial" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                                    text-align: left" Text='<%# Bind("posicion_inicial") %>' Width="80px"></asp:TextBox>
                                                    <asp:filteredtextboxextender id="fte2" runat="server" targetcontrolid="txtposicion_inicial"
                                                    filtertype="Custom, Numbers" validchars="+-=/*()." />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Longitud">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtlongitud" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                                    text-align: left" Text='<%# Bind("longitud") %>' Width="80px"></asp:TextBox>
                                                    <asp:filteredtextboxextender id="fte3" runat="server" targetcontrolid="txtlongitud"
                                                    filtertype="Custom, Numbers" validchars="+-=/*()." />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Justificación">
                                            <ItemTemplate>
                                                <asp:Label ID="lbljustificacion" runat="server" Text='<%# Bind("justificacion") %>'
                                                    Visible="false">
                                                </asp:Label>
                                                <cc1:DropDownListGrid ID="ddljustificacion" runat="server" CommandArgument="<%#Container.DataItemIndex %>"
                                                    CssClass="textbox" Width="160px">
                                                </cc1:DropDownListGrid>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Justificador">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtjustificador" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                                    text-align: left" Text='<%# Bind("justificador") %>' Width="120px" MaxLength="1"></asp:TextBox>
                                                    <asp:filteredtextboxextender id="fte4" runat="server" targetcontrolid="txtjustificador"
                                                    filtertype="Custom, Numbers" validchars="+-=/*()." />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nro Decimales">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDecimales" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                                    text-align: left" Text='<%# Bind("decimales") %>' Width="90px" MaxLength="5"></asp:TextBox>
                                                    <asp:filteredtextboxextender id="fte5" runat="server" targetcontrolid="txtDecimales"
                                                    filtertype="Custom, Numbers" validchars="+-=/*()." />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center"/>
                                        </asp:TemplateField>                                        
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" />
                                    <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
                <asp:Panel id="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br /><br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            Se ha
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                            &nbsp;correctamente la Estructura</td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Button ID="btnFinal" runat="server" Text="Continuar" 
                                onclick="btnFinal_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <asp:HiddenField ID="HiddenField1" runat="server" />    
     
     <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>
</asp:Content>