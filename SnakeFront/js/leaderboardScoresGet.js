function fillTScores(scores) {
    const tscore = document.getElementById("leaderboard-body");
    tscore.innerHTML = '';

    scores.forEach((score, index) => {
        const row = document.createElement("tr");

        const positionTd = document.createElement("td");
        positionTd.textContent = index + 1;
        row.appendChild(positionTd);

        const usernameTd = document.createElement("td");
        usernameTd.textContent = score.username;
        row.appendChild(usernameTd);

        const scoreTd = document.createElement("td");
        scoreTd.textContent = score.scoreValue;
        row.appendChild(scoreTd);

        tscore.appendChild(row);
    });
}

const API_URL = 'https://localhost:7076/api/';

async function loadLeaderboard() {
    try {
        const response = await fetch(API_URL + 'Score');
        if (!response.ok) {
            throw new Error("Cannot connect to the server");
        }
        const data = await response.json();
        console.log(data);
        fillTScores(data);
    } catch (error) {
        console.log('Error: ' + error.message);
        throw error;
    }
}

document.addEventListener('DOMContentLoaded', function (){
    loadLeaderboard().then(r => {
        const leaderboardBody = document.getElementById("leaderboard-body");
        const errorRow = document.createElement("tr");
        const errorCell = document.createElement("td");

        errorCell.ColSpan = 3;
        errorCell.className = "text-center text-red-500 py-4";
        errorCell.textContent = "Cannot load leaderboard. Try again later!";

        errorRow.appendChild(errorCell);
        leaderboardBody.appendChild(errorRow);
    });
});