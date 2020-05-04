<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/ctlLineaAhorro.ascx" TagName="ddlLineaAhorro" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlOficina.ascx" TagName="ddlOficina" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: 1200,
                height: 500,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });
        }        
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table cellpadding="5" cellspacing="0" style="width: 80%; margin-right: 0px;">
            <tr>
               <td style="height: 15px; text-align: left; width: 119px;">
                    Tipo de Operación<br />
                    <asp:DropDownList ID="ddloperacion" runat="server" Width="241px" CssClass="textbox"
                        AppendDataBoundItems="true">
                        <asp:ListItem Value="0">Seleccione Un Item</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="height: 15px; text-align: left; width: 119px;">
                    Tipo de Transacción<br />
                    <asp:DropDownList ID="ddltransaccion" runat="server" Width="232px" CssClass="textbox"
                        AppendDataBoundItems="true">
                        <asp:ListItem Value="0">Seleccione Un Item</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: left; height: 15px; width: 99px;">
                    Tipo de Producto<br />
                    <asp:DropDownList ID="ddlproducto" runat="server" Width="200px " CssClass="textbox">
                        <asp:ListItem Value="0">Seleccione Un Item</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: left; height: 15px;" colspan="2">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table style="width: 100%">
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="80%" AutoGenerateColumns="False"
                    AllowPaging="False" GridLines="Horizontal" ShowHeaderWhenEmpty="True" OnPageIndexChanging="gvLista_PageIndexChanging"
                    OnRowDeleting="gvLista_RowDeleting" OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    OnRowDataBound="gvLista_RowDataBound" DataKeyNames="idparametrogmf" Style="font-size: xx-small">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar"/>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>                  
                            <ItemStyle CssClass="gridIco"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Borrar" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            <ItemStyle CssClass="gI" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="idparametrogmf" HeaderText="ID">
                            <ItemStyle HorizontalAlign="Left"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo_ope" HeaderText="Cod. Operación">
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:BoundField>
                           <asp:BoundField DataField="operacion" HeaderText="Tipo Operación">
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo_tran" HeaderText="Cod. Transacción">
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="transaccion" HeaderText="Tipo Transacción">
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Afecta Producto">
                            <ItemTemplate>
                                <asp:Label ID="lblafectaproducto" Text='<%# Eval("afecta_producto") %>' runat="server"
                                    Width="100px" Visible="false" />
                                <asp:CheckBox ID="cbproducto" runat="server" Width="100" Enabled="false" Checked='<%#Convert.ToBoolean(Eval("afecta_producto")) %>' />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Asume Cliente">
                            <ItemTemplate>
                                <asp:Label ID="lblasume" Text='<%# Eval("asume") %>' runat="server"
                                    Width="100px" Visible="false" />
                                <asp:CheckBox ID="cbasume" runat="server" Width="100" Enabled="false" Checked='<%#Convert.ToBoolean(Eval("asume")) %>' />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="porasume_cliente" HeaderText="%Asume" DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Maneja Excentas">
                            <ItemTemplate>
                                <asp:Label ID="lblmanejaexcentas" Text='<%# Eval("maneja_exentas") %>' runat="server"
                                    Width="60px" Visible="false" />
                                <asp:CheckBox ID="cbmanejaexcentas" runat="server" Width="100" Enabled="false" Checked='<%#Convert.ToBoolean(Eval("maneja_exentas")) %>' />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Efectivo">
                            <ItemTemplate>
                                <asp:Label ID="lblpagoefectivo" Text='<%# Eval("pago_efectivo") %>' runat="server"
                                    Width="60px" Visible="false" />
                                <asp:CheckBox ID="cbpagoefectivo" runat="server" Enabled="false" Checked='<%#Convert.ToBoolean(Eval("pago_efectivo")) %>' />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Cheque">
                            <ItemTemplate>
                                <asp:Label ID="lblpagocheque" Text='<%# Eval("pago_cheque") %>' runat="server"
                                    Width="60px" Visible="false" />
                                <asp:CheckBox ID="cbpagocheque" runat="server" Enabled="false" Checked='<%#Convert.ToBoolean(Eval("pago_cheque")) %>' />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Traslado">
                            <ItemTemplate>
                                <asp:Label ID="lbltraslado" Text='<%# Eval("pago_traslado") %>' runat="server"
                                    Width="60px" Visible="false" />
                                <asp:CheckBox ID="cbpagotraslado" runat="server" Enabled="false" Checked='<%#Convert.ToBoolean(Eval("pago_traslado")) %>' />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                         <asp:BoundField DataField="tipo_producto" HeaderText="Tipo Producto" DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:BoundField DataField="cod_linea" HeaderText="Línea Producto" DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                           <asp:BoundField DataField="nom_linea" HeaderText="Línea Producto" DataFormatString="{0:n0}">
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
    <uc1:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
