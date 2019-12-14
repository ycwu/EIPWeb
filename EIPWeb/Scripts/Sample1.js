var userID = "";

$(function () {

    while (userID.length == 0) {
        userID = window.prompt("請輸入使用者名稱");
        if (!userID)
            userID = "";
    }
    $("#userName").append(userID).show();

    //建立與Server端的Hub的物件，注意Hub的開頭字母一定要為小寫
    var chat = $.connection.Sample1Hub;

    //建立連線後，我們接著來定義client端的function來讓Server端的hub呼叫。
    chat.client.hello = function (message) {
        $("#messageList").append("<li>" + message + "</li");
    }
    chat.client.sendAllMessage = function (message) {
        $("#messageList").append("<li>" + message + "</li");
        $("#message").val('');
    }
    chat.client.sendMessage = function (message) {
        $("#messageList").append("<li>" + message + "</li");
        $("#message").val('');
    }
    chat.client.addList = function (name) {
        //$("#chatList").children().remove().end();
        $("#chatList").append("<li>" + name + "</li>");
    }
    chat.client.addBox = function (id, name) {
        $("#box").append("<option value='" + id + "'>" + name+"</option>");
    }

    //將連線打開
    $.connection.hub.start().done(function () {
        //當連線完成後，呼叫Server端的hello方法，並傳送使用者姓名給Server
        chat.server.userConnected(userID);
    });

    var chatInput = $("#message");
    chatInput.keydown(function (e) {
        if (e.which === 13) {
            SendMessage();
        }
    });

    $("#send").click(function () {
        SendMessage();
    });

    function SendMessage() {
        //呼叫Server端的sendMessage方法，並傳送使用者姓名及訊息內容給Server
        if ($("#box option:selected").text() == "所有人")
            chat.server.sendAllMessage($("#message").val());
        else
            chat.server.sendMessage($("#box").val(), $("#message").val());
    }
});
