import { Prodavnica } from "./prodavnica.js";

const body = document.querySelector('.glavni')
body.innerHTML = 'hi'


fetch('https://localhost:5001/Ispit/vratiProdavnice')
        .then( res => res.json().then(data => {
            data.forEach(element => {
                console.log(element)
                const p = new Prodavnica(element)
                p.crtajProdavnicu(body)
            });
}))