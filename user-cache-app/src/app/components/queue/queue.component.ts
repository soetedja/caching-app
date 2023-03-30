import { Component, Input } from '@angular/core';
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
        this.dataSource = new MatTableDataSource(this.requestQueue);
    }
}
