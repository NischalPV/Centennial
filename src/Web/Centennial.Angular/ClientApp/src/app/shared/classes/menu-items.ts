import { Injectable } from '@angular/core';
import { MenuItem, MenuSection } from '../interfaces/menu-item';

const MASTERDATA = 'masterdata';


export const SECTIONS: { [key: string]: MenuSection } = {
  [MASTERDATA]: {
    name: 'masterdata',
    summary: ''
  }
}


const DOCS: { [key: string]: MenuItem[] } = {
  [MASTERDATA]: [
    {
      id: 'customers',
      name: 'Customers',
      summary: '',
      icon: '',
      section: 'masterdata'
    },
    {
      id: 'materials',
      name: 'Materials',
      summary: '',
      icon: '',
      section: 'masterdata'
    },
    {
      id: 'processes',
      name: 'Processes',
      summary: '',
      icon: '',
      section: 'masterdata'
    },
    {
      id: 'products',
      name: 'Products',
      summary: '',
      icon: '',
      section: 'masterdata'
    },
    {
      id: 'rawmaterials',
      name: 'Raw Materials',
      summary: '',
      icon: '',
      section: 'masterdata'
    },
    {
      id: 'employees',
      name: 'Employees',
      summary: '',
      icon: '',
      section: 'masterdata'
    },
  ]
}

const ALL_MASTERDATA = processDocs(DOCS[MASTERDATA]);
const ALL_MENU = [...ALL_MASTERDATA];

@Injectable()
export class MenuItems {

  getItems(): MenuItem[] {
    return ALL_MASTERDATA;
  }

  getItemById(id: string, section: string): MenuItem | undefined {
    const sectionLookup = section === 'cdk' ? 'cdk' : 'material';
    return ALL_MENU.find(doc => doc.id === id);
  }
}

function processDocs(docs: MenuItem[]): MenuItem[] {
  return docs.sort((a, b) => a.name.localeCompare(b.name, 'en'));
}

