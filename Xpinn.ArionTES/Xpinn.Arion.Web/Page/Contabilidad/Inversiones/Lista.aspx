<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <table id="tbCriterios" border="0" cellpadding="5" cellspacing="5">
            <tr>
                <td style="text-align:left">Código<br />
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" />
                </td>
                <td style="text-align:left">Número Título<br />
                    <asp:TextBox ID="txtNroTitulo" runat="server" CssClass="textbox" MaxLength="128" Width="200px" />
                </td>
                <td style="text-align: left">Fecha de Emisión<br />
                    <uc1:fecha ID="txtFechaEmi" runat="server" />
                </td>
                <td style="text-align: left">Tipo Inversión<br />
                    <asp:DropDownList ID="ddlTipoInv" runat="server" CssClass="textbox" Width="230px" />
                </td>
                <td style="text-align: left">Entidad<br />
                    <asp:TextBox ID="txtCodPersona" runat="server" Visible="false" />
                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="125px"
                        Style="text-align: left" />
                     <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="474px" Style="text-align: left"
                                                Visible="False"></asp:TextBox>
                    <asp:Button ID="btnConsultaPersonas" CssClass="btn8" runat="server" Text="..." Height="26px"
                        OnClick="btnConsultaPersonas_Click" />
                    <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <hr />

    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False" GridLines="Horizontal"
                    OnRowDeleting="gvLista_RowDeleting" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging"
                    OnRowEditing="gvLista_RowEditing" PageSize="20" DataKeyNames="cod_inversion">
                    <Columns>
                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True" />
                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                        <asp:BoundField DataField="cod_inversion" HeaderText="Código">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="numero_titulo" HeaderText="Nro Título">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor_capital" HeaderText="Vr Capital" DataFormatString="{0:n}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor_interes" HeaderText="Vr Intereses" DataFormatString="{0:n}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="plazo" HeaderText="Plazo">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_emision" HeaderText="Fecha Emisión" DataFormatString="{0:d}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_vencimiento" HeaderText="Fecha Vencimiento" DataFormatString="{0:d}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tasa_interes" HeaderText="Tasa Int.">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nom_tipo" HeaderText="Tipo Inversión">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombrebanco" HeaderText="Entidad Bancaria">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                    <PagerStyle CssClass="gridPager"></PagerStyle>
                    <RowStyle CssClass="gridItem"></RowStyle>
                </asp:GridView>                
            </td>            
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Visible="False" Text="Su consulta no obtuvo ningún resultado" />
            </td>
        </tr>
    </table>

    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>

</asp:Content>

