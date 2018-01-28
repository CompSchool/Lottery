<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminM.Master" AutoEventWireup="true" CodeBehind="B100.aspx.cs" Inherits="FastReadServer.admin.B100" %>

<asp:Content ID="Content1" ContentPlaceHolderID="rightContent" runat="server">

<style>
#databody img{max-height:200px}
</style>
    <div class="breadcrumbs" id="breadcrumbs">
        <script type="text/javascript">
            try { ace.settings.check('breadcrumbs', 'fixed') } catch (e) { }
        </script>

        <ul class="breadcrumb">
            <li>
                <i class="menu-icon fa fa-folder-open"></i>
                <a href="#">资源</a>
            </li>
            <li class="active">课堂训练</li> 
        </ul>
        <!-- /.breadcrumb -->
    </div>

    <div class="page-content">


        <div class="page-header">
            <h1>文章
            </h1>
        </div>
        <!-- /.page-header -->
        <div class="row">

            <!-- PAGE CONTENT BEGINS -->
            <div class="widget-body">
                <div class="widget-main">
                   
                        <label for="form-field-select-1">阅读速度</label>
                        <asp:DropDownList ID="ddlSpead" runat="server" Style="width: 160px"></asp:DropDownList>
                               <label for="form-field-select-1">标题</label>
                               <input type="text" id="txtSearchTitle" />
                                    <a  class="btn btn-info btn-sm" href="javascript:DoSearch(1)" >查询</a>
                        &nbsp; &nbsp; 
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
                                        <th>阅读速度
                                        </th>
                                        <th>标题
                                        </th>
                                        <th>封面
                                        </th>
                                        <th>字数
                                        </th>
                                        <th class="hidden-480">操作</th>
                                    </tr>
                                </thead>

                                <tbody id="databody">

                                    
                                </tbody>

                                
                            </table>

                            <div id="pager"></div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.span -->

        </div>
        <!-- /.row -->
        <!-- PAGE CONTENT ENDS -->
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
                    "tp":"1",//新闻 
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
                for (var i = 0; i < r.lst.length; i++) {
                    var row = r.lst[i];
                    sHtml += "<tr>";
                    sHtml += "<td>" + row.speedname + "</td>";
                    sHtml += "<td>" + row.title + "</td>";
                    sHtml += "<td><img src=\"../upload/train/" + row.photo + "\"/></td>";
                    sHtml += "<td>" + row.words + "</td>";
                    sHtml += "<td><a  class=\"btn btn-xs btn-success\" href=\"B101.aspx?id=" + row.train_id + "\">编辑</a><a class=\"btn btn-xs btn-info\"  href=\"javascript:DoDeleteNews(" + row.id + ")\">删除</a></td>";
                  
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


    </script>
</asp:Content>
