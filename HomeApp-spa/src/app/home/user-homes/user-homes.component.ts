import { Component, OnInit } from '@angular/core';
import { HomeService } from 'src/app/_services/home.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-user-homes',
  templateUrl: './user-homes.component.html',
  styleUrls: ['./user-homes.component.css']
})
export class UserHomesComponent implements OnInit {

  userHomes;

  constructor(private homeService: HomeService, private as: AlertifyService) { }

  ngOnInit() {
    this.homeService.getHomesByUser().subscribe(res => {
      this.userHomes = res;
    }, err => console.log(err));
  }

  deleteHome(homeId: number) {
    this.as.confirm('Are you sure you want to delete this home?', () => {
      this.homeService.deleteHome(homeId).subscribe(res => {
        this.as.success('Home deleted');
        this.userHomes = this.userHomes.filter(h => h.id !== homeId);
      }, err => console.log(err));
    });
   }

}
