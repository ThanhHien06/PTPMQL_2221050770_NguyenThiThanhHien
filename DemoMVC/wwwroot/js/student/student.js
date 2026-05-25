const StudentApp = {
    urls: {
        getStudents: '/Student/GetStudents',
        create: '/Student/Create',
        edit: '/Student/Edit',
        delete: '/Student/Delete'
    },
    state: {
        currentPage: 1,
        pageSize: 10
    },
    // Hàm gọi AJAX để tải danh sách sinh viên
    loadTable: function () {
        $.ajax({
            url: this.urls.getStudents,
            type: 'GET',
            data: { 
                page: this.state.currentPage, 
                pageSize: this.state.pageSize 
            },
            success: function (res) {
                // Nhét HTML nhận được vào khung trống
                $('#studentTableContainer').html(res);
            },
            error: function (err) {
                console.error('Lỗi khi tải dữ liệu', err);
                alert('Không thể tải danh sách sinh viên.');
            }
        });
    }
};

// Gọi hàm load dữ liệu ngay khi trang web vừa tải xong
$(document).ready(function () {
    StudentApp.loadTable();
});