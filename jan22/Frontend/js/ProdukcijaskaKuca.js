import { Film } from "./Film.js";

export class ProdukcijaskaKuca {
    constructor(id, naziv, kategorije) {
        this.id = id;
        this.naziv = naziv;
        this.kontejner = null;
        this.listaKategorija = kategorije;
        this.listaFilmova = [];
    }

    nacrtajKucu(host) {
        let divZaJednuKucu = document.createElement("div");
        host.appendChild(divZaJednuKucu);
        divZaJednuKucu.className = 'divZaJednuKucu'
        this.kontejner = divZaJednuKucu;

        let nazivKuce = document.createElement("h2");
        nazivKuce.innerHTML = this.naziv;
        divZaJednuKucu.appendChild(nazivKuce);

        this.napraviSelect(
            divZaJednuKucu,
            "Kategorija",
            this.listaKategorija,
            this.prikaziFilm,
            this.id
        );

        this.napraviSelect(divZaJednuKucu, "Film", []);

        let divZaOcenu = document.createElement("div");
        divZaJednuKucu.appendChild(divZaOcenu);

        let labela = document.createElement("label");
        labela.innerHTML = "Unesite ocenu";
        divZaOcenu.appendChild(labela);

        const inputZaOcenu = document.createElement("input");
        inputZaOcenu.type = "number";
        inputZaOcenu.className = "inputZaOcenu";
        divZaOcenu.appendChild(inputZaOcenu);

        const dugme = document.createElement("button");
        dugme.innerHTML = "unesi";
        dugme.onclick = ev =>
            this.unesiOcenu(divZaJednuKucu, inputZaOcenu.value);
        divZaJednuKucu.appendChild(dugme);
    }

    napraviSelect(parent, tekst, niz, event, idKuce) {
        let div = document.createElement("div");
        parent.appendChild(div);

        let labela = document.createElement("label");
        labela.innerHTML = tekst;
        div.appendChild(labela);

        let select = document.createElement("select");
        div.appendChild(select);
        select.className = tekst;
        select.onchange = ev => {
            if (event) {
                this.prikaziFilm(
                    ev.target.value,
                    idKuce,
                    parent,
                    this.listaFilmova
                );
            }
        };

        let startOption = document.createElement("option");
        startOption.value = "";
        startOption.innerHTML = "Izaberite " + tekst;
        startOption.selected = "selected";
        startOption.hidden = "hidden";
        select.appendChild(startOption);

        niz.map(k => {
            let opcija = document.createElement("option");
            opcija.innerHTML = k.naziv;
            opcija.value = k.id;
            select.appendChild(opcija);
        });
    }

    prikaziFilm(idKategorije, idKuce, parent, div) {
        console.log(idKategorije, idKuce);
        /// console.log(this)
        fetch(
            `https://localhost:5001/Ispit/VratiFilmove/${idKuce}/${idKategorije}`
        ).then(p =>
            p.json().then(data => {
                const sel = parent.querySelector(".Film");
                //sel.innerHTML = "";

                this.brisiDecu(sel);
                this.listaFilmova = [];

                data.map(k => {
                    // console.log(k)
                    let film = new Film(k);
                    film.nacrtajFilm(sel, parent);

                    this.listaFilmova.push(film);
                });

                this.prikaziTriFilma();
            })
        );
    }

    prikaziTriFilma() {
         console.log('tri filam: ',this.listaFilmova)
        // console.log('kad udje',this.kontejner);

        const div = this.kontejner.querySelector('.divZaStubove')
         
        if(div){
            this.brisiDecu(div)
            const parent = div.parentNode
            
            parent.removeChild(div)
            
        }

        const poredi = (film1, film2) => {
            if (film1.prosecnaOcena > film2.prosecnaOcena) return 1;
            if (film1.prosecnaOcena < film2.prosecnaOcena) return -1;
            return 0;
        };
        this.listaFilmova.sort(poredi); // rastuci
        //    console.log(this.listaFilmova);

        const dizZaStubove = document.createElement("div");
        dizZaStubove.className = 'divZaStubove'
      //  const divZaJednuKucu = this.kontejner.querySelector('.divZaJednuKucu')
        this.kontejner.appendChild(dizZaStubove);

        this.listaFilmova[0].crtajStub(dizZaStubove);

        if (this.listaFilmova.length >= 2) {
            this.listaFilmova[this.listaFilmova.length - 1].crtajStub(
                dizZaStubove
            );

            if (this.listaFilmova.length >= 3) {
                this.listaFilmova[
                    parseInt(this.listaFilmova.length / 2)
                ].crtajStub(dizZaStubove);
            }
        }
       // console.log(dizZaStubove)

        //this.listaFilmova[0],[this.listaFilmova.length-1], [parseInt(this.listaFilmova.length/2)]
    }

    brisiDecu(element) {
        //  console.log(element);
        let child = element.lastElementChild;
        while (child) {
            element.removeChild(child);
            child = element.lastElementChild;
        }
    }

    async unesiOcenu(host, ocena) {
        const sel = host.querySelector(".Film");
        // console.log(sel.value)
        //provere ...

        await fetch(`https://localhost:5001/Ispit/OceniFilm/${sel.value}/${ocena}`, {
            method: "PUT"
        }).then(data => {
            data.json().then(info => {
                console.log(info);
                this.listaFilmova.map( film => {
                    if(film.id == info.id){
                        console.log('uso')
                        this.listaFilmova.pop(film)
                        this.listaFilmova.push(info)
                    }
                })
            });
        });
        console.log(this.listaFilmova)
        this.prikaziTriFilma()
    }
}
