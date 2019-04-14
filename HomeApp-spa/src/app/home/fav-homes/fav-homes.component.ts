import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Home } from 'src/app/_models/home';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';
import { HomeService } from 'src/app/_services/home.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-fav-homes',
  templateUrl: './fav-homes.component.html',
  styleUrls: ['./fav-homes.component.css']
})
export class FavHomesComponent implements OnInit {

  favHomes: Home[];
  pagination: Pagination;

  constructor(private route: ActivatedRoute, private homeService: HomeService, private as: AlertifyService) { }

  ngOnInit() {
    this.loadUsersFavHomes();
  }

  loadUsersFavHomes() {
    this.route.data.subscribe(data => {
      this.favHomes = data['homes'].result;
      this.pagination = data['homes'].pagination;
    }, err => this.as.error(err));
  }

 pageChanged(event: any): void {
   this.pagination.currentPage = event.page;
   this.getFavHomesOnPageChange();
 }

 getFavHomesOnPageChange() {
   this.homeService.getFavHomesByUser(this.pagination.currentPage, this.pagination.pageSize).subscribe((res: PaginatedResult<Home[]>) => {
     this.favHomes = res.result;
     this.pagination =  res.pagination;
   }, err => this.as.error(err));
 }

 removeHome() {
   this.getFavHomesOnPageChange();
 }

}
