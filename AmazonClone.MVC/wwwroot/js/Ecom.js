/*$(document).ready(function () {
    $(".category-link").click(function (e) {
        e.preventDefault();

        var categoryId = $(this).data("category-id");
        var container = $(this).siblings(".subcategory-container");

        // Clear previous dropdown or create if it doesn't exist
        container.empty();

        $.ajax({
            url: '/Home/GetSubCategoryByCategoryId/' + categoryId,
            method: 'GET',
            success: function (data) {
                if (data.length > 0) {
                    var dropdown = $('<select></select>').attr('id', 'subCategoryDropdown_' + categoryId);
                    dropdown.append('<option value="">Select Subcategory</option>');

                    $.each(data, function (i, sub) {
                        dropdown.append('<option value="' + sub.subCategoryId + '">' + sub.subCategoryName + '</option>');
                    });

                    container.append(dropdown);
                    dropdown.show();
                } else {
                    container.append('<p>No subcategories found.</p>');
                }
            },
            error: function () {
                alert("Error loading subcategories.");
            }
        });
    });
});
*/