import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AlertModule } from 'ngx-bootstrap';
import { DisplaySongComponent } from './display-song/display-song.component';
import { LatestSongsComponent } from './latest-songs/latest-songs.component';
import { AlbumsAllComponent } from './albums-all/albums-all.component';
import { DisplayAlbumComponent } from './display-album/display-album.component';

@NgModule({
  declarations: [
    AppComponent,
    DisplaySongComponent,
    LatestSongsComponent,
    AlbumsAllComponent,
    DisplayAlbumComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    AlertModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
