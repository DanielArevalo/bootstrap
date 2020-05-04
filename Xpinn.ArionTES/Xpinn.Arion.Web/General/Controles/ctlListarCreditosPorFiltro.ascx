<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlListarCreditosPorFiltro.ascx.cs" Inherits="ctlListarCreditosPorFiltro" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
<br />
<table border="0" style="width: 70%">
    <tr style="text-align: left">
        <td colspan="4">
            <strong>Seleccione el crédito </strong>
        </td>
    </tr>
    <tr>
        <td style="width: 20%; height: 51px;" class="logo">
            <asp:Label ID="Labelnumero_credito"
                runat="server" Text="Número de Crédito"></asp:Label>
            <br />
            <asp:TextBox ID="txtNumCredito" CssClass="textbox" onkeypress="return isNumber(event)" Width="90%" runat="server">
            </asp:TextBox>
        </td>
        <td style="width: 16%; height: 51px;" class="logo">&nbsp;
                <asp:Label ID="LabelLínea" runat="server" Text="Línea"></asp:Label>
            <br />
            <asp:DropDownList ID="ddlLineasCred" Width="90%" class="textbox" runat="server">
            </asp:DropDownList>
        </td>
        <td style="width: 16%; text-align: left;">
            <asp:Label ID="lbloficina" Text="Oficina" runat="server" /><br />
            <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox"
                Width="90%" />
        </td>
    </tr>
    <tr>
        <td style="width: 16%; height: 51px;" class="logo">
            <asp:Label ID="lblIdentificacion"
                runat="server" Text="Identificación"></asp:Label>
            <br />
            <asp:TextBox ID="txtIdentificacion" CssClass="textbox" onkeypress="return isNumber(event)" Width="90%" runat="server">
            </asp:TextBox>
        </td>
        <td style="width: 16%; height: 51px;" class="logo">
            <asp:Label ID="Label1"
                runat="server" Text="Primer Nombre"></asp:Label>
            <br />
            <asp:TextBox ID="txtPrimerNombre" CssClass="textbox" onkeypress="return isOnlyLetter(event)" Width="90%" runat="server">
            </asp:TextBox>
        </td>
        <td style="width: 16%; height: 51px;" class="logo">
            <asp:Label ID="Label2"
                runat="server" Text="Primer Apellido"></asp:Label>
            <br />
            <asp:TextBox ID="txtPrimerApellido" CssClass="textbox" onkeypress="return isOnlyLetter(event)" Width="90%" runat="server">
            </asp:TextBox>
        </td>
        <td class="tdI" style="text-align: left">
            <asp:Label ID="Label3"
                runat="server" Text="Código de nómina"></asp:Label>
            <br />
            <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="110px" />
        </td>
    </tr>
</table>
<table border="0" style="height: 84px; width: 100%">
    <tr>
        <td>
            <asp:GridView ID="gvSinGarantia" runat="server" Width="100%" AutoGenerateColumns="False"
                OnPageIndexChanging="gvSinGarantia_PageIndexChanging" AllowPaging="True" OnSelectedIndexChanged="gvSinGarantias_SelectIndexChanged"
                PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="numero_radicacion">
                <Columns>
                    <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEditar" runat="server" CommandName="Select" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="numero_radicacion" HeaderText="Num.Radi" />
                    <asp:BoundField DataField="nom_linea_credito" HeaderText="Línea" />
                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                    <asp:BoundField DataField="cod_persona" HeaderText="Cod.Persona" />
                    <asp:BoundField DataField="identificacion" HeaderText="Identificación" />
                    <asp:BoundField DataField="nombres" HeaderText="Nombre" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="oficina" HeaderText="Oficina">
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="monto" DataFormatString="${0:#,##0.00}" HeaderText="Monto" />
                    <asp:BoundField DataField="saldo_capital" DataFormatString="${0:#,##0.00}" HeaderText="Saldo Capital" />
                    <asp:BoundField DataField="plazo" HtmlEncode="false" HeaderText="Plazo" />
                    <asp:BoundField DataField="valor_cuota" DataFormatString="${0:#,##0.00}" HtmlEncode="false" HeaderText="Cuota" />
                    <asp:BoundField DataField="cuotas_pagadas" DataFormatString="{0:N0}" HtmlEncode="false" HeaderText="Ctas Pagadas" />
                    <asp:BoundField DataField="NombreAsesor" HeaderText="Nombre asesor" />
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblNumeroRegistros" Visible="false" runat="server"></asp:Label>
        </td>
    </tr>
</table>
<asp:Label ID="lblAvisoNoResultadoGrilla" Visible="false" runat="server" Text="No hay resultados para la busqueda"></asp:Label>