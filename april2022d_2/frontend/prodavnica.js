export class Prodavnica {
    constructor(id, naziv) {
        this.id = id;
        this.naziv = naziv;
        this.kontejner = null;
        this.korpa = [];
    }
    crtajProdavnicu(host, brendovi, tipovi) {
        const naziv = document.createElement("h2");
        naziv.innerHTML = this.naziv;
        host.appendChild(naziv);

        this.kontejner = this.napraviDiv(host, "divJednaProdavnica");

        const divLevo = this.napraviDiv(this.kontejner, "divLevo");
        this.crtajLevi(divLevo, brendovi, tipovi);
        const divDesno = this.napraviDiv(this.kontejner, "divDesno");
    }
    crtajLevi(host, brendovi, tipovi) {
        let red = this.napraviDiv(host, "red");
        this.napraviLabelu(red, "Tip");
        this.napraviSelekciju(red, tipovi, "selTip");

        red = this.napraviDiv(host, "red");
        this.napraviLabelu(red, "Brend");
        this.napraviSelekciju(red, brendovi, "selBrend");

        red = this.napraviDiv(host, "red");
        this.napraviLabelu(red, "Cena od");
        this.napraviInput(red, "inCenaOd");

        red = this.napraviDiv(host, "red");
        this.napraviLabelu(red, "Cena do");
        this.napraviInput(red, "inCenaDo");

        red = this.napraviDiv(host, "redDugme");
        const btn = document.createElement("button");
        btn.innerHTML = "Pronadji";
        btn.onclick = ev => this.nadji();
        red.appendChild(btn);
    }
    nadji() {
        const tip = this.kontejner.querySelector(".selTip").value;
        if (!tip) {
            return alert("morate izabrati tip");
        }
        const brend = this.kontejner.querySelector(".selBrend").value;
        const cenaOd = this.kontejner.querySelector(".inCenaOd").value;
        const cenaDo = this.kontejner.querySelector(".inCenaDo").value;

        //?brendId=1&cenaOd=1&cenaDo=1

        let query = "";
        if (brend) {
            query += `?brendId=${brend}`;
            if (cenaOd) {
                query += `&cenaOd=${cenaOd}`;
            }
            if (cenaDo) {
                query += `&cenaDo=${cenaDo}`;
            }
        } else {
            if (cenaOd) {
                query += `?cenaOd=${cenaOd}`;
                if (cenaDo) {
                    query += `&cenaDo=${cenaDo}`;
                }
            } else if (cenaDo) {
                query += `?cenaDo=${cenaDo}`;
            }
        }

        fetch(
            `https://localhost:5001/Ispit/nadjiKomponente/${this.id}/${tip}${query}`
        ).then(res =>
            res.json().then(data => {
                console.log(data);

                this.crtajKomponente(data);
            })
        );
    }
    crtajKomponente(data) {
        const brisi = this.kontejner.querySelector(".divZaKomponente");
        if (brisi) this.brisiDecu(brisi);

        const divDesni = this.kontejner.querySelector(".divDesno");
        const divZaKomponente = brisi ? brisi : this.napraviDiv(divDesni, "divZaKomponente");

        data.map(k => {
            const divJedna = this.napraviDiv(
                divZaKomponente,
                "divJednaKomponenta"
            );
            this.napraviLabelu(divJedna, `Sifra: ${k.sifra}`);
            this.napraviLabelu(divJedna, `Naziv: ${k.komponenta.naziv}`);
            this.napraviLabelu(divJedna, `Cena: ${k.cena}`);

            const btn = document.createElement("button");
            btn.innerHTML = "Konfigurisi";
            btn.onclick = ev => this.dodajKomponentu(k);
            divJedna.appendChild(btn);
        });
        this.nacrtajTabelu();
    }
    dodajKomponentu(k) {
        const tabela = this.kontejner.querySelector(".tabela");
        const redovi = tabela.querySelectorAll(".bodyTabela");
        console.log(redovi)

        let naso = false;
        if (redovi) {
            redovi.forEach(red => {
                if (red.value == k.id) {
                    // console.log(red)
                    const tdCena = red.querySelector(".tdcena");
                    const tdKolicina = red.querySelector(".tdkolicina");
                    const kolicina = parseInt(tdKolicina.value);
                    tdCena.innerHTML = kolicina * k.cena;
                    tdKolicina.innerHTML = kolicina + 1;
                    tdKolicina.value = kolicina + 1;
                    naso = true;

                    this.korpa.map(el => {
                        if (el.id == k.id) {
                            // this.korpa.pop(el);
                            // this.korpa.push({
                            //    id: k.id ,
                            //     kolicina: kolicina + 1
                            // });
                            el.kolicina++;
                        }
                    });

                   // return;
                }
            });
            if (naso) {
                return;
            }
        }

        // console.log(this.korpa);
        // if (naso) {
        //     return;
        // }

        const red = document.createElement("tr");
        red.className = "bodyTabela";
        red.value = k.id;
        tabela.appendChild(red);

        const niz = ["sifra", "komponenta", "kolicina", "cena"];

        this.korpa.push({
             id: k.id ,
            kolicina: 1
        });

        niz.map(el => {
            const data = document.createElement("td");
            if (el === "komponenta") {
                data.innerHTML = k.komponenta.naziv;
                data.className = "tdnaziv";
            } else if (el === "kolicina") {
                data.innerHTML = 1;
                data.className = "td" + el;
                data.value = 1;
            } else {
                data.innerHTML = k[el];
                data.className = "td" + el;
            }

            red.appendChild(data);
        });
    }
    nacrtajTabelu() {
         const brisi = this.kontejner.querySelector('.divZaTabelu')
         if(brisi) return

        const divDesni = this.kontejner.querySelector(".divDesno");
        const divZaTabelu = this.napraviDiv(divDesni, "divZaTabelu");
        const tabela = document.createElement("table");
        tabela.className = "tabela";
        divZaTabelu.appendChild(tabela);

        const red = document.createElement("tr");
        red.className = "headTabela";
        tabela.appendChild(red);

        const niz = ["Sifra", "Naziv", "Kolicina", "Cena"];

        niz.forEach(el => {
            const data = document.createElement("th");
            data.innerHTML = el;
            red.appendChild(data);
        });

        const btn = document.createElement("button");
        btn.innerHTML = "Kupi";
        btn.onclick = ev => this.kupi();
        divZaTabelu.appendChild(btn);
    }

    kupi() {
        console.log(this.korpa)
        fetch(`https://localhost:5001/Ispit/kupiKomponente/${this.id}`, {
            method: "PUT",
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify(this.korpa)
        }).then(res => res.json().then(data => {
            // console.log(res)
            if(res.ok){
                alert('uspesno kupljeno')
            }
            else if (res.status == 422){
                alert('nema dovoljno za kupovinu')
            }
        }));

        this.korpa = [];
        this.brisiDecu(this.kontejner.querySelector(".divDesno"));
    }
    brisiDecu(element) {
        let child = element.lastElementChild;
        while (child) {
            element.removeChild(child);
            child = element.lastElementChild;
        }
    }
    napraviSelekciju(host, niz, klasa) {
        const sel = document.createElement("select");
        sel.className = klasa;
        host.appendChild(sel);

        const opt = document.createElement("option");
        opt.innerHTML = "izaberite";
        opt.value = -1;
        sel.appendChild(opt);

        niz.map(el => {
            const op = document.createElement("option");
            op.innerHTML = el.naziv;
            op.value = el.id;
            sel.appendChild(op);
        });
    }
    napraviDiv(parent, klasa) {
        const div = document.createElement("div");
        div.className = klasa;
        parent.appendChild(div);
        return div;
    }
    napraviLabelu(parent, tekst) {
        const l = document.createElement("label");
        l.innerHTML = tekst;
        parent.appendChild(l);
    }
    napraviInput(parent, klasa) {
        const input = document.createElement("input");
        input.className = klasa;
        input.type = "number";
        parent.appendChild(input);
    }
}
