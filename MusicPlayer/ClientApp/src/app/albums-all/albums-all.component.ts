import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpParams, HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-albums-all',
  templateUrl: './albums-all.component.html',
  styleUrls: ['./albums-all.component.css']
})
export class AlbumsAllComponent implements OnInit {
  albums: Album[] = [];
  constructor(private _activatedRoute: ActivatedRoute, private http: HttpClient) { }

  ngOnInit() {
    var url = 'api/Album/GetAlbums';
    const params = new HttpParams()
      .set('includeSongInfos', 'false');

    this.http.get<Album[]>(url, { params }).subscribe(result => {
      this.albums = result;
      console.log(this.albums[0]);
    }, error => console.error(error));
  }

}

interface Album{
  id: number;
  name: string;
  year: Date;
  ImagePath: string[];  
}
