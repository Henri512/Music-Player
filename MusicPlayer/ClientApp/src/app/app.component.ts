import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpParams  } from '@angular/common/http';
import { Time } from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
  
export class AppComponent implements OnInit {
  latestSongs: SongInfo[] = [];
  constructor(private http: HttpClient) { }

  ngOnInit() {
    var url = 'https://localhost:44385/api/SongInfo/GetSongInfos';
    const params = new HttpParams()
      .set('includeAlbum', 'true');
    this.http.get<SongInfo[]>(url, { params}).subscribe(result => {
      this.latestSongs = result;
    }, error => console.error(error));
  }

}

interface SongInfo {
  name: string;
  author: string;
  album: Album;
  duration: Time;
  genre: string;
  bitRate: number;
  timesPlayed: number;
}

interface Album {
  name: string;
  year: Date;
}
