import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HomeService } from 'src/app/_services/home.service';
import { TabsetComponent } from 'ngx-bootstrap';
import { AuthService } from 'src/app/_services/auth.service';
import { Home } from 'src/app/_models/home';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-edit-home',
  templateUrl: './edit-home.component.html',
  styleUrls: ['./edit-home.component.css']
})
export class EditHomeComponent implements OnInit {

  home: Home;
  @ViewChild('addHomeTabs') addHomeTabs: TabsetComponent;

  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private homeService: HomeService,
    private router: Router,
    private as: AlertifyService
    ) { }

  ngOnInit() {

    this.route.queryParams.subscribe(params => {
      const selectedTab = params['tab'];
      if (selectedTab && selectedTab === '1') {
        this.addHomeTabs.tabs[selectedTab].active = true;
      }
    });

    this.loadHome();
  }

  loadHome() {
    this.route.data.subscribe(data => {
      this.home = data['home'];
      if (this.home.userId.toString() !== this.authService.decodedToken.nameid) {
        this.router.navigate(['/']);
      }
    }, err => this.as.error(err));
  }

  updateHome() {
    this.homeService.updateHome(this.home).subscribe(res => {
      this.as.success('Successfully updated');
    }, err => this.as.error(err));
  }

}
