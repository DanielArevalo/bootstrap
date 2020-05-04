<%@ Page Title="Expinn - Estructura Archivos" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>  

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    

   
    <asp:Panel ID="panelActividadReg" runat="server" BackColor="White" Style="text-align: right" Width="80%" >
        <div id="popupcontainer" style="width: 100%">
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: left; width: 20%">
                        Código
                        <br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="60%" MaxLength="7"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 80%">
                        Nombre o Descripción<br />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="70%">
                        </asp:TextBox>
                    </td>
                </tr>
            </table>            
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2" style="text-align: left; width: 100%; height: 19px;">
                            <hr style="width: 100%" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="2">
                        
                        <asp:Panel ID="panelGrilla" runat="server">
                        <strong>Estructuras:</strong><br />
                            <asp:GridView ID="gvLista" runat="server" Width="90%" PageSize="15" ShowHeaderWhenEmpty="True"
                                AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: x-small;
                                margin-bottom: 0px;" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging"
                                DataKeyNames="idestructuraextracto" OnRowEditing="gvLista_RowEditing" GridLines="Horizontal"
                                OnRowDeleting="gvLista_RowDeleting">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True">
                                        <ItemStyle HorizontalAlign="center"/>
                                    </asp:CommandField>
                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="true">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:CommandField>
                                    <asp:BoundField DataField="idestructuraextracto" HeaderText="Código">
                                        <ItemStyle HorizontalAlign="Left"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                        <ItemStyle HorizontalAlign="Left"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cod_banco" HeaderText="Cod Banco">
                                        <ItemStyle HorizontalAlign="Left"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_banco" HeaderText="Banco">
                                        <ItemStyle HorizontalAlign="Left"/>
                                    </asp:BoundField>
                                     <asp:BoundField DataField="nom_tipoarchivo" HeaderText="Tipo de Archivo">
                                        <ItemStyle HorizontalAlign="Left"/>
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                <RowStyle CssClass="gridItem" />
                                <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                            </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="2">
                            <asp:Label ID="lblTotalRegs" runat="server" Visible="False" Style="text-align: center" />
                            <asp:Label ID="Label2" runat="server" Text="Su consulta no obtuvo ningun resultado."
                                Visible="False" />
                        </td>
                    </tr>
                </table>            
        </div>
    </asp:Panel>

   <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>
  
</asp:Content>