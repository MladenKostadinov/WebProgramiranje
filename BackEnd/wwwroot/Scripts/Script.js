function promenaParagrafa(id) {

    let text = prompt("Unesite novi opis knjige:", "");
    if (text == null || text == "") {
        return;
    } else if (text.length > 1000) {
        return alert("Max duzina opisa je 1000 karaketra!");
    }
    fetch(`https://localhost:5001/Knjiga/IzmeniOpis?IdKnjige=${id}&opis=${text}`, {
        method: "PUT"
    })
        .then(response => response.text())
        .then(p1 => {
            console.log(p1);

            fetch(`https://localhost:5001/Knjiga/PreuzmiOpis?IdKnjige=${id}`, {
                method: "GET"
            })
                .then(response => response.text())
                .then(p1 => {
                    console.log(p1);
                    document.body.querySelector(".par").innerHTML = p1;
                });
        });
}

function promenaNaslova(id) {

    let text = prompt("Unesite novi naslov:", "");
    if (text == null) return;
    if (text.length < 1 || text.length > 50) {
        return alert("Naslov mora da ima izmedju 1 i 50 karaktera!");
    }
    fetch(`https://localhost:5001/Knjiga/IzmeniNaslovKnjige?IdKnjige=${id}&naslov=${text}`, {
        method: "PUT"
    })
        .then(response => response.text())
        .then(p1 => {
            console.log(p1);

            fetch(`https://localhost:5001/Knjiga/PreuzmiNaslovKnjige?IdKnjige=${id}`, {
                method: "GET"
            })
                .then(response => response.text())
                .then(p1 => {
                    console.log(p1);
                    document.body.querySelector(".lNaslov").innerHTML = p1;
                });
        });
}
function promenaAutora(id) {

    let text = prompt("Unesite novog autora:", "");
    if (text == null) return;
    if (text.length < 1 || text.length > 50) {
        return alert("Ime autora mora da ima izmedju 1 i 50 karaktera!");
    }
    fetch(`https://localhost:5001/Knjiga/IzmeniAutoraKnjige?IdKnjige=${id}&autor=${text}`, {
        method: "PUT"
    })
        .then(response => response.text())
        .then(p1 => {
            console.log(p1);

            fetch(`https://localhost:5001/Knjiga/PreuzmiAutoraKnjige?IdKnjige=${id}`, {
                method: "GET"
            })
                .then(response => response.text())
                .then(p1 => {
                    console.log(p1);
                    document.body.querySelector(".lAutor").innerHTML = p1;
                });
        });
}
function promenaSlike(id) {
    let file1;
    let input = document.createElement('input');
    input.type = 'file';
    input.onchange = e => {
        file1 = e.target.files[0];
        var data = new FormData();
        data.append("img", file1);
        if (file1) {
            fetch(`https://localhost:5001/Knjiga/DodajSliku?IdKnjige=${id}`, {
                method: "POST",
                body: data
            })
                .then(response => response.text())
                .then(p1 => {
                    console.log(p1);
                    var fr = new FileReader();
                    fr.readAsDataURL(input.files[0]);
                    fr.onload = function () {
                        document.body.querySelector("img").src = fr.result;
                    };
                });
        }
    }
    input.click();
}
function promenaIzdavaca(id) {

    let text = prompt("Unesite novog izdavaca:", "");
    if (text == null) return;
    if (text.length < 1 || text.length > 50) {
        return alert("Ime izdavaca mora da ima izmedju 1 i 50 karaktera!");
    }
    fetch(`https://localhost:5001/Knjiga/IzmeniIzdavacaKnjige?IdKnjige=${id}&naziv=${text}`, {
        method: "PUT"
    })
        .then(response => response.text())
        .then(p1 => {
            console.log(p1);

            fetch(`https://localhost:5001/Knjiga/PreuzmiIzdavaca?IdKnjige=${id}`, {
                method: "GET"
            })
                .then(response => response.text())
                .then(p1 => {
                    console.log(p1);
                    document.body.querySelector(".lIzdavac").innerHTML = p1;
                });
        });
}
function promenaKolicine(id) {

    let text = prompt("Unesite novu kolicinu:", "");
    if (text == null ) {
        return;
    } else if (parseInt(text) < 0) {
        return alert("Kolicina mora da bude veca ili jednaka 0!");
    }
    fetch(`https://localhost:5001/Knjiga/IzmeniKolicinu?IdKnjige=${id}&Nkolicina=${text}`, {
        method: "PUT"
    })
        .then(async response => {
            if (!response.ok) {
                const text = await response.text();
                throw new Error(text);
            }
            return response.text();
        })
        .then(p1 => {
            console.log(p1);

            fetch(`https://localhost:5001/Knjiga/PreuzmiKolicinu?IdKnjige=${id}`, {
                method: "GET"
            })
                .then(response => response.text())
                .then(p1 => {
                    console.log(p1);
                    document.body.querySelector(".lKolicina").innerHTML = "Kolicina: " + p1;
                });
        }).catch(e => {
            alert(e);
        });
}