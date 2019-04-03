import { Component, OnInit, Input } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { environment } from '../../environments/environment';
import { AuthService } from '../_services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { PhotoService } from '../_services/photo.service';
import { Photo } from '../_models/photo';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-photo-upload',
  templateUrl: './photo-upload.component.html',
  styleUrls: ['./photo-upload.component.css']
})
export class PhotoUploadComponent implements OnInit {

  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  homeId: number;
  @Input() photos: Photo[];

  constructor(
    private authService: AuthService,
    private photoService: PhotoService,
    private route: ActivatedRoute,
    private as: AlertifyService
    ) { }

  ngOnInit() {
    this.homeId = +this.route.snapshot.params['id'];  // can't use input for home id, is null this is init
    this.initializeUploader();
  }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
     this.uploader = new FileUploader({
      url: this.baseUrl + 'homes/' + this.homeId + '/photos',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = (file) => { file.withCredentials = false; };  // fixes cors issue

    this.uploader.onSuccessItem = (item, response, status, header) => {
      if (response) {
        console.log('res', response);
        const res: any = JSON.parse(response);
        this.photos.push(res);
      }
    };
  }

  onSetCover(photo: Photo) {
    this.photoService.setCoverPhoto(this.homeId, photo.id).subscribe(res => {
      const currentCover = this.photos.find(p => p.isCover);
      currentCover.isCover = false;
      photo.isCover = true; // set the chosen photo to be cover
     }, err => console.log(err));
  }

  onDelete(id: number) {
    this.as.confirm('Are you sure you want to delete this photo?', () => {
      this.photoService.deletePhoto(this.homeId, id).subscribe(res => {
        this.photos = this.photos.filter(p => p.id !== id);
        this.as.success('Photo deleted');
      }, err => console.log(err));
    });
  }

}
