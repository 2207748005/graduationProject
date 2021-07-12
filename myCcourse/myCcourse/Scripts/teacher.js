$(function () {
    //alert("1")
})
$("#information").on("click", function () {
    if (confirm("确认要查看个人信息")) {
        window.location.href = "/teacher/TeacherInfo";
    }  
})
$("#CourseRelease").on("click", function () {
    confirm("确认要发布课程")
    window.location.href = "/teacher/CourseRelease";
})
$("#inquire").on("click", function () {
    confirm("查看课程")
    window.location.href = "/teacher/inquire";
})