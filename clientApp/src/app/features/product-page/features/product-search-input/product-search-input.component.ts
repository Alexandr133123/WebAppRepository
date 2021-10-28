import { Component, OnInit, Output} from '@angular/core';
import { EventEmitter } from '@angular/core';
import { FilterEventService } from '../../service/filter-event.service';

@Component({
  selector: 'product-search-input',
  templateUrl: './product-search-input.component.html',
})

export class ProductSearchInputComponent  implements OnInit{

  constructor( private eventService: FilterEventService){}
  @Output() sendInputString = new EventEmitter<string>();
  public productInputString = "";
  
  public ngOnInit(){
    
  }
  public startSearch(){
    this.sendInputString.emit(this.productInputString);
    this.eventService.searchInvoked.next();
  }
 

}
