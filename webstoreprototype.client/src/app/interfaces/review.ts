import { Entity } from "./entity";
import { Product } from "./product";
import { User } from "./user";

export interface Review extends Entity{
    rating: number;
    comment: string;
    createdAt: Date;
    userId: number;
    productId: number;
    user: User;
    product: Product;

}
