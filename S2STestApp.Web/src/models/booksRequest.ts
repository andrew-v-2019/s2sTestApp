import { SortDirection } from "./sortDirection";

export class BooksRequest {

    page: number;
    sortField: string;
    sortDirection: SortDirection
    userData: string

    constructor(
        page: number,
        sortField: string,
        sortDirection: SortDirection,
        userId: string
    ) {
        this.page = page;
        this.sortField = sortField;
        this.sortDirection = sortDirection;
        this.userData = userId;
    }
}