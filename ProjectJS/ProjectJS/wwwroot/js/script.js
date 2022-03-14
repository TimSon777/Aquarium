function displayFish(fish) {
    let div = `<div class="fish" id="fish-id-${fish.id}" style="position:absolute; top: ${fish.currentLocation.y}px; left: ${fish.currentLocation.x}px">Рыба ${fish.threadId}</div>`
    document.getElementById("aquarium").innerHTML += div
}

function deleteElement(e) {
    e.parentNode.removeChild(e)
}

async function createFish() {
    let speed = parseInt(document.getElementById("speed").value)
    let typeFish = document.getElementById("type-fish").value

    await fetch(`/api/Aquarium/CreateFish?typeFish=${typeFish}&speed=${speed}`, {method: "GET"})
        .then(async r => await r.json())
        .then(fish => displayFish(fish))
}

async function deleteAll() {
    await fetch("/api/Aquarium/DeleteAll", {method: "GET"})
        .then(() => {
            let fishes = document.querySelectorAll('.fish');
            fishes.forEach(fish => {
                deleteElement(fish)
            })
        })
}

async function deleteLast() {
    await fetch("/api/Aquarium/DeleteLast", {method: "GET"})
        .then(async r => await r.json())
        .then(id => {
            if (id !== 0) {
                deleteElement(document.getElementById(`fish-id-${id}`))
            }
        })
}

async function deleteRandom() {
    await fetch("/api/Aquarium/DeleteRandomFish", {method: "GET"})
        .then(async r => await r.json())
        .then(id => {
            if (id !== 0) {
                deleteElement(document.getElementById(`fish-id-${id}`))
            }
        })
}

function sendForm(form, selector) {
    form.onsubmit = async function (e) {
        e.preventDefault()
        await selector()
    }
}

function changeLocationAndId(fishes) {
    fishes.forEach(fish => {
        let divFish = document.getElementById(`fish-id-${fish.id}`);
        if (divFish !== null) {
            divFish.style.left = fish.currentLocation.x + "px";
            divFish.innerText = `Рыба ${fish.threadId}`
        }
    })
}

async function startAquarium() {
    await fetch('/api/Aquarium/GetAll',
        {method: "GET"})
        .then(async r => await r.json())
        .then(fishes => fishes.forEach(fish => displayFish(fish)))

    const aquariumConnection = new signalR.HubConnectionBuilder()
        .withUrl("/aquarium")
        .build();

    setInterval(() => {
        aquariumConnection.invoke("SendFishes")
            .then(fishes => changeLocationAndId(fishes))
    }, 15)

    aquariumConnection.start();
}

document.onreadystatechange = async function () {
    if (document.readyState === "complete") {
        await startAquarium()
        sendForm(document.getElementById("add-fish-form"), createFish)
        sendForm(document.getElementById("delete-all-form"), deleteAll)
        sendForm(document.getElementById("delete-last-form"), deleteLast)
        sendForm(document.getElementById("delete-random-form"), deleteRandom)
    }
}