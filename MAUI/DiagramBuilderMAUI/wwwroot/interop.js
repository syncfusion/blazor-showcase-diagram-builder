function renameDiagram(args) {
    document.getElementsByClassName('db-diagram-name-container')[0].classList.add('db-edit-name');
    var element = document.getElementById('diagramEditable');
    element.value = document.getElementById('diagramName').innerHTML;
    element.focus();
    element.select();
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
        document.getElementById("exportfileName").value = document.getElementById('diagramName').innerHTML;
    }
}

function getDiagramFileName(dialogName) {
    if (dialogName === 'export')
        return document.getElementById('diagramName').innerHTML.toString();
    if (dialogName === 'save')
        return document.getElementById('saveFileName').value.toString();
    else
    return document.getElementById('diagramName').innerHTML.toString();
}

function UtilityMethods_enableToolbarItems(selectedItems) {
    selectedItems = JSON.parse(selectedItems);
    var toolbarContainer = document.getElementsByClassName('db-toolbar-container')[0];
    var toolbarClassName = 'db-toolbar-container';
    if (toolbarContainer.classList.contains('db-undo')) {
        toolbarClassName += ' db-undo';
    }
    if (toolbarContainer.classList.contains('db-redo')) {
        toolbarClassName += ' db-redo';
    }
    toolbarContainer.className = toolbarClassName;
    if (selectedItems.length === 1) {
        if (selectedItems[0].tooltip.content) {
            (document.getElementById('objectTooltip')).value = selectedItems[0].tooltip.content;
        }
        else {
            (document.getElementById('objectTooltip')).value = '';
        }
        toolbarContainer.className = toolbarContainer.className + ' db-select';
        if (selectedItems[0].children) {
            if (selectedItems[0].children) {
                if (selectedItems[0].children.length > 2) {
                    toolbarContainer.className = toolbarContainer.className + ' db-select db-double db-multiple db-node db-group';
                }
                else {
                    toolbarContainer.className = toolbarContainer.className + ' db-select db-double db-node db-group';
                }
            }
            else {
                toolbarContainer.className = toolbarContainer.className + ' db-select db-node';
            }
        }
    }
    else if (selectedItems.length === 2) {
        toolbarContainer.className = toolbarContainer.className + ' db-select db-double';
    }
    else if (selectedItems.length > 2) {
        toolbarContainer.className = toolbarContainer.className + ' db-select db-double db-multiple';
    }
    if (selectedItems.length > 1) {
        var isNodeExist = false;
        for (var i = 0; i < selectedItems.length; i++) {
            if (!selectedItems[i].segements) {
                toolbarContainer.className = toolbarContainer.className + ' db-select db-node';
                break;
            }
        }
    }
};
UtilityMethods_objectTypeChange = function (objectType) {
    document.getElementById('diagramPropertyContainer').style.display = 'none';
    document.getElementById('nodePropertyContainer').style.display = 'none';
    document.getElementById('textPropertyContainer').style.display = 'none';
    document.getElementById('connectorPropertyContainer').style.display = 'none';
    switch (objectType) {
        case 'diagram':
            document.getElementById('diagramPropertyContainer').style.display = '';
            break;
        case 'node':
            document.getElementById('nodePropertyContainer').style.display = '';
            break;
        case 'connector':
            document.getElementById('connectorPropertyContainer').style.display = '';
            break;
    }
};

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
};

function hideMenubar() {
    UtilityMethods_hideElements('hide-menubar');
}

function DiagramClientSideEvents_historyChange(historyManager) {
    var diagram = document.getElementById("diagram").ej2_instances[0];
    var data = { undo: false, redo: false };
    if (diagram) {
        historyManager = diagram.historyManager;
        var toolbarContainer = document.getElementsByClassName('db-toolbar-container')[0];
        toolbarContainer.classList.remove('db-undo');
        toolbarContainer.classList.remove('db-redo');
        if (historyManager.undoStack.length > 0) {
            toolbarContainer.classList.add('db-undo');
            data.undo = true;
        }
        if (historyManager.redoStack.length > 0) {
            toolbarContainer.classList.add('db-redo');
            data.redo = true;
        }
    }
    return data;
};

function getDiagramHistoryList(isUndo) {
    var diagram = document.getElementById("diagram").ej2_instances[0];
    if (diagram) {
        historyManager = diagram.historyManager;
        if (isUndo)
            return historyManager.undoStack.length.toString();
        else
            return historyManager.redoStack.length.toString();
    }
    return '';
}

function removeSelectedToolbarItem(tool) {
    document.getElementById('btnDrawShape').classList.remove('tb-item-selected');
    document.getElementById('btnDrawConnector').classList.remove('tb-item-selected');
    if (tool === "shape") {
        document.getElementById('btnDrawShape').classList.add('tb-item-selected');
    }
    else if (tool === "connector") {
        document.getElementById('btnDrawConnector').classList.add('tb-item-selected');
    }
}
function RestartApplication() {
    location.reload();
}
function printContent(diagram) {
    var content = document.getElementById(diagram);
    var originalContents = document.body.innerHTML;
    document.body.innerHTML = content.innerHTML;
    window.print();
    document.body.innerHTML = originalContents;
}
function CommonKeyboardCommands_newDiagram() {
    var origin = window.location.origin;
    if (!origin) {
        origin = window.location.protocol + '//'
            + window.location.hostname
            + (window.location.port ? ':' + window.location.port : '');
    }
    window.open(origin + window.location.pathname);
};

function getTooltipText(content) {
    return (document.getElementById('objectTooltip')).value;
}
function setShortCutKey(args) {
    var shortcut = getShortCutKey(args.item.text);
    if (shortcut) {
        var span = document.createElement("span");
        span.style.pointerEvents = 'none';
        span.classList.add("db-shortcut");
        span.textContent = shortcut;
        document.getElementById(args.element.id).appendChild(span);
    }
}
function getShortCutKey(menuItem)
{
    var shortCutKey = navigator.platform.indexOf('Mac') > -1 ? 'Cmd' : 'Ctrl';
    switch (menuItem) {
        case "New":
            shortCutKey = "Shift" + "+N";
            break;
        case "Open":
            shortCutKey = shortCutKey + "+O";
            break;
        case "Save":
            shortCutKey = shortCutKey + "+S";
            break;
        case "Undo":
            shortCutKey = shortCutKey + "+Z";
            break;
        case "Redo":
            shortCutKey = shortCutKey + "+Y";
            break;
        case "Cut":
            shortCutKey = shortCutKey + "+X";
            break;
        case "Copy":
            shortCutKey = shortCutKey + "+C";
            break;
        case "Paste":
            shortCutKey = shortCutKey + "+V";
            break;
        case "Delete":
            shortCutKey = "Delete";
            break;
        case "Duplicate":
            shortCutKey = shortCutKey + "+D";
            break;
        case "Select All":
            shortCutKey = shortCutKey + "+A";
            break;
        case "Zoom In":
            shortCutKey = shortCutKey + "++";
            break;
        case "Zoom Out":
            shortCutKey = shortCutKey + "+-";
            break;
        case "Group":
            shortCutKey = shortCutKey + "+G";
            break;
        case "Ungroup":
            shortCutKey = shortCutKey + "+U";
            break;
        case "Send To Back":
            shortCutKey = shortCutKey + "+Shift+B";
            break;
        case "Bring To Front":
            shortCutKey = shortCutKey + "+Shift+F";
            break;
        default:
            shortCutKey = "";
            break;
    }
    return shortCutKey;
}

function setHyperLinkValuesToDocument(id, value) {
    document.getElementById(id).value = value;
}

function getHyperLinkValueFromDocument(id, attribute) {
    return document.getElementById(id).value;
}

function click() {
    document.getElementById('UploadFiles').click();
}

function enablePropertyCheckBox(id, value) {
    var element = document.getElementById(id);
    if (value) {
        if (id == "gradientStyle") {
            element.className = 'row db-prop-row db-gradient-style-show';
        } else {
            element.style.display = '';
        }
    }
    else {
        if (id == "gradientStyle") {
            element.className = 'row db-prop-row db-gradient-style-hide';
        } else {
            element.style.display = 'none';
        }
    }
}

function objectTypeChange(objectType, isTextNode) {
    if (objectType != "annotation") {
        document.getElementById('diagramPropertyContainer').style.display = 'none';
        document.getElementById('nodePropertyContainer').style.display = 'none';
        document.getElementById('textPropertyContainer').style.display = 'none';
        document.getElementById('connectorPropertyContainer').style.display = 'none';
    }
    switch (objectType) {
        case 'diagram':
            document.getElementById('diagramPropertyContainer').style.display = '';
            break;
        case 'node':
            document.getElementById('nodePropertyContainer').style.display = '';
            break;
        case 'connector':
            document.getElementById('connectorPropertyContainer').style.display = '';
            break;
        case 'annotation':
            document.getElementById('textPropertyContainer').style.display = '';
            if(isTextNode){
                document.getElementById('toolbarTextAlignmentDiv').style.display = 'none';
                document.getElementById('textPositionDiv').style.display = 'none';
                document.getElementById('textColorDiv').className = 'col-xs-6 db-col-left';
            } else {
                document.getElementById('toolbarTextAlignmentDiv').style.display = '';
                document.getElementById('textPositionDiv').style.display = '';
                document.getElementById('textColorDiv').className = 'col-xs-6 db-col-right';
            }
    }
}

function updateToolbarState(toolbarName, isSelected, index) {
    var toolbarTextStyle = document.getElementById(toolbarName);
    if (toolbarTextStyle) {
        toolbarTextStyle = toolbarTextStyle.ej2_instances[0];
    }
    if (toolbarTextStyle) {
        var cssClass = toolbarTextStyle.items[index].cssClass;
        toolbarTextStyle.items[index].cssClass = isSelected ? cssClass + ' tb-item-selected' : cssClass.replace(' tb-item-selected', '');
        toolbarTextStyle.dataBind();
    }
}

function removeClassInElement(id, className){
    document.getElementById(id).classList.remove(className);
}

function hideElements(elementType) {
    var diagramContainer = document.getElementsByClassName('diagrambuilder-container')[0];
    if (diagramContainer.classList.contains(elementType)) {
        diagramContainer.classList.remove(elementType);
    } else {
        diagramContainer.classList.add(elementType);
    }
}

function CustomPageSettingsChange(value) {
    if (value) {
        document.getElementById('pageDimension').style.display = '';
        document.getElementById('pageOrientation').style.display = "none";
    }
    else {
        document.getElementById('pageDimension').style.display = "none";
        document.getElementById('pageOrientation').style.display = "";
    }
}

function addClassInElement(id, className) {
    document.getElementById(id).classList.add(className);
}

function showSpinnerDiagram(isDiagram) {
    //document.getElementById("diagrambuilder-container").style.visibility = "visible";
    //document.getElementById("spinner").style.visibility = "hidden";
    var loader = document.querySelector(".sb-loading");
    loader.classList.add("sb-hide");
    loader.classList.remove("sb-trans");
    var bodyOverlay = document.querySelector(".sb-body-overlay");
    bodyOverlay.classList.add("sb-hide");
    bodyOverlay.classList.remove("sb-trans");
}
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

function setWaterLevel(value) {
    var slider = document.getElementById('slider')
    var percent = slider.value;
    document.getElementById('node1_content_linear').children[0].setAttribute("offset", percent + '%');
    document.getElementById('node1_content_linear').children[1].setAttribute("offset", percent + '%');
}

function click() {
    document.getElementById('defaultfileupload').click();
}
function loadFile(file) {
    //   var blob = new File(file);

    var base64 = file.rawFile.replace("data:application/json;base64,", "");
    var json = atob(base64)
    return json;
}

function loadDiagram(event) {
    return event.target.result.toString();
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
function getViewportBounds() {

    var bounds = document.getElementsByClassName('e-control e-diagram e-lib e-droppable e-tooltip')[0].getBoundingClientRect();

    return { width: bounds.width, height: bounds.height };

}