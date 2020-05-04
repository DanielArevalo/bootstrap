<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
 
     
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <table style="width: 779px; height: 400px; margin-right: 25px;">
            <tr>
                <td style="font-size: x-small; height: 30px; text-align: center;" 
                    colspan="24">
                    <asp:Label ID="Lblerror" runat="server" ForeColor="Red" CssClass="align-rt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="font-size: x-small; height: 30px; width: 145px; text-align: left;">
                    Código del Plan:</td>
                <td style="font-size: x-small; height: 30px; width: 98px; text-align: left;">
                    <asp:TextBox ID="txtcodigoplan" runat="server" Width="129px" Enabled="False"></asp:TextBox>
                    </td>
                <td style="font-size: x-small; height: 30px; text-align: center;" 
                    colspan="12">
                    Nombre del Plan:</td>
                <td style="font-size: x-small; height: 30px; " colspan="5">
                    &nbsp;
                    <asp:TextBox ID="txtnombreplan" runat="server" Width="264px"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td style="font-size: x-small; height: 14px; width: 871px; text-align: left;" 
                    colspan="11">
                    &nbsp;</td>
                <td style="font-size: x-small; height: 14px; width: 10px; text-align: left;">
                    &nbsp;</td>
                <td style="font-size: x-small; height: 14px; text-align: left; width: 698px;" 
                    colspan="2">
                    &nbsp;</td>
                <td style="font-size: x-small; height: 14px; width: 28px; text-align: left;">
                    &nbsp;</td>
                <td style="font-size: x-small; height: 14px; width: 19px; text-align: left;">
                    &nbsp;</td>
                <td style="font-size: x-small; height: 14px; width: 19px; text-align: left;">
                    &nbsp;</td>
                <td style="font-size: x-small; height: 14px; width: 20px; text-align: left;">
                    &nbsp;</td>
                <td style="font-size: x-small; height: 14px; width: 20px; text-align: left;">
                    &nbsp;</td>
                <td style="font-size: x-small; height: 14px; width: 6px;">
                    &nbsp;</td>
                <td style="font-size: x-small; height: 14px; width: 8px;">
                    &nbsp;</td>
                <td style="font-size: x-small; height: 14px; width: 6px;">
                    &nbsp;</td>
                <td style="font-size: x-small; height: 14px; width: 34px;">
                    &nbsp;</td>
                <td style="font-size: x-small; height: 14px; text-align: center; width: 47px;">
                    &nbsp;</td>
            </tr>
              <tr>
                <td style="font-size: x-small; height: 15px; text-align: center; font-weight: 700;" 
                    colspan="24">
                    <strong><span style="font-size: small; color: #0099FF;">V</span></strong><span 
                    style="font-size: small; color: #0099FF;"><strong style="text-align: center">IDA GRUPO VOLUNTARIO -AMPAROS Y VALORES 
                ASEGURADOS</strong></span></td>
            </tr>
            <tr>
                <td style="font-size: x-small; height: 14px; text-align: center;" 
                    colspan="7">
                    Valor&nbsp; prima&nbsp;&nbsp; Mensual&nbsp;&nbsp; Individual</td>
                <td style="font-size: x-small; height: 14px; text-align: center;" 
                    colspan="5">
                    &nbsp;</td>
                <td style="font-size: x-small; height: 14px; text-align: center;" 
                    colspan="12">
                    Valor prima Mensual
                    conyuge a Primera
                    pérdida</td>
                </tr>
            <tr>
                <td style="font-size: x-small; text-align: center;" colspan="7">
                    <asp:TextBox ID="txtPrima_Ind" class="numeric" runat="server" Width="142px" 
                        style="margin-right: 0px; margin-left: 0px;"></asp:TextBox>
                    </td>
                <td style="font-size: x-small; text-align: center;" class="gridIco">
                    &nbsp;</td>
                <td style="font-size: x-small; text-align: center;" colspan="2">
                    &nbsp;</td>
                <td style="font-size: x-small; text-align: center;" colspan="4">
                    &nbsp;</td>
                <td style="font-size: x-small; text-align: center;" colspan="10">
                    <asp:TextBox ID="txtPrima_Cony" class="numeric" runat="server" Width="144px"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td class="logo" colspan="24">
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    
                    </strong>

                    <asp:GridView ID="gvVidaGrupo" runat="server" Width="97%" 
                    ShowHeaderWhenEmpty = "true"
                    AutoGenerateColumns="False" PageSize="5" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical" Height="12px" 
                        onselectedindexchanged="gvVidaGrupo_SelectedIndexChanged" 
                        DataKeyNames="consecutivo" onrowcancelingedit="gvVidaGrupo_RowCancelingEdit" 
                        onrowcommand="gvVidaGrupo_RowCommand" onrowdeleted="gvVidaGrupo_RowDeleted" 
                        onrowdeleting="gvVidaGrupo_RowDeleting" onrowediting="gvVidaGrupo_RowEditing" 
                        onrowupdated="gvVidaGrupo_RowUpdated" 
                        onrowupdating="gvVidaGrupo_RowUpdating" style="font-size: xx-small" 
                        ShowFooter="True">
                                               
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <EditItemTemplate>
                                &nbsp;<asp:ImageButton ID="btnActualizar" runat="server" CommandName="Update" 
                                    ImageUrl="~/Images/gr_guardar.jpg" ToolTip="Actualizar" Width="16px" />
                                <asp:ImageButton ID="btnCancelar" runat="server" CommandName="Cancel" 
                                    ImageUrl="~/Images/gr_cancelar.jpg" ToolTip="Cancelar" Width="16px" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:ImageButton ID="btnNuevo" runat="server" CausesValidation="False" 
                                    CommandName="AddNew" ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" 
                                    Width="16px" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                    ToolTip="Editar" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" 
                            DeleteText="" ShowDeleteButton="True" />
                        <asp:TemplateField HeaderText="Amparo">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtdescripcion" runat="server" 
                                    Text='<%# Bind("descripcion") %>' 
                                    ontextchanged="txtdescripcion_TextChanged"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtnewdescripcion" runat="server" 
                                    Text='<%# Bind("descripcion") %>'></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbldescripcion" runat="server" Text='<%# Bind("descripcion") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                       
                        <asp:TemplateField HeaderText="Valor">
                            <EditItemTemplate>
                                <asp:TextBox class="numeric" ID="txtvalor_cubierto" runat="server"
                                                    Text='<%# Bind("valor_cubierto") %>'></asp:TextBox>
                                                   
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox class="numeric" ID="txtnewvalor_cubierto" runat="server" 
                                    Text='<%# Bind("valor_cubierto") %>'></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblvalor_cubierto" runat="server" 
                                    Text='<%# Bind("valor_cubierto") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle CssClass="gridHeader" />
                    <HeaderStyle CssClass="gridHeader" />
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
               <td style="font-size: x-small; height: 14px; text-align: center; font-weight: 700;" 
                    colspan="24">
                    &nbsp;</td>   </tr>



            <tr>
               <td style="font-size: x-small; height: 14px; text-align: center; font-weight: 700;" 
                    colspan="24">
                    <strong><span style="font-size: small; color: #0099FF;">ACCIDENTES PERSONALES</span></strong><span 
                    style="font-size: small; color: #0099FF;"><strong style="text-align: center"> 
                    AMPAROS Y VALORES&nbsp; 
                ASEGURADOS</strong></span></td>   </tr>



               <tr>
                <td style="font-size: x-small; height: 14px; text-align: center;" 
                    colspan="5">
                    <span style="font-size: x-small; text-align: center">Prima Mensual Opción Familiar Asegurado Principal</span></td>
                <td style="font-size: x-small; height: 14px; text-align: center;" 
                    colspan="19">
                    <span style="font-size: x-small">Prima Mensual Opción Familiar Asegurado Ppal-conyuge y hasta 3 hijos)</span></td>
                </tr>



               <tr>
                <td style="text-align: center;" colspan="3">
                    <asp:TextBox class="numeric" ID="txtPrima_Acci_Ind" runat="server" 
                        Width="144px"></asp:TextBox>
                   </td>
                <td style="text-align: center;">
                    &nbsp;</td>
                <td style="text-align: center;" colspan="2">
                    &nbsp;</td>
                <td style="text-align: center;" colspan="3">
                    &nbsp;</td>
                <td style="text-align: center;" colspan="4">
                    &nbsp;</td>
                <td style="text-align: center;" colspan="11">
                    <asp:TextBox ID="txtPrima_Acci_Fam" class="numeric" runat="server" Width="156px"></asp:TextBox>
                   </td>
            </tr>



               <tr>
                <td class="logo" colspan="24" style="text-align: center; ">
                <asp:GridView ID="gvAccPers" runat="server" Width="99%" 
                    ShowHeaderWhenEmpty = "True" 
                        AutoGenerateColumns="False" PageSize="5" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical" Height="10px" 
                        onrowcommand="gvAccPers_RowCommand" ShowFooter="True" 
                        DataKeyNames="consecutivo" onrowcancelingedit="gvAccPers_RowCancelingEdit" 
                        onrowdeleted="gvAccPers_RowDeleted" onrowdeleting="gvAccPers_RowDeleting" 
                        onrowediting="gvAccPers_RowEditing" onrowupdated="gvAccPers_RowUpdated" 
                        onrowupdating="gvAccPers_RowUpdating" 
                        onselectedindexchanged="gvAccPers_SelectedIndexChanged" 
                        style="font-size: xx-small">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <EditItemTemplate>
                                &nbsp;<asp:ImageButton ID="btnActualizar0" runat="server" CommandName="Update" 
                                    ImageUrl="~/Images/gr_guardar.jpg" ToolTip="Actualizar" Width="16px" />
                                <asp:ImageButton ID="btnCancelar0" runat="server" CommandName="Cancel" 
                                    ImageUrl="~/Images/gr_cancelar.jpg" ToolTip="Cancelar" Width="16px" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:ImageButton ID="btnNuevo0" runat="server" CausesValidation="False" 
                                    CommandName="AddNew" ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" 
                                    Width="16px" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                    ToolTip="Editar" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" 
                            EditText="" ShowDeleteButton="True" />
                        <asp:TemplateField HeaderText="Amparo">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtdescripcionacc" runat="server" 
                                    Text='<%# Bind("descripcion") %>' style="font-size: xx-small"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtnewdescripcionacc" runat="server" 
                                    Text='<%# Bind("descripcion") %>'></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbldescripcionacc" runat="server" 
                                    Text='<%# Bind("descripcion") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor Cubierto Princ">
                            <EditItemTemplate>
                                <asp:TextBox class="numeric" ID="txtvalor_cubiertoacc" runat="server" 
                                    Text='<%# Bind("valor_cubierto") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox class="numeric"  ID="txtnewvalor_cubiertoacc" runat="server" 
                                    Text='<%# Bind("valor_cubierto") %>'></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblvalor_cubiertoacc" runat="server" 
                                    Text='<%# Bind("valor_cubierto") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor Cubierto Cony">
                            <EditItemTemplate>
                                <asp:TextBox class="numeric"  ID="txtvalor_cubierto_cony" runat="server" 
                                    Text='<%# Bind("valor_cubierto_conyuge") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox class="numeric"  ID="txtnewvalor_cubierto_cony" runat="server" 
                                    Text='<%# Bind("valor_cubierto_hijos") %>'></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblvalor_cubierto_cony" runat="server" 
                                    Text='<%# Bind("valor_cubierto_conyuge") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor Cubierto Hijos">
                            <EditItemTemplate>
                                <asp:TextBox class="numeric"  ID="txtvalor_cubierto_hijos" runat="server" 
                                    Text='<%# Bind("valor_cubierto_hijos") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox class="numeric" ID="txtnewvalor_cubierto_hijos" runat="server" 
                                    Text='<%# Bind("valor_cubierto_hijos") %>'></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblvalor_cubierto_hijos" runat="server" 
                                    Text='<%# Bind("valor_cubierto_hijos") %>'></asp:Label>
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

            <script type="text/javascript">
                $(".numeric").numeric();
                $(".integer").numeric(false, function () { alert("Integers only"); this.value = ""; this.focus(); });
                $(".positive").numeric({ negative: false }, function () { alert("No negative values"); this.value = ""; this.focus(); });
                $(".positive-integer").numeric({ decimal: false, negative: false }, function () { alert("Positive integers only"); this.value = ""; this.focus(); });
                $("#remove").click(
		function (e) {
		    e.preventDefault();
		    $(".numeric,.integer,.positive").removeNumeric();
		}
	);
	</script>

               </table>
        
</asp:Content>

