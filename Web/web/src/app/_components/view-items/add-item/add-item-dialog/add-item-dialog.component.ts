import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ItemAdd } from 'src/app/_models/item-add';
import { Customer } from 'src/app/_models/customer';
import { Manufacturer } from 'src/app/_models/manufacturer';

export interface DialogData {
  manufacturers: Manufacturer[];
  customers: Customer[];
}

@Component({
  selector: 'app-add-item-dialog',
  templateUrl: './add-item-dialog.component.html',
  styleUrls: [
    './add-item-dialog.component.css',
    '../../dialog.component.css'
  ]
})
export class AddItemDialogComponent {

  item: ItemAdd = new ItemAdd('');
  constructor(
    public dialogRef: MatDialogRef<AddItemDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) { }

  onCancelClick(): void {
    this.dialogRef.close();
  }

}
