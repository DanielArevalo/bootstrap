//===========================================================
//          Permite solo numeros ENTEROS
//       Sirve para onkeypress event
//          onkeypress="return isNumber(event)" 
//===========================================================

function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

//===========================================================
//          Permite solo numeros DECIMALES
//       Sirve para onkeypress event
//          onkeypress="return isDecimalNumber(event)" 
//===========================================================

function isDecimalNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        if (charCode != 44) {
            return false;
        }
    }
    return true;
}

//===========================================================
//          Permite solo letras y espacios
//           Sirve para onkeypress event
//          onkeypress="return isOnlyLetter(event)" 
//===========================================================

function isOnlyLetter(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (!(charCode >= 65 && charCode <= 120) && (charCode != 32 && charCode != 0)) {
        return false;
    }
    return true
}

//===========================================================
//    Sirve para cambiar el color de un control cuando es seleccionado 
//                  o cualquier otro evento
//      EXAMPLE ====> <input type="button2" id="3" onclick="changeColorByEvent('hola', this.id, '#E8254C', '#1037D2')" class="hola" style="background-color: #1037D2;" />
//   Primer parametro es la clase que le pones al grupo de controles al cual quieres alternal los colores
//  Segundo parametro es el id que provoca el evento, el que va a cambiar
//   Tercer parametro es el color al que vas a cambiar al control que provoca el evento
//  Cuarto parametro es el color por default en el que quedan el resto de controles del grupo que no han sido seleccionados
//===========================================================

//                      MUERE TRAS POSTBACK
//                  SI FUERA MVC NO PASARIA ESTO :D                    

function changeColorByEvent(classToManage, controlIDToChange, colorToChange, colorToKeep) {

    var controlsWithClass = document.getElementsByClassName(classToManage);
    var controlToChange = document.getElementById(controlIDToChange);

    for (var i = 0; i < controlsWithClass.length; ++i) {
        var item = controlsWithClass[i];

        if (item == controlToChange) {
            item.style.background = colorToChange;
        }
        else {
            item.style.background = colorToKeep;
        }
    }
}


//===========================================================
//    Sirve para validar si un campo esta vacio o con solo espacios
//          Sirve para validar un solo textbox
//===========================================================

function validateControl(id) {
    var x = document.getElementById(id).value;
    if (x.trim() == null || x.trim() == "" || x === " ") {
        return false;
    }
    return true;
}

//===========================================================
//    Sirve para validar si varios campos estan vacios o con solo espacios
//          Sirve para validar antes de enviar al server
//===========================================================

function validateMultipleControls(id) {
    for (var i = 0, max = id.length; i < max; i++) {
        var x = document.getElementById(id[i]).value;
        if (x.trim() == null || x.trim() == "" || x === " ") {
            alert('Faltan Campos Por Llenar');
            break;
        }
    }
}

//===========================================================
//    Sirve para sumar varios campos y mostrar el resultado en un campo unico
//                      Sirve para estetica
//     Se debe pasar un array con los ID de los controles a sumar como primer parametro
//          como segundo parametro es el ID del control destino
//===========================================================

function CalcularSumaTotalVariosControlesAControlUnico(arrayIDControlesCalcular, IDControlDestino) {
    var total = 0;
    var x = document.getElementById(IDControlDestino);

    for (var i = 0, max = arrayIDControlesCalcular.length; i < max; i++) {
        var temp = parseInt(total);

        temp += parseInt(document.getElementById(arrayIDControlesCalcular[i]).value);

        if (isNaN(temp)) {
            continue;
        }
        total = parseInt(temp);
    }

    x.value = total;
}


//===========================================================
//    Sirve para dividir varios campos y mostrar el resultado en un campo unico
//                      Sirve para estetica
//     Se debe pasar un array con los ID de los controles a sumar como primer parametro
//          como segundo parametro es el ID del control destino
//      CUIDADO CON EL ORDEN DEL ARRAY, IMPORTA CUANDO SE EJECUTA LA DIVISION
//                    PROBAR PARA EVITAR TROLLEADAS
//===========================================================

function CalcularDivisionTotalVariosControlesAControlUnico(arrayIDControlesCalcular, IDControlDestino) {
    var total = 0;
    var x = document.getElementById(IDControlDestino);

    for (var i = 0, max = arrayIDControlesCalcular.length; i < max; i++) {
        var temp = parseInt(total);
        var valueControl = parseInt(document.getElementById(arrayIDControlesCalcular[i]).value);

        if (temp == 0) {
            temp = valueControl;
        }
        else {
            temp = temp / valueControl;
        }

        if (isNaN(temp)) {
            total = 0;
            break;
        }

        total = parseInt(temp);
    }

    x.value = total;
}


//===========================================================
//    Sirve para restar varios campos y mostrar el resultado en un campo unico
//                      Sirve para estetica
//     Se debe pasar un array con los ID de los controles a restar como primer parametro
//          como segundo parametro es el ID del control destino
//===========================================================

//                      EN DESARROLLO AUN NO TESTEADO

function CalcularRestaTotalVariosControlesAControlUnico(arrayIDControlesCalcular, IDControlDestino) {
    var total = 0;
    var x = document.getElementById(IDControlDestino);

    for (var i = 0, max = arrayIDControlesCalcular.length; i < max; i++) {
        var temp = parseInt(total);

        temp -= parseInt(document.getElementById(arrayIDControlesCalcular[i]).value);

        if (isNaN(temp)) {
            continue;
        }
        total = parseInt(temp);
    }

    x.value = total;
}

//===========================================================
//    Sirve para asegurarse que solo un radiobutton es seleccionado en una GridView
//             Se debe pasar el ID del radiobutton
//    http://www.aspdotnet-suresh.com/2010/12/how-to-make-single-radio-button.html
//===========================================================

function SelectSingleRadiobutton(rdbtnid) {
    var rdBtn = document.getElementById(rdbtnid);
    var rdBtnList = document.getElementsByTagName("input");
    for (i = 0; i < rdBtnList.length; i++) {
        if (rdBtnList[i].type == "radio" && rdBtnList[i].id != rdBtn.id) {
            rdBtnList[i].checked = false;
        }
    }
}

//===========================================================
//    Sirve para asegurarse que solo un CheckBox es seleccionado en una GridView
//             Se debe pasar el ID del CheckBox
//    http://www.aspdotnet-suresh.com/2010/12/how-to-make-single-radio-button.html
//===========================================================

function SelectSingleCheckBox(rdbtnid) {
    var rdBtn = document.getElementById(rdbtnid);
    var rdBtnList = document.getElementsByTagName("input");
    for (i = 0; i < rdBtnList.length; i++) {
        if (rdBtnList[i].type == "checkbox" && rdBtnList[i].id != rdBtn.id) {
            rdBtnList[i].checked = false;
        }
    }
}

//===========================================================
//    Sirve para asegurarse que solo un CheckBox es seleccionado en una GridView
//             Se debe pasar el ID del CheckBox
//    http://www.aspdotnet-suresh.com/2010/12/how-to-make-single-radio-button.html
//===========================================================

function SelectSingleCheckBoxInsideContainer(rdbtnid, containerID) {
    var rdBtn = document.getElementById(rdbtnid);
    var container = document.getElementById(containerID);
    var rdBtnList = container.getElementsByTagName("input");
    for (i = 0; i < rdBtnList.length; i++) {
        if (rdBtnList[i].type == "checkbox" && rdBtnList[i].id != rdBtn.id) {
            rdBtnList[i].checked = false;
        }
    }
}


//================================================================
//    Sirve para imprimir un reporte DIRECTO (SIN MOSTRAR EL PDF
//        PARA LUEGO IMPRIMIR) de manera que solo se muestra el
//              menu de impresion del Browser
//   Se debe pasar el =======ID CLIENTE DEL REPORTE =======
//      POR EJEMPLO '<%= Rpview.ClientID %>' ENTRE COMILLAS SIMPLES
//       TIENE PROBLEMAS IMPRIMIENDO RDLC QUE SON MUY DELGADOS EN ANCHO
//=================================================================

function PrintReportBrowserDirectlyWithoutShowPDF(id) {
    var rv1 = $('#' + report_ID);
    var iDoc = rv1.parents('html');

    // Reading the report styles
    var styles = iDoc.find("head style[id$='ReportControl_styles']").html();
    if ((styles == undefined) || (styles == '')) {
        iDoc.find('head script').each(function () {
            var cnt = $(this).html();
            var p1 = cnt.indexOf('ReportStyles":"');
            if (p1 > 0) {
                p1 += 15;
                var p2 = cnt.indexOf('"', p1);
                styles = cnt.substr(p1, p2 - p1);
            }
        });
    }

    styles = '<style type="text/css">' + styles + "</style>";

    // Reading the report html
    var table = rv1.find("div[id$='_oReportDiv']");
    if (table == undefined) {
        alert("Report source not found.");
        return;
    }

    var frame1 = document.createElement('iframe');
    frame1.name = "frame1";
    frame1.style.position = "absolute";
    frame1.style.top = "-1000000px";
    document.body.appendChild(frame1);
    var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
    frameDoc.document.open();
    frameDoc.document.write('<html><head><title>RDLC Report</title></head>');
    frameDoc.document.write('<body>');
    var docCnt = styles + table.parent().html();
    frameDoc.document.write(docCnt);
    frameDoc.document.write('</body></html>');
    frameDoc.document.close();
    setTimeout(function () {
        window.frames["frame1"].focus();
        window.frames["frame1"].print();
        document.body.removeChild(frame1);
    }, 500);
}

//================================================================
//    Sirve para imprimir un control de HTML mediante su ID 
//   Es util cuando se pasa un DIV o UPDATEPANEL para imprimir esa parte
//   Se debe pasar el =======ID CLIENTE EN CASO DE SER ASP CONTROL=======
//     POR EJEMPLO '<%= Rpview.ClientID %>' ENTRE COMILLAS SIMPLES
//=================================================================

function PrintPanelOrControlOrDivByID(id) {
    var gridInsideDiv = document.getElementById(id);
    var printWindow = window.open('gview.htm', 'PrintWindow', 'letf=0,top=0,width=150,height=300,toolbar=1,scrollbars=1,status=1');
    printWindow.document.write(gridInsideDiv.innerHTML);
    printWindow.document.close();
    var newStyle = document.createElement('style');
    newStyle.setAttribute('type', 'text/css');
    newStyle.setAttribute('width', '21cm');
    newStyle.setAttribute('height', '14,85cm');
    newStyle.setAttribute('rel', 'stylesheet');
    printWindow.document.head.appendChild(newStyle);
    printWindow.focus();
    printWindow.print();
    printWindow.close();
}


//================================================================
//    Sirve para convertir el numero en formato decimal 
//              mientras se esta escribiendo
//      SOLO SIRVE SI LE PASAS UN NUMERO ENTERO NO SIRVE 
//      PARA FORMATEAR EN EVENTOS COMO ONKEYPRES ONKEYUP
//              EN DESARROLLO :DDDDD
//=================================================================


//===================    EN DESARROLLO :DDDDD     =======================


function FormatearNumerosMientrasEscribes(valor) {
    var valor_formato = "";
    for (var i = 3; i < valor.length; i += 3) {
        var subvalor = valor.substring((valor.length - (i - 3)), (valor.length - i));
        if (valor.length > (i + 3)) {
            valor_formato = "." + subvalor + valor_formato;
        }
        else {
            valor_formato = valor.substring(0, (valor.length - i)) + "." + subvalor + valor_formato;
        }
    }
    if (valor.length > 3) {
        return valor_formato;
    }
    else {
        return valor;
    }
}


//================================================================
//    Sirve para mostrar una imagen cargada con un upload file 
//                  en un image control
//    Se debe pasar el ID del image control y upload contorl
//      SE DEBE USAR UN UPLOAD FILE DE HTML5 (FILE API)
//                      EJEMPLO
// <input ID="avatarUpload" type="file" name="file" onchange="previewFile('<%=imgDocumento.ClientID %>', '<%=avatarUpload.ClientID %>', '<%=hdnField.ClientID %>')"  runat="server" />
//=================================================================


function PrevisualizarArchivoCargadoYGuardarEnHidden(idImage, idUpload, hiddenfield) {
    var preview = document.querySelector('#' + idImage);
    var file = document.querySelector('#' + idUpload).files[0];
    var reader = new FileReader();

    reader.onloadend = function () {
        preview.src = reader.result;
        var imagenControl = document.getElementById(idImage);
        var hiddenControl = document.getElementById(hiddenfield);
        hiddenControl.value = imagenControl.src;
    }

    if (file) {
        reader.readAsDataURL(file);
    } else {
        preview.src = "";
    }
}

//================================================================
//    Sirve para mostrar una imagen cargada con un upload file 
//                  en un image control
//    Se debe pasar el ID del image control y upload contorl
//      SE DEBE USAR UN UPLOAD FILE DE HTML5 (FILE API)
//                      EJEMPLO
// <input ID="avatarUpload" type="file" name="file" onchange="previewFile('<%=imgDocumento.ClientID %>', '<%=avatarUpload.ClientID %>', '<%=hdnField.ClientID %>')"  runat="server" />
//=================================================================

function PrevisualizarArchivoCargado(idImage, idUpload) {
    var preview = document.querySelector('#' + idImage);
    var file = document.querySelector('#' + idUpload).files[0];
    var reader = new FileReader();

    reader.onloadend = function () {
        preview.src = reader.result;
        var imagenControl = document.getElementById(idImage);
    }

    if (file) {
        reader.readAsDataURL(file);
    } else {
        preview.src = "";
    }
}

//================================================================
//      Sirve para testear el archivo a cargar en un input type="file"
//          DEVUELVE TRUE CUANDO TODO ESTA BIEN Y PASA AL CODE BEHIND
//   DEVUELVE FALSE CUANDO HAY UN ERROR MUESTRA ALERTA Y NO DEJA PASAR AL CODE BEHIND
//          SOLO SIRVE PARA EL UPLOAD FILE DEL FILE API DE HTML5
//          SE DEBE PASAR EL ID DEL CONTROL
//=================================================================

function TestInputFile(uploadFileID) {
    var validFileExtensions = [".jpg", ".jpeg", ".bmp", ".png"];
    var input, file;

    // (Can't use `typeof FileReader === "function"` because apparently
    // it comes back as "object" on some browsers. So just see if it's there
    // at all.)
    if (!window.FileReader) {
        alert("File API no es soportada en este navegador, actualizelo a una version mas reciente. Gracias!.");
        return;
    }

    input = document.getElementById(uploadFileID);
    if (!input.files) { // aqui
        alert("Problemas al validar el archivo!"); // No soporta files property en files (lo que se intenta acceder en el if)
        return false;
    }
    else if (!input.files[0]) {
        alert("Selecciona un archivo antes de guardar!");
        return false;
    }

    var error = true;
    file = input.files[0];

    if (file.size > 2000000) {
        alert("Selecciona un archivo menos pesado (2MB MÁX)");
        return false;
    }

    if (file.size <= 0) {
        alert("Selecciona un archivo con contenido!.");
        return false;
    }

    for (var i = 0; i < validFileExtensions.length; i++) {
        if (file.name.includes(validFileExtensions[i])) {
            error = false
            break;
        }
    }

    if (error) {
        alert("La extensión de archivo no es válida, verifique por favor. ");
        return false;
    }

    return true;
}

//================================================================
//      Sirve para testear el archivo a cargar en un input type="file"
//          DEVUELVE TRUE CUANDO TODO ESTA BIEN Y PASA AL CODE BEHIND
//   DEVUELVE FALSE CUANDO HAY UN ERROR MUESTRA ALERTA Y NO DEJA PASAR AL CODE BEHIND
//          SOLO SIRVE PARA EL UPLOAD FILE DEL FILE API DE HTML5
//          SE DEBE PASAR EL ID DEL CONTROL
//=================================================================

function TestInputFileForImagesAndPDF(uploadFileID) {
    var validFileExtensions = [".jpg", ".jpeg", ".bmp", ".png", ".pdf"];
    var input, file;

    // (Can't use `typeof FileReader === "function"` because apparently
    // it comes back as "object" on some browsers. So just see if it's there
    // at all.)
    if (!window.FileReader) {
        alert("File API no es soportada en este navegador, actualizelo a una version mas reciente. Gracias!.");
        return;
    }

    input = document.getElementById(uploadFileID);
    if (!input.files) { // aqui
        alert("Problemas al validar el archivo!"); // No soporta files property en files (lo que se intenta acceder en el if)
        return false;
    }
    else if (!input.files[0]) {
        alert("Selecciona un archivo antes de guardar!");
        return false;
    }

    var error = true;
    file = input.files[0];

    if (file.size > 5000000) {
        alert("Selecciona un archivo menos pesado (5MB MÁX)");
        return false;
    }

    if (file.size <= 0) {
        alert("Selecciona un archivo con contenido!.");
        return false;
    }

    for (var i = 0; i < validFileExtensions.length; i++) {
        if (file.name.includes(validFileExtensions[i])) {
            error = false
            break;
        }
    }

    if (error) {
        alert("La extensión de archivo no es válida, verifique por favor. ");
        return false;
    }

    return true;
}

//================================================================
//      Sirve para testear el archivo a cargar en un input type="file"
//          DEVUELVE TRUE CUANDO TODO ESTA BIEN Y PASA AL CODE BEHIND
//   DEVUELVE FALSE CUANDO HAY UN ERROR MUESTRA ALERTA Y NO DEJA PASAR AL CODE BEHIND
//          SOLO SIRVE PARA EL UPLOAD FILE DEL FILE API DE HTML5
//          SE DEBE PASAR EL ID DEL CONTROL
//=================================================================

function TestInputFileToImportData(uploadFileID) {
    var validFileExtensions = [".txt", ".xls", ".xlsx"];
    var input, file;

    // (Can't use `typeof FileReader === "function"` because apparently
    // it comes back as "object" on some browsers. So just see if it's there
    // at all.)
    if (!window.FileReader) {
        alert("File API no es soportada en este navegador, actualizelo a una version mas reciente. Gracias!.");
        return;
    }

    input = document.getElementById(uploadFileID);
    if (!input.files) { // aqui
        alert("Problemas al validar el archivo!"); // No soporta files property en files (lo que se intenta acceder en el if)
        return false;
    }
    else if (!input.files[0]) {
        alert("Selecciona un archivo antes de guardar!");
        return false;
    }

    var error = true;
    file = input.files[0];

    if (file.size > 100000000) {
        alert("Selecciona un archivo menos pesado (100MB MÁX)");
        return false;
    }

    if (file.size <= 0) {
        alert("Selecciona un archivo con contenido!.");
        return false;
    }

    for (var i = 0; i < validFileExtensions.length; i++) {
        if (file.name.includes(validFileExtensions[i])) {
            error = false
            break;
        }
    }

    if (error) {
        alert("La extensión de archivo no es válida, verifique por favor. ");
        return false;
    }

    return true;
}

//================================================================
//      Sirve para solo permitir que un checkbox este chekeado cuando se usa un CheckBoxList
//                 SOLOR SIRVE PARA EL CONTROL DE "CheckBoxList"
//                 SE DEBE PASAR EL CONTROL EN SI CON "this"
//      <asp:CheckBoxList ID="CheckBoxList1" runat="server" Width="90%">
//          <asp:ListItem Text="Si" Value="1" onclick="ExclusiveCheckBoxList(this);" />
//          <asp:ListItem Text="No" Value="0" onclick="ExclusiveCheckBoxList(this);" />
//      </asp:CheckBoxList>
//=================================================================

function ExclusiveCheckBoxList(chk)
{
    var chkList = chk.parentNode.parentNode.parentNode;
    var chks = chkList.getElementsByTagName("input");
    for(var i=0;i<chks.length;i++)
    {
        if(chks[i] != chk && chk.checked)
        {
            chks[i].checked=false;
        }
    }
}