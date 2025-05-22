
$(document).ready(function () {
    // Add to Cart
    $("#addToCart").click(function () {
        $.post("/Cart/Add", { productId: @Model.Id }, function (data) {
            toastr.success('Added to cart!');
        }).fail(function () {
            toastr.error('Failed to add to cart.');
        });
    });

    // Add to Wishlist
    $("#addToWishlist").click(function () {
        $.post("/Wishlist/Add", { productId: @Model.Id }, function (data) {
            toastr.success('Added to wishlist!');
        }).fail(function () {
            toastr.error('Failed to add to wishlist.');
        });
    });
});