<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Balance Iniciación :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <asp:Panel ID="pConsulta" runat="server">
        <table>
            <tr>
                <td style="width:150px; height: 34px">
                </td>
                <td style="text-align: left; height: 34px;">
                    &nbsp;&nbsp;<asp:ImageButton ID="btnGenerarSaldos" runat="server" ImageUrl="~/Images/btnGenerarSaldos.jpg"
                        OnClick="btnGenerarSaldos_Click" />
                </td>
                <td style="text-align: left; height: 34px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <br />
                </td>
            </tr>
            <tr>
                <td style="width:150px;text-align: left">
                    &nbsp;Fecha Corte :<br />
                </td>
                <td style="text-align: left">
                    <ucFecha:fecha ID="txtFechaCorte" runat="server" CssClass="textbox" MaxLength="1"
                        Width="148px" />
                    &nbsp;
                    <br />
                </td>
                <td style="text-align: left">
                    <br />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pDatos" runat="server">
        <hr style="width: 100%" />
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                        AllowPaging="True" OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                        OnPageIndexChanging="gvLista_PageIndexChanging" style="font-size: x-small" DataKeyNames="cod_persona">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                        ToolTip="Modificar" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco" />
                                <ItemStyle CssClass="gridIco" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Código de Cuenta" DataField="cod_cuenta_niif">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Nombre" DataField="nombre">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Tipo" DataField="tipo">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Ident. Tercero" DataField="identificacion">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Nombre del Tercero" DataField="nombre_tercero">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Saldo Local" DataFormatString="{0:n0}" DataField="saldo_colgaap">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="reclasificacion" DataFormatString="{0:n0}" 
                                HeaderText="Reclasificación">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Ajuste" DataFormatString="{0:n0}" DataField="ajuste">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Saldo NIIF" DataFormatString="{0:n0}" DataField="saldo">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Centro Costo" DataField="centro_costo">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Cod Persona" DataField="cod_persona" Visible="false">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="panelCarga" runat="server">
        <table>
            <tr>
                <td>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:RadioButtonList ID="rblTipoCarga" runat="server" RepeatDirection="Horizontal" Width="300px" AutoPostBack="true"
                                OnSelectedIndexChanged="rblTipoCarga_SelectedIndexChanged">
                                <asp:ListItem Value="1" Selected="True" Text="Balance Inicial" />
                                <asp:ListItem Value="2" Text="Ajustes de Balance" />
                            </asp:RadioButtonList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table style="width:100%">
            <tr>
                <td colspan="3" style="text-align: left; font-size: x-small">
                    <strong>Criterios de Carga</strong>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: left; font-size: x-small">
                    <asp:UpdatePanel runat="server" RenderMode="Inline" UpdateMode="Conditional">
                        <ContentTemplate>
                            <strong>Estructura de archivo : </strong>&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblEstBalance" runat="server" Text="Cod Cuenta Niif, Nombre Cuenta(opcional), Cod Moneda, 
                                    Cod Persona(opcional), Identificación(opcional), Nombre(opcional), Saldo, Centro de Costo."></asp:Label>
                            <asp:Label ID="lblEstAjuste" runat="server" Visible="false" Text="Cod Cuenta Niif, Centro Costo, Tipo Ajuste( 1 = Reclasificación, 2 = Ajuste), Cod Cuenta Destino (Opcional solo si es de tipo Reclasificación)
                                    Valor, Observación."></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rblTipoCarga" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: left; font-size: x-small">
                    <strong>Nombre de Hoja excel : </strong>&nbsp;&nbsp;&nbsp; Datos
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: left">
                    <asp:FileUpload ID="fupArchivoCarga" runat="server" Height="21px" Width="400px" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: left">
                    <asp:ImageButton ID="btnCargaDatos" runat="server" ImageUrl="~/Images/btnCargaExcel.jpg"
                        OnClick="btnCargaDatos_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: left; width:100%">
                    <asp:Panel ID="panelAjustes" runat="server" Visible="false">
                        <div style="overflow: scroll; width: 100%">
                            <strong>Listado de Ajustes del Balance</strong><br />
                            <asp:GridView ID="gvCargaAjuste" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                HeaderStyle-CssClass="gridHeader" DataKeyNames="cod_cuentaOrigen_niif, saldo"
                                PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" Style="font-size: x-small">
                                <Columns>
                                    <asp:BoundField HeaderText="Código de Cuenta" DataField="cod_cuentaOrigen_niif">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Centro Costo" DataField="centro_costo">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Tipo" DataField="ajuste">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Cod Cuenta Destino" DataField="cod_cuenta_niif">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="saldo" DataFormatString="{0:n0}" HeaderText="Valor">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Observación" DataField="nombre">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <br />

    <asp:ModalPopupExtender ID="mpeVisualizarDatos" runat="server" 
        PopupControlID="panelMostrar" TargetControlID="hfNiif" BackgroundCssClass="backgroundColor" >       
    </asp:ModalPopupExtender>  
    <asp:HiddenField ID="hfNiif" runat="server" />
    <asp:Panel ID="panelMostrar" runat="server" BackColor="White" BorderWidth="1px" Style="text-align: left; padding: 10px">
        <asp:UpdatePanel ID="upReclasificacion" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table style="width:100%">
                    <tr>
                        <td colspan="4" class="gridHeader" align="center">RECLASIFICACIÓN AJUSTES DE SALDOS - NIIF
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; text-align: left">Codigo Cuenta :
                        </td>
                        <td style="width: 199px">
                            <asp:TextBox ID="txtcod_cuenta" runat="server" CssClass="textbox" MaxLength="128"
                                Width="105px"></asp:TextBox>
                        </td>
                        <td style="width: 75px; text-align: left">Nombre :
                        </td>
                        <td>
                            <asp:TextBox ID="txtnombre" runat="server" Width="161px" CssClass="textbox" MaxLength="128"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; text-align: left">Tipo Ajuste:
                        </td>
                        <td style="width: 199px">
                            <asp:DropDownList ID="ddlTipo" runat="server" AutoPostBack="True" CssClass="textbox"
                                OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged" Width="191px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 75px; text-align: left">Saldo :
                        </td>
                        <td>
                            <asp:TextBox ID="txtsaldo" runat="server" CssClass="textbox" MaxLength="128" Width="127px" Style="text-align: right"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; text-align: left">
                            <asp:Label ID="lblcuenta_dest" runat="server" Text="Cuenta Destino :" Visible="false"></asp:Label>
                        </td>
                        <td style="width: 199px" colspan="2">
                            <asp:DropDownList ID="ddlCuenta_Dest" runat="server" CssClass="textbox" Width="300px"
                                Visible="false">
                            </asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="width: 116px; text-align: left">Valor :
                        </td>
                        <td style="width: 199px; text-align: left">
                            <uc2:decimales ID="txtValor" runat="server" />
                        </td>
                        <td style="width: 75px">&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; text-align: left; vertical-align: top">Observaciones :
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtObserv" runat="server" Height="81px" TextMode="MultiLine" Width="443px"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:Label ID="lblMenError" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:Button ID="btnModificar" runat="server" Text="Guardar" Height="30px" OnClick="btnModificar_Click1" />
                            &nbsp;
                                    <asp:Button ID="btnCloseReg1" runat="server" Text="Cancelar" Height="30px" OnClick="btnCloseReg1_Click1" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <br />
    <br />
      
    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>

    <script type='text/javascript'>
        function Forzar() {
            __doPostBack('', '');
        }
    </script>
</asp:Content>