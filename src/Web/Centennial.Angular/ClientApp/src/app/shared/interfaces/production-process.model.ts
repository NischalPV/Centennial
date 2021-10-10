import { IProcess } from './process.model';
import { IProduct } from './product.model';

export interface IProductionProcess {
  id: string;
  productId: string;
  processId: number;
  sequence: number;
  isMandatory: boolean;
  createdDate: Date;
  isActive: boolean;
  product: IProduct;
  process: IProcess;
}