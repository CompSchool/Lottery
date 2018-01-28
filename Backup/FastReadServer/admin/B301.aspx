<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminM.Master" AutoEventWireup="true"
    Codebehind="B301.aspx.cs" Inherits="FastReadServer.admin.B301" %>

<asp:Content ID="Content1" ContentPlaceHolderID="rightContent" runat="server">
<style>
#divRoute {width:330px;}
#divRoute li {width:50px;height:50px;float:left;margin:2px;}
#divRoute li {background-color:#6fb3e0}
#divRoute .active {background-color:#fee188}
</style>
    <div class="breadcrumbs" id="breadcrumbs">

        <script type="text/javascript">
            try { ace.settings.check('breadcrumbs', 'fixed') } catch (e) { }
        </script>

        <ul class="breadcrumb">
            <li><i class="menu-icon fa fa-folder-open"></i><a href="#">视幅扩展训练</a> </li>
            <li class="active">修改/新建</li>
        </ul>
        <!-- /.breadcrumb -->
    </div>
    <div class="page-content">
        <div class="page-header">
            <h1>
                修改/新建
            </h1>
        </div>
        <!-- /.page-header -->
        <div class="row">
            <div class="col-xs-12 col-sm-8" id="pic">
                <div class="widget-box" style="">
                    <div class="widget-body">
                        <asp:Label ID="lbMsg" runat="server" CssClass="error"></asp:Label>
                        <div class="widget-main">
                            <div class="">
                                <label class="col-sm-2 control-label no-padding-right" for="form-field-1">
                                    阅读速度</label>
                                <asp:HiddenField runat="server" ID="hidNewsId" />
                                <asp:HiddenField runat="server" ID="hidDesc" />
                                <div class="col-sm-9">
                                    
                                <asp:DropDownList ID="ddlViewType" runat="server" Style="width: 160px">
                                <asp:ListItem Value="" Text="请选择"></asp:ListItem>
                                <asp:ListItem Value="1" Text="视点移动训练"></asp:ListItem>
                                <asp:ListItem Value="2" Text="视幅扩展训练"></asp:ListItem>
                                <asp:ListItem Value="3" Text="瞬间感知能力"></asp:ListItem>
                                </asp:DropDownList>
                                </div>
                            </div>
                            <hr />
                            <div class="">
                                <label class="col-sm-2 control-label no-padding-right" for="form-field-1">
                                    标题</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtTitle" runat="server" class="col-xs-10 col-sm-7"></asp:TextBox>
                                    <%--<input type="text" class="col-xs-10 col-sm-7" />--%>
                                </div>
                            </div>
                            <hr />
                            <div class="">
                                <label class="col-sm-2 control-label no-padding-right" for="form-field-1">
                                    封面</label>
                                <div class="col-sm-5">
                                    <asp:Image runat="server" ID="imgPhoto" style="max-height:300px"/>
                                </div>
                                <div class="col-sm-5">
                                    <asp:FileUpload runat="server" ID="fileImg" />
                                </div>
                            </div>
                            <hr />
                            <div class="">
                                <label class="col-sm-2 control-label no-padding-right" for="form-field-1">
                                    描述</label>
                                <div class="col-sm-9">
                                    <textarea name="txtDesc" runat="server" id="txtDesc" rows="5" cols="120" style="width: 550px;
                                        height: 150px;"></textarea>
                                </div>
                            </div>
                            <hr />
                            <div class="">
                                <label class="col-sm-2 control-label no-padding-right" for="form-field-1">
                                    内容</label>
                                <div class="col-sm-9">
                                    <textarea name="txtContent" runat="server" id="txtContent" rows="5" cols="120" style="width: 550px;
                                        height: 150px;"></textarea>
                                </div>
                            </div>
                            <hr />
                            <div class="">
                                <label class="col-sm-2 control-label no-padding-right" for="form-field-1">
                                    路径</label>
                                <div class="col-sm-9">
                                <asp:HiddenField runat="server" ID="hidRoute" /><asp:Label ID="lblRoute" runat="server"></asp:Label>
                                    <div >
                                   <ul id="divRoute"></ul>
                                   <ul><button onclick="return DoClearRoute()" class="btn btn-app btn-warning">
											<i class="ace-icon fa fa-undo btn-sm"></i>
											清除
										</button></ul>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="">
                                <asp:Button ID="btnSave" runat="server" Text="确定" class="btn btn-info" OnClientClick=""
                                    OnClick="btnSave_Click" />
                                &nbsp; &nbsp; &nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="取消" class="btn" OnClientClick=""
                                    OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.span -->
        </div>
    </div>

    <script>
       
       $("#<%=fileImg.ClientID%>").ace_file_input({
            style: 'well',
            no_icon: "ace-icon fa fa-picture-o",
            btn_choose: "选择封面图片",
            btn_change: null,
            droppable: false,
            onchange: null,
            thumbnail: "large", //| true | large
            allowExt: ["jpeg", "jpg", "png", "gif", "bmp"],
            allowMime: ["image/jpg", "image/jpeg", "image/png", "image/gif", "image/bmp"]
            //blacklist:'exe|php'
            //onchange:''
            //
        });
        
        
        function DoClearRoute(){
            $("#<%=hidRoute.ClientID %>").val("");
            iCurIdx = 0;
            $("#divRoute li").removeClass("active");
            $("#divRoute li").html("");
            return false;
        }
        var iCurIdx = 0;
        function InitRouteBlock(idxs){
            
            var i =0;
            iCurIdx = 0;
            sHtml = "";
            for(var iRow = 0;iRow<5;iRow++){
                for(var iCol = 0;iCol<6;iCol++){
                    i++;
                    
                    sHtml+="<li data-idx=\""+ i +"\" id=\"liIdx"+i+"\"></li>";
                }
            }
            $("#divRoute").html(sHtml);
            var arr = idxs.split(",");
            for(var j = 0;j<arr.length;j++){
                $("#liIdx"+arr[j]).html((j+1));
                $("#liIdx"+arr[j]).addClass("active");
            }
            $("#divRoute li").click(function(a,b){
                iCurIdx++;
                $(this).html(iCurIdx);
                $(this).addClass("active");
                
                if($("#<%=hidRoute.ClientID %>").val()==""){
                    $("#<%=hidRoute.ClientID %>").val($(this).data("idx"));
                }else{
                    $("#<%=hidRoute.ClientID %>").val($("#<%=hidRoute.ClientID %>").val()+","+$(this).data("idx"));
                }
            });
            
        }
        
        InitRouteBlock($("#<%=hidRoute.ClientID %>").val());
    </script>

</asp:Content>
