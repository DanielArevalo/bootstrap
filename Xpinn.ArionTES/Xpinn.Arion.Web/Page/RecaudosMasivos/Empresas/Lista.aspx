<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>  
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            
            <table style="width: 100%">
                <tr>
                    <td>
                        <div style="overflow: scroll; height: 500px; width: 100%;">
                            <div style="width: 100%;">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                <asp:GridView ID="gvLista" runat="server" Width="100%" PageSize="15"
                                    ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" 
                                    SelectedRowStyle-Font-Size="XX-Small" 
                                    Style="font-size: x-small; margin-bottom: 0px;" AllowPaging="True" 
                                    onpageindexchanging="gvLista_PageIndexChanging" 
                                    onselectedindexchanged="gvLista_SelectedIndexChanged" 
                                    DataKeyNames="cod_empresa" onrowediting="gvLista_RowEditing" 
                                    GridLines="Horizontal">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" 
                                            ShowEditButton="True" />
                                        <asp:BoundField DataField="cod_empresa" HeaderText="Código" />
                                        <asp:BoundField DataField="nom_empresa" HeaderText="Nombre" >
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Estado">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEstado" runat="server" Checked='<%#Convert.ToBoolean(Eval("estado"))%>' Enabled ="false" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="direccion" HeaderText="Dirección" > 
                                            <ItemStyle HorizontalAlign="Left" /> 
                                            </asp:BoundField>
                                        <asp:BoundField DataField="telefono" HeaderText="Teléfono" >
                                            <ItemStyle HorizontalAlign="Left" /> 
                                            </asp:BoundField>                                       
                                        <asp:BoundField DataField="responsable" HeaderText="Responsable" >
                                         <ItemStyle HorizontalAlign="Left" /> 
                                            </asp:BoundField>      
                                        <asp:BoundField DataField="nomtipo_novedad" HeaderText="Tipo de Novedad" HeaderStyle-Width="60px" >
                                         <ItemStyle HorizontalAlign="center" /> 
                                            </asp:BoundField>      
                                        <asp:BoundField DataField="dias_novedad" HeaderText="Días de Novedad" HeaderStyle-Width="60px" >
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Fecha Convenio"  DataFormatString="{0:d}" 
                                            DataField="fecha_convenio" >
                                        </asp:BoundField>    
                                        <asp:BoundField DataField="aplica_novedades" HeaderText="Aplica Novedades" HeaderStyle-Width="60px">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>    
                                        <asp:BoundField DataField="mayores_valores" HeaderText="Mayores Valores" HeaderStyle-Width="60px">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="debito_automatico" HeaderText="Débito Automático" HeaderStyle-Width="60px">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" />
                                    <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                                </asp:GridView>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        </div>
                        <asp:Label ID="Label2" runat="server" Text="Su consulta no obtuvo ningun resultado."
                            Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>

    <asp:Label ID="msg" runat="server" Font-Bold="true" ForeColor="Red" />

</asp:Content>