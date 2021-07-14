<%@ Page Title="" Language="C#" MasterPageFile="~/Base.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="WeExchange.HomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Homepage_style.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--轮播-->
    <div class="container">
      <div id="lunbo" class="wrap" style="left: -1000px;" runat="server">
        <img src="../images/lunbo_5.jpg" alt="5">
        <img src="../images/lunbo_1.jpg" alt="1">
        <img src="../images/lunbo_2.jpg" alt="2">
        <img src="../images/lunbo_3.jpg" alt="3">
        <img src="../images/lunbo_4.jpg" alt="4">
        <img src="../images/lunbo_5.jpg" alt="5">
        <img src="../images/lunbo_1.jpg" alt="1">
      </div>
      <div class="buttons">
        <span class="on"></span>
        <span></span>
        <span></span>
        <span></span>
        <span></span>
      </div>
      <a href="javascript:;" class="arrow arrow_left">&lt;</a>
      <a href="javascript:;" class="arrow arrow_right">&gt;</a>
    </div>


    <div style="margin-left: 180px; width: 1000px; height: 200px;">
      <img src="../images/maincategories.jpg">
    </div>

    <!--纵向排列主要种类-->
    <div class="Category_Info"
         style="margin-left: 15%; margin-right: 10%; display: flex; flex-direction: column; min-height: 100%;">


      <!--食品种类，图片左置-->
      <div id="Category_Food" style="margin: 50px; display: flex; flex-direction: row; background-color: white; ">

        <!--图片及链接-->
        <a href="/CategoryPage/CategoryPage.aspx?category_id=0" style="margin: 10px">
          <img src="../images/Food.jpg" width="300px" height="300px" style="border-radius: 0px;">
        </a>

        <div>
          <div style="margin-left: 40px;">
            <img src="../images/01.jpg" width="250px" height="100px">
          </div>

          <!--简介及链接-->
          <div style="margin-left:50px; margin-top: 20px; margin-bottom: 40px; width: 500px;">
            <p style="font-size: 18px; word-break:normal; color: rgb(100, 98, 98);">
              Love eating and surfing the Internet, then you will love WeExchange. Open WeExchange without hesitation, high-quality second-hand food give you provision. Thousands of tastes, gathered at our web site.
            </p>
          </div>

          <div style="top: 200px; margin-left: 30px; float: left;">
            <a href="/CategoryPage/CategoryPage.aspx?category_id=0" style="margin-left: 20px; text-decoration: none;">
              <button style="color:rgb(54, 53, 53); width: 65px; height: 40px; font-size: 20px; font-weight: normal; background: rgb(248, 235, 118); border-width: 0px; border-radius: 20%;"
                      onmouseover="this.className='more_hover'" onmouseout="this.className='more_move'">
                More
              </button>
            </a>
          </div>
        </div>
      </div>


      <!--电器种类，图片右置-->
      <div id="Category_Electronics" style="margin: 50px; display: flex; flex-direction: row; background-color: white;">

        <div>
          <div style="margin-left: 5px;">
            <img src="../images/02.jpg" width="400px" height="100px">
          </div>

          <!--简介及链接-->
          <div style="left: 20px; margin-left:20px; margin-top: 20px; margin-bottom: 40px; width: 500px;">
            <p style="font-size: 18px; word-break:normal; color: rgb(100, 98, 98);">
              Let second-hand electronics more favorable, cheap and convenient to buy at will. WeExchange second-hand electronic trading, the best choice without cheating. Tap your fingertips to meet high-quality electronics.
            </p>
          </div>

          <div style="left: 20px; float: left;">
            <a href="/CategoryPage/CategoryPage.aspx?category_id=1" style="margin-left: 20px; text-decoration: none; float: right;">
              <button style="color:rgb(54, 53, 53); width: 65px; height: 40px; font-size: 20px; font-weight: normal; background: rgb(250, 200, 242); border-width: 0px; border-radius: 20%;"
                      onmouseover="this.className='more_hover'" onmouseout="this.className='more_move'">
                More
              </button>
            </a>
          </div>
        </div>

        <!--图片及链接-->
        <a href="/CategoryPage/CategoryPage.aspx?category_id=1" style="margin-left: 20px; float: right;">
          <img src="../images/camera.jpg" width="300px" height="300px" style="border-radius: 0px;">
        </a>
      </div>


      <!--衣物种类，图片左置-->
      <div id="Category_Clothes" style="margin: 50px; display: flex; flex-direction: row; background-color: white;">

        <!--图片及链接-->
        <a href="/CategoryPage/CategoryPage.aspx?category_id=2" style="margin: 10px">
          <img src="../images/Clothes.jpg" width="300px" height="300px" style="border-radius: 0px;">
        </a>

        <div>
          <div style="margin-left: 40px;">
            <img src="../images/03.jpg" width="300px" height="100px">
          </div>

          <!--简介及链接-->
          <div style="margin-left:50px; margin-top: 20px; margin-bottom: 40px; width: 500px;">
            <p style="font-size: 18px; word-break:normal; color: rgb(100, 98, 98);">
              All kinds of clothing are available here. They are mostly clothes that have been worn once or twice by their owners. Buying clothes here can greatly reduce your expenses.
            </p>
          </div>

          <div style="top: 200px; margin-left: 30px; float: left;">
            <a href="/CategoryPage/CategoryPage.aspx?category_id=2" style="margin-left: 20px; text-decoration: none;">
              <button style="color:rgb(54, 53, 53); width: 65px; height: 40px; font-size: 20px; font-weight: normal; background: rgb(177, 218, 255); border-width: 0px; border-radius: 20%;"
                      onmouseover="this.className='more_hover'" onmouseout="this.className='more_move'">
                More
              </button>
            </a>
          </div>
        </div>
      </div>


      <!--化妆品种类，图片右置-->
      <div id="Category_Makeup" style="margin: 50px; display: flex; flex-direction: row; background-color: white;">

        <div>
          <div style="margin-left: 5px;">
            <img src="../images/04.jpg" width="300px" height="100px">
          </div>

          <!--简介及链接-->
          <div style="left: 20px; margin-left:20px; margin-top: 20px; margin-bottom: 40px; width: 500px;">
            <p style="font-size: 18px; word-break:normal; color: rgb(100, 98, 98);">
              When it comes to service, we'll take care of it for you. Whether you have a favorite brand or need help finding the perfect match, here's where you can find the brand you want.
            </p>
          </div>

          <div style="left: 20px; float: left;">
            <a href="/CategoryPage/CategoryPage.aspx?category_id=3" style="margin-left: 20px; text-decoration: none; float: right;">
              <button style="color:rgb(54, 53, 53); width: 65px; height: 40px; font-size: 20px; font-weight: normal; background: rgb(247, 238, 197); border-width: 0px; border-radius: 20%;"
                      onmouseover="this.className='more_hover'" onmouseout="this.className='more_move'">
                More
              </button>
            </a>
          </div>
        </div>

        <!--图片及链接-->
        <a href="/CategoryPage/CategoryPage.aspx?category_id=3" style="margin-left: 20px; float: right;">
          <img src="../images/Makeup.jpg" width="300px" height="300px" style="border-radius: 0px;">
        </a>
      </div>


      <!--文具种类，图片左置-->
      <div id="Category_Stationery" style="margin: 50px; display: flex; flex-direction: row; background-color: white;">

        <!--图片及链接-->
        <a href="/CategoryPage/CategoryPage.aspx?category_id=4" style="margin: 10px">
          <img src="../images/Stationery.jpg" width="300px" height="300px" style="border-radius: 0px;">
        </a>

        <div>
          <div style="margin-left: 40px;">
            <img src="../images/05.jpg" width="400px" height="100px">
          </div>

          <!--简介及链接-->
          <div style="margin-left:50px; margin-top: 20px; margin-bottom: 40px; width: 500px;">
            <p style="font-size: 18px; word-break:normal; color: rgb(100, 98, 98);">
              Indeed, you can purchase on our website small stationary, such as sticky notes, pen, eraser, scissors and pencil, but also all types of solutions to file your paper and keep your work environment clean, organized and tidy.
            </p>
          </div>

          <div style="top: 200px; margin-left: 30px; float: left;">
            <a href="/CategoryPage/CategoryPage.aspx?category_id=4" style="margin-left: 20px; text-decoration: none;">
              <button style="color:rgb(54, 53, 53); width: 65px; height: 40px; font-size: 20px; font-weight: normal; background: rgb(175, 221, 198); border-width: 0px; border-radius: 20%;"
                      onmouseover="this.className='more_hover'" onmouseout="this.className='more_move'">
                More
              </button>
            </a>
          </div>
        </div>
      </div>

    </div>
    <script src="Homepage.js"></script>
</asp:Content>
