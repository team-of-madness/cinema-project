const inputs = document.getElementsByClassName('onlyLettersField'); // отримання всіх елементів вводу з класом onlyLettersField
const regex = /^[a-zA-Z\s]*$/; // регулярний вираз, який відповідає лише літерам та пробілам

for (let i = 0; i < inputs.length; i++) {
    const input = inputs[i]; // отримання поточного елементу вводу
    input.addEventListener('input', () => { // додавання обробника події для вводу поточного елементу
        const value = input.value; // отримання значення вводу
        if (!regex.test(value)) { // перевірка, чи відповідає регулярному виразу
            input.value = value.replace(/[^a-zA-Z\s]/g, ''); // видалення неприпустимих символів зі значення
        }
    });
}

const datetimeInputs = document.getElementsByClassName('datetime-input'); // отримати елемент введення datetime

for (let i = 0; i < datetimeInputs.length; i++) {
    const datetimeInput = datetimeInputs[i];
    datetimeInput.addEventListener('input', () => { // додати обробника події для введення
        const selectedDate = new Date(datetimeInput.value); // отримати дату, яку ввів користувач
        const currentDate = new Date(); // отримати поточний час
        if (selectedDate < currentDate) { // перевірити, чи введений час є меншим ніж поточний час
            datetimeInput.setCustomValidity('Ви повинні вибрати час в майбутньому!'); // встановити власне повідомлення про помилку валідації
        } else {
            datetimeInput.setCustomValidity(''); // зняти попереднє повідомлення про помилку валідації (якщо воно було)
        }
    });
}