import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from "./app.component";
import { DisplaySongComponent } from "./display-song/display-song.component";


const routes: Routes = [
  { path: 'home', component: AppComponent },
  { path: 'displaySong/:id', component: DisplaySongComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
