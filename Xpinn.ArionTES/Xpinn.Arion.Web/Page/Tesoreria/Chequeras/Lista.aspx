<%@ Page Title="Expinn - Chequeras" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>  

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    

   
    <asp:Panel ID="panelActividadReg" runat="server" BackColor="White" Style="text-align: right" Width="100%" >
        <div id="popupcontainer" style="width: 100%">
            <table style="width: 100%;">
                 <tr>
                        <td style="text-align: left" colspan="2">
                            <strong>Criterios de Búsqueda :</strong>
                        </td>
                    </tr>
                <tr>
                    <td style="text-align: left; width: 30%">
                        Cuenta
                        <br />
                        <asp:DropDownList ID="ddlCuenta" runat="server" CssClass="textbox" Width="80%" 
                            AppendDataBoundItems="True">
                            <asp:ListItem Value="0">Seleccione un item</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 40%">
                        Banco<br />
                         <asp:DropDownList ID="ddlBanco" runat="server" CssClass="textbox" Width="80%">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 30%">
                        Estado<br />
                         <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox" Width="80%">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="panelGrilla" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2" style="text-align: left; width: 100%; height: 19px;">
                            <hr style="width: 100%" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="2">
                            <strong>Listado de Chequeras :</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="2">
                            <asp:GridView ID="gvLista" runat="server" Width="100%" PageSize="15" ShowHeaderWhenEmpty="True"
                                AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: x-small;
                                margin-bottom: 0px;" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging"
                                DataKeyNames="idchequera" OnRowEditing="gvLista_RowEditing" GridLines="Horizontal"
                                OnRowDeleting="gvLista_RowDeleting">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True">
                                        <ItemStyle HorizontalAlign="center" Width="4%" />
                                    </asp:CommandField>
                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="true">
                                        <ItemStyle HorizontalAlign="center" Width="4%" />
                                    </asp:CommandField>
                                    <asp:BoundField DataField="idchequera" HeaderText="Código">
                                        <ItemStyle HorizontalAlign="Left" Width="" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="num_cuenta" HeaderText="Num. Cuenta">
                                        <ItemStyle HorizontalAlign="Left" Width="" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombrebanco" HeaderText="Banco">
                                        <ItemStyle HorizontalAlign="Left" Width="" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Prefijo" DataField="prefijo" />
                                    <asp:BoundField HeaderText="Cheque Inicial" DataField="cheque_ini" />
                                    <asp:BoundField HeaderText="Cheque Final" DataField="cheque_fin" />
                                    <asp:BoundField HeaderText="Num. Sig Cheque" DataField="num_sig_che" />
                                    <asp:BoundField DataFormatString="{0:d}" HeaderText="Fecha" 
                                        DataField="fecha_entrega" />
                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                <RowStyle CssClass="gridItem" />
                                <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                            </asp:GridView>
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
            </asp:Panel>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:ModalPopupExtender ID="mpeConfirmar" runat="server" PopupControlID="panelEliminar"
        TargetControlID="HiddenField1" BackgroundCssClass="backgroundColor">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelEliminar" runat="server" BackColor="White" Style="text-align: right"
        BorderWidth="1px" Width="500px">
        <div id="Div1" style="width: 500px">
            <table style="width: 100%;">
                <tr>
                    <td colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">
                        Esta Seguro de Eliminar el Registro Seleccionado?
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="text-align: center">
                        <asp:Button ID="btnContinuar" runat="server" Text="Continuar" CssClass="btn8" Width="182px"
                            OnClick="btnContinuar_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnParar" runat="server" Text="Cancelar" CssClass="btn8" Width="182px"
                            OnClick="btnParar_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
  
</asp:Content>