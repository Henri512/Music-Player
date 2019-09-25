import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Time } from '@angular/common';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-display-song',
  templateUrl: './display-song.component.html',
  styleUrls: ['./display-song.component.css']
})

export class DisplaySongComponent implements OnInit {
  display: string[];
  id: string;
  songInfo: Observable<SongInfo>;
  constructor(private _activatedRoute: ActivatedRoute, private http: HttpClient) { }

  ngOnInit() {
    this._activatedRoute.paramMap.subscribe(params => {
      this.id = params.get('id');
    });

    var url = 'api/SongInfo/GetSongInfoById?id=' + this.id + "&includeAlbum=true";
    this.http.get<SongInfo>(url).subscribe(result => {
      this.songInfo = of(result).pipe(); 
      var length = 0;
      this.songInfo.subscribe(val => length = val.albumImagePath.length);
      this.display = new Array(length);
      for(let i = 0; i < length ; i++){
        this.display[i] = 'none';
      }
    }, error => console.error(error));
  }

  playSong(songInfo: SongInfo) {
    console.log("Song playing");
    // timer should be started here
  }

  pauseSong(songInfo: SongInfo) {
    console.log("Song pausing");
    // if song is already playing for some time(30 seconds for an example) or more
    // we will increase the playCount of that song in the database
  }

  openModalImg(i:number){
    this.display[i] = 'block';
  }

  onCloseHandled(i:number){  
    this.display[i] = 'none';
  }
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
