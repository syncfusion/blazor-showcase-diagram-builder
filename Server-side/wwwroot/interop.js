function CommonKeyboardCommands_newDiagram() {
    var origin = window.location.origin;
    if (!origin) {
        origin = window.location.protocol + '//'
            + window.location.hostname
            + (window.location.port ? ':' + window.location.port : '');
    }
    window.open(origin + window.location.pathname);
};
function getDiagramFileName(dialogName) {
    if (dialogName === 'export')
        return document.getElementById('diagramName').innerHTML.toString();
    if (dialogName === 'save')
        return document.getElementById('saveFileName').value.toString();
    else
        return document.getElementById('diagramName').innerHTML.toString();
}
function importDescription(formatName) {
    if (formatName === 'CSV')
    {
        document.getElementById('descriptionText1').innerHTML = "Make sure that each column of the table has a header";
        document.getElementById('descriptionText2').innerHTML = "Each employee should have a reporting person (except the top most employee of the organization), and it should be indicated by any field from the data source.";
        
    }
    else if (formatName == 'XML')
    {
        document.getElementById('descriptionText1').innerHTML = "Make sure that XML document has a unique root element and start-tags have matching end-tags.";
        document.getElementById('descriptionText2').innerHTML = "All XML elements will be considered employees and will act as a reporting person for its child XML elements.";
    
    }
    else
    {
        document.getElementById('descriptionText1').innerHTML = "Make sure that you have defined a valid JSON format.";
        document.getElementById('descriptionText2').innerHTML = "Each employee should have a reporting person (except the top most employee of the organization), and it should be indicated by any field from the data source.";
    }
        
}
importData = function (object) {
    var orgDataSource = [];var columnsList = []
    orgDataSource = JSON.parse(object);
    var dada = orgDataSource[0];
    for (var prop in dada) {columnsList.push(prop) }
    return columnsList
};
function saveDiagram(data, filename) {
    if (window.navigator.msSaveBlob) {
        let blob = new Blob([data], { type: 'data:text/json;charset=utf-8,' });
        window.navigator.msSaveOrOpenBlob(blob, filename + '.json');
    } else {
        let dataStr = 'data:text/json;charset=utf-8,' + encodeURIComponent(data);
        let a = document.createElement('a');
        a.href = dataStr;
        a.download = filename + '.json';
        document.body.appendChild(a);
        a.click();
        a.remove();
    }
}

function downloadCsv() {
    var csv = 'Name,EmployeeID,Role,Department,Location,Phone,Email,SupervisorName,SupervisorID,ImageURL\n';
    var data = [
        {
            'Name': 'Maria Anders', 'EmployeeID': '1', 'Role': 'Managing Director', 'Department': '',
            'Location': 'US', 'Phone': '(555) 111-1111', 'Email': 'mariaanders@fakecompany.com', 'Supervisor Name': '',
            'Supervisor ID': '', 'Image URL': '/assets/dbstyle/orgchart_images/blank-male.jpg'
        },
        {
            'Name': 'Carine Schmitt', 'EmployeeID': '2', 'Role': 'Project Manager', 'Department': 'Development',
            'Location': 'US', 'Phone': '(555) 222-2222', 'Email': 'carineschmitt@fakecompany.com', 'Supervisor Name': 'Maria Anders',
            'Supervisor ID': '1', 'Image URL': '/assets/dbstyle/orgchart_images/blank-male.jpg'
        },
        {
            'Name': 'Daniel Tonini', 'EmployeeID': '3', 'Role': 'Project Manager', 'Department': 'Development',
            'Location': 'US', 'Phone': '(555) 333-3333', 'Email': 'danieltonini@fakecompany.com', 'Supervisor Name': 'Maria Anders',
            'Supervisor ID': '1', 'Image URL': '/assets/dbstyle/orgchart_images/blank-male.jpg'
        },
        {
            'Name': 'Alex Camino', 'EmployeeID': '4', 'Role': 'Project Lead', 'Department': 'Development',
            'Location': 'US', 'Phone': '(555) 444-4444', 'Email': 'alexcamino@fakecompany.com', 'Supervisor Name': 'Daniel Tonini',
            'Supervisor ID': '3', 'Image URL': '/assets/dbstyle/orgchart_images/blank-male.jpg'
        },
        {
            'Name': 'Jones Bergson', 'EmployeeID': '5', 'Role': 'Project Lead', 'Department': 'Development',
            'Location': 'US', 'Phone': '(555) 555-5555', 'Email': 'jonesbergson@fakecompany.com', 'Supervisor Name': 'Daniel Tonini',
            'Supervisor ID': '3', 'Image URL': '/assets/dbstyle/orgchart_images/blank-male.jpg'
        },
        {
            'Name': 'Rene Phillips', 'EmployeeID': '6', 'Role': 'Project Lead', 'Department': 'Development',
            'Location': 'US', 'Phone': '(555) 666-6666', 'Email': 'renephillips@fakecompany.com', 'Supervisor Name': 'Daniel Tonini',
            'Supervisor ID': '3', 'Image URL': '/assets/dbstyle/orgchart_images/blank-male.jpg'
        },
    ];
    data.forEach(function (row) {
        for (var prop in row) {
            csv += row[prop].toString() + ',';
        }
        csv += '\n';
    });
    if (window.navigator.msSaveBlob) {
        var blob = new Blob([csv], { type: 'text/plain;charset=utf-8;' });
        window.navigator.msSaveOrOpenBlob(blob, 'people.csv');
    }
    else {
        var hiddenElement = document.createElement('a');
        hiddenElement.href = 'data:text/csv;charset=utf-8,' + encodeURI(csv);
        hiddenElement.target = '_blank';
        hiddenElement.download = 'people.csv';
        document.body.appendChild(hiddenElement);
        hiddenElement.click();
        hiddenElement.remove();
    }
}

function downloadXML() {
    var xmltext = '<?xml version="1.0" encoding="utf-8" ?><people>' +
        '<person Name="Maria Anders" Role="Managing Director" Department="Development"  Location="US" Phone="(555) 111-1111" Email="mariaanders@fakecompany.com" SupervisorName="Maria Anders" ImageURL="/assets/dbstyle/orgchart_images/blank-male.jpg">' +
        '<person Name="Carine Schmitt" Role="Project Manager" Department="Development" Location="US" Phone="(555) 222-2222" Email="carineschmitt@fakecompany.com" SupervisorName="Maria Anders" ImageURL="/assets/dbstyle/orgchart_images/blank-male.jpg"></person>' +
        '<person Name="Daniel Tonini" Role="Project Manager" Department="Development" Location="US" Phone="(555) 333-3333" Email="danieltonini@fakecompany.com" SupervisorName="Maria Anders" ImageURL="/assets/dbstyle/orgchart_images/blank-male.jpg">' +
        '<person Name="Alex Camino" Role="Project Manager" Department="Development" Location="US" Phone="(555) 444-4444" Email="alexcamino@fakecompany.com" SupervisorName="Daniel Tonini" ImageURL="/assets/dbstyle/orgchart_images/blank-male.jpg"></person>' +
        '<person Name="Jones Bergson" Role="Project Lead" Department="Development" Location="US" Phone="(555) 555-5555" Email="jonesbergson@fakecompany.com" SupervisorName="Daniel Tonini" ImageURL="/assets/dbstyle/orgchart_images/blank-male.jpg"></person>' +
        '<person Name="Rene Phillips" Role="Project Lead" Department="Development" Location="US" Phone="(555) 666-6666" Email="renephillips@fakecompany.com" SupervisorName="Daniel Tonini" ImageURL="/assets/dbstyle/orgchart_images/blank-male.jpg"></person>' +
        '</person>' +
        '</person>' +
        '</people>';
    var filename = 'people.xml';
    var bb = new Blob([xmltext], { type: 'text/plain' });
    if (window.navigator.msSaveBlob) {
        window.navigator.msSaveOrOpenBlob(bb, filename);
    }
    else {
        var pom = document.createElement('a');
        pom.setAttribute('href', window.URL.createObjectURL(bb));
        pom.setAttribute('download', filename);
        document.body.appendChild(pom);
        pom.click();
        pom.remove();
    }
}
function downloadFile(data, filename) {
    let dataStr = 'data:text/json;charset=utf-8,' + encodeURIComponent(data);
    let a = document.createElement('a');
    a.href = dataStr;
    a.download = filename + '.json';
    document.body.appendChild(a);
    a.click();
    a.remove();
}
UtilityMethods_hideElements = function (elementType, diagramType) {
    var diagramContainer = document.getElementsByClassName('diagrambuilder-container')[0];
    if (diagramContainer.classList.contains(elementType)) {
        if (!(diagramType === 'mindmap-diagram' || diagramType === 'orgchart-diagram')) {
            diagramContainer.classList.remove(elementType);
        }
    }
    else {
        diagramContainer.classList.add(elementType);
    }
    window.dispatchEvent(new Event('resize'));
};
function hideMenubar() {
    UtilityMethods_hideElements('hide-menubar');
}
function getHyperLinkValueFromDocument(id, attribute) {
    return document.getElementById(id).value;
}
function setHyperLinkValuesToDocument(id, value) {
    document.getElementById(id).value = value;
}
function click() {
    document.getElementById('UploadFiles').click();
}
function hideElements(elementType) {
    var diagramContainer = document.getElementsByClassName('diagrambuilder-container')[0];
    if (diagramContainer.classList.contains(elementType)) {
        diagramContainer.classList.remove(elementType);
    } else {
        diagramContainer.classList.add(elementType);
    }
}
function click() {
    document.getElementById('defaultfileupload').click();
}
function loadFile(file) {
    var base64 = file.rawFile.replace("data:application/json;base64,", "");
    var json = atob(base64)
    return json;
}
function loadCSVFile(file) {

    var base64 = file.rawFile.replace("data:text/csv;base64,", "");
    var json = atob(base64)
    return json;
    
}
function loadXMLFile(file) {

    var base64 = file.rawFile.replace("data:text/xml;base64,", "");
    var json = atob(base64)
    return json;

}
function loadDiagram(event) {
    //var reader = new FileReader();
    //var str=  reader.readAsText(event);
    return str.target.result.toString();
}

function diagramNameKeyDown(args) {
    if (args.which === 13) {
        document.getElementById('diagramName').innerHTML = document.getElementById('diagramEditable').value;
        document.getElementsByClassName('db-diagram-name-container')[0].classList.remove('db-edit-name');
    }
}

function diagramNameChange(args, isSet) {
    if (isSet) {
        document.getElementById('diagramName').innerHTML = args;
    }
    else {
        document.getElementById('diagramName').innerHTML = document.getElementById('diagramEditable').value;
        document.getElementsByClassName('db-diagram-name-container')[0].classList.remove('db-edit-name');
        //document.getElementById("exportfileName").value = document.getElementById('diagramName').innerHTML;
    }
}
function renameDiagram1(args) {
    document.getElementsByClassName('db-diagram-name-container')[0].classList.add('db-edit-name');
    var element = document.getElementById('diagramEditable');
    element.value = document.getElementById('diagramName').innerHTML;
    element.focus();
    element.select();
}
UtilityMethods_native = function (object) {
    var selectedItems = JSON.parse(object);    
    console.log(selectedItems);
};
function pageSizeUpdate() {
    window.dispatchEvent(new Event('resize'));
}

window.downloadPdf = function downloadPdf(base64String, fileName) {
    var sliceSize = 512;
    var byteCharacters = atob(base64String);
    var byteArrays = [];

    for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
        var slice = byteCharacters.slice(offset, offset + sliceSize);
        var byteNumbers = new Array(slice.length);

        for (var i = 0; i < slice.length; i++) {
            byteNumbers[i] = slice.charCodeAt(i);
        }

        var byteArray = new Uint8Array(byteNumbers);
        byteArrays.push(byteArray);
    }

    var blob = new Blob(byteArrays, {
        type: 'application/pdf'
    });
    var blobUrl = window.URL.createObjectURL(blob);
    this.triggerDownload("PDF", fileName, blobUrl);
}

triggerDownload: function triggerDownload(type, fileName, url) {
    var anchorElement = document.createElement('a');
    anchorElement.download = fileName + '.' + type.toLocaleLowerCase();
    anchorElement.href = url;
    anchorElement.click();
}
var getId;
function getViewportBounds() {

    var bounds = document.getElementsByClassName('e-control e-diagram e-lib e-droppable e-tooltip')[0].getBoundingClientRect();

    return { width: bounds.width, height: bounds.height };

}
