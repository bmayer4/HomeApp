import { Component, OnInit } from '@angular/core';
import { HomeService } from 'src/app/_services/home.service';
import { ActivatedRoute } from '@angular/router';
import { Home } from 'src/app/_models/home';

@Component({
  selector: 'app-home-list',
  templateUrl: './home-list.component.html',
  styleUrls: ['./home-list.component.css']
})
export class HomeListComponent implements OnInit {

  homes: Home[];

  constructor(private homeService: HomeService, private route: ActivatedRoute) { }

ngOnInit() {
   this.loadHomes();
}

loadHomes() {
  this.route.data.subscribe(data => {
    this.homes = data['homes'];
  }, err => console.log(err));
}

}
