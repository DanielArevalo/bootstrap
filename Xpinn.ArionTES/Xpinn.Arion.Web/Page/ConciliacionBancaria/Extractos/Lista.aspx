<%@ Page Title="Expinn - Extructura" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">            
        <table cellpadding="5" cellspacing="0" style="width: 790px">
           
            <tr>
                <td style="text-align: left; font-size: x-small;" colspan="5">
                    <strong>Criterios de Búsqueda:</strong></td>
            </tr>
            <tr>
                <td style="text-align: left ; width:120px;">
                    Código<br />
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="90%"></asp:TextBox>
                </td>
                <td style="text-align: left; width:200px">
                    Banco<br />
                    <asp:DropDownList ID="ddlBanco" runat="server" CssClass="textbox" Width="95%" />
                </td>
                <td style="text-align: left;width:160px">
                    Cuenta<br />
                    <asp:DropDownList ID="ddlCuenta" runat="server" CssClass="textbox" Width="90%" />
                </td>
                <td style="text-align: left; width:150px">
                   Periodo<br />
                   <asp:TextBox ID="txtPeriodo" runat="server" CssClass="textbox" Width="90%" />
                </td>
                <td style="text-align: left;width:160px">
                    Estado<br />
                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox" Width="90%" />
                </td>
            </tr>            
        </table>
         <hr style="width:100%" />
    </asp:Panel>
    <table style="width:100%">
        <tr>  
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="80%" AutoGenerateColumns="False"
                    AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
                    OnRowEditing="gvLista_RowEditing" DataKeyNames="idextracto">
                    <Columns>                        
                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" 
                            ShowEditButton="True" />
                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" 
                            ShowDeleteButton="True" />
                        
                        <asp:BoundField DataField="idextracto" HeaderText="Código" >
                            <ItemStyle HorizontalAlign="Left"/>
                        </asp:BoundField>
                         <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombanco" HeaderText="Banco" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="numero_cuenta" HeaderText="Cuenta" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="periodo" HeaderText="Periodo" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="saldo_anterior" HeaderText="Saldo Inicial" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nomestado" HeaderText="Estado" >
                            <ItemStyle HorizontalAlign="Left" />
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

    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>

</asp:Content>
