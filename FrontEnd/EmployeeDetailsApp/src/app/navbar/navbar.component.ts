import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

@Component({
  selector: "app-navbar",
  templateUrl: "./navbar.component.html",
  styleUrls: ["./navbar.component.css"],
})
export class NavbarComponent implements OnInit {
  title: string = "Employee Details App";
  constructor(private router: Router) {}

  ngOnInit() {}

  logout() {
    localStorage.removeItem("jwt");
    this.router.navigate(["/login"]);
  }
  doNothing() {}
}
