export class Calendar {

    public calendarEvents: CalendarEvent[];
    public url: string;

    constructor()
    { }
}

export interface CalendarEvent {
    start: string;
    end: string;
    location: string;
    summary: string;
}