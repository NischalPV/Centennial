export interface MenuItem {
  /** Id of the doc item. Used in the URL for linking to the doc. */
  id: string;
  /** Display name of the menu item. */
  name: string;
  /** Short summary of the menu item. */
  summary?: string;
  /** Icon of the menu item. */
  icon?: string;
  section: string;
}

export interface MenuSection {
  name: string;
  summary: string;
}