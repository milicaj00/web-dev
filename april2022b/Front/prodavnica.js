export class Prodavnica {
    constructor(prodavnica) {
        this.id = prodavnica.id;
        this.naziv = prodavnica.naziv;
        this.kontejner = null;
        this.sastojci = prodavnica.sastojci;
        this.brojSendvica = prodavnica.brojSendvica;
        this.dnevnaZarada = prodavnica.dnevnaZarada;
        this.naruci = [];
    }

    crtajProdavnicu(host) {
        const divProdavnica = document.createElement("div");
        host.appendChild(divProdavnica);
        this.kontejner = divProdavnica;
        divProdavnica.className = "prodavnica";

        const naslov = document.createElement("h2");
        naslov.innerHTML = this.naziv;
        divProdavnica.appendChild(naslov);

        const divZaPrikaz = document.createElement("div");
        divZaPrikaz.className = "divZaPrikaz";
        this.kontejner.appendChild(divZaPrikaz);

        const divLevo = document.createElement("div");
        divLevo.className = "divLevo";
        divZaPrikaz.appendChild(divLevo);

        const divDesno = document.createElement("div");
        divDesno.className = "divDesno";
        divZaPrikaz.appendChild(divDesno);

        this.crtajSelekciju(divLevo);
        this.crtajSendvice(divDesno);

        const dugme = document.createElement("button");
        dugme.innerHTML = "Dodaj";
        dugme.onclick = ev => this.dodajSastojak();
        divLevo.appendChild(dugme);
    }

    dodajSastojak() {
        const sto = this.kontejner.querySelector(".selectSto").value;
        const sastojak = this.kontejner.querySelector(".selectSastojak").value;
        const kolicina = this.kontejner.querySelector(".inputKolicina").value;

        if (kolicina <= 0) {
            return;
        }

        const sendvic = this.kontejner.querySelector(`.divZaSastojak${sto}`);
        // console.log(sendvic)

        const el = document.createElement("div");
        el.className = "noviSastojak";

        // console.log(sendvic.style.height.split('').slice(0,-2).join(''))

        let visina = parseInt(
            sendvic.style.height.split("").slice(0, -2).join("")
        );
        // console.log(visina)

        sendvic.style.height = visina + 10 * kolicina + "px";
        el.style.height = 10 * kolicina + "px";
        el.style.backgroundColor = "gray";
        sendvic.appendChild(el);

        this.naruci.push({
            sto: sto,
            sastojak: { id: sastojak, kolicina: kolicina }
        });
    }

    napraviRed(host) {
        const red = document.createElement("div");
        red.className = "red";
        host.appendChild(red);
        return red;
    }

    crtajSelekciju(host) {
        let red = document.createElement("div");
        red.className = "red";
        host.appendChild(red);

        let labela = document.createElement("label");
        labela.innerHTML = "Sto:";
        red.appendChild(labela);
        const nizSendvica = [...Array(this.brojSendvica).keys()];
        const select = document.createElement("select");
        select.className = "selectSto";
        red.appendChild(select);
        this.dodajOpcije(select, nizSendvica);

        red = this.napraviRed(host);
        labela = document.createElement("label");
        labela.innerHTML = "Sastojak:";
        red.appendChild(labela);
        const selectSastojak = document.createElement("select");
        selectSastojak.className = "selectSastojak";
        red.appendChild(selectSastojak);
        this.dodajOpcijeSastojak(selectSastojak);

        red = this.napraviRed(host);
        labela = document.createElement("label");
        labela.innerHTML = "Kolicina:";
        red.appendChild(labela);

        const inputKolicina = document.createElement("input");
        inputKolicina.type = "number";
        inputKolicina.className = "inputKolicina";
        red.appendChild(inputKolicina);
    }

    dodajOpcije(sel, niz) {
        niz.map(el => {
            const op = document.createElement("option");
            op.value = el;
            op.innerHTML = el;
            sel.appendChild(op);
        });
    }

    dodajOpcijeSastojak(sel) {
        this.sastojci.map(el => {
            const op = document.createElement("option");
            op.value = el.id;
            op.innerHTML = el.sastojak.naziv;
            sel.appendChild(op);
        });
    }

    crtajSendvice(host) {
        const nizSendvica = [...Array(this.brojSendvica).keys()];
        nizSendvica.forEach(el => {
            const jedanSendvic = document.createElement("div");
            jedanSendvic.className = "divJedanSendvic";
            jedanSendvic.classList.add(`sto${el}`);
            host.appendChild(jedanSendvic);

            const dugme = document.createElement("button");
            dugme.innerHTML = "Isporuci";
            dugme.onclick = ev => this.prodajSendvic(el);
            jedanSendvic.appendChild(dugme);

            const labela = document.createElement("label");
            labela.innerHTML = "Sto " + el;
            jedanSendvic.appendChild(labela);

            const prviHleb = this.crtajHleb(jedanSendvic);
            prviHleb.classList.add(`hleb${el}`);

            const hleb = document.createElement("div");
            hleb.className = `divZaSastojak${el}`;
            hleb.classList.add("divZaSastojak");
            jedanSendvic.appendChild(hleb);

            this.crtajHleb(jedanSendvic);
        });
    }
    prodajSendvic(sto) {
        // console.log(this.naruci)

        const narucivanje = this.naruci.filter(el => el.sto == sto).map(el => el.sastojak)

        if (narucivanje.length == 0) return;

        //sad u bazu da sa;jem
       console.log( JSON.stringify(narucivanje))

        fetch("https://localhost:5001/Ispit/kupiSendvic/" + this.id, {
            method: "PUT",
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json"
            },
            body:  JSON.stringify(narucivanje)
            
        }).then(res =>
            res.json().then(data => {
                console.log(data);
            })
        );

        //da poveca zaradu i da doda novi hleb i da dodam uslove za kolicinu
    }

    crtajHleb(host) {
        const hleb = document.createElement("div");
        hleb.className = "hleb";
        host.appendChild(hleb);
        return hleb;
    }
}
