import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Home } from 'src/app/_models/home';
import { AuthService } from 'src/app/_services/auth.service';
import { HomeService } from 'src/app/_services/home.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-home-item',
  templateUrl: './home-item.component.html',
  styleUrls: ['./home-item.component.css']
})
export class HomeItemComponent implements OnInit {

  @Input() home: Home;
  @Input() onFavPage = false;
  @Output() removeHomeFromList = new EventEmitter();

  constructor(private authService: AuthService, private homeService: HomeService, private as: AlertifyService) { }

  ngOnInit() {
  }

  loggedIn(): boolean {
    return this.authService.isAuth();
  }

  isFav(favUserIds: number[]): boolean {

      if (!this.loggedIn()) { return false; }

      // tslint:disable-next-line:radix
      return favUserIds.includes(parseInt(this.authService.decodedToken.nameid));
  }

  onHeartClick(event, home: Home) {
    event.stopPropagation();

    if (!this.loggedIn()) { return false; }

    // tslint:disable-next-line:radix
    const userId = parseInt(this.authService.decodedToken.nameid);
    const isFav = home.favUserIds.includes(userId);

      this.homeService.toggleHomeAsFavorite(home.id).subscribe(() => {
        if (isFav && !this.onFavPage) {
          this.home.favUserIds = this.home.favUserIds.filter(h => h !== userId);
          this.as.success('Home removed form favorites');
        } else if (isFav && this.onFavPage) {
          this.removeHomeFromList.emit();
          this.as.success('Home removed form favorites');
        } else {
          this.home.favUserIds.push(userId);
          this.as.success('Home added to favorites');
        }
      }, err => console.log(err));
  }

}
