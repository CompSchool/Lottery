<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminM.Master" AutoEventWireup="true"
    Codebehind="B201.aspx.cs" Inherits="FastReadServer.admin.B201" %>

<asp:Content ID="Content1" ContentPlaceHolderID="rightContent" runat="server">
    <div class="breadcrumbs" id="breadcrumbs">

        <script type="text/javascript">
            try { ace.settings.check('breadcrumbs', 'fixed') } catch (e) { }
        </script>

        <ul class="breadcrumb">
            <li><i class="menu-icon fa fa-folder-open"></i><a href="#">阅读测评</a> </li>
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
                                    <asp:DropDownList ID="ddlSpead" runat="server" Style="width: 160px">
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
                                    内容</label>
                                <div class="col-sm-9">
                                    <textarea name="txtDesc" runat="server" id="newDesc" rows="5" cols="120" style="width: 550px;
                                        height: 400px;"></textarea>
                                </div>
                            </div>
                            <hr />
                            <div class="">
                                <label class="col-sm-2 control-label no-padding-right" for="form-field-1">
                                    字数</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtWordsCnt" runat="server" class="col-xs-10 col-sm-7"></asp:TextBox>
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
    </script>

</asp:Content>
