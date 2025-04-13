// Variables globales
let canvas = document.getElementById('game-canvas');
let ctx = canvas.getContext('2d');
let scoreElement = document.getElementById('score');
let startButton = document.getElementById('start-button');
let usernameInput = document.getElementById('username');
let leaderboardBody = document.getElementById('leaderboard-body');

// Configuración del juego
const blockSize = 20;
const width = canvas.width / blockSize;
const height = canvas.height / blockSize;
let snake = [];
let food = {};
let direction = "right";
let nextDirection = "right";
let score = 0;
let gameInterval;
let gameRunning = false;

// URL del backend (cambiar según donde esté desplegado)
const apiUrl = 'https://localhost:27017/api/scores';

// Inicializar el juego
function init() {
    if (!usernameInput.value.trim()) {
        alert('Por favor ingresa un nombre de usuario');
        return;
    }

    if (gameRunning) {
        clearInterval(gameInterval);
    }

    // Reiniciar variables
    snake = [{x: 5, y: 5}];
    direction = "right";
    nextDirection = "right";
    score = 0;
    scoreElement.textContent = score;

    // Generar comida
    createFood();

    // Iniciar el bucle del juego
    gameRunning = true;
    gameInterval = setInterval(gameLoop, 150);

    // Deshabilitar el botón de inicio
    startButton.disabled = true;
}

// Función principal del juego
function gameLoop() {
    moveSnake();

    if (checkCollision()) {
        gameOver();
        return;
    }

    if (checkFood()) {
        score += 10;
        scoreElement.textContent = score;
        createFood();
    } else {
        // Si no come, eliminar la cola
        snake.pop();
    }

    // Actualizar el canvas
    draw();
}

// Dibujar el estado del juego
function draw() {
    // Limpiar el canvas con el color correspondiente al modo
    ctx.fillStyle = isDarkMode ? "black" : "white";
    ctx.fillRect(0, 0, canvas.width, canvas.height);

    // Dibujar la serpiente (color cambia según modo)
    ctx.fillStyle = isDarkMode ? "lime" : "green";
    for (let i = 0; i < snake.length; i++) {
        ctx.fillRect(
            snake[i].x * blockSize,
            snake[i].y * blockSize,
            blockSize,
            blockSize
        );
    }

    // Dibujar la comida
    ctx.fillStyle = "red";
    ctx.fillRect(
        food.x * blockSize,
        food.y * blockSize,
        blockSize,
        blockSize
    );
}

// Mover la serpiente
function moveSnake() {
    // Actualizar la dirección
    direction = nextDirection;

    // Crear una nueva cabeza basada en la dirección
    let head = {x: snake[0].x, y: snake[0].y};

    switch(direction) {
        case "right":
            head.x++;
            break;
        case "left":
            head.x--;
            break;
        case "up":
            head.y--;
            break;
        case "down":
            head.y++;
            break;
    }

    // Añadir la nueva cabeza al frente del array
    snake.unshift(head);
}

// Verificar colisiones
function checkCollision() {
    // Colisión con los bordes
    if (
        snake[0].x < 0 ||
        snake[0].x >= width ||
        snake[0].y < 0 ||
        snake[0].y >= height
    ) {
        return true;
    }

    // Colisión con su propio cuerpo
    for (let i = 1; i < snake.length; i++) {
        if (snake[0].x === snake[i].x && snake[0].y === snake[i].y) {
            return true;
        }
    }

    return false;
}

// Verificar si la serpiente come
function checkFood() {
    return snake[0].x === food.x && snake[0].y === food.y;
}

// Crear comida en una posición aleatoria
function createFood() {
    food = {
        x: Math.floor(Math.random() * width),
        y: Math.floor(Math.random() * height)
    };

    // Asegurarse de que la comida no aparezca en la serpiente
    for (let i = 0; i < snake.length; i++) {
        if (food.x === snake[i].x && food.y === snake[i].y) {
            createFood();
            return;
        }
    }
}

// Game over
function gameOver() {
    clearInterval(gameInterval);
    gameRunning = false;

    // Enviar puntuación al servidor
    sendScore(usernameInput.value, score);

    // Habilitar el botón de inicio
    startButton.disabled = false;

    // Mostrar mensaje
    alert(`¡Juego terminado! Tu puntaje: ${score}`);

    usernameInput.value = "";
}

// Enviar puntuación al servidor
function sendScore(username, scoreValue) {
    fetch(apiUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            username: username,
            scoreValue: scoreValue
        })
    })
        .then(response => {
            if (response.ok) {
                loadLeaderboard();
            } else {
                console.error('Error al enviar la puntuación');
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

// Cargar tabla de puntuaciones
function loadLeaderboard() {
    fetch(apiUrl)
        .then(response => response.json())
        .then(data => {
            leaderboardBody.innerHTML = '';

            data.forEach((item, index) => {
                let row = document.createElement('tr');

                row.innerHTML = `
                            <td>${index + 1}</td>
                            <td>${item.username}</td>
                            <td>${item.scoreValue}</td>
                        `;

                leaderboardBody.appendChild(row);
            });
        })
        .catch(error => {
            console.error('Error al cargar el leaderboard:', error);
        });
}

// Controles del teclado
document.addEventListener('keydown', (e) => {
    if (!gameRunning) return;

    // Evitar que la serpiente vaya en la dirección opuesta
    switch(e.key) {
        case "ArrowUp":
            if (direction !== "down") nextDirection = "up";
            break;
        case "ArrowDown":
            if (direction !== "up") nextDirection = "down";
            break;
        case "ArrowLeft":
            if (direction !== "right") nextDirection = "left";
            break;
        case "ArrowRight":
            if (direction !== "left") nextDirection = "right";
            break;
    }
});

// Evento para iniciar el juego
startButton.addEventListener('click', init);

// Cargar el leaderboard al cargar la página
loadLeaderboard();