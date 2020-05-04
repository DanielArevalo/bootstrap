<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>    
    <br />
    <table border="0" style="width: 80%">
        <tr style="text-align: left">
            <td colspan="4">
                <strong>Seleccione el crédito para el cual corresponde la garantía</strong>
            </td>
        </tr>
        <tr>
            <td style="width: 209px; height: 51px;" class="logo">
                <asp:Label ID="Labelnumero_credito"
                    runat="server" Text="Número de Crédito"></asp:Label>
                <br />
                <asp:TextBox ID="txtNumCredito" CssClass="textbox" onkeypress="return isNumber(event)" runat="server" Width="180px">
                </asp:TextBox>
            </td>
            <td style="width: 209px; height: 51px;" class="logo">
                <asp:Label ID="lblIdentificacion"
                    runat="server" Text="Identificación"></asp:Label>
                <br />
                <asp:TextBox ID="txtIdentificacion" CssClass="textbox" onkeypress="return isNumber(event)" runat="server" Width="180px">
                </asp:TextBox>
            </td>
            <td style="width: 218px; height: 51px;" class="logo">&nbsp;
                <asp:Label ID="LabelLínea" runat="server" Text="Línea"></asp:Label>
                <br />
                <asp:DropDownList ID="ddlLineasCred" Width="180px" class="textbox" runat="server">
                </asp:DropDownList>
            </td>
            <td style="text-align: left;">
                <asp:Label ID="lbloficina" Text="Oficina" runat="server" /><br />
                <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox"
                    Width="180px" />
            </td>
        </tr>
    </table>
    <table border="0" style="height: 84px; width: 100%">
        <tr>
            <td>
                <asp:GridView ID="gvSinGarantia" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                    OnPageIndexChanging="gvSinGarantia_PageIndexChanging" AllowPaging="True" OnRowEditing="gvSinGarantias_RowEditing"
                    PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="NumeroRadicacion">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar" />
                            </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:BoundField DataField="NumeroRadicacion" HeaderText="Número Radicación" />
                        <asp:BoundField DataField="cod_persona" HeaderText="Cod.Persona" />
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación" />
                        <asp:BoundField DataField="nom_persona" HeaderText="Nombre" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="oficina" HeaderText="Oficina " DataFormatString="{0:N0}">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="monto" DataFormatString="{0:N0}" HeaderText="Monto" />
                        <asp:BoundField DataField="plazo" HtmlEncode="false" HeaderText="Plazo" />
                        <asp:BoundField DataField="cuota" DataFormatString="{0:N0}" HtmlEncode="false" HeaderText="Cuota" />
                        <asp:BoundField DataField="nombre_asesor" HeaderText="Nombre asesor" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblNumeroRegistros" Visible="false" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblAvisoNoResultadoGrilla" Visible="false" runat="server" Text="No hay resultados para la busqueda"></asp:Label>
</asp:Content>
