import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MatFormFieldModule, MatInputModule, MatOptionModule, MatSelectModule, MatSortModule, MatMenuModule, MatIconModule } from '@angular/material';
import { MatTableModule, MatPaginatorModule } from '@angular/material';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ViewItemsComponent } from './view-items.component';
import { AddItemComponent } from './add-item/add-item.component';
import { DeleteItemComponent } from './delete-item/delete-item.component';
import { UpdateItemComponent } from './update-item/update-item.component';
import { WaitAnimComponent } from '../wait-anim/wait-anim.component';
import { UpdateItemDialogComponent } from './update-item/update-item-dialog/update-item-dialog.component';
import { AddItemDialogComponent } from './add-item/add-item-dialog/add-item-dialog.component';

const modules = [
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatMenuModule,
    MatOptionModule,
    MatSelectModule,
    MatSortModule, 
];

@NgModule({
    imports: [
        HttpClientModule,
        FormsModule,
        MatDialogModule,
        BrowserAnimationsModule,
        MatTableModule,
        MatPaginatorModule,
        ...modules
    ],
    declarations: [
        ViewItemsComponent,
        AddItemComponent,
        DeleteItemComponent,
        UpdateItemComponent,
        UpdateItemDialogComponent,
        AddItemDialogComponent,
        WaitAnimComponent,
    ],
    entryComponents: [
        UpdateItemDialogComponent,
        AddItemDialogComponent,
    ],
    providers: [
        { provide: MatDialogRef, useValue: {} },
        { provide: MAT_DIALOG_DATA, useValue: [] },
    ],
    bootstrap: [],
    exports: [...modules, WaitAnimComponent]
})
export class ViewItemsModule { }