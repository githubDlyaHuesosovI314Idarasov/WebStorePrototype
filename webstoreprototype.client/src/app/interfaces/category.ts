import { Entity } from "./entity";
import { Product } from "./product";

export interface Category extends Entity {
    name: string;
    icon: string;
    route: string;
    products: Product[];
}
