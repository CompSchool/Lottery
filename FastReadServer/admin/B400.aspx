<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminM.Master" AutoEventWireup="true" CodeBehind="B400.aspx.cs" Inherits="FastReadServer.admin.B400" %>
<asp:Content ID="Content1" ContentPlaceHolderID="rightContent" runat="server">
    <style>
        #imgList img {width:200px;
        }
    </style>
    <div class="page-content">
					

					<div class="page-header">
						<h1>
							图标
						</h1>
					</div><!-- /.page-header -->

					<div class="row">

                       
						<div class="col-xs-12">

							<!-- PAGE CONTENT BEGINS -->
							<div class="col-xs-6 col-md-3">
								<asp:FileUpload runat="server" ID="fileImg" />
								&nbsp; &nbsp; 
							<asp:Button runat="server" id="btnUpload" Text="上传" class="btn btn-info btn-sm" OnClick="btnUpload_Click">
				
							</asp:Button>
									</div>

                            <div class="col-xs-6 col-md-12">
                                <ul id="imgList" class="ace-thumbnails clearfix">
                                <asp:Repeater runat="server" ID="rptData">

                                    <ItemTemplate>

                                        <li>
										<a href='../upload/anima/<%#Eval("file_name") %>' title="" data-rel="colorbox">
											<img alt="" src='../upload/anima/<%#Eval("file_name") %>' />
										</a>
										<div class="tools tools-bottom clear" onclick='doDeleteImg(this,<%#Eval("img_id") %>)' >
											<a href="#">
												<i class="ace-icon fa fa-times red"></i>
											</a>
										</div>
									</li>
                                    </ItemTemplate>

                                </asp:Repeater>
								
									
							    </ul>
								</div>
							<!-- PAGE CONTENT ENDS -->
						</div><!-- /.col -->
					</div><!-- /.row -->
				</div><!-- /.page-content -->


    <script>
        $("#<%=fileImg.ClientID%>").ace_file_input({
            style: 'well',
            no_icon: "ace-icon fa fa-picture-o",
            btn_choose: "选择图片",
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

        function doDeleteImg(obj,imgid) {

            layer.open({
                type: 1,
                shade: false,
                title: '询问框', //不显示标题
                content: '&nbsp;&nbsp;确定要删除吗？',
                area: ['150', '60'],
                btn: ['删除', '取消'],
                yes: function (index) {
                    //回复
                    window._IF.REMOVEIMG(this,
                    {
                        "id": imgid
                    },
                    function (r) {
                        if (r.ok) {
                            $(obj).parent().remove();
                            layer.closeAll();
                            layer.msg("已删除");
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


    </script>
</asp:Content>
