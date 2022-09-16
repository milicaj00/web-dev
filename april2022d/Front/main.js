import { Prodavnica } from "./Prodavnica.js"

const body = document.getElementById('main')
body.innerHTML = 'hi'
const divZaProdavnice = document.createElement('div')
body.appendChild(divZaProdavnice)

fetch('https://localhost:5001/Ispit/sveProdavnice')
        .then(p=> p.json().then( data => {
            console.log(data)
            data.map(data => {

                const dugme = document.createElement('button')
                dugme.innerHTML = data.naziv
                divZaProdavnice.appendChild(dugme)
                dugme.onclick = ev =>{ 
                    const p = new Prodavnica(data.naziv, data.id)
                    p.crtajProdavnicu(divZaProdavnice)
                }

            })
            
        }))