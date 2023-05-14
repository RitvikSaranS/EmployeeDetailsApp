import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { RegistrationComponent } from "./registration/registration.component";
import { EditemployeeComponent } from "./editemployee/editemployee.component";
import { LoginComponent } from "./login/login.component";
import { AuthGuardService } from "./guards/auth-guard.service";
import { NotfoundComponent } from "./notfound/notfound.component";

const routes: Routes = [
  { path: "", redirectTo: "/login", pathMatch: "full" },
  { path: "login", component: LoginComponent },
  { path: "home", component: HomeComponent, canActivate: [AuthGuardService] },
  {
    path: "registration",
    component: RegistrationComponent,
    canActivate: [AuthGuardService],
  },
  {
    path: "edit/:empid",
    component: EditemployeeComponent,
    canActivate: [AuthGuardService],
  },
  { path: "404", component: NotfoundComponent },
  { path: "**", redirectTo: "/404" },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
