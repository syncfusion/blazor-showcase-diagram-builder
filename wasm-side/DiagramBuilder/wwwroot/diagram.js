window.createHtmlElement = function (elementType, attribute) {
    var element = document.createElement(elementType);
    if (attribute) {
        this.setAttribute(element, attribute);
    }
    return element;
};
window.setAttribute = function (element, attributes) {
    var keys = Object.keys(attributes);
    for (var i = 0; i < keys.length; i++) {
        element.setAttribute(keys[i], attributes[keys[i]]);
    }
};
window.createMeasureElements = function (isZoomValue, layerList, width, height) {
    updateZoomPanTool(isZoomValue);
    updateInnerLayerSize(layerList, width, height);
    var measureWindowElement = 'measureElement';
    if (!window[measureWindowElement]) {
        var divElement = createHtmlElement('div', {
            id: 'measureElement',
            style: 'visibility:hidden ; height: 0px ; width: 0px; overflow: hidden;'
        });
        var text = createHtmlElement('span', { 'style': 'display:inline-block ; line-height: normal' });
        divElement.appendChild(text);
        var imageElement = void 0;
        imageElement = createHtmlElement('img', {});
        divElement.appendChild(imageElement);
        var svg = document.createElementNS('http://www.w3.org/2000/svg', 'svg');
        svg.setAttribute('xlink', 'http://www.w3.org/1999/xlink');
        divElement.appendChild(svg);
        var element = document.createElementNS('http://www.w3.org/2000/svg', 'path');
        svg.appendChild(element);
        var data = document.createTextNode('');
        var tSpan = document.createElementNS('http://www.w3.org/2000/svg', 'text');
        tSpan.setAttributeNS('http://www.w3.org/XML/1998/namespace', 'xml:space', 'preserve');
        svg.appendChild(tSpan);
        window[measureWindowElement] = divElement;
        window[measureWindowElement].usageCount = 1;
        document.body.appendChild(divElement);
        var measureElementCount = 'measureElementCount';
        if (!window[measureElementCount]) {
            window[measureElementCount] = 1;
        }
        else {
            window[measureElementCount]++;
        }
    }
    else {
        window[measureWindowElement].usageCount += 1;
    }
};
window.measurePath = function (data) {
    if (data) {
        var measureWindowElement = 'measureElement';
        window[measureWindowElement].style.visibility = 'visible';
        var svg = window[measureWindowElement].children[2];
        var element = getChildNode(svg)[0];
        element.setAttribute('d', data);
        var bounds = element.getBBox();
        var svgBounds = {x: bounds.x, y: bounds.y, width: bounds.width, height: bounds.height };
        window[measureWindowElement].style.visibility = 'hidden';
        return svgBounds;
    }
    return { X: 0, Y: 0, Width: 0, Height: 0 };
};

window.measureBounds = async function (pathobj, textObj, imageObj, nativeObj) {
    if (nativeObj != null) {
        var accordianPanel = document.getElementsByClassName("e-acrdn-panel e-content-hide");
        var previousValue = []
        for (var k = 0; k < accordianPanel.length; k++) {
            previousValue[k] = accordianPanel[k].style.display;
            accordianPanel[k].style.display = "block"
        }
    }
    var finalResult = {};
    var pathResult = {};
    var textResult = {};
    var imageResult = {};
    var nativeResult = {};
    var measureWindowElement = 'measureElement';
    if (pathobj) {
        var result = Object.keys(pathobj).map((key) => [pathobj[key], key]);
        window[measureWindowElement].style.visibility = 'visible';
        var svg = window[measureWindowElement].children[2];
        var element = getChildNode(svg)[0];

        for (var i = 0; i < result.length; i++) {
            if (result[i][0] == "Path") {
                var data = result[i][1];
                element.setAttribute('d', data);
                var bounds = element.getBBox();
                var svgBounds = { x: bounds.x, y: bounds.y, width: bounds.width, height: bounds.height };
                pathResult[data] = svgBounds;
            } else if (result[i][0].indexOf("GetBoundingClientRect") != -1) {
                var dom = document.getElementById(result[i][1]);
                var bounds = dom.getBoundingClientRect();
                pathResult[result[i][0]] = { x: bounds.x, y: bounds.y, width: bounds.width, height: bounds.height };;
                if (result[i][0] == "GetBoundingClientRect") {
                    window.scrollDiagramID = result[i][1];
                    pathResult["GetScrollerBounds"] = measureScrollValues(result[i][1]);
                }
            }
        }
    }
    if (textObj) {
        var result = Object.keys(textObj).map((key) => [textObj[key], key]);
        for (var i = 0; i < result.length; i++) {
            var data = result[i][1];
            var content = textObj[data].content;
            var style = textObj[data].style;
            var size = textObj[data].bounds;
            var nodeSz = textObj[data].nodeSize;
            size.width = size.width == null ? undefined : size.width;
            size.height = size.height == null ? undefined : size.height;
            nodeSz.width = nodeSz.width == null ? undefined : nodeSz.width;
            nodeSz.height = nodeSz.height == null ? undefined : nodeSz.height;
            textResult[data] = measureText(size, style, content, (size.width || nodeSz.width));
        }
    }
    if (imageObj) {
        var images = Object.keys(imageObj).map(function (key) { return [imageObj[key], key]; });
        if (images.length > 0) {
            var value = 0;
            var result = {};
            await this.loadImage(images, value, result);
            imageResult = result;
        }
    }
    if (nativeObj) {
        var result = Object.keys(nativeObj).map((key) => [nativeObj[key], key]);
        if (result.length > 0) {
            var results = {};
            var nativeSize = {};
            for (var i = 0; i < result.length; i++) {
                var nativeId = result[i][0];
                var nativeBounds = document.getElementById(nativeId);
                var svgBounds = nativeBounds.getBoundingClientRect();
                nativeSize = { width: svgBounds.width, height: svgBounds.height };
                results[result[i][1]] = nativeSize;
            }
            nativeResult = results;
        }
    }
    pathResult["GetScrollerWidth"] = getScrollerWidth();
    finalResult["Path"] = pathResult;
    finalResult["Text"] = textResult;
    finalResult["Image"] = imageResult;
    finalResult["Native"] = nativeResult;
    if (previousValue != null) {
        for (var k = 0; k < accordianPanel.length; k++) {
            accordianPanel[k].style.display = previousValue[k];
        }
    }
    window[measureWindowElement].style.visibility = 'hidden';
    return finalResult;
};

//Symbol palette Snippet Starts here 
window.initialiseModule = async function (element, component) {
    
    var symbolPaletteInstance = 'symbolPaletteInstance';
    if (window[symbolPaletteInstance]) {
        var object = { id: element.children[0].id, componentInstance: component }
        window[symbolPaletteInstance].push(object);
    } else {
        var object = { id: element.children[0].id, componentInstance: component }
        window[symbolPaletteInstance] = []
        window[symbolPaletteInstance].push(object);
    }
    

    element.addEventListener('mousedown',
        _.throttle(e =>
            invokePaletteEvents(e, component),
            0)
    );
    element.addEventListener('mousemove', _.throttle(e =>
        invokePaletteEvents(e, component), 0));
    element.addEventListener('mouseup', _.throttle(e =>
        invokePaletteEvents(e, component), 0));
    setTimeout(function () {
        this.symbolPaletteDragAndDropModule = new InitDraggable(element, window[symbolPaletteInstance]);
    }, 10000);
}
function invokePaletteEvents(e, component) {
    const invokePaletteEvents = "InvokePaletteEvents";
    const symbolDraggableClass = "e-symbol-draggable"
    const symbolhoverClass = "e-symbol-hover"
    e.preventDefault()
    var args = PalettegetMouseEvents(e);
    if (e.target.id.split('_').length === 2) {
        var symbolId = e.target.id.split('_')[0];
    }
    if ((e.type == "mousemove" && !window.eventStarted) || e.type != "mousemove" || !window.inAction) {
        if (e && e.target && e.type) {
            var element = document.getElementById(e.target.id)
            if (element) {
                for (var k = 0; k < element.classList.length; k++) {
                    if (element.classList[k] == symbolDraggableClass) {
                        var container = e.target;
                        container.classList.add(symbolhoverClass);
                        break;
                    }
                }
                var hoverElementCount = document.getElementsByClassName(symbolhoverClass)
                if (hoverElementCount && hoverElementCount.length > 0) {
                    for (var a = 0; a < hoverElementCount.length; a++) {
                        var oldcontainer = hoverElementCount[a]
                        if (container && (container != oldcontainer) || container == undefined) {
                            oldcontainer.classList.remove(symbolhoverClass);

                        }
                    }
                }
            }
        }
        component.invokeMethodAsync(invokePaletteEvents, args, symbolId);

    }
}
var InitDraggable =
    /** @class */
    function () {
        /**
         * Constructor for the Symbol Palette Draggable Component
         * @hidden
         */
        function InitDraggable(parent, symbolPaletteInstance) {
            var _this = this;
            this.over = function (e) {
                const previewElementValue = "previewElement";
                const symbolPaletteDragEnter = "SymbolPaletteDragEnter"
                var previewElement = document.getElementById(previewElementValue);
                var component;
                for (var i = symbolPaletteInstance.length - 1; i >= 0; i--) {
                    if (e.dragData.draggable.id === symbolPaletteInstance[i].id) {
                        component = symbolPaletteInstance[i].componentInstance;
                        break;
                    }
                }
                component.invokeMethodAsync(symbolPaletteDragEnter, e.target.id.split("_")[0]);
                if (previewElement) {
                    sf.base.remove(previewElement);
                    var check = document.getElementsByClassName("e-cloneproperties e-draganddrop e-grid e-dragclone")
                    check[0].style.width = "1px"
                    check[0].style.height = "1px"
                }
            }
            this.drop = function (e) {
                var component;
                const symbolPaletteDrop = "SymbolPaletteDrop";
                const diagramClass = "e-diagram";
                for (var i = symbolPaletteInstance.length - 1; i >= 0; i--) {
                    if (e.dragData.draggable.id === symbolPaletteInstance[i].id) {
                        component = symbolPaletteInstance[i].componentInstance;
                        break;
                    }
                }
                var ParentElement = parentsUntil(e.target, diagramClass)
                component.invokeMethodAsync(symbolPaletteDrop, ParentElement.id);
                sf.base.remove(e.droppedElement);
            };
            this.out = function (e) {
                var component;
                var symbolPaletteDragLeave = "SymbolPaletteDragLeave"
                for (var i = symbolPaletteInstance.length - 1; i >= 0; i--) {
                    if ((e.target.children[0].id === symbolPaletteInstance[i].id) || (e.target.parentNode.parentNode.parentElement.id === symbolPaletteInstance[i].id)) {
                        component = symbolPaletteInstance[i].componentInstance;
                        break;
                    }
                }
                component.invokeMethodAsync(symbolPaletteDragLeave);
            }

            this.helper = function (e) {
                const previewID = "previewID";
                const symbolSelectedClass = "e-symbol-selected";
                const accordianControl = "e-control e-accordion";
                const renderPreviewSymbol = 'RenderPreviewSymbol';
                const symbolHover = "e-symbol-hover";//helperElement
                const helperElement = "helperElement";
                //Used to check and remove the selected CSS class names starts here
                var SymbolSelection = document.getElementsByClassName(symbolSelectedClass)
                for (var a = 0; a < SymbolSelection.length; a++) {
                    if (target != SymbolSelection) {
                        SymbolSelection[a].classList.remove(symbolSelectedClass)
                    }
                }
                //Used to check and remove the selected CSS class names Ends     here
                var PaletteControl = document.getElementsByClassName(accordianControl)[0];
                var target = _this.draggable.currentStateTarget;

                var visualElement = sf.base.createElement('div', {
                    className: 'e-cloneproperties e-draganddrop e-grid e-dragclone',
                    styles: 'height:"auto",  width:' + PaletteControl.offsetWidth
                });
                var id = e.sender.srcElement.id
                var checkElement = document.getElementById(id)
                for (var k = 0; k < checkElement.classList.length; k++) {
                    if (checkElement.classList[k] === symbolHover) {
                        checkElement.classList.add(symbolSelectedClass)
                    }
                }
                var previewElement = document.getElementById(previewID)
                if (previewElement === null) {
                    previewElement = e.sender.target
                }

                if (previewElement) {
                    visualElement.setAttribute("id", helperElement)
                    document.body.appendChild(visualElement);
                    return visualElement;
                }
            };

            this.dragStart = function (e) {
                e.bindEvents(e.dragElement);
            };

            this.drag = function (e) {
                const diagramClass = "e-diagram";
                if (!parentsUntil(e.target, diagramClass)) {
                    const helperElement = "helperElement";
                    const previewID = "previewID";
                    const previewElementValue = "previewElement"
                    var previewElement = document.getElementById(previewID);
                    if (previewElement) {
                        var visualElement = sf.base.createElement('div', {
                            className: 'e-cloneproperties e-draganddrop e-grid e-dragclone',
                            styles: 'height:"auto";  width:' + previewElement.style.width
                        });

                        var cln = previewElement.cloneNode(true);
                        cln.style.display = "Block"
                        cln.style.visibility = true;

                        cln.setAttribute("id", "clonedNode")
                        visualElement.appendChild(cln)
                        visualElement.setAttribute("id", previewElementValue)
                        var a = document.getElementById(helperElement)
                        if (a.children[0]) {
                            a.removeChild(a.children[0])
                        }
                        a.appendChild(visualElement)
                    }
                }
            };

            this.dragStop = function (e) {
                const diagramClass = "e-diagram";
                if (!parentsUntil(e.target, diagramClass)) {
                    const helperElements = "helperElement";
                    var helperElement = document.getElementById(helperElements)
                    helperElement.remove()
                }
            };
            function parentsUntil(elem, selector, isID) {
                var parent = elem;
                while (parent) {
                    if (isID ? parent.id === selector : hasClass(parent, selector)) {
                        break;
                    }
                    parent = parent.parentNode;
                }
                return parent;
            }
            function hasClass(element, className) {
                var eClassName = (typeof element.className === 'object') ? element.className.animVal : element.className;
                return ((' ' + eClassName + ' ').indexOf(' ' + className + ' ') > -1) ? true : false;
            }

            this.initializeDrag(parent);
        }

        InitDraggable.prototype.initializeDrag = function (parent) {
            var element = parent.children[0]
            this.draggable = new sf.base.Draggable(element, {
                dragTarget: '.e-symbol-draggable',
                helper: this.helper,
                dragStart: this.dragStart,
                drag: this.drag,
                dragStop: this.dragStop,
                preventDefault: false
            });
            var droppableElements = document.getElementsByClassName("e-control e-diagram e-lib e-droppable e-tooltip")

            for (var i = 0; i < droppableElements.length; i++) {
                this.droppable = new sf.base.Droppable(droppableElements[i], {
                    accept: '.e-dragclone',
                    drop: this.drop,
                    over: this.over,
                    out: this.out
                });
            }


        };
        /**
        * To destroy the Draggable
        * @return {void}
        * @hidden
        */


        InitDraggable.prototype.destroy = function () {
            this.draggable.destroy();
        };

        return InitDraggable;
    }();
function PalettegetMouseEvents(evt) {
    var mouseEventArgs = {};
    mouseEventArgs = {
        altKey: evt.altKey, shiftKey: evt.shiftKey, ctrlKey: evt.ctrlKey, detail: evt.detail,
        metaKey: evt.metaKey, screenX: evt.screenX, screenY: evt.screenY,
        clientX: evt.clientX, clientY: evt.clientY,
        offsetX: evt.offsetX, offsetY: evt.offsetY, type: evt.type
    }

    return mouseEventArgs;
}
//Symbol palette Snippet Ends here here 

window.getScrollerWidth = function () {
    var outer = createHtmlElement('div', { 'style': 'visibility:hidden; width: 100px' });
    document.body.appendChild(outer);
    var widthNoScroll = outer.getBoundingClientRect().width;
    outer.style.overflow = 'scroll';
    var inner = createHtmlElement('div', { 'style': 'width:100%' });
    outer.appendChild(inner);
    var widthWithScroll = inner.getBoundingClientRect().width;
    outer.parentNode.removeChild(outer);
    var svgBounds = { X: 0, Y: 0, Width: widthNoScroll - widthWithScroll, Height: 0 };
    return svgBounds;
}

window.measureScrollValues = function (diagramId) {
    var element = document.getElementById(diagramId);
    var point = { X: element.scrollLeft, Y: element.scrollTop, Width: element.scrollWidth, Height: element.scrollHeight };
    return point;
}
window.pathPoints = async function (pathPointsObj) {
    var pathPoints = {};
    if (pathPointsObj) {
        var result = Object.keys(pathPointsObj).map((key) => [pathPointsObj[key], key]);
        for (var i = 0; i < result.length; i++) {
            if (result.length > 0) {
                var data = result[i][1];
                pathPoints[result[i][0]] = findSegmentPoints(data);
            }
        }
    }
    return pathPoints;
}
window.findSegmentPoints = function (pathData) {
    var pts = [];
    var sample;
    var sampleLength;
    var measureWindowElement = 'measureElement';
    window[measureWindowElement].style.visibility = 'visible';
    var svg = window[measureWindowElement].children[2];
    var pathNode = getChildNode(svg)[0];
    pathNode.setAttributeNS(null, 'd', pathData);
    var pathLength = pathNode.getTotalLength();
    for (sampleLength = 0; sampleLength <= pathLength; sampleLength += 10) {
        sample = pathNode.getPointAtLength(sampleLength);
        pts.push({ X: sample.x, Y: sample.y });
    }
    window[measureWindowElement].style.visibility = 'hidden';
    return pts;
};

window.measureText = function (size, style, content, maxWidth, textValue) {
    var finalResult = {};
    var bounds = { width: 0, height: 0 };
    var childNodes;
    var wrapBounds;
    var options = getTextOptions(content, size, style, maxWidth);
    childNodes = wrapSvgText(options, textValue, maxWidth);
    wrapBounds = wrapSvgTextAlign(options, childNodes);
    bounds.width = wrapBounds.width;
    if (wrapBounds.width >= maxWidth && options.textOverflow !== 'Wrap') {
        bounds.width = maxWidth;
    }
    bounds.height = childNodes.length * style.fontSize * 1.2;
    if (wrapBounds.width > options.width && options.textOverflow !== 'Wrap' && options.textWrapping === 'NoWrap') {
        childNodes[0].text = overFlow(options.content, options);
    }
    finalResult["Bounds"] = bounds;
    finalResult["WrapBounds"] = wrapBounds;
    finalResult["ChildNodes"] = childNodes;
    return finalResult;
};
window.getTextOptions = function (content, size, style, maxWidth) {
    var options = {
        fill: style.fill, stroke: style.strokeColor,
        strokeWidth: style.strokeWidth,
        dashArray: style.strokeDashArray, opacity: style.opacity,
        gradient: style.gradient,
        width: maxWidth || size.width, height: size.height,
    };
    options.fontSize = style.fontSize;
    options.fontFamily = style.fontFamily;
    options.textOverflow = style.textOverflow;
    options.textDecoration = style.textDecoration;
    options.doWrap = style.doWrap;
    options.whiteSpace = whiteSpaceToString(style.whiteSpace, style.textWrapping);
    options.content = content;
    options.textWrapping = style.textWrapping;
    options.breakWord = wordBreakToString(style.textWrapping);
    options.textAlign = textAlignToString(style.textAlign);
    options.color = style.color;
    options.italic = style.italic;
    options.bold = style.bold;
    options.dashArray = '';
    options.strokeWidth = 0;
    options.fill = '';
    return options;
};
async function loadImage(images, value, result) {
    var promise = new Promise(function (resolve, reject) {
        var imageSize = {};
        var measureWindowElement = 'measureElement';
        window[measureWindowElement].style.visibility = 'visible';
        var imageElement = window[measureWindowElement].children[1];
        imageElement.setAttribute('src', images[value][0]);
        window[measureWindowElement].style.visibility = 'hidden';
        var element = document.createElement('img');
        element.setAttribute('src', images[value][0]);
        this.setAttributeHtml(element, { id: "imagesf" + value + "imageNode", style: 'display: none;' });
        document.body.appendChild(element);
        element.onload = function () {
            var loadedImage = event.currentTarget;
            imageSize = { width: loadedImage.width, height: loadedImage.height };
            resolve(imageSize);
        };
    });
    result[images[value][1]] = await promise;
    if (value == (images.length - 1)) {
        return result
    } else {
        value++;
        await loadImage(images, value, result);
    }
}

function setAttributeHtml(element, attributes) {
    var keys = Object.keys(attributes);
    for (var i = 0; i < keys.length; i++) {
        if (keys[i] !== 'style') {
            element.setAttribute(keys[i], attributes[keys[i]]);
        }
        else {
            this.applyStyleAgainstCsp(element, attributes[keys[i]]);
        }
    }
}
function applyStyleAgainstCsp(svg, attributes) {
    var keys = attributes.split(';');
    var attribute;
    for (var i = 0; i < keys.length; i++) {
        attribute = keys[i].split(':');
        if (attribute.length === 2) {
            svg.style[attribute[0].trim()] = attribute[1].trim();
        }
    }
}

function bBoxText(textContent, options) {
    var measureWindowElement = 'measureElement';
    window[measureWindowElement].style.visibility = 'visible';
    var svg = window[measureWindowElement].children[2];
    var text = getChildNode(svg)[1];
    text.textContent = textContent;
    applyStyleAgainstCsp(text, 'font-size:' + options.fontSize + 'px; font-family:'
        + options.fontFamily + ';font-weight:' + (options.bold ? 'bold' : 'normal'));
    var bBox = text.getBBox().width;
    window[measureWindowElement].style.visibility = 'hidden';
    return bBox;
}
window.wrapSvgText = function (text, textValue, laneWidth) {
    var childNodes = [];
    var k = 0;
    var txtValue;
    var bounds1;
    var content = textValue || text.content;
    if (text.whiteSpace !== 'nowrap' && text.whiteSpace !== 'pre') {
        if (text.breakWord === 'breakall') {
            txtValue = '';
            txtValue += content[0];
            for (k = 0; k < content.length; k++) {
                bounds1 = bBoxText(txtValue, text);
                if (bounds1 >= text.width && txtValue.length > 0) {
                    childNodes[childNodes.length] = { text: txtValue, x: 0, dy: 0, width: bounds1 };
                    txtValue = '';
                }
                else {
                    txtValue = txtValue + (content[k + 1] || '');
                    if (txtValue.indexOf('\n') > -1) {
                        childNodes[childNodes.length] = { text: txtValue, x: 0, dy: 0, width: bBoxText(txtValue, text) };
                        txtValue = '';
                    }
                    var width = bBoxText(txtValue, text);
                    if (Math.ceil(width) + 2 >= text.width && txtValue.length > 0) {
                        childNodes[childNodes.length] = { text: txtValue, x: 0, dy: 0, width: width };
                        txtValue = '';
                    }
                    if (k === content.length - 1 && txtValue.length > 0) {
                        childNodes[childNodes.length] = { text: txtValue, x: 0, dy: 0, width: width };
                        txtValue = '';
                    }
                }
            }
        }
        else {
            childNodes = wordWrapping(text, textValue, laneWidth);
        }
    }
    else {
        childNodes[childNodes.length] = { text: content, x: 0, dy: 0, width: bBoxText(content, text) };
    }
    return childNodes;
}
window.wordWrapping = function (text, textValue, laneWidth) {
    var childNodes = [];
    var txtValue = '';
    var j = 0;
    var i = 0;
    var wrap = text.whiteSpace !== 'nowrap' ? true : false;
    var content = textValue || text.content;
    if (content == undefined) {
        content = "";
    }
    var eachLine = content.split('\n');
    var txt;
    var words;
    var newText;
    var existingWidth;
    var existingText;
    for (j = 0; j < eachLine.length; j++) {
        txt = '';
        words = text.textWrapping !== 'NoWrap' ? eachLine[j].split(' ') : (text.textWrapping === 'NoWrap') ? [eachLine[j]] : eachLine;
        for (i = 0; i < words.length; i++) {
            txtValue += (((i !== 0 || words.length === 1) && wrap && txtValue.length > 0) ? ' ' : '') + words[i];
            newText = txtValue + ' ' + (words[i + 1] || '');
            var width = bBoxText(newText, text);
            if (Math.floor(width) > (laneWidth || text.width) - 2 && txtValue.length > 0) {
                childNodes[childNodes.length] = {
                    text: txtValue, x: 0, dy: 0,
                    width: newText === txtValue ? width : (txtValue === existingText) ? existingWidth : bBoxText(txtValue, text)
                };
                txtValue = '';
            }
            else {
                if (i === words.length - 1) {
                    childNodes[childNodes.length] = { text: txtValue, x: 0, dy: 0, width: width };
                    txtValue = '';
                }
            }
            existingText = newText;
            existingWidth = width;
        }
    }
    return childNodes;
}
window.wrapSvgTextAlign = function (text, childNodes) {
    var wrapBounds = { x: 0, width: 0 };
    var k = 0;
    var txtWidth;
    var width;
    for (k = 0; k < childNodes.length; k++) {
        txtWidth = childNodes[k].width;
        width = txtWidth;
        if (text.textAlign === 'left') {
            txtWidth = 0;
        }
        else if (text.textAlign === 'center') {
            if (txtWidth > text.width && (text.textOverflow === 'Ellipsis' || text.textOverflow === 'Clip')) {
                txtWidth = 0;
            }
            else {
                txtWidth = -txtWidth / 2;
            }
        }
        else if (text.textAlign === 'right') {
            txtWidth = -txtWidth
        }
        else {
            txtWidth = childNodes.length > 1 ? 0 : -txtWidth / 2;
        }
        childNodes[k].dy = text.fontSize * 1.2;
        childNodes[k].x = txtWidth;
        if (!wrapBounds) {
            wrapBounds = {
                x: txtWidth,
                width: width
            };
        }
        else {
            wrapBounds.x = Math.min(wrapBounds.x, txtWidth);
            wrapBounds.width = Math.max(wrapBounds.width, width);
        }
    }
    return wrapBounds;
}
window.overFlow = function (text, options) {
    var i = 0;
    var j = 0;
    var middle = 0;
    var bounds = 0;
    var temp = '';
    j = text.length;
    var t = 0;
    do {
        if (bounds > 0) {
            i = middle;
        }
        middle = Math.floor(middleElement(i, j));
        temp += text.substr(i, middle);
        bounds = bBoxText(temp, options);
    } while (bounds <= options.width);
    temp = temp.substr(0, i);
    for (t = i; t < j; t++) {
        temp += text[t];
        bounds = bBoxText(temp, options);
        if (bounds >= options.width) {
            text = text.substr(0, temp.length - 1);
            break;
        }
    }
    if (options.textOverflow === 'Ellipsis') {
        text = text.substr(0, text.length - 3);
        text += '...';
    }
    else {
        text = text.substr(0, text.length);
    }
    return text;
}
window.middleElement = function (i, j) {
    var m = 0;
    m = (i + j) / 2;
    return m;
}
window.getChildNode = function (node) {
    var child;
    var collection = [];
    //if (ej2_base_1.Browser.info.name === 'msie' || ej2_base_1.Browser.info.name === 'edge') {
    //  for (var i = 0; i < node.childNodes.length; i++) {
    //    child = node.childNodes[i];
    //    if (child.nodeType === 1) {
    //      collection.push(child);
    //  }
    //  }
    // }
    // else {
    collection = node.children;
    //}
    return collection;
}
function applyStyleAgainstCsp(svg, attributes) {
    var keys = attributes.split(';');
    for (var i = 0; i < keys.length; i++) {
        var attribute = keys[i].split(':');
        if (attribute.length === 2) {
            svg.style[attribute[0].trim()] = attribute[1].trim();
        }
    }
}
function whiteSpaceToString(value, wrap) {
    if (wrap === 'NoWrap' && value === 'PreserveAll') {
        return 'pre';
    }
    var state = '';
    switch (value) {
        case 'CollapseAll':
            state = 'nowrap';
            break;
        case 'CollapseSpace':
            state = 'pre-line';
            break;
        case 'PreserveAll':
            state = 'pre-wrap';
            break;
    }
    return state;
};
function wordBreakToString(value) {
    var state = '';
    switch (value) {
        case 'Wrap':
            state = 'breakall';
            break;
        case 'NoWrap':
            state = 'keepall';
            break;
        case 'WrapWithOverflow':
            state = 'normal';
            break;
        case 'LineThrough':
            state = 'line-through';
            break;
    }
    return state;
};
function textAlignToString(value) {
    var state = '';
    switch (value) {
        case 'Center':
            state = 'center';
            break;
        case 'Left':
            state = 'left';
            break;
        case 'Right':
            state = 'right';
            break;
    }
    return state;
};

window.eventStarted = false; window.inAction = false; window.eventInvokeValue = 0; window.isMouseWheel = false;
window.isZoomPan = false; window.scrollDiagramID = "";

window.onAddWireEvents = function (element, component, interval) {
    interval = 0;
    element.addEventListener('mousedown',
        _.throttle(e =>
        invokeDiagramEvents(e, component),
            0)
    );
    element.addEventListener('mousemove', _.throttle(e => 
        invokeDiagramEvents(e, component), interval));
    element.addEventListener('mouseup', _.throttle(e => 
        invokeDiagramEvents(e, component), 0));
    element.addEventListener('mouseleave', _.throttle(e => 
        invokeDiagramEvents(e, component), 0));

    element.addEventListener('scroll', _.throttle(e => invokeDiagramEvents(e, component), interval));
    
    element.addEventListener('mousewheel', _.throttle(e => invokeDiagramEvents(e, component), 0));
    element.addEventListener('keydown', _.throttle(e => invokeDiagramEvents(e, component), 0));
};

function invokeDiagramEvents(e, component) {
    var args = getMouseEvents(e);
    if ((e.type == "mousemove" && !window.eventStarted) || e.type != "mousemove" || !window.inAction) {
        if (e.type == "mousemove" && window.inAction && window.isZoomPan) {
            window.eventStarted = true;
            args.eventInvokeValue = ++window.eventInvokeValue;
        }
        if (e.type == "keydown" || e.type != "scroll" || (e.type == "scroll" && !isMouseWheel)) {
            component.invokeMethodAsync('InvokeDiagramEvents', args);
        }
    }
    if (e.type == "mouseup") {
        e.currentTarget.focus();
    }
}

function getMouseEvents(evt) {
    var mouseEventArgs = {};
    mouseEventArgs = {
        altKey: evt.altKey, shiftKey: evt.shiftKey, ctrlKey: evt.ctrlKey, detail: evt.detail,
        metaKey: evt.metaKey, screenX: evt.screenX, screenY: evt.screenY,
        clientX: evt.clientX, clientY: evt.clientY,
        offsetX: evt.offsetX, offsetY: evt.offsetY, type: evt.type, key: evt.key, keyCode: evt.keyCode
    }
    if (evt.currentTarget) {
        var bounds = measureScrollValues(evt.currentTarget.id);
        mouseEventArgs.diagramCanvasScrollBounds = bounds;
        mouseEventArgs.diagramGetBoundingClientRect = evt.currentTarget.getBoundingClientRect();
    } else if (evt.target) {
        var bounds = measureScrollValues(evt.target.id);
        mouseEventArgs.diagramCanvasScrollBounds = bounds;
        mouseEventArgs.diagramGetBoundingClientRect = evt.target.getBoundingClientRect();
    }

    if (evt.type == "mousewheel") {
        evt.preventDefault();
        evt.currentTarget.focus();
        mouseEventArgs.wheelDelta = evt.wheelDelta;
        isMouseWheel = true;
    }

    if (evt.type == "mousedown") {
        window.inAction = true;
    }
   
    if (evt.type == "mouseup") {
        window.inAction = false;
    }

    if (evt.type == "mouseleave" || evt.type == "mousmove" || evt.type == "mousedown" || evt.type == "mouseup" || evt.type == "keydown") {
        evt.preventDefault();
    }

    return mouseEventArgs;
}

window.onChangeScrollValues = function (element, left, top, eventValue) {
    if (element && left && top) {
        if (window.eventInvokeValue == eventValue) {
            window.eventStarted = false;
        }
        element.scrollLeft = left;
        element.scrollTop = top;
        return measureScrollValues(window.scrollDiagramID);
    }
    return null;
}

window.updateZoomPanTool = function (val) {
    window.isZoomPan = val;
}


window.updateInnerLayerSize = function (layerList, width, height, element, left, top, eventValue) {
    if (layerList != undefined && width != undefined && height != undefined && layerList.length > 0) {
        var layer;
		for (var i = 0; i < layerList.length; i++) {
            layer = document.getElementById(layerList[i]);
			if (layer) {
                layer.style.width = width;
                layer.style.height = height;
            }
        }
    }
    if (isMouseWheel) {
        isMouseWheel = false;
    }
   return onChangeScrollValues(element, left, top, eventValue);
}