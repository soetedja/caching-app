import { Component } from '@angular/core';
import { UserFactory } from './factories/user.factory';
import { CacheInfo } from './models/cache-info.model';
import { User } from './models/user.model';
import { AppService } from './services/app.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'user-cache-app';
  numUsers: number = 100;
  autoUpdate: boolean = false;
  users: User[] = [];
  cacheInfo: CacheInfo = new CacheInfo();
  MAX_NUMBER: number = 100000;
  INTERVAL: number = 0;
  requestQueue: any[] = [];
  allocations = new Array(100).fill(0);
  batchId: number = 0;

  constructor(private userFactory: UserFactory, private appService: AppService) { }

  generateUsers() {
    this.requestQueue = [];
    this.batchId++;
    this.users = this.userFactory.generateUsers(this.numUsers);

    let interval = 0;
    this.users.forEach(user => {
      let randomInterval = (Math.floor(Math.random() * 5) + 1) * 1000;
      if (this.INTERVAL > 0) {
        interval = interval + this.INTERVAL * 1000;
        randomInterval = this.INTERVAL * 1000;
      } else {
        interval = interval + randomInterval;
      }
      const randomId = Math.floor(Math.random() * this.MAX_NUMBER);
      this.requestQueue.push({ batch: this.batchId, userId: user.id, number: randomId, executed: false, interval: randomInterval });
      setTimeout(() => {
        this.appService.getModNumber(randomId).subscribe(res => {
          this.requestQueue.find(s => s.userId == user.id && s.batch == this.batchId).executed = true;
          if (this.autoUpdate) {
            this.queryCache();
          }
        });
      }, interval);
    });
  }

  get isProcessing() {
    return this.requestQueue.some((request) => !request.executed);
  }

  queryCache() {
    this.appService.getQueryCache().subscribe(res => {
      this.cacheInfo = res;
    });
  }

  clearCache() {
    this.appService.clearCache().subscribe(res => {
      this.queryCache();
    });
  }
}
