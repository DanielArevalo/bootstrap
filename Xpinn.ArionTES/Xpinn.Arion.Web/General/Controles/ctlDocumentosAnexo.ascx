<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlDocumentosAnexo.ascx.cs" Inherits="General_Controles_ctlDocumentosAnexo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Panel ID="panelAnexos" runat="server">
    <table style="width: 101%;">
        <tr>
            <td>
                <div style="font-size: large">
                    <br />
                    DOCUMENTOS ANEXOS<br />
                </div>
            </td>
        </tr>
        <tr>
            <td style="height: 4px;">&nbsp;</td>
        </tr>
        <tr>
            <td id="TablaAnexos" style="height: 4px;">
                <asp:Panel runat="server" ID="Documentos">
                    <asp:GridView ID="gvAnexos" runat="server" AllowPaging="False"
                        AutoGenerateColumns="False" DataKeyNames="idimagen"
                        GridLines="Horizontal" HeaderStyle-CssClass="gridHeader"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" Width="100%"
                        OnRowCommand="gvAnexos_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="idimagen" HeaderStyle-CssClass="gridColNo"
                                ItemStyle-CssClass="gridColNo"></asp:BoundField>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnInfo" runat="server" CommandName="Anexo"
                                        ImageUrl="~/Images/gr_info.jpg" ToolTip="Documentos Anexos" Width="16px" CommandArgument='<%#Eval("idimagen")%>' />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="tipo_documento" HeaderText="Tipo Doc." ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Formato" HeaderText="Formato" Visible="True" />
                            <asp:TemplateField HeaderText="Documento">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnAnexo" runat="server" CommandName="Anexo" ImageUrl="~/Images/Lupa.jpg"
                                        ToolTip="Documentos Anexos" Width="16px" CommandArgument='<%#Eval("idimagen")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                    <br />
                    <asp:Label ID="lblAnexos" runat="server" Text=""></asp:Label>
                    <asp:HiddenField ID="hfDocAnexo" runat="server" />
                    <asp:ModalPopupExtender ID="mpeDocAnexo" runat="server" Enabled="True" BackgroundCssClass="backgroundColor"
                        PopupControlID="panelDocAnexo" TargetControlID="hfDocAnexo" CancelControlID="btnCloseDocAnexo">
                    </asp:ModalPopupExtender>
                    <asp:ResizableControlExtender ID="rceDocAnexo" runat="server" TargetControlID="panelDocAnexo" OnClientResizing="rceDocAnexoResize"
                        HandleCssClass="handle" ResizableCssClass="resizing" MinimumHeight="580" MinimumWidth="420" HandleOffsetY="20">
                    </asp:ResizableControlExtender>
                    <asp:DragPanelExtender ID="dpeDocAnexo" runat="server"
                        Enabled="True" TargetControlID="panelDocAnexo" DragHandleID="panelDocAnexo">
                    </asp:DragPanelExtender>
                    <asp:Panel ID="panelDocAnexo" runat="server" Style="border: solid 2px Gray" BackColor="White">
                        <div style="border-style: none; border-width: medium; background-color: #3399FF; cursor: move">
                            DOCUMENTOS ANEXOS
                        </div>
                        <br />
                        <asp:Button ID="btnCloseDocAnexo" runat="server" Text="Cerrar" CssClass="button" OnClick="btnCloseDocAnexo_Click" CausesValidation="false" Height="20px" />
                        &nbsp;&nbsp;&nbsp;        
                                <br />
                        <asp:Image ID="imgDocAnexo" runat="server" Height="90%" Width="100%" />
                        <asp:Literal ID="LiteralDcl" runat="server" />

                    </asp:Panel>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel runat="server" ID="UpdatePanel">
                    <Triggers></Triggers>
                    <ContentTemplate>
                        <asp:Label runat="server" Style="color: #008000; font-weight: bold">Se refrescará la página cada vez que se agregue un archivo</asp:Label>
                        <asp:GridView ID="gvArchivosPlus" AutoPostBack="True" runat="server" Width="98%" ShowHeaderWhenEmpty="True"
                            EmptyDataText="No se encontraron registros." AutoGenerateColumns="False" PageSize="5"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" ForeColor="Black" GridLines="Vertical" Height="16px" ShowFooter="True"
                            Style="font-size: x-small" OnRowDeleting="gvCuoExt_RowDeleting" Visible="False"
                            OnPageIndexChanging="gvArchivosPlus_PageIndexChanging" OnDataBound="gvAnexos_OnDataBound">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                            ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Añadir Archivo">
                                    <ItemTemplate>
                                        <asp:FileUpload runat="server" ID="fileUpload" type="file" name="File" Width="60%" class="inputFile" onchange="Fiiles(this)" />
                                        <asp:Label runat="server" ID="lblFile"  class="labelFile" AssociatedControlID="fileUpload">Añadir Archivo</asp:Label>
                                        <asp:Label runat="server" ID="lblNombreArhivo" Style="text-align: left;" Visible="False" Text='<%# Bind("NombreArchivo") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre Archivo">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtNombreArchivo" Width="60%" CssClass="textbox" Text='<%# Bind("Nombre") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Formato">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtFormato" Width="60%" Style="text-align: left;border: none; background: none" Text='<%# Bind("Formato") %>' ></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" />
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <div style="margin-top: 18px">
                    <asp:Button runat="server" Text="Agregar" OnClick="BtnAgregarOnClick" CommandName="AddNew" class="agregarBoton" ID="btnAgregar" />
                </div>
            </td>
        </tr>
    </table>
</asp:Panel>
<br />

<style>
    .labelFile {
        font-size: 14px;
        font-weight: 600;
        color: #fff;
        background-color: #106BA0;
        display: inline-block;
        transition: all .5s;
        cursor: pointer;
        padding: 6px 8px !important;
        text-transform: uppercase;
        width: fit-content;
        text-align: center;
            margin: 4px;
    }

    .agregarBoton {
        font-size: 14px;
        font-weight: 600;
        color: #fff;
        background-color: #106BA0;
        display: inline-block;
        transition: all .5s;
        cursor: pointer;
        padding: 9px 18px !important;
        text-transform: uppercase;
        width: fit-content;
        text-align: center;
    }

    .inputFile {
        width: 0.1px;
        height: 0.1px;
        opacity: 0;
        overflow: hidden;
        position: absolute;
        z-index: -1;
    }

    .customers {
        font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
        border-collapse: collapse;
        width: 100%;
    }

        .customers td, #customers th {
            border: 1px solid #ddd;
            padding: 8px;
        }

        .customers tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        .customers tr:hover {
            background-color: #ddd;
        }

        .customers th {
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: center;
            background-color: #008CBA;
            color: white;
        }
</style>

<script type="text/javascript">
    var contador = 0;

    function PanelClick(sender, e) {
    }

    function ActiveTabChanged(sender, e) {
    }

    function mpeSeleccionOnOk() {
    }

    var HighlightAnimations = {};

    function Highlight(el) {
        if (HighlightAnimations[el.uniqueID] == null) {
            HighlightAnimations[el.uniqueID] = Sys.Extended.UI.Animation.createAnimation({
                AnimationName: "color",
                duration: 0.5,
                property: "style",
                propertyKey: "backgroundColor",
                startValue: "#FFFF90",
                endValue: "#FFFFFF"
            }, el);
        }
        HighlightAnimations[el.uniqueID].stop();
        HighlightAnimations[el.uniqueID].play();
    }

    function mpeSeleccionOnCancel() {
    }

    function rceDocAnexoResize(sender, e) {
        var panelDocAnexo = document.getElementById('<%= panelDocAnexo.ClientID %>');
        var imgDocAnexo = document.getElementById('<%= imgDocAnexo.ClientID %>');
        var literalDcl = document.getElementById('<%= LiteralDcl.ClientID %>');
        if (imgDocAnexo !== null) {
            imgDocAnexo.width = panelDocAnexo.width;
            imgDocAnexo.height = panelDocAnexo.height;
        }

    }


    function Fiiles(e) {
        debugger;
        if ($(e).val() != null) {
            if (e.files[0].size > 2097152) {
                alert("Try to upload file less than 2MB!");
            } else {
                var index = e.id.split("_").pop();
                var nombreArchivo = $(e).val().split('\\').pop().split('.')[0];
                var formato = $(e).val().split('\\').pop().split('.').pop();
                $("#cphMain_DocumentosAnexo_gvArchivosPlus_txtNombreArchivo_" + index).val(nombreArchivo);
                $("#cphMain_DocumentosAnexo_gvArchivosPlus_txtFormato_" + index).val(formato);

            }
        }
    }

</script>
