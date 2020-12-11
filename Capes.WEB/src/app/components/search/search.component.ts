import { Component, OnInit } from '@angular/core';
import { faSearch, faChevronDown, faChevronLeft } from '@fortawesome/free-solid-svg-icons';
import { SearchService } from '@app/services/search.service';
import { ToastrService } from 'ngx-toastr';
import { ISearchFilters } from '@app/domain/search-filters';
import { IFilterResult } from '@app/domain/filter-result';
import { PaginationInstance } from 'ngx-pagination';

@Component({
    selector: 'app-search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
    icons: any = {
        faSearch: faSearch,
        faChevronDown: faChevronDown,
        faChevronLeft: faChevronLeft
    };
    filters: ISearchFilters = {
        text: null
    };
    loading: boolean = false;
    filterResult: IFilterResult =  {
        books: null,
        awards: null,
        participations: null,
        publications: null
    };
    bookConfig: PaginationInstance = {
        id: 'books-paginator',
        itemsPerPage: 15,
        currentPage: 1,
        totalItems: this.filterResult.books == null ? 0 : this.filterResult.books.length
    };
    awardConfig: PaginationInstance = {
        id: 'awards-paginator',
        itemsPerPage: 15,
        currentPage: 1,
        totalItems: this.filterResult.awards == null ? 0 : this.filterResult.awards.length
    };
    participationConfig: PaginationInstance = {
        id: 'participations-paginator',
        itemsPerPage: 15,
        currentPage: 1,
        totalItems: this.filterResult.participations == null ? 0 : this.filterResult.participations.length
    };
    publicationConfig: PaginationInstance = {
        id: 'publications-paginator',
        itemsPerPage: 15,
        currentPage: 1,
        totalItems: this.filterResult.publications == null ? 0 : this.filterResult.publications.length
    };
    collapse: any = {
        book: true,
        award: true,
        participation: true,
        publication: true
    };

    constructor(private searchService: SearchService, private toastrService: ToastrService) { }

    ngOnInit() {
    }

    public resetCollapse() {
        this.collapse = {
            book: true,
            award: true,
            participation: true,
            publication: true
        };
    };
    public search() {
        if (!this.filters.text) {
            this.toastrService.warning('Text can\'t be empty.');
            return;
        }

        this.searchService.filter(this.filters).subscribe((result: IFilterResult) => {
            this.filterResult = result;
            this.resetCollapse();
        }, (error) => {
              this.toastrService.error('Unexpected error, try again.');
        });
    }
    public trySearch(event) {
        if (event.key === "Enter") {
            this.search();
        }
    }
    public bookPageChanged($event) {
        this.bookConfig.currentPage = $event;
    }
    public awardPageChanged($event) {
        this.awardConfig.currentPage = $event;
    }
    public participationPageChanged($event) {
        this.participationConfig.currentPage = $event;
    }
    public publicationPageChanged($event) {
        this.publicationConfig.currentPage = $event;
    }
}
