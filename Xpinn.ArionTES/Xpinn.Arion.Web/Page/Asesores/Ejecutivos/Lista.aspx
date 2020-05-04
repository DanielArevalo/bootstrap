<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/Imprimir.ascx" TagName="imprimir" TagPrefix="ucImprimir" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                    <table cellpadding="5" cellspacing="0" style="width: 100%; text-align:left">
                        <tr>
                            <td class="tdI" colspan="3" style="font-size: x-small">
                                <strong>Ingresar datos de búsqueda y presione botón de consultar o presione 
                                botón NUEVO si desea crear un ejecutivo</strong></td>
                            <td class="tdI" style="font-size: x-small">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="tdI" style="width: 328px">
                                Identificación<br />
                                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td class="tdI" style="width: 328px">
                                Primer Nombre Ejecutivo<br />
                                <asp:TextBox ID="txtPrimerNombre" runat="server" CssClass="textbox" 
                                    Width="306px"></asp:TextBox>
                            </td>
                            <td class="tdD" style="width: 221px">
                                Primer Apellido Ejecutivo<br />
                                <asp:TextBox ID="txtPrimerApellido" runat="server" CssClass="textbox" 
                                    Width="307px"></asp:TextBox>
                            </td>
                            <td class="tdD">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="tdI" style="width: 328px">
                                &nbsp;</td>
                            <td class="tdI" style="width: 328px">
                                Nombre Oficina<br />
                                <asp:DropDownList ID="ddlOficina" runat="server" CssClass="dropdown" 
                                    Height="25px" Width="307px" />
                                <br />
                            </td>
                            <td class="tdD" style="width: 221px">
                                Estado<br />
                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="dropdown" 
                                    Height="25px" Width="155px" />
                                <br />
                            </td>
                            <td class="tdD">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="tdI" style="width: 328px">
                                &nbsp;</td>
                            <td class="tdI" style="width: 328px">
                                <br />
                            </td>
                            <td class="tdD" style="width: 221px"></td>
                            <td class="tdD">
                                &nbsp;</td>
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
            <td style="font-size: x-small">
                <strong>Listado de Asesores:</strong></td>
        </tr>
        <tr>
            <td>
                &nbsp;<ucImprimir:imprimir ID="ucImprimir" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                    AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
                    OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    OnRowDataBound="gvLista_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="IdEjecutivo" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo"></asp:BoundField>                        
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"  ToolTip="Borrar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NumeroDocumento" HeaderText="Número Documento" />
                        <asp:BoundField DataField="NombreTipoDocumento" HeaderText="Tipo Documento" />
                        <asp:BoundField DataField="PrimerNombre" HeaderText = "Primer Nombre" />
                        <asp:BoundField DataField="SegundoNombre" HeaderText="Segundo Nombre"/>
                        <asp:BoundField DataField="PrimerApellido" HeaderText="Primer Apellido" />
                        <asp:BoundField DataField="SegundoApellido" HeaderText="Segundo Apellido" />
                         <asp:BoundField DataField="NombreOficina" HeaderText="Oficina" />
                        <asp:BoundField DataField="NombreEstado" HeaderText="Estado" />
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
    <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtCodigo').focus();
        }
        window.onload = SetFocus;
    </script>
</asp:Content>