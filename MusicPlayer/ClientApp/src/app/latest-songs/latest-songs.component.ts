import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpParams } from '@angular/common/http';
import { CountdownModule } from 'ngx-countdown';
import { ActivatedRoute } from '@angular/router';
import { Time } from '@angular/common';

@Component({
  selector: 'app-latest-songs',
  templateUrl: './latest-songs.component.html',
  styleUrls: ['./latest-songs.component.css']
})

export class LatestSongsComponent implements OnInit {
  latestSongs: SongInfo[] = [];
  constructor(private _activatedRoute: ActivatedRoute, private http: HttpClient) { }

  ngOnInit() {
    var url = 'api/SongInfo/GetSongInfos';
    const params = new HttpParams()
      .set('includeAlbum', 'true');
      
    this.http.get<SongInfo[]>(url, { params}).subscribe(result => {
      this.latestSongs = result;
    }, error => console.error(error));
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
  albumImagePath: string;
  fullPath: string;
  fullName: string;
}
