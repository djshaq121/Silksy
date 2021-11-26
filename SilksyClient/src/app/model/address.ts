export class Address {

    constructor(addressLine1, addressLine2, country, city, postCode) {
        this.addressLine1 = addressLine1;
        this.addressLine2 = addressLine2;
        this.country = country;
        this.city = city;
        this.postCode = postCode;
    }

    addressLine1: string;
    addressLine2: string;
    city: string;
    country: string;
    postCode: string;
}