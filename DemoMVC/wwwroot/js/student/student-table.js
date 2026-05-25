$(document).ready(function () {
    // Chuyển trang (1, 2, 3...)
    $(document).on('click', '.pagination-link', function (e) {
        e.preventDefault();
        let page = $(this).data('page');
        if (page) {
            StudentApp.state.currentPage = page;
            StudentApp.loadTable();
        }
    });

    // Thay đổi số lượng sinh viên hiển thị (10, 20...)
    $(document).on('change', '#pageSizeSelect', function () {
        StudentApp.state.pageSize = $(this).val();
        StudentApp.state.currentPage = 1; // Reset về trang 1
        StudentApp.loadTable();
    });
});