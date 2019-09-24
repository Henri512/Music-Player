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
  display: string = 'none';
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
    }, error => console.error(error));
  }

  cacheSong(songInfo: SongInfo) {
    console.log("Kesiranje pesme " + songInfo.name);

    var url = 'api/SongInfo/CacheSongFile?id=' + songInfo.id + "&cacheFolder=../assets/cache/";
    this.http.get<boolean>(url).subscribe(result => {
      if (result) {
        console.log("Song cached!");
      } else {
        console.log("Song already exist!");
      }
    }, error => console.error(error));
  }

  getDecodedSongPath(songFullName: string): string {
    return decodeURIComponent('../assets/cache/' + songFullName);
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

  openModalImg(){
    this.display = 'block';
  }

  onCloseHandled(){  
    this.display = 'none';
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
