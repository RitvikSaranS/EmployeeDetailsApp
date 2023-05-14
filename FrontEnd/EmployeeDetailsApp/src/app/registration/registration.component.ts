import { Component, OnInit, ViewChild } from "@angular/core";
import { DepartmentRequestsService } from "../services/department-requests.service";
import { Department } from "../Models/Department";
import { FormGroup, FormControl, Validators, NgForm } from "@angular/forms";
import { EmployeeRequestsService } from "../services/employee-requests.service";
import { Employee } from "../Models/Employee";

@Component({
  selector: "app-registration",
  templateUrl: "./registration.component.html",
  styleUrls: ["./registration.component.css"],
})
export class RegistrationComponent implements OnInit {
  deptList: Department[];
  registrationForm: FormGroup;
  tmpEmployee: Employee = {
    empid: null,
    name: null,
    age: null,
    gender: null,
    dob: null,
    salary: null,
    address: null,
    deptId: null,
    createdBy: null,
    createdDate: null,
  };
  constructor(
    private deptreq: DepartmentRequestsService,
    private empreq: EmployeeRequestsService
  ) {}

  ngOnInit() {
    this.deptreq.getDepartment().subscribe((department) => {
      this.deptList = department;
    });
    this.registrationForm = new FormGroup({
      name: new FormControl("", Validators.required),
      age: new FormControl("", Validators.required),
      gender: new FormControl("", Validators.required),
      dob: new FormControl("", Validators.required),
      salary: new FormControl("", Validators.required),
      address: new FormControl("", Validators.required),
      department: new FormControl("", Validators.required),
      createdBy: new FormControl("", Validators.required),
    });
  }
  onSubmit(form: NgForm) {
    console.log(this.registrationForm.value);
    this.tmpEmployee.name = this.registrationForm.value.name;
    this.tmpEmployee.age = +this.registrationForm.value.age;
    this.tmpEmployee.gender = this.registrationForm.value.gender;
    this.tmpEmployee.dob = new Date(
      this.registrationForm.value.dob
    ).toISOString();
    this.tmpEmployee.salary = this.registrationForm.value.salary;
    this.tmpEmployee.address = this.registrationForm.value.address;
    this.deptList.forEach((dept) => {
      if (dept.deptName == this.registrationForm.value.department) {
        this.tmpEmployee.deptId = dept.deptId;
      }
    });
    this.tmpEmployee.createdBy = this.registrationForm.value.createdBy;
    this.tmpEmployee.createdDate = new Date().toISOString();
    this.empreq.postEmployee(this.tmpEmployee).subscribe(() => {
      console.log("Employee Added Successfully");
      this.empreq.getEmployee();
    });
    form.reset();
  }
}
