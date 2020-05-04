<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            var dateDefault = today = new Date();
            var dateAc = dateDefault.getFullYear() + "-" + ((dateDefault.getMonth() < 9) ? "0" + (dateDefault.getMonth() + 1) : dateDefault.getMonth() + 1) + "-" + ((dateDefault.getDate() < 9) ? "0" + (dateDefault.getDate()) : dateDefault.getDate());
            document.getElementById("txtFina").value = dateAc;            
        });
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <table id="tbCriterios" border:"0" cellpadding:"0" cellspacing:"0" width:"70%">
            <tr>
                <td style="text-align: left">
                    <strong>Filtrar por:</strong>
                </td>
            </tr>
            <tr>
<%--                <td class="tdD" style="text-align: left; width: 110px">codigo Actulizacion<br />
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="110px" />
                </td>--%>
                <td class="tdD" style="text-align: left; width: 150px">Identificación<br />
                    <asp:TextBox ID="txtidentificacion" runat="server" CssClass="textbox"
                        MaxLength="100" Width="150px" />
                </td>
                 <td class="tdD" style="text-align: left; width: 150px">Primer Nombre<br />
                    <asp:TextBox ID="txtPrimerNom" runat="server" CssClass="textbox"
                        MaxLength="100" Width="150px" />
                </td>
                 <td class="tdD" style="text-align: left; width: 150px">Segundo Nombre<br />
                    <asp:TextBox ID="txtSeguNom" runat="server" CssClass="textbox"
                        MaxLength="100" Width="150px" />
                </td>
                 <td class="tdD" style="text-align: left; width: 150px">Primer Apellido<br />
                    <asp:TextBox ID="txtPrimerape" runat="server" CssClass="textbox"
                        MaxLength="100" Width="150px" />
                </td>
            </tr>
            <tr>
                <td class="tdD" style="text-align: left; width: 50px">Periodo<br />
                </td>
                  <td class="tdD" style="text-align: left; width: 120px">Fecha Inicial<br />
                     <asp:TextBox ID="txtfechaIni" type="date" runat="server" Width="170px" name="txtfechaIni"/>
                </td>
                   <td class="tdD" style="text-align: left; width: 120px">Fecha Final<br />
                     <asp:TextBox ID="txtFina" type="date" runat="server" Width="170px" name="txtFina"/>
                </td>
                <td class="tdD" style="text-align: left; width: 100px"><br />
                    <asp:CheckBox ID="CheckActualizados" runat="server" Width="100px" Text="Actualizados"/>
                </td>
                <td class="tdD" style="text-align: left; width: 150px"><br />
                    <asp:CheckBox ID="ChecknoActu" runat="server" Text="No Actualizados"  Width="150px"/>
                </td>
            </tr>

        </table>
    </asp:Panel>
    <hr />

    <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False"
        AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
        OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
        OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
        DataKeyNames="ID_UPDATE"
        Style="font-size: x-small">
        <Columns>
         <%--   <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True" />
            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />--%>
            <asp:BoundField DataField="Id_update" HeaderText="Código Actulización">
                <ItemStyle HorizontalAlign="Left" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="Identificacion" HeaderText="Identificación">
                <ItemStyle HorizontalAlign="Left" Width="200px" />
            </asp:BoundField>
            <asp:BoundField DataField="Primer_nombre" HeaderText="Primer nombre">
                <ItemStyle HorizontalAlign="Left" Width="300px" />
            </asp:BoundField>
             <asp:BoundField DataField="Segundo_nombre" HeaderText="Segundo nombre">
                <ItemStyle HorizontalAlign="Left" Width="300px" />
            </asp:BoundField>
             <asp:BoundField DataField="Primer_Apellido" HeaderText="Primer Apellido">
                <ItemStyle HorizontalAlign="Left" Width="300px" />
            </asp:BoundField>
                 <asp:BoundField DataField="Fecha_act" HeaderText="Fecha actualización">
                <ItemStyle HorizontalAlign="Left" Width="300px" />
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

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

</asp:Content>
