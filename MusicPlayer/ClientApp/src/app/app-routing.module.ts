import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DisplaySongComponent } from "./display-song/display-song.component";
import { LatestSongsComponent } from './latest-songs/latest-songs.component';
import { AlbumsAllComponent } from './albums-all/albums-all.component';
import { DisplayAlbumComponent } from './display-album/display-album.component';

const routes: Routes = [
  {path: '', redirectTo: '/albums-all', pathMatch: 'full'},
  { path: 'latest-songs', component: LatestSongsComponent },
  { path: 'display-song/:id', component: DisplaySongComponent },
  { path: 'albums-all', component: AlbumsAllComponent },
  { path: 'display-album/:id', component: DisplayAlbumComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
