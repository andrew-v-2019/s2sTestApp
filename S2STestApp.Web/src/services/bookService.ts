import { Book } from "../models/book"
import { BooksRequest } from "../models/booksRequest";
import { PageData } from "../models/pageData";


export class BooksService {
    private baseUrl: string = '/books'

    getBooks(request: BooksRequest): Promise<Book[]> {
        let url = `${this.baseUrl}/getbooks?`;
        Object.keys(request).forEach(key => { url = `${url}&${key}=${request[key]}` });

        return fetch(url.toString())
            .then(res => res.json())
            .then(res => {
                return res as Book[]
            });
    }


    getPageData(request: BooksRequest): Promise<PageData> {
        let url = `${this.baseUrl}/getpagedata?`;
        Object.keys(request).forEach(key => { url = `${url}&${key}=${request[key]}` });

        return fetch(url.toString())
            .then(res => {
                try {
                    console.log(res);
                    if (!res.ok) {
                        res.text().then(txt => {
                            console.log(txt);
                        });

                        return [];
                    }
                    let d = res.json();
                    return d;
                } catch (error) {
                    console.warn(error);
                }

            })
            .then(res => {
                return res as PageData
            });
    }

}