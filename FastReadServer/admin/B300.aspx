<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminM.Master" AutoEventWireup="true"
    Codebehind="B300.aspx.cs" Inherits="FastReadServer.admin.B300" %>

<asp:Content ID="Content1" ContentPlaceHolderID="rightContent" runat="server">
    <style>
#databody img{max-height:200px}
</style>
    <div class="breadcrumbs" id="breadcrumbs">

        <script type="text/javascript">
            try { ace.settings.check('breadcrumbs', 'fixed') } catch (e) { }
        </script>

        <ul class="breadcrumb">
            <li><i class="menu-icon fa fa-folder-open"></i><a href="#">��Դ</a> </li>
            <li class="active">�ӷ���չѵ��</li>
        </ul>
        <!-- /.breadcrumb -->
    </div>
    <div class="page-content">
        <div class="page-header">
            <h1>
                �ӷ���չѵ��
            </h1>
        </div>
        <!-- /.page-header -->
        <div class="row">
            <!-- PAGE CONTENT BEGINS -->
            <div class="widget-body">
                <div class="widget-main">
                    <label for="form-field-select-1">
                        ѵ������</label>
                    <asp:DropDownList ID="ddlViewType" runat="server" Style="width: 160px">
                    <asp:ListItem Value="" Text="��ѡ��"></asp:ListItem>
                    <asp:ListItem Value="1" Text="�ӵ��ƶ�ѵ��"></asp:ListItem>
                    <asp:ListItem Value="2" Text="�ӷ���չѵ��"></asp:ListItem>
                    <asp:ListItem Value="3" Text="˲���֪����"></asp:ListItem>
                    </asp:DropDownList>
                    <label for="form-field-select-1">
                        ����</label>
                    <input type="text" id="txtSearchTitle" />
                    <a class="btn btn-info btn-sm" href="javascript:DoSearch(1)">��ѯ</a> &nbsp; &nbsp;
                    <asp:Button ID="btnAdd" runat="server" Text="�½�" class="btn btn-info btn-sm" OnClick="btnAdd_Click" />
                    <%--<input type="text"/>&nbsp; &nbsp; 
					<button type="button" class="btn btn-info btn-sm">
				<i class="ace-icon fa fa-search bigger-110"></i> ��ѯ
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
                                            ����
                                        </th>
                                        <th>
                                            ����
                                        </th>
                                        <th class="hidden-480">
                                            ����</th>
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

        function DoSearch(pg) {
            var catId = $("#<%= ddlViewType.ClientID%>").val();
            
                //�Ӹ���չѵ���б�
                window._IF.VIEWLIST(this,
                {
                    "vt": catId,
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
                //ɾ��ԭ�еļ�¼
                $("#databody").html("");

                var sHtml = "";
                if(r.cnt>0){
                    for (var i = 0; i < r.lst.length; i++) {
                        var row = r.lst[i];
                        sHtml += "<tr>";
                        sHtml += "<td>" + row.vt_name + "</td>";
                        sHtml += "<td>" + row.title + "</td>";
                        sHtml += "<td><a  class=\"btn btn-xs btn-success\" href=\"B301.aspx?id=" + row.viewtrain_id + "\">�༭</a><a class=\"btn btn-xs btn-info\"  href=\"javascript:DoRemoveQues(" + row.viewtrain_id + ")\">ɾ��</a>";
                        
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

        
        
        function DoRemoveQues(qid){
            //ѯ�ʿ�
            layer.confirm('ȷ��Ҫɾ����ѵ���룿', {
              btn: ['ɾ��','ȡ��'] //��ť
            }, function(index){

                 window._IF.REMOVEVIEW(this,
                {
                    "vid":qid
                },
                function (r) {
                    if(r.ok){
                        layer.close(index);
                        layer.msg("ɾ���ɹ���");
                        DoSearch(1);
                       
                        
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
