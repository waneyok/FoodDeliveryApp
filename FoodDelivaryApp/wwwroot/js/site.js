// Количество товаров в корзине
function updateCartCount() {
    fetch('/Cart/GetCount')
        .then(response => response.json())
        .then(data => {
            const cartBadge = document.querySelector('.cart-badge');
            if (cartBadge) {
                if (data.count > 0) {
                    cartBadge.textContent = data.count;
                    cartBadge.style.display = 'inline';
                } else {
                    cartBadge.style.display = 'none';
                }
            }
        });
}

// Обновление количества товара
function updateQuantity(itemId, change) {
    const input = document.getElementById(`quantity-${itemId}`);
    let newValue = parseInt(input.value) + change;
    
    if (newValue < 1) newValue = 1;
    if (newValue > 10) newValue = 10;
    
    input.value = newValue;
    
    // Отправка формы
    document.getElementById(`form-${itemId}`).submit();
}

// Инициализация при загрузке страницы
document.addEventListener('DOMContentLoaded', function() {
    updateCartCount();
    
    // Обработчики для кнопок +/- в корзине
    document.querySelectorAll('.quantity-btn').forEach(button => {
        button.addEventListener('click', function() {
            const itemId = this.dataset.itemId;
            const change = parseInt(this.dataset.change);
            updateQuantity(itemId, change);
        });
    });
});