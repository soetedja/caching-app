import { Component, Input, SimpleChanges } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

@Component({
    selector: 'queue',
    templateUrl: './queue.component.html',
    styleUrls: ['./queue.component.css']
})
export class QueueComponent {
    @Input() requestQueue!: any[];
    dataSource = new MatTableDataSource(this.requestQueue);
    
    ngOnInit(){
    }

    ngOnChanges(changes: SimpleChanges) {
        if (changes['requestQueue'].currentValue !== changes['requestQueue'].previousValue)
        {
            this.dataSource = new MatTableDataSource(this.requestQueue);
        }
    }
}
