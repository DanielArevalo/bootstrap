<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:MultiView ID="mvCalendar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwData" runat="server">
            <table style="width: 100%">
                <tr style="text-align: left">
                    <td>
                        <strong>Seleccione los días no hábiles en el calendario</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#3366CC"
                            BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana"
                            Font-Size="8pt" ForeColor="#003399" Height="320px" Width="400px" OnDayRender="Calendar1_DayRender"
                            OnVisibleMonthChanged="Calendar1_VisibleMonthChanged" OnSelectionChanged="Calendar1_SelectionChanged">
                            <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                            <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True"
                                Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                            <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                            <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                        </asp:Calendar>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 100%; text-align: center">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Dias no hábiles grabados correctamente."></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
</asp:Content>
