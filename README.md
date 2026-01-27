Họ tên: Nguyễn Thị Thanh Hiền
MSV: 2221050770
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
    




