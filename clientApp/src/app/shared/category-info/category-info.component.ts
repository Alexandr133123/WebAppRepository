import { Component, Input, OnInit, Output } from "@angular/core";
import { EventEmitter } from "@angular/core";
import { Category } from "src/app/shared/models/Category";
import { FlatTreeControl } from "@angular/cdk/tree";
import { MatTreeFlatDataSource, MatTreeFlattener } from "@angular/material/tree";
import { CategoryService } from "./service/category.service";
import { SelectionModel } from '@angular/cdk/collections';
import { EventService } from "../../features/product-page/service/event.service";
import { first } from "rxjs/operators";
import { MatTree } from "@angular/material/tree";

export class FlatTreeNode {
  expandable: boolean;
  name: string;
  level: number;
  categoryId: number;
  parentCategoryId: number | null;
}

@Component({
  selector: 'XCategory-info',
  templateUrl: './category-info.component.html',
})
export class CategoryInfoComponent implements OnInit {
  private categoryFiltersString: string[];
  private selectedEditCategories: Category[];
  public flatNodeMap = new Map<FlatTreeNode, Category>();
  public categoriesLoaded = false;
  public nestedNodeMap = new Map<Category, FlatTreeNode>();
  public treeControl: FlatTreeControl<FlatTreeNode>;
  public treeFlattener: MatTreeFlattener<Category, FlatTreeNode>;
  public dataSource: MatTreeFlatDataSource<Category, FlatTreeNode>;
  public checklistSelection = new SelectionModel<FlatTreeNode>(true);

  @Input() categories: Category[];
  @Output() sendCategories = new EventEmitter<string[]>();
  @Output() sendEditCategories = new EventEmitter<Category[]>();

  constructor(private categoryService: CategoryService, private eventService: EventService) {
    this.selectedEditCategories = new Array<Category>();
    this.treeFlattener = new MatTreeFlattener(this.transformer.bind(this), this.getLevel,
      this.isExpandable, this.getChildren);
    this.treeControl = new FlatTreeControl<FlatTreeNode>(this.getLevel, this.isExpandable);
    this.dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);
  }

  public ngOnInit() {
    this.categoryService.GetCategory()
    .pipe(first())
    .subscribe((data: Category[]) => {
      this.dataSource.data = data;
      this.categoriesLoaded = true;
      this.initializeTreeNodes();
    });
    this.eventService.searchInvoked.subscribe(e => this.sendCategories.emit(this.setFilters()));
  }
  public sendSelectedCategoriesId(){
    var categoriesSelected = this.checklistSelection.selected.map(n => n.categoryId.toString()); 
    this.sendCategories.emit(categoriesSelected);
  }
  private sendEditedCategories() {
    this.sendEditCategories.emit(this.selectedEditCategories);
  }

  public descendantsAllSelected(node: FlatTreeNode): boolean {
    const descendants = this.treeControl.getDescendants(node);
    const isSelectedChild = descendants.some((child: FlatTreeNode) => {
      return this.checklistSelection.selected.find(c => c == node);
    });
    const descAllSelected = descendants.length > 0 && isSelectedChild;
    return descAllSelected;
  }

  public todoItemSelectionToggle(node: FlatTreeNode): void {
    this.checklistSelection.toggle(node);
      this.categoryChanges(node);
  

    const descendants = this.treeControl.getDescendants(node);
    if(this.checklistSelection.isSelected(node)){
      this.checklistSelection.select(...descendants)
    }
    descendants.forEach(child => this.checklistSelection.isSelected(child));
  }

  public todoLeafItemSelectionToggle(node: FlatTreeNode): void {
    this.checklistSelection.toggle(node);
      this.categoryChanges(node);
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

  public hasChild = (_: number, _nodeData: FlatTreeNode) => _nodeData.expandable;

  private getLevel = (node: FlatTreeNode) => node.level;
  private isExpandable = (node: FlatTreeNode) => node.expandable;
  private getChildren = (node: Category): Category[] => node.children;

  //public hasNoContent = (_: number, _nodeData: FlatTreeNode) => _nodeData.name === '';

  private findCategoryByName(node: FlatTreeNode, categories: Category[]) {
    var selectedCategory = categories.find(c => c.categoryName == node.name);

    categories.forEach((c: Category) => {
      if (c.children !== undefined && c.children.length > 0 && selectedCategory == undefined) {
        selectedCategory = this.findCategoryByName(node, c.children);
      }

    });
    return selectedCategory;
  }

  private categoryChanges(node: FlatTreeNode) {
    var selectedEditCategory = this.findCategoryByName(node, this.dataSource.data);

    if (this.checklistSelection.isSelected(node) && selectedEditCategory !== undefined) {
      this.addCategoryChanges(selectedEditCategory);
    } else if (selectedEditCategory) {
      this.deleteCategoryChanges(selectedEditCategory);
    }
  }

  private addCategoryChanges(selectedCategory: Category) {
    this.selectedEditCategories.push(selectedCategory);
    this.sendEditedCategories();
  }
  private deleteCategoryChanges(selectedCategory: Category) {
    this.selectedEditCategories = this.selectedEditCategories.filter((c: Category) => !(selectedCategory.categoryName === c.categoryName));
    this.sendEditedCategories();
  }

  private transformer(node: Category, level: number):FlatTreeNode {
    const existingNode = this.nestedNodeMap.get(node);
    const flatNode = existingNode && existingNode.name === node.categoryName
      ? existingNode
      : new FlatTreeNode();
    flatNode.categoryId = node.categoryId;
    flatNode.name = node.categoryName;
    flatNode.parentCategoryId = node.parentCategoryId;
    flatNode.level = level;
    flatNode.expandable = !!node.children?.length;
    this.flatNodeMap.set(flatNode, node);
    this.nestedNodeMap.set(node, flatNode);
    return flatNode;
  }

  private setFilters(): string[] {
    let selectedCategories = JSON.parse(JSON.stringify(this.checklistSelection.selected)) as FlatTreeNode[];
    const maxArrayLevel = Math.max(...selectedCategories.map(s => s.level));
    const minArrayLevel = Math.min(...selectedCategories.map(s => s.level));

    for (let i = minArrayLevel; i < maxArrayLevel; i++) {
      let rootCategories = this.checklistSelection.selected.filter(f => f.level === i);

      rootCategories.forEach((d: FlatTreeNode) => {
       
            if(d.expandable === true){
              var descendants = this.treeControl.getDescendants(d);
              var descAllSelected = descendants.length > 0 && descendants.every(child => {
                return this.checklistSelection.isSelected(child);
              });
      
              if (descAllSelected) {
                let childCategories = selectedCategories.filter(pc => pc.parentCategoryId == d.categoryId);
                selectedCategories = selectedCategories.filter((el) => !childCategories.includes(el));
              }else{
                selectedCategories = selectedCategories.filter((el: FlatTreeNode) => !(el.categoryId === d.categoryId));
                this.isAllChildNodesSelected(d, selectedCategories);   
              }
            }
          
      });
    }

    this.categoryFiltersString = new Array<string>();
    console.log(selectedCategories);
    selectedCategories.forEach((d: FlatTreeNode) => {
      this.categoryFiltersString.push(d.name);
    });
    return this.categoryFiltersString;
  }

  private isAllChildNodesSelected(node: FlatTreeNode, selectedCategories: FlatTreeNode[]): void {

    if (selectedCategories.some(pc => pc.parentCategoryId === node.categoryId)) {
      let childCategories = selectedCategories.filter(pc => pc.parentCategoryId == node.categoryId);
      childCategories.forEach((n: FlatTreeNode) => {
        if(n.expandable === true){
          var descendants = this.treeControl.getDescendants(n);
          var descAllSelected = descendants.length > 0 && descendants.every(child => {
            return this.checklistSelection.isSelected(child);
          });
  
          if (descAllSelected) {
            let childCategories = selectedCategories.filter(pc => pc.parentCategoryId == n.categoryId);
            selectedCategories = selectedCategories.filter((el) => !childCategories.includes(el));
          }else{
            selectedCategories = selectedCategories.filter((el: FlatTreeNode) => !(el.categoryId === n.categoryId));
            this.isAllChildNodesSelected(n, selectedCategories);   
          }
        }
      });
    }
  }


  private initializeTreeNodes(): void {
    if (this.categories && this.categories.length > 0) {
      this.selectedEditCategories = [...this.categories];
      this.treeControl.expandAll();
      this.treeControl.dataNodes.forEach((n: FlatTreeNode) => {
        this.categories.forEach((c: Category) => {
          if (n.name === c.categoryName) {
            this.checklistSelection.select(n);
          }
        });
      });

    } else {
      this.selectedEditCategories = new Array<Category>();
    }
  }
}