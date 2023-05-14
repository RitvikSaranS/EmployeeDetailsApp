import { Component, OnInit } from "@angular/core";
import { EmployeeRequestsService } from "../services/employee-requests.service";
import { Employee } from "../Models/Employee";
import { Department } from "../Models/Department";
import { DepartmentRequestsService } from "../services/department-requests.service";
import { ActivatedRoute, Router } from "@angular/router";
import { JwtHelperService } from "@auth0/angular-jwt";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"],
})
export class HomeComponent implements OnInit {
  public employeeList: Employee[];
  public deptList: Department[];
  constructor(
    private empreq: EmployeeRequestsService,
    private deptreq: DepartmentRequestsService,
    private router: Router,
    private route: ActivatedRoute,
    private jwtHelper: JwtHelperService
  ) {}
  ngOnInit() {
    this.empreq.getEmployee().subscribe((item) => (this.employeeList = item));
    this.deptreq.getDepartment().subscribe((item) => (this.deptList = item));
  }

  isAuthenticated() {
    const token: string = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    } else {
      return false;
    }
  }

  removeEmployee(employee: Employee) {
    if (confirm(`Do you wanna delete ${employee.name}`) == true) {
      this.empreq.deleteEmployee(employee.empid).subscribe(() => {
        this.empreq
          .getEmployee()
          .subscribe((item) => (this.employeeList = item));
        alert("Employee Deleted Successfully");
      });
    }
  }

  editEmployee(employee: Employee) {
    this.router.navigate(["/edit", employee.empid]);
  }
}
