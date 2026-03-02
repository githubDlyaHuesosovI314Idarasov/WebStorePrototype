import { Category } from "./category";
import { Entity } from "./entity";
import { ProductImage } from "./product-image";
import { Stock } from "./stock";

export interface Product extends Entity {
    name: string;
    description: string;
    price: number;
    discountedPrice: number | null;
    isOnSale: boolean;
    isCouponAppliable: boolean;
    categoryId: string;
    category: Category;
    stocks: Stock[];
    images: ProductImage[];
}
