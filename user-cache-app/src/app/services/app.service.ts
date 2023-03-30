import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AppService {
    private apiUrl = 'https://localhost:7168';

    constructor(private http: HttpClient) { }

    getModNumber(random: number): Observable<any> {
        // return new Observable<number>((observer) => {
        //     let count = random%1234;
        //     setTimeout(() => {
        //         observer.next(count);
        //     }, 1000);
        // });
        return this.http.post<any>(`${this.apiUrl}/WebServer`, {"number": random});
    }

    getQueryCache(): Observable<any> {
        return this.http.get<any>(`${this.apiUrl}/Cache`);
    }


}