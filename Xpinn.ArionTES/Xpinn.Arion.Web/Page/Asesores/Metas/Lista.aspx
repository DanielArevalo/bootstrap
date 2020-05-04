<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>
  <%@ Register src="~/General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc2" %>
<%@ Register Src="~/General/Controles/imprimir.ascx" TagName="imprimir" TagPrefix="ucImprimir" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%">
        <tr> 
            <td>

                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                <ucImprimir:imprimir ID="ucImprimir" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Año<asp:DropDownList ID="DdlYear" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="DdlYear_SelectedIndexChanged">
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
            <td>
                Mes 
                <asp:DropDownList ID="DdlMes" runat="server">
                    <asp:ListItem Value="1">ENERO</asp:ListItem>
                    <asp:ListItem Value="2">FEBRERO</asp:ListItem>
                    <asp:ListItem Value="3">MARZO</asp:ListItem>
                    <asp:ListItem Value="4">ABRIL</asp:ListItem>
                    <asp:ListItem Value="5">MAYO</asp:ListItem>
                    <asp:ListItem Value="6">JUNIO</asp:ListItem>
                    <asp:ListItem Value="7">JULIO</asp:ListItem>
                    <asp:ListItem Value="8">AGOSTO</asp:ListItem>
                    <asp:ListItem Value="9">SEPTIEMBRE</asp:ListItem>
                    <asp:ListItem Value="10">OCTUBRE</asp:ListItem>
                    <asp:ListItem Value="11">NOVIEMBRE</asp:ListItem>
                    <asp:ListItem Selected="True"  Value="12">DICIEMBRE</asp:ListItem>
                </asp:DropDownList>
                </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;
                <asp:Label ID="Label1" runat="server" BackColor="White" ForeColor="#359AF2" 
                    Text="Label" Visible="False"></asp:Label>                                                                     
                <asp:Button ID="Button3" runat="server" CssClass="btn8" 
                    onclick="btnDescargarMetas_Click" Text="Descargar Metas"   Width="126px" />                                                                     
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
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                    AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" 
                    OnRowCommand="gvLista_RowCommand" OnRowDeleting="gvLista_RowDeleting" 
                    onselectedindexchanged="gvLista_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="IdEjecutivoMeta" HeaderStyle-CssClass="gridColNo" 
                            ItemStyle-CssClass="gridColNo">
<HeaderStyle CssClass="gridColNo"></HeaderStyle>

<ItemStyle CssClass="gridColNo"></ItemStyle>
                        </asp:BoundField>                       
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" 
                                ToolTip="Borrar" />
                            </ItemTemplate>

<HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/Images/gr_edit.jpg"
                                    ToolTip="Editar" CommandName="Editar"  CommandArgument='<%#Eval("meta")%>'/>
                            </ItemTemplate>

<HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PrimerNombre" HeaderText = "Primer Nombre" />
                        <asp:BoundField DataField="SegundoNombre" HeaderText="Segundo Nombre"/>
                        <asp:BoundField DataField="NombreOficina" HeaderText="Oficina" />
                        <asp:BoundField DataField="Mes" HeaderText="Mes" />
                        <asp:BoundField DataField="NombreMeta" HeaderText="Nombre Meta" />
                        <asp:BoundField DataField="vlrmeta" DataFormatString="{0:n}" 
                            HeaderText="Valor Meta" />
                        <asp:BoundField DataField="Year" HeaderText="Año" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>

                <br />

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
    <asp:Label ID="msg" runat="server" Font-Bold="true" ForeColor="Red" />
</asp:Content>