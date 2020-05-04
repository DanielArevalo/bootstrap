<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="5" cellspacing="0" style="width: 80%">
        <tr>
            <td class="tdI">
            </td>
        </tr> 
        <tr>
            <td class="tdI" style="text-align:left">
                C&oacute;digo
                <asp:Label ID="lblCodigo" runat="server"></asp:Label>
            </td>
            <td class="tdI" style="text-align:left">
                Oficina<br />
                <asp:TextBox ID="txtOficina" runat="server" Enabled="False" CssClass="textbox" 
                    Width="250px"></asp:TextBox>
            </td>
            <td class="tdD" style="text-align:left">
                Fecha de Creación<br /> 
                <asp:TextBox ID="txtFechaCreacion" Enabled="False" runat="server" CssClass="textbox" 
                    MaxLength="10" Width="110px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align:left">
                Ciudad &#160;&#160;<br />
                <asp:DropDownList ID="ddlCiudades" enabled="false" CssClass="dropdown"  runat="server" 
                    Height="28px" Width="182px">
                </asp:DropDownList> 
            </td>
            <td style="text-align:left">
                Dirección<br />
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="textbox" Width="250px" 
                    Enabled="False" ></asp:TextBox>
            </td>
            <td style="text-align:left">
                Teléfono&#160;<br />
                <asp:TextBox ID="txtTelefono" runat="server" Enabled="False" CssClass="textbox" Width="113px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align:left">
                Centro de Costo&#160;<br />
                <asp:DropDownList ID="ddlCentrosCosto" Enabled="false" CssClass="dropdown"  runat="server" Height="26px" Width="182px">
                </asp:DropDownList> 
            </td>
            <td style="text-align:left">
                Encargado&#160;<br />
                <asp:DropDownList ID="ddlEncargados" Enabled="False" CssClass="dropdown"  
                    runat="server" Height="26px" Width="250px">
                </asp:DropDownList> 
            </td>
            <td style="text-align:left">
                Estado &#160<br />
                <asp:Label ID="lblEstado" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <table cellpadding="5" cellspacing="0" style="width: 100%">
        <tr>
            <td class="tdI" style="text-align:left">
                Cajas de la Oficina:   
            </td>
            <td class="tdD" align="right" >
                <asp:Button ID="btnAdicionar" runat="server" Text="Adicionar una Caja" 
                    Enabled="True" onclick="btnAdicionar_Click"/>
            </td>
        </tr>
         <tr>
            <td class="tdD" colspan="2">
                <asp:GridView ID="gvCajas" runat="server" Width="100%" 
                    AutoGenerateColumns="False" PageSize="20" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical" onrowediting="gvCajas_RowEditing" 
                    onrowdeleting="gvCajas_RowDeleting" OnSelectedIndexChanged="gvCajas_SelectedIndexChanged" onrowdatabound="gvCajas_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                    ToolTip="Editar" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                    ToolTip="Eliminar" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="cod_caja" HeaderText="Código" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="fecha_creacion" DataFormatString="{0:d}" HeaderText="Fecha Creación" />
                        <asp:BoundField DataField="state" HeaderText="Estado" />
                        <asp:BoundField DataField="cod_cuenta_contable" HeaderText="Cod.Cuenta" />
                        <asp:BoundField DataField="desc_cuenta_contable" HeaderText="Nom.Cuenta" />
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
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Horario Normal 
                <asp:Button ID="btnHorarioNormal" runat="server" Text="Agregar " 
                    onclick="btnHorarioNormal_Click" />
            </td>
            <td class="tdD">
                Horario Adicional
                <asp:Button ID="btnHorarioAdi" runat="server" Text="Agregar " 
                        onclick="btnHorarioAdi_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvHorarioNormal" runat="server"
                        AutoGenerateColumns="False" AllowPaging="False" PageSize="20" 
                        onrowdatabound="gvHorarioNormal_RowDataBound" 
                        onrowdeleting="gvHorarioNormal_RowDeleting" 
                        onrowediting="gvHorarioNormal_RowEditing">
                    <Columns>
                        <asp:BoundField DataField="cod_horario" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                    ToolTip="Editar" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                    ToolTip="Eliminar" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="nom_dia" HeaderText="Día" />
                        <asp:BoundField DataField="hora_inicial" DataFormatString="{0:t}" HeaderText="Hora Inicio" />
                        <asp:BoundField DataField="hora_final" DataFormatString="{0:t}" HeaderText="Hora Final" />
                    </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>       
            </td>
            <td>
                <asp:GridView ID="gvHorarioAdicional" runat="server" 
                    AutoGenerateColumns="False" AllowPaging="False" PageSize="20" 
                    onrowdatabound="gvHorarioAdicional_RowDataBound" 
                    onrowdeleting="gvHorarioAdicional_RowDeleting" 
                    onrowediting="gvHorarioAdicional_RowEditing">
                    <Columns>
                        <asp:BoundField DataField="cod_horario" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                    ToolTip="Editar" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                    ToolTip="Eliminar" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="nom_dia" HeaderText="Día" /> 
                        <asp:BoundField DataField="hora_inicial"  DataFormatString="{0:t}" HeaderText="Hora Inicio" />
                        <asp:BoundField DataField="hora_final"  DataFormatString="{0:t}" HeaderText="Hora Final" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
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




