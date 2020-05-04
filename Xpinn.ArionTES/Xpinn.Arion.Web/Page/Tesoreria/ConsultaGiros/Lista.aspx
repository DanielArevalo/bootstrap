<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="85%">
            <tr>
                <td class="tdI" style="text-align:left;">
                    Código<br/>
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" />
                </td>
                <td class="tdD" style="text-align:left; width: 157px;">
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
                <td class="tdD" style="text-align:left">
                    Fecha Aprobación<br />
                    <uc1:fecha ID="txtFechaAprobacion" runat="server"></uc1:fecha>                
                </td>                 
                <td class="tdD" style="text-align:left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="tdI" style="text-align:left;">Estado<br />
                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox" Width="100%">
                    </asp:DropDownList>
                </td>
                <td class="tdD" style="text-align:left"><span style="color: rgb(64, 65, 66); font-family:Segoe UI, Arial, Helvetica, Verdana, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Primer Nombre</span><br/><asp:TextBox ID="txtPrimerNombre" runat="server" CssClass="textbox" MaxLength="128" Width="120px" />
                </td>
                <td class="tdD" style="text-align:left"><span style="color: rgb(64, 65, 66); font-family:Segoe UI, Arial, Helvetica, Verdana, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Segundo Nombre </span><br/><asp:TextBox ID="txtSegundoNombre" runat="server" CssClass="textbox" MaxLength="128" Width="120px" />
                </td>
                <td class="tdD" style="text-align:left"><span style="color: rgb(64, 65, 66); font-family:Segoe UI, Arial, Helvetica, Verdana, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Primer Apellido </span><br/><asp:TextBox ID="txtPrimerApellido" runat="server" CssClass="textbox" MaxLength="128" Width="120px" />
                </td>
                <td class="tdD" style="text-align:left"><span style="letter-spacing: normal; background-color: #FFFFFF">Segundo</span><span style="color: rgb(64, 65, 66); font-family: Segoe UI, Arial, Helvetica, Verdana, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;"> Apellido<asp:TextBox ID="txtSegundoApellido" runat="server" CssClass="textbox" MaxLength="128" Width="120px" />
                </td>
                <td class="tdD" style="text-align:left">Ordenado Por<br />
                    <asp:DropDownList ID="ddlOrdenar" runat="server" CssClass="textbox" Width="200px">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="1">IdGiro</asp:ListItem>
                        <asp:ListItem Value="2">Nombres</asp:ListItem>
                        <asp:ListItem Value="3">Fecha</asp:ListItem>
                        <asp:ListItem Value="4">Identificación</asp:ListItem>
                        <asp:ListItem Value="5">Estado</asp:ListItem>
                        <asp:ListItem Value="6">Primer Nombre</asp:ListItem>
                        <asp:ListItem Value="7">Segundo Nombre</asp:ListItem>
                        <asp:ListItem Value="8">Primer Apellido</asp:ListItem>
                        <asp:ListItem Value="9">Segundo Apellido</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="tdD" style="text-align:left">&nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
    <hr />
        <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
            onclick="btnExportar_Click" Text="Exportar a Excel" />
        <br />
        <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
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
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
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
                  <asp:BoundField DataField="identificacion_beneficiario" HeaderText="Beneficiario_Ide" >
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                 <asp:BoundField DataField="nombre_beneficiario" HeaderText="Beneficiario_Dis" >
                    <ItemStyle HorizontalAlign="Right" />
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
