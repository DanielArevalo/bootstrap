<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Tipos de Documento :." ValidateRequest="False" %>

<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScritpManager1" runat="server" EnablePageMethods="true" />
    <script type="text/javascript">
        function Grabar() {
            if (!editor)
                return;
            var textoHTML = document.getElementById('FreeTextBox1').Text;
            alert(textoHTML);
            document.getElementById('contents').style.display = '';
            PageMethods.GrabarTipoDocumento(2, textoHTML, OnSuccess);
        }
        function OnSuccess(response, userContext, methodName) {
            alert(response);
        }
	</script>    
    <script type="text/javascript">
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#blah')
                        .attr('src', e.target.result)
                        .width($("#ancho").val())
                        .height('auto');
                };
                reader.readAsDataURL(input.files[0]);
                
                
            }
        }
    </script>    
    <br /><br /> 
    <asp:MultiView ID="mvContenido" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
             <div>
               <table border="0" cellpadding="1" cellspacing="0" width="100%" >
                <tr>
                    <td class="tdI" style="text-align:left;width:15%" colspan="2">
                        Código*&nbsp;<asp:RequiredFieldValidator ID="rfvtipoliq" runat="server" ControlToValidate="txtCodOpcion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                        <asp:TextBox ID="txtCodOpcion" runat="server" CssClass="textbox" Width="90%"
                            MaxLength="128" Enabled="False" />
                    </td>
                    <td class="tdI" style="text-align:left;width:15%" colspan="2">
                        Nombre&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" MaxLength="128" Width="95%" />
                    </td> 
                    <td class="tdI" style="text-align:left;width:15%" colspan="2">
                        Oficina Virtual&nbsp;<br />
                        <asp:CheckBox ID="chkOficinaVirtual" runat="server" Text="Mostrar" />
                    </td>
                </tr>
            </table>
                 <br />
                 <hr />
                 <br />
                <table border="0" width="100%" style="text-align: left;" >                    
                    <tr>
                        <td>
                            ancho
                        </td>
                        <td>
                            Seleccione una imagena a cargar                            
                        </td>                                                
                    </tr>
                    <tr>
                        <td>
                            <input type='text' class="textbox" style="width:30px; margin: 0 auto" />
                        </td>
                        <td>                            
                            <input type='file' id="ancho" value="Seleccione su imagen" onchange="readURL(this);" />
                        </td>                                                
                    </tr>                                        
                </table>
                 <table style='margin: 0 auto;'>
                     <tr>
                        <td>                                    
                            <img id="blah" src="#" alt="Tu Imagen" />
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: x-small">
                            <em>Arrastre la imagen hacia el campo de texto </em>
                        </td>
                    </tr>
                 </table>
            
            </div>
            <div>    	    		
		        <FTB:FreeTextBox id="FreeTextBox1" runat="Server" Text="" Width="900px" OnSaveClick="btnGuardar_Click" toolbarlayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu,FontForeColorPicker,FontBackColorsMenu,FontBackColorPicker|Bold,Italic,Underline,Strikethrough,Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;CreateLink,Unlink,InsertImage|Cut,Copy,Paste,Delete;Undo,Redo,Print,Save|SymbolsMenu,StylesMenu,InsertHtmlMenu|InsertRule,InsertDate,InsertTime|InsertTable,EditTable;InsertTableRowAfter,InsertTableRowBefore,DeleteTableRow;InsertTableColumnAfter,InsertTableColumnBefore,DeleteTableColumn|InsertDiv,EditStyle,InsertImageFromGallery,Preview,SelectAll,WordClean,NetSpell" />
	        </div>	
        </asp:View>
        <asp:View ID="mvFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Documento Grabado Correctamente"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">                            
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
</asp:Content>