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
  display: string = 'none';
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
    }, error => console.error(error));
  }
}

interface Album{
  id: number;
  name: string;
  year: Date;
  ImagePath: string[];  
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
