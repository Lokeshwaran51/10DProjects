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
   /* $("#buyNow").click(function () {
        const productId = parseInt($("#productId").val());
        const quantity = parseInt($("#quantity").val()) || 1;

        $.ajax({
            url: `/Order/PlaceOrder`,
            type: "POST",
            data: {
                ProductId: productId,
                Quantity: quantity
            },
            success: function (response) {
                console.log("Order placed successfully:", response);
            },
            error: function (xhr, status, error) {
                console.error("Error placing order:", error);
            }
        });
    });*/

    $("#buyNow").click(function () {
        const productId = parseInt($("#productId").val());
        const quantity = parseInt($("#quantity").val()) || 1;
        window.location.href = `/Order/PlaceOrder?ProductId=${productId}&quantity=${quantity}`;
    });


    // Add to Cart
    $("#addToCart").click(function () {
        var $button = $(this);
        var quantity = parseInt($("#quantity").val()) || 1;
        var productId = parseInt($("#productId").val());
        var userId = $("#Email").val(); 

        //window.location.href = `/Cart/AddToCart?ProductId=${productId}&quantity=${quantity}`;
        if (!productId) {
            toastr.error('Product ID not found!');
            return;
        }

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
                toastr.success(data.message || 'Added to cart!');
                $button.prop('disabled', false);
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
        var $button = $(this);
        var productId = parseInt($("#productId").val());

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
        var $button = $(this);
        var Email = $("#Email").val();

        $.ajax({
            url: '/Cart/ViewCart?Email=' + encodeURIComponent(Email),
            type: "GET",
            success: function (data) {
                $('#cart-content').html(data); 
                $button.prop('disabled', false);
            },
            error: function (xhr) {
                const errorMsg = xhr.responseJSON?.message || 'Failed to load cart.';
                toastr.error(errorMsg);
                $button.prop('disabled', false);
            }
        });
    });

    /*$(".cart-option").click(function () {
        var Email = $("#Email").val();
        window.location.href = `/Cart/ViewCart?Email=${Email}`;
    });*/


});