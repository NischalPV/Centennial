import { IMaterial } from './material.model';

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
}
