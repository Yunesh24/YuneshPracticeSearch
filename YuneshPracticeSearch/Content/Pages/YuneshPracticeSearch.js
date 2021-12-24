$(document).ready(function () {
    $("#txtName").focus();

    $("#txtName").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/YuneshSearch/SearchCustomer",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term },
                success: function (data) {
                    response($.map(data, function (item) {

                        return { label: item.Name, value: item.Name, Id: item.Id, email: item.Email, address: item.Address,  };
                    }))
                }
            })
        },
        minLength: 2,
        select: function (event, ui) {
            $("#txtMobile").val(ui.item.mobile);
            $("#txtAddress").val(ui.item.address);
            $("#txtId").val(ui.item.Id);
            $("#txtEmail").val(ui.item.email);
        }

    });
})

var saveCustomer = function () {
    var Id = $("#Id").val();
    var name = $("#txtName").val();
    var email = $("#txtEmail").val();
    var address = $("#txtAddress").val();
    
    

    var model = {
        Id: Id,
        Name: name,
        Email: email,
        Address: address,
        
    };

    $.ajax({
        url: "/YuneshSearch/SaveCustomer",
        method: "Post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        datatype: "json",

        success: function (response) {
            alert("Successfull");

        }
    })
}
