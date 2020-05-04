<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <table border="0" cellpadding="0" cellspacing="0" width="95%">
            <tr>
                <td colspan="2" style="text-align: left">
					<asp:Panel ID="panelCampos" runat="server">
						<table border="0" cellpadding="0" cellspacing="0" width="90%">
							<tr>
								<td colspan="5" style="text-align: left;">Código de control<br />
									<asp:TextBox ID="txtCodigoControl"  runat="server" CssClass="textbox" Width="150px"
										MaxLength="20" Enabled="false" />
								</td>
								<td colspan="3" style="text-align: left;">&nbsp&nbsp&nbsp&nbsp Descripción<br />
									<asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="150" style="margin-left:4%;" Width="390px" />
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
		</table>
</asp:Content>

