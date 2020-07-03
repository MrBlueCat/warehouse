import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Item } from 'src/app/_models/item';
import { MatDialog } from '@angular/material/dialog';
import { UpdateItemDialogComponent } from './update-item-dialog/update-item-dialog.component';

@Component({
  selector: 'app-update-item',
  templateUrl: './update-item.component.html',
  styleUrls: ['./update-item.component.css'],
})
export class UpdateItemComponent {

  @Input() item: Item
  @Output() onUpdate = new EventEmitter<any>();

  constructor(public dialog: MatDialog) { }

  openUpdateDialog(): void {
    const dialogRef = this.dialog.open(UpdateItemDialogComponent, {
      width: '300px',
      data: { itemName: this.item.name }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result && result !== undefined) {
        let newItem = { name: result, manufacturerId: this.item.manufacturer.id, customerId: this.item.customer?this.item.customer.id:null };
        this.onUpdate.emit({ oldItem: this.item, newItem: newItem });
      }
    });
  }
}
