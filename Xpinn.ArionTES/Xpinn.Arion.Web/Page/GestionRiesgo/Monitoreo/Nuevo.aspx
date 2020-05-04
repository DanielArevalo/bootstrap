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
									<asp:DropDownList ID="ddlResponsable" runat="server" CssClass="textbox" Width="230px">
									</asp:DropDownList>
								</td>
								<td colspan="4" style="text-align: left">
								</td>
							</tr>
						</table>
					</asp:Panel>
				</td>
			</tr>
		</table>
</asp:Content>

