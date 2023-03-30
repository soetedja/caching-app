import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { User } from "../models/user.model";
import { AppService } from "../services/app.service";

@Injectable({
    providedIn: 'root'
})

export class UserFactory {
    private users: User[] = [];

    constructor(private http: HttpClient, private appService: AppService) { }

    generateUsers(numUsers: number): User[] {
        this.users = [];
        for (let i = 0; i < numUsers; i++) {
            const id = i + 1;
            const name = `User ${id}`;
            const user = new User(id, name);
            this.users.push(user);
        }
        return this.users;
    }
}