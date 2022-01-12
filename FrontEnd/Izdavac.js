export class Izdavac {
    constructor(id, naziv) {
        this.id = id;
        this.knjige = [];
        this.naziv = naziv;
    }
    dodajKnjiigu(knjiga) {
        this.knjige.push(knjiga);
    }
}