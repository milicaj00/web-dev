import { Prodavnica } from "./prodavnica.js"

const mainDiv = document.querySelector('.mainDiv')
mainDiv.innerHTML = 'hi'

fetch('https://localhost:5001/Ispit/sveProdavnice')
    .then( p => p.json().then( data => {
        
        console.log(data)
        
        data.map(el => {
            const divZaProdavnicu = document.createElement('div')
            divZaProdavnicu.className = 'divZaProdavnicu'
            mainDiv.appendChild(divZaProdavnicu)

            const prodavnica = new Prodavnica(el.naziv, el.id, el.brendovi);
            prodavnica.crtajProdavnicu(divZaProdavnicu)

        })

    }))