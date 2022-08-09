import { CartItem } from "../models/cartItem";


export class CartService {
    private baseUrl: string = '/cart'

    buy(bookId: number): Promise<number> {
        let userId = localStorage.getItem("s2suserId");
        let url = `${this.baseUrl}/buy?bookId=${bookId}&userData=${userId}`;

        return fetch(url.toString())
            .then(res => res.json())
            .then(res => {
                return res as number
            });
    }

    cancel(cartItemId: number): Promise<CartItem[]> {
        let userId = localStorage.getItem("s2suserId");
        let url = `${this.baseUrl}/cancelbuy?cartItemId=${cartItemId}&userData=${userId}`;

        return fetch(url.toString())
            .then(res => res.json())
            .then(res => {
                return res as CartItem[]
            });
    }

    getCart(): Promise<CartItem[]> {
        let userId = localStorage.getItem("s2suserId");

        let url = `${this.baseUrl}/getcart?userData=${userId}`;

        return fetch(url.toString())
            .then(res => res.json())
            .then(res => {
                return res as CartItem[]
            });
    }

}