import { Knjiga } from "./Knjiga.js";
export class Izdavac {
    constructor(id, naziv) {
        this.id = id;
        this.knjige = [];
        this.naziv = naziv;
        this.minContainer;
    }
    dodajKnjiigu(knjiga) {
        this.knjige.push(knjiga);
    }
    async crtajKnjigeIzdavaca(host, idBiblioteke, maxNum) {
        if (!host)
            throw Error("host je null/...");
        this.minContainer = host;
        let help;
        let izdavacPom = new Izdavac(this.id, this.naziv);
        await fetch(`https://localhost:5001/Izdavac/PribaviKnjigeIzdavaca/${this.id}/${idBiblioteke}`).then(p => p.json()).then(q => {
            q.forEach((el, index) => {
                help = new Knjiga(el.id, el.m, el.n, el.naslov, el.trenKolicina, el.autor, izdavacPom, el.slika);
                if (index <= maxNum) {
                    help.crtajKnjigu(this.minContainer.querySelectorAll("div")[index]);
                }
            });
        });

    }
}