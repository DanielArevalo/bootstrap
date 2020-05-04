<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="70%">
            <tr>
                <td class="tdI" style="text-align:left;">
                    Código<br/>
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" />
                </td>
                <td class="tdD" style="text-align:left">
                    Identificación<br/>
                    <asp:TextBox ID="txtIdentific" runat="server" CssClass="textbox" 
                        MaxLength="128" Width="120px" />
                </td>
                <td class="tdD" style="text-align:left">
                    Num.Radic<br/>
                    <asp:TextBox ID="txtNumRadic" runat="server" CssClass="textbox" 
                        MaxLength="128" Width="120px" />
                    <asp:FilteredTextBoxExtender ID="txtNumRadic_FilteredTextBoxExtender" runat="server" Enabled="True" 
                        FilterType="Numbers, Custom" TargetControlID="txtNumRadic" ValidChars="." />
                </td>
                <td class="tdD" style="text-align:left">
                    Fecha Registro<br />
                    <uc1:fecha ID="txtFechaRegistro" runat="server"></uc1:fecha>                
                </td>
                <td class="tdD" style="text-align:left">
                    Fecha Giro<br />
                    <uc1:fecha ID="txtFechaGiro" runat="server"></uc1:fecha>                
                </td>
            </tr>
        </table>
    </asp:Panel>
    <hr />
        <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
            onclick="btnExportar_Click" Text="Exportar a Excel" />
        <br />
        <asp:GridView ID="gvLista" runat="server" Width="90%" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
            OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
            OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
            OnRowDataBound="gvLista_RowDataBound" DataKeyNames="idgiro" 
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
                <asp:BoundField DataField="idgiro" HeaderText="No." >
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="cod_persona" HeaderText="Cod.Per" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="identificacion" HeaderText="Identific." >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="nombre" HeaderText="Nombre" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="fec_reg" HeaderText="Fecha Registro" DataFormatString="{0:d}">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="nom_forma_pago" HeaderText="Forma Pago" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="fec_giro" HeaderText="Fecha Giro" DataFormatString="{0:d}">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="num_referencia" HeaderText="Cta Bancaria del Giro" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="nom_banco" HeaderText="Banco del Giro" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="cod_ope" HeaderText="Cod.Ope" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="numero_radicacion" HeaderText="No.Radic" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="num_comp" HeaderText="No.Comp" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="nom_tipo_comp" HeaderText="Tipo Comp" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="num_referencia1" HeaderText="Cta Bancaria Destino" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="nom_banco1" HeaderText="Banco Destino" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N2}">
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="nom_estado" HeaderText="Estado" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
            <HeaderStyle CssClass="gridHeader" />
            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
            <RowStyle CssClass="gridItem" />
        </asp:GridView>
        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
            Visible="False" />
    <br />
</asp:Content>
