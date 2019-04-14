import { Component, OnInit } from '@angular/core';
import { HomeService } from 'src/app/_services/home.service';
import { ActivatedRoute } from '@angular/router';
import { Home } from 'src/app/_models/home';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-home-list',
  templateUrl: './home-list.component.html',
  styleUrls: ['./home-list.component.css']
})
export class HomeListComponent implements OnInit {

  homes: Home[];
  pagination: Pagination;
  minPrice = [{ value: 100000, display: '100k'},
    { value: 200000, display: '200k'},
    { value: 300000, display: '300k'},
    { value: 400000, display: '400k'},
    { value: 500000, display: '500k'}];
  maxPrice = [{ value: 100000, display: '100k'},
    { value: 200000, display: '200k'},
    { value: 300000, display: '300k'},
    { value: 400000, display: '400k'},
    { value: 500000, display: '500k'}];
  bed = [1, 2, 3, 4, 5];
  bath = [1, 2, 3, 4, 5];
  homeParams: any  = {};

  constructor(private homeService: HomeService, private route: ActivatedRoute, private as: AlertifyService) { }

ngOnInit() {
   this.loadHomesOnInit();
}

loadHomesOnInit() {
  this.route.data.subscribe(data => {
    this.homes = data['homes'].result;
    this.pagination = data['homes'].pagination;
  }, err => this.as.error(err));
}

pageChanged(event: any): void {
  this.pagination.currentPage = event.page;
  this.getHomes();
}

getHomes() {
  this.homeService.getHomes(this.pagination.currentPage, this.pagination.pageSize, this.homeParams)
  .subscribe((res: PaginatedResult<Home[]>) => {
    this.homes = res.result;
    this.pagination =  res.pagination;
  }, err => this.as.error(err));
}

orderBy(orderBy: string): void {
  this.homeParams.orderBy = orderBy;
  this.getHomes();
}

resetFilters() {
  this.homeParams = {};
  this.getHomes();
}

}
