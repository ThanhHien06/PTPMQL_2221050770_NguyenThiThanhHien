$(document).ready(function () {
    // 1. Bấm nút Xóa -> Bật cảnh báo
    $(document).on('click', '.btn-delete-student', function () {
        let id = $(this).data('id');
        $.get(StudentApp.urls.delete + '/' + encodeURIComponent(id), function (res) {
            $('#modalContainer').html(res);
            $('#deleteStudentModal').modal('show');
        });
    });

    // 2. Xác nhận Xóa
    $(document).on('submit', '#deleteStudentForm', function (e) {
        e.preventDefault();
        let form = $(this);

        $.ajax({
            url: StudentApp.urls.delete,
            type: 'POST',
            data: form.serialize(),
            success: function (res) {
                if (res.success) {
                    $('#deleteStudentModal').modal('hide');
                    StudentApp.loadTable();
                } else {
                    alert('Lỗi: Không thể xóa sinh viên này.');
                }
            }
        });
    });
});