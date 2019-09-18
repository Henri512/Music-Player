import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DisplaySongComponent } from "./display-song/display-song.component";
import { LatestSongsComponent } from './latest-songs/latest-songs.component';

const routes: Routes = [
  {path: '', redirectTo: '/latest-songs', pathMatch: 'full'},
  { path: 'latest-songs', component: LatestSongsComponent },
  { path: 'displaySong/:id', component: DisplaySongComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
