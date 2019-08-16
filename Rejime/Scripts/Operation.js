
if (window.Operation == undefined) {
    window.Operation = {
       // data: {},
        init: function () {
        },
        beforeSend: function () {
            $('.Main_Div').preloader({
                text: 'لطفا منتظر بمانید',
            });
        },
        complete: function () {
            $('.Main_Div').preloader('remove')
        },
        OnSuccess: function (data) {
            if (data.error == false) {
                setTimeout(function () {
                    $(".btn-blue").focus()
                }, 10);
                //======== Alert  ===========
                $.alert({
                    title: 'تایید',
                    icon: 'fa fa-thumbs-o-up',
                    content: data.message,
                    rtl: true,
                    theme: 'light',
                    buttons: {
                        confirm: {
                            text: 'تایید',
                            btnClass: 'btn-blue',
                            action: function () {
                            }
                        },

                    }
                });
                //======== Alert  ===========
            }
        },
        OnFailure: function (xhr, status) {

            alert('Error: ' + xhr.statusText);
        },
        SendData: function (FormName, AddressUrl) {
            //for (var i = 0; i < form.elements.length; i++) {
            //    if (form.elements[i].nodeName === "INPUT") {
            //        Operation.data[form.elements[i].name] = form.elements[i].value
            //        alert(Operation.data[form.elements[i].name])
            //    }
            //}
            event.preventDefault();
            if ($("#" + FormName).valid()) {
                Operation.beforeSend();
                var data = $("#" + FormName).serialize();
                $.ajax({
                    //type: "POST",
                    method:"POST",
                    url: AddressUrl,
                    //dataType:"json",
                    contenttype: "application/json",
                    data: data,
           
                    success: function (msg) {
                        setTimeout(function () {
                            alert(55)
                           Operation.complete();
                            $(".btn-blue").focus()
                        }, 10);
                            //======== Alert  ===========
                            $.alert({
                                title: 'تایید',
                                icon: 'fa fa-thumbs-o-up',
                                content: msg,
                                rtl: true,
                                theme: 'light',
                                buttons: {
                                    confirm: {
                                        text: 'تایید',
                                        btnClass: 'btn-blue',
                                        action: function () {
                                        }
                                    },

                                }
                            });
                            //======== Alert  ===========
                        

                    },

                });
            }
        
        },


    }

}