import { Component, Inject } from '@angular/core';
import { Http, RequestOptions, URLSearchParams } from '@angular/http';
import { CalendarEvent, Calendar } from "./calendar";

@Component({
    selector: 'calendar',
    templateUrl: './calendar.component.html'
})
export class CalendarComponent {
    
    private calendar: Calendar;

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
        this.calendar = new Calendar();
    }

    onSubmit() {
        this.getCalendar();
    }

    getCalendar() {
        let params: URLSearchParams = new URLSearchParams();
        params.set('url', this.calendar.url);

        let requestOptions = new RequestOptions();
        requestOptions.params = params;

        this.http.get(this.baseUrl + 'api/Calendar/GetCalendarEvents', requestOptions).subscribe(result => {
            this.calendar.calendarEvents = result.json() as CalendarEvent[];
        }, error => console.error(error));
    }
}


