

    $(document).ready(function () {
        //var userEmail = '@HttpContextAccessor.HttpContext.Session.GetString("Email")';
        var Email = $("#Email").val();

        if (Email) {
        $.ajax({
            url: '/Cart/CartItemCount?Email=' + encodeURIComponent(Email),
            type: 'GET',
            success: function (count) {
                $('#cart-count').text(count);
            },
            error: function () {
                console.error('Failed to load cart item count.');
            }
        });
        }
    });
