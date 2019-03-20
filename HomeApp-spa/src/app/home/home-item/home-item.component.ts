import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-home-item',
  templateUrl: './home-item.component.html',
  styleUrls: ['./home-item.component.css']
})
export class HomeItemComponent implements OnInit {

  @Input() home;

  constructor() { }

  ngOnInit() {
  }

}
