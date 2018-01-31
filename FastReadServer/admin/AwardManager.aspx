<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/admin/AdminM.Master" CodeBehind="AwardManager.aspx.cs" Inherits="FastReadServer.admin.AwardManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="rightContent" runat="server">
    <div class="breadcrumbs" id="breadcrumbs">

        <script type="text/javascript">
            try { ace.settings.check('breadcrumbs', 'fixed') } catch (e) { }
        </script>

        <ul class="breadcrumb">
            <li><i class="menu-icon fa fa-folder-open"></i><a href="#">奖品信息</a> </li>
            <li class="active">奖品</li>
        </ul>
        <!-- /.breadcrumb -->
    </div>
    <div class="page-content">
        <div class="page-header">
            <h1>
                奖品
            </h1>
        </div>
        <!-- /.page-header -->
        <div class="row">
            <!-- PAGE CONTENT BEGINS -->
            <div class="widget-body">
                <div class="widget-main">
                    <label for="form-field-select-1">
                        奖品</label>
                    <input type="text" id="txtSearchName" />
                  
                     <button class="btn btn-primary" onclick="return DoSearch(1);">
                        <i class="ace-icon fa fa-search align-top bigger-125"></i>查询
                    </button>
                    
                    <button class="btn btn-primary" onclick="return DoCreateVerCode();">
                        <i class="ace-icon fa fa-plus-circle align-top bigger-125"></i>新增
                    </button>
                </div>
            </div>
            <div class="col-xs-12 col-sm-12 widget-container-col">
                <div class="widget-box">
                    <div class="widget-body">
                        <div class="widget-main no-padding">
                            <table class="table table-striped table-bordered table-hover">
                                <thead class="thin-border-bottom">
                                    <tr>
                                        <th width="150px" class="center">
                                            奖品ID
                                        </th>
                                        <th width="150px" class="center">
                                            奖品名称
                                        </th>
                                        <th width="80px" class="center">
                                            状态
                                        </th>
                                        <th class="center">
                                            备注
                                        </th>
                                        <th width="120px" class="center">
                                            使用时间
                                        </th>
                                        <th  width="120px" class="hidden-480 center">
                                            操作</th>
                                    </tr>
                                </thead>
                                <tbody id="databody">
                                </tbody>
                            </table>
                            <div id="pager">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.span -->
        </div>
        <!-- /.row -->
        <!-- PAGE CONTENT ENDS -->
    </div>
    <div id="dlgDetail" style="display: none">
        <div class="text">
            <div class="title">
                <h4 id="newsTitle">
                    请填写备注内容</h4>
                <h5 id="newTimeAuthor">
                </h5>
            </div>
            <div id="newsContent" class="line-middle text-center">
                <textarea id="txtReply" cols="100" rows="12"></textarea>
            </div>
        </div>
    </div>
    <!-- /.page-content -->

    <script>
        var laypage = null;
        $(function () {

            layui.use('laypage', function () {
                laypage = layui.laypage;
                DoSearch(1);
            });


        });
        var curPage = "";
        function DoSearch(pg) {
            curPage = pg;
            //新闻
            window._IF.VERCODELLIST(this,
            {
                "vercode": $("#txtSearchName").val(),//新闻
                "page": pg, "all": 1,
                "limit": 12
            },
            fnNewsListCallBack,
            function () {
            });

            return false;
        }

        function ShowReply(qaid) {

            layer.open({
                type: 1,
                shade: false,
                title: false, //不显示标题
                content: $('#dlgDetail'), //捕获的元素，注意：最好该指定的元素要存放在body最外层，否则可能被其它的相对元素所影响
                area: ['40%', '30%'],
                btn: ['回复', '取消'],
                yes: function (index) {
                    if ($("*#txtReply:last").val() == "") {
                        layer.msg("请填写回复内容。");
                        return false;
                    }
                    //回复
                    window._IF.VERCODECHANGE(this,
                    {
                        "id": qaid,
                        "bak": $("*#txtReply:last").val(),//回复内容
                    },
                    function (r) {
                        if (r.ok) {
                            DoSearch(curPage);
                            layer.closeAll();
                            layer.msg("已修改备注");
                        } else {
                            layer.msg(r.msg);
                        }
                    },
                    function () {
                    });
                },
                cancel: function () {

                }
            });
        }

        function DoPass(qaid) {

            layer.open({
                type: 1,
                shade: false,
                title: '询问框', //不显示标题
                content: '&nbsp;&nbsp;确定要启用吗？',
                area: ['150', '60'],
                btn: ['通过', '取消'],
                yes: function (index) {

                    //回复
                    window._IF.VERCODECHANGE(this,
                    {
                        "id": qaid,
                        "state": "1",//回复内容
                    },
                    function (r) {
                        if (r.ok) {
                            DoSearch(curPage);
                            layer.closeAll();
                            layer.msg("已启用");
                        } else {
                            layer.msg(r.msg);
                        }
                    },
                    function () {
                    });
                },
                cancel: function () {

                }
            });
        }

        function DoStop(qaid) {

            layer.open({
                type: 1,
                shade: false,
                title: '询问框', //不显示标题
                content: '&nbsp;&nbsp;确定要停用吗？',
                area: ['150', '60'],
                btn: ['通过', '取消'],
                yes: function (index) {

                    //回复
                    window._IF.VERCODECHANGE(this,
                    {
                        "id": qaid,
                        "state": "2",//回复内容
                    },
                    function (r) {
                        if (r.ok) {
                            DoSearch(curPage);
                            layer.closeAll();
                            layer.msg("已停用");
                        } else {
                            layer.msg(r.msg);
                        }
                    },
                    function () {
                    });
                },
                cancel: function () {

                }
            });
        }

        function fnNewsListCallBack(r) {
            if (r.ok) {
                //删除原有的记录
                $("#databody").html("");

                var sHtml = "";
                for (var i = 0; i < r.lst.length; i++) {
                    var row = r.lst[i];
                    sHtml += "<tr>";
                    sHtml += "<td>" + row.code + "</td>";
                    sHtml += "<td>" + row.mac + "</td>";
                    sHtml += "<td>" + row.state_name + "</td>";
                    sHtml += "<td>" + row.bak + "</td>";
                    sHtml += "<td>" + row.use_time + "</td>";
                    sHtml += "<td>";
                    if (row.state == "1") {
                        sHtml += "<a class=\"ace-icon fa fa-no bigger-120 red\" href=\"javascript:DoStop(" + row.id + ")\">停用</a>";
                    } else {
                        sHtml += "<a class=\"ace-icon fa fa-check bigger-120 green\" href=\"javascript:DoPass(" + row.id + ")\">启用</a>";
                    }

                    sHtml += "<a class=\"ace-icon fa fa-flag bigger-120 blue\" href=\"javascript:ShowReply(" + row.id + ")\">修改备注</a>";

                    sHtml += "</td>";
                    sHtml += "</tr>";
                }

                $("#databody").html(sHtml);


                ResetPager(r.cnt, r.curpage);

            }
        }

        function ResetPager(cnts, curpage) {

            laypage({
                cont: 'pager'
                  , pages: Math.ceil(cnts / 12)
                  , groups: 6
                  , skin: '#6fb3e0'
                  , curr: curpage
                  , first: false
                  , last: false
                  , jump: function (obj, first) {
                      if (!first) {
                          DoSearch(obj.curr);
                      }
                  }
            });
        }


        function DoCreateVerCode() {

            layer.prompt({ title: '输入您要产生的授权码个数', formType: 0 }, function (pass, index) {
                layer.close(index);
                //回复
                window._IF.CREATEVERCODE(this,
                {
                    "num": pass
                },
                function (r) {
                    if (r.ok) {
                        DoSearch(curPage);
                        layer.closeAll();
                        layer.msg("已生成授权码");
                    } else {

                        layer.msg(r.msg);
                    }
                },
                function () {
                });


            });

            return false;
        }

    </script>

</asp:Content>
