$(document).ready(function () {
    // 1. Bấm nút Sửa trên bảng -> Mở Modal
    $(document).on('click', '.btn-edit-student', function () {
        let id = $(this).data('id'); 
        
        // Gọi lên /Student/Edit/MãSV
        $.get(StudentApp.urls.edit + '/' + encodeURIComponent(id), function (res) {
            $('#modalContainer').html(res);
            $('#editStudentModal').modal('show');
            $.validator.unobtrusive.parse('#editStudentForm');
        });
    });

    // 2. Bấm nút Cập nhật -> Lưu dữ liệu
    $(document).on('submit', '#editStudentForm', function (e) {
        e.preventDefault();
        let form = $(this);

        if (!form.valid()) return;

        $.ajax({
            url: StudentApp.urls.edit,
            type: 'POST',
            data: form.serialize(),
            success: function (res) {
                if (res.success) {
                    $('#editStudentModal').modal('hide');
                    StudentApp.loadTable(); // Cập nhật lại giao diện bảng
                } else {
                    $('#modalContainer').html(res);
                    $('#editStudentModal').modal('show');
                    $.validator.unobtrusive.parse('#editStudentForm');
                }
            }
        });
    });
});