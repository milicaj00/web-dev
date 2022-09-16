import {ProdukcijaskaKuca} from './js/ProdukcijaskaKuca.js'

let plato = document.getElementById("main");
// plato.innerHTML = "hi";

let divZaKuce = document.createElement("div");
divZaKuce.className = 'divZaKuce'
plato.appendChild(divZaKuce);

fetch("https://localhost:5001/Ispit/VratiSveKuce").then(p =>
    p.json().then(data => {
        console.log(data);
        data.map(kuca => {
            let p = new ProdukcijaskaKuca(kuca.id, kuca.naziv, kuca.listaKategorija)
            p.nacrtajKucu(divZaKuce)
        });
    })
);

