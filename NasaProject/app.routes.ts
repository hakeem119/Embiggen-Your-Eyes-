import { Routes } from '@angular/router';
import {HomeComponent} from './components/home/home.component';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', loadComponent: () => import("./components/home/home.component").then((c) => c.HomeComponent), title: "Home", },
  { path: 'planet', loadComponent: () => import("./components/planet/planet.component").then((c) => c.PlanetComponent), title: "Mars Planet" },
  { path: 'information', loadComponent: () => import("./components/information/information.component").then((c) => c.InformationComponent), title: "Information" },
  {
    path: '**',
    loadComponent: () => import("./components/home/home.component")
      .then((c) => c.HomeComponent),
    title: "Not Found"
  }
];
