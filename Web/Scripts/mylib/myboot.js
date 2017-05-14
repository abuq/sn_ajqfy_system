
Array.prototype.toObject = function () {
    var obj = {};
    for (var i = 0; i < this.length; i++) {
        var key = this[i]["name"];
        var value = this[i]["value"];
        obj[key] = value;
    }
    return obj;
}

//对bootstarp的封装

function myBootstarp() {

    //创建Modal
    function creatModal(url) {
       var element=$('<div/ class="modal fade"  tabindex="-1" role="dialog" aria-hidden="true" id="modalDialog">').
               modal({ remote: url });
       // //当模态框对用户可见时触发（将等待 CSS 过渡效果完成
       // //绑定确认按钮事件
       //$(element).on("shown.bs.modal", function () {
       //    debugger
       //    //给确认按钮绑定事件
       //    $(element).find('#ok').on("click", function () {
       //        if (callback) {
       //            callback();
       //            // if (res) $(element).modal('hide');
       //        }
       //    });
       //});

       //绑定隐藏时移除元素
       $(element).on("hidden.bs.modal", function () {
           $(element).remove();
       });
       return $(element);
    }

    //关闭弹出框
    function hideModal() {
        setTimeout(function () {
            $('#modalDialog').modal('hide');
        },1000);
    }
    
        /*
     * ajax的post请求的封装（form）.
     * @author [汤台]
     * @version 1.0.0
     * @param   url,data,callback,er_callback,async
     * @return {void}
     */
    function Ajax(url, data, callback, er_callback, async) {
        $.ajax({
            url: url,
            data: data,
            type: 'POST',
            dataType: 'json',
            async: (async == null) ? true : async,
            success: function (r) {
                if (!r.over) { /*判断登录是否过期*/
                    if (r.success) {
                        callback(r);
                    }
                    else {
                        if (er_callback) {
                            er_callback(r);//手动提示错误
                        }
                        else {
                            alert(r.info);
                            //f.alert("操作失败,请联系管理员!");
                        }
                    }
                }
                else {

                    //f.alert("登录过期,请重新登录");
                    //location.href = "/Admin/Home/Tip";
                }
            },
            // jqXHR 是经过jQuery封装的XMLHttpRequest对象
            // textStatus 可能为null、 'timeout（超时）'、 'error（错误）'、 'abort(中止)'和'parsererror（解析错误)'等
            // errorMsg 是错误信息字符串(响应状态的文本描述部分，例如'Not Found'或'Internal Server Error')
            error: function (jqXHR, textStatus, errorMsg) {
                switch (jqXHR.status) {
                    case 404: alert('链接地址错误!', null, 'error'); break;
                    case 500: alert('服务器内部错误!', null, 'error'); break;
                    default:  alert(jqXHR.status + ":" + jqXHR.statusText, null, 'error');
                }
            }
        });
    }

    //提示信息展示
    function alert(tip) {
        if ($('.mytip').length > 0) {
            $('.mytip span').text(tip);
        }
        else {
            var html = [];
            html.push('<div class="alert alert-warning mytip">');
            html.push('<a href="#" class="close" data-dismiss="alert">&times;</a>');
            html.push('<strong>提示！</strong><span>' + tip + '</span></div>');
            $('.modal-body').append(html.join(''));
        }
    }


    return {
        creatModal: creatModal,
        Ajax: Ajax,
        hideModal: hideModal,
        alert: alert
        
    }
}