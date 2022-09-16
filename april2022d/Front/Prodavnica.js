export class Prodavnica {
    constructor(naziv, id) {
        this.id = id;
        this.naziv = naziv;
        this.kontejner = null;
        this.korpa =[]
        this.divKomponente = null
    }

    crtajProdavnicu(host) {
        let p = host.querySelector(".prodavnica");
        if (p) {
            const parent = p.parentNode;
            parent.removeChild(p);
        }
        p = host.querySelector(".naslov");
        if (p) {
            const parent = p.parentNode;
            parent.removeChild(p);
        }

        const naslov = document.createElement("h2");
        naslov.innerHTML = this.naziv;
        naslov.className = 'naslov'
        host.appendChild(naslov);

        const glavniDiv = document.createElement("div");
        glavniDiv.className = "prodavnica";
        host.appendChild(glavniDiv);
        this.kontejner = glavniDiv;

        this.crtajSelekciju()
    }

    async crtajSelekciju(){
        const divZaSelekciju = document.createElement('div')
        this.kontejner.appendChild(divZaSelekciju)
        divZaSelekciju.className = 'divLevoSelekcija'

        const dizZaSelekciju = document.createElement('div')
        divZaSelekciju.appendChild(dizZaSelekciju)
        dizZaSelekciju.className = 'divZaSelekciju'

        const divKomponente = document.createElement('div')
        divKomponente.className = 'divKomponente'
        this.kontejner.appendChild(divKomponente)
        this.divKomponente = divKomponente

        const divZaLabele = document.createElement('div')
        dizZaSelekciju.appendChild(divZaLabele)
        divZaLabele.className = 'divZaLabele'
        const niz = ['Tip', 'Brend', 'Cena od', 'Cena do']
        this.napraviLabele(divZaLabele, niz)

        const divZaInput = document.createElement('div')
        dizZaSelekciju.appendChild(divZaInput)
        divZaInput.className = 'divZaInput'

        await this.ucitajTipove(divZaInput)
        await this.ucitajBrendove(divZaInput)
        
        const cenaOd = document.createElement('input')
        divZaInput.appendChild(cenaOd)
        cenaOd.className = 'cenaOd'
        cenaOd.type = 'number'
        cenaOd.defaultValue = 0
        
        const cenaDo = document.createElement('input')
        divZaInput.appendChild(cenaDo)
        cenaDo.className = 'cenaDo'
        cenaDo.type = 'number'
        cenaDo.defaultValue = 0

        const divZaDugme = document.createElement('div')
        divZaDugme.className = 'divZaDugme'
        divZaSelekciju.appendChild(divZaDugme)
        const dugme = document.createElement('button')
        dugme.innerHTML = 'Prikazi'
        dugme.onclick = ev => this.prikaziKomponente(dizZaSelekciju)
        divZaDugme.appendChild(dugme)
    }

    napraviLabele(host, niz){
       
        niz.map(el => {
            const labela = document.createElement('label')
            labela.innerHTML = el
            host.appendChild(labela)
        })
    }

    napraviOpcije(select, niz){
        niz.map(el => {
            const op = document.createElement('option')
            op.innerHTML = el.naziv
            op.value = el.id
            select.appendChild(op)
        })
    }
    
    async ucitajTipove(host){

        await fetch("https://localhost:5001/Ispit/sviTipovi").then(p=>p.json().then(data=>{
           // console.log(data)
            const sel = document.createElement('select')
            sel.className = 'selTip'
            host.appendChild(sel)
            const op = document.createElement('option')
            op.innerHTML = 'izaberite tip'
            op.value = -1
            sel.appendChild(op)
            this.napraviOpcije(sel,data)
        }))
    }

    async ucitajBrendove(host){
        await fetch("https://localhost:5001/Ispit/sviBrendovi").then(p=>p.json().then(data=>{
           // console.log(data)
            const sel = document.createElement('select')
            sel.className = 'selBrend'
            host.appendChild(sel)
            const op = document.createElement('option')
            op.innerHTML = 'izaberite brend'
            op.value = -1
            sel.appendChild(op)
            this.napraviOpcije(sel,data)
        }))
    }

    async prikaziKomponente(host){
        
        console.log(host.querySelector('.selTip'))
        const tip = host.querySelector('.selTip').value
        if(!tip || tip <= 0){
            alert('Morate izabrati tip za pretragu')
            return
        }
        const cenaOd = this.kontejner.querySelector('.cenaOd').value ?? 0
        const cenaDo = host.querySelector('.cenaDo').value 
        const brend = host.querySelector('.selBrend').value 
        
        
        await fetch(`https://localhost:5001/Ispit/nadjiKomponentu/${this.id}/${tip}?brend=${brend}&cenaOd=${cenaOd}&cenaDo=${cenaDo}`)
        .then(p => p.json() .then(data => {
            console.log(data)
            this.nacrtajKomponente(data)
        }))
    }

    nacrtajKomponente(data){
        let brisi = this.kontejner.querySelector('.divZaKomponente')
        if(brisi){
            const parent = brisi.parentNode
            parent.removeChild(brisi)
        }
        
        const divZaKomponente = document.createElement('div')
        divZaKomponente.className = 'divZaKomponente'
        this.divKomponente.appendChild(divZaKomponente)
        
        data.map(k => {
            const divZaJednuKomponentu = document.createElement('div')
            divZaJednuKomponentu.className = 'divZaJednuKomponentu'
            divZaKomponente.appendChild(divZaJednuKomponentu)
            //sifra naziv cena dugme konfigurisi

           const niz = [ `Sifra ${k.id}`, `Naziv ${k.brend}`, `Cena ${k.cena}`]
           this.napraviLabele(divZaJednuKomponentu,niz)

           const divZaDugme = document.createElement('div')
            const dugme = document.createElement('button')
            dugme.value = k
            dugme.innerHTML = 'Konfigurisi'
            divZaJednuKomponentu.appendChild(divZaDugme)
            divZaDugme.appendChild(dugme)

            this.nacrtajKorpu()

            dugme.onclick = ev =>{ this.dodajKomponentu(k)}

        })
    }
    
    nacrtajKorpu(){

        const brisi = this.divKomponente.querySelector('.divTabela')
        if(brisi){
            const parent = brisi.parentNode
            parent.removeChild(brisi)
        }

        const divTabela = document.createElement('div')
        divTabela.className = 'divTabela'
        this.divKomponente.appendChild(divTabela)

        const tabela = document.createElement('table')
        tabela.className = 'tabelaKorpa'
        divTabela.appendChild(tabela)

        const head = ['Sifra', 'Naziv', 'Kolicina', 'Cena']

        const headRow = document.createElement('tr')
        tabela.appendChild(headRow)
        headRow.className = 'tabelaHead'

        head.map(el => {
            const cell = document.createElement('td')
            cell.innerHTML = el
            cell.className = el
            headRow.appendChild(cell)
        })
        this.crtajKorpu()
    }

    dodajKomponentu(novaKomponenta){
        let i = 0
        this.korpa.map(el => {
            if(el.id == novaKomponenta.id){
                if(el.kolicina <= el.ukKolicina){
                    alert('Nema vise')
                    return
                }
                el.ukKolicina++
                el.ukCena = novaKomponenta.cena*el.ukKolicina
            }else i++
        })

        if(this.korpa.length === 0 || i == this.korpa.length){
            this.korpa.push({...novaKomponenta, ukCena: novaKomponenta.cena, ukKolicina: 1})
        }
        this.crtajKorpu()
    }

    crtajKorpu(){
        const tabela = this.divKomponente.querySelector('.tabelaKorpa')

        let brisi = this.divKomponente.querySelector('.tabelaBody')
        if(brisi){
            console.log('brisi body')
            const parent = brisi.parentNode
            parent.removeChild(brisi)
        }

        brisi = this.divKomponente.querySelector('.btnKupi')
        if(brisi){
            const parent = brisi.parentNode
            parent.removeChild(brisi)
        }

        const tabelaBody = document.createElement('tbody')
        tabelaBody.className = 'tabelaBody'
        tabela.appendChild(tabelaBody)

        const body = ['id', 'brend', 'ukKolicina', 'ukCena']
        
        this.korpa.map((el)=> {

            const row = document.createElement('tr')
            tabelaBody.appendChild(row)

            body.forEach(index => {
                const cell = document.createElement('td')
                cell.innerHTML = el[index]
                row.appendChild(cell)
            })
        })

        const dugme = document.createElement('button')
        dugme.innerHTML = 'Kupi'
        dugme.className = 'btnKupi'
        dugme.onclick = ev => this.kupi()
        tabela.parentNode.appendChild(dugme)
    }

    kupi(){
        this.korpa.map(el => {
            fetch(`https://localhost:5001/Ispit/kupiKomponentu/${this.id}/${el.idKomp}/${el.ukKolicina}`,{
                method: 'PUT'
            })
            .then( res => res.json().then(data => {
                if(res.ok){
                    alert('uspesno kupljeno')
                    this.crtajProdavnicu(this.kontejner.parentNode)
                }
                else{
                    alert('doslo je do greske')
                }
            }))
        })
    }
}
