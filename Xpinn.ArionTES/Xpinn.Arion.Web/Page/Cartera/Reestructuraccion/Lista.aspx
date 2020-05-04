<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Reestructuración :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>

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
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                    <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="80%">
                        <tr>
                            <td>
                                <asp:Panel ID="Panel1" runat="server">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td colspan="3" style="text-align: left">
                                                <strong>Criterios de Búsqueda</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">Número Radicación<br />
                                            </td>
                                            <td style="text-align: left">&nbsp;Línea de Crédito
                                            </td>
                                            <td style="text-align: left">&nbsp;Oficina<br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" MaxLength="128" Width="120px" />
                                                <br />
                                                <asp:CompareValidator ID="cvnumero_radicacion1" runat="server" ControlToValidate="txtnumero_radicacion"
                                                    Display="Dynamic" ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck"
                                                    SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Style="font-size: x-small" />
                                            </td>
                                            <td style="text-align: left">
                                                <asp:DropDownList ID="ddllineacredito" runat="server" CssClass="textbox" Width="295px" AppendDataBoundItems="true">
                                                    <asp:ListItem Text="Seleccione un Item" Value=" " />
                                                </asp:DropDownList>
                                                <br />
                                            </td>
                                            <td style="text-align: left">
                                                <asp:DropDownList ID="ddlOficinas" runat="server" CssClass="textbox" Width="182px">
                                                </asp:DropDownList>
                                                <br />
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">Identificación
                                            </td>
                                            <td style="text-align: left">Nombre Completo
                                            </td>
                                            <td style="text-align: left">Código de nómina
                                            </td>
                                            <td style="text-align: left;">Fecha Reestructuración
                                            </td>
                                            <td style="text-align: left;">&nbsp
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="120px" /><br />
                                                <asp:CompareValidator ID="cvidentificacion" runat="server" ControlToValidate="txtidentificacion"
                                                    Display="Dynamic" ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck"
                                                    SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Style="font-size: x-small" />
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="290px" />
                                                <br />
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="182px" />
                                                <br />
                                            </td>
                                            <td style="text-align: left">
                                                <uc1:fecha ID="txtFecha" runat="server" autopostback="True" cssclass="textbox" Enabled="true"
                                                    maxlength="1" validationgroup="vgGuardar" Width="148px" />
                                                <br />
                                            </td>
                                            <td style="width: 30%">
                                                <asp:CheckBox Text="Créditos Mora 30- Días" ID="chkMoras" Checked="true" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;"></td>
                                            <td style="text-align: left;"></td>
                                            <td style="text-align: left;"></td>
                                            <td style="text-align: left;"></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <hr style="width: 100%" />
    <table style="width: 100%">
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Style="font-size: x-small" Width="100%"
                    GridLines="Horizontal" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging"
                    OnSelectedIndexChanged="gvLista_SelectedIndexChanged" OnRowEditing="gvLista_RowEditing"
                    PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                    RowStyle-CssClass="gridItem" DataKeyNames="numero_radicacion">
                    <Columns>
                        <asp:BoundField DataField="numero_radicacion" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                            <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                            <ItemStyle CssClass="gridColNo" HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                    ToolTip="Modificar" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            <ItemStyle CssClass="gridIco"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="numero_radicacion" HeaderText="Radicación">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Nombre completo">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="oficina" HeaderText="Oficina">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="plazo" HeaderText="Plazo">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="periodicidad" HeaderText="Periodicidad">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="forma_pago" HeaderText="Forma de pago">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cuotas_pagadas" HeaderText="Cuotas Pagadas" DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombres" HeaderText="Lineas Credito" DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <%--<asp:BoundField DataField="tipo_refinancia" HeaderText="Tipo Refinancia" DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="minimo_refinancia" HeaderText="Valor Minimo" DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="maximo_refinancia" HeaderText="Valor Maximo" DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="total_a_pagar" HeaderText="Total a Pagar"
                            DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor_para_refinanciar" HeaderText="Valor Para reestructurar"
                            DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <%--
                        <asp:BoundField DataField="estado" HeaderText="Estado">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>--%>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                    <PagerStyle CssClass="gridPager"></PagerStyle>
                    <RowStyle CssClass="gridItem"></RowStyle>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado" Visible="False" />
            </td>
        </tr>
    </table>
</asp:Content>
