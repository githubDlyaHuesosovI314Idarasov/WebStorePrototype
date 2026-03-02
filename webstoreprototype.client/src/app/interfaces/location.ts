import { Entity } from "./entity";
import { FullAddress } from "./full-address";
import { Stock } from "./stock";

export interface Location extends Entity {
    adress: FullAddress;
    stocks: Stock[];
}
