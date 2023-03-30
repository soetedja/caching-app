import { Component, Input } from '@angular/core';
import { CacheInfo } from 'src/app/models/cache-info.model';

@Component({
    selector: 'visualize',
    templateUrl: './visualize.component.html',
    styleUrls: ['./visualize.component.css']
})
export class VisualizeComponent {
    @Input()
    cacheInfo!: CacheInfo;
}
