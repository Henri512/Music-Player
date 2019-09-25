import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Time } from '@angular/common';

@Component({
  selector: 'app-display-album',
  templateUrl: './display-album.component.html',
  styleUrls: ['./display-album.component.css']
})
export class DisplayAlbumComponent implements OnInit {
  display: string[];
  id: string;
  album: Observable<Album>;
  constructor(private _activatedRoute: ActivatedRoute, private http: HttpClient) { }

  ngOnInit() {
    this._activatedRoute.paramMap.subscribe(params => {
      this.id = params.get('id');
    });

    var url = 'api/Album/GetAlbumById?id=' + this.id + "&includeSongInfos=true";
    this.http.get<Album>(url).subscribe(result => {
      this.album = of(result).pipe();
      var length = 0;
      this.album.subscribe(val => length = val.ImagePaths.length);
      this.display = new Array(length);
      for(let i = 0; i < length ; i++){
        this.display[i] = 'none';
      }
    }, error => console.error(error));
  }  

  openModalImg(i: number){
    this.display[i] = 'block';
  }

  onCloseHandled(i: number){  
    this.display[i] = 'none';
  }
}

interface Album{
  id: number;
  name: string;
  year: Date;
  ImagePaths: string[];  
  SongInfos: Observable<SongInfo>;
}

interface SongInfo {
  id: number;
  name: string;
  author: string;
  duration: Time;
  genre: string;
  bitRate: string;
  timesPlayed: number;
  albumName: string;
  albumYear: Date;
  albumImagePath: string[];
  fullPath: string;
  fullName: string;
  blobFileReference: string;
}
