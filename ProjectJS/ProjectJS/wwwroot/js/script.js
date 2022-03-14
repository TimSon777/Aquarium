const Direction = {Left: 1, Right: 2};

class BaseFish {
    constructor(id, speed, direction) {
        this.id = 0;
        this.speed = speed;
        this.direction = direction;
        this.x = 0;
        this.y = 0;
    }

    /*sayHi() {
        alert(this.name);
    }*/
}

function sendForm (form) {
    
    fetch("/api/Aquarium/CreateFish",
        {
            method: "POST",
            body: JSON.stringify(form.elements)
        })
        .then(r => alert(r))
        
    
   /* let httpRequest = new XMLHttpRequest();
    httpRequest.open("POST", "/api/Aquarium/CreateFish");
    httpRequest.onload = function(event) {
        alert("Success, server responded with: " + event.target.response);
    };
    let formData = new FormData(form);
    httpRequest.send(formData);*/
    
}

function deleteAll (form) {
    let httpRequest = new XMLHttpRequest();
    httpRequest.open("POST", "/api/Aquarium/DeleteAll");
    httpRequest.onload = function(event) {
        alert("Success, server responded with: " + event.target.response);
    };
    let formData = new FormData(form);
    httpRequest.send(formData);
}

document.onreadystatechange = function () {
    if (document.readyState === "complete"){
        
        
        
        let formCreateFish = document.getElementById("add-fish-form");
        formCreateFish.onsubmit = function (e) {
            e.preventDefault()
            sendForm(formCreateFish)
        }
        
        
        
        let formDeleteAll = document.getElementById("delete-all-form");
        formDeleteAll.onsubmit = function (e) {
            e.preventDefault()
            deleteAll(formDeleteAll)
        }

      
    }
}

