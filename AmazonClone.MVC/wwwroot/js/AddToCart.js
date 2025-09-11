$(document).ready(function () {
    // Quantity Change Buttons
    $("#increaseQty").click(function () {
        let qty = parseInt($("#quantity").val()) || 1;
        $("#quantity").val(qty + 1);
    });

    $("#decreaseQty").click(function () {
        let qty = parseInt($("#quantity").val()) || 1;
        if (qty > 1) {
            $("#quantity").val(qty - 1);
        }
    });

    // Buy Now
    

    $("#buyNow").click(function () {
        const productId = parseInt($("#productId").val());
        const quantity = parseInt($("#quantity").val()) || 1;
        window.location.href = `/Order/PlaceOrder?ProductId=${productId}&quantity=${quantity}`;
    });


    // Add to Cart
    $("#addToCart").click(function () {
        let $button = $(this);
        let quantity = parseInt($("#quantity").val()) || 1;
        let productId = parseInt($("#productId").val());
        let userId = $("#Email").val();

        //window.location.href = `/Cart/AddToCart?ProductId=${productId}&quantity=${quantity}`;
        $button.prop('disabled', true);
        $.ajax({
            url: "/Cart/AddToCart",
            type: "POST",
            data: {
                ProductId: productId,
                Quantity: quantity,
                UserId: userId
            },
            success: function (data) {
                /*toastr.success(data.message || 'Added to cart!');*/
                $button.prop('disabled', false);
                setTimeout(function () {
                    window.location.href = '/Home/Index';
                    toastr.success(data.message || 'Added to cart!');
                }, 0);
            },
            error: function (xhr) {
                const errorMsg = xhr.responseJSON?.message || 'Failed to add to cart.';
                toastr.error(errorMsg);
                $button.prop('disabled', false);
            }
        });
    });

    // Add to Wishlist
    $("#addToWishlist").click(function () {
        let $button = $(this);
        let productId = parseInt($("#productId").val());

        if (!productId) {
            toastr.error('Product ID not found!');
            return;
        }

        $button.prop('disabled', true);
        $.post("/Wishlist/Add", { productId: productId })
            .done(function () {
                toastr.success('Added to wishlist!');
                $button.prop('disabled', false);
            })
            .fail(function () {
                toastr.error('Failed to add to wishlist.');
                $button.prop('disabled', false);
            });
    });

    $(".cart-option").click(function () {
        let Email = $("#Email").val();
        window.location.href = `/Cart/ViewCart?Email=${Email}`;
    });



    $("#account-toggle").click(function (e) {
        e.stopPropagation();
        $("#account-dropdown").toggle();
    });
    $(document).click(function () {
        $("#account-dropdown").hide();
    });
});

