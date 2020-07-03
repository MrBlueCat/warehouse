export class ItemAdd {
  name: string;
  manufacturerId: number;
  customerId?: number;

  constructor(name: string, mId: number = null, cId: number = null) {
    this.manufacturerId = mId;
    this.customerId = cId;
    this.name = name;
  }
}
