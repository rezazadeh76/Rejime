
if (window.Operation == undefined) {
    window.Operation = {
       // data: {},
        init: function () {
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

                var data = $("#" + FormName).serialize();
                $.ajax({
                    type: "POST",
                    url: AddressUrl,
                    contenttype: "application/json",
                    data: data,
                    success: function (msg) {

                            setTimeout(function () { $(".btn-blue").focus() }, 10);
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
                                       //     $('#exampleModal').modal('hide');

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