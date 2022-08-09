import { Book } from "./book";


export class PageData {
    pageCount: number;
    books: Array<Book> = new Array<Book>();

    cartItemsCount: number;

    userData: string;
}