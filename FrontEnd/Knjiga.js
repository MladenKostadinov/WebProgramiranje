
export class Knjiga {

    constructor(id, m, n, naslov, trenKolicina, autor, izdavac, slika) {
        this.id = id || 0;
        this.naslov = naslov || "";
        this.m = m || 0;
        this.n = n || 0;
        this.autor = autor || "";
        this.trenKolicina = trenKolicina || 0;
        this.izdavac = izdavac;
        this.slika = slika || "";
        this.minContainer = null;
    }

    crtajKnjigu(host) {
        if (!host)
            throw Error("host je null/...");
        this.minContainer = host;
        host.innerHTML = '';

        let pom = document.createElement("div");
        pom.className = "knjiga " + this.m + " " + this.n;
        let help = this.id;
        pom.onclick = function () {
            location.href = `https://localhost:5001/InformacijeKnjiga/${help}`;
        }
        this.minContainer = pom;
        host.replaceWith(pom);

        pom = document.createElement("label");
        if (this.naslov === "") pom.innerHTML = "Slobodno";
        else pom.innerHTML = this.naslov;
        pom.className += "naslov";
        this.minContainer.appendChild(pom);

        pom = document.createElement("label");
        if (this.naslov === "") pom.innerHTML = "Mesto";
        else pom.innerHTML = this.autor;
        pom.className += "autor";
        this.minContainer.appendChild(pom);

        pom = document.createElement("label");
        if (this.izdavac != null) pom.innerHTML = this.izdavac.naziv;
        else pom.innerHTML = "-----";
        pom.className += "izdavac";
        this.minContainer.appendChild(pom);

        pom = document.createElement("label");
        if (this.trenKolicina != 0) pom.innerHTML = "Kolicina: " + this.trenKolicina;
        else pom.innerHTML = "-----";
        pom.className += "trenKolicina";
        this.minContainer.appendChild(pom);

        if (this.slika) {
            let oImg = document.createElement("img");
            oImg.src = `data:image/png;base64,${this.slika}`;
            oImg.height = 160;
            oImg.width = 120;
            this.minContainer.appendChild(oImg);
        }
        else {
            let oImg = document.createElement("img");
            oImg.height = 160;
            oImg.width = 120;
            this.minContainer.appendChild(oImg);
        }
    }
    static crtajPraznoPolje(host) {
        if (!host)
            throw Error("host je null/...");

        let pom;
        let div = document.createElement("div");
        div.className = "praznoPolje";
        host.appendChild(div);

        pom = document.createElement("label");
        pom.innerHTML = "Slobodno";
        div.appendChild(pom);

        pom = document.createElement("label");
        pom.innerHTML = "Mesto";
        div.appendChild(pom);

        pom = document.createElement("label");
        pom.innerHTML = "-----";
        div.appendChild(pom);

        pom = document.createElement("label");
        pom.innerHTML = "-----";
        div.appendChild(pom);

        let oImg = document.createElement("img");
        oImg.height = 160;
        oImg.width = 120;
        div.appendChild(oImg);
        return div;
    }

}