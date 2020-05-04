<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="../../../General/Controles/decimalesGrid.ascx" TagName="decimalesgrid"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript">
        var TotalChkBx;
        var Counter;

        window.onload = function () {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.gvLista.Rows.Count %>');
            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;
        }

        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.gvLista.ClientID %>');
            var TargetChildControl = "chkBxSelect";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[n].checked = CheckBox.checked;
            //Reset Counter
            Counter = CheckBox.checked ? TotalChkBx : 0;
        }

        function ChildClick(CheckBox, HCheckBox) {
            //get target base & child control.
            var HeaderCheckBox = document.getElementById(HCheckBox);

            //Modifiy Counter;            
            if (CheckBox.checked && Counter < TotalChkBx)
                Counter++;
            else if (Counter > 0)
                Counter--;

            //Change state of the header CheckBox.
            if (Counter < TotalChkBx)
                HeaderCheckBox.checked = false;
            else if (Counter == TotalChkBx)
                HeaderCheckBox.checked = true;
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
    </asp:Panel>
    <asp:MultiView ID="mvAprobacionScoringNegados" runat="server">
        <asp:View ID="View1" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td>
                        No. Crédito
                        <asp:CompareValidator ID="cvNoCredito" runat="server" ControlToValidate="txtCredito"
                            Display="Dynamic" ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck"
                            SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                        <br />
                        <asp:TextBox ID="txtCredito" runat="server" CssClass="textbox" Width="190px"></asp:TextBox>
                    </td>
                    <td>
                        Nombre del Cliente<br />
                        <asp:TextBox ID="txtCliente" runat="server" CssClass="textbox" Width="190px"></asp:TextBox>
                        <br />
                    </td>
                    <td>
                        Identificación<br />
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="190px"></asp:TextBox>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Parametrizar Motivos" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <hr noshade width="100%"></hr>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left">
                        <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                            OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                            ShowHeaderWhenEmpty="True" Style="text-align: left" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="NumeroSolicitud" HeaderStyle-HorizontalAlign="Center"
                                    HeaderText="No.Crédito" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Identificacion" HeaderStyle-HorizontalAlign="Center" HeaderText="Identificación"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Nombres" HeaderStyle-HorizontalAlign="Center" HeaderText="Nombres"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Monto" DataFormatString="{0:C}" HeaderStyle-HorizontalAlign="Center"
                                    HeaderText="Monto" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Plazo" HeaderStyle-HorizontalAlign="Center" HeaderText="Plazo"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="puntscoring" HeaderStyle-HorizontalAlign="Center" HeaderText="Punt.Scoring"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkBxSelect" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    <HeaderTemplate>
                                        <asp:Label ID="lblHeader" runat="server" Text="Aprobación"></asp:Label>
                                        <asp:CheckBox ID="chkBxHeader" runat="server" onclick="javascript:HeaderClick(this);" />
                                    </HeaderTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
                            Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left;">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left; font-weight: 700;">
                        Motivos de Aprobación de Scoring Negados
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: left">
                        Motivo<br />
                        <asp:DropDownList ID="ddlMotivo" runat="server" CssClass="dropdown">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left">
                        Observación<br />
                        <asp:TextBox ID="txtObservacion" runat="server" CssClass="textbox" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <table width="100%">
                <tr>
                    <td>
                        <asp:GridView ID="gvMotivos" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="ListaId"
                            ForeColor="Black" GridLines="Vertical" Height="10px" OnRowCancelingEdit="gvMotivos_RowCancelingEdit"
                            OnRowCommand="gvMotivos_RowCommand" OnRowDataBound="gvMotivos_RowDataBound" OnRowDeleting="gvMotivos_RowDeleting"
                            OnRowEditing="gvMotivos_RowEditing" OnRowUpdating="gvMotivos_RowUpdating" PageSize="5"
                            ShowFooter="True" Style="font-size: xx-small" Width="97%">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField ShowHeader="False">
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="btnActualizar0" runat="server" CommandName="Update" ImageUrl="~/Images/gr_guardar.jpg"
                                            ToolTip="Actualizar" Width="16px" />
                                        <asp:ImageButton ID="btnCancelar0" runat="server" CommandName="Cancel" ImageUrl="~/Images/gr_cancelar.jpg"
                                            ToolTip="Cancelar" Width="16px" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:ImageButton ID="btnNuevo0" runat="server" CausesValidation="False" CommandName="AddNew"
                                            ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar0" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                            ToolTip="Editar" Width="16px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                <asp:TemplateField HeaderText="IDSCOREVAR" Visible="false">                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdscorevar" runat="server" Text='<%# Bind("ListaId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             
                              
                                <asp:TemplateField HeaderText="Descripción">
                                    <EditItemTemplate>                                       
                                        <asp:TextBox ID="txtDescripcionE" runat="server" Text='<%# Bind("ListaDescripcion") %>' />
                                    </EditItemTemplate>
                                    <FooterTemplate>                                      
                                        <asp:TextBox ID="txtDescripcionF" runat="server" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescripcion" runat="server" Enabled="False" Text='<%# Bind("ListaDescripcion") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                              
                            </Columns>
                            <FooterStyle CssClass="gridHeader" />
                            <HeaderStyle CssClass="gridHeader" HorizontalAlign="Center" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <asp:Label ID="lblTotalRegs0" runat="server" Visible="False" />
                    <asp:Label ID="lblInfo0" runat="server" Text="Su consulta no obtuvo ningun resultado."
                        Visible="False" />
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
