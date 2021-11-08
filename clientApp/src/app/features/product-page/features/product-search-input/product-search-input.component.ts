import { Component, OnInit, Output } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { EventService } from '../../service/event.service';

@Component({
  selector: 'product-search-input',
  templateUrl: './product-search-input.component.html',
  styleUrls: ['./product-search-input.component.css']
})

export class ProductSearchInputComponent implements OnInit {

  @Output() sendInputString = new EventEmitter<string>();
  public productInputString = "";
  constructor(private eventService: EventService) { }
  public ngOnInit() { }
  public startSearch() {
    this.sendInputString.emit(this.productInputString);
    this.eventService.searchInvoked.next();
  }
}
