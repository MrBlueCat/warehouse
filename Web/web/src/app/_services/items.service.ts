import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Item } from '../_models/item';
import { ItemUpdate } from '../_models/item-update';
import { ItemAdd } from '../_models/item-add';

@Injectable({
  providedIn: 'root'
})
export class ItemsService {

  constructor(private http: HttpClient) { }

  async getItems() {
    return await this.http.get<Promise<any>>(`${environment.warehouseApiRoot}/items`).toPromise();
  }

  async deleteItem(item: Item) {
    return await this.http.delete<Promise<any>>(`${environment.warehouseApiRoot}/items/` + item.id).toPromise();
  }

  async updateItem(item: ItemUpdate, id: number) {
    return await this.http.put<Promise<any>>(`${environment.warehouseApiRoot}/items/` + id, item).toPromise();
  }

  async addItem(item: ItemAdd) {
    return await this.http.post<Promise<any>>(`${environment.warehouseApiRoot}/items/`, item).toPromise();
  }

  async getManufacturers() {
    return await this.http.get<Promise<any>>(`${environment.warehouseApiRoot}/manufacturers`).toPromise();
  }

  async getCustomers() {
    return await this.http.get<Promise<any>>(`${environment.warehouseApiRoot}/customers`).toPromise();
  }
}
