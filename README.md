Họ tên: Nguyễn Thị Thanh Hiền
MSV: 2221050770
<<<<<<< HEAD
# Bài thực hành số 3
=======
>>>>>>> 1dbd2ee44ff1b30470c45f3c1bbfc3994af01883
Cấu trúc thư mục của dự án .Net MVC
    1. Controllers
        - Chứa các Controller - xử lý yêu cầu từ người dùng
        - Nhận yêu cầu, gọi Model xử lý dữ liệu, trả kết quả về View
        - Mỗi controller tương ứng với mỗi nhóm chức năng
        vd: HomeController.cs
        -> Khi người dùng truy cập URL, hệ thống sẽ tìm Controller phù hợp để xử lý
    2. Models
        - Chứa các class mô tả dữ liệu và nghiệp vụ
        - Gồm: 
            *+ Entily (đối tượng ánh xạ bảng CSDL)
            *+ ViewModel (dữ liệu hiển thị cho View)
        - Giups Controller và View trao đổi dữ liệu
        vd: Product.cs
    3. Views
        - Chứa các file giao diện .cshtml
        - Mỗi Controller có 1 thư mục View cùng tên
        - View hiển thị dữ liệu cho người dùng
        - Cấu trúc:
            Views
                ├── Home
                │    ├── Index.cshtml
                │    └── About.cshtml
                └── Shared (chứa các View dùng chung)
                      ├── _Layout.cshtml (giao diện khung chung)
                      └── Error.cshtml (trang lỗi)
    4. wwwroot (ASP.NET Core MVC)
        - Chứa tài nguyên tĩnh: CSS, JavaScript, Images, Bootrap,...
        - các file trong thư mục có thể truy cập trực tiếp từ trình duyệt
    5. Data
        - Chứa DbContext để kết nối CSDL
        - Quản lý Entily Framework và thao tác với database
        vd: ApplicationDbContext.cs
    6. Migrations
        - Lưu lịch sử thay đổi CSDL
        - Được tạo khi sử dụng Entity Framework Code First
        - Giúp cập nhật database theo phiên bản
    7. Properties
        - Chứa file launchSettings.json
        - Cấu hình môi trường chạy ứng dụng 
    8. Các file cấu hình
        - Program.cs
            + điểm khởi động ứng dụng
            + cấu hình routing, middleware
        - appsettings.json
            + Lưu các cấu hình: 
                |- chuỗi kết nối CSDL
                |- Logging
                |- Các thiết lập hệ thống
Tìm hiểu về định tuyến (Route) trong .NET MVC
    - Cấu trúc cơ bản: 
        app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
        );
    vd: https://localhost:5001/Product/Detail/5
        controller: Product
        action: Detail
        id: 5 (không bắt buộc tham số)
    vd: http://localhost:5051/
        khi không tham chiếu thì:
            - controller mặc định là Home
            - action mặc định là Index
    vd: http://localhost:5051/Index
            - controller: Index
            - action (không tham chiếu): Index
Tìm hiểu về namespace trong C#
    - Namespace là nơi dùng để nhóm các class liên quan và tránh trùng tên.
    ->Namespace = thư mục, Class = file
    vd: namespace MyApp
        {
            class Student { }
        }
    - Mục đích:
        + Tránh trùng tên class
        + Sắp xếp code gọn gàng
        + Dễ quản lý dự án lớn
Tìm hiểu về Controller, View trong .Net MVC
    - Controller: nhận yêu cầu từ người dùng -> xử lý -> trả về View
    vd: public class HomeController : Controller
        {
            public IActionResult Index()
                {
                    return View();
                }
        }
    - View: hiển thị giao diện cho người dùng 
<<<<<<< HEAD
# Bài thực hành số 4
    1. Tìm hiểu về ViewBag trong MVC: truyền dữ liệu từ controller sang view
        - Là một dynamic object (đối tượng động)
        - Dùng để chứa dữ liệu tạm thời trong một request
        - Không cần khai báo kiểu dữ liệu
        vd:
            Controller:
                public ActionResult Index()
                {
                    ViewBag.Title = "Trang chủ";
                    ViewBag.Message = "Chào mừng bạn đến website!";
                    return View();
                }
            View:
                <h1>@ViewBag.Title</h1>
                <p>@ViewBag.Message</p>
    2. Tìm hiểu về gửi nhận dữ liệu giữa View và Controller thông qua Submit form.
        - Khi người dùng bấm Submit:
            View (Form) - chỉ gửi dữ liệu
                ↓
            Gửi dữ liệu HTTP Request (GET hoặc POST)
                ↓
            Controller nhận dữ liệu
                ↓
            Controller - nhận và xử lý dữ liệu
                ↓
            Trả dữ liệu về View
        - Các thành phần bắt buộc:
            + 1 form dữ liệu cần: <form method="post" action="...">
            + Input phải có: name="TenBien"
            + Controller phải có: public ActionResult ActionName(string TenBien)
            -> tên input phải trùng với tên tham số trong controller
        vd:
            controller: 
            namespace DemoWebMVC.Controllers
            {
                public class Buoi4Controller : Controller
                {
                    [HttpGet]
                    public IActionResult Index()
                    {
                        // Gửi dữ liệu từ Controller sang View
                        ViewBag.Message = "Thông báo!";
                        
                        return View(); 
                    }

                    [HttpPost]
                    public IActionResult Index(string hoTen) 
                    {
                        // Xử lý: Gửi thông báo kèm họ tên ngược lại View
                        ViewBag.LoiChao = "Xin chào " + hoTen;

                        return View();
                    }  
                }
            }
            view:
            <h2>@ViewBag.Message</h2>
            <form method="post">
                <label>Nhập họ tên:</label>
                <input type="text" name="hoTen" />

                <br /><br />

                <button type="submit">Gửi</button>
            </form>

            @if (ViewBag.LoiChao != null)
            {
                <h3>@ViewBag.LoiChao</h3>
            }

    3. Tìm hiểu về Models
        - Mô hình MVC:
            + Model: chứa dữ liệu, xử lý nghiệp vụ
            + View: hiển thị giao diện
            + Controller: nhận yêu cầu
        -> Model - mô tả đối tượng trong hệ thống 
        - Vai trò:
            + Quản lý dữ liệu rõ ràng
            + Dễ bảo trì
            + Kiểm tra kiểu dữ liệu
        vd: 
        model:
            namespace Sinhvien.Models
            {
                public class Student
                {
                    public string StudentCode { get; set; }
                    public string FullName { get; set; }
                }
            }
        controller: 
            using Sinhvien.Models;
            public class StudentController : Controller
            {
                // Hiển thị form
                [HttpGet]
                public ActionResult Index()
                {
                    return View();
                }

                // Nhận dữ liệu từ form
                [HttpPost]
                public ActionResult Index(Student std)
                {
                    // Gửi lại object Student sang View
                    ViewBag.Thongbao = "Xin chào: " + std.FullName + " - Mã sinh viên: " + std.StudentCode;
                    return View();
                }
            }
        view:
            <h2>Thông báo: @ViewBag.ThongBao</h2>
            <form asp-action="Index" method="post">
                <input type="text" name="StudentCode" placeholder="Nhập mã sinh viên của bạn" />
                <input type="text" name="FullName" placeholder="Nhập họ tên của bạn" />
                <button type="submit">Gửi dữ liệu</button>
            </form>
    4. Tìm hiểu về Layout 
        - Layout là trang giao diện dùng chung cho toàn bộ website.
            vd những phần dùng chung:
            + Header
            + Menu
            + Footer
            + CSS, JS
            -> Thay vì mỗi View phải viết lại → ta gom vào Layout.
        





        

=======
>>>>>>> 1dbd2ee44ff1b30470c45f3c1bbfc3994af01883
    




