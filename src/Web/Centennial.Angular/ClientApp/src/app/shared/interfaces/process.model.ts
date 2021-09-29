export interface IProcess {
  id: number;
  name: string;
  isMandatory: boolean;
  isRemovable: boolean;
  isOutBound: boolean;
  isActive: boolean;
  createdBy: string;
  createdDate: Date;
}
