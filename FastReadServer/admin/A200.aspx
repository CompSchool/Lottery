<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminM.Master" AutoEventWireup="true"
    Codebehind="A200.aspx.cs" Inherits="FastReadServer.admin.A200" %>

<asp:Content ID="Content1" ContentPlaceHolderID="rightContent" runat="server">
    <div class="breadcrumbs" id="breadcrumbs">

        <script type="text/javascript">
            try { ace.settings.check('breadcrumbs', 'fixed') } catch (e) { }
        </script>

        <ul class="breadcrumb">
            <li><i class="menu-icon fa fa-folder-open"></i><a href="#">��Ȩ��Ϣ</a> </li>
            <li class="active">��Ȩ����һ��</li>
        </ul>
        <!-- /.breadcrumb -->
    </div>
    <div class="page-content">
        <div class="page-header">
            <h1>
                ����һ��
            </h1>
        </div>
        <!-- /.page-header -->
        <div class="row">
            <!-- PAGE CONTENT BEGINS -->
            <div class="widget-body">
                <div class="widget-main">
                    <label for="form-field-select-1">
                        ��Ȩ��</label>
                    <input type="text" id="txtSearchName" />
                  
                     <button class="btn btn-primary" onclick="return DoSearch(1);">
                        <i class="ace-icon fa fa-search align-top bigger-125"></i>��ѯ
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
                                        <th width="300px" class="center">
                                            ��Ȩ��
                                        </th>
                                        <th width="240px" class="center">
                                            ����ʱ��
                                        </th>
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
                //����
                window._IF.VERCODEUPDATELIST(this,
                {
                    "vercode": $("#txtSearchName").val(),//����
                    "page": pg, 
                    "limit": 12
                },
                fnNewsListCallBack,
                function () {
                });
                
               return false;
        }

       
        function fnNewsListCallBack(r) {
            if (r.ok) {
                //ɾ��ԭ�еļ�¼
                $("#databody").html("");

                var sHtml = "";
                for (var i = 0; i < r.lst.length; i++) {
                    var row = r.lst[i];
                    sHtml += "<tr>";
                    sHtml += "<td>" + row.code + "</td>";
                    sHtml += "<td>" + row.last_time + "</td>";
                   
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
