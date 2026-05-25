$(document).ready(function () {
    // 1. Bấm nút Thêm -> Mở Modal
    $(document).on('click', '#btnAddStudent', function () {
        $.get(StudentApp.urls.create, function (res) {
            $('#modalContainer').html(res);
            $('#createStudentModal').modal('show');
            // Kích hoạt lại Validate của C# cho form vừa nạp
            $.validator.unobtrusive.parse('#createStudentForm');
        });
    });

    // 2. Bấm nút Lưu -> Gửi AJAX POST lên Server
    $(document).on('submit', '#createStudentForm', function (e) {
        e.preventDefault();
        let form = $(this);
        
        // Trình duyệt tự kiểm tra lỗi trước (để trống, sai định dạng email...)
        if (!form.valid()) return;

        $.ajax({
            url: StudentApp.urls.create,
            type: 'POST',
            data: form.serialize(), 
            success: function (res) {
                if (res.success) {
                    $('#createStudentModal').modal('hide');
                    StudentApp.state.currentPage = 1; // Đưa về trang 1 để xem SV mới
                    StudentApp.loadTable(); // Tải lại bảng
                } else {
                    // Nếu lỗi từ server, nạp lại form (để hiện dòng chữ đỏ báo lỗi)
                    $('#modalContainer').html(res);
                    $('#createStudentModal').modal('show');
                    $.validator.unobtrusive.parse('#createStudentForm');
                }
            }
        });
    });
});