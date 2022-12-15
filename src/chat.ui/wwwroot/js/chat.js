"use strict";
var roomId = window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1)
var connection = new signalR.HubConnectionBuilder().withUrl(`/chatHub?chatroomId=${roomId}`).build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message, dateTime, to) {
    var date = new Date(dateTime * 1000);

    var table = document.getElementById("messagesList");
    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);
    var cell = row.insertCell(0);  
    cell.innerHTML = `${date.mmddyyyyhhmmss()} - <strong>${user}</strong> says to <strong>${to}</strong> - ${message}`;
    var input = document.getElementById("messageInput");
    input.value = '';
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    connection.invoke("SendMessage", "", roomId, "", true, "").catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("messageInput").value;
    var to = document.querySelector('input[name="usersonline"]:checked').value
    connection.invoke("SendMessage", message, roomId,to,false,"").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

Date.prototype.mmddyyyyhhmmss = function () {
    var mm = this.getMonth() + 1; // getMonth() is zero-based
    var dd = this.getDate();
    var hours = this.getHours();
    var minutes = + this.getMinutes();    
    var seconds = this.getSeconds();

    var hour = hours12(hours)
    var amPm = "AM";
    if (hours > 11) { amPm = "PM"; }
    return [
        (mm > 9 ? '' : '0') + mm + '/',
        (dd > 9 ? '' : '0') + dd + '/',
        this.getFullYear() + ', ',
        hour + ':',
        (minutes > 9 ? '' : '0') + minutes + ':',
        (seconds > 9 ? '' : '0') + seconds + ' ',
        amPm
    ].join('');
};

function hours12(h) { return (h + 24) % 12 || 12; }