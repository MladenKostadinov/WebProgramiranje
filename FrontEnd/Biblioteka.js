import { Izdavac } from "./Izdavac.js";
import { Knjiga } from "./Knjiga.js";

export class Biblioteka {

    constructor(idBiblioteke, m, n, ime, maxKapacitet, nizKnjiga) {
        this.idBiblioteke = idBiblioteke;
        this.m = m;
        this.n = n;
        this.ime = ime;
        this.maxKapacitet = maxKapacitet;
        this.nizKnjiga = [];
        this.container = null;
        this.crtajBiblioteku(document.body);

        for (let i = 0; i < this.m * this.n; i++) {
            Knjiga.crtajPraznoPolje(this.container.querySelector(".divRight"));
        }
        let help;
        nizKnjiga.forEach(el => {
            this.nizKnjiga.push(help = new Knjiga(el.id, el.m, el.n, el.naslov, el.trenKolicina, el.autor, el.izdavac, el.slika));
            help.crtajKnjigu(this.container.querySelector(".divRight").querySelectorAll("div")[el.n + el.m * this.n]);
        });
        console.log(this.nizKnjiga);
    }
    crtajBiblioteku(host) {
        if (!host)
            throw Error("host je null");
        this.container = host;

        let pom = document.createElement("div");
        pom.className += "glavniDiv " + this.ime;
        this.container.appendChild(pom);
        this.container = pom;

        pom = document.createElement("div");
        pom.className += "divLeft";
        this.container.appendChild(pom);

        pom = document.createElement("div");
        pom.className += "divRight";
        this.container.appendChild(pom);

        pom = document.createElement("label");
        pom.innerHTML = "Naslov";
        this.container.querySelector(".divLeft").appendChild(pom);

        pom = document.createElement("input");
        pom.type = "text";
        pom.className += "naslov";
        this.container.querySelector(".divLeft").appendChild(pom);

        pom = document.createElement("label");
        pom.innerHTML = "Autor";
        this.container.querySelector(".divLeft").appendChild(pom);

        pom = document.createElement("input");
        pom.type = "text";
        pom.className += "autor" + " " + this.ime;
        this.container.querySelector(".divLeft").appendChild(pom);

        pom = document.createElement("label");
        pom.innerHTML = "Kolicina";
        this.container.querySelector(".divLeft").appendChild(pom);

        pom = document.createElement("input");
        pom.type = "number";
        pom.className += "kolicina";
        this.container.querySelector(".divLeft").appendChild(pom);

        pom = document.createElement("label");
        pom.innerHTML = "Slika";
        this.container.querySelector(".divLeft").appendChild(pom);

        pom = document.createElement("input");
        pom.type = "file";
        pom.className += "fileInput";
        pom.accept = "image/*";
        this.container.querySelector(".divLeft").appendChild(pom);

        pom = document.createElement("label");
        pom.innerHTML = "Izdavac";
        this.container.querySelector(".divLeft").appendChild(pom);

        pom = document.createElement("select");
        pom.className += "selectIzdavaca";
        this.container.querySelector(".divLeft").appendChild(pom);

        let niz;
        fetch("https://localhost:5001/Izdavac/PribaviIzdavace").then(p => p.json()).then(q => {
            q.forEach(e => {
                niz = document.createElement("option");
                niz.innerText = e.naziv;
                niz.value = e.id;
                this.container.querySelector(".selectIzdavaca").add(niz);
            });
        });

        pom = document.createElement("button");
        pom.className += "dugme";
        pom.innerHTML = "Informacije o izdavacu";
        pom.onclick = function () {
            location.href = `https://localhost:5001/InformacijeIzdavaca/${this.parentElement.querySelector(".selectIzdavaca").value}`;
        }

        this.container.querySelector(".divLeft").appendChild(pom);

        pom = document.createElement("label");
        pom.innerHTML = "Pozicija";
        this.container.querySelector(".divLeft").appendChild(pom);

        niz = ["X", "Y"];

        pom = document.createElement("div");
        pom.className += "divZaSelect";
        this.container.querySelector(".divLeft").appendChild(pom);

        niz.forEach(element => {

            pom = document.createElement("label");
            pom.innerHTML = element;
            this.container.querySelector(".divZaSelect").appendChild(pom);

            pom = document.createElement("select");
            pom.className += element + "Select";
            this.container.querySelector(".divZaSelect").appendChild(pom);
        });
        for (let i = 0; i < this.m; i++) {
            pom = document.createElement("option");
            pom.innerText = i;
            pom.value = i;
            this.container.querySelector(".XSelect").add(pom);
        }
        for (let i = 0; i < this.n; i++) {
            pom = document.createElement("option");
            pom.innerText = i;
            pom.value = i;
            this.container.querySelector(".YSelect").add(pom);
        }

        pom = document.createElement("button");
        pom.className += "dugme";
        pom.innerHTML = "Dodaj";

        function pomocna(idBiblioteke, bibliotekaKojaSeMenja) {
            pom.addEventListener('click', function () {
                let izdavac = new Izdavac(this.parentElement.querySelector(".selectIzdavaca").value, this.parentElement.querySelector(".selectIzdavaca").
                    options[this.parentElement.querySelector(".selectIzdavaca").selectedIndex].text);

                let knjiga = new Knjiga(0,
                    this.parentElement.querySelector(".XSelect").value,
                    this.parentElement.querySelector(".YSelect").value,
                    this.parentElement.querySelector(".naslov").value,
                    this.parentElement.querySelector(".kolicina").value,
                    this.parentElement.querySelector(".autor").value,
                    izdavac);
                izdavac.dodajKnjiigu(knjiga);
                let ista;

                if (knjiga.trenKolicina <= 0)
                    return alert("Broj knjiga koji se dodaje mora da bude veci od 0");

                if (knjiga.autor.length <= 0 || knjiga.autor.length > 50)
                    return alert("Polje autor mora da ima izmedju 1 i 50 karaktera!");

                if (knjiga.naslov.length <= 0 || knjiga.naslov.length > 50)
                    return alert("Polje naslov mora da ima izmedju 1 i 50 karaktera!");

                if (knjiga.trenKolicina > bibliotekaKojaSeMenja.maxKapacitet)
                    return alert("Max kolicina knjige je: "+bibliotekaKojaSeMenja.maxKapacitet);

                bibliotekaKojaSeMenja.nizKnjiga.forEach(el => {
                    if (el.m == knjiga.m && el.n == knjiga.n)
                        ista = el;
                });

                if (!ista) {
                    fetch(`https://localhost:5001/Knjiga/DodajKnjigu?IdBiblioteke=${idBiblioteke}`, {
                        method: "POST",
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify(knjiga, (key, value) => {
                            if (key === "knjige") return undefined;
                            else if (key === "minContainer") return undefined;
                            else return value;
                        })
                    })
                        .then(response => response.text())
                        .then(p1 => {

                            console.log(p1);

                            fetch(`https://localhost:5001/Knjiga/PreuzmiIdKnjige?M=${knjiga.m}&N=${knjiga.n}&IdBiblioteke=${idBiblioteke}`, {
                                method: "GET"
                            })
                                .then(q => q.text())
                                .then(q1 => {
                                    console.log(q1);
                                    knjiga.id = q1;
                                    bibliotekaKojaSeMenja.nizKnjiga.push(knjiga);
                                    {
                                        var input = bibliotekaKojaSeMenja.container.querySelector('input[type="file"]');
                                        var data = new FormData();
                                        data.append("img", input.files[0]);
                                        if (input.files[0]) {
                                            fetch(`https://localhost:5001/Knjiga/DodajSliku?IdKnjige=${q1}`, {
                                                method: "POST",
                                                body: data
                                            })
                                                .then(q => q.text())
                                                .then(q1 => {
                                                    console.log(q1);
                                                    knjiga.crtajKnjigu(bibliotekaKojaSeMenja.container.querySelector(".divRight").querySelectorAll("div")[parseInt(knjiga.n) + parseInt(knjiga.m) * parseInt(bibliotekaKojaSeMenja.n)]);
                                                    var fr = new FileReader();
                                                    fr.readAsDataURL(input.files[0]);
                                                    fr.onload = function () {
                                                        knjiga.minContainer.querySelector("img").src = fr.result;
                                                    };
                                                });
                                        }
                                        else {
                                            knjiga.crtajKnjigu(bibliotekaKojaSeMenja.container.querySelector(".divRight").querySelectorAll("div")[parseInt(knjiga.n) + parseInt(knjiga.m) * parseInt(bibliotekaKojaSeMenja.n)]);
                                        }
                                    }
                                });
                        });
                }
                else {
                    console.log("zauzeto mesto");
                    alert("Zauzeto mesto!");
                }
            })
        };
        pomocna(this.idBiblioteke, this);
        this.container.querySelector(".divLeft").appendChild(pom);

        pom = document.createElement("button");
        pom.className += "dugme";
        pom.innerHTML = "Obrisi";
        function pomocna1(bibliotekaKojaSeMenja) {
            pom.addEventListener('click', function () {
                let id = -1;;
                let ptr;
                let m = this.parentElement.querySelector(".XSelect").value;
                let n = this.parentElement.querySelector(".YSelect").value;
                bibliotekaKojaSeMenja.nizKnjiga.forEach(el => {
                    if (el.m == m && el.n == n) {
                        id = el.id;
                        ptr = bibliotekaKojaSeMenja.nizKnjiga.indexOf(el);
                        console.log("ptr: " + ptr);
                    }
                });
                if (id != -1) {
                    fetch(`https://localhost:5001/Knjiga/BrisiKnjigu/${id}`, {
                        method: "DELETE"
                    }).then(p => p.text())
                        .then(p1 => {
                            console.log(p1);
                            bibliotekaKojaSeMenja.nizKnjiga.splice(ptr, 1);
                            bibliotekaKojaSeMenja.container.querySelector(".divRight").querySelectorAll("div")[parseInt(n) + parseInt(m) * parseInt(bibliotekaKojaSeMenja.n)]
                                .replaceWith(Knjiga.crtajPraznoPolje(bibliotekaKojaSeMenja.container.querySelector(".divRight")
                                    .querySelectorAll("div")[parseInt(n) + parseInt(m) * parseInt(bibliotekaKojaSeMenja.n)]));
                            console.log(bibliotekaKojaSeMenja.nizKnjiga);
                        });
                }
                else {
                    console.log(bibliotekaKojaSeMenja.nizKnjiga);
                }
            })
        };
        pomocna1(this);
        this.container.querySelector(".divLeft").appendChild(pom);

        var perfEntries = performance.getEntriesByType("navigation");
        if (perfEntries[0].type === "back_forward") {
            location.reload(true);
        }
    }
}