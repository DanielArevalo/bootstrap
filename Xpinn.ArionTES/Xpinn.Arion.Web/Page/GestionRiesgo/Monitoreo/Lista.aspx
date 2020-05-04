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
								<td colspan="4" style="text-align: left;">Código de monitoreo<br />
									<asp:TextBox ID="txtCodigoMonitoreo"  runat="server" CssClass="textbox" Width="230px"
										MaxLength="20" Enabled="false" />
								</td>
								<td colspan="4" style="text-align: left;">Estado Alerta:
									<br />
									<asp:DropDownList ID="ddlEstadoAlerta" runat="server" CssClass="textbox" Width="230px">
									</asp:DropDownList>
								</td>
								<td colspan="4" style="text-align: left;">Periodicidad:
									<br />
									<asp:DropDownList ID="ddlPeriodicidad" runat="server" CssClass="textbox" Width="230px">
									</asp:DropDownList>
								</td>
							</tr>
							<br />
							<tr>
								<td colspan="4" style="text-align: left;">Área de ejecución:
									<br />
									<asp:DropDownList ID="ddlAreaEjecucion" runat="server" CssClass="textbox" Width="240px">
									</asp:DropDownList>
								</td>
								<td colspan="4" style="text-align: left;">Responsable ejecución:
									<br />
									<asp:DropDownList ID="ddlResponsableEjecucion" runat="server" CssClass="textbox" Width="230px">
									</asp:DropDownList>
								</td>
								<td colspan="4" style="text-align: left">
								</td>
							</tr>
						</table>
					</asp:Panel>
				</td>
			</tr>
            <tr>
                <td colspan="2" style="text-align: center"><br/>
					<asp:Panel ID="panelGrilla" runat="server"><br/>
						<asp:GridView runat="server" ID="gvMonitoreo" HorizontalAlign="Center" Width="99%" AutoGenerateColumns="false"
							Style="font-size: x-small" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvMonitoreo_PageIndexChanging"
                            OnRowDeleting="gvMonitoreo_RowDeleting" OnRowEditing="gvMonitoreo_RowEditing" DataKeyNames="cod_monitoreo">
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
								<asp:BoundField DataField="cod_monitoreo" HeaderText="Cód. Monitoreo">
									<ItemStyle HorizontalAlign="Center" Width="150px" />
								</asp:BoundField>
								<asp:BoundField DataField="nom_alerta" HeaderText="Estado Alerta">
									<ItemStyle HorizontalAlign="Center" Width="150px" />
								</asp:BoundField>
								<asp:BoundField DataField="nom_periodicidad" HeaderText="Periodicidad">
									<ItemStyle HorizontalAlign="Center" Width="200px" />
								</asp:BoundField>
								<asp:BoundField DataField="nom_area" HeaderText="Área ejecución">
									<ItemStyle HorizontalAlign="Center" Width="150px" />
								</asp:BoundField>
								<asp:BoundField DataField="nom_cargo" HeaderText="Responsable ejecución">
									<ItemStyle HorizontalAlign="Center" Width="200px" />
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

