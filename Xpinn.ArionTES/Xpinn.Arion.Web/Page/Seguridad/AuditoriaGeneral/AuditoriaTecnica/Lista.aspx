<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Auditoria :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css" rel="Stylesheet" type="text/css" />
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                    <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdI">Codusuario&nbsp;<asp:CompareValidator ID="cvcodusuario" runat="server" ControlToValidate="ddlUsuarios" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:DropDownList ID="ddlUsuarios" runat="server" CssClass="dropdown" Width="200px" Height="30px" />
                            </td>
                            <td class="tdI">Nombre Usuario<br />
                                <asp:TextBox runat="server" ID="txtNombreUsuario" CssClass="textbox" />
                            </td>
                            <td class="tdD">Codopcion&nbsp;<asp:CompareValidator ID="cvcodopcion" runat="server" ControlToValidate="ddlOpciones" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:DropDownList ID="ddlOpciones" runat="server" CssClass="dropdown" Width="200px" Height="30px"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">Fecha Inicio&nbsp;<%--<asp:CompareValidator ID="cvfecha" runat="server" ControlToValidate="txtfecha" ErrorMessage="Formato de Fecha (dd/mm/aaaa)" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" />--%><br />
                                <ucFecha:fecha ID="txtFecha" CssClass="textbox" runat="server" />
                            </td>
                            <td class="tdI">FechaFinal&nbsp;<%--<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtFechaFinal" ErrorMessage="Formato de Fecha (dd/mm/aaaa)" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" />--%><br />
                                <ucFecha:fecha ID="txtFechaFinal" CssClass="textbox" runat="server" />
                            </td>
                            <td class="tdD">Procedimiento&nbsp;<br />
                                <asp:TextBox runat="server" ID="txtProcedimiento" CssClass="textbox"/>
                            </td>
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
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="consecutivo">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="codigousuario" HeaderText="Cod. Usuario" ItemStyle-Width="50px" />
                        <asp:BoundField DataField="nombreusuario" HeaderText="Usuario" />
                        <asp:BoundField DataField="codigoOpcion" HeaderText="Cod. Opcion" ItemStyle-Width="50px"/>
                        <asp:BoundField DataField="nombre_opcion" HeaderText="Opcion" />
                        <asp:BoundField DataField="fechaejecucion" HeaderText="Fecha" />
                        <asp:BoundField DataField="nombresp" HeaderText="Procedimiento" />
                        <asp:BoundField DataField="exitoso" HeaderText="Exitoso" ItemStyle-Width="50px"/>
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
            </td>
        </tr>
    </table>
    <script type="text/javascript">
<%--    $(function () {
        $("[id$=txtProcedimiento]").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("~/Page/Seguridad/AuditoriaTecnica/Lista.aspx/GetProcedimientos") %>',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item,
                                val: item
                            }
                        }))
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            select: function (e, i) {
                $("[id$=hfCustomerId]").val(i.item.val);
            },
            minLength: 3
        });
    });  --%>
    </script>
</asp:Content>
