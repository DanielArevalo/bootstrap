<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Src="~/General/Controles/ctlMensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>--%>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <br />
        <asp:Panel ID="pBusqueda" runat="server">
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="height: 15px; text-align: left; font-size: x-small;" colspan="4">
                        <strong>Criterios de Búsqueda:</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;" width="14%">Tipo de Persona<br />
                        <asp:DropDownList ID="ddlTipoPersona" runat="server" CssClass="textbox"
                            AppendDataBoundItems="True">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="N">Natural</asp:ListItem>
                            <asp:ListItem Value="J">Juridica</asp:ListItem>
                            <asp:ListItem Value="M">Menor de Edad</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left;" width="14%">Código<br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="40%"></asp:TextBox>
                    </td>
                    <td style="text-align:left" colspan="2">Periodo Consulta<br>
                        <uc:fecha ID="txtFecha" runat="server" cssClass="textbox" Height="16px" />
                        &nbsp;a&nbsp;
                        <uc:fecha ID="txtFechaFinal" runat="server" cssClass="textbox" Height="16px" />
                    </td>
                    <%--<td style="text-align: left;" width="14%"">Ciudad<br />
                        <asp:DropDownList ID="ddlCiudad" runat="server" Width="43%" CssClass="textbox"
                            AppendDataBoundItems="True">
                            <asp:ListItem Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>--%>
                </tr>
                <tr>
                    <td style="text-align: left;" width="14%">Identificación<br />
                        <asp:TextBox ID="txtNumeIdentificacion" runat="server" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="text-align: left;" width="14%">Nombres<br />
                        <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Enabled="true" Width="90%"></asp:TextBox>
                    </td>
                    <td style="text-align: left;" width="14%">Apellidos<br />
                        <asp:TextBox ID="txtApellidos" runat="server" CssClass="textbox" Enabled="true" Width="90%"></asp:TextBox>
                    </td>
                    <td style="text-align: left;" width="14%">Tipo de Rol<br />
                        <asp:DropDownList ID="ddlTipoRol" runat="server" Width="43%" CssClass="textbox"
                            AppendDataBoundItems="True">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="A" Text="Asociado"></asp:ListItem>
                            <asp:ListItem Value="R" Text="Retirado"></asp:ListItem>
                            <asp:ListItem Value="T" Text="Tercero"></asp:ListItem>
                        </asp:DropDownList>
                    </td>                           
                </tr>
                <tr>
                    <td style="text-align: left;">Filtrar coincidencias OFAC
                        <asp:CheckBox ID="checkOFAC" runat="server" />
                    </td>
                    <td style="text-align: left;" width="14%">Filtrar coincidencias ONU
                        <asp:CheckBox ID="checkONU" runat="server" />
                    </td>     
                    <td style="text-align: left;" width="14%">Ver Ultima Consulta
                        <asp:CheckBox ID="cbUltima" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br />
                        <asp:Button ID="btnConsultaMasiva" runat="server" Text="  Consulta Masiva Asociados Activos  " CssClass="btn8"
                            Height="30px" OnClick="btnConsultaMasiva_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnConsultaMasivaOtros" runat="server" Text="  Consulta Masiva Otros  " CssClass="btn8"
                            Height="30px" OnClick="btnConsultaMasivaOtros_Click" />
                        
                        <br />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>    
    <table style="width: 100%">
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                    AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDataBound="gvLista_RowDataBound" Style="font-size: small">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
							<ItemTemplate>
								<asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                ToolTip="Detalle" /></ItemTemplate>
							<HeaderStyle CssClass="gridIco"></HeaderStyle>
						</asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblTipoConsulta" runat="server" Text='<%# Bind("tipo_consulta") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridOF" HeaderText="Encontrado ONU" HeaderImageUrl="~/Images/ofac.png">
                            <ItemTemplate>
                                <asp:Image ID="btnCheckO" runat="server" ImageUrl="~/Images/CheckCircularVerde.png" ToolTip="Detalle" Width="16px" />
                                <asp:Image ID="btnEquisO" runat="server" ImageUrl="~/Images/EquisCircularRojo.png" ToolTip="Detalle" Width="16px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridONU"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridOF" HeaderText="Encontrado OFAC" HeaderImageUrl="~/Images/onu.jpg">
                            <ItemTemplate>
                                <asp:Image ID="btnCheckF" runat="server" ImageUrl="~/Images/CheckCircularVerde.png" ToolTip="Detalle" Width="16px" />
                                <asp:Image ID="btnEquisF" runat="server" ImageUrl="~/Images/EquisCircularRojo.png" ToolTip="Detalle" Width="16px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridOFAC"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="cod_persona" HeaderText="Código">
                            <ItemStyle HorizontalAlign="Center" Width="8%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Tipo Persona">
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Nombre - Razón Social ">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_consulta" HeaderText="Fecha Ult. Consulta">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                    Visible="False" />
            </td>
        </tr>
    </table>
    
    <br />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

    <asp:HiddenField ID="HF2" runat="server" />
    <asp:ModalPopupExtender ID="mpeProcesando" runat="server" PopupControlID="pProcesando"
        TargetControlID="HF2" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pProcesando" runat="server" BackColor="White" Style="text-align: right;"
        CssClass="pnlBackGround">
        <table style="width: 100%;">
            <tr>
                <td align="center">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/loading.gif" />
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="Espere un momento mientras se ejecuta el Proceso."></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlFinal" runat="server" BackColor="White" Style="text-align: right;">
        <table style="width: 100%;">
            <tr>
                <td align="center">
                    <asp:Label ID="lblSalida" Visible="false" ForeColor="Green" runat="server" Text="el proceso se completó exitosamente."></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" Style="text-align: center; width: 100%">
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" Interval="300" OnTick="Timer1_Tick" />
            <br />
            <asp:Label ID="lblError" runat="server" Text=""
                Style="text-align: left; color: #FF3300"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type='text/javascript'>
        function Forzar() {
            __doPostBack('', '');
        }
    </script>
</asp:Content>