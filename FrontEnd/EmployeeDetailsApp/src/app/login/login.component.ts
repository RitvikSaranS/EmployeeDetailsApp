import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { environment } from "src/environments/environment";
import { FormControl, FormGroup, NgForm, Validators } from "@angular/forms";
import { PasswordHasherService } from "../services/password-hasher.service";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"],
})
export class LoginComponent implements OnInit {
  passwordType: string = "password";
  register: boolean;
  invalidLogin: boolean;
  invalidRegistration: boolean;
  loginForm: FormGroup;
  registerUserBody: any = {
    username: null,
    passwordHash: null,
    empId: null,
  };
  constructor(
    private router: Router,
    private http: HttpClient,
    private hasher: PasswordHasherService
  ) {}

  ngOnInit() {
    this.loginForm = new FormGroup({
      username: new FormControl("", Validators.required),
      password: new FormControl("", Validators.required),
    });
  }

  toggleRegLog() {
    if (this.register) {
      this.register = false;
    } else {
      this.register = true;
    }
  }

  togglePasswordVisibility() {
    this.passwordType = this.passwordType == "password" ? "text" : "password";
  }

  onSubmit(form: NgForm) {
    const credentials = {
      username: this.loginForm.value.username,
      passwordHash: this.hasher.hashPassword(this.loginForm.value.password),
    };
    this.registerUserBody.empId = null;
    this.registerUserBody.username = this.loginForm.value.username;
    this.registerUserBody.passwordHash = this.hasher.hashPassword(
      this.loginForm.value.password
    );
    if (this.register) {
      const httpOptions = {
        headers: new HttpHeaders({
          "Content-Type": "application/json",
          Authorization: "my-auth-token",
        }),
      };
      this.http
        .post(
          environment.API_URL + "/register",
          JSON.stringify(this.registerUserBody),
          httpOptions
        )
        .subscribe(
          (status) => {
            if (status) {
              this.invalidRegistration = false;
              alert("Registered user successfully");
            } else {
              this.invalidRegistration = true;
            }
            this.register = false;
          },
          (err) => {
            this.invalidLogin = true;
          }
        );
    } else {
      this.http
        .post(environment.API_URL + "/auth/login", credentials)
        .subscribe(
          (res) => {
            const token = (<any>res).token;
            localStorage.setItem("jwt", token);
            this.invalidLogin = false;
            this.router.navigate(["home"]);
          },
          (err) => {
            this.invalidLogin = true;
          }
        );
    }
    this.register = false;
  }
}
