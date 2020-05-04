<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
 
<%@ Register Src="~/General/Controles/imprimir.ascx" TagName="imprimir" TagPrefix="ucImprimir" %>  
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="txtPesos" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register src="~/General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc2" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlNumero.ascx" TagName="numero" TagPrefix="uc5" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 51%">
        <tr>
            <td colspan="7">
                <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 135px">Periodo</td>
            <td style="width: 135px">
                <asp:DropDownList ID="DdlPeriodicidad" runat="server">
                    <asp:ListItem Value="1">MENSUAL</asp:ListItem>
                </asp:DropDownList>
                </td>
            <td style="width: 135px">Año</td>
            <td style="width: 135px">
                <asp:DropDownList ID="DdlYear" runat="server">
                    <asp:ListItem Value="2015">2015</asp:ListItem>
                    <asp:ListItem>2016</asp:ListItem>
                    <asp:ListItem>2017</asp:ListItem>
                    <asp:ListItem>2018</asp:ListItem>
                    <asp:ListItem>2019</asp:ListItem>
                    <asp:ListItem>2020</asp:ListItem>
                    <asp:ListItem>2021</asp:ListItem>
                    <asp:ListItem>2022</asp:ListItem>
                </asp:DropDownList>
                </td>
            <td style="width: 135px">Mes</td>
            <td colspan="2">
                <asp:DropDownList ID="DdlMes" runat="server">
                    <asp:ListItem Value="1">ENERO</asp:ListItem>
                    <asp:ListItem Value="2">FEBRERO</asp:ListItem>
                    <asp:ListItem Value="3">MARZO</asp:ListItem>
                    <asp:ListItem Value="4">ABRIL</asp:ListItem>
                    <asp:ListItem Value="5">MAYO</asp:ListItem>
                    <asp:ListItem Value="6">JUNIO</asp:ListItem>
                    <asp:ListItem Selected="True" Value="7">JULIO</asp:ListItem>
                    <asp:ListItem Value="8">AGOSTO</asp:ListItem>
                    <asp:ListItem Value="9">SEPTIEMBRE</asp:ListItem>
                    <asp:ListItem Value="10">OCTUBRE</asp:ListItem>
                    <asp:ListItem Value="11">NOVIEMBRE</asp:ListItem>
                    <asp:ListItem Value="12">DICIEMBRE</asp:ListItem>
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td style="width: 135px" colspan="5">&nbsp;</td>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 135px" colspan="5">
                Rodamiento</td>
            <td style="width: 28%">
                <asp:CheckBox ID="ChkPorcentaje" runat="server" Text="Porcentaje" 
                    TextAlign="Left" style="font-size: x-small" AutoPostBack="True" 
                    Checked="True" oncheckedchanged="ChkPorcentaje_CheckedChanged" />
            </td>
            <td style="width: 21%">
                <asp:CheckBox ID="ChkPesos" runat="server" Text="Pesos                                                                             " 
                    TextAlign="Left" style="font-size: x-small" AutoPostBack="True" 
                    oncheckedchanged="ChkPesos_CheckedChanged" />
            </td>
        </tr>
        <tr>
            <td style="width: 135px" colspan="5">
                Cartera Menor a 30
            </td>
            <td style="width: 28%">
                <asp:CheckBox ID="ChkPorcCarMenor" runat="server" Text="Porcentaje" 
                    TextAlign="Left" style="font-size: x-small" AutoPostBack="True" 
                    Checked="True" oncheckedchanged="ChkPorcCarMenor_CheckedChanged" />
            </td>
            <td style="width: 21%">
                <asp:CheckBox ID="ChkPesosCarMenor" runat="server" Text="Pesos                                                                             " 
                    TextAlign="Left" style="font-size: x-small" AutoPostBack="True" 
                    oncheckedchanged="ChkPesosCarMenor_CheckedChanged" />
            </td>
        </tr>
        <tr>
            <td style="width: 135px; height: 25px;" colspan="5">
                Carter Mayor a 30
            </td>
            <td style="width: 28%; height: 25px;">
                <asp:CheckBox ID="ChkPorcCarMayor" runat="server" Text="Porcentaje" 
                    TextAlign="Left" style="font-size: x-small" AutoPostBack="True" 
                    Checked="True" oncheckedchanged="ChkPorcCarMayor_CheckedChanged" />
            </td>
            <td style="width: 21%; height: 25px;">
                <asp:CheckBox ID="ChkPesosCarMayor" runat="server" Text="Pesos                                                                             " 
                    TextAlign="Left" style="font-size: x-small" AutoPostBack="True" 
                    oncheckedchanged="ChkPesosCarMayor_CheckedChanged" />
            </td>
        </tr>
        <tr>
            <td style="width: 135px" colspan="5">
                Colocación</td>
            <td style="width: 28%">
                <asp:CheckBox ID="ChkColocNumero" runat="server" Text="Numero" 
                    TextAlign="Left" style="font-size: x-small" AutoPostBack="True" 
                    Checked="True" oncheckedchanged="ChkColocNumero_CheckedChanged" />
            </td>
            <td style="width: 21%">
                <asp:CheckBox ID="ChkColocPesos" runat="server" Text="Pesos" 
                    TextAlign="Left" style="font-size: x-small" AutoPostBack="True" 
                    Checked="True" oncheckedchanged="ChkColocPesos_CheckedChanged" />
            </td>
        </tr>
    </table>
    &nbsp;
    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <hr width="100%" noshade />
            </td>
        </tr>
        <tr>
            <td>
                <span>
                <br />
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                    AllowPaging="True" PageSize="4" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" 
                    OnRowCommand="gvLista_RowCommand" OnRowDeleting="gvLista_RowDeleting" 
                    onrowdatabound="gvLista_RowDataBound" style="margin-right: 0px">
                    <Columns>
                        <asp:TemplateField HeaderText="Codigo">
                            <ItemTemplate>
                                <asp:Label ID="Lblcodigo" runat="server" Text='<%# Bind("IdEjecutivo") %>'></asp:Label>                            
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("IdEjecutivo") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle CssClass="gridColNo" />
                            <ItemStyle CssClass="gridColNo" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Borrar" />                            
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco">
                            </HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Codigo">
                            <ItemTemplate>
                                <span>
                                <asp:Label ID="Lblcodigoejec" runat="server" Text='<%# Bind("IdEjecutivo") %>'></asp:Label>
                                </span>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Nombres">
                            <ItemTemplate>
                                <span>
                                <asp:Label ID="LblNombres" runat="server" Text='<%# Bind("Nombres") %>' Width="250px"></asp:Label>
                                </span>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("Nombres") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle CssClass="gridIco">
                            </HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Oficina">
                            <ItemTemplate>
                                <span>
                                <asp:Label ID="Lbloficina" runat="server" Text='<%# Bind("NombreOficina") %>' Width="100px"></asp:Label>
                                </span>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("NombreOficina") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Colocacion$">
                            <ItemTemplate>
                                <span>                                  
                                  <asp:TextBox ID="txtpesoscolocacion" runat="server" CssClass="textbox" DataFormatString="{0:n0}"
                                    Height="15px" MaxLength="38" Width="120px"  />
                                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                    ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999" MaskType="Number"
                                    MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                    TargetControlID="txtpesoscolocacion" />                         
                                </span>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Colocacion #">
                            <ItemTemplate>
                                <span>
                                <asp:TextBox ID="txtnumcolocacion" runat="server" Width="80px" 
                                    CssClass="textbox"></asp:TextBox>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Llave Rodamiento">
                            <ItemTemplate>
                                <span>
                                <asp:TextBox ID="txtRodamientoPorc" runat="server" CssClass="textbox" 
                                    ForeColor="#FF0066" Style="font-size: xx-small;
                                    text-align: left" Width="100px"></asp:TextBox>
                               <asp:TextBox ID="txtRodamientoPesos" runat="server" CssClass="textbox" DataFormatString="{0:n0}"
                                    Height="15px" MaxLength="38" Width="120px"  />
                                <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                    ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999" MaskType="Number"
                                    MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                    TargetControlID="txtRodamientoPesos" />
                                <br />
                                </span>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <span>
                                <asp:TextBox ID="txtRodamientoPorc" runat="server" CssClass="textbox" 
                                    ForeColor="#FF0066" Style="font-size: xx-small;
                                    text-align: left" Width="100px">r</asp:TextBox>
                                <br />
                                <asp:TextBox ID="txtRodamientoPesos" runat="server" CssClass="textbox" 
                                    ForeColor="#FF0066" Style="font-size: xx-small;
                                    text-align: left" Width="100px">r</asp:TextBox>
                                </span>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Menor a 30">
                            <ItemTemplate>
                                <span>
                                <asp:TextBox ID="txtMenor30Porc" runat="server" 
                                    CssClass="textbox" ForeColor="#FF0066" Style="font-size: xx-small;
                                    text-align: left" Width="100px"></asp:TextBox>
                                <asp:TextBox ID="LblColocPesos" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                    text-align: left" Width="120px"></asp:TextBox>                             
                                <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" 
                                    AcceptNegative="Left" DisplayMoney="Left"
                                    ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999" MaskType="Number"
                                    MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                    TargetControlID="txtMenor30Pesos" />
                                </span>
                            </ItemTemplate>
                            <ItemTemplate>
                                <span>
                                <asp:TextBox ID="txtMenor30Porc" runat="server" CssClass="textbox" 
                                    ForeColor="#FF0066" Style="font-size: xx-small;
                                    text-align: left" Width="100px"></asp:TextBox>
                                <asp:TextBox ID="txtMenor30Pesos" runat="server" CssClass="textbox" DataFormatString="{0:n0}"
                                    Height="15px" MaxLength="38" Width="120px"  />
                                <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                    ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999" MaskType="Number"
                                    MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                    TargetControlID="txtMenor30Pesos" />
                                </span>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mayor a 30">
                            <ItemTemplate>
                                <span>
                                <asp:TextBox ID="txtRodamientoPorc" runat="server" CssClass="textbox" 
                                    ForeColor="#FF3399" Style="font-size: xx-small;
                                    text-align: left" Width="100px"></asp:TextBox>                               
                                <asp:TextBox ID="txtRodamientoPesos" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                    text-align: left" Width="120px"></asp:TextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" 
                                    AcceptNegative="Left" DisplayMoney="Left"
                                    ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999" MaskType="Number"
                                    MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                    TargetControlID="txtMayor30Pesos" />
                                </span>
                            </ItemTemplate>
                            <ItemTemplate>
                                <span>
                                <asp:TextBox ID="txtMayor30Por" runat="server" CssClass="textbox" 
                                    ForeColor="#FF0066" Style="font-size: xx-small;
                                    text-align: left" Width="100px"></asp:TextBox>
                                <asp:TextBox ID="txtMayor30Pesos" runat="server" CssClass="textbox" DataFormatString="{0:n0}"
                                    Height="15px" MaxLength="38" Width="120px"  />
                                <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                    ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999" MaskType="Number"
                                    MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                    TargetControlID="txtMayor30Pesos" />
                                </span>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <br />
                <br />
                </span>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
            </td>
        </tr>
    </table>
    <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtCodigo').focus();
        }
        window.onload = SetFocus;
    </script>
    <asp:Label ID="msg" runat="server" Font-Bold="true" ForeColor="Red" />
</asp:Content>