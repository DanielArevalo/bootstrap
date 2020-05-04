<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="ArchivosTablas.aspx.cs" Inherits="ArchivoTablas" Title=".: Xpinn - Migraciones :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <table class="style17" style="width: 920px">
        <tr>
            <td class="style11">
                &nbsp;</td>
            <td class="style15">
                <asp:FileUpload ID="FileUpLoad" runat="server" enabled="true" 
                    
                    Width="366px" 
                    Height="26px" />
            </td>
            <td class="style19">
                &nbsp;</td>
            <td class="style19">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style10">
                <asp:Button ID="BtnCargar" runat="server" onclick="BtnCargar_Click" 
                    Text="Subir Archivo Servidor" Width="187px" />
            </td>
            <td class="style14">
                &nbsp;</td>
            <td class="style12">
                <asp:DropDownList ID="DdlHojas" runat="server" AutoPostBack="True" 
                    Width="165px" Visible="False">
                </asp:DropDownList>
            </td>
            <td class="style12">
                </td>
        </tr>
        <tr>
            <td class="style11">
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                    Text="Campos Archivo" Width="187px" />
            </td>
            <td class="style15">
                Nombre Archivo:<asp:Label ID="lblNombreArchivo" runat="server" Text="..."></asp:Label>
            </td>
            <td class="style19">
                Nombre Hoja</td>
            <td class="style19">
                <asp:TextBox ID="txtHoja" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style11">
    <asp:Button ID="btnConsultarEsq" runat="server" onclick="btnConsultarEsq_Click" 
        Text="Consultar  Esquemas" Width="187px" />
            </td>
            <td class="style15">
                <asp:DropDownList ID="DDlEsquemas" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="DDTablas_SelectedIndexChanged" Width="365px">
                </asp:DropDownList>
            </td>
            <td class="style19">
                &nbsp;</td>
            <td class="style19">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style20">
    <asp:Button ID="btnConsultar" runat="server" onclick="btnConsultar_Click" 
        Text="Consultar  Tablas" Width="187px" />
            </td>
            <td class="style21">
                <asp:DropDownList ID="DDlTablas" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="DDTablas_SelectedIndexChanged" Width="365px">
                </asp:DropDownList>
            </td>
            <td class="style22">
                Base Datos</td>
            <td class="style22">
                <asp:DropDownList ID="ddTipoBaseDatos" runat="server" Width="165px">
                    <asp:ListItem>Oracle</asp:ListItem>
                    <asp:ListItem>SqlServer</asp:ListItem>
                
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style11">
                <asp:Button ID="BtnProcesarCargue" runat="server" Text="Procesar Cargue" 
                    Width="187px" onclick="BtnProcesarCargue_Click" />
            </td>
            <td class="style15">
                <asp:Label ID="lblMensajeError" runat="server" ForeColor="Red" Text="..."></asp:Label>
            </td>
            <td class="style19">
                &nbsp;</td>
            <td class="style19">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style11">
                Registros Exitosos</td>
            <td class="style15">
                <asp:TextBox ID="txtExitosos" runat="server" Enabled="False"></asp:TextBox>
            </td>
            <td class="style19">
                Registros Fallidos</td>
            <td class="style19">
                <asp:TextBox ID="txtFallidos" runat="server" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style11">
                &nbsp;</td>
            <td class="style15">
                &nbsp;</td>
            <td class="style19">
                &nbsp;</td>
            <td class="style19">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style11">
                &nbsp;</td>
            <td class="style15">
                &nbsp;</td>
            <td class="style19">
                &nbsp;</td>
            <td class="style19">
                &nbsp;</td>
        </tr>
    </table>
    <div>
    
        <table style="width: 80%; margin-right: 0px;">
            <tr>
                <td bgcolor="#99CCFF" class="logo" style="width: 211px; height: 25px">
                    <strong>CAMPOS ARCHIVO</strong></td>
                <td class="logo" style="width: 21px; height: 25px;">
                    </td>
                <td bgcolor="#FFFF66" class="style4" style="height: 25px">
                    <strong>INTEGRACION</strong></td>
                <td class="style4" style="height: 25px">
                    </td>
                <td bgcolor="#99CCFF" class="style2" style="height: 25px">
                    <strong>CAMPOS TABLA BASE DATOS</strong></td>
            </tr>
            <tr>
                <td class="logo" style="width: 211px">
                    <asp:GridView ID="GvColumnasArchivo" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" ForeColor="#333333" GridLines="None" Height="101px" 
                        Width="263px">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="Nombre" HeaderText="Columna" />
                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </td>
                <td class="logo" style="width: 21px">
                    &nbsp;</td>
                <td class="style6">
                    <asp:GridView ID="gvColumnasUnidas" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" BackColor="White" BorderColor="#3366CC" BorderStyle="None" 
                        BorderWidth="1px" onrowdatabound="gvColumnasUnidas_RowDataBound" 
                        onselectedindexchanged="gvColumnasUnidas_SelectedIndexChanged" 
                        Width="263px" Height="128px">
                        <Columns>
                            <asp:TemplateField HeaderText="Campo Origen">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddNombreOrigen" runat="server" Height="35px" 
                                        Width="217px">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Campo Destino">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("NombreDestino") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("NombreDestino") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                        <RowStyle BackColor="White" ForeColor="#003399" />
                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                        <SortedDescendingHeaderStyle BackColor="#002876" />
                    </asp:GridView>
                </td>
                <td class="style6">
                    &nbsp;</td>
                <td class="style7">
                    <asp:GridView ID="GvColumnas" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" ForeColor="#333333" GridLines="None" Height="133px" 
                        Width="263px">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="Nombre" HeaderText="Columna" />
                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                            <asp:BoundField DataField="Longitud" HeaderText="Longitud" />
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="5">
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    <table style="width:84%;">
        <tr>
            <td>
                <strong>Registros Fallidos:</strong></td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvFallidos" runat="server" AutoGenerateColumns="False" 
                    BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="4">
                    <Columns>
                        <asp:BoundField DataField="Registro" HeaderText="Registros Fallidos" />
                        <asp:BoundField DataField="Mensaje" HeaderText="Mensaje" />
                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" ForeColor="#003399" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                    <SortedDescendingHeaderStyle BackColor="#002876" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
  
</asp:Content>
