<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PostPage.aspx.cs" Inherits="WeExchange.PostPage.PostPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Fill Basic Information</title>
    <link rel="stylesheet" href="post_style.css" type="text/css"/>
    <script src="https://kit.fontawesome.com/a076d05399.js"></script>
</head>

<body>

    <div class="container">
            <header>Basic Information</header>
            <div class="progress-bar" runat="server">
                <div class="step">
                    <p>Item</p>
                    <div class="bullet">
                        <span>1</span>
                    </div>
                    <div class="check fas fa-check"></div>
                </div>

                <div class="step">
                    <p>Contact</p>
                    <div class="bullet">
                        <span>2</span>
                    </div>
                    <div class="check fas fa-check"></div>
                </div>

                <div class="step">
                    <p>Confirm</p>
                    <div class="bullet">
                        <span>3</span>
                    </div>
                    <div class="check fas fa-check"></div>
                </div>
            </div>

            <div class="form-outer">
                <form id="infoForm" runat="server" method="post" enctype="multipart/form-data">
                    <div class="page slidepage">
                        <div class="title">Item Information</div>
                        <div class="field">
                            <div class="label">Item name
                                <div style="color: red; display: inline-block;">*</div>
                            </div>
                            <asp:TextBox runat="server" type="text" id="itemname" name="name" placeholder="No more than 20 words" required="required" />
                        </div>

                        <div class="field">
                            <div class="label">Category
                                <div style="color: red; display: inline-block;">*</div>
                            </div>
                            <select id="itemcategory" runat="server" required="required" name="category">
                                <option value="Food">Food</option>
                                <option value="Electronics">Electronics</option>
                                <option value="Clothes">Clothes</option>
                                <option value="Makeup">Makeup</option>
                                <option value="Stationery">Stationery</option>
                            </select>
                        </div>

                        <div class="field">
                            <div class="label">Degree
                                <div style="color: red; display: inline-block;">*</div>
                            </div>
                            <select id="itemtag" runat="server" required="required" name="tag">
                                <option value="Totally new">Totally new</option>
                                <option value="80% or 90% new">80% or 90% new</option>
                                <option value="Others">Others</option>
                            </select>
                        </div>

                        <div class="field">
                            <div class="label">Description
                                <div style="color: red; display: inline-block;">*</div>
                            </div>
                            <asp:Textbox runat="server" required="required" id="itemdescription" name="description" placeholder="No more than 100 words"></asp:Textbox>
                        </div>

                        <div class="field">
                            <div class="label" style="margin-top: 50px;">Images
                                <div style="color: red; display: inline-block;">*</div>
                            </div>
                            <asp:FileUpload runat="server" onchange="change();" value="点击上传图片"  id="itemimage" multiple="multiple" name="itemimage" style="margin-top: 50px; width: 100%; padding-top: 5px;"/>
                            <asp:FileUpload runat="server" onchange="change2();" value="点击上传图片"  id="itemimage2" multiple="multiple" name="itemimage2" style="margin-top: 50px; width: 100%; padding-top: 5px;"/>
                            <asp:FileUpload runat="server" onchange="change3();" value="点击上传图片"  id="itemimage3" multiple="multiple" name="itemimage3" style="margin-top: 50px; width: 100%; padding-top: 5px;"/>
                            <asp:FileUpload runat="server" onchange="change4();" value="点击上传图片"  id="itemimage4" multiple="multiple" name="itemimage4" style="margin-top: 50px; width: 100%; padding-top: 5px;"/>
                        </div>

                        <div class="field">
                            <div class="label" style="margin-top: 50px;">Price
                                <div style="color: red; display: inline-block;">*</div>
                            </div>
                            <asp:Textbox runat="server" type="text" style="margin-top: 50px; width: 25%;" id="itemprice" name="price" required="required" min="0" />
                        </div>

                        <div class="field nextBtn">
                            <asp:Button runat="server" class="nextBtn next" type="button" Text="Next" />
                        </div>
                    </div>

                    <div class="page">
                        <div class="title">Contact Information</div>
                        <div class="field">
                            <div class="label">Email
                                <div style="color: red; display: inline-block;">*</div>
                            </div>
                            <asp:Textbox runat="server" type="text" id="email" required="required" />
                        </div>

                        <div class="field">
                            <div class="label">Phone number
                                <div style="color: red; display: inline-block;">*</div>
                            </div>
                            <asp:Textbox runat="server" type="text" id="phoneNo" required="required" />
                        </div>

                        <div class="field btns">
                            <asp:Button runat="server" class="prev-1 prev" type="button" Text="Previous" />
                            <asp:Button runat="server" class="nextSec next" type="button" Text="Next" />
                        </div>
                    </div>

                    <div class="page">
                        <div class="title">Item Info</div>
                        <div class="confirmpart">
                            <div class="confirm">Name</div>
                            <div runat="server" class="answer" id="iName" name="iName">None</div>
                        </div>

                        <div class="confirmpart">
                            <div class="confirm">Category</div>
                            <div runat="server" class="answer" id="iCategory" name="iCategory">None</div>
                        </div>

                        <div class="confirmpart">
                            <div class="confirm">Degree</div>
                            <div runat="server" class="answer" id="iTag" name="iTag">None</div>
                        </div>

                        <div class="confirmpart">
                            <div class="confirm">Description</div>
                            <div runat="server" class="answer" id="iDescription" name="iDescription">None</div>
                        </div>

                        <div class="confirmpart">
                            <div class="confirm">Images</div>
                            <div runat="server" class="answer" id="iImage" name="iImage">None</div>
                        </div>

                        <div class="confirmpart">
                            <div class="confirm">Price</div>
                            <div runat="server" class="answer" id="iPrice" name="iPrice">None</div>
                        </div>

                        <div class="title">Contact Info</div>
                        <div class="confirmpart">
                            <div class="confirm">Email</div>
                            <div runat="server" class="answer" id="uEmail">None</div>
                        </div>

                        <div class="confirmpart">
                            <div class="confirm">Phone Number</div>
                            <div runat="server" class="answer" id="uPhoneNo">None</div>
                        </div>

                        <div class="field btns">
                            <asp:Button runat="server" class="prev-2 prev" type="button" Text="Previous" />
                            <asp:Button runat="server" type="button" class="submit next" Text="Submit" OnClick="Post_Item" />
                        </div>
                    </div>
                </form>
            </div>
        </div>
        <script>
            function submit() {
                document.getElementById("infoForm").submit();
            }
        </script>
        <script src="post_control.js"></script>
</body>
</html>