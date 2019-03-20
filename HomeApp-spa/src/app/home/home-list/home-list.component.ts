import { Component, OnInit } from '@angular/core';
import { HomeService } from 'src/app/_services/home.service';

@Component({
  selector: 'app-home-list',
  templateUrl: './home-list.component.html',
  styleUrls: ['./home-list.component.css']
})
export class HomeListComponent implements OnInit {

  homes;

  constructor(private homeService: HomeService) { }

ngOnInit() {
    this.homeService.getHomes().subscribe(homes => {
      this.homes = homes;
    }, err => {
      console.log(err);
    });
}

}
