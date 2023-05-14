import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { RegistrationComponent } from "./registration/registration.component";
import { HomeComponent } from "./home/home.component";
import { EmployeeRequestsService } from "./services/employee-requests.service";
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { DepartmentRequestsService } from "./services/department-requests.service";
import { ReactiveFormsModule } from "@angular/forms";
import { EditemployeeComponent } from "./editemployee/editemployee.component";
import { LoginComponent } from "./login/login.component";
import { JwtModule } from "@auth0/angular-jwt";
import { AuthGuardService } from "./guards/auth-guard.service";
import { NavbarComponent } from "./navbar/navbar.component";
import { NotfoundComponent } from "./notfound/notfound.component";
import { PasswordHasherService } from "./services/password-hasher.service";

export function tokenGetter() {
  return localStorage.getItem("jwt");
}

@NgModule({
  declarations: [
    AppComponent,
    RegistrationComponent,
    HomeComponent,
    EditemployeeComponent,
    LoginComponent,
    NavbarComponent,
    NotfoundComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ["localhost:44382"],
        blacklistedRoutes: [],
      },
    }),
  ],
  providers: [
    EmployeeRequestsService,
    DepartmentRequestsService,
    AuthGuardService,
    PasswordHasherService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
