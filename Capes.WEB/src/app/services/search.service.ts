import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { HttpClient } from '@angular/common/http';
import { ISearchFilters } from '@app/domain/search-filters';

@Injectable({
    providedIn: 'root'
})
export class SearchService {
    private env = environment;
    private controllerName: string = 'Search';
    private controllerUrl: string = `${this.env.apiUrl}/${this.controllerName}`;
    constructor(private httpClient: HttpClient) { }

    filter(filters: ISearchFilters) {
        return this.httpClient.post(`${this.controllerUrl}`, filters);
    }
}
