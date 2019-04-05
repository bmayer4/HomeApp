import { Component, OnInit } from '@angular/core';
import { HomeService } from 'src/app/_services/home.service';
import { ActivatedRoute } from '@angular/router';
import { Home } from 'src/app/_models/home';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';

@Component({
  selector: 'app-home-list',
  templateUrl: './home-list.component.html',
  styleUrls: ['./home-list.component.css']
})
export class HomeListComponent implements OnInit {

  homes: Home[];
  pagination: Pagination;

  constructor(private homeService: HomeService, private route: ActivatedRoute) { }

ngOnInit() {
   this.loadHomes();
}

pageChanged(event: any): void {
  this.pagination.currentPage = event.page;
  this.getHomesOnPageChange();
}

loadHomes() {
  this.route.data.subscribe(data => {
    this.homes = data['homes'].result;
    this.pagination = data['homes'].pagination;
  }, err => console.log(err));
}

getHomesOnPageChange() {
  this.homeService.getHomes(this.pagination.currentPage, this.pagination.pageSize).subscribe((res: PaginatedResult<Home[]>) => {
    this.homes = res.result;
    this.pagination =  res.pagination;
  }, err => console.log(err));
}

}
