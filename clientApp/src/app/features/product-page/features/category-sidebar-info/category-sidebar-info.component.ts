import { Component, Input, OnInit, Output } from "@angular/core";
import { Product } from "src/app/shared/models/Product";
import { NgForm, NumberValueAccessor } from "@angular/forms";
import { MatTableDataSource } from "@angular/material/table";
import { EventEmitter } from "@angular/core";
import { Category } from "src/app/shared/models/Category";
import { FlatTreeControl, NestedTreeControl } from "@angular/cdk/tree";
import { MatTreeFlatDataSource, MatTreeFlattener, MatTreeNestedDataSource } from "@angular/material/tree";
import { CategoryService } from "./service/category.service";
import {SelectionModel} from '@angular/cdk/collections';
import { ProductComponent } from "../../product.component";
import { FilterEventService } from "../../service/filter-event.service";

export class FlatTreeNode {
    [x: string]: any;
    expandable: boolean;
    name: string;
    level: number;
    categoryId:number;
    parentCategoryId: number | null;
}


@Component({
    selector: 'XCategory-sidebar-info',
    templateUrl: './category-sidebar-info.component.html',
})

export class CategorySidebarInfoComponent  implements OnInit{

    @Output() sendCategories = new EventEmitter<string[]>();
    private filteredString: string[];
    private setFilters(): string[]
    {
      let selectedCategories = JSON.parse(JSON.stringify(this.checklistSelection.selected)) as FlatTreeNode[];
      const maxArrayLevel = Math.max(...selectedCategories.map(s => s.level));
      const minArrayLevel = Math.min(...selectedCategories.map(s => s.level));
          for(let i = minArrayLevel; i < maxArrayLevel; i++){
              let rootCategories = this.checklistSelection.selected.filter(f => f.level === i);
              rootCategories.forEach((d: FlatTreeNode) => {
                    if(selectedCategories.find(pc => pc.parentCategoryId == d.categoryId)){
                       let childCategories = selectedCategories.filter(pc => pc.parentCategoryId == d.categoryId);
                       selectedCategories = selectedCategories.filter( ( el ) => !childCategories.includes( el ) );
                    }
              });
              
          }
        this.filteredString = new Array<string>();  
        selectedCategories.forEach((d: FlatTreeNode) => {
            this.filteredString.push(d.name);
        });
       return this.filteredString; 
    }

    public flatNodeMap = new Map<FlatTreeNode, Category>();

    public nestedNodeMap = new Map<Category, FlatTreeNode>();

    private categoryNames: string[];

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
    public treeControl: FlatTreeControl<FlatTreeNode>;
    public treeFlattener: MatTreeFlattener<Category, FlatTreeNode>;
    public dataSource: MatTreeFlatDataSource<Category, FlatTreeNode>;

    checklistSelection = new SelectionModel<FlatTreeNode>(true /* multiple */);
    
    constructor(private categoryService: CategoryService, private filterEvent: FilterEventService){
        this.treeFlattener = new MatTreeFlattener(this.transformer, this.getLevel,
            this.isExpandable, this.getChildren);
          this.treeControl = new FlatTreeControl<FlatTreeNode>(this.getLevel, this.isExpandable);
          this.dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);
            
    }

    getLevel = (node: FlatTreeNode) => node.level;
    
    getRootCategoryId = (node: FlatTreeNode) => node.categoryId;

    isExpandable = (node: FlatTreeNode) => node.expandable;

    getChildren = (node: Category): Category[] => node.parentCategory;

    hasChild = (_: number, _nodeData: FlatTreeNode) => _nodeData.expandable;

    hasNoContent = (_: number, _nodeData: FlatTreeNode) => _nodeData.name === '';

    ngOnInit(){
        this.filterEvent.searchInvoked.subscribe(e => this.sendCategories.emit(this.setFilters()));
        this.loadCategories();
    }

    public loadCategories(){
        this.categoryService.GetCategory().subscribe((data: Category[]) => { this.dataSource.data = data});
    }


  
  todoItemSelectionToggle(node: FlatTreeNode): void {
    this.checklistSelection.toggle(node);
    const descendants = this.treeControl.getDescendants(node);
    this.checklistSelection.isSelected(node)
      ? this.checklistSelection.select(...descendants)
      : this.checklistSelection.deselect(...descendants);

    descendants.forEach(child => this.checklistSelection.isSelected(child));
    this.checkAllParentsSelection(node);
    console.log(this.checklistSelection.selected);
  }

  todoLeafItemSelectionToggle(node: FlatTreeNode): void {
    this.checklistSelection.toggle(node);
    this.checkAllParentsSelection(node);
    console.log(this.checklistSelection.selected);
  }

 
  checkAllParentsSelection(node: FlatTreeNode): void {
    let parent: FlatTreeNode | null = this.getParentNode(node);
    while (parent) {
      this.checkRootNodeSelection(parent);
      parent = this.getParentNode(parent);
    }
  }

  checkRootNodeSelection(node: FlatTreeNode): void {
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

 
  getParentNode(node: FlatTreeNode): FlatTreeNode | null {
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