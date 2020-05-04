<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="ListaEn.aspx.cs" Inherits="ListaEn" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:panel runat="server" ID="panelenti" runat="Server">

        <table style="width: 100%" runat="server">
            <tr>
                <td style="height: 15px; text-align: left; font-size: x-small;" colspan="6" >
                <strong>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    Criterios de Búsqueda:</strong></td>
            </tr>
            <tr>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left; width: 20%">
                    Consecutivo<br />
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="95%" onkeypress="return isNumber(event)"  ></asp:TextBox>
                </td>
                <td style="text-align: left; width: 20%">
                    Identificación<br />
                    <asp:TextBox ID="txtNumeIdentificacion" CssClass="textbox"  runat="server"  Width="95%" onkeypress="return isNumber(event)" ></asp:TextBox>
                </td>
                <td style="text-align: left; width: 20%">
                    Nombres<br />
                    <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox"  Width="95%" onkeypress="return isString(event)"  ></asp:TextBox>
                 </td>
                 <td style="text-align: left; width: 20%">
                    Ciudad<br />
                    <asp:DropDownList ID="ddlCiudad" runat="server" CssClass="textbox" Width="95%" AppendDataBoundItems="True">
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: left; width: 20%">
                    Entidad<br />
                    <asp:DropDownList ID="ddlEntidad" runat="server" Width="95%"  CssClass="textbox" AppendDataBoundItems="True">
                      <asp:ListItem Value="">Seleccione Una Entidad</asp:ListItem>
                        <asp:ListItem Value="1">Prestadoras De Salud</asp:ListItem> 
                        <asp:ListItem Value="2">Fondo De Pensiones</asp:ListItem>
                        <asp:ListItem Value="3">Cesantias</asp:ListItem>
                        <asp:ListItem Value="4">Arl</asp:ListItem>
                         <asp:ListItem Value="5">ICBF</asp:ListItem>
                        <asp:ListItem Value="6">SENA</asp:ListItem>
                          
                    </asp:DropDownList>
                   
                </td>
           </tr>
        </table>
    </asp:panel>


    <table style="width:100%">  
        <tr>
            <td>  
        <asp:GridView ID="ENgrid" 
            runat="server" 
            AllowPaging="True"
            AutoGenerateColumns="False" 
            GridLines="Horizontal"
            PageSize="20"
            Width="100%" 
            ShowHeaderWhenEmpty="True"
            OnSelectedIndexChanged="ENgrid_SelectedIndexChanged"
            DataKeyNames="consecutivo" 
            OnRowDeleting="ENgrid_RowDeleting" 
            OnRowDataBound = "ENgrid_RowDataBound"
            OnRowEditing="ENgrid_RowEditing"
            style="font-size: x-small">
                    <Columns>                   
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                    ToolTip="Editar" Width="16px" />
                            </ItemTemplate>

                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                    ToolTip="Eliminar" Width="16px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>

                        <asp:BoundField DataField="consecutivo" HeaderText="Código" ItemStyle-HorizontalAlign="Center">

                        </asp:BoundField>
                        <asp:BoundField DataField="nom_persona" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center">

                        </asp:BoundField>
                        <asp:BoundField DataField="nit" HeaderText="Nit" ItemStyle-HorizontalAlign="Center">

                        </asp:BoundField>
                        <asp:BoundField DataField="email" HeaderText="E-mail" ItemStyle-HorizontalAlign="Center">

                        </asp:BoundField>
                        <asp:BoundField DataField="clase" HeaderText="clase" ItemStyle-HorizontalAlign="Center" >

                        </asp:BoundField>
                        <asp:BoundField DataField="direccion" HeaderText="Dirección" ItemStyle-HorizontalAlign="Center">

                        </asp:BoundField>
                        <asp:BoundField DataField="ciudad" HeaderText="Ciudad" ItemStyle-HorizontalAlign="Center">

                        </asp:BoundField>
                        <asp:BoundField DataField="telefono" HeaderText="Teléfono" ItemStyle-HorizontalAlign="Center">

                        </asp:BoundField>
                        <asp:BoundField DataField="responsable" HeaderText="Responsable" ItemStyle-HorizontalAlign="Center" >

                        </asp:BoundField>
                        <asp:BoundField DataField="codigociiu" HeaderText="Código De CIIU" ItemStyle-HorizontalAlign="Center" >

                        </asp:BoundField>
                        <asp:BoundField DataField="codigopila" HeaderText="Código De Pila" ItemStyle-HorizontalAlign="Center" >

                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                 <asp:Label ID="lblTotalRegs" runat="server" />
               </td>
             </tr>
            </table>
    <uc4:mensajegrabar ID="ctlMensajeBorrar" runat="server" />
</asp:Content>