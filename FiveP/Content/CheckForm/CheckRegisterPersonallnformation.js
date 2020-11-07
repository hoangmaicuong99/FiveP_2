$("#selectTechnology").select2({
        placeholder: 'Chọn công nghệ (tối đa 3 công nghệ)..',
        maximumSelectionLength: 3,
        allowClear: true
    });
    $(document).ready(function () {
        $("#provincial").click(function () {
            let searchText = $(this).val();
            $.ajax({
                type: "post",
                url: "/Account/District?id=" + searchText,
                contentType: "html",
                success: function (response) {
                    $("#district").html(response);
                }
            })
        })
    })
    $(document).ready(function () {
        $("#district").click(function () {
            let searchText = $(this).val();
            $.ajax({
                type: "post",
                url: "/Account/Commune?id=" + searchText,
                contentType: "html",
                success: function (response) {
                    $("#commune").html(response);
                }
            })
        })
    })
function CheckForm() {
    let user_nicename = document.getElementById("user_nicename").value;
    let user_phone = document.getElementById("user_phone").value;
    let user_link_github = document.getElementById("user_link_github").value;
    let user_link_facebok = document.getElementById("user_link_facebok").value;
    let regex_name = /^[a-z0-9_-]{3, 16}$/;
    let regex_phone = /((09|03|07|08|05)+([0-9]{8})\b)/;
    let regex_github = /((git|ssh|http(s)?)|(git@a[\w.]+))(:(\/\/)?)([\w.@a:/\-~]+)(\.git)(\/)?/;
    let regex_facebook = /^(?:https?:\/\/)?(?:www\.|m\.|mobile\.|touch\.|mbasic\.)?(?:facebook\.com|fb(?:\.me|\.com))\/(?!$)(?:(?:\w)*#!\/)?(?:pages\/)?(?:photo\.php\?fbid=)?(?:[\w\-]*\/)*?(?:\/)?(?:profile\.php\?id=)?([^\/?&\s]*)(?:\/|&|\?)?.*$/;
    
    if (user_nicename.length < 3 || user_nicename.length > 17) {
        window.alert("Tên từ 3 đến 16 ký tự !");
        return false;
    }
    if (user_link_github != "")
    {
        if (regex_github.test(user_link_github) == false) {
            window.alert("Không đúng định dạng github !");
            return false;
        }
    }
    if (user_link_facebok != "")
    {
        if (regex_facebook.test(user_link_facebok) == false) {
            window.alert("Không đúng định dạng facebook !");
            return false;
        }
    }
    if (user_phone != "")
    {
        if (regex_phone.test(user_phone) == false) {
            window.alert("Không đúng định dạng sdt Việt Nam !");
            return false;
        }
    }
    return true;
    
}
