<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - HojaDeRuta :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvControlTiempos" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 230px">&nbsp; Identificación<br />
                            <asp:TextBox ID="txtIdenificacion" runat="server" CssClass="textbox" Width="200px"
                                MaxLength="128" />
                        </td>
                        <td style="width: 278px">Número de Crédito<br />
                            <asp:TextBox ID="txtNumeroCredito" runat="server" CssClass="textbox" Width="200px"
                                MaxLength="128" />
                        </td>
                        <td style="font-weight: 700; width: 223px;">Oficina<br />
                            <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox"
                                Height="24px" AutoPostBack="True" Width="200px"
                                OnSelectedIndexChanged="ddlOficina_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 223px;">Código de nómina
                            <br />
                            <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="200px" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>Responsable<br />
                            <asp:DropDownList ID="DdlResponsable" runat="server" CssClass="textbox"
                                Height="24px" Width="210px">
                            </asp:DropDownList>
                        </td>
                        <td>Fecha Proceso<br />
                            <asp:TextBox ID="txtFechaProceso" runat="server" AutoPostBack="True"
                                CssClass="textbox" MaxLength="1"
                                ValidationGroup="vgGuardar" Width="200px" />
                            <asp:CalendarExtender ID="txtFechaProceso_CalendarExtender" runat="server"
                                Enabled="True" Format="MM/dd/yyyy" TargetControlID="txtFechaProceso">
                            </asp:CalendarExtender>
                        </td>
                        <td style="font-weight: 700; width: 223px;">
                            <span style="font-weight: normal">Estado</span><br />
                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox" Width="200px"
                                Height="24px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 230px">&nbsp;
                        </td>
                        <td style="width: 278px">&nbsp;
                        </td>
                        <td colspan="2">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:GridView ID="gvLista" runat="server" AllowPaging="True"
                AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE"
                BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="numerocredito"
                ForeColor="Black" GridLines="Vertical"
                OnPageIndexChanging="gvLista_PageIndexChanging"
                OnRowEditing="gvLista_RowEditing"
                OnSelectedIndexChanged="gvLista_SelectedIndexChanged" PageSize="20"
                Style="margin-right: 0px" Width="100%"
                OnRowDataBound="gvLista_RowDataBound">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="numerocredito" Visible="False" />
                    <asp:TemplateField HeaderText="Gestionar">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select"
                                ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle" Width="16px" />
                            <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit"
                                ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" Width="16px" />
                            <asp:HyperLink ID="HyperConsulta" runat="server" Enabled="False"
                                Visible="False">ir</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="NumeroCredito" HeaderText="Número Crédito" />
                    <asp:BoundField DataField="ultimoproceso" HeaderText="Proceso" />
                    <asp:BoundField DataField="sig_proceso_nom" HeaderText="Siguiente Proceso" />
                    <asp:BoundField DataField="Identificacion" HeaderText="Identificación" />
                    <asp:BoundField DataField="NombreDeudor" HeaderText="Nombre Deudor" />
                    <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina" />
                    <asp:BoundField DataField="FechaS" HeaderText="Fecha Solicitud" />
                    <asp:BoundField DataField="fechadata" HeaderText="Fecha_Datacredito" DataFormatString="{0:d}" Visible="False" />
                    <asp:BoundField DataField="FechaU" HeaderText="Fecha Ultimo Proceso" />
                    <asp:BoundField DataField="encargado" HeaderText="Usuario Ultimo Proceso" />
                    <asp:BoundField DataField="nom_linea" HeaderText="Línea" />
                    <asp:BoundField DataField="estado" HeaderText="Estado Credito" />
                    <asp:TemplateField HeaderText="Enviar Correo">
                        <ItemTemplate>
                            <cc1:ButtonGrid ID="btnCorreo" runat="server"
                                CommandArgument="<%#Container.DataItemIndex %>" CssClass="btn8"
                                OnClick="btnCorreo_Click" Text="Enviar Correo" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCC99" />
                <HeaderStyle CssClass="gridHeader" />
                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                <RowStyle CssClass="gridItem" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                <SortedAscendingHeaderStyle BackColor="#848384" />
                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                <SortedDescendingHeaderStyle BackColor="#575357" />
            </asp:GridView>
            <br />
            <asp:Label ID="lblTotalRegs" runat="server" />
        </asp:View>
    </asp:MultiView>
    <script type="text/javascript" language="javascript">
        function SetFocus() {
            //document.getElementById('cphMain_txtNumeroCredido').focus();
        }
        window.onload = SetFocus;

        function OpenPage() {

            window.open(url), '_blank';
        }
    </script>
</asp:Content>
