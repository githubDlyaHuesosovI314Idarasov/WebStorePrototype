import { Entity } from "./entity";
import { Product } from "./product";

export interface Stock extends Entity {
    productId: string;
    locationId: string;
    product: Product;
    location: Location;
    quantity: number;
    
}
