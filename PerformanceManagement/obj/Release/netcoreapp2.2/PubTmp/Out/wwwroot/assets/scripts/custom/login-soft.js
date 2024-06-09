var Login = function () {

    var handleLogin = function () {
        $('.login-form').validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            rules: {
                username: {
                    required: true
                },
                password: {
                    required: true
                },
                remember: {
                    required: false
                }
            },

            messages: {
                username: {
                    required: "وارد کردن نام کاربری الزامی می باشد."
                },
                password: {
                    required: "وارد کردن کلمه عبور الزامی می باشد."
                }
            },

            invalidHandler: function (event, validator) { //display error alert on form submit   
                $('.alert-danger', $('.login-form')).show();
            },

            highlight: function (element) { // hightlight error inputs
                $(element)
                    .closest('.form-group').addClass('has-error'); // set error class to the control group
            },

            success: function (label) {
                label.closest('.form-group').removeClass('has-error');
                label.remove();
            },

            errorPlacement: function (error, element) {
                error.insertAfter(element.closest('.input-icon'));
            },

            submitHandler: function (form) {
                form.submit();
                //window.location = "index.html";
            }
        });

        $('.login-form input').keypress(function (e) {
            if (e.which == 13) {
                if ($('.login-form').validate().form()) {
                    $('.login-form').submit();
                }
                return false;
            }
        });
    }

    var handleForgetPassword = function () {
        //$('.forget-form').validate({
        //    errorElement: 'span', //default input error message container
        //    errorClass: 'help-block', // default input error message class
        //    focusInvalid: false, // do not focus the last invalid input
        //    ignore: "",
        //    rules: {
        //        email: {
        //            required: true,
        //            email: true
        //        }
        //    },

        //    messages: {
        //        email: {
        //            required: "وارد کردن ایمیل اجباری می باشد.",
        //            email: "لطفا یک ایمیل آدرس معتبر وارد نمایید."
        //        }
        //    },

        //    invalidHandler: function (event, validator) { //display error alert on form submit   

        //    },

        //    highlight: function (element) { // hightlight error inputs
        //        $(element)
        //            .closest('.form-group').addClass('has-error'); // set error class to the control group
        //    },

        //    success: function (label) {
        //        label.closest('.form-group').removeClass('has-error');
        //        label.remove();
        //    },

        //    errorPlacement: function (error, element) {
        //        error.insertAfter(element.closest('.input-icon'));
        //    },

        //    submitHandler: function (form) {
        //        form.submit();
        //    }
        //});

        //$('.forget-form input').keypress(function (e) {
        //    if (e.which == 13) {
        //        if ($('.forget-form').validate().form()) {
        //            $('.forget-form').submit();
        //        }
        //        return false;
        //    }
        //});
        $("#forgetFormId").submit(function (e) {
            //alert('dpCreationSubmit');
            //var postdata2 = $(this).serializeArray();
            //debugger;
            var form = $('#forgetFormId');
            form.validate({
                //console.log($('#registerform').serializeArray());
                errorElement: 'span', //default input error message container
                errorClass: 'help-block', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                ignore: "",
                rules: {
                    nationalNumber: {
                        required: true,
                        number:true
                    },
                    employeeNumber: {
                        required: true,
                        number: true
                    }
                },
                messages: {
                    nationalNumber: {
                        required: "پر کردن این فیلد الزامی می باشد",
                        number:"لطفا عدد وارد نمایید"
                    },
                    employeeNumber: {
                        required: "پر کردن این فیلد الزامی می باشد",
                        number: "لطفا عدد وارد نمایید"
                    }
                },
                invalidHandler: function (event, validator) { //display error alert on form submit
                    //                    success2.hide();
                    //                    error2.show();
                    //                    App.scrollTo(error2, -200);
                },
                errorPlacement: function (error, element) { // render error placement for each input type
                    var icon = $(element).parent('.input-icon').children('i');
                    icon.removeClass('fa-check').addClass("fa-warning");
                    icon.attr("data-original-title", error.text()).tooltip();
                },
                highlight: function (element) { // hightlight error inputs
                    $(element)
                        .closest('.form-group').addClass('has-error'); // set error class to the control group
                },

                unhighlight: function (element) { // revert the change done by hightlight

                },

                success: function (label, element) {
                    var icon = $(element).parent('.input-icon').children('i');
                    $(element).closest('.form-group').removeClass('has-error').addClass('has-success'); // set success class to the control group
                    icon.removeClass("fa-warning").addClass("fa-check");
                },

                submitHandler: function (form) {
                    //                    success2.show();
                    //                    error2.hide();
                }
            })
            if (form.valid() == false) {
                //console.log($('#registerform').serializeArray());
                return false;
            } else {
                //$("#articleSubmit").addClass('disabled');
                $("#forgetFormBtn").attr("disabled", "disabled");
                var btn = $("#forgetFormBtn");
                btn.button('loading');
                var postdata = new FormData(this);
                //var postdata = $('#dpcreation').serializeArray();

                $.ajax(
                    {
                        //data:postdata,
                        url: "/employee/profile/RecoveryPassword",
                        //url : formURL,
                        //data: postdata2,
                        data: postdata,
                        //data: "firstName=" + $("fn").val(),
                        cache: false,
                        contentType: false,
                        processData: false,
                        type: "post",

                        success: function (data, textStatus, jqXHR) {
                            
                            //if ($.trim(data) === "yess")
                            //alert("that's one i wanted. excellent")
                            //alert(data);
                            var message = "";
                            if (data == 1) {
                                message += "<span>اطلاعات  مورد نظر با موفقیت ثبت گردید و پسورد بازیابی شده به ایمیل شما ارسال گردید.</span><br><br>";
                                toastr.options.timeout = "15000";
                                toastr.options.positionclass = "toast-top-center";
                                toastr.success(message);
                            }
                            else if (data == 0) {
                                message += "<span>اطلاعات  مورد نظر ثبت نگردیداز صحت اطلاعات ارسالی اطمینان حاصل فرمایید.</span><br><br>";
                                toastr.options.timeout = "15000";
                                toastr.options.positionclass = "toast-top-center";
                                toastr.info(message);
                            }
                            $("#forgetFormId")[0].reset();
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            alert("erorr00000");
                            console.log(jqXHR);
                            console.log(textStatus);
                            console.log(errorThrown);

                            alert(jqXHR);
                            alert(textStatus);
                        }
                    }).always(function () {
                        $("#forgetFormBtn").button('reset');
                    });
                e.preventDefault(e);
            }
        });
        jQuery('#forget-password').click(function () {
            jQuery('.login-form').hide();
            jQuery('.forget-form').show();
        });

        jQuery('#back-btn').click(function () {
            jQuery('.login-form').show();
            jQuery('.forget-form').hide();
        });

    }

    var handleRegister = function () {

        function format(state) {
            if (!state.id) return state.text; // optgroup
            return "<img class='flag' src='assets/img/flags/" + state.id.toLowerCase() + ".png'/>&nbsp;&nbsp;" + state.text;
        }


        $("#select2_sample4").select2({
            placeholder: '<i class="fa fa-map-marker"></i>&nbsp;Select a Country',
            allowClear: true,
            formatResult: format,
            formatSelection: format,
            escapeMarkup: function (m) {
                return m;
            }
        });


        $('#select2_sample4').change(function () {
            $('.register-form').validate().element($(this)); //revalidate the chosen dropdown value and show error or success message for the input
        });



        $('.register-form').validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "",
            rules: {

                fullname: {
                    required: true
                },
                email: {
                    required: true,
                    email: true
                },
                address: {
                    required: true
                },
                city: {
                    required: true
                },
                country: {
                    required: true
                },

                username: {
                    required: true
                },
                password: {
                    required: true
                },
                rpassword: {
                    equalTo: "#register_password"
                },

                tnc: {
                    required: true
                }
            },

            messages: { // custom messages for radio buttons and checkboxes
                tnc: {
                    required: "Please accept TNC first."
                }
            },

            invalidHandler: function (event, validator) { //display error alert on form submit   

            },

            highlight: function (element) { // hightlight error inputs
                $(element)
                    .closest('.form-group').addClass('has-error'); // set error class to the control group
            },

            success: function (label) {
                label.closest('.form-group').removeClass('has-error');
                label.remove();
            },

            errorPlacement: function (error, element) {
                if (element.attr("name") == "tnc") { // insert checkbox errors after the container                  
                    error.insertAfter($('#register_tnc_error'));
                } else if (element.closest('.input-icon').size() === 1) {
                    error.insertAfter(element.closest('.input-icon'));
                } else {
                    error.insertAfter(element);
                }
            },

            submitHandler: function (form) {
                form.submit();
            }
        });

        $('.register-form input').keypress(function (e) {
            if (e.which == 13) {
                if ($('.register-form').validate().form()) {
                    $('.register-form').submit();
                }
                return false;
            }
        });

        jQuery('#register-btn').click(function () {
            jQuery('.login-form').hide();
            jQuery('.register-form').show();
        });

        jQuery('#register-back-btn').click(function () {
            jQuery('.login-form').show();
            jQuery('.register-form').hide();
        });
    }

    return {
        //main function to initiate the module
        init: function () {

            handleLogin();
            handleForgetPassword();
            handleRegister();

            $.backstretch([
                "/assets/img/bg/5.jpg",
                "/assets/img/bg/1.jpg",
                "/assets/img/bg/2.jpg",
                "/assets/img/bg/3.jpg",
                "/assets/img/bg/4.jpg"
                
            ], {
                    fade: 1000,
                    duration: 3000
                });
        }

    };

}();