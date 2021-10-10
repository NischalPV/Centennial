import { IMaterial } from './material.model';
import { IProductPriceRecords } from './product-price-records.model';
import { IProductionProcess } from './production-process.model';

export interface IProduct {
  id: string;
  name: string;
  dimensions: string;
  price: number;
  uniqueIdentifier: string;
  materialId: string;
  isActive: boolean;
  createdDate: Date;
  createdBy: string;
  updatedDate: Date;
  updatedBy: string;
  material: IMaterial;
  productionProcesses: IProductionProcess[];
  productPriceRecords: IProductPriceRecords[];
}
