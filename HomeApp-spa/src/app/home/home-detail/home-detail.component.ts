import { Component, OnInit } from '@angular/core';
import { HomeService } from 'src/app/_services/home.service';
import { ActivatedRoute } from '@angular/router';
import { isType } from '@angular/core/src/type';
import { isNumber } from 'util';

@Component({
  selector: 'app-home-detail',
  templateUrl: './home-detail.component.html',
  styleUrls: ['./home-detail.component.css']
})
export class HomeDetailComponent implements OnInit {

  home;

  constructor(private homeService: HomeService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.loadHome();
  }

  loadHome() {
    this.homeService.getHome(+this.route.snapshot.params['id']).subscribe(home => {
      this.home = home;
      console.log(home);
    }, err => {
      console.log(err);
    });
  }

}
