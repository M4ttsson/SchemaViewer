import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common'
import { Http, RequestOptions, URLSearchParams } from '@angular/http';
import { CalendarEvent, Calendar } from "../../shared/calendar";

@Component({
    selector: 'calendar',
    templateUrl: './calendar.component.html'
})
export class CalendarComponent {
    
    private calendar: Calendar;
    private errorMessage: string;
    private isLoading: boolean;
    private calendarStorageKey = "calendarStorage";

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string, @Inject(PLATFORM_ID) private platformId: Object) {
        this.calendar = new Calendar();

        if (isPlatformBrowser(this.platformId)) {
            let calTemp = localStorage.getItem(this.calendarStorageKey);
            if (calTemp) {
                this.calendar = JSON.parse(calTemp) as Calendar;
            }
        }

        this.isLoading = false;

    }

    onSubmit() {
        this.errorMessage = "";
        this.isLoading = true;

        this.getCalendar();

        if (isPlatformBrowser(this.platformId)) {
            localStorage.setItem(this.calendarStorageKey, JSON.stringify(this.calendar));
        }
    }

    //getUrlFromCookie() :string {
    //    return this.cookieService.get(this.calendarStorageKey);
    //}

    //saveUrlInCookie(url: string) {
    //    this.cookieService.set(this.calendarStorageKey, url);
    //}

    // look at this for service worker
    // https://houssein.me/progressive-angular-applications

    // move this to service later
    getCalendar() {
        let params: URLSearchParams = new URLSearchParams();
        params.set('url', this.calendar.url);

        let requestOptions = new RequestOptions();
        requestOptions.params = params;

        this.http.get(this.baseUrl + 'api/Calendar/GetCalendarEvents', requestOptions).subscribe(result => {
            this.calendar.calendarEvents = result.json() as CalendarEvent[];
            this.isLoading = false;
        }, error => this.handleError(error.json()));
    }

    private handleError(error: any) {
        console.error(error);
        this.errorMessage = error.message;
        this.isLoading = false;
        this.calendar.calendarEvents = [];
    }
}


