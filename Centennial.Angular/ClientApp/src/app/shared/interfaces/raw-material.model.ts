import { IMaterial } from './material.model';

export interface IRawMaterial {
  id: string;
  name: string;
  size: string;
  materialId: string;
  createdDate: Date;
  material: IMaterial;
}
