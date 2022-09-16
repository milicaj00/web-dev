import { Prodavnica } from "./prodavnica.js"

const glavniDiv = document.querySelector('.glavni')
glavniDiv.innerHTML= 'hi'

await fetch('https://localhost:5001/Ispit/sveProdavnice')
.then(res => res.json().then( data => {
    console.log(data)
    data.prodavnice.map(p => {
        const divProdavnica = document.createElement('div')
        divProdavnica.className = 'divProdavnica'
        glavniDiv.appendChild(divProdavnica)

        const prod = new Prodavnica(p.id,p.naziv)
        prod.crtajProdavnicu(divProdavnica, data.brendovi, data.tipovi)
    })
}))