import { Component, OnInit} from '@angular/core';
import { HttpClient, HttpParams  } from '@angular/common/http';
import { Time } from '@angular/common';
import { CountdownModule } from 'ngx-countdown';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  latestSongs: SongInfo[] = [];
  //timers: Map<string, CountdownModule> = new Map<string, CountdownModule>();
  constructor(private http: HttpClient) { }

  ngOnInit() {
    var url = 'https://localhost:44385/api/SongInfo/GetSongInfos';
    const params = new HttpParams()
      .set('includeAlbum', 'true');
    this.http.get<SongInfo[]>(url, { params}).subscribe(result => {
      this.latestSongs = result;
      console.log(this.latestSongs[0]);
    }, error => console.error(error));
  }
  
  cacheSong(audio: HTMLAudioElement,  songInfo: SongInfo) {
    var cachedSongPath = decodeURIComponent('../assets/cache/' + songInfo.fullName);

      var url = 'https://localhost:44385/api/SongInfo/CacheSongFile';
      const params = new HttpParams()
        .set('id', songInfo.id.toString())
        .set('cachedSongPath', cachedSongPath);
      this.http.get<boolean>(url, { params }).subscribe(result => {
        if (result) {
          console.log("Song cached!");
        } else {
          console.log("Song already exist!");
        }
        audio.src = cachedSongPath;
      }, error => console.error(error));
  }
  
  getDecodedSongPath(songFullName: string): string {
    return decodeURIComponent('../assets/cache/' + songFullName);
  }

  playSong(songInfo: SongInfo) {
    // timer should be started here
  }

  pauseSong(songInfo: SongInfo) {
    // if song is already playing for some time(30 seconds for an example) or more
    // we will increase the playCount of that song in the database
  }

}

interface SongInfo {
  id: number;
  name: string;
  author: string;
  duration: Time;
  genre: string;
  bitRate: number;
  timesPlayed: number;
  albumName: string;
  albumYear: Date;
  albumImagePath: string;
  fullPath: string;
  fullName: string;
}
