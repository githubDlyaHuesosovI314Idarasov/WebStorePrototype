import { Entity } from "./entity";
import { Product } from "./product";
import { User } from "./user";

export interface Order extends Entity {

    userId: string;
    orderNumber: number;
    productsCount: number;
    totalAmount: number;
    orderDate: Date;
    status: OrderStatus;
    user: User;
    products: Array<Product>;
}

enum OrderStatus{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}
