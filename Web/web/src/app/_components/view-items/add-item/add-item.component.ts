import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Manufacturer } from 'src/app/_models/manufacturer';
import { Customer } from 'src/app/_models/customer';
import { ItemsService } from 'src/app/_services/items.service';
import { MatDialog } from '@angular/material';
import { ItemAdd } from 'src/app/_models/item-add';
import { AddItemDialogComponent } from './add-item-dialog/add-item-dialog.component';


@Component({
  selector: 'app-add-item',
  templateUrl: './add-item.component.html',
  styleUrls: ['./add-item.component.css'],
})

export class AddItemComponent implements OnInit {

  @Output() onAdd = new EventEmitter<ItemAdd>();
  @Output() onError = new EventEmitter<any>();

  manufacturers: Manufacturer[] = [];
  customers: Customer[] = [];

  constructor(private http: ItemsService, public dialog: MatDialog) { }

  async ngOnInit() {
    this.manufacturers = await this.http.getManufacturers().catch(error => this.onError.emit(error));
    this.customers = await this.http.getCustomers().catch(error => this.onError.emit(error));
  }
  
  openAddDialog() {
    const dialogRef = this.dialog.open(AddItemDialogComponent, {
      width: '300px',
      data: { manufacturers: this.manufacturers, customers: this.customers },
    });

    dialogRef.afterClosed().subscribe((result: ItemAdd) => {
      if (result) {
        this.onAdd.emit(result);
      }
    });
  }
}