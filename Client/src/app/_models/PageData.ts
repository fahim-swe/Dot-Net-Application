import { Member } from "./member";

export interface PageData {
    pageNumber: number;
    pageSize: number;
    firstPage: string;
    lastPage: string;
    totalPages: number;
    totalRecords: number;
    nextPage?: any;
    previousPage: string;
    data: Member[];
    succeeded: boolean;
    errors?: any;
    message?: any;
}