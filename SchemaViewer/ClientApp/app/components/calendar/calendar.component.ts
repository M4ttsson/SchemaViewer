import { Component, Inject } from '@angular/core';
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

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
        this.calendar = new Calendar();
        this.isLoading = false;
    }

    onSubmit() {
        this.errorMessage = "";
        this.isLoading = true;

        this.getCalendar();
    }

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
    }
}


