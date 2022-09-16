const main = document.querySelector('.glavniDiv')

const divBody = napraviDiv(main,'divBody')
const divLevo = napraviDiv(divBody, 'divLevo')
const divDesno = napraviDiv(divBody, 'divDesno')

    let red = napraviDiv(divLevo,'red')
    napraviLabelu(red, 'Zapremina (cm3)')
    const zapremina = napraviInput(red, 'inZapremina', 'number')

    red = napraviDiv(divLevo,'red')
    napraviLabelu(red, 'Tezina (kg)')
    const tezina = napraviInput(red, 'inTezina', 'number')

    red = napraviDiv(divLevo,'red')
    napraviLabelu(red, 'Datum prijema')
    const datumOd = napraviInput(red, 'inDatumOd', 'date')

    red = napraviDiv(divLevo,'red')
    napraviLabelu(red, 'Datum dostave')
    const datumDo = napraviInput(red, 'inDatumDo', 'date')

    red = napraviDiv(divLevo,'red')
    napraviLabelu(red, 'Cena od')
    const cenaOd = napraviInput(red, 'inCenaOd', 'number')

    red = napraviDiv(divLevo,'red')
    napraviLabelu(red, 'Cena do')
    const cenaDo = napraviInput(red, 'inCenaDo', 'number')

    red = napraviDiv(divLevo,'red')
    const btn = document.createElement('button')
    btn.className = 'btnNadji'
    btn.innerHTML = 'Pronadji'
    
    btn.onclick = ev => prikaziVozila()
    red.appendChild(btn)

    if(zapremina.value && tezina.value && cenaDo.value && cenaOd.value && datumOd.value && datumDo.value){
        btn.disabled = true
    }


function prikaziVozila(){

    if(!(zapremina.value && tezina.value && cenaDo.value && cenaOd.value && datumOd.value && datumDo.value)){
        return alert('Morate uneti sva polja')
    }
    
    fetch(`https://localhost:5001/Ispit/vratiVozila/${zapremina.value}/${tezina.value}/${cenaOd.value}/${cenaDo.value}/${datumOd.value}`)
    .then( res => res.json().then(data => {
        console.log(data)
        brisiDecu(divDesno)
        data.forEach(element => {
            crtajVozilo(element)
        });
    })).catch(reason => alert(reason))
}

function crtajVozilo(v){

    const divJednoVozilo = napraviDiv(divDesno, 'divJednoVozilo')
    napraviLabelu(divJednoVozilo, `Naziv ${v.kompanija.naziv}`)

    const slika = document.createElement('img')
    slika.className = 'slika'
    slika.src = '../Slike/'+v.id+'.png'
    slika.alt = v.naziv
    divJednoVozilo.appendChild(slika)

    napraviLabelu(divJednoVozilo, `Cena ${v.cenaPoDanu}`)
    napraviLabelu(divJednoVozilo, `Prosecna zarada ${v.prosecnaZarada}`)

    const dugme = document.createElement('button')
    dugme.className = 'btnIsporuci'
    dugme.innerHTML = 'Isporuci'
    dugme.onclick = ev => zakaziVozilo(v.id)
    divJednoVozilo.appendChild(dugme)

}
function zakaziVozilo(voziloId){

    if(!(zapremina.value && tezina.value && cenaDo.value && cenaOd.value && datumOd.value && datumDo.value)){
        return alert('Morate uneti sva polja')
    }

    //https://localhost:5001/Ispit/zauzimiVozilo/1/2022-08-24/2022-08-25

    fetch(`https://localhost:5001/Ispit/zauzimiVozilo/${voziloId}/${datumOd.value}/${datumDo.value}`,{
        method: 'PUT'
    })
    .then( res => res.json(). then ( data => {
        console.log(res)
        brisiDecu(divDesno)
        brisiInput()
        alert("Uspesno zakazano")
    })).catch(reason => alert(reason))
}
function brisiInput(){
    const inputi = divLevo.querySelectorAll('input')
    inputi.forEach(el => {
        el.value = ''
    })
}
export function brisiDecu(element){
    let dete = element.lastElementChild
    while(dete){
        element.removeChild(dete)
        dete =  element.lastElementChild
    }
}
export function napraviDiv(host, klasa){
    const div = document.createElement('div')
    div.className = klasa
    host.appendChild(div)
    return div
}
export function napraviLabelu(host, tekst){
    const el = document.createElement('label')
    el.innerHTML = tekst
    host.appendChild(el)
}
export function napraviInput(host, klasa, tip){
    const el = document.createElement('input')
    el.className = klasa
    el.type = tip
    host.appendChild(el)
    return el
}