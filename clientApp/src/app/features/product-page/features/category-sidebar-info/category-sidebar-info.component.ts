import { Component, Input, OnInit, Output } from "@angular/core";
import { Product } from "src/app/shared/models/Product";
import { NgForm, NumberValueAccessor } from "@angular/forms";
import { MatTableDataSource } from "@angular/material/table";
import { EventEmitter } from "@angular/core";
import { Category } from "src/app/shared/models/Category";
import { FlatTreeControl, NestedTreeControl } from "@angular/cdk/tree";
import { MatTreeFlatDataSource, MatTreeFlattener, MatTreeNestedDataSource } from "@angular/material/tree";
import { CategoryService } from "./service/category.service";
import { SelectionModel } from '@angular/cdk/collections';
import { ProductComponent } from "../../product.component";
import { FilterEventService } from "../../service/filter-event.service";

export class FlatTreeNode {
  [x: string]: any;
  expandable: boolean;
  name: string;
  level: number;
  categoryId: number;
  parentCategoryId: number | null;
}


@Component({
  selector: 'XCategory-sidebar-info',
  templateUrl: './category-sidebar-info.component.html',
})

export class CategorySidebarInfoComponent implements OnInit {

  private categoryFiltersString: string[];
  private categoryNames: string[];
  public flatNodeMap = new Map<FlatTreeNode, Category>();
  public nestedNodeMap = new Map<Category, FlatTreeNode>();
  private transformer = (node: Category, level: number) => {
    const existingNode = this.nestedNodeMap.get(node);
    const flatNode = existingNode && existingNode.name === node.categoryName
      ? existingNode
      : new FlatTreeNode();
    flatNode.categoryId = node.categoryId;
    flatNode.name = node.categoryName;
    flatNode.parentCategoryId = node.parentCategoryId;
    flatNode.level = level;
    flatNode.expandable = !!node.parentCategory?.length;
    this.flatNodeMap.set(flatNode, node);
    this.nestedNodeMap.set(node, flatNode);
    return flatNode;
  }
  @Output() sendCategories = new EventEmitter<string[]>();
  public treeControl: FlatTreeControl<FlatTreeNode>;
  public treeFlattener: MatTreeFlattener<Category, FlatTreeNode>;
  public dataSource: MatTreeFlatDataSource<Category, FlatTreeNode>;
  public checklistSelection = new SelectionModel<FlatTreeNode>(true);

  constructor(private categoryService: CategoryService, private filterEvent: FilterEventService) {
    this.treeFlattener = new MatTreeFlattener(this.transformer, this.getLevel,
      this.isExpandable, this.getChildren);
    this.treeControl = new FlatTreeControl<FlatTreeNode>(this.getLevel, this.isExpandable);
    this.dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

  }

  private setFilters(): string[] {
    let selectedCategories = JSON.parse(JSON.stringify(this.checklistSelection.selected)) as FlatTreeNode[];
    const maxArrayLevel = Math.max(...selectedCategories.map(s => s.level));
    const minArrayLevel = Math.min(...selectedCategories.map(s => s.level));
    for (let i = minArrayLevel; i < maxArrayLevel; i++) {
      let rootCategories = this.checklistSelection.selected.filter(f => f.level === i);
      rootCategories.forEach((d: FlatTreeNode) => {
        if (selectedCategories.some(pc => pc.parentCategoryId == d.categoryId)) {
          let childCategories = selectedCategories.filter(pc => pc.parentCategoryId == d.categoryId);
          selectedCategories = selectedCategories.filter((el) => !childCategories.includes(el));
        }
      });

    }
    this.categoryFiltersString = new Array<string>();
    selectedCategories.forEach((d: FlatTreeNode) => {
      this.categoryFiltersString.push(d.name);
    });
    return this.categoryFiltersString;
  }

  public getLevel = (node: FlatTreeNode) => node.level;

  public getRootCategoryId = (node: FlatTreeNode) => node.categoryId;

  public isExpandable = (node: FlatTreeNode) => node.expandable;

  public getChildren = (node: Category): Category[] => node.parentCategory;

  public hasChild = (_: number, _nodeData: FlatTreeNode) => _nodeData.expandable;

  public hasNoContent = (_: number, _nodeData: FlatTreeNode) => _nodeData.name === '';

  public ngOnInit() {
    this.filterEvent.searchInvoked.subscribe(e => this.sendCategories.emit(this.setFilters()));
    this.loadCategories();
  }

  public loadCategories() {
    this.categoryService.GetCategory().subscribe((data: Category[]) => { this.dataSource.data = data });
  }

  public todoItemSelectionToggle(node: FlatTreeNode): void {
    this.checklistSelection.toggle(node);
    const descendants = this.treeControl.getDescendants(node);
    this.checklistSelection.isSelected(node)
      ? this.checklistSelection.select(...descendants)
      : this.checklistSelection.deselect(...descendants);

    descendants.forEach(child => this.checklistSelection.isSelected(child));
    this.checkAllParentsSelection(node);
    console.log(this.checklistSelection.selected);
  }

  public todoLeafItemSelectionToggle(node: FlatTreeNode): void {
    this.checklistSelection.toggle(node);
    this.checkAllParentsSelection(node);
    console.log(this.checklistSelection.selected);
  }

  public checkAllParentsSelection(node: FlatTreeNode): void {
    let parent: FlatTreeNode | null = this.getParentNode(node);
    while (parent) {
      this.checkRootNodeSelection(parent);
      parent = this.getParentNode(parent);
    }
  }

  public checkRootNodeSelection(node: FlatTreeNode): void {
    const nodeSelected = this.checklistSelection.isSelected(node);
    const descendants = this.treeControl.getDescendants(node);
    const descAllSelected = descendants.length > 0 && descendants.every(child => {
      return this.checklistSelection.isSelected(child);
    });
    if (nodeSelected && !descAllSelected) {
      this.checklistSelection.deselect(node);
    } else if (!nodeSelected && descAllSelected) {
      this.checklistSelection.select(node);
    }
  }

  public getParentNode(node: FlatTreeNode): FlatTreeNode | null {
    const currentLevel = this.getLevel(node);

    if (currentLevel < 1) {
      return null;
    }

    const startIndex = this.treeControl.dataNodes.indexOf(node) - 1;

    for (let i = startIndex; i >= 0; i--) {
      const currentNode = this.treeControl.dataNodes[i];

      if (this.getLevel(currentNode) < currentLevel) {
        return currentNode;
      }
    }
    return null;
  }
}