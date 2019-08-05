
if (window.Operation == undefined) {
    window.Operation = {
        data: {},
        init: function () {
        },
    
        SendData: function (form, AddressUrl) {
            for (var i = 0; i < form.elements.length; i++) {
                if (form.elements[i].nodeName === "INPUT") {
                    Operation.data[form.elements[i].name] = form.elements[i].value
                }
            }
            $.ajax({
               // method:"post",
                type:"POST",
                url: AddressUrl,
                data: Operation.data,
                success: function () {
                    alert(45)
                },
            });
        },

            //$.post(ChangePassword.URL + "?ACT=AddPassword", data, function (data) {
            //    if (data.error == false) {
            //        setTimeout(function () { $(".btn-blue").focus() }, 10);
            //        $.alert({
            //            title: 'تایید',
            //            icon: 'fa fa-thumbs-o-up',
            //            content: data.message,
            //            rtl: true,
            //            theme: 'light',
            //            buttons: {
            //                confirm: {
            //                    text: 'تایید',
            //                    btnClass: 'btn-blue',
            //                    action: function () {

            //                    }
            //                },

            //            }
            //        });
            //        //$.pgwModal({ content: data.data[1].status.message });

            //    } else {
            //        setTimeout(function () { $(".btn-blue").focus() }, 10);
            //        $.alert({
            //            title: 'هشدار',
            //            icon: 'fa fa-warning',
            //            content: data.message,
            //            rtl: true,
            //            theme: 'light',
            //            buttons: {
            //                confirm: {
            //                    text: 'تایید',
            //                    btnClass: 'btn-danger',
            //                    action: function () {

            //                    }
            //                },

            //            }
            //        });
            //        //$.pgwModal({ content: data.data[1].status.message });
            //    }
            //}, 'json');


    }

}