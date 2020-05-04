<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Crear Mensajes App :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <table border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td class="tdD" colspan="2">Descripción<br />
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" Width="350px" />
                <br />
                <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" />
                <br />
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 140px">Personas<br />
                <asp:TextBox ID="txtCodPersona" runat="server" CssClass="textbox" ReadOnly="True"
                    Width="50px" Visible="false" />
                <asp:TextBox ID="txtIdPersona" runat="server" CssClass="textbox" AutoPostBack="true"
                    Width="100px" OnTextChanged="txtIdPersona_TextChanged" />
                <asp:FilteredTextBoxExtender ID="fte121" runat="server" TargetControlID="txtIdPersona"
                    FilterType="Custom, Numbers" ValidChars="-" />
                <asp:Button ID="btnConsultaPersonas" runat="server" CssClass="btn8" Height="26px"
                    OnClick="btnConsultaPersonas_Click" Text="..." />
            </td>
            <td style="text-align: left; width: 420px" colspan="3">Nombre<br />
                <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" OneventotxtIdentificacion_TextChanged="txtIdPersona_TextChanged" />
                <asp:TextBox ID="txtNomPersona" runat="server" CssClass="textbox" ReadOnly="True"
                    Width="350px" />
                <asp:RequiredFieldValidator ID="rfvEncargado" runat="server" ControlToValidate="txtNomPersona"
                    Display="Dynamic" ErrorMessage="Seleccione encargado" ForeColor="Red" InitialValue="0"
                    Style="font-size: xx-small" ValidationGroup="vgGuardar" />

                <asp:LinkButton OnClick="btnGuardarTemp_Click" runat="server" CssClass="btn btn-sm btn-outline-info">
                        <i class="fa fa-cloud-download">
                            &nbsp;
                        </i>Agregar
                </asp:LinkButton>
               
            </td>
        </tr>
        <tr>            
            <td colspan="2">
                <br />
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="documento">
                    <Columns>
                        <asp:BoundField DataField="documento" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Borrar" />
                            </ItemTemplate>
                            <ItemStyle CssClass="gI" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="documento" HeaderText="Documento" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
            </td>
        </tr>
    </table>
    <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtDescripcion').focus();
        }
        window.onload = SetFocus;
    </script>
</asp:Content>
