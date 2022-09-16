export class Prodavnica {
    constructor(naziv, id, brendovi) {
        this.naziv = naziv;
        this.id = id;
        this.brendovi = brendovi;
        this.kontejner = null;

        this.artikli = [];
    }

    crtajProdavnicu(host) {
        const naziv = document.createElement("h2");
        naziv.innerHTML = this.naziv;
        host.appendChild(naziv);

        const divBody = document.createElement("div");
        divBody.className = "divBodyProdavnica";
        this.kontejner = divBody;
        host.appendChild(divBody);

        const divLevo = document.createElement("div");
        divLevo.className = "divLevo";
        this.kontejner.appendChild(divLevo);
        this.crtajDivLevo(divLevo);

        const divDesno = document.createElement("div");
        divDesno.className = "divDesno";
        this.kontejner.appendChild(divDesno);
    }

    crtajDivLevo(host) {
        let div = this.divRedLevo(host);
        this.napraviLabelu(div, "Brend:");
        this.dodajBrendove(div);

        div = this.divRedLevo(host);
        this.napraviLabelu(div, "Cena od:");
        this.napraviInput(div, "inCenaOd");

        div = this.divRedLevo(host);
        this.napraviLabelu(div, "Cena do:");
        this.napraviInput(div, "inCenaDo");

        div = this.divRedLevo(host);
        this.napraviRadio(div);

        div = this.divRedLevo(host);
        const btn = document.createElement("button");
        btn.className = "btnNadji";
        btn.innerHTML = "Nadji";
        btn.onclick = ev => this.nadji();
        div.appendChild(btn);
    }

    nadji() {
        console.log("nadji");
        this.artikli = [];

        const brend = this.kontejner.querySelector(".selBrendovi").value;
        if (brend == 0) {
            return alert("Morate izabrati brend");
        }
        const cenaOd = this.kontejner.querySelector(".inCenaOd").value;
        const cenaDo = this.kontejner.querySelector(".inCenaDo").value;

        const velicina = this.kontejner
            .querySelector("input[type='radio'][name='velicina']:checked")
            ?.value.toLowerCase();

        let query = "";
        if (cenaOd) {
            query += `?cenaOd=${cenaOd}`;
            if (cenaDo) {
                query += `&cenaDo=${cenaDo}`;
            }
        } else if (cenaDo) {
            query += `?cenaDo=${cenaDo}`;
        }

        fetch(
            `https://localhost:5001/Ispit/nadjiArtikle/${this.id}/${brend}${query}`
        ).then(res =>
            res.json().then(data => {
                if (!velicina) {
                    this.artikli = data;
                } else {
                    data.map(el => {
                        if (el[velicina] > 0) {
                            this.artikli.push(el);
                        }
                    });
                }

                this.crtajArtikal(velicina);
            })
        );
    }

    crtajArtikal(velicina) {
        const host = this.kontejner.querySelector(".divDesno");
        console.log(this.artikli);

        this.brisiDecu(host);

        this.artikli.map(data => {
            const div = document.createElement("div");
            div.className = "divJedanArtikal";
            host.appendChild(div);

            const slika = document.createElement("img");
            slika.className = "slika";
            slika.src = 'https://www.chanel.com/images/t_fashionzoom1/f_auto/chanel-22-large-handbag-black-shiny-calfskin-gold-tone-metal-shiny-calfskin-gold-tone-metal--packshot-default-as3262b0803794305-8857336053790.jpg'
            div.appendChild(slika);

            this.napraviLabelu(div, data.artikal.naziv);

            if (velicina) this.napraviLabelu(div, "Kolicina " + data[velicina]);

            this.napraviLabelu(div, "Cena " + data.cena);

            if(velicina){
                const btn = document.createElement("button");
                btn.className = "btnKupi";
                btn.innerHTML = "Kupi";
                btn.onclick = ev => this.kupiArtikal(data.id, velicina)
                div.appendChild(btn);
            }
        });
    }

    kupiArtikal(spojId, velicina){
        
        fetch(`https://localhost:5001/Ispit/kupi/${spojId}/${velicina}`,{
            method:'PUT'
        }).then(
            res => {
                if(res.ok){
                    alert('Uspesno ste kupili')

                    this.brisiDecu(this.kontejner.querySelector('.divDesno'))
                    this.brisiInput()
                }
                else{
                    alert('Doslo je do greske')
                }
            })
    }

    brisiInput(){
        const brend = this.kontejner.querySelector(".selBrendovi")
        brend.value = 0
        const cenaOd = this.kontejner.querySelector(".inCenaOd")
        cenaOd.value = ''
        const cenaDo = this.kontejner.querySelector(".inCenaDo")
        cenaDo.value = ''

        const velicina = this.kontejner
            .querySelector("input[type='radio'][name='velicina']")
            console.log(velicina)
            
    }

    brisiDecu(kont) {
        let child = kont.lastElementChild;
        while (child) {
            kont.removeChild(child);
            child = kont.lastElementChild;
        }
    }

    divRedDesno(host) {
        const div = document.createElement("div");
        div.className = "divRedDesno";
        host.appendChild(div);

        return div;
    }

    divRedLevo(host) {
        const div = document.createElement("div");
        div.className = "divRedLevo";
        host.appendChild(div);

        return div;
    }

    napraviLabelu(host, labela) {
        const l = document.createElement("label");
        l.innerHTML = labela;
        host.appendChild(l);
    }

    dodajBrendove(host) {
        const sel = document.createElement("select");
        sel.className = "selBrendovi";
        host.appendChild(sel);

        const option = document.createElement("option");
        option.innerHTML = "Izaberite brend";
        option.value = 0;
        sel.appendChild(option);

        this.brendovi.map(el => {
            const op = document.createElement("option");
            op.innerHTML = el.naziv;
            op.value = el.id;
            sel.appendChild(op);
        });
    }

    napraviInput(host, klasa) {
        const input = document.createElement("input");
        input.className = klasa;
        input.type = "number";
        // input.defaultValue = 0
        host.appendChild(input);
    }

    napraviRadio(host) {
        const niz = ["S", "M", "L"];
        niz.map(el => {
            const btn = document.createElement("input");
            btn.type = "radio";
            btn.name = "velicina";
            btn.value = el;
            host.appendChild(btn);
            this.napraviLabelu(host, el);
        });
    }
}
