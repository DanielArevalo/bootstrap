<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <table border="0" cellpadding="0" cellspacing="0" width="95%">
            <tr>
                <td colspan="2" style="text-align: left">
					<asp:Panel ID="pConsulta" runat="server">
						<table border="0" cellpadding="0" cellspacing="0" width="90%">
							<tr>
								<td colspan="5" style="text-align: left;">Código de control<br />
									<asp:TextBox ID="txtCodigoControl"  runat="server" CssClass="textbox" Width="150px"
										MaxLength="20" Enabled="false" />
								</td>
								<td colspan="3" style="text-align: left;">&nbsp&nbsp&nbsp&nbsp Descripción<br />
									<asp:TextBox ID="txtDescripcionControl" runat="server" CssClass="textbox" MaxLength="150" style="margin-left:4%;" Width="390px" />
								</td>
								<td colspan="4" style="text-align: left;">Clase:
									<br />
									<asp:DropDownList ID="ddlClase" runat="server" CssClass="textbox" Width="150px">
									</asp:DropDownList>
								</td>
							</tr>
							<br />
							<tr>
								<td colspan="5" style="text-align: left;">Área de ejecución:
									<br />
									<asp:DropDownList ID="ddlAreaEjecucion" runat="server" CssClass="textbox" Width="160px">
									</asp:DropDownList>
								</td>
								<td colspan="3" style="text-align: left">&nbsp&nbsp&nbsp&nbsp Responsable de ejecución:
									<br />
									<asp:DropDownList ID="ddlResponsable" runat="server" CssClass="textbox" style="margin-left:4%;" Width="400px">
									</asp:DropDownList>
								</td>
								<td colspan="4" style="text-align: left">Grado de aceptación:
									<br />
									<asp:DropDownList ID="ddlGradoAceptacion" runat="server" CssClass="textbox" Width="150px">
									</asp:DropDownList>
								</td>
							</tr>
						</table>
					</asp:Panel>
				</td>
			</tr>
            <tr>
                <td colspan="2" style="text-align: center">
					<asp:Panel ID="panelGrilla" runat="server"><br/>
						<asp:GridView runat="server" ID="gvControl" HorizontalAlign="Center" Width="99%" AutoGenerateColumns="false"
                            OnRowDeleting="gvControl_RowDeleting" OnPageIndexChanging="gvControl_PageIndexChanging" OnRowEditing="gvControl_RowEditing"
							Style="font-size: x-small" PageSize="20" AllowPaging="true" DataKeyNames="cod_control">
							<Columns>
								<asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
									<ItemTemplate>
										<asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
											ToolTip="Modificar" /></ItemTemplate>
									<HeaderStyle CssClass="gridIco"></HeaderStyle>
								</asp:TemplateField>
								<asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
									<ItemTemplate>
										<asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
											ToolTip="Borrar" /></ItemTemplate>
									<HeaderStyle CssClass="gridIco"></HeaderStyle>
								</asp:TemplateField>
								<asp:BoundField DataField="cod_control" HeaderText="Cód. Control">
									<ItemStyle HorizontalAlign="Center" Width="80px" />
								</asp:BoundField>
								<asp:BoundField DataField="descripcion" HeaderText="Descripción">
									<ItemStyle HorizontalAlign="Center" Width="200px" />
								</asp:BoundField>
								<asp:BoundField DataField="nom_clase" HeaderText="Clase">
									<ItemStyle HorizontalAlign="Center" Width="80px" />
								</asp:BoundField>
								<asp:BoundField DataField="nom_area" HeaderText="Área ejecución">
									<ItemStyle HorizontalAlign="Center" Width="80px" />
								</asp:BoundField>
								<asp:BoundField DataField="nom_cargo" HeaderText="Responsable ejecución">
									<ItemStyle HorizontalAlign="Center" Width="150px" />
								</asp:BoundField>
								<asp:BoundField DataField="grado_aceptacion" HeaderText="Grado de aceptación">
									<ItemStyle HorizontalAlign="Center" Width="90px" />
								</asp:BoundField>
							</Columns>
							<HeaderStyle CssClass="gridHeader" />
							<PagerStyle CssClass="gridHeader" Font-Bold="False" />
							<RowStyle CssClass="gridItem" />
						</asp:GridView>
					</asp:Panel>                    
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="true" Text="" Style="text-align: center" />
				</td>
			</tr>
		</table>
</asp:Content>

