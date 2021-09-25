export interface IProductionProcess {
  id: string;
  productId: string;
  processId: number;
  sequence: number;
  isMandatory: boolean;
  createdDate: Date;
  isActive: boolean;
}