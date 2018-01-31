/* Com.js  */

var _Config = {
    Server: "http://localhost:14913/api.ashx",
    Time_out: 10 * 1000,
    PageSize: 20,
    Uid: "",
    Reload: 5 * 1000
};

(function () {
    if (!window["_P_M_"]) window["_P_M_"] = {};

    window._IF = {};

    window._IF.getJson = function (scope, url, param, sCallback, fCallBack, accessType, showMask, fsm, fem, onErrNoop) {
        var oldurl = url;
        var path = null;
        path = url;
        url = _Config.Server + url;

        console.log(url);

        accessType = accessType ? accessType : "GET";

        param = param ? param : {};
      

        var rtn;
        var t = scope ? scope : window;

        param["sigVer"] = "1";

        $.ajax({
            data: param,
            url: url,
            cache: false,
            dataType: "jsonp",
            jsonp: "callback",
            success: function (r) {
               
                if (t && (t._status_ === -1 || t._status_ === 1)) return;
                sCallback && sCallback.call(t, r);
            },
            error: function () {
                this._Alert("程序出错,请联系管理员.");
            }
        });
    };
    
    
    //修改密码
    window._IF.CHANGPWD = function (scope, param, cb, error) {
        !param && (param = {});
        var url = "?act=changePwd";
        this.getJson(scope, url, param, cb, error, "GET");
    };
    
    //获取训练资料列表
    window._IF.TRAINSLIST = function (scope, param, cb, error) {
        !param && (param = {});
        var url = "?act=loadTrain";
        this.getJson(scope, url, param, cb, error, "GET");
    };
    
    //授权一览
    window._IF.VERCODELLIST = function (scope, param, cb, error) {
        !param && (param = {});
        var url = "?act=loadVercode";
        this.getJson(scope, url, param, cb, error, "GET");
    };
    
    
    //授权更新一览
    window._IF.VERCODEUPDATELIST = function (scope, param, cb, error) {
        !param && (param = {});
        var url = "?act=loadUpdateLog";
        this.getJson(scope, url, param, cb, error, "GET");
    };
    
    //新建授权
    window._IF.CREATEVERCODE = function (scope, param, cb, error) {
        !param && (param = {});
        var url = "?act=createVercode";
        this.getJson(scope, url, param, cb, error, "GET");
    };
    //授权备注
    window._IF.VERCODECHANGE = function (scope, param, cb, error) {
        !param && (param = {});
        var url = "?act=vercodeBak";
        this.getJson(scope, url, param, cb, error, "GET");
    };
    
    //删除动物图片
    window._IF.REMOVEIMG = function (scope, param, cb, error) {
        !param && (param = {});
        var url = "?act=removeAnima";
        this.getJson(scope, url, param, cb, error, "GET");
    };
    //问题一览
    window._IF.QUESLIST = function (scope, param, cb, error) {
        !param && (param = {});
        var url = "?act=loadQues";
        this.getJson(scope, url, param, cb, error, "GET");
    };
    
    //新增问题
    window._IF.CREATEQUES = function (scope, param, cb, error) {
        !param && (param = {});
        var url = "?act=addQues";
        this.getJson(scope, url, param, cb, error, "GET");
    };
    //删除问题
    window._IF.REMOVEQUES = function (scope, param, cb, error) {
        !param && (param = {});
        var url = "?act=removeQues";
        this.getJson(scope, url, param, cb, error, "GET");
    };
    //修改问题
    window._IF.UPDATEEQUES = function (scope, param, cb, error) {
        !param && (param = {});
        var url = "?act=updateQues";
        this.getJson(scope, url, param, cb, error, "GET");
    };
    //问题详情
    window._IF.QUESDETAIL = function (scope, param, cb, error) {
        !param && (param = {});
        var url = "?act=detailQues";
        this.getJson(scope, url, param, cb, error, "GET");
    };
    //视幅一览
    window._IF.VIEWLIST = function (scope, param, cb, error) {
        !param && (param = {});
        var url = "?act=loadView";
        this.getJson(scope, url, param, cb, error, "GET");
    };
    
    //删除视幅扩展训练
    window._IF.REMOVEVIEW = function (scope, param, cb, error) {
        !param && (param = {});
        var url = "?act=removeView";
        this.getJson(scope, url, param, cb, error, "GET");
    };

    //得到抽奖用户信息
    window._IF.GetAwardUsers = function (scope, param, cb, error) {
        !param && (param = {});
        var url = "?act=GetAwardUsers";
        this.getJson(scope, url, param, cb, error, "GET");
    };

    

}).call(window);




function ShowMessage(s) {
    layer.alert(s, {
        skin: 'layui-layer-lan'
    , closeBtn: 0
    , anim: 4 //动画类型
    });
}

function  htmlDecodeByRegExp(str){  
    var s = "";
    if(str.length == 0) return "";
    s = str.replace(/&amp;/g,"&");
    s = s.replace(/&lt;/g,"<");
    s = s.replace(/&gt;/g,">");
    s = s.replace(/&nbsp;/g," ");
    s = s.replace(/&#39;/g,"\'");
    s = s.replace(/&quot;/g,"\"");
    return s;  
}


function htmlencode(s) {
    var div = document.createElement('div');
    div.appendChild(document.createTextNode(s));
    return div.innerHTML;
}

function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
    var r = window.location.search.substr(1).match(reg);  //匹配目标参数
    if (r != null) return unescape(r[2]); return null; //返回参数值
}
