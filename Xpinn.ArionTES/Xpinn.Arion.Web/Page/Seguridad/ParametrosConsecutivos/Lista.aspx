<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Consecutivo Oficinas :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                   <tr>
                       <td width="30%" style="text-align:left">
                           Tabla&nbsp;<br/>                         
                           <asp:DropDownList ID="ddltabla" runat="server" CssClass="textbox" Width="90%" >                   
                               <asp:ListItem Text="Seleccione un item" Value="0" Selected="True"/>
                               <asp:ListItem Text="CREDITO" Value="CREDITO" />
                               <asp:ListItem Text="COMPROBANTES" Value="COMPROBANTES"/>
                               <asp:ListItem Text="PERSONA" Value="PERSONA"/>
                               <asp:ListItem Text="APORTE" Value="APORTE"/>
                           </asp:DropDownList>
                       </td>
                       <td width="30%" style="text-align:left">
                           Oficina&nbsp;<br />
                           <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" Width="90%">
                               <asp:ListItem Text="Sin Datos" Value="0" />
                           </asp:DropDownList>
                       </td>
                       <td width="40%">
                           &nbsp;
                       </td>
                   </tr>
                </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td><hr width="100%"/></td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" onselectedindexchanged="gvLista_SelectedIndexChanged" onrowediting="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"  DataKeyNames="idconsecutivo" >
                    <Columns>
                        <asp:BoundField DataField="idconsecutivo" HeaderStyle-CssClass="gridColNo" 
                            ItemStyle-CssClass="gridColNo" >
                            <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                            <ItemStyle CssClass="gridColNo"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle"/>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            <ItemStyle CssClass="gridIco"></ItemStyle>
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
                                <HeaderStyle CssClass="gridIco" />
                            <ItemStyle CssClass="gridIco" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="idconsecutivo" HeaderText="C&oacute;digo" />
                        <asp:BoundField DataField="tabla" HeaderText="Tabla" />
                        <asp:BoundField DataField="columna" HeaderText="Columna" />
                        <asp:BoundField DataField="cod_oficina" HeaderText="Cod Oficina" />
                        <asp:BoundField DataField="nom_oficina" HeaderText="Nombre Oficina" />
                        <asp:BoundField DataField="rango_inicial"  HeaderText="Rango Inicial" />
                        <asp:BoundField DataField="rango_final" HeaderText="Rango Final" />
                        <asp:BoundField DataField="fechacreacion" HeaderText="F.Creación" />
                        <asp:BoundField DataField="usuariocreacion" HeaderText="U.Creación" />
                    </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
            </td>
        </tr>
    </table>
    <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtCod_opcion').focus();
        }
        window.onload = SetFocus;
    </script>
</asp:Content>