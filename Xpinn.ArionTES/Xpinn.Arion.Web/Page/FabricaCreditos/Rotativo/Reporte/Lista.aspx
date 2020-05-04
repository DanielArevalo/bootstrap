<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - LineasCredito :." %>

<%@ Register Src="../../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="Panel1" runat="server" Visible="False">
                </asp:Panel>
                <asp:Panel ID="pConsulta" runat="server">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdI" style="width: 143px">Número Crédito<br />
                                <asp:TextBox ID="txtNumero_radicacion" CssClass="textbox" runat="server"
                                    MaxLength="128" />
                            </td>
                            <td class="tdI">Identificación<br />
                                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox"
                                    Width="87%" />
                            </td>
                            <td class="tdI">Nombre
                                <br />
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="87%" />
                            </td>
                            <td class="tdI">Código de nómina<br />
                                <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="87%" />
                            </td>
                        </tr>
                        <tr>
                            <td>Fecha Solicitud<br />
                                <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" />
                            </td>
                            <td class="tdI" style="width: 143px">Linea<br />
                                <asp:DropDownList ID="ddlLinea" runat="server" CssClass="textbox" Width="90%" />
                            </td>
                            <td class="tdI">Oficina;
                                <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox"
                                    Width="90%" />
                            </td>
                            <td class="tdI">Estado;
                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox"
                                    Width="90%" />
                            </td>
                            <td class="tdI" colspan="2">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="tdI" colspan="5">&nbsp;</td>
                            <td class="tdI">&nbsp;</td>
                            <td class="tdI">&nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <hr width="100%" noshade>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
            </td>
        </tr>
    </table>
    <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtCOD_LINEA_CREDITO').focus();
        }
        window.onload = SetFocus;
    </script>
    </table>
        <div style="overflow: scroll; height: 500px; width: 100%;">
            <div style="width: 100%;">
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                    OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader"
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"
                    DataKeyNames="COD_LINEA_CREDITO" Style="font-size: x-small">
                    <Columns>
                        <asp:BoundField DataField="NumeroCredito" HeaderStyle-CssClass="gridColNo"
                            ItemStyle-CssClass="gridColNo">
                            <HeaderStyle CssClass="gridColNo"></HeaderStyle>

                            <ItemStyle CssClass="gridColNo"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                    ToolTip="Modificar" />
                            </ItemTemplate>

                            <HeaderStyle CssClass="gridIco"></HeaderStyle>

                            <ItemStyle CssClass="gridIco"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NumeroCredito" HeaderText="Número Radicación" />
                        <asp:BoundField DataField="fecha" DataFormatString="{0:d}"
                            HeaderText="Fecha Solicitud" />
                        <asp:BoundField DataField="LineaCredito" HeaderText="Nombre de Línea">
                            <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Identificacion" HeaderText="Identificacion" />
                        <asp:BoundField DataField="Nombres" HeaderText="Nombre" />
                        <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina" />
                        <asp:BoundField DataField="Monto" HeaderText="Monto Solicitado" />
                        <asp:BoundField DataField="Plazo" HeaderText="Plazo" />
                        <asp:BoundField DataField="Periodicidad" HeaderText="Periodicidad" />
                        <asp:BoundField DataField="forma_pago" HeaderText="Forma Pago" />
                    </Columns>

                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>

                    <PagerStyle CssClass="gridPager"></PagerStyle>

                    <RowStyle CssClass="gridItem"></RowStyle>
                </asp:GridView>
            </div>
        </div>
</asp:Content>
