console.log("gamesList.js loaded");
function updateGameList() { //funktionen som ska uppdatera 
    fetch('/Games/GameList') //Skickar en GET request till partialview som ska visa alla spel. Skickar anrop till funktionen icontroller 
        .then(r => r.text()) //r är HTTP-svaret från fetch och .text behandlar svaret som html och inte json.
        .then(html => { //Körs när html är hämtat 
            const container = document.getElementById("gameListContainer"); //Letar upp gameListContainer i cshtml vyn 
            if (container) { //Om containern finns 
                container.innerHTML = html; //Stoppar in den nya tabellen och uppdaterar sidan 
            }
        })
        .catch(err => console.error(err));
}

setInterval(updateGameList, 2000);