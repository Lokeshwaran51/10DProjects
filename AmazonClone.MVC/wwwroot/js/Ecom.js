

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



$(document).ready(function () {
    $('.remove-btn').on('click', function () {
        const productId = $(this).data('productid');

        if (confirm('Are you sure you want to remove this item?')) {
            $.ajax({
                url: '/Cart/RemoveItemFromCart',
                type: 'POST',
                data: { ProductId: productId },
                success: function (response) {
                    if (response.success) {
                        alert(response.message);
                        location.reload();
                    } else {
                        alert('Failed to remove item: ' + response.message);
                    }
                },
                error: function () {
                    alert('An error occurred while removing the item.');
                }
            });
        }
    });
});


