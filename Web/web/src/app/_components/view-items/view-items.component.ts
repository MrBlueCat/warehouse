import { Component, OnInit, ViewChild } from '@angular/core';
import { Item } from '../../_models/item';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { MatTableDataSource, MatPaginator, MatMenuTrigger } from '@angular/material';
import { ItemsService } from 'src/app/_services/items.service';
import { WaitAnimComponent } from '../wait-anim/wait-anim.component';
import { ItemUpdate } from 'src/app/_models/item-update';
import { ItemAdd } from 'src/app/_models/item-add';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-view-items',
  templateUrl: './view-items.component.html',
  styleUrls: ['./view-items.component.css'],
})

export class ViewItemsComponent implements OnInit {

  coloumnWidth: number = 10;
  items: any[] = [];
  error: string;
  isGrouped: boolean = false;

  displayedColumns: string[] = ['name', 'manufacturer', 'customer', 'date'];
  groupByColumns: string[] = [];
  dataSource = new MatTableDataSource<any | Group>(this.items);

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(WaitAnimComponent, { static: true }) processAnim: WaitAnimComponent;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatMenuTrigger, { static: false }) menuTrigger: MatMenuTrigger;

  constructor(private itemsService: ItemsService, private authService: AuthenticationService) {
    this.groupByColumns = ['name'];
  }

  ngOnInit() {
    if (this.authService.isAdmin) {
      this.displayedColumns.push('change');
    }

    this.coloumnWidth = 100 / this.displayedColumns.length;
    this.dataSource.sortingDataAccessor = (item, property) => {
      switch (property) {
        case 'fromDate': return new Date(item.fromDate);
        default: return item[property];
      }
    }
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.readItems();
    this.dataSource.data = this.addGroups(this.items, this.groupByColumns);
    this.dataSource.filterPredicate = this.customFilterPredicate.bind(this);
    this.dataSource.filter = performance.now().toString();
  }

  customFilterPredicate(data: any | Group, filter: string): boolean {
    return (data instanceof Group) ? data.visible : this.getDataRowVisible(data);
  }

  async onItemUpdate(updateModel: { oldItem: Item, newItem: ItemUpdate }) {
    this.processAnim.isProcessing = true;

    let updatedItem = await this.itemsService.updateItem(updateModel.newItem, updateModel.oldItem.id).catch(er => this.errorHandler(er));
    if (updatedItem) {
      this.updateItemInTable(updateModel.oldItem, updatedItem);
    }

    this.processAnim.isProcessing = false;
  }

  async onItemDelete(item: Item) {
    this.processAnim.isProcessing = true;

    await this.itemsService.deleteItem(item).then(
      res => this.deleteFromTable(item)).catch(er => this.errorHandler(er));

    this.processAnim.isProcessing = false;
  }

  async onItemAdd(item: ItemAdd) {
    this.processAnim.isProcessing = true;

    let newItem = await this.itemsService.addItem(item).catch(er => this.errorHandler(er));
    if (newItem) {
      this.items.push(newItem);
      this.group(null, this.isGrouped)
    }

    this.processAnim.isProcessing = false;
  }

  async readItems() {
    this.processAnim.isProcessing = true;

    this.items = await this.itemsService.getItems().catch(er => this.errorHandler(er));

    this.processAnim.isProcessing = false;
    this.dataSource.data = this.items;
  }

  errorHandler(error: any) {
    this.error = error.message;
  }

  private deleteFromTable(item: Item) {
    let index = this.items.indexOf(item);
    if (index != -1)
      this.items.splice(this.items.indexOf(item), 1)

    this.group(null, this.isGrouped)
  }

  private updateItemInTable(oldItem: Item, newItem: Item) {
    let index = this.items.indexOf(oldItem);
    if (index != -1)
      this.items.splice(index, 1, newItem);
    this.group(null, this.isGrouped)
  }

  group(event, group: boolean) {
    if (event) {
      event.stopPropagation();
    }
    this.menuTrigger.closeMenu();

    if (this.isGrouped == group)
      return;

    this.isGrouped = group;
    this.checkGroupByColumn("name", group);
    this.dataSource.data = this.addGroups(this.items, this.groupByColumns);
    this.dataSource.filter = performance.now().toString();
  }

  isGroup(index, item): boolean {
    return item.level;
  }

  getDataRowVisible(data: any): boolean {
    const groupRows = this.dataSource.data.filter(
      row => {
        if (!(row instanceof Group)) {
          return false;
        }
        let match = true;
        this.groupByColumns.forEach(column => {
          if (!row[column] || !data[column] || row[column] !== data[column]) {
            match = false;
          }
        });
        return match;
      }
    );

    if (groupRows.length === 0) {
      return true;
    }
    const parent = groupRows[0] as Group;
    return parent.visible && parent.expanded;
  }

  addGroups(data: any[], groupByColumns: string[]): any[] {
    const rootGroup = new Group();
    rootGroup.expanded = true;
    return this.getSublevel(data, 0, groupByColumns, rootGroup);
  }

  checkGroupByColumn(field, add) {
    let found = null;
    for (const column of this.groupByColumns) {
      if (column === field) {
        found = this.groupByColumns.indexOf(column, 0);
      }
    }
    if (found != null && found >= 0) {
      if (!add) {
        this.groupByColumns.splice(found, 1);
      }
    } else {
      if (add) {
        this.groupByColumns.push(field);
      }
    }
  }

  getSublevel(data: any[], level: number, groupByColumns: string[], parent: Group): any[] {
    if (level >= groupByColumns.length) {
      return data;
    }
    const groups = this.uniqueBy(
      data.map(
        row => {
          const result = new Group();
          result.level = level + 1;
          result.parent = parent;
          for (let i = 0; i <= level; i++) {
            result[groupByColumns[i]] = row[groupByColumns[i]];
          }
          return result;
        }
      ),
      JSON.stringify);

    const currentColumn = groupByColumns[level];
    let subGroups = [];
    groups.forEach(group => {
      const rowsInGroup = data.filter(row => group[currentColumn] === row[currentColumn]);
      group.totalCounts = rowsInGroup.length;
      const subGroup = this.getSublevel(rowsInGroup, level + 1, groupByColumns, group);
      subGroup.unshift(group);
      subGroups = subGroups.concat(subGroup);
    });
    return subGroups;
  }

  uniqueBy(a, key) {
    const seen = {};
    return a.filter((item) => {
      const k = key(item);
      return seen.hasOwnProperty(k) ? false : (seen[k] = true);
    });
  }
}

export class Group {
  level = 0;
  parent: Group;
  expanded = true;
  totalCounts = 0;
  get visible(): boolean {
    return !this.parent || (this.parent.visible && this.parent.expanded);
  }
}

