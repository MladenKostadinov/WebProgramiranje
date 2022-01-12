import { Biblioteka } from "./Biblioteka.js";

document.title = "Pocetna";

let a = fetch("https://localhost:5001/Biblioteka/PribaviSveBiblioteke").then(p => p.json()).then(q => {
    q.forEach(e => {
        let v = new Biblioteka(e.id, e.m, e.n, e.ime, e.maxKolicina, e.knjige);
    });
});



