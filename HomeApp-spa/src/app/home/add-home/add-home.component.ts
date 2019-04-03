import { Component, OnInit, ViewChild } from '@angular/core';
import { HomeService } from 'src/app/_services/home.service';
import { Router } from '@angular/router';
import { Home } from 'src/app/_models/home';

@Component({
  selector: 'app-add-home',
  templateUrl: './add-home.component.html',
  styleUrls: ['./add-home.component.css']
})
export class AddHomeComponent implements OnInit {

  model: any = { renevated: false };

  constructor(private homeService: HomeService, private router: Router) { }

  ngOnInit() { }

  addHome() {
    this.homeService.addHome(this.model).subscribe((res: any) => {
      this.router.navigate(['/homes/edit/', res.id], { queryParams: { tab: 1 }});
    }, err => console.log(err));
  }

}
