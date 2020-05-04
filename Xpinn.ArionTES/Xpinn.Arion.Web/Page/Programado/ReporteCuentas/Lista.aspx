<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
	<asp:ScriptManager ID="ScriptManager1" runat="server">
	</asp:ScriptManager>
	<asp:Panel ID="Principal" runat="server">
		<asp:Panel ID="pConsulta" runat="server" style="width: 70%;">
			<table style="width: 70%;">
				<tr>
					<td style="font-size: x-small; text-align:left" colspan="3">
						<strong>Filtrar por :</strong>
					</td>
				</tr>
				<tr>
					<td style="width: 120px; text-align:left">
						<asp:TextBox ID="txtNroProducto" runat="server" CssClass="textbox" 
							Width="121px" Visible="false"></asp:TextBox> 
						Línea<br />
						<asp:DropDownList ID="ddlLinea" runat="server" ClientIDMode="Static" CssClass="textbox">
					</asp:DropDownList>
					  
					 
					 </td>
					<td style="text-align:left; width:140px ">
						Fecha Inicial<br />
						<ucFecha:fecha ID="txtFechaIni" runat="server" />
					</td>
					<td style="text-align:left; width:140px ">
						Fecha Final<br />
						<ucFecha:fecha ID="txtFechaFin" runat="server" />
					</td> 
					 <td style="text-align:left; width:140px ">
					  Oficina<br />
					 <asp:DropDownList ID="ddlOficina" runat="server" ClientIDMode="Static" CssClass="textbox">
					</asp:DropDownList>
					</td>   
				</tr>
				<tr>
					<td colspan="3">
						&nbsp;
					</td>
				</tr>
			</table>
		</asp:Panel>
		<asp:Panel ID="Listado" runat="server">
			<table style="width: 80%;">
				<tr>
					<td>
					<strong>Reportes Generados</strong><br />

						<asp:GridView ID="gvLista" runat="server" AllowPaging="True" 
							AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
							BorderStyle="None" BorderWidth="1px" CellPadding="4"  
							ForeColor="Black" GridLines="Horizontal" PageSize="20"
							onpageindexchanging="gvLista_PageIndexChanging" style="font-size: x-small" 
							onrowdatabound="gvLista_RowDataBound" DataKeyNames="numeroCuenta" 
							>
							<AlternatingRowStyle BackColor="White" />
							<Columns>
								<asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_info.jpg"
									ShowEditButton="True" Visible="False" />
								<asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg"
									ShowDeleteButton="True" Visible="False" />
								
								<asp:BoundField DataField="numeroCuenta" HeaderText="Numero Cuenta" />
								<asp:BoundField DataField="linea" HeaderText="Linea"/>
								<asp:BoundField DataField="identificacion" HeaderText="Identificacion">
								   <ItemStyle HorizontalAlign="Left" />
								</asp:BoundField>
								<asp:BoundField DataField="nombre" HeaderText="Nombre"  >
								   <ItemStyle HorizontalAlign="Left" />
								</asp:BoundField>
								<asp:BoundField DataField="oficina" HeaderText="Oficina" >
								   <ItemStyle HorizontalAlign="Left" />
								</asp:BoundField>
								<asp:BoundField DataField="fechaApertura" HeaderText="Fecha Apertura" DataFormatString="{0:d}" >
								   <ItemStyle HorizontalAlign="Left" />
								</asp:BoundField>
								<asp:BoundField DataField="saldoInicial" HeaderText="Saldo Inicial" >
								   <ItemStyle HorizontalAlign="Left" />
								</asp:BoundField>
                                <asp:BoundField DataField="depocito" HeaderText="Deposito" >
								   <ItemStyle HorizontalAlign="Left" />
								</asp:BoundField>
                                <asp:BoundField DataField="retiro" HeaderText="Retiro" >
								   <ItemStyle HorizontalAlign="Left" />
								</asp:BoundField>
                                <asp:BoundField DataField="intereses" HeaderText="Interes" >
								   <ItemStyle HorizontalAlign="Left" />
								</asp:BoundField>
                                <asp:BoundField DataField="retencion" HeaderText="Retencion" >
								   <ItemStyle HorizontalAlign="Left" />
								</asp:BoundField>  
                                <asp:BoundField DataField="saldoFinal" HeaderText="Saldo Final" >
								   <ItemStyle HorizontalAlign="Left" />
								</asp:BoundField> 
							</Columns>
							<FooterStyle BackColor="#CCCC99" />
							<HeaderStyle CssClass="gridHeader" />
							<PagerStyle CssClass="gridHeader" Font-Bold="False" />
							<RowStyle CssClass="gridItem" />
							<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
							<SortedAscendingCellStyle BackColor="#FBFBF2" />
							<SortedAscendingHeaderStyle BackColor="#848384" />
							<SortedDescendingCellStyle BackColor="#EAEAD3" />
							<SortedDescendingHeaderStyle BackColor="#575357" />
						</asp:GridView>
						<asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
						<asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado." Visible="False"/>
					</td>
				</tr>
			</table>
		</asp:Panel>
	</asp:Panel>
	 <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>
</asp:Content>
