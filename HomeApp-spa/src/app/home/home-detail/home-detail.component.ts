import { Component, OnInit } from '@angular/core';
import { HomeService } from 'src/app/_services/home.service';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from 'ngx-gallery';

@Component({
  selector: 'app-home-detail',
  templateUrl: './home-detail.component.html',
  styleUrls: ['./home-detail.component.css']
})
export class HomeDetailComponent implements OnInit {

  home;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  constructor(private homeService: HomeService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.loadHome();

    this.galleryOptions = [
      {
          width: '100%',
          height: '440px',
          imagePercent: 100,
          thumbnailsColumns: 4,
          imageAnimation: NgxGalleryAnimation.Slide,
          previewCloseOnClick: true,
          previewAnimation: false
      },
      {
        breakpoint: 767.98,
        height: '340px',
      },
      {
        breakpoint: 575.98,
        thumbnails: false,
        height: '300px',
        preview: false
      }
    ];

      this.galleryImages = this.getImages();
  }

  loadHome() {
    this.route.data.subscribe(data => {
      this.home = data['home'];
    }, err => {
      console.log(err);
    });
  }

  getImages() {
      const imageUrls = [];
      for (const photo of this.home.photos) {
        imageUrls.push({
          small: photo.url,
          medium: photo.url,
          big: photo.url
        });
      }
      return imageUrls;
    }

}
