
const input = document.getElementById('onlyLettersField'); // отримання елементу вводу
const regex = /^[a-zA-Z\s]*$/; // регулярний вираз, який відповідає лише літерам та пробілам

input.addEventListener('input', () => { // додавання обробника події для вводу
    const value = input.value; // отримання значення вводу
    if (!regex.test(value)) { // перевірка, чи відповідає регулярному виразу
        input.value = value.replace(/[^a-zA-Z\s]/g, ''); // видалення неприпустимих символів зі значення
    }
});
