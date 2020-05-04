<%@ Page Language="C#" MasterPageFile="~/site.master"AutoEventWireup="true"
     CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                    <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="80%">
                        <tr>
                            <td style="text-align: left">Número Identificación<br />
                                <asp:TextBox ID="txtNumIdentifi" runat="server" CssClass="textbox" />
                            </td>
                            <td style="text-align: left" visible="false"><%--Número Recaudo--%><br />
                                <asp:TextBox ID="txtNumeroRecaudo" runat="server" CssClass="textbox" visible="false" />
                            </td>
                            <td style="text-align: left" visible="false"><%--Fecha Periodo--%><br />
                                <ucFecha:fecha ID="txtFechaPeriodo" runat="server" visible="false"/>
                            </td>
                            <td style="text-align: left" ><%--Empresa--%><br />
                                <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="textbox" visible="false"
                                    Width="208px" />
                            </td>
                            <td>&nbsp;
                            </td>
                            <td style="text-align: left">
                                <asp:CheckBox ID="NomGenerada" runat="server" Checked="false" visible="false" Text="Nominas generadas sin aplicar"/>
                            </td>                            
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <hr />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="numero_recaudo">
                    <Columns>
                        <asp:BoundField DataField="numero_recaudo" HeaderStyle-CssClass="gridColNo"
                            ItemStyle-CssClass="gridColNo">
                            <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                            <ItemStyle CssClass="gridColNo"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Imagenes/gr_general.png" ToolTip="Consultar"  Height="20px" />
                            </ItemTemplate>   
                        </asp:TemplateField>
                        <asp:BoundField DataField="numero_recaudo" HeaderText="No.Recaudo" />
                        <asp:BoundField DataField="nom_tipo_recaudo" HeaderText="Tipo Recaudo" />
                        <asp:BoundField DataField="nom_empresa" HeaderText="Empresa" />
                        <asp:BoundField DataField="periodo_corte" HeaderText="Período"
                            DataFormatString="{0:d}" />
                        <asp:BoundField DataField="fecha_recaudo" HeaderText="Fecha Recaudo"
                            DataFormatString="{0:d}" />
                        <asp:BoundField DataField="fecha_aplicacion" HeaderText="Fecha Aplicacion"
                            DataFormatString="{0:d}" />
                        <asp:BoundField DataField="estado" HeaderText="Estado" />
                        <asp:BoundField DataField="nom_estado" HeaderText="Desc.Estado" />
                        <asp:BoundField DataField="usuario" HeaderText="Usuario" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                    <PagerStyle CssClass="gridPager"></PagerStyle>
                    <RowStyle CssClass="gridItem"></RowStyle>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblTotalRegs" runat="server" Visible="True" />
</asp:Content>
