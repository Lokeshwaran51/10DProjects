function addToWishlist(productId) {
    fetch('/api/add-to-wishlist', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ productId: productId }),
    })
        .then(response => response.json())
        .then(data => {
            alert('Product added to wishlist!');
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}