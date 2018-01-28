<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminM.Master" AutoEventWireup="true"
    Codebehind="B200.aspx.cs" Inherits="FastReadServer.admin.B200" %>

<asp:Content ID="Content1" ContentPlaceHolderID="rightContent" runat="server">
    <style>
#databody img{max-height:200px}
</style>
    <div class="breadcrumbs" id="breadcrumbs">

        <script type="text/javascript">
            try { ace.settings.check('breadcrumbs', 'fixed') } catch (e) { }
        </script>

        <ul class="breadcrumb">
            <li><i class="menu-icon fa fa-folder-open"></i><a href="#">资源</a> </li>
            <li class="active">阅读测评</li>
        </ul>
        <!-- /.breadcrumb -->
    </div>
    <div class="page-content">
        <div class="page-header">
            <h1>
                文章
            </h1>
        </div>
        <!-- /.page-header -->
        <div class="row">
            <!-- PAGE CONTENT BEGINS -->
            <div class="widget-body">
                <div class="widget-main">
                    <label for="form-field-select-1">
                        阅读速度</label>
                    <asp:DropDownList ID="ddlSpead" runat="server" Style="width: 160px">
                    </asp:DropDownList>
                    <label for="form-field-select-1">
                        标题</label>
                    <input type="text" id="txtSearchTitle" />
                    <a class="btn btn-info btn-sm" href="javascript:DoSearch(1)">查询</a> &nbsp; &nbsp;
                    <asp:Button ID="btnAdd" runat="server" Text="新建" class="btn btn-info btn-sm" OnClick="btnAdd_Click" />
                    <%--<input type="text"/>&nbsp; &nbsp; 
					<button type="button" class="btn btn-info btn-sm">
				<i class="ace-icon fa fa-search bigger-110"></i> 查询
							</button>--%>
                </div>
            </div>
            <div class="col-xs-12 col-sm-10 widget-container-col">
                <div class="widget-box">
                    <div class="widget-body">
                        <div class="widget-main no-padding">
                            <table class="table table-striped table-bordered table-hover">
                                <thead class="thin-border-bottom">
                                    <tr>
                                        <th>
                                            阅读速度
                                        </th>
                                        <th>
                                            标题
                                        </th>
                                        <th>
                                            封面
                                        </th>
                                        <th>
                                            字数
                                        </th>
                                        <th class="hidden-480">
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
    <div style="display: none" id="dlgQuesDetail">
        <div class="row">
            <div id="divQuesDetail" class="col-xs-12">
                <!-- PAGE CONTENT BEGINS -->
                <form class="form-horizontal" role="form">
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="form-field-1">
                            题目
                        </label>
                        <div class="col-sm-9">
                            <input type="hidden" id="hidQuesId" />
                            <input type="text" id="txtQuesTitle" placeholder="题目" class="col-xs-10 col-sm-5">
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="form-field-1-1">
                            答案1
                        </label>
                        <div class="col-sm-9">
                            <input type="text" id="txtOp1" placeholder="答案1" class="form-control" /><label class="middle">
												<input class="ace" type="radio" name="rdoAnswer" id="rdo1">
												<span class="lbl">正确答案</span>
											</label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="form-field-1-1">
                            答案2
                        </label>
                        <div class="col-sm-9">
                            <input type="text" id="txtOp2" placeholder="答案2" class="form-control" /><label class="middle">
												<input class="ace" type="radio" name="rdoAnswer" id="rdo2">
												<span class="lbl">正确答案</span>
											</label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="form-field-1-1">
                            答案3
                        </label>
                        <div class="col-sm-9">
                            <input type="text" id="txtOp3" placeholder="答案3" class="form-control" /><label class="middle">
												<input class="ace" type="radio" name="rdoAnswer" id="rdo3">
												<span class="lbl">正确答案</span>
											</label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label no-padding-right" for="form-field-1-1">
                            答案4
                        </label>
                        <div class="col-sm-9">
                            <input type="text" id="txtOp4" placeholder="答案4" class="form-control" /><label class="middle">
												<input class="ace" type="radio" name="rdoAnswer" id="rdo4">
												<span class="lbl">正确答案</span>
											</label>
                        </div>
                    </div>
                    <div class="space-4">
                    </div>
                   
            </div>
        </div>
    </div>
    <div style="display: none" id="dlgQuestions">
        <div class="row">
            <div class="col-xs-12">
                <table id="sample-table-1" class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th class="center">
                                <button onclick="return DoEditQues();" class="btn btn-xs btn-success">
                                    <i class="ace-icon fa fa-plus bigger-120"></i>
                                </button>
                            </th>
                            <th>
                                题目</th>
                            <th>
                                答案1</th>
                            <th>
                                答案2</th>
                            <th>
                                答案3</th>
                            <th>
                                答案4</th>
                            <th>
                                操作</th>
                        </tr>
                    </thead>
                    <tbody id="quesList">
                        <tr>
                            <td class="center">
                                1
                            </td>
                            <td>
                                以下哪比喻没有出现在文中。
                            </td>
                            <td>
                                一幅美丽的水墨画</td>
                            <td>
                                一首美妙的交响曲</td>
                            <td>
                                珍珠撒在翡翠上</td>
                            <td>
                                <span class="label label-sm label-warning">天上的棉花糖</span>
                            </td>
                            <td>
                                <div class="hidden-sm hidden-xs btn-group">
                                    <button class="btn btn-xs btn-info">
                                        <i class="ace-icon fa fa-pencil bigger-120"></i>
                                    </button>
                                    <button class="btn btn-xs btn-danger">
                                        <i class="ace-icon fa fa-trash-o bigger-120"></i>
                                    </button>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <!-- /.span -->
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

        function DoSearch(pg) {
            var catId = $("#<%= ddlSpead.ClientID%>").val();
            
                //新闻
                window._IF.TRAINSLIST(this,
                {
                    "tp":"2",//新闻 
                    "speed": catId,
                    "key": $("#txtSearchTitle").val(),
                    "page": pg,
                    "limit": 12
                },
                fnNewsListCallBack,
                function () {
                });


        }


        function fnNewsListCallBack(r) {
            if (r.ok) {
                //删除原有的记录
                $("#databody").html("");

                var sHtml = "";
                if(r.cnt>0){
                    for (var i = 0; i < r.lst.length; i++) {
                        var row = r.lst[i];
                        sHtml += "<tr>";
                        sHtml += "<td>" + row.speedname + "</td>";
                        sHtml += "<td>" + row.title + "</td>";
                        sHtml += "<td><img src=\"../upload/test/" + row.photo + "\"/></td>";
                        sHtml += "<td>" + row.words + "</td>";
                        sHtml += "<td><a  class=\"btn btn-xs btn-success\" href=\"B201.aspx?id=" + row.train_id + "\">编辑</a><a class=\"btn btn-xs btn-info\"  href=\"javascript:DoDeleteNews(" + row.id + ")\">删除</a>";
                        
                        sHtml += "<a class=\"blue\"  href=\"javascript:ShowTestQuestion(" + row.train_id + ")\"><i class=\"ace-icon fa fa-search-plus bigger-130\"></i>题目</a>";
                      
                        sHtml += "</td></tr>";
                    }
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

        function ShowTestQuestion(testid){
        
            curTrainId=testid;
        //新闻
                window._IF.QUESLIST(this,
                {
                    "id":testid,"qt":2
                },
                function (r) {
                    if(r.ok){
                        var sHtml ="";
                        if(r.cnt>0){
                            for (var i = 0; i < r.lst.length; i++) {
                                var row = r.lst[i];
                                sHtml += "<tr>";
                                sHtml += "<td>" + row.q_id + "</td>";
                                sHtml += "<td>" + row.title + "</td>";
                                sHtml += "<td>" + row.op1 + "</td>";
                                sHtml += "<td>" + row.op2 + "</td>";
                                sHtml += "<td>" + row.op3 + "</td>";
                                sHtml += "<td>" + row.op4 + "</td>";
                                sHtml += "<td><div class=\"hidden-sm hidden-xs btn-group\">";
                                
                                sHtml += " <button onclick=\"return DoEditQues("+row.q_id+")\" class=\"btn btn-xs btn-info\">";
                                sHtml += "        <i class=\"ace-icon fa fa-pencil bigger-120\"></i>";
                                sHtml += "    </button>";
                                sHtml += "    <button onclick=\"return DoRemoveQues("+row.q_id+")\"  class=\"btn btn-xs btn-danger\">";
                                sHtml += "        <i class=\"ace-icon fa fa-trash-o bigger-120\"></i>";
                                sHtml += "    </button>";
                                sHtml += "</div></td></tr>";
                            }
                        }
                        $("#quesList").html(sHtml);
                    
                        layer.open({
                          type: 1,
                          skin: 'layui-layer-rim', //加上边框
                          area: ['80%', '80%'], //宽高
                          content: $("#dlgQuestions")
                        });
                        
                    }else{
                        layer.msg(r.msg);
                    }
                },
                function () {
                });
         
                        
                return false;
             
        }
        
        var curTrainId=0;
        function RefreshQuesList(){
             window._IF.QUESLIST(this,
                {
                    "id":curTrainId,"qt":2
                },
                function (r) {
                    if(r.ok){
                        var sHtml ="";
                        if(r.cnt>0){
                            for (var i = 0; i < r.lst.length; i++) {
                                var row = r.lst[i];
                                sHtml += "<tr>";
                                sHtml += "<td>" + row.q_id + "</td>";
                                sHtml += "<td>" + row.title + "</td>";
                                sHtml += "<td>" + row.op1 + "</td>";
                                sHtml += "<td>" + row.op2 + "</td>";
                                sHtml += "<td>" + row.op3 + "</td>";
                                sHtml += "<td>" + row.op4 + "</td>";
                                sHtml += "<td><div class=\"hidden-sm hidden-xs btn-group\">";
                                
                                sHtml += " <button onclick=\"return DoEditQues("+row.q_id+")\" class=\"btn btn-xs btn-info\">";
                                sHtml += "        <i class=\"ace-icon fa fa-pencil bigger-120\"></i>";
                                sHtml += "    </button>";
                                sHtml += "    <button onclick=\"return DoRemoveQues("+row.q_id+")\"  class=\"btn btn-xs btn-danger\">";
                                sHtml += "        <i class=\"ace-icon fa fa-trash-o bigger-120\"></i>";
                                sHtml += "    </button>";
                                sHtml += "</div></td></tr>";
                            }
                        }
                        $("#quesList:last").html(sHtml);
                    
                       
                        
                    }else{
                        layer.msg(r.msg);
                    }
                },
                function () {
                });
        }
        
        function getRightIdx(){
            var idx=0;
            $("*#divQuesDetail:last").find("input:radio").each(function(a,b){
                if($(b).is(":checked")){
                    idx =a +1;
                }
            });
            return idx;
        }
        
        function CheckQuesInput(){
            var hidId = $("*#hidQuesId:last").val();
            var txtTitle = $("*#txtQuesTitle:last").val();
            var txtOp1 = $("*#txtOp1:last").val();
            var txtOp2 = $("*#txtOp2:last").val();
            var txtOp3 = $("*#txtOp3:last").val();
            var txtOp4 = $("*#txtOp4:last").val();
            
            if(txtTitle==""){
                layer.msg("请输入题目！");
                return false;
            }
            if(txtOp1==""){
                layer.msg("请输入答案1！");
                return false;
            }
            if(txtOp2==""){
                layer.msg("请输入答案2！");
                return false;
            }
            if(txtOp3==""){
                layer.msg("请输入答案3！");
                return false;
            }
            if(txtOp4==""){
                layer.msg("请输入答案4！");
                return false;
            }
            if(getRightIdx()==0){
                
                layer.msg("请指定正确答案！");
                return false;
            }
            return true;
        }
        
        function QuesDetailClear(){
            $("#txtQuesTitle").val("");
            $("#hidQuesId").val("");
            $("#txtOp1").val("");
            $("#rdo1").removeAttr("checked");
            $("#txtOp2").val("");
            $("#rdo2").removeAttr("checked");
            $("#txtOp3").val("");
            $("#rdo3").removeAttr("checked");
            $("#txtOp4").val("");
            $("#rdo4").removeAttr("checked");
        }
        
        function DoEditQues(qid){
            if(qid==undefined)
            {
                QuesDetailClear();
                
                 layer.open({
                          type: 1,
                          skin: 'layui-layer-rim', //加上边框
                          area: ['60%', '60%'], //宽高，
                          content: $("#dlgQuesDetail"),
                          btn:['确定','取消'],
                          yes:function(index){
                            if(CheckQuesInput()){
                                 window._IF.CREATEQUES(this,
                                {
                                    "id":curTrainId
                                   ,"qt":"2"
                                   ,"title":$("*#txtQuesTitle:last").val()
                                   ,"op1":$("*#txtOp1:last").val()
                                   ,"op2":$("*#txtOp2:last").val()
                                   ,"op3":$("*#txtOp3:last").val()
                                   ,"op4":$("*#txtOp4:last").val()
                                   ,"right":getRightIdx()
                                },
                                function (r) {
                                    if(r.ok){
                                        layer.close(index);
                                        layer.msg("添加题目成功！");
                                        RefreshQuesList();
                                       
                                        
                                    }else{
                                        layer.msg(r.msg);
                                    }
                                },
                                function () {
                                });
                                
                                layer.close(index);
                            }
                          },
                          no:function(index){
                            layer.close(index);
                          }
                        });
            } else{
                window._IF.QUESDETAIL(this,
                {
                    "qid":qid
                },
                function (r) {
                    if(r.ok){
                        QuesDetailClear()
                    var row = r.data;
                     $("#txtQuesTitle").val(row.title);
                    $("#hidQuesId").val(qid);
                    $("#txtOp1").val(row.op1);
                    $("#txtOp2").val(row.op2);
                    $("#txtOp3").val(row.op3);
                    $("#txtOp4").val(row.op4);
                    $("#rdo"+row.answer).attr("checked",true);
                     $("#rdo"+row.answer).click();
                 layer.open({
                          type: 1,
                          skin: 'layui-layer-rim', //加上边框
                          area: ['60%', '60%'], //宽高，
                          content: $("#dlgQuesDetail"),
                          btn:['确定','取消'],
                          yes:function(index){
                            if(CheckQuesInput()){
                                 window._IF.UPDATEEQUES(this,
                                {
                                    "qid":qid
                                   ,"title":$("*#txtQuesTitle:last").val()
                                   ,"op1":$("*#txtOp1:last").val()
                                   ,"op2":$("*#txtOp2:last").val()
                                   ,"op3":$("*#txtOp3:last").val()
                                   ,"op4":$("*#txtOp4:last").val()
                                   ,"right":getRightIdx()
                                },
                                function (r) {
                                    if(r.ok){
                                        layer.close(index);
                                        layer.msg("更新题目成功！");
                                        RefreshQuesList();
                                       
                                        
                                    }else{
                                        layer.msg(r.msg);
                                    }
                                },
                                function () {
                                });
                                
                                
                            }
                          },
                          no:function(index){
                            layer.close(index);
                          }
                        });
                        
                    }else{
                        layer.msg(r.msg);
                    }
                },
                function () {
                });
            
            }
        
             
                        
            return false;
             
        }
        
        
        function DoRemoveQues(qid){
            //询问框
            layer.confirm('确定要删除此问题码？', {
              btn: ['删除','取消'] //按钮
            }, function(index){

                 window._IF.REMOVEQUES(this,
                {
                    "qid":qid
                },
                function (r) {
                    if(r.ok){
                        layer.close(index);
                        layer.msg("删除题目成功！");
                        RefreshQuesList();
                       
                        
                    }else{
                        layer.msg(r.msg);
                    }
                },
                function () {
                });
            }, function(){
              
            });
            return false;
        }

    </script>

</asp:Content>
