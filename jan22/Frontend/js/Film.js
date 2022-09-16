
export class Film {
    constructor(film) {
        this.id = film.id;
        this.naziv = film.naziv;
        this.prosecnaOcena = film.prosecnaOcena;
        this.brojOcena = film.brojOcena;
        this.kategorijaId = film.kategorijaId;
    }

    nacrtajFilm(sel, divZaJednuKucu) {
        let opcija = document.createElement("option");
        opcija.innerHTML = this.naziv;
        opcija.value = this.id;
        sel.appendChild(opcija);
    }

    crtajStub(host) {

        let divZaJedanStub = document.createElement("div");
        host.appendChild(divZaJedanStub)

        divZaJedanStub.className = 'divZaJedanStub'
        
        let ocena = document.createElement("label");
        
        ocena.innerHTML = this.prosecnaOcena;
        divZaJedanStub.appendChild(ocena);

        let stub = document.createElement("div");
        stub.className = 'stub'
        divZaJedanStub.appendChild(stub)

        let divProcenat = document.createElement("div");
        divProcenat.style.backgroundColor = "green";
        divProcenat.style.height = `${this.prosecnaOcena * 10}px`;
        divProcenat.className = 'divProcenat'
        stub.appendChild(divProcenat)


        let labelime = document.createElement("label");
        labelime.className = "labelime";
        labelime.innerHTML = this.naziv;
        divZaJedanStub.appendChild(labelime);
    }
}
