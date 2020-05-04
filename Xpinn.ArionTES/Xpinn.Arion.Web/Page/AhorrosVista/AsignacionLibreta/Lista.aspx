<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/ctlLineaAhorro.ascx" TagName="ddlLineaAhorro" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>  

    <asp:Panel ID="pConsulta" runat="server">            
        <table style="width: 100%">           
            <tr>
                <td>Numero Libreta
                  <asp:TextBox ID="txtNumeroLibreta" CssClass="textbox" runat="server"></asp:TextBox>
                  </td>
                <td>Oficina
                 <asp:DropDownList ID="ddlOficina" CssClass="textbox" runat="server">
                </asp:DropDownList>
                </td>
                <td>Fecha Asignacion
                  <asp:TextBox ID="txtAsignacion" CssClass="textbox" runat="server"></asp:TextBox> </td>
                <td>Numero Cuenta
                   <asp:TextBox ID="txtNumeroCuenta" CssClass="textbox" runat="server"></asp:TextBox></td>
                <td>Identificacion
                   <asp:TextBox ID="txtIdentificacion" CssClass="textbox" runat="server"></asp:TextBox></td>
                <td>Nombre
                    <asp:TextBox ID="txtNombre" CssClass="textbox" runat="server"></asp:TextBox></td>
                <td>Código de nómina
                    <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" ></asp:TextBox>
                </td>
                <td>Estado
                    <br />
                    <asp:DropDownList ID="ddlEstado" runat="server" 
                        CssClass="textbox" Width="100px" DataSource="<%#ListaEstadosLibreta() %>" 
                        DataTextField="descripcion" DataValueField="codigo" Enabled="true" 
                       >
                    </asp:DropDownList>
                   </td>
            </tr>
        </table>
    </asp:Panel>
    <table style="width:100%">
        <tr>  
            <td> Listados De Libretas
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                   AllowPaging="False" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                   OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
                   OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                   OnRowDataBound="gvLista_RowDataBound" DataKeyNames="id_Libreta"
                   style="font-size: xx-small">
                   <Columns>
                       <asp:TemplateField HeaderStyle-CssClass="gridIco">
                           <ItemTemplate>
                               <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                   ToolTip="Editar" />
                           </ItemTemplate>
                           <HeaderStyle CssClass="gridIco"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-CssClass="gridIco">
                           <ItemTemplate>
                               <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                   ToolTip="Eliminar"  />
                           </ItemTemplate>
                           <HeaderStyle CssClass="gridIco"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:BoundField DataField="id_Libreta" HeaderText="cod.Libreta" >
                           <ItemStyle HorizontalAlign="Left" />
                       </asp:BoundField>
                       <asp:BoundField DataField="numero_libreta" HeaderText="No.Libreta" >
                           <ItemStyle HorizontalAlign="Left"/>
                       </asp:BoundField>
                       <asp:BoundField DataField="desde" HeaderText="Desde" >
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="valor_libreta" HeaderText="Valor Libreta" >
                           <ItemStyle HorizontalAlign="Left" />
                       </asp:BoundField>
                       <asp:BoundField DataField="hasta" HeaderText="Hasta" >
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="cod_oficina" HeaderText="Oficina" >
                           <ItemStyle HorizontalAlign="Left"  />
                       </asp:BoundField>
                       <asp:BoundField DataField="fecha_asignacion" HeaderText="Fech. Asignacion" DataFormatString="{0:d}" >
                           <ItemStyle HorizontalAlign="Center"/>
                       </asp:BoundField>
                       <asp:BoundField DataField="numero_cuenta" HeaderText="Nom.Cuenta" >
                           <ItemStyle HorizontalAlign="Left" />
                       </asp:BoundField>
                       <asp:BoundField DataField="identificacion" HeaderText="Identificacion" >
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina">
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="nombre" HeaderText="Nombre" >
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="saldo_total" HeaderText="Saldo Total" >
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="estado" HeaderText="Estado" >
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>                       
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
            </td>
        </tr>        
    </table>
</asp:Content>
