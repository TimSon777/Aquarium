function displayFish(fish) {
    let img = '';
    if (fish.typeFish === 1) 
        img = `<img id="img-id-${fish.id}" class="shark shark1" width="100" height="100" src="images/shark3.png" alt="Shark">`
    else 
        img = `<img id="img-id-${fish.id}" class="shark shark2" width="100" height="100" src="images/shark2.png" alt="Shark">`
    
    let div = `<div class="fish" id="fish-id-${fish.id}" style="position:absolute; top: ${fish.currentLocation.y}px; left: ${fish.currentLocation.x}px">
                    <p class="threadId" id="thread-fish-${fish.id}">${fish.threadId}</p> 
                    ${img}
               </div>`
    document.getElementById("aquarium").innerHTML += div
}

function deleteElement(e) {
    e.parentNode.removeChild(e)
}

async function createFish() {
    let speed = parseInt(document.getElementById("speed").value)
    let typeFish = document.getElementById("type-fish").value
    
    await fetch(`/api/Aquarium/CreateFish?typeFish=${typeFish}&speed=${speed}`, {method: "GET"})
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
            let threadFish = document.getElementById(`thread-fish-${fish.id}`);
            threadFish.innerText = `${fish.threadId}`;
            let image = document.getElementById(`img-id-${fish.id}`);
            if (divFish.style.left === "800px" || fish.direction === 1)
                image.style.transform = 'scale(-1, 1)';
            else if (divFish.style.left === "0px" || fish.direction === 2)
                image.style.transform = 'scale(1, 1)';
        }
        else {
            displayFish(fish);
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
        aquariumConnection.invoke("GetFishes")
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