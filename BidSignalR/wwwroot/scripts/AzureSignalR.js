//Add script to update the page and send messages.

document.addEventListener('DOMContentLoaded', function () {
    function createMessageEntry(msgRes) {

        //Start
        let accordionItemList = document.getElementsByClassName("accordion-item");
        let newIdNumber = accordionItemList.length + 1;

        let gapBetweenTwoDiv = document.createElement("div");
        gapBetweenTwoDiv.classList.add("mb-3");
        gapBetweenTwoDiv.setAttribute("id", "idMb3" + newIdNumber);

        let mainDiv = document.createElement("div");
        mainDiv.classList.add("accordion-item");
        mainDiv.setAttribute("id", "idAccordionItem" + newIdNumber);

        let h2 = document.createElement("h2");
        h2.classList.add("accordion-header");
        h2.setAttribute("id", "flush-heading" + newIdNumber);

        let collapseExpand = document.createElement("button");
        collapseExpand.classList.add("accordion-button");
        collapseExpand.setAttribute("type", "button");
        collapseExpand.setAttribute("id", "idButton" + newIdNumber);
        collapseExpand.setAttribute("data-bs-toggle", "collapse");
        collapseExpand.setAttribute("data-bs-target", "#flush-collapse" + newIdNumber);
        collapseExpand.setAttribute("aria-expanded", "true");
        collapseExpand.setAttribute("aria-controls", "flush-collapse" + newIdNumber);

        let actualDiv = document.createElement("div");
        actualDiv.classList.add("accordion-collapse", "collapse", "show");
        actualDiv.setAttribute("id", "flush-collapse" + newIdNumber);
        actualDiv.setAttribute("aria-labelledby", "flush-heading" + newIdNumber);

        let textDiv = document.createElement("div");
        textDiv.classList.add("accordion-body");
        textDiv.setAttribute("id", "idText" + newIdNumber);

        document.getElementById("accordionPanelsStayOpenExample").appendChild(gapBetweenTwoDiv);
        document.getElementById("idMb3" + newIdNumber).appendChild(mainDiv);
        document.getElementById("idAccordionItem" + newIdNumber).appendChild(h2);
        document.getElementById("idAccordionItem" + newIdNumber).appendChild(actualDiv);
        document.getElementById("flush-collapse" + newIdNumber).appendChild(textDiv);
        document.getElementById("flush-heading" + newIdNumber).appendChild(collapseExpand);

        document.getElementById("idButton" + newIdNumber).innerHTML = "Message - " + newIdNumber;
        document.getElementById("idText" + newIdNumber).innerHTML = msgRes;
        //End
    }

    function bindConnectionMessage(connectionScopedVariable) {
        var messageCallback = function (message) {
            createMessageEntry(message);
        };
        connectionScopedVariable.on('ReceiveMessage', messageCallback);
        connectionScopedVariable.onclose(onConnectionError);
    }

    function onConnected(connectionScopedVariable) {
        console.log('connection started', connectionScopedVariable);
    }

    async function onConnectionError(error) {
        if (error && error.message) {
            document.getElementById("error").innerHTML = error.message;
        } else {
            document.getElementById("error").innerHTML = "disconnected";
        }
    }

    const connection = new signalR.HubConnectionBuilder()
        .withUrl("https://atgsignalr.azurewebsites.net/chat")
        .configureLogging(signalR.LogLevel.Information)
        .withAutomaticReconnect({
            nextRetryDelayInMilliseconds: retryContext => {
                if (retryContext.elapsedMilliseconds < 60000) {
                    // If we've been reconnecting for less than 60 seconds so far,
                    // wait between 0 and 10 seconds before the next reconnect attempt.
                    return Math.random() * 10000;
                } else {
                    // If we've been reconnecting for more than 60 seconds so far, stop reconnecting.
                    return null;
                }
            }
        })
        .build()

    bindConnectionMessage(connection);

    async function start() {
        connection.start()
            .then(() => onConnected(connection))
            .then(res => {
                connection.invoke("AddToGroup", group)
                    .catch(err => {
                        console.log(err);
                    });
            })
            .catch(error => {
                console.error('errormsg', (error != undefined ? error.message : ""));
                // setTimeout(start, 5000)
            });
    }

    start();
});

function collapseAllDivisions() {
    let accordionItemList = document.getElementsByClassName("accordion-item");

    if (accordionItemList.length == 0) {
        alert("There is no any message in Auction Update section to collapse.");
    } else {
        for (var i = 1; i <= accordionItemList.length; i++) {
            let buttonTag = document.getElementById("idButton" + i);
            let flushCollapseDivTag = document.getElementById("flush-collapse" + i);

            buttonTag.classList.add("accordion-button", "collapsed");
            buttonTag.setAttribute("aria-expanded", "true");

            flushCollapseDivTag.classList.remove("show");
        }
    }
}

function removeAllDivisions() {
    let accordionItemList = document.getElementsByClassName("accordion-item");

    if (accordionItemList.length == 0) {
        alert("There is no any message in Auction Update section to remove.");
    } else {
        document.querySelectorAll('.accordion-item').forEach(function (a) {
            a.remove()
        })
    }
}

function addRow() {
    let rows = $("#idIncrementTable").find("tbody>tr").length + 1;

    let lastRow = $('table tr:last').attr("id");

    console.log(lastRow);

    $('<tr id="idRow_' + rows + '"><td>' +
        '<input type="text" class="form-control text-box single-line" id="idLow_' + rows + '" />' +
        '</td>' +
        '<td>' +
        '<input type="text" class="form-control text-box single-line" id="idHigh_' + rows + '" />' +
        '</td>' +
        '<td>' +
        '<input type="text" class="form-control text-box single-line" id="idIncrementValue_' + rows + '" />' +
        '</td>' +
        '<td>' +
        '<button type="button" id="idDeleteButton_' + rows + '" class="btn btn-danger" onclick="deleteRow(' + rows + ');"><i class="fa fa-trash"></i></button>' +
        '</td>' +
        '</tr>').appendTo("#idIncrementTable");
}



function deleteRow(index) {
    let rows = $("#idIncrementTable").find("tbody>tr").length;

    if (rows > 1) {
        for (var i = index; i < rows; i++) {
            var element = document.getElementById("idRow_" + (i + 1));
            element.id = "idRow_" + i;

            let deleteButton = document.getElementById('idDeleteButton_' + (i + 1));

            deleteButton.removeAttribute("onclick");
            deleteButton.setAttribute("onclick", "deleteRow(" + i + ")");

            deleteButton.id = "idDeleteButton_" + i;
        }

        $('#idRow_' + index).remove();
    }

    return false;
}