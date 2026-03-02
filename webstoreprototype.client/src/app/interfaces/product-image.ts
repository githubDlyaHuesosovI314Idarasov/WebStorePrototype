import { Entity } from "./entity";
import { Product } from "./product";

export interface ProductImage extends Entity {
    url: string;
    productId: string;
    product: Product;
}
